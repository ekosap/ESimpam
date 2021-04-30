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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepo repo;
        public TransactionController(ITransactionRepo transactionRepo)
        {
            repo = transactionRepo;
        }
        [HttpGet]
        public async Task<Metadata<TransactionViewModel>> GetAsync(DateTime TrxDate, long ResidentID)
        {
            var data = await repo.GetTransaction(TrxDate, ResidentID);
            return data;
        }
    }
}
