using CoreSimpam.Model;
using CoreSimpam.Model.Data;
using CoreSimpam.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Repo
{
    public interface IRoleScreenAccessRepo
    {
        Metadata<RoleScreenViewModel> get(long RoleID);
        Task<Metadata> update(RoleScreenViewModel model);
        Metadata AllowPermission(string Controller, string Action, string Method, int Accesslevel = 0);
        Task<Metadata<UserMenuViewModel>> GetMenuAsync();
    }
    public class RoleScreenAccessRepo : IRoleScreenAccessRepo
    {
        private readonly HttpContext _httpContext;
        private readonly SimpamDBContext _context;

        public RoleScreenAccessRepo(IHttpContextAccessor HttpContextAccessor, SimpamDBContext dBContext)
        {
            _httpContext = HttpContextAccessor.HttpContext;
            _context = dBContext;
        }

        public Metadata AllowPermission(string Controller, string Action, string Method, int Accesslevel = 0)
        {
            Metadata res = new Metadata();
            if (Accesslevel == 0)
                switch (Method)
                {
                    case "GET":
                        Accesslevel = 0;
                        break;
                    case "POST":
                    case "PUT":
                        Accesslevel = 1;
                        break;
                    case "DELETE":
                        Accesslevel = 2;
                        break;
                }
            try
            {
                ScreenViewModel dataMenu = (from s in _context.Screen
                            join sr in _context.RoleScreen on s.ScreenID equals sr.ScreenID
                            join r in _context.Roles on sr.RoleID equals r.RoleID
                            where s.ControllerName == Controller && s.ActionName == Action
                            && r.RoleName == _httpContext.User.GetUserRole() && ((Accesslevel == 0
                            && sr.ReadFlag == true) || (Accesslevel == 1 && sr.WriteFlag == true)
                            || (Accesslevel == 2 && sr.DeleteFlag == true))
                            select new ScreenViewModel()
                            {
                                ActionName = s.ActionName,
                                ControllerName = s.ControllerName,
                                IsActive = s.IsActive,
                                IsMenu = s.IsMenu,
                                ParentID = s.ParentID,
                                ScreenID = s.ScreenID,
                                ScreenName = s.ScreenName
                            }).FirstOrDefault();
                res.status = dataMenu != null;
            }
            catch (Exception e)
            {
                res.data = e.Message.ToString();
            }
            return res;
        }

        public Metadata<RoleScreenViewModel> get(long RoleID)
        {
            Metadata<RoleScreenViewModel> metadata = new Metadata<RoleScreenViewModel>();
            metadata.data.RoleID = RoleID;
            var result = (from s in _context.Screen
                          join rs in _context.RoleScreen on new { screenID = s.ScreenID, roleID = RoleID } equals new { screenID = rs.ScreenID, roleID = rs.RoleID } into screenRole
                          from srs in screenRole.DefaultIfEmpty()
                          select new ScreenComponentViewModel
                          {
                              RoleID = srs == null ? 0 : srs.RoleID,
                              ScreenID = s.ScreenID,
                              ScreenName = s.ScreenName,
                              DeleteFlag = srs == null ? false : srs.DeleteFlag,
                              WriteFlag = srs == null ? false : srs.WriteFlag,
                              ReadFlag = srs == null ? false : srs.ReadFlag,
                          }).ToList();
            metadata.status = true;
            metadata.data.Screens = result;
            return metadata;
        }

        public async Task<Metadata<UserMenuViewModel>> GetMenuAsync()
        {
            Metadata<UserMenuViewModel> res = new Metadata<UserMenuViewModel>();
            res.status = _httpContext.User.Identity.IsAuthenticated;
            if (_httpContext.User.Identity.IsAuthenticated)
            {
                var dataMenu = (from s in _context.Screen
                                join sr in _context.RoleScreen on s.ScreenID equals sr.ScreenID
                                join r in _context.Roles on sr.RoleID equals r.RoleID
                                where s.IsActive == true && s.IsMenu == true
                                && r.RoleName == _httpContext.User.GetUserRole()
                                select new ScreenViewModel()
                                {
                                    ActionName = s.ActionName,
                                    ControllerName = s.ControllerName,
                                    IsActive = s.IsActive,
                                    IsMenu = s.IsMenu,
                                    ParentID = s.ParentID,
                                    ScreenID = s.ScreenID,
                                    ScreenName = s.ScreenName
                                }).ToListAsync();
                res.data.Menu = await dataMenu;
                res.data.CountChild = res.data.Menu.Count;
            }
            return res;
        }

        public async Task<Metadata> update(RoleScreenViewModel model)
        {
            Metadata res = new Metadata();
            if (model.RoleID == 0 || model.Screens == null || model.Screens.Count == 0)
            {
                return new Metadata() { data = "Application not allowed" };
            };
            try
            {
                _context.RoleScreen.RemoveRange(_context.RoleScreen.Where(x => x.RoleID == model.RoleID));
                List<RoleScreenModel> lsModel = new List<RoleScreenModel>();
                model.Screens.ForEach(rolescreen =>
                {
                    lsModel.Add(new RoleScreenModel()
                    {
                        RoleID = model.RoleID,
                        ScreenID = rolescreen.ScreenID,
                        DeleteFlag = rolescreen.DeleteFlag,
                        ReadFlag = rolescreen.ReadFlag,
                        WriteFlag = rolescreen.WriteFlag
                    });
                });
                _context.RoleScreen.AddRange(lsModel);
                int affect = await _context.SaveChangesAsync();
                res.status = true;
            }
            catch (Exception e)
            {
                res.data = e.Message.ToString();
            }
            return res;
        }
    }
}
