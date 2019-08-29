using JobsProjectApp.Models;
using JobsProjectApp.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobsProjectApp.Controllers
{
    [Authorize(Roles ="Publisher")]
    public class PublisherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Publisher
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var publisherJobs = db.Jobs.Where(j => j.UserId == userId).ToList();
            return View(publisherJobs);
        }

        public ViewResult GetJobApplicants(int id)
        {
            JobApplicantsViewModel javm;
            List<JobApplicantsViewModel> javmList = new List<JobApplicantsViewModel>();
            var applicants = (from j in db.Jobs
                              join uj in db.SeekersToJobs on j.job_id equals uj.job_id
                              join u in db.Users on uj.UserId equals u.Id
                              where j.job_id == id
                              select new
                              {
                                  job_id = j.job_id,
                                  user_id = u.Id,
                                  user_name = u.UserName
                              }).ToList();
            foreach (var item in applicants)
            {
                javm = new JobApplicantsViewModel();
                javm.Job_Id = item.job_id;
                javm.User_Id = item.user_id;
                javm.User_Name = item.user_name;
                javmList.Add(javm);
            }
            return View(javmList);
        }

        public ActionResult ShowApplicantProfile(string user_id, int job_id)
        {
            ApplicantProfileViewModel apvm = new ApplicantProfileViewModel();
            var profile = (from j in db.Jobs
                           join uj in db.SeekersToJobs on j.job_id equals uj.job_id
                           join u in db.Users on uj.UserId equals u.Id
                           where j.job_id == job_id && u.Id == user_id
                           select new
                           {
                               job_title = j.job_title,
                               job_id = j.job_id,
                               user_id = u.Id,
                               user_name = u.UserName,
                               qualification = u.Qualifications,
                               cv = u.CVpath
                           }).FirstOrDefault();

            apvm.Job_title = profile.job_title;
            apvm.Job_id = profile.job_id;
            apvm.User_id = profile.user_id;
            apvm.Username = profile.user_name;
            apvm.Qualification = profile.qualification;
            apvm.cv = profile.cv;

            return View(apvm);
        }


        public ActionResult AcceptProfile(string user_id, int job_id)
        {
            SeekerToJob sj = db.SeekersToJobs.Where(s => s.UserId == user_id && s.job_id == job_id).FirstOrDefault();
            sj.IsAccepted = true;
            db.SaveChanges();
            return RedirectToAction("GetJobApplicants", new { id = job_id });
        }

        public ActionResult RejectProfile(string user_id, int job_id)
        {
            SeekerToJob sj = db.SeekersToJobs.Where(s => s.UserId == user_id && s.job_id == job_id).FirstOrDefault();
            sj.IsAccepted = false;
            db.SaveChanges();
            return RedirectToAction("GetJobApplicants", new { id = job_id });
        }
    }
}