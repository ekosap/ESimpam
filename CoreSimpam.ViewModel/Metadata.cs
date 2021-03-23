using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSimpam.ViewModel
{
    public class Metadata<T> where T : new()
    {
        public bool status { get; set; }
        public T data { get; set; } = new T();
    }
    public class Metadata
    {
        public bool status { get; set; }
        public string data { get; set; }
    }
}
