using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Roazhon_Fest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Connexion()
        {
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

        public ActionResult AccueilConvive()
        {
            return View();
        }

        public ActionResult Inscription()
        {
            return View();
        }
        public ActionResult AccueilAdministrateur()
        {
            return View();
        }

    }
}