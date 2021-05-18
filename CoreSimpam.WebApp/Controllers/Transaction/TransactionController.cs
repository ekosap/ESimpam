using CoreSimpam.Repo;
using CoreSimpam.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Controllers.Transaction
{
    public class TransactionController : Controller
    {
        private readonly IResidentRepo resident;

        public TransactionController(IResidentRepo resident)
        {
            this.resident = resident;
        }
        public IActionResult Index()
        {
            var model = new TransactionViewModel();
            var items = resident.Get().data.Where(x => x.IsActive == true).Select(x => new SelectListItem() { Value = x.ResidentID.ToString(), Text = x.ResidentName }).ToList();
            items.Insert(0, new SelectListItem() { Text = "All", Value = "0" });
            ViewData["resident"] = items;
            return View(model);
        }
    }
}
