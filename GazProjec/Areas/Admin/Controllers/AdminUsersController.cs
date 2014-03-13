using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gaz.DAL.Repositories;
using Gaz.Models.Models;
using Gaz.DAL;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using GazProjec.Areas.Admin.Models;

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

        private List<UserModel> GetUsers()
        {
            using (var db = new GazDBContext())
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
                using (var db = new GazDBContext())
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
                using (var db = new GazDBContext())
                {
                    //var entity = new User
                    //{
                    //    ID = user.ID,
                    //    FirstName = user.FirstName,
                    //    LastName = user.LastName,
                    //    PhoneNumber = user.PhoneNumber,
                    //    Email = user.Email,
                    //    Password = user.Password,
                    //    RoleID = user.RoleID,
                    //    Username = user.Username
                    //};

                    //db.Users.Attach(entity);

                    var entity = db.Users.Single(o => o.ID == user.ID);

                    db.Users.Remove(entity);
                    db.SaveChanges();
                }
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

    }


}
