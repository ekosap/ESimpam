using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.ViewModel
{
    public class TransactionViewModel : DTOBaseViewModel
    {
        public long TrxID { get; set; }
        public DateTime TrxDate { get; set; }
        public decimal Total { get; set; }
        public List<TrxItemViewModel> listItemTrx { get; set; } = new List<TrxItemViewModel>();
    }
}
