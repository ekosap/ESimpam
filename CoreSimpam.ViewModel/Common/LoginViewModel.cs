using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username not allow null")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password not allow null")]
        public string password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
