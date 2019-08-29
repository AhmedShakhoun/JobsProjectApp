using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsProjectApp.ViewModels
{
    public class ApplicantViewModel
    {
        public int Job_Id { get; set; }
        public string Job_Title { get; set; }
        public bool? Is_Accepted { get; set; }
    }
}