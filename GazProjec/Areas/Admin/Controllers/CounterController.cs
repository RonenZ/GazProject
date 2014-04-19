using System;
using System.Collections.Generic;
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
        public ActionResult UsersUpdate([DataSourceRequest]DataSourceRequest request, CounterModel counter)
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

            return Json(new[] { counter }.ToDataSourceResult(request, ModelState));
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
