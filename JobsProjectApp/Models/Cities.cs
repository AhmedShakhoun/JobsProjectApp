using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsProjectApp.Models
{
    public class Cities
    {
        [Key]
        public int cityId { get; set; }
        [Required(ErrorMessage= "Please Enter City Name ?" )]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage ="City Name Accept characters only!")]
        public string cityName { get; set;}
        public int CountryId { get; set; }
        public virtual Countries country { get; set; }
    }
}