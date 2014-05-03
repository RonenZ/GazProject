using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gaz.DAL.DbContexts;
using GazProjec.Areas.Customers.Models;

namespace GazProjec.Areas.Customers.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Customers/Home/

        public ActionResult Index()
        {
            return View(GetSystemNotifications());
        }

        public List<NotificationViewModel> GetSystemNotifications()
        {
            using (var db = new GazDbContext())
            {
                var userId = db.Users.Single(o => o.Username == User.Identity.Name);

                var notifications = db.UserNotifications.Where(o => o.UserID == userId.ID && o.Disabled == false);

                return notifications.Select(o => new NotificationViewModel {Content = o.NotificationDescription, CreateTime = o.CreateTime}).ToList();
            }
        }

    }
}
