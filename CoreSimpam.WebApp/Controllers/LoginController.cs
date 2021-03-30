using CoreSimpam.Repo;
using CoreSimpam.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepo _repo;

        public LoginController(ILoginRepo repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();
            model.ReturnUrl = Request.Query["ReturnUrl"];
            ViewData["Title"] = "Login to apps";
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            var login = await _repo.Login(model);
            return Json(new Metadata() { status = login, data = login ? (string.IsNullOrWhiteSpace(model.ReturnUrl) ? "/" : model.ReturnUrl) : "Login Failed" });
        }
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            var logout = await _repo.Logout();
            if (logout) return RedirectToAction("index");
            return Json(new Metadata() { status = logout, data = logout ? "" : "LogOut Failed" });
        }
    }
}
