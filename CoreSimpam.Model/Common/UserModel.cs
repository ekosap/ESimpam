using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreSimpam.Model
{
    [Table("users", Schema ="common")]
    public class UserModel
    {
        [Key]
        public long userid { get; set; }
        [ForeignKey("roleid")]
        public long roleid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string salt { get; set; }
        public virtual RoleModel role { get; set; }
    }
}
