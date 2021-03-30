using CoreSimpam.Model;
using CoreSimpam.Model.Data;
using CoreSimpam.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Repo
{
    public interface IScreenRepo
    {
        Task<Metadata<ScreenViewModel>> GetAll();
        Task<Metadata<ScreenViewModel>> GetByID(long ScreenID);
        Task<Metadata> Insert(ScreenViewModel model);
        Task<Metadata> Update(ScreenViewModel model);
        Task<Metadata> Delete(long ScreenID);
    }
    public class ScreenRepo : IScreenRepo
    {
        private readonly SimpamDBContext context;

        public ScreenRepo(SimpamDBContext dBContext)
        {
            context = dBContext;
        }
        public async Task<Metadata> Delete(long ScreenID)
        {
            Metadata res = new Metadata();
            try
            {
                var data = await context.Screen.Where(x => x.ScreenID == ScreenID).FirstOrDefaultAsync();
                if (data == null)
                    return new Metadata() { status = false, data = "Screen application not found" };

                context.Screen.Remove(data);

                await context.SaveChangesAsync();
                res.status = true;
                res.data = "Data was successfully updated";
            }
            catch (Exception ex)
            {
                res.status = false;
                res.data = ex.Message;
            }

            return res;
        }

        public async Task<Metadata<ScreenViewModel>> GetAll()
        {
            Metadata<ScreenViewModel> res = new Metadata<ScreenViewModel>();
            var dataScreen = await context.Screen
                .GroupJoin(
                    context.Screen,
                    s1 => s1.ParentID,
                    s2 => s2.ScreenID,
                    (s1, s2) => new { screen = s1, parent = s2 })
                .SelectMany(
                    x => x.parent.DefaultIfEmpty(),
                    (x, y) => new ScreenViewModel()
                    {
                        ScreenID = x.screen.ScreenID,
                        ScreenName = x.screen.ScreenName,
                        ControllerName = x.screen.ControllerName,
                        ActionName = x.screen.ActionName,
                        IsMenu = x.screen.IsMenu,
                        IsActive = x.screen.IsActive,
                        ParentID = x.screen.ParentID,
                        ParentName = y.ScreenName
                    })
                .ToListAsync();
            res.data.screens = dataScreen;
            res.status = true;
            return res;
        }

        public async Task<Metadata<ScreenViewModel>> GetByID(long ScreenID)
        {
            Metadata<ScreenViewModel> res = new Metadata<ScreenViewModel>();
            var data = await context.Screen.FirstOrDefaultAsync(x => x.ScreenID == ScreenID);
            var dataParent = await context.Screen.FirstOrDefaultAsync(x => x.ScreenID == data.ParentID);
            if (data != null)
            {
                res.status = true;
                res.data.ScreenID = data.ScreenID;
                res.data.ScreenName = data.ScreenName;
                res.data.ControllerName = data.ControllerName;
                res.data.ActionName = data.ActionName;
                res.data.IsMenu = data.IsMenu;
                res.data.IsActive = data.IsActive;
                res.data.ParentID = data.ParentID;
                res.data.ParentName = dataParent?.ScreenName;
            }
            return res;
        }

        public async Task<Metadata> Insert(ScreenViewModel model)
        {
            Metadata res = new Metadata();
            try
            {
                var data = await context.Screen.AnyAsync(x => x.ScreenName.Contains(model.ScreenName));
                if (data)
                    return new Metadata() { status = false, data = "Screen name is ready" };
                await context.Screen.AddAsync(new ScreenModel()
                {
                    ScreenID = model.ScreenID,
                    ScreenName = model.ScreenName,
                    ControllerName = model.ControllerName,
                    ActionName = model.ActionName,
                    IsMenu = model.IsMenu,
                    IsActive = model.IsActive,
                    ParentID = model.ParentID,
                });
                await context.SaveChangesAsync();
                res.status = true;
                res.data = "Data was successfully inserted";
            }
            catch (Exception ex)
            {
                res.status = false;
                res.data = ex.Message;
            }

            return res;
        }

        public async Task<Metadata> Update(ScreenViewModel model)
        {
            Metadata res = new Metadata();
            try
            {
                var data = await context.Screen.Where(x => x.ScreenID == model.ScreenID).FirstOrDefaultAsync();
                if (data == null)
                    return new Metadata() { status = false, data = "Screen not found" };

                data.ScreenID = model.ScreenID;
                data.ScreenName = model.ScreenName;
                data.ControllerName = model.ControllerName;
                data.ActionName = model.ActionName;
                data.IsMenu = model.IsMenu;
                data.IsActive = model.IsActive;
                data.ParentID = model.ParentID;

                await context.SaveChangesAsync();
                res.status = true;
                res.data = "Data was successfully updated";
            }
            catch (Exception ex)
            {
                res.status = false;
                res.data = ex.Message;
            }

            return res;
        }
    }
}
