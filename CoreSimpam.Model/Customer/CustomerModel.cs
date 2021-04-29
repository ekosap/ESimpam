using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    [Table("customer", Schema = "customer")]
    public class CustomerModel
    {
        [Key]
        public long customerid { get; set; }
        public string customername { get; set; }
        public string customeraddress { get; set; }
        public string phone { get; set; }
        public string customernumber { get; set; }
        public long residentid { get; set; }
    }
}
