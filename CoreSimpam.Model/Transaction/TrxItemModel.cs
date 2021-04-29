using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    [Table("trxitem", Schema = "billing")]
    public class TrxItemModel
    {
        [Key]
        public long trxitemid { get; set; }
        public long trxid { get; set; }
        public long customerid { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal beforeamount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal afteramount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal  qty { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal price { get; set; }
        public bool isclear { get; set; }
    }
}
