using CoreSimpam.Repo;
using CoreSimpam.ViewModel;
using CoreSimpam.ViewModel.Query;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Controllers.Customer
{
    [AuthorizeWebAttributes]
    public class ResidentController : Controller
    {
        private readonly IResidentRepo repo;

        public ResidentController(IResidentRepo repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Resident Customer";
            return View();
        }
        public IActionResult GetData(JqueryDatatableParam param)
        {
            var dataResident = repo.Get().data;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                dataResident = dataResident.Where(
                    x => x.ResidentName.ToLower().Contains(param.sSearch.ToLower())
                    || x.ResidentNumber.ToLower().Contains(param.sSearch.ToLower())
                    ).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["sSortDir_0"];

            Func<ResidentViewModel, string> orderingFunction = e =>
                sortColumnIndex == 3 ? (e.IsActive ? "Enable" : "Disable") :
                sortColumnIndex == 2 ? e.ResidentName :
                sortColumnIndex == 1 ? e.ResidentNumber : e.ResidentID.ToString();

            dataResident = sortDirection == "asc" ? dataResident.OrderBy(orderingFunction).ToList() : dataResident.OrderByDescending(orderingFunction).ToList();

            var displayResult = dataResident.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = dataResident.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                Data = displayResult
            });

        }
        [AuthorizeWebAttributes(AccessLevel = AccessLevel.AllowWrite)]
        public IActionResult Add()
        {
            ViewData["Title"] = "Add Resident Customer";
            return PartialView("_Add", new ResidentViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAsync(ResidentViewModel model)
        {
            ViewData["Title"] = "Add Resident Customer";
            if (ModelState.IsValid)
            {
                var result = await repo.Insert(model);
                return Json(result);
            }
            return PartialView("_Add", model);
        }
        [AuthorizeWebAttributes(AccessLevel = AccessLevel.AllowWrite)]
        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Edit Resident Customer";
            var result = repo.GetByID(id).data;
            return PartialView("_Edit", result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(ResidentViewModel model)
        {
            ViewData["Title"] = "Edit Resident Customer";
            if (ModelState.IsValid)
            {
                var result = await repo.Update(model);
                return Json(result);
            }
            return PartialView("_Edit", model);
        }
        [AuthorizeWebAttributes(AccessLevel = AccessLevel.AllowDelete)]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await repo.Delete(id);
            return Json(model);
        }
    }
}
