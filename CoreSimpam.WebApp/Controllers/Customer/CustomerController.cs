using CoreSimpam.Repo;
using CoreSimpam.ViewModel;
using CoreSimpam.ViewModel.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Controllers.Customer
{
    [AuthorizeWebAttributes]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepo repo;
        private readonly IResidentRepo resident;

        public CustomerController(ICustomerRepo repo, IResidentRepo resident)
        {
            this.repo = repo;
            this.resident = resident;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Customer";
            return View();
        }
        public IActionResult GetData(JqueryDatatableParam param)
        {
            var Customers = repo.Get().data;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                Customers = Customers.Where(
                    x => x.CustomerName.ToLower().Contains(param.sSearch.ToLower())
                    || x.CustomerNumber.ToLower().Contains(param.sSearch.ToLower())
                    || x.CustomerAddress.ToLower().Contains(param.sSearch.ToLower())
                    || x.Phone.ToLower().Contains(param.sSearch.ToLower())
                    || x.Resident.ResidentName.ToLower().Contains(param.sSearch.ToLower())
                    ).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["sSortDir_0"];

            Func<CustomerViewModel, string> orderingFunction = e =>
                sortColumnIndex == 5 ? e.Resident.ResidentName:
                sortColumnIndex == 4 ? e.Phone :
                sortColumnIndex == 3 ? e.CustomerAddress :
                sortColumnIndex == 2 ? e.CustomerNumber :
                sortColumnIndex == 1 ? e.CustomerName : e.CustomerID.ToString();

            Customers = sortDirection == "asc" ? Customers.OrderBy(orderingFunction).ToList() : Customers.OrderByDescending(orderingFunction).ToList();

            var displayResult = Customers.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = Customers.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                Data = displayResult
            });

        }
        public IActionResult Add()
        {
            ViewData["Title"] = "Add Customer";
            ViewData["resident"] = resident.Get().data.Where(x=> x.IsActive == true).Select(x => new SelectListItem() { Value = x.ResidentID.ToString(), Text = x.ResidentName }).ToList();
            return PartialView("_Add", new CustomerViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAsync(CustomerViewModel model)
        {
            ModelState.SkipToValid("resident.ResidentName");
            if (ModelState.IsValid)
            {
                var result = await repo.Insert(model);
                return Json(result);
            }
            ViewData["Title"] = "Add Customer";
            ViewData["resident"] = resident.Get().data.Where(x => x.IsActive == true).Select(x => new SelectListItem() { Value = x.ResidentID.ToString(), Text = x.ResidentName }).ToList();
            return PartialView("_Add", model);
        }
        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Edit Customer";
            ViewData["resident"] = resident.Get().data.Where(x => x.IsActive == true).Select(x => new SelectListItem() { Value = x.ResidentID.ToString(), Text = x.ResidentName }).ToList();
            var result = repo.GetByID(id).data;
            return PartialView("_Edit", result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(CustomerViewModel model)
        {
            ViewData["Title"] = "Edit Customer";
            ModelState.SkipToValid("resident.ResidentName");
            if (ModelState.IsValid)
            {
                var result = await repo.Update(model);
                return Json(result);
            }
            ViewData["resident"] = resident.Get().data.Where(x => x.IsActive == true).Select(x => new SelectListItem() { Value = x.ResidentID.ToString(), Text = x.ResidentName }).ToList();
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
