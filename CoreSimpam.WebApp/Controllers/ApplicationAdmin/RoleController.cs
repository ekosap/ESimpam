using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Controllers.ApplicationAdmin
{
    public class RoleController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Application Role";
            return await Task.FromResult(View());
        }
        public async Task<IActionResult> Add()
        {
            ViewBag.Title = "Add Application Role";
            return await Task.FromResult(PartialView("_Add"));
        }
    }
}
