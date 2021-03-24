using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreSimpam.Model
{
    [Table("Users", Schema ="Common")]
    public class UserModel
    {
        [Key]
        public long UserID { get; set; }
        [ForeignKey("RoleID")]
        public long RoleID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public virtual RoleModel Role { get; set; }
    }
}
