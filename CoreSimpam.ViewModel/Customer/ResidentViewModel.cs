using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.ViewModel
{
    public class ResidentViewModel
    {
        public long ResidentID { get; set; }
        public string ResidentName { get; set; }
        public string ResidentNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
