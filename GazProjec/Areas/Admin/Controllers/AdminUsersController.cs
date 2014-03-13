using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gaz.DAL.Repositories;
using Gaz.Models.Models;
using Gaz.DAL;

namespace GazProjec.Areas.Admin.Controllers
{
    public class AdminUsersController : Controller
    {
        //
        // GET: /Admin/AdminUsers/

        public ActionResult Index()
        {
            return View(GetUsers());
        }

        public List<User> GetUsers()
        {
            using (var db = new GazDBContext())
            {
                return db.Users.ToList();
            }
            
        }

    }
}
