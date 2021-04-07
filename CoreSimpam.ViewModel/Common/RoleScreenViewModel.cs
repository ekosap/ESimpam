using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.ViewModel
{
    public class RoleScreenViewModel
    {
        public long RoleID { get; set; }
        public List<ScreenComponentViewModel> Screens { get; set; } = new List<ScreenComponentViewModel>();
    }
    public class ScreenComponentViewModel
    {
        public long RoleID { get; set; }
        public long ScreenID { get; set; }
        public string ScreenName { get; set; }
        public bool ReadFlag { get; set; }
        public bool WriteFlag { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
