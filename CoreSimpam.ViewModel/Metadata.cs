using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSimpam.ViewModel
{
    public class Metadata<T>
    {
        public bool status { get; set; }
        public T data { get; set; }
    }
}
