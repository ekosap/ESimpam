using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    [Table("TrxItem", Schema = "Billing")]
    public class TrxItemModel
    {
        [Key]
        public long TrxItemID { get; set; }
        public long TrxID { get; set; }
        public long CustomerID { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal BeforeAmount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal AfterAmount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal  Qty { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public bool IsClear { get; set; }
    }
}
