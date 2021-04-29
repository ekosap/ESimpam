using CoreSimpam.Repo;
using CoreSimpam.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimpam.WebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IRoleScreenAccessRepo _repo;
        public MenuController(IRoleScreenAccessRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<List<ScreenViewModel>> GetMenuAsync()
        {
            var data = await _repo.GetMenuAsync();
            return GetMenuTree(data.data.Menu, 0);
        }

        private List<ScreenViewModel> GetMenuTree(List<ScreenViewModel> ListMenu, long parentID)
        {
            return ListMenu.Where(x => x.ParentID == parentID).Select(s => new ScreenViewModel()
            {
                ActionName = s.ActionName,
                ControllerName = s.ControllerName,
                IsActive = s.IsActive,
                IsMenu = s.IsMenu,
                ParentID = s.ParentID,
                ScreenID = s.ScreenID,
                ScreenName = s.ScreenName,
                AllowDelete = s.AllowDelete,
                AllowRead = s.AllowRead,
                AllowWrite = s.AllowWrite,
                IconCss = s.IconCss,
                screens = GetMenuTree(ListMenu, s.ScreenID)
            }).ToList();
        }
    }
}
