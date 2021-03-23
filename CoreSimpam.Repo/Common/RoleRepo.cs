﻿using CoreSimpam.Model;
using CoreSimpam.Model.Data;
using CoreSimpam.ViewModel;
using CoreSimpam.ViewModel.Query;
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
        Task<Metadata<string>> Insert(RoleViewModel model);
        Task<Metadata<string>> Update(RoleViewModel model);
        Task<Metadata<string>> Delete(long RoleID);
    }
    public class RoleRepo : IRoleRepo
    {
        private readonly SimpamDBContext context;

        public RoleRepo(SimpamDBContext dBContext)
        {
            context = dBContext;
        }
        public async Task<Metadata<string>> Delete(long RoleID)
        {
            Metadata<string> res = new Metadata<string>();
            try
            {
                var dataRoles = await context.Roles.FirstOrDefaultAsync(x => x.RoleID == RoleID);
                if (dataRoles == null)
                    return new Metadata<string>() { status = false, data = "Data not found" };

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
            var dataRoles = context.Roles.Where(x => x.RoleID > 1).Select(x => new RoleViewModel()
            {
                RoleID = x.RoleID,
                RoleName = x.RoleName,
                IsEnabled = x.IsEnabled
            }).ToList();
            res.data = new RoleViewModel();
            res.data.Roles = dataRoles.OrderBy(s => s.RoleName).ToList(); 
            res.status = true;
            return await Task.FromResult(res);
        }

        public async Task<Metadata<RoleViewModel>> GetByID(long RoleID)
        {
            Metadata<RoleViewModel> res = new Metadata<RoleViewModel>();
            var dataRoles = await context.Roles.FirstOrDefaultAsync(x => x.RoleID == RoleID);
            res.data = new RoleViewModel() { 
                RoleID = dataRoles.RoleID,
                IsEnabled = dataRoles.IsEnabled,
                RoleName = dataRoles.RoleName
            };
            res.status = true;
            return res;
        }

        public async Task<Metadata<string>> Insert(RoleViewModel model)
        {
            Metadata<string> res = new Metadata<string>();
            try
            {
                var dataRoles = await context.Roles.AnyAsync(x=> x.RoleName.Contains(model.RoleName));
                if (dataRoles)
                    return new Metadata<string>() { status = false, data = "Role name is ready" };

                await context.Roles.AddAsync(new Model.RoleModel()
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

        public async Task<Metadata<string>> Update(RoleViewModel model)
        {
            Metadata<string> res = new Metadata<string>();
            try
            {
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
