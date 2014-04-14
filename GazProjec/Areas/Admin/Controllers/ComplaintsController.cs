using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gaz.DAL;
using GazProjec.Areas.Admin.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace GazProjec.Areas.Admin.Controllers
{
    public class ComplaintsController : Controller
    {
        //
        // GET: /Admin/AdminComplaints/

        public ActionResult Index()
        {
            return View(GetData());
        }


        public List<ComplaintModel> GetData()
        {
            using (var db = new GazDBContext())
            {
                return db.UserComplaints.Select(o => new ComplaintModel
                {
                    CounterID = o.CounterID,
                    ComplaintDescription = o.ComplaintDescription,
                    ComplaintID = o.ID,
                    CreateTime = o.CreateTime,
                    Disable = o.Disable,
                    UserID = o.UserID
                }).Where(o => o.Disable == false).ToList();
            }
        }

        public ActionResult ComplaintRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetData(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult ComplaintUpdate([DataSourceRequest] DataSourceRequest request, ComplaintModel comp)
        {
            if (ModelState.IsValid)
            {
                using (var db = new GazDBContext())
                {
                    var result = db.UserComplaints.Single(o => o.ID == comp.ComplaintID);

                    result.Disable = comp.Disable;

                    db.SaveChanges();
                }
            }

            return Json(new[] { comp }.ToDataSourceResult(request, ModelState));
        }

    }
}
