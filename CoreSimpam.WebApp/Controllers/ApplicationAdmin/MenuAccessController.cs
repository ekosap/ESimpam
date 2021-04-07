using CoreSimpam.Repo;
using CoreSimpam.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Controllers.ApplicationAdmin
{
    public class MenuAccessController : Controller
    {
        private RoleScreenViewModel RoleScreenViewModel
        {
            get
            {
                if (ViewBag.rolescreen == null)
                {
                    ViewBag.rolescreen = _repo.get(RoleID).data;
                }
                return (RoleScreenViewModel)ViewBag.rolescreen;
            }
            set
            {
                ViewBag.rolescreen = value ?? _repo.get(RoleID).data;
            }
        }
        private long RoleID
        {
            get
            {
                return (long)ViewBag.roleID;
            }
            set
            {
                ViewBag.roleID = value;
            }
        }
        private readonly IRoleScreenAccessRepo _repo;
        private readonly IRoleRepo _roleRepo;
        public MenuAccessController(IRoleScreenAccessRepo accessRepo, IRoleRepo roleRepo)
        {
            _repo = accessRepo;
            _roleRepo = roleRepo;
        }
        public async Task<IActionResult> RoleAsync(long id)
        {
            RoleID = id;

            var role = await _roleRepo.GetByID(id);
            var model = RoleScreenViewModel;
            model.RoleName = role.data.RoleName;
            ViewData["Title"] = $"{model.RoleName} Configuration Role Access";
            RoleScreenViewModel = model;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleAsync(RoleScreenViewModel model)
        {
            ViewData["Title"] = $"{model.RoleName} Configuration Role Access";
            if (ModelState.IsValid)
            {
                var res = await _repo.update(model);
                return RedirectToAction("index", "role");
            }
            return View(model);
        }
        public IActionResult changevalue(int type, bool value)
        {
            var data = RoleScreenViewModel;
            return Json(new { value, type, data });
        }
    }
}
