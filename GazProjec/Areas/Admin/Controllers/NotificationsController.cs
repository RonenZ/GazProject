﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Gaz.DAL;
using Gaz.DAL.DbContexts;
using Gaz.Models.Models;
using GazProjec.Areas.Admin.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Gaz.DAL.Repositories;

namespace GazProjec.Areas.Admin.Controllers
{
    public class NotificationsController : Controller
    {
        //
        // GET: /Admin/Notifications/
        public ActionResult Index()
        {
            return View(GetData());
        }

        public JsonResult ReadNotifications([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult NotificationUpdate([DataSourceRequest] DataSourceRequest request, NotificationModel note)
        {
            if (ModelState.IsValid)
            {
                using (var db = new GazDbContext())
                {
                    var result = db.UserNotifications.Single(o => o.ID == note.NotificationID);

                    result.UserID = note.UserID;
                    result.CreateTime = note.CreateTime;
                    result.Disabled = note.Disabled;
                    result.NotificationDescription = note.NotificationDescription;

                    db.SaveChanges();
                }
            }

            return Json(new[] { note }.ToDataSourceResult(request, ModelState));
        }

        private List<NotificationModel> GetData()
        {
            using (var db = new GazDbContext())
            {
                return db.UserNotifications.Select(o => new NotificationModel
                {
                    NotificationID = o.ID,
                    NotificationDescription = o.NotificationDescription,
                    UserID = o.UserID,
                    UserName =  o.User.FirstName + " " + o.User.LastName,
                    CreateTime = o.CreateTime,
                    Disabled = o.Disabled
                }).ToList();
            }
        }

        private void SetNotification(int userID, string message)
        {
            using (var db = new GazDbContext())
            {
                db.UserNotifications.Add(new UserNotification()
                {
                    UserID = userID,
                    CreateTime = DateTime.Now,
                    Disabled = false,
                    NotificationDescription = message
                });

                db.SaveChanges();
            }
        }

        private void SetNotificationForMultipleUsers(string message, int[] userIds)
        {
            userIds = userIds.Distinct().ToArray();
            
            foreach (var id in userIds)
            {
                SetNotification(id, message);
            }
        }

        [HttpPost]
        public void SetNotificationForMultipleUsers(string message, int[] userIds, bool json)
        {
            using (var db = new GazDbContext())
            {
                var repo = new UserNotificationRepository(db);
                repo.AddNotificationForMultipleUsers(userIds, message);
                repo.Commit();
            }
        }

        private void SetNotificationForAllUsers(string message)
        {
            using (var db = new GazDbContext())
            {
                var users = db.Users.Where(o => o.RoleID == 1).Select(o => o.ID).ToArray();

                SetNotificationForMultipleUsers(message, users);
            }
        }

        public ActionResult GetAddNotificationView()
        {
            return PartialView("_AddNotification", GetUsers());
        }

        internal List<UserModel> GetUsers()
        {
            var control = new UsersController();
            return control.GetUsers();
        }
    }
}
