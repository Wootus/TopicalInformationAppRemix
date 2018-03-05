using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TopicalInformationApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [StringLength(50), Required (ErrorMessage = "A Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A Style Is Required")]
        public string Style { get; set; }

        [Range(3, 14, ErrorMessage ="ABV Must Fall Within 3% and 14%"), Required(ErrorMessage = "As ABV Is Required")]
        public double ABV { get; set; }

        [Range(1, 150, ErrorMessage = "IBU Must Fall Within 1 and 150"), Required(ErrorMessage = "An IBU Is Required")]
        public double IBU { get; set; }
    }
}