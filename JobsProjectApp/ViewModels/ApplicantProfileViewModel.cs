using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsProjectApp.ViewModels
{
    public class ApplicantProfileViewModel
    {
        public string Job_title { get; set; }
        public int Job_id { get; set; }
        public string User_id { get; set; }
        public string Username { get; set; }
        public string Qualification { get; set; }
        public string cv { get; set; }
    }
}