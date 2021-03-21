using System;
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
    }
}
