using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    [Table("Customer", Schema = "Customer")]
    public class CustomerModel
    {
        [Key]
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string Phone { get; set; }
        public string CustomerNumber { get; set; }
        public long ResidentID { get; set; }
    }
}
