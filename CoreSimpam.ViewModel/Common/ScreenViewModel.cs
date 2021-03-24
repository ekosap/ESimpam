using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.ViewModel
{
    public class ScreenViewModel
    {
        public long ScreenID { get; set; }
        public string ScreenName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ParentName { get; set; }
        public bool IsMenu { get; set; }
        public bool IsActive { get; set; }
        public long ParentID { get; set; }
        public List<ScreenViewModel> screens { get; set; } = new List<ScreenViewModel>();
    }
}
