using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    [Table("trx", Schema ="billing")]
    public class TransactionModel :DTOBase
    {
        [Key]
        public long trxid { get; set; }
        public DateTime trxdate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal total { get; set; }        
    }
}
