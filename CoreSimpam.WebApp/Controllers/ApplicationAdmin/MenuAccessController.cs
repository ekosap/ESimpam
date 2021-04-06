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
        private readonly IRoleScreenAccessRepo _repo;
        public MenuAccessController(IRoleScreenAccessRepo accessRepo)
        {
            _repo = accessRepo;
        }
        public IActionResult Role(string id)
        {
            ViewData["Title"] = "Configuration Role Access";
            return View(_repo.get(Convert.ToInt64(id.FromBase64())));
        }
    }
}
