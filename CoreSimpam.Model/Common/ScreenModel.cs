using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    [Table("screen", Schema = "common")]
    public class ScreenModel
    {
        [Key]
        public long screenid { get; set; }
        public string screenname { get; set; }
        public string controllername { get; set; }
        public string actionname { get; set; }
        public string iconcss { get; set; }
        public bool ismenu { get; set; }
        public bool isactive { get; set; }
        public long parentid { get; set; }
    }
}
