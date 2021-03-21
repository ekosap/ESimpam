using CoreSimpam.ViewModel;
using CoreSimpam.ViewModel.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Repo
{
    public interface IRoleRepo
    {
        Task<Metadata<List<RoleViewModel>>> GetAll(RoleQuery query);
        Task<Metadata<RoleViewModel>> GetByID(long RoleID);
        Task<Metadata<RoleViewModel>> Insert(RoleViewModel model);
        Task<Metadata<RoleViewModel>> Update(RoleViewModel model);
        Task<Metadata<RoleViewModel>> Delete(RoleViewModel model);
    }
    public class RoleRepo : IRoleRepo
    {
        public async Task<Metadata<RoleViewModel>> Delete(RoleViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<Metadata<List<RoleViewModel>>> GetAll(RoleQuery query)
        {
            Metadata<List<RoleViewModel>> res = new Metadata<List<RoleViewModel>>();
            res.data = new List<RoleViewModel>();
            res.data.Add(new RoleViewModel()
            {
                RoleID = 1,
                RoleName = "root",
                IsEnabled = true
            });
            res.status = true;
            return await Task.FromResult(res);
        }

        public async Task<Metadata<RoleViewModel>> GetByID(long RoleID)
        {
            throw new NotImplementedException();
        }

        public async Task<Metadata<RoleViewModel>> Insert(RoleViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<Metadata<RoleViewModel>> Update(RoleViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
