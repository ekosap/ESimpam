using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    [Table("RoleScreen", Schema = "Common")]
    public class RoleScreenModel
    {
        [Key]
        public long RoleScreenID { get; set; }
        public long RoleID { get; set; }
        public long ScreenID { get; set; }
        public bool ReadFlag { get; set; }
        public bool WriteFlag { get; set; }
        public bool DeleteFlag { get; set; }
        public virtual ScreenModel Screen { get; set; }
        public virtual RoleModel Role { get; set; }
    }
}
