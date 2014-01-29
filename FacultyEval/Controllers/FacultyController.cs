using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacultyEval.Controllers
{
    public class FacultyController : Controller
    {
        //
        // GET: /Faculty/

        public ActionResult Index()
        {
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

            }


            return View();
        }

        private bool isValid(string userID, string password)
        {
            bool isvalid = false;

            using (var db = new Models.fesContext())
            {
                var admin = db.Faculties.FirstOrDefault(u => u.userID == userID);

                if (admin != null)
                {
                    if (admin.password == password)
                    {
                        isvalid = true;
                    }
                }
            }


            return isvalid;
        }

    }
}
