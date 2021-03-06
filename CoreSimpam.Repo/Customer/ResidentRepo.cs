using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreSimpam.Model.Data;
using CoreSimpam.Model;
using CoreSimpam.ViewModel;

namespace CoreSimpam.Repo
{
    public interface IResidentRepo
    {
        Metadata<List<ResidentViewModel>> Get();
        Metadata<ResidentViewModel> GetByID(long ResidentID);
        Task<Metadata> Insert(ResidentViewModel model);
        Task<Metadata> Update(ResidentViewModel model);
        Task<Metadata> Delete(long ResidentID);
    }
    public class ResidentRepo : IResidentRepo
    {
        private readonly SimpamDBContext context;

        public ResidentRepo(SimpamDBContext context)
        {
            this.context = context;
        }

        public async Task<Metadata> Delete(long ResidentID)
        {
            Metadata res = new Metadata();
            try
            {
                var resident = (from r in context.Resident where r.residentid == ResidentID select r).FirstOrDefault();

                if (resident == null) { return new Metadata() { data = "Resident not found", status = false }; }

                context.Resident.Remove(resident);

                await context.SaveChangesAsync();
                res.status = true;
                res.data = "Data Deleted";
            }
            catch (Exception e)
            {
                res.data = e.Message.ToString();
            }
            return res;
        }

        public Metadata<List<ResidentViewModel>> Get()
        {
            Metadata<List<ResidentViewModel>> res = new Metadata<List<ResidentViewModel>>();
            var data = (from r in context.Resident
                        select new ResidentViewModel()
                        {
                            IsActive = r.isactive,
                            ResidentID = r.residentid,
                            ResidentName = r.residentname,
                            ResidentNumber = r.residentnumber
                        }).ToList();
            res.status = data.Count > 0;
            res.data = data;
            return res;
        }

        public Metadata<ResidentViewModel> GetByID(long ResidentID)
        {
            Metadata<ResidentViewModel> res = new Metadata<ResidentViewModel>();
            var data = (from r in context.Resident
                        where r.residentid == ResidentID
                        select new ResidentViewModel()
                        {
                            IsActive = r.isactive,
                            ResidentID = r.residentid,
                            ResidentName = r.residentname,
                            ResidentNumber = r.residentnumber,
                            Price = r.price
                        }).FirstOrDefault();
            res.status = data != null;
            res.data = data;
            return res;
        }

        public async Task<Metadata> Insert(ResidentViewModel model)
        {
            Metadata res = new Metadata();
            try
            {
                var resident = (from r in context.Resident where r.residentname.ToLower().Replace(" ", "") == model.ResidentName.ToLower().Replace(" ", "") && r.residentid != model.ResidentID select r).FirstOrDefault();

                if(resident != null) { return new Metadata() { data = "Resident name is ready", status = false }; }
                string NumberVal = "RC"+DateTime.Now.ToString("yyyyMM");
                var lastNumber = (from r in context.Resident where r.residentnumber.Contains(NumberVal) select r).OrderBy(x=>x.residentid).LastOrDefault()?.residentnumber;

                if (lastNumber == null) 
                    model.ResidentNumber = $"{NumberVal}001";
                else 
                    model.ResidentNumber = $"{NumberVal}{(Convert.ToInt32(lastNumber.Remove(0,8)) + 1).ToString("000")}";

                context.Resident.Add(new ResidentModel()
                {
                    isactive = model.IsActive,
                    residentname = model.ResidentName,
                    residentnumber = model.ResidentNumber,
                    price = model.Price
                });

                await context.SaveChangesAsync();
                res.status = true;
            }
            catch (Exception e)
            {
                res.data = e.Message.ToString();
            }
            return res;
        }

        public async Task<Metadata> Update(ResidentViewModel model)
        {
            Metadata res = new Metadata();
            try
            {
                var resident = (from r in context.Resident where r.residentname.ToLower().Replace(" ", "") == model.ResidentName.ToLower().Replace(" ", "") && r.residentid != model.ResidentID select r).FirstOrDefault();

                if (resident != null) { return new Metadata() { data = "Resident name is ready", status = false }; }

                resident = (from r in context.Resident where r.residentid == model.ResidentID select r).FirstOrDefault();

                if (resident == null) { return new Metadata() { data = "Resident not found", status = false }; }

                resident.residentname = model.ResidentName;
                resident.isactive = model.IsActive;
                resident.price = model.Price;

                await context.SaveChangesAsync();
                res.status = true;
            }
            catch (Exception e)
            {
                res.data = e.Message.ToString();
            }
            return res;
        }
    }
}
