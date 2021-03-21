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
            return await Task.FromResult(View());
        }
        public async Task<IActionResult> GetData(JqueryDatatableParam param)
        {
            var employees = await _repo.GetAll(new RoleQuery());

            //employees.data.Roles.ForEach(x => x. = x.StartDate.ToString("dd'/'MM'/'yyyy"));

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                employees.data.Roles = employees.data.Roles.Where(x => x.RoleName.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["sSortDir_0"];

            //if (sortColumnIndex == 3)
            //{
            //    employees.data.Roles = sortDirection == "asc" ? employees.data.Roles.OrderBy(c => c.Age) : employees.data.Roles.OrderByDescending(c => c.Age);
            //}
            //else if (sortColumnIndex == 4)
            //{
            //    employees = sortDirection == "asc" ? employees.data.Roles.OrderBy(c => c.StartDate) : employees.data.Roles.OrderByDescending(c => c.StartDate);
            //}
            //else if (sortColumnIndex == 5)
            //{
            //    employees = sortDirection == "asc" ? employees.OrderBy(c => c.Salary) : employees.OrderByDescending(c => c.Salary);
            //}
            //else
            //{
            //    Func<Employee, string> orderingFunction = e => sortColumnIndex == 0 ? e.Name :
            //                                                   sortColumnIndex == 1 ? e.Position :
            //                                                   e.Location;

            //    employees = sortDirection == "asc" ? employees.OrderBy(orderingFunction) : employees.OrderByDescending(orderingFunction);
            //}

            var displayResult = employees.data.Roles.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = employees.data.Roles.Count();

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
                return Json(res);
            }
            return PartialView("_Add", model);
        }
        public async Task<IActionResult> Edit(long id)
        {
            ViewData["Title"] = "Edit Application Role";
            var model = await _repo.GetByID(id);
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
                return Json(res);
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
