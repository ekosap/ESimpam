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
    public interface ITransactionRepo
    {
        Task<Metadata<TransactionViewModel>> GetTransaction(DateTime date, long ResidentID);
    }
    public class TransactionRepo : ITransactionRepo
    {
        private readonly SimpamDBContext context;
        public TransactionRepo(SimpamDBContext dBContext)
        {
            context = dBContext;
        }
        public async Task<Metadata<TransactionViewModel>> GetTransaction(DateTime date, long ResidentID = 0)
        {
            Metadata<TransactionViewModel> res = new Metadata<TransactionViewModel>();
            var dataTxn = (from t in context.Trx
                           where t.trxdate == date
                           select new TransactionViewModel()
                           {
                               TrxID = t.trxid,
                               CreatedBy = t.createdby,
                               CreatedDate = t.createddate,
                               Total = t.total,
                               TrxDate = t.trxdate,
                               UpdatedBy = t.updatedby,
                               UpdatedDate = t.updateddate,
                           }).FirstOrDefaultAsync();

            var txn = await dataTxn;
            if (txn == null) txn = new TransactionViewModel();
            txn.listItemTrx = (from c in context.Customer
                               join r in context.Resident on c.residentid equals r.residentid
                               join ti in context.TrxItem on c.customerid equals ti.customerid
                               into CustomerItem
                               from cti in CustomerItem.DefaultIfEmpty()
                               where (txn.TrxID == 0 ? true : (cti.trxid == txn.TrxID))
                               && (ResidentID == 0 ? true : (c.residentid == ResidentID))
                               select new TrxItemViewModel()
                               {
                                   CustomerName = c.customername,
                                   CustomerNumber = c.customernumber,
                                   AfterAmount = cti.afteramount,
                                   BeforeAmount = cti.beforeamount,
                                   CustomerID = cti.customerid,
                                   Price = r.price,
                                   Qty = cti.qty,
                                   TrxID = cti.trxid,
                                   TrxItemID = cti.trxitemid
                               }).ToList();
            res.data = txn;
            res.status = true;
            return res;
        }
    }
}
