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
    public interface ICustomerRepo
    {
        Metadata<List<CustomerViewModel>> Get();
        Metadata<CustomerViewModel> GetByID(long CustomerID);
        Task<Metadata> Insert(CustomerViewModel model);
        Task<Metadata> Update(CustomerViewModel model);
        Task<Metadata> Delete(long CustomerID);
    }
    public class CustomerRepo : ICustomerRepo
    {
        private readonly SimpamDBContext context;

        public CustomerRepo(SimpamDBContext context)
        {
            this.context = context;
        }

        public async Task<Metadata> Delete(long CustomerID)
        {
            Metadata res = new Metadata();
            try
            {
                var Customer = (from r in context.Customer where r.customerid == CustomerID select r).FirstOrDefault();

                if (Customer == null) { return new Metadata() { data = "Customer not found", status = false }; }

                context.Customer.Remove(Customer);

                await context.SaveChangesAsync();
                res.status = true; res.data = "Data Deleted";
            }
            catch (Exception e)
            {
                res.data = e.Message.ToString();
            }
            return res;
        }

        public Metadata<List<CustomerViewModel>> Get()
        {
            Metadata<List<CustomerViewModel>> res = new Metadata<List<CustomerViewModel>>();
            var data = (from c in context.Customer
                        join r in context.Resident on c.residentid equals r.residentid
                        select new CustomerViewModel()
                        {
                            CustomerAddress = c.customeraddress,
                            CustomerID = c.customerid,
                            CustomerName = c.customername,
                            CustomerNumber = c.customernumber,
                            Phone = c.phone,
                            Resident = new ResidentViewModel()
                            {
                                IsActive = r.isactive,
                                ResidentID = r.residentid,
                                ResidentName = r.residentname,
                                ResidentNumber = r.residentnumber
                            }
                        }).ToList();
            res.status = data.Count > 0;
            res.data = data;
            return res;
        }

        public Metadata<CustomerViewModel> GetByID(long CustomerID)
        {
            Metadata<CustomerViewModel> res = new Metadata<CustomerViewModel>();
            var data = (from c in context.Customer
                        join r in context.Resident on c.residentid equals r.residentid
                        where c.customerid == CustomerID
                        select new CustomerViewModel()
                        {
                            CustomerAddress = c.customeraddress,
                            CustomerID = c.customerid,
                            CustomerName = c.customername,
                            CustomerNumber = c.customernumber,
                            Phone = c.phone,
                            Resident = new ResidentViewModel()
                            {
                                IsActive = r.isactive,
                                ResidentID = r.residentid,
                                ResidentName = r.residentname,
                                ResidentNumber = r.residentnumber
                            }
                        }).FirstOrDefault();
            res.status = data != null;
            res.data = data;
            return res;
        }

        public async Task<Metadata> Insert(CustomerViewModel model)
        {
            Metadata res = new Metadata();
            try
            {
                var customer = (from r in context.Customer where r.customername.ToLower().Replace(" ", "") == model.CustomerName.ToLower().Replace(" ", "") && r.customerid != model.CustomerID select r).FirstOrDefault();

                if (customer != null) { return new Metadata() { data = "Customer name is ready", status = false }; }
                string NumbVal = $"CUS{DateTime.Now.ToString("yyyyMM")}";
                var lastNumber = (from c in context.Customer where c.customernumber.Contains(NumbVal) select c).OrderBy(x=> x.customerid).LastOrDefault()?.customernumber;

                if (lastNumber == null)
                    model.CustomerNumber = $"{NumbVal}00001";
                else
                    model.CustomerNumber = $"{NumbVal}{(Convert.ToInt32(lastNumber.Remove(0, 9)) + 1).ToString("00000")}";

                context.Customer.Add(new CustomerModel()
                {
                    customeraddress = model.CustomerAddress,
                    customername = model.CustomerName,
                    customernumber = model.CustomerNumber,
                    phone = model.Phone,
                    residentid = model.Resident.ResidentID
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

        public async Task<Metadata> Update(CustomerViewModel model)
        {
            Metadata res = new Metadata();
            try
            {
                var customer = (from r in context.Customer where r.customername.ToLower().Replace(" ", "") == model.CustomerName.ToLower().Replace(" ", "") && r.customerid != model.CustomerID select r).FirstOrDefault();

                if (customer != null) { return new Metadata() { data = "Customer name is ready", status = false }; }

                customer = (from r in context.Customer where r.customerid == model.CustomerID select r).FirstOrDefault();

                if (customer == null) { return new Metadata() { data = "Customer not found", status = false }; }

                customer.customeraddress = model.CustomerAddress;
                customer.customername = model.CustomerName;
                customer.phone = model.Phone;

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
