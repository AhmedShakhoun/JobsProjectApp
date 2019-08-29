using JobsProjectApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JobsProjectApp.Controllers
{
    
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Jobs
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Jobs.ToList());
        }

        [AllowAnonymous]
        public ActionResult JobsInCategories(int id)
        {
            var jobs = db.Jobs.Where(j => j.cat_id == id).ToList();
            return View(jobs);
        }

        //Details

        // Get Create
        [Authorize(Roles="Publisher,Admins")]
        public ActionResult Create()
        {
            ViewBag.cat_id = new SelectList(db.Categories, "cat_id", "cat_name");
            return View();
        }

        //Post Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "job_id,job_title,job_description,job_requirement,cat_id")]*/ Jobs jobs)
        {
            if(ModelState.IsValid)
            {
                jobs.UserId = User.Identity.GetUserId();
                db.Jobs.Add(jobs);
                db.SaveChanges();
                return RedirectToAction("Index", "Publisher");
            }

            ViewBag.cat_id = new SelectList(db.Categories, "cat_id", "cat_name", jobs.cat_id);
            return View(jobs);
        }

        //Get Edit
        [Authorize(Roles = "Publisher,Admins")]
        public ActionResult Edit(int ? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs jobs = db.Jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }

            ViewBag.cat_id = new SelectList(db.Categories, "cat_id", "cat_name", jobs.cat_id);
            return View(jobs);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "job_id,job_title,job_description,job_requirement,cat_id")] Jobs jobs)
        {
            if (ModelState.IsValid)
            {
                jobs.UserId = User.Identity.GetUserId();
                db.Entry(jobs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Publisher");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "cat_id", "cat_name", jobs.cat_id);
            
            return View(jobs);
        }

        //Get Delete
        [Authorize(Roles = "Publisher,Admins")]
        public ActionResult Delete(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs jobs = db.Jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        //Post Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jobs jobs = db.Jobs.Find(id);
            db.Jobs.Remove(jobs);
            db.SaveChanges();
            return RedirectToAction("Index", "Publisher");
        }
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs jobs = db.Jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            Session["job_id"] = id;
            int cat_id = jobs.cat_id;
            Session["cat_id"] = cat_id;
            return View(jobs);
        }

        [Authorize(Roles="Applicant")]
        public ActionResult ApplyForJob()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ApplyForJob(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var job_id = (int)Session["job_id"];
            var check = db.SeekersToJobs.Where(j => j.job_id == job_id && j.UserId == UserId).ToList();
            if (check.Count < 1)
            {
                var SeekToJob = new SeekerToJob();
                SeekToJob.UserId = UserId;
                SeekToJob.job_id = job_id;
                SeekToJob.Message = Message;
                SeekToJob.Date = DateTime.Now;
                db.SeekersToJobs.Add(SeekToJob);
                db.SaveChanges();
                ViewBag.Msg = "Data Send Successfully";
            }
            else
            {
                ViewBag.Msg = "User can't apply for the job more than one time";
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}