using CoreSimpam.Repo;
using CoreSimpam.ViewModel;
using CoreSimpam.ViewModel.Query;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Controllers.ApplicationAdmin
{
    public class RoleController : Controller
    {
        private readonly IRoleRepo _repo;

        public RoleController(IRoleRepo repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Application Role";
            RoleViewModel model = new RoleViewModel();
            var respon = await  _repo.GetAll(new RoleQuery());
            if (respon.status) model.Roles = respon.data;
            return View(model);
        }
        public async Task<IActionResult> Add()
        {
            ViewData["Title"] = "Add Application Role";
            return await Task.FromResult(PartialView("_Add"));
        }
    }
}
