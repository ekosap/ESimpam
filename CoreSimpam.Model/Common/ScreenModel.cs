using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    [Table("Screen", Schema = "Common")]
    public class ScreenModel
    {
        [Key]
        public long ScreenID { get; set; }
        public string ScreenName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string IconCss { get; set; }
        public bool IsMenu { get; set; }
        public bool IsActive { get; set; }
        public long ParentID { get; set; }
    }
}
