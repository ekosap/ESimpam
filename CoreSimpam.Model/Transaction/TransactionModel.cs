using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    [Table("Trx", Schema ="Billing")]
    public class TransactionModel :DTOBase
    {
        [Key]
        public long TrxID { get; set; }
        public DateTime TrxDate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }        
    }
}
