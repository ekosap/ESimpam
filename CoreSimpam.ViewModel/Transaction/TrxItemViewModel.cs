using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.ViewModel
{
    public class TrxItemViewModel
    {
        public long? TrxItemID { get; set; }
        public long? TrxID { get; set; }
        public long? CustomerID { get; set; }
        public decimal? BeforeAmount { get; set; }
        public decimal? AfterAmount { get; set; }
        public decimal?  Qty { get; set; }
        public decimal? Price { get; set; }
    }
}
