using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gaz.DAL;
using Gaz.DAL.DbContexts;
using Gaz.DAL.Repositories;
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

        public ActionResult CounterRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetCounters().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UsersUpdate(CounterModel counter)
        {
            if (ModelState.IsValid)
            {
                using (var db = new GazCountersDbContext())
                {
                    var arepo = new AddressesRepository(db);
                    var address = arepo.GetByID(counter.AddressID);
                    
                    if (address == null)
                        throw new Exception("Address ID cant be found in DB, maybe deleted or data is invalid");

                    counter.AddressData.ToDbAddress(address);
                    arepo.Update(address);

                    arepo.Commit();
                }
            }

            return null;
        }

        public ActionResult AddCounterView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCounter(string cityName, string streetName, int houseNumber, int apartmentNumber, float longitude, float latitude)
        {
            using (var db = new GazDbContext())
            {
                var result = db.Addresses.Add(new Address
                {
                    ApartmentNumber = apartmentNumber,
                    CityName = cityName,
                    HouseNumber = houseNumber,
                    StreetName = streetName,
                    latitude = latitude,
                    longitude = longitude,
                });

                db.SaveChanges();

                db.Counters.Add(new Counter()
                {
                    AddressID = result.ID
                });

                db.SaveChanges();
            }

            return Content("נוסף מונה");
        }

        private IEnumerable<CounterModel> GetCounters()
        {
            using (var db = new GazCountersDbContext())
            {
                var crepo = new CountersRepository(db);
                var counters = crepo.GetAll("Address").ToList();
                var model = counters.Select(s => new CounterModel(s));

                return model;
            }
        }

    }
}
