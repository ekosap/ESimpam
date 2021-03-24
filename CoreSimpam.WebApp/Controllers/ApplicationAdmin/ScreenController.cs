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
    public class ScreenController : Controller
    {
        private ScreenViewModel Screen
        {
            get
            {
                if (ViewData["Screen"] == null)
                {
                    ViewData["Screen"] = _repo.GetAll().Result.data;
                }
                return (ScreenViewModel)ViewData["Screen"];
            }
            set
            {
                ViewData["Screen"] = value ?? _repo.GetAll().Result.data;
            }
        }
        private readonly IScreenRepo _repo;

        public ScreenController(IScreenRepo screenRepo)
        {
            _repo = screenRepo;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Application Screen";
            return await Task.FromResult(View());
        }
        public IActionResult GetData(JqueryDatatableParam param)
        {
            var data = Screen;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                data.screens = data.screens.Where(
                    x => x.ScreenName.ToLower().Contains(param.sSearch.ToLower())
                    || x.ControllerName.ToLower().Contains(param.sSearch.ToLower())
                    || x.ActionName.ToLower().Contains(param.sSearch.ToLower())
                    || (x.IsActive ? "Enable" : "Disable").ToLower().Contains(param.sSearch.ToLower())
                    || (x.IsMenu ? "Menu" : "Screen").ToLower().Contains(param.sSearch.ToLower())
                    || x.ParentName.ToLower().Contains(param.sSearch.ToLower())
                    ).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["sSortDir_0"];

            Func<ScreenViewModel, string> orderingFunction = e =>
                sortColumnIndex == 6 ? e.ParentName :
                sortColumnIndex == 5 ? (e.IsMenu ? "Menu" : "Screen") :
                sortColumnIndex == 4 ? (e.IsActive ? "Enable" : "Disable") :
                sortColumnIndex == 3 ? e.ActionName :
                sortColumnIndex == 2 ? e.ControllerName :
                sortColumnIndex == 1 ? e.ScreenName : e.ScreenID.ToString();

            data.screens = sortDirection == "asc" ? data.screens.OrderBy(orderingFunction).ToList() : data.screens.OrderByDescending(orderingFunction).ToList();

            var displayResult = data.screens.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = data.screens.Count();

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
            ViewData["Title"] = "Add Application Screen";
            return await Task.FromResult(PartialView("_Add"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ScreenViewModel model)
        {
            ViewData["Title"] = "Add Application Screen";
            if (ModelState.IsValid)
            {
                var res = await _repo.Insert(model);
                if (res.status) Screen = null;
                return Json(res);
            }
            return PartialView("_Add", model);
        }
        public async Task<IActionResult> Edit(long id)
        {
            ViewData["Title"] = "Edit Application Screen";
            var model = await _repo.GetByID(id);
            return PartialView("_Edit", model.data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ScreenViewModel model)
        {
            ViewData["Title"] = "Edit Application Screen";
            if (ModelState.IsValid)
            {
                var res = await _repo.Update(model);
                if (res.status) Screen = null;
                return Json(res);
            }
            return PartialView("_Edit", model);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var model = await _repo.Delete(id);
            if (model.status) Screen = null;
            return Json(model);
        }
    }
}
