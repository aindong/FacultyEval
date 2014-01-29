using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacultyEval.Controllers
{

    public class StudentController : Controller
    {
        //
        // GET: /Student/
        private Models.fesContext db = new Models.fesContext();
        private int evalID;

        public ActionResult Index()
        {

            if (!Request.IsAuthenticated && Session["role"] != "student" )
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.EvaluationStatus = CheckEvaluationStatus();
            string studentID = Session["studentID"].ToString();
            var faculty = from f in db.FacultyStudents
                            where f.studentID == studentID
                            select f;

            return View(faculty);
        }

        

       [HttpGet]
        public ActionResult EvaluateFaculty(string stid, string fid, string sid)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

           ViewBag.EvaluationStatus = CheckEvaluationStatus();
           if (!ViewBag.EvaluationStatus)
           {
               return RedirectToAction("Index", "Student");
           }

            Session["stid"] = stid;
            Session["fid"] = fid;
            Session["sid"] = sid;

            ViewBag.evalID = evalID;
            return View();
        }

       [HttpPost]
       public ActionResult EvaluateFaculty(Models.EvaluationResult result)
       {
           if (ModelState.IsValid)
           {
               db.EvaluationResults.Add(result);
               db.SaveChanges();

               string stid = Session["stid"].ToString();
               string fid = Session["fid"].ToString();
               string sid = Session["sid"].ToString();

               var c = db.FacultyStudents.First(i => i.studentID == stid && i.facultyID == fid && i.subjectID == sid);
               var lastResult = db.EvaluationResults.OrderByDescending(u => u.resultID).FirstOrDefault();
               c.resultID = lastResult.resultID;
               db.SaveChanges();
               return RedirectToAction("Index", "Student");
           }
           return View();
       }

      

       private bool CheckEvaluationStatus()
       {
           bool isactive = false;

           var settings = db.EvaluationSchedules.OrderByDescending(s => s.scheduleID).FirstOrDefault();
           if (settings != null)
           {
               ViewBag.lastStart = settings.EvaluationStart;
               ViewBag.lastEnd = settings.EvaluationEnd;
               evalID = settings.scheduleID;
               if (ViewBag.lastStart < DateTime.Now && ViewBag.lastEnd > DateTime.Now)
               {
                   isactive = true;
               }
               else
               {
                    isactive =  false;
               }
           }
           else
           {
               isactive = false;
           }

           return isactive;
       }
    }
}
