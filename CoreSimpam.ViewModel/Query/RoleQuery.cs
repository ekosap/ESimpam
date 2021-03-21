using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSimpam.ViewModel.Query
{
    public class RoleQuery
    {
        public string searchValue { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 25;
    }
}
