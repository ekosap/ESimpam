using CoreSimpam.Repo;
using CoreSimpam.ViewModel;
using CoreSimpam.ViewModel.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Controllers.ApplicationAdmin
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepo _repo;
        private readonly IRoleRepo _repoRole;
        public UserController(IUserRepo userRepo, IRoleRepo roleRepo)
        {
            _repo = userRepo;
            _repoRole = roleRepo;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Application User";
            return await Task.FromResult(View());
        }
        public async Task<IActionResult> GetDataAsync(JqueryDatatableParam param)
        {
            var dataUser = (await _repo.GetAll()).data;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                dataUser.users = dataUser.users.Where(
                    x => x.UserName.ToLower().Contains(param.sSearch.ToLower())
                    || x.Email.ToLower().Contains(param.sSearch.ToLower())
                    || x.RoleName.ToLower().Contains(param.sSearch.ToLower())
                    ).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["sSortDir_0"];

            Func<UserViewModel, string> orderingFunction = e =>
                sortColumnIndex == 3 ? e.RoleName :
                sortColumnIndex == 2 ? e.Email :
                sortColumnIndex == 1 ? e.UserName : e.UserID.ToString();

            dataUser.users = sortDirection == "asc" ? dataUser.users.OrderBy(orderingFunction).ToList() : dataUser.users.OrderByDescending(orderingFunction).ToList();

            var displayResult = dataUser.users.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = dataUser.users.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                Data = displayResult
            });

        }
        public async Task<IActionResult> Add()
        {
            ViewData["Title"] = "Add Application User";
            UserViewModel model = new UserViewModel();
            ViewData["roles"] = _repoRole.GetAll(new RoleQuery()).Result.data.Roles.Select(x => new SelectListItem() { Value = x.RoleID.ToString(), Text = x.RoleName }).ToList();
            return await Task.FromResult(PartialView("_Add", model));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserViewModel model)
        {
            ViewData["Title"] = "Add Application User";
            ViewData["roles"] = _repoRole.GetAll(new RoleQuery()).Result.data.Roles.Select(x => new SelectListItem() { Value = x.RoleID.ToString(), Text = x.RoleName }).ToList();
            if (ModelState.IsValid)
            {
                var user = await _repo.Insert(model);
                return Json(user);
            }
            return PartialView("_Add", model);
        }
        public async Task<IActionResult> Edit(long id)
        {
            ViewData["Title"] = "Edit Application User";
            UserViewModel model = (await _repo.GetByID(id)).data;
            ViewData["roles"] = _repoRole.GetAll(new RoleQuery()).Result.data.Roles.Select(x => new SelectListItem() { Value = x.RoleID.ToString(), Text = x.RoleName }).ToList();
            return PartialView("_Edit", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            ViewData["Title"] = "Edit Application User";
            ViewData["roles"] = _repoRole.GetAll(new RoleQuery()).Result.data.Roles.Select(x => new SelectListItem() { Value = x.RoleID.ToString(), Text = x.RoleName }).ToList();
            ModelState.SkipToValid("Password,Username");
            if (ModelState.IsValid)
            {
                var user = await _repo.Update(model);
                return Json(user);
            }
            return PartialView("_Edit", model);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var model = await _repo.Delete(id);
            return Json(model);
        }
    }
}
