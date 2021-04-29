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
                var data = await context.Screen.Where(x => x.screenid == ScreenID).FirstOrDefaultAsync();
                if (data == null)
                    return new Metadata() { status = false, data = "Screen application not found" };

                context.RoleScreen.RemoveRange(context.RoleScreen.Where(x => x.screenid == ScreenID));
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
                    s1 => s1.parentid,
                    s2 => s2.screenid,
                    (s1, s2) => new { screen = s1, parent = s2 })
                .SelectMany(
                    x => x.parent.DefaultIfEmpty(),
                    (x, y) => new ScreenViewModel()
                    {
                        ScreenID = x.screen.screenid,
                        ScreenName = x.screen.screenname,
                        ControllerName = x.screen.controllername,
                        ActionName = x.screen.actionname,
                        IsMenu = x.screen.ismenu,
                        IsActive = x.screen.isactive,
                        ParentID = x.screen.parentid,
                        ParentName = y.screenname,
                        IconCss = x.screen.iconcss
                    })
                .ToListAsync();
            res.data.screens = dataScreen;
            res.status = true;
            return res;
        }

        public async Task<Metadata<ScreenViewModel>> GetByID(long ScreenID)
        {
            Metadata<ScreenViewModel> res = new Metadata<ScreenViewModel>();
            var data = await context.Screen.FirstOrDefaultAsync(x => x.screenid == ScreenID);
            var dataParent = await context.Screen.FirstOrDefaultAsync(x => x.screenid == data.parentid);
            if (data != null)
            {
                res.status = true;
                res.data.ScreenID = data.screenid;
                res.data.ScreenName = data.screenname;
                res.data.ControllerName = data.controllername;
                res.data.ActionName = data.actionname;
                res.data.IsMenu = data.ismenu;
                res.data.IsActive = data.isactive;
                res.data.ParentID = data.parentid;
                res.data.ParentName = dataParent?.screenname;
                res.data.IconCss = data.iconcss;
            }
            return res;
        }

        public async Task<Metadata> Insert(ScreenViewModel model)
        {
            Metadata res = new Metadata();
            try
            {
                var data = await context.Screen.AnyAsync(x => x.screenname.ToLower().Replace(" ", "") == model.ScreenName.ToLower().Replace(" ", "") && x.screenid != model.ScreenID);
                if (data)
                    return new Metadata() { status = false, data = "Screen name is ready" };
                await context.Screen.AddAsync(new ScreenModel()
                {
                    screenid = model.ScreenID,
                    screenname = model.ScreenName,
                    controllername = model.ControllerName,
                    actionname = model.ActionName,
                    ismenu = model.IsMenu,
                    isactive = model.IsActive,
                    parentid = model.ParentID,
                    iconcss = model.IconCss
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
                var dataScreen = await context.Screen.AnyAsync(x => x.screenname.ToLower().Replace(" ", "") == model.ScreenName.ToLower().Replace(" ", "") && x.screenid != model.ScreenID);
                if (dataScreen)
                    return new Metadata() { status = false, data = "Screen name is ready" };
                var data = await context.Screen.Where(x => x.screenid == model.ScreenID).FirstOrDefaultAsync();
                if (data == null)
                    return new Metadata() { status = false, data = "Screen not found" };

                data.screenid = model.ScreenID;
                data.screenname = model.ScreenName;
                data.controllername = model.ControllerName;
                data.actionname = model.ActionName;
                data.ismenu = model.IsMenu;
                data.isactive = model.IsActive;
                data.parentid = model.ParentID;
                data.iconcss = model.IconCss;

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
