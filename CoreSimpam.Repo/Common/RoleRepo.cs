using CoreSimpam.Model;
using CoreSimpam.Model.Data;
using CoreSimpam.ViewModel;
using CoreSimpam.ViewModel.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Repo
{
    public interface IRoleRepo
    {
        Task<Metadata<RoleViewModel>> GetAll(RoleQuery query);
        Task<Metadata<RoleViewModel>> GetByID(long RoleID);
        Task<Metadata> Insert(RoleViewModel model);
        Task<Metadata> Update(RoleViewModel model);
        Task<Metadata> Delete(long RoleID);
    }
    public class RoleRepo : IRoleRepo
    {
        private readonly SimpamDBContext context;
        private readonly HttpContext _httpContext;

        public RoleRepo(SimpamDBContext dBContext, IHttpContextAccessor HttpContextAccessor)
        {
            context = dBContext;
            _httpContext = HttpContextAccessor.HttpContext;
        }
        public async Task<Metadata> Delete(long RoleID)
        {
            Metadata res = new Metadata();
            try
            {
                var dataRoles = await context.Roles.FirstOrDefaultAsync(x => x.RoleID == RoleID);
                if (dataRoles == null)
                    return new Metadata() { status = false, data = "Data not found" };

                context.Remove(dataRoles);
                await context.SaveChangesAsync();
                res.status = true;
                res.data = "Data was successfully deleted";
            }
            catch (Exception ex)
            {
                res.status = false;
                res.data = ex.Message;
            }
            return res;
        }

        public async Task<Metadata<RoleViewModel>> GetAll(RoleQuery query)
        {
            Metadata<RoleViewModel> res = new Metadata<RoleViewModel>();
            //var data = context.Roles.Include("RoleScreen");
            List<RoleViewModel> dataRoles = new List<RoleViewModel>();
            if (_httpContext.User.Identity.Name == "root")
            {
                dataRoles = context.Roles.Select(x => new RoleViewModel()
                {
                    RoleID = x.RoleID,
                    RoleName = x.RoleName,
                    IsEnabled = x.IsEnabled,
                    StringRoleID = x.RoleID.ToString().ToBase64(),
                    CountScreen = context.RoleScreen.Where(y => y.RoleID == x.RoleID).Count(p => p.RoleScreenID > 0)
                }).ToList();
            }
            else
            {
                dataRoles = context.Roles.Where(x => x.RoleID > 1).Select(x => new RoleViewModel()
                {
                    RoleID = x.RoleID,
                    RoleName = x.RoleName,
                    IsEnabled = x.IsEnabled,
                    StringRoleID = x.RoleID.ToString().ToBase64(),
                    CountScreen = context.RoleScreen.Where(y => y.RoleID == x.RoleID).Count(p => p.RoleScreenID > 0)
                }).ToList();
            }
            res.data.Roles = dataRoles.OrderBy(s => s.RoleName).ToList();
            res.status = true;
            return await Task.FromResult(res);
        }

        public async Task<Metadata<RoleViewModel>> GetByID(long RoleID)
        {
            Metadata<RoleViewModel> res = new Metadata<RoleViewModel>();
            var dataRoles = await context.Roles.FirstOrDefaultAsync(x => x.RoleID == RoleID);
            if (dataRoles != null)
            {
                res.data = new RoleViewModel()
                {
                    RoleID = dataRoles.RoleID,
                    IsEnabled = dataRoles.IsEnabled,
                    RoleName = dataRoles.RoleName,
                    StringRoleID = dataRoles.RoleID.ToString().ToBase64()
                };
                res.status = true;
            }
            return res;
        }

        public async Task<Metadata> Insert(RoleViewModel model)
        {
            Metadata res = new Metadata();
            try
            {
                var dataRoles = await context.Roles.AnyAsync(x => x.RoleName.Contains(model.RoleName) && x.RoleID != model.RoleID);
                if (dataRoles)
                    return new Metadata() { status = false, data = "Role name is ready" };

                await context.Roles.AddAsync(new RoleModel()
                {
                    RoleName = model.RoleName,
                    IsEnabled = model.IsEnabled
                });
                await context.SaveChangesAsync();
                res.status = true;
                res.data = "Data was successfully inserted";
            }
            catch (Exception ex)
            {
                res.status = false;
                res.data = ex.Message;
            }

            return res;
        }

        public async Task<Metadata> Update(RoleViewModel model)
        {
            Metadata res = new Metadata();
            try
            {
                var data = await context.Roles.AnyAsync(x => x.RoleName.Contains(model.RoleName) && x.RoleID != model.RoleID);
                if (data)
                    return new Metadata() { status = false, data = "Role name is ready" };

                var dataRoles = await context.Roles.FirstOrDefaultAsync(x => x.RoleID == model.RoleID);

                dataRoles.RoleName = model.RoleName;
                dataRoles.IsEnabled = model.IsEnabled;

                await context.SaveChangesAsync();
                res.status = true;
                res.data = "Data was successfully inserted";
            }
            catch (Exception ex)
            {
                res.status = false;
                res.data = ex.Message;
            }

            return res;
        }
    }
}
