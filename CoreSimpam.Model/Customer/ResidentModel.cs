using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    [Table("resident", Schema ="customer")]
    public class ResidentModel
    {
        [Key]
        public long residentid { get; set; }
        public string residentname { get; set; }
        public string residentnumber { get; set; }
        public bool isactive { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal price { get; set; }
    }
}
