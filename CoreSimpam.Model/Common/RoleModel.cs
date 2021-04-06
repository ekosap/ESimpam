using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreSimpam.Model
{
    [Table("Roles", Schema = "Common")]
    public class RoleModel
    {
        [Key]
        public long RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsEnabled { get; set; }
        public virtual UserModel User { get; set; }
        public virtual List<RoleScreenModel> RoleScreens { get; set; }
    }
}
