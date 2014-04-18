using Gaz.DAL;
using Gaz.DAL.DbContexts;
using Gaz.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace GazProjec.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var test = WebSecurity.CurrentUserName;
            var db = new GazDbContext();
            var repo = new UserRepository(db);
            var users = repo.GetAll();

            return View(users);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
