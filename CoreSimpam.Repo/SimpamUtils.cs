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
        /// <summary>
        /// param => param1,param2,param3
        /// </summary>

        public static void SkipToValid(this ModelStateDictionary modelState, string param)
        {
            foreach (string item in param.Split(',').ToArray())
                modelState.Remove(item);
        }
    }
}
