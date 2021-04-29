using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreSimpam.Model
{
    [Table("roles", Schema = "common")]
    public class RoleModel : DTOBase
    {
        [Key]
        public long roleid { get; set; }
        public string rolename { get; set; }
        public bool isenabled { get; set; }
        public virtual UserModel user { get; set; }
        public virtual List<RoleScreenModel> rolescreens { get; set; }
    }
}
