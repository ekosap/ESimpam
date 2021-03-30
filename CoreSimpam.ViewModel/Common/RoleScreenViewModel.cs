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
        public List<ScreenComponentViewModel> Screens { get; set; }
    }
    public class ScreenComponentViewModel
    {
        public long ScreenID { get; set; }
        public bool ReadFlag { get; set; }
        public bool WriteFlag { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
