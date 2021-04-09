using CoreSimpam.Repo;
using CoreSimpam.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Controllers.ApplicationAdmin
{
    [AuthorizeWebAttributes]
    public class MenuAccessController : Controller
    {
        private readonly IRoleScreenAccessRepo _repo;
        private readonly IRoleRepo _roleRepo;
        public MenuAccessController(IRoleScreenAccessRepo accessRepo, IRoleRepo roleRepo)
        {
            _repo = accessRepo;
            _roleRepo = roleRepo;
        }
        public async Task<IActionResult> RoleAsync(long id)
        {
            var role = await _roleRepo.GetByID(id);
            var model = _repo.get(id).data;
            model.RoleName = role.data.RoleName;
            ViewData["Title"] = $"{model.RoleName} Configuration Role Access";
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeWebAttributes(AccessLevel = AccessLevel.AllowWrite)]
        public async Task<IActionResult> RoleAsync(RoleScreenViewModel model)
        {
            ViewData["Title"] = $"{model.RoleName} Configuration Role Access";
            if (ModelState.IsValid)
            {
                var res = await _repo.update(model);
                return Json(res);
            }
            return View(model);
        }
    }
}
