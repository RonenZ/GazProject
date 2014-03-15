using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gaz.DAL;
using GazProjec.Areas.Admin.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace GazProjec.Areas.Admin.Controllers
{
    public class AdminCounterController : Controller
    {
        //
        // GET: /Admin/AdminCounter/

        public ActionResult Index()
        {
            return View(GetCounters());
        }

        private List<CounterModel> GetCounters()
        {
            using (var db = new GazDBContext())
            {
                var model = new List<CounterModel>();
                
                foreach (var counter in db.Counters)
                {
                    model.Add(new CounterModel(counter.ID));
                }

                return model;
            }
        }

        public ActionResult CounterRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetCounters().ToDataSourceResult(request), JsonRequestBehavior.AllowGet); 
        }

    }
}
