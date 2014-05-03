using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
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

        public FileResult UserBillImage(int counterId)
        {
            //using (var db = new GazDBContext())
            //{

            //    var mu = Membership.GetUser(User.Identity.Name);
               
            //    var urepo = new UserRepository(db);
            //    var user = urepo.(counterId);

            //    var crepo = new CountersRepository(db);
            //    var counter = crepo.GetByID(counterId);
                
            //    var userBillModel = new UserBillModel()
            //    {
            //        Counter = counterId.ToString(),

            //    };
            //}

            var userBillModel = new UserBillModel()
            {
                Name = "testName",
                Amount = "123",
                BillId = Guid.NewGuid().ToString(),
                Counter = counterId.ToString(),
                Date = DateTime.UtcNow
            };

            var image = ImageService.GenerateBillImage(userBillModel);

            using (var streak = new MemoryStream())
            {
                image.Save(streak, ImageFormat.Jpeg);
                return File(streak.ToArray(), "image/jpg");
            }   
        }
    }
}
