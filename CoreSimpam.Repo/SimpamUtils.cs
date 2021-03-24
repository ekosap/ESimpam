using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Repo
{
    public static class SimpamUtils
    {
        public static void SkipToValid(this ModelStateDictionary modelState, string[] param)
        {
            foreach (string item in param)
                modelState.Remove(item);
        }
    }
}
