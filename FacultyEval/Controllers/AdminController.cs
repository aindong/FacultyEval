using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FacultyEval.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated && Session["role"] != "admin")
            {
                return RedirectToAction("AdminLogin");
            }

            GetLastSettings();
            return View();
        }


        private void GetLastSettings()
        {
            using(var db = new Models.fesContext())
            {
                var settings = db.EvaluationSchedules.OrderByDescending(s => s.scheduleID).FirstOrDefault();
                if (settings != null)
                {
                    ViewBag.lastStart = settings.EvaluationStart;
                    ViewBag.lastEnd = settings.EvaluationEnd;
                    if(ViewBag.lastStart < DateTime.Now && ViewBag.lastEnd > DateTime.Now)
                    {
                        ViewBag.evalStatus = "On-Going";
                    }
                    else
                    {
                        ViewBag.evalStatus = "Stopped";
                    }
                }
                
            }
        }

        [HttpPost]
        public ActionResult ApplySettings(Models.EvaluationSchedule evaluationSchedule)
        {

            if(ModelState.IsValid)
            {
                using(var db = new Models.fesContext())
                {
                    db.EvaluationSchedules.Add(evaluationSchedule);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(Models.Admin admin)
        {

            if(ModelState.IsValid)
            {
                if(isValid(admin.userID, admin.password))
                {
                    FormsAuthentication.SetAuthCookie(admin.userID, false);
                    Session["userID"] = admin.userID;
                    Session["role"] = "admin";
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        public ActionResult AdminOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool isValid(string userID, string password)
        {
            bool isvalid = false;

            using(var db = new Models.fesContext())
            {
                var admin = db.Admins.FirstOrDefault(u => u.userID == userID);

                if(admin != null)
                {
                    if(admin.password == password)
                    {
                        isvalid = true;
                    }
                }
            }


            return isvalid;
        }

    }
}
