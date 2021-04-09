using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.ViewModel
{
    public class CustomerViewModel
    {
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string Phone { get; set; }
        public string CustomerNumber { get; set; }
        public ResidentViewModel Resident { get; set; } = new ResidentViewModel();
    }
}
