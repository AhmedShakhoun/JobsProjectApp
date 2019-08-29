using JobsProjectApp.Models;
using JobsProjectApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace JobsProjectApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ActionDirect()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            if (currentUser.UserType == "Applicant")
            {
                if (currentUser.Qualifications == null || currentUser.CVpath == null)
                {
                    return RedirectToAction("CreateProfile", "Applicant");
                }
                return RedirectToAction("Index");
            }
            else if (currentUser.UserType == "Publisher")
            {
                return RedirectToAction("", "");
            }

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ViewResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel con)
        {
            if (ModelState.IsValid)
            {
                var mail = new System.Net.Mail.MailMessage();
                var loginInfo = new NetworkCredential("menamagdy210@gmail.com", "minamagdyctvchannelmina.com");
                mail.From = new MailAddress(con.Email);
                mail.To.Add(new MailAddress("menamagdy210@gmail.com"));
                mail.Subject = con.Subject;
                mail.IsBodyHtml = true;
                string body = "sender : " + con.Name + "<br />" +
                    "Mail : " + con.Email + "<br />" +
                    "Title : " + con.Subject + "<br />" +
                    "Content : <b>" + con.Message + "</b>";
                mail.Body = body;
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(mail);
                return RedirectToAction("Index");
            }
            return View(con);
            
        }

        public ActionResult GetCategories()
        {
            List<Categories> Categories = db.Categories.ToList();
            ViewBag.CategoryList = new SelectList(Categories, "cat_id", "cat_name");
            return PartialView("_CategoriesListPartialView");
        }

        public ActionResult JobsInCategories(int id)
        {
            var jobs = db.Jobs.Where(j => j.cat_id == id).ToList();
            return View(jobs);
        }
    }
}