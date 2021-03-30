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
        private List<SelectListItem> Parent
        {
            get
            {
                if (ViewData["xparent"] == null)
                {
                    var listItems = _repo.GetAll().Result.data.screens.Where(x => x.ParentID == 0).Select(x => new SelectListItem() { Value = x.ScreenID.ToString(), Text = x.ScreenName }).ToList();
                    listItems.Insert(0, new SelectListItem() { Text = "No Parent Menu", Value = "0" });
                    ViewData["xparent"] = listItems;
                }
                return (List<SelectListItem>)ViewData["xparent"];
            }
            set
            {
                var listItems = value ?? _repo.GetAll().Result.data.screens.Where(x => x.ParentID == 0).Select(x => new SelectListItem() { Value = x.ScreenID.ToString(), Text = x.ScreenName }).ToList();
                listItems.Insert(0, new SelectListItem() { Text = "No Parent Menu", Value = "0" });
                ViewData["xparent"] = listItems;
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
                    x => (x.ScreenName ?? string.Empty).ToLower().Contains(param.sSearch.ToLower())
                    || (x.ControllerName ?? string.Empty).ToLower().Contains(param.sSearch.ToLower())
                    || (x.ActionName ?? string.Empty).ToLower().Contains(param.sSearch.ToLower())
                    || (x.IsActive ? "Enable" : "Disable").ToLower().Contains(param.sSearch.ToLower())
                    || (x.IsMenu ? "Menu" : "Screen").ToLower().Contains(param.sSearch.ToLower())
                    || (x.ParentName ?? string.Empty).ToLower().Contains(param.sSearch.ToLower())
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
            ViewData["parent"] = Parent;
            return await Task.FromResult(PartialView("_Add"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ScreenViewModel model)
        {
            ViewData["Title"] = "Add Application Screen";
            ViewData["parent"] = Parent;
            if (ModelState.IsValid)
            {
                var res = await _repo.Insert(model);
                if (res.status) { Screen = null; Parent = null; }
                return Json(res);
            }
            return PartialView("_Add", model);
        }
        public async Task<IActionResult> Edit(long id)
        {
            ViewData["Title"] = "Edit Application Screen";
            ViewData["parent"] = Parent;
            var model = await _repo.GetByID(id);
            return PartialView("_Edit", model.data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ScreenViewModel model)
        {
            ViewData["Title"] = "Edit Application Screen";
            ViewData["parent"] = Parent;
            if (ModelState.IsValid)
            {
                var res = await _repo.Update(model);
                if (res.status) { Screen = null; Parent = null; }
                return Json(res);
            }
            return PartialView("_Edit", model);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var res = await _repo.Delete(id);
            if (res.status) { Screen = null; Parent = null; }
            return Json(res);
        }
    }
}
