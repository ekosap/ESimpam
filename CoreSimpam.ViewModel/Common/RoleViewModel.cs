using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreSimpam.ViewModel
{
    public class RoleViewModel
    {
        public long RoleID { get; set; }
        [Required(ErrorMessage = "Role Name must be not null")]
        public string RoleName { get; set; }
        public bool IsEnabled { get; set; }
        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();
    }
}
