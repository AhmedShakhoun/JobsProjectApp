using JobsProjectApp.Models;
using JobsProjectApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobsProjectApp.Controllers
{
    [Authorize(Roles = "Applicant")]
    public class ApplicantController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Applicant
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitProfile(ProfileViewModel pvm, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var currentUser = db.Users.Where(u => u.Id == userId).FirstOrDefault();

                string path = Path.Combine(Server.MapPath("~/CV"), upload.FileName);
                upload.SaveAs(path);
                pvm.CV = upload.FileName;

                currentUser.Qualifications = pvm.Qualification;
                currentUser.CVpath = pvm.CV;
                
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View("CreateProfile", pvm);
        }
        public ActionResult ShowApplicantJobs()
        {
            ApplicantViewModel appvm;
            List<ApplicantViewModel> appvmres = new List<ApplicantViewModel>();
            string userid = User.Identity.GetUserId();
            var applicantsdata = (from j in db.Jobs
                                 join uj in db.SeekersToJobs on j.job_id equals uj.job_id
                                 where uj.UserId == userid
                                 select new
                                 {
                                     job_id = j.job_id,
                                     Job_Title = j.job_title,
                                     Is_Accepted = uj.IsAccepted
                                 }).ToList();
            foreach (var item in applicantsdata)
            {
                appvm = new ApplicantViewModel();
                appvm.Job_Id = item.job_id;
                appvm.Job_Title = item.Job_Title;
                appvm.Is_Accepted = item.Is_Accepted;
                appvmres.Add(appvm);
            }
            return View(appvmres);
        }

    }
}