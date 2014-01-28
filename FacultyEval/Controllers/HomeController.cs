using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FacultyEval.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Models.StudentModel student)
        {

            if (ModelState.IsValid)
            {
                if (isValid(student.studentID, student.studentCOR))
                {
                    FormsAuthentication.SetAuthCookie(student.studentID, false);
                    Session["studentID"] = student.studentID;
                    return RedirectToAction("Index", "Student");
                }
            }

            return View();
        }

        public ActionResult StudentOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool isValid(string studentID, string studentCOR)
        {


            bool isvalid = false;

            using (var db = new Models.fesContext())
            {
                var student = db.Students.FirstOrDefault(u => u.studentID == studentID);

                if (student != null)
                {
                    if (student.studentCOR == studentCOR)
                    {
                        isvalid = true;
                    }
                }
            }


            return isvalid;
        }

    }
}
