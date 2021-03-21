using System;
using System.Collections.Generic;

namespace CoreSimpam.ViewModel
{
    public class RoleViewModel
    {
        public long RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsEnabled { get; set; }
        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();
    }
}
