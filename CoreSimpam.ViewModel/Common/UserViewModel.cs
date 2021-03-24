using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.ViewModel
{
    public class UserViewModel
    {
        public long UserID { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username not allow null")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password not allow null")]
        [MinLength(8, ErrorMessage = "Password min 8 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email not allow null")]
        [EmailAddress(ErrorMessage = "Must be valid email")]
        public string Email { get; set; }
        public string Salt { get; set; }
        public long RoleID { get; set; }
        public string RoleName { get; set; }
        public List<UserViewModel> users { get; set; } = new List<UserViewModel>();
    }
}
