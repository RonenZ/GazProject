﻿
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gaz.DAL.DbContexts;
using Gaz.DAL.Repositories;
using Gaz.Models.Models;
using Gaz.DAL;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using GazProjec.Areas.Admin.Models;

namespace GazProjec.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Admin/AdminUsers/

        public ActionResult Index()
        {
            return View(GetUsers());
        }

        internal List<UserModel> GetUsers()
        {
            using (var db = new GazDbContext())
            {
                return db.Users.Select(o => new UserModel 
                { 
                    ID = o.ID,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    PhoneNumber = o.PhoneNumber,
                    Email = o.Email,
                    Password = o.Password,
                    RoleID = o.RoleID,
                    Username = o.Username
                
                }).ToList() ;
            }
        }

        public JsonResult UsersRead([DataSourceRequest]DataSourceRequest request)
        {
            return Json(GetUsers().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UsersUpdate([DataSourceRequest]DataSourceRequest request, UserModel user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new GazDbContext())
                {
                    var result = db.Users.Single(o => o.ID == user.ID);

                    result.FirstName = user.FirstName;
                    result.LastName = user.LastName;
                    result.Password = user.Password;
                    result.PhoneNumber = user.PhoneNumber;
                    result.RoleID = user.RoleID;
                    result.Email = user.Email;

                    db.SaveChanges();
                }
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult RemoveUser([DataSourceRequest]DataSourceRequest request, UserModel user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new GazDbContext())
                {

                    var entity = db.Users.Single(o => o.ID == user.ID);

                    db.Users.Remove(entity);
                    db.SaveChanges();
                }
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult GetCountersForUser(int userID)
        {
            var model = new UserCounterModel(userID);

            using (var db = new GazDbContext())
            {
                for (var index = 0; index < model.Counters.Count; index++)
                {
                    var counter = model.Counters[index];
                    var reads = db.CounterReads.Where(o => o.CounterID == counter.CounterID);
                    model.Counters[index].SumReads = reads.Select(o => o.ReadAmount).Sum();
                }
            }

            return PartialView("~/Areas/Admin/Views/Counter/_UserCounter.cshtml", model);
        }

        public ActionResult AddNewUser([DataSourceRequest]DataSourceRequest request, UserModel model)
        {
            using (var db = new GazDbContext())
            {
                db.Users.Add(new User
                {
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    RoleID = model.RoleID,
                    Username = model.Username,
                });

                db.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

    }


}
