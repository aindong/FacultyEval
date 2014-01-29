using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FacultyEval.Controllers
{
    public class FacultyController : Controller
    {
        //
        // GET: /Faculty/

        public ActionResult Index()
        {
            string role = Session["role"] != null ? Session["role"].ToString() : "";
            if (!Request.IsAuthenticated || role != "faculty")
            {
                return RedirectToAction("FacultyLogin");
            }

            return View();
        }

        [HttpGet]
        public ActionResult FacultyLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FacultyLogin(Models.Faculty faculty)
        {

            if(ModelState.IsValid)
            {
                if(isValid(faculty.userID, faculty.password))
                {
                    FormsAuthentication.SetAuthCookie(faculty.userID, false);
                    Session["userID"] = faculty.userID;
                    Session["role"] = "faculty";
                    return RedirectToAction("Index");
                }
            }


            return View();
        }

        private bool isValid(string userID, string password)
        {
            bool isvalid = false;

            using (var db = new Models.fesContext())
            {
                var faculty = db.Faculties.FirstOrDefault(u => u.userID == userID);

                if (faculty != null)
                {
                    if (faculty.password == password)
                    {
                        isvalid = true;
                    }
                }
            }


            return isvalid;
        }

    }
}
