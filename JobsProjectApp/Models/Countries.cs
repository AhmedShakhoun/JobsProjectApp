using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsProjectApp.Models
{
    public class Countries
    {

        [Key]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Please Enter Country Name ?")]
        [RegularExpression("^[a-zA-Z ]*$",ErrorMessage = "Country Name Accept characters only!")]
        public string countryName { get; set; }
        public virtual ICollection<Cities> city { get; set; }
    }
}