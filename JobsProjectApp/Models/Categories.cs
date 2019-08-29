using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace JobsProjectApp.Models
{
    public class Categories
    {
        [Key]
        public int cat_id { get; set; }
        [Required(ErrorMessage = "Please Enter Category Name:")]
        [Display(Name = "Category Name")]
        [Remote("CheckCatName", "Categories", ErrorMessage = "Category Name Already Exists!!")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Category Name Accept characters only!")]
        public string cat_name { get; set; }
        public virtual ICollection<Jobs> Jobs { get; set; }
    }
}