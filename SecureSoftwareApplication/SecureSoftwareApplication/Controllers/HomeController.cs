using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecureSoftwareApplication.Controllers
{
    public class HomeController : RootController
    {
        public ActionResult Index()
        {

            ViewBag.isAdmin = isAdmin();

            if (getAccount() != null)
            {
                ViewBag.IsLoggedIn = true; 
            }
            else
            {
                ViewBag.IsLoggedIn = false;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}