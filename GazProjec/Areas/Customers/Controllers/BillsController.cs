using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Gaz.DAL.DbContexts;
using Gaz.DAL.Repositories;
using Gaz.Models.Models;
using GazProjec.Models;
using GazProjec.Services;

namespace GazProjec.Areas.Customers.Controllers
{
    public class BillsController : Controller
    {
        //
        // GET: /Customers/Bills/

        public ActionResult Index()
        {


            return View();
        }

        public JsonResult GetCascadeCounters()
        {
            using (var db = new GazDbContext())
            {
                var urepo = new UserRepository(db);
                var counters = urepo.GetCountersByUserName(User.Identity.Name)
                .Select(counter => new
                {
                    CounterID = counter.ID.ToString(CultureInfo.InvariantCulture),
                    Address = CounterUiSerivce.GetDetails(counter)
                }).ToList();

                return Json(counters, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCascadeBillsMonths(int? counterId)
        {
            if (!counterId.HasValue)
            {
                return null;
            }

            using (var db = new GazDbContext())
            {
                var brepo = new BillsRepository(db);
                var bills = brepo.GetAllBillsByCounterId(counterId.Value).Select(s => s.CreateTime).ToList();

                return Json(bills.Select(s => new
                {
                    MonthFormatted = s.ToString("MM/yyyy"),
                    Date = s.ToString("dd/MM/yyyy")
                }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetBillImage(int counterId, string dateStr)
        {
            UserBillModel userBillModel;

            var date = DateTime.ParseExact(dateStr, "dd/MM/yyyy", CultureInfo.CurrentCulture);
            using (var db = new GazDbContext())
            {
                var repo = new BillsRepository(db);
                var userBill = repo.GetBillByDate(counterId, date);

                if (userBill == null)
                {
                    return Content("no image");
                }

                userBillModel = new UserBillModel(User.Identity.Name, userBill.BillAmount.ToString(CultureInfo.InvariantCulture),
                    userBill.CreateTime, userBill.ID.ToString(CultureInfo.InvariantCulture), userBill.CounterID.ToString());
            }

            return this.UserBillImage(userBillModel);
        }

        public FileResult UserBillImage(UserBillModel userBillModel)
        {
            var image = ImageService.GenerateBillImage(userBillModel);

            using (var streak = new MemoryStream())
            {
                image.Save(streak, ImageFormat.Jpeg);
                return File(streak.ToArray(), "image/jpg");
            }
        }
    }
}
