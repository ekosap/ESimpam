using CoreSimpam.Repo;
using CoreSimpam.ViewModel;
using CoreSimpam.ViewModel.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Controllers.ApplicationAdmin
{
    [AuthorizeWebAttributes]
    public class RoleController : Controller
    {
        private RoleViewModel Roles
        {
            get
            {
                if (ViewData["roles"] == null)
                {
                    ViewData["roles"] = _repo.GetAll(new RoleQuery()).Result.data;
                }
                return (RoleViewModel)ViewData["roles"];
            }
            set
            {
                ViewData["roles"] = value ?? _repo.GetAll(new RoleQuery()).Result.data;
            }
        }
        private readonly IRoleRepo _repo;

        public RoleController(IRoleRepo repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Application Role";
            return await Task.FromResult(View());
        }
        public IActionResult GetData(JqueryDatatableParam param)
        {
            var dataroles = Roles;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                dataroles.Roles = dataroles.Roles.Where(
                    x => x.RoleName.ToLower().Contains(param.sSearch.ToLower())
                    || (x.IsEnabled ? "Enable" : "Disable").ToLower().Contains(param.sSearch.ToLower())
                    ).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["sSortDir_0"];

            Func<RoleViewModel, string> orderingFunction = e =>
                sortColumnIndex == 2 ? (e.IsEnabled ? "Enable" : "Disable") :
                sortColumnIndex == 1 ? e.RoleName : e.RoleID.ToString();

            dataroles.Roles = sortDirection == "asc" ? dataroles.Roles.OrderBy(orderingFunction).ToList() : dataroles.Roles.OrderByDescending(orderingFunction).ToList();

            var displayResult = dataroles.Roles.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = dataroles.Roles.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                Data = displayResult
            });

        }

        [AuthorizeWebAttributes(AccessLevel = AccessLevel.AllowWrite)]
        public async Task<IActionResult> Add()
        {
            ViewData["Title"] = "Add Application Role";
            return await Task.FromResult(PartialView("_Add"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleViewModel model)
        {
            ViewData["Title"] = "Add Application Role";
            if (ModelState.IsValid)
            {
                var res = await _repo.Insert(model);
                if (res.status) Roles = null;
                return Json(res);
            }
            return PartialView("_Add", model);
        }
        [AuthorizeWebAttributes(AccessLevel = AccessLevel.AllowWrite)]
        public async Task<IActionResult> Edit(string id)
        {
            ViewData["Title"] = "Edit Application Role";
            if (!long.TryParse(id.FromBase64(), out long RoleID)) return Json(new Metadata() { status = false, data = "RoleID not allow" });
            var model = await _repo.GetByID(RoleID);
            return PartialView("_Edit", model.data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            ViewData["Title"] = "Edit Application Role";
            if (ModelState.IsValid)
            {
                var res = await _repo.Update(model);
                if (res.status) Roles = null;
                return Json(res);
            }
            return PartialView("_Edit", model);
        }
        [AuthorizeWebAttributes(AccessLevel = AccessLevel.AllowDelete)]
        public async Task<IActionResult> Delete(string id)
        {
            if (!long.TryParse(id.FromBase64(), out long RoleID)) return Json(new Metadata() { status = false, data = "RoleID not allow" });
            
            var model = await _repo.Delete(RoleID);
            if (model.status) Roles = null;
            return Json(model);
        }
    }
}
