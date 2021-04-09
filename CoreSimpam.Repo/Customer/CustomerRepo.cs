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
                var Customer = (from r in context.Customer where r.CustomerID == CustomerID select r).FirstOrDefault();

                if (Customer == null) { return new Metadata() { data = "Customer not found", status = false }; }

                context.Customer.Remove(Customer);

                await context.SaveChangesAsync();
                res.status = true;
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
                        join r in context.Resident on c.ResidentID equals r.ResidentID
                        select new CustomerViewModel()
                        {
                            CustomerAddress = c.CustomerAddress,
                            CustomerID = c.CustomerID,
                            CustomerName = c.CustomerName,
                            CustomerNumber = c.CustomerNumber,
                            Phone = c.Phone,
                            Resident = new ResidentViewModel()
                            {
                                IsActive = r.IsActive,
                                ResidentID = r.ResidentID,
                                ResidentName = r.ResidentName,
                                ResidentNumber = r.ResidentNumber
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
                        join r in context.Resident on c.ResidentID equals r.ResidentID
                        where c.CustomerID == CustomerID
                        select new CustomerViewModel()
                        {
                            CustomerAddress = c.CustomerAddress,
                            CustomerID = c.CustomerID,
                            CustomerName = c.CustomerName,
                            CustomerNumber = c.CustomerNumber,
                            Phone = c.Phone,
                            Resident = new ResidentViewModel()
                            {
                                IsActive = r.IsActive,
                                ResidentID = r.ResidentID,
                                ResidentName = r.ResidentName,
                                ResidentNumber = r.ResidentNumber
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
                var customer = (from r in context.Customer where r.CustomerName.ToLower().Replace(" ", "") == model.CustomerName.ToLower().Replace(" ", "") && r.CustomerID != model.CustomerID select r).FirstOrDefault();

                if (customer != null) { return new Metadata() { data = "Customer name is ready", status = false }; }

                var lastNumber = (from c in context.Customer where c.CustomerNumber.Contains($"CUS{DateTime.Now.Year}{DateTime.Now.Month}") select c.CustomerNumber).LastOrDefault();

                if (lastNumber == null)
                    model.CustomerNumber = $"CUS{DateTime.Now.Year}{DateTime.Now.Month}00001";
                else
                    model.CustomerNumber = $"CUS{DateTime.Now.Year}{DateTime.Now.Month}{Convert.ToInt32(lastNumber.Remove(0, 9)) + 1}";

                context.Customer.Add(new CustomerModel()
                {
                    CustomerAddress = model.CustomerAddress,
                    CustomerName = model.CustomerName,
                    CustomerNumber = model.CustomerNumber,
                    Phone = model.Phone,
                    ResidentID = model.Resident.ResidentID
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
                var customer = (from r in context.Customer where r.CustomerName.ToLower().Replace(" ", "") == model.CustomerName.ToLower().Replace(" ", "") && r.CustomerID != model.CustomerID select r).FirstOrDefault();

                if (customer != null) { return new Metadata() { data = "Customer name is ready", status = false }; }

                customer = (from r in context.Customer where r.CustomerID == model.CustomerID select r).FirstOrDefault();

                if (customer == null) { return new Metadata() { data = "Customer not found", status = false }; }

                customer.CustomerAddress = model.CustomerAddress;
                customer.CustomerName = model.CustomerName;
                customer.Phone = model.Phone;

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
