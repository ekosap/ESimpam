using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    [Table("Resident", Schema ="Customer")]
    public class ResidentModel
    {
        [Key]
        public long ResidentID { get; set; }
        public string ResidentName { get; set; }
        public string ResidentNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
