using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace JobsProjectApp.Models
{
    public class Jobs
    {
        [Key]
        public int job_id { get; set; }
        [Required(ErrorMessage = "Please Enter Job Title ?")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Job Title Accept characters only!")]
        [Display(Name = "Job Title")]
        public string job_title { get; set; }
        [Required(ErrorMessage = "Please Enter Job Description ?")]
        [RegularExpression("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$", ErrorMessage = "Only Alphabets and Numbers,spaces,underscores allowed in Job Description")]
        [Display(Name = "Job Description")]
        public string job_description { get; set; }
        [Required(ErrorMessage = "Please Enter Job Requirement ")]
        [RegularExpression("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$", ErrorMessage = "Only Alphabets and Numbers,spaces,underscores allowed in Job Requirement")]
        [Display(Name = "Job Requirement")]
        public string job_requirement { get; set; }
        public int cat_id { get; set; }
        public virtual Categories category { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}