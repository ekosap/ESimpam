using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.ViewModel
{
    public class ScreenViewModel
    {
        public long ScreenID { get; set; }
        [Display(Name ="Screen name")]
        [Required(ErrorMessage = "Screen Name must be not null")]
        public string ScreenName { get; set; }
        [Display(Name = "Controller name")]
        [Required(ErrorMessage = "Controller Name must be not null")]
        public string ControllerName { get; set; }
        [Display(Name = "Action name")]
        public string ActionName { get; set; }
        [Display(Name = "CSS Icon")]
        public string IconCss { get; set; }
        public string ParentName { get; set; }
        [Display(Name = "Menu")]
        public bool IsMenu { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public bool AllowRead { get; set; }
        public bool AllowWrite { get; set; }
        public bool AllowDelete { get; set; }
        [Display(Name = "Parent")]
        public long ParentID { get; set; }
        public List<ScreenViewModel> screens { get; set; } = new List<ScreenViewModel>();
    }
}
