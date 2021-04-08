using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.ViewModel
{
    public class UserMenuViewModel
    {
        public List<ScreenViewModel> Menu { get; set; } = new List<ScreenViewModel>();
        public int CountChild { get; set; }
    }
}
