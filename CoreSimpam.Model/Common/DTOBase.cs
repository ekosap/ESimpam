using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    public class DTOBase
    {
        public DateTime createddate { get; set; }
        public long createdby { get; set; }
        public DateTime? updateddate { get; set; }
        public long? updatedby { get; set; }
    }
}
