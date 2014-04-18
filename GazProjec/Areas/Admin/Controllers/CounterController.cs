using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gaz.DAL;
using Gaz.DAL.DbContexts;
using Gaz.Models.Models;
using GazProjec.Areas.Admin.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace GazProjec.Areas.Admin.Controllers
{
    public class CounterController : Controller
    {
        //
        // GET: /Admin/AdminCounter/

        public ActionResult Index()
        {
            return View(GetCounters());
        }

        private List<CounterModel> GetCounters()
        {
            using (var db = new GazDbContext())
            {
                var model = new List<CounterModel>();
                
                foreach (var counter in db.Counters)
                {
                    model.Add(new CounterModel(counter));
                }

                return model;
            }
        }

        public ActionResult CounterRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetCounters().ToDataSourceResult(request), JsonRequestBehavior.AllowGet); 
        }

        public ActionResult UsersUpdate([DataSourceRequest]DataSourceRequest request, CounterModel counter)
        {
            if (ModelState.IsValid)
            {
                using (var db = new GazDbContext())
                {
                    var result = db.Addresses.Single(o => o.ID == counter.AddressID);

                    result.CityName = counter.AddressData.CityName;
                    result.StreetName = counter.AddressData.StreetName;
                    result.HouseNumber = counter.AddressData.HouseNumber;
                    result.ApartmentNumber = counter.AddressData.ApartmentNumber;
                    result.latitude = counter.AddressData.Latitude;
                    result.longitude = counter.AddressData.Longitude;

                    db.SaveChanges();
                }
            }

            return Json(new[] { counter }.ToDataSourceResult(request, ModelState));
        }

    }
}
