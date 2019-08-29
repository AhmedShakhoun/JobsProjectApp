using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace JobsProjectApp.Models
{
    public class SeekerToJob
    {
        [Key]
        public int apply_id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool? IsAccepted { get; set; }
        public int job_id { get; set; }
        public virtual Jobs Job { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}