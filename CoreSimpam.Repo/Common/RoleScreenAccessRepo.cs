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
        Metadata AllowPermission(string Controller, string Action, AccessLevel Accesslevel = AccessLevel.AllowRead);
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

        public Metadata AllowPermission(string Controller, string Action, AccessLevel Accesslevel = AccessLevel.AllowRead)
        {
            Metadata res = new Metadata();
            try
            {
                ScreenViewModel dataMenu = (from s in _context.Screen
                            join sr in _context.RoleScreen on s.screenid equals sr.screenid
                            join r in _context.Roles on sr.roleid equals r.roleid
                            where s.controllername.ToLower().Replace(" ","") == Controller.ToLower().Replace(" ", "") /*&& s.ActionName == Action*/
                            && r.rolename.ToLower().Replace(" ", "") == _httpContext.User.GetUserRole().ToLower().Replace(" ", "") && ((Accesslevel == AccessLevel.AllowRead && sr.readflag == true) || (Accesslevel == AccessLevel.AllowWrite && sr.writeflag == true)
                            || (Accesslevel == AccessLevel.AllowDelete && sr.deleteflag == true))
                            select new ScreenViewModel()
                            {
                                ActionName = s.actionname,
                                ControllerName = s.controllername,
                                IsActive = s.isactive,
                                IsMenu = s.ismenu,
                                ParentID = s.parentid,
                                ScreenID = s.screenid,
                                ScreenName = s.screenname
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
                          join rs in _context.RoleScreen on new { screenID = s.screenid, roleID = RoleID } equals new { screenID = rs.screenid, roleID = rs.roleid } into screenRole
                          from srs in screenRole.DefaultIfEmpty()
                          select new ScreenComponentViewModel
                          {
                              RoleID = srs == null ? 0 : srs.roleid,
                              ScreenID = s.screenid,
                              ScreenName = s.screenname,
                              DeleteFlag = srs == null ? false : srs.deleteflag,
                              WriteFlag = srs == null ? false : srs.writeflag,
                              ReadFlag = srs == null ? false : srs.readflag,
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
                                join sr in _context.RoleScreen on s.screenid equals sr.screenid
                                join r in _context.Roles on sr.roleid equals r.roleid
                                where s.isactive == true && s.ismenu == true
                                && r.rolename.ToLower().Replace(" ", "") == _httpContext.User.GetUserRole().ToLower().Replace(" ", "")
                                select new ScreenViewModel()
                                {
                                    ActionName = s.actionname,
                                    ControllerName = s.controllername,
                                    IsActive = s.isactive,
                                    IsMenu = s.ismenu,
                                    ParentID = s.parentid,
                                    ScreenID = s.screenid,
                                    ScreenName = s.screenname,
                                    AllowDelete = sr.deleteflag,
                                    AllowRead = sr.readflag,
                                    AllowWrite = sr.writeflag,
                                    IconCss = s.iconcss
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
                _context.RoleScreen.RemoveRange(_context.RoleScreen.Where(x => x.roleid == model.RoleID));
                List<RoleScreenModel> lsModel = new List<RoleScreenModel>();
                model.Screens.ForEach(rolescreen =>
                {
                    lsModel.Add(new RoleScreenModel()
                    {
                        roleid = model.RoleID,
                        screenid = rolescreen.ScreenID,
                        deleteflag = rolescreen.DeleteFlag,
                        readflag = rolescreen.ReadFlag,
                        writeflag = rolescreen.WriteFlag
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
