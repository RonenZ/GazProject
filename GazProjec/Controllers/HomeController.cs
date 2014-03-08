using Gaz.DAL;
using Gaz.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GazProjec.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new GazDBContext();
            var repo = new UserRepository(db);
            var users = repo.GetAll();

            return View(users);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
