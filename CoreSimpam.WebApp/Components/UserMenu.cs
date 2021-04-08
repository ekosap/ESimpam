using CoreSimpam.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Components
{
    public class UserMenu : ViewComponent
    {
        private readonly IRoleScreenAccessRepo _repo;
        public UserMenu(IRoleScreenAccessRepo repo)
        {
            _repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _repo.GetMenuAsync();
            return View(result.data);
        }
    }
}
