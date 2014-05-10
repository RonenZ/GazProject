using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gaz.DAL.DbContexts;
using Gaz.Models.Models;

namespace GazProjec.Areas.Customers.Controllers
{
    public class ComplaintsController : Controller
    {
        //
        // GET: /Customers/Complaints/

        public ActionResult Index()
        {
            return View(GetListOfCounters(User.Identity.Name));
        }

        private List<SelectListItem> GetListOfCounters(string userName)
        {
            using (var db = new GazDbContext())
            {
                var user = db.Users.First(o => o.Username == userName);

                var counters = db.Users.Find(user.ID).User_Counters.ToList();
                var list = new List<SelectListItem>();


                foreach (var counter in counters)
                {
                    var counterView = new SelectListItem();
                    counterView.Value = counter.ID.ToString(CultureInfo.InvariantCulture);
                    counterView.Text = CounterDetails(counter);
                    list.Add(counterView);
                }

                return list;
            }
        }

        [HttpPost]
        public ActionResult SubmitComplaint(string desc, int counterId)
        {

            desc = desc.Trim();

            var userId = GetUserIdFromUserName(User.Identity.Name);
            
            using (var db = new GazDbContext())
            {
                var complaint = new UserComplaint
                {
                    ComplaintDescription = desc,
                    CounterID = counterId,
                    Disable = false,
                    CreateTime = DateTime.Now,
                    UserID = userId
                };

                db.UserComplaints.Add(complaint);
                db.SaveChanges();
            }



            return null;
        }

        private int GetUserIdFromUserName(string userName)
        {
            using (var db = new GazDbContext())
            {
                return db.Users.Single(o => o.Username == userName).ID;
            }
        }

        private string CounterDetails(Counter counter)
        {
            return string.Format("{0} {1} {2}, {3}", counter.Address.CityName, counter.Address.StreetName, counter.Address.HouseNumber,
                counter.Address.ApartmentNumber);
        }

    }
}
