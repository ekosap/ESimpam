using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.ViewModel
{
    public class ResidentViewModel
    {
        public long ResidentID { get; set; }
        [Required]
        [Display(Name = "Resident Name")]
        public string ResidentName { get; set; }
        [Display(Name = "Resident Number")]
        public string ResidentNumber { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
