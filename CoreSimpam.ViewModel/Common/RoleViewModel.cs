using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreSimpam.ViewModel
{
    public class RoleViewModel
    {
        public long RoleID { get; set; }
        [Required(ErrorMessage = "Role Name must be not null")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "Enable")]
        public bool IsEnabled { get; set; }
        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();
    }
}
