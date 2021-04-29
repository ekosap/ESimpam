using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Model
{
    [Table("rolescreen", Schema = "common")]
    public class RoleScreenModel :DTOBase
    {
        [Key]
        public long rolescreenid { get; set; }
        public long roleid { get; set; }
        public long screenid { get; set; }
        public bool readflag { get; set; }
        public bool writeflag { get; set; }
        public bool deleteflag { get; set; }
        public virtual ScreenModel screen { get; set; }
        public virtual RoleModel role { get; set; }
    }
}
