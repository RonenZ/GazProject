using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Gaz.DAL.DbContexts;
using Gaz.DAL.Repositories;
using Gaz.Models.Models;
using GazProjec.Areas.Admin.Models;
using GazProjec.Areas.Customers.Models;

namespace GazProjec.Areas.Customers.Controllers
{
    public class ConsumptionController : Controller
    {
        //
        // GET: /Customers/Consumption/

        public ActionResult Index()
        {
            return View(GetListOfCounters(User.Identity.Name));
        }

        //GET: 


        public ActionResult GetConsumptionTable(int counterId, DateTime startTime, DateTime endTime)
        {
            return PartialView("_CounterReadTable", GetConsumptionData(counterId, startTime, endTime));
        }


        private List<CounterReadModel> GetConsumptionData(int counterId, DateTime startTime, DateTime endTime)
        {
            using (var db = new GazDbContext())
            {
                var counterReads =  db.CounterReads.ToList().Where(
                    o => o.CounterID == counterId && o.CreateTime >= startTime && o.CreateTime <= endTime);

                return counterReads.Select(o => new CounterReadModel
                {
                    CounterID = o.CounterID,
                    CreateTime = o.CreateTime,
                    ReadAmount = o.ReadAmount,
                    ReadID = o.ID
                }).OrderByDescending(o => o.CreateTime).ToList();
            }
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

        private string CounterDetails(Counter counter)
        {
            return string.Format("{0} {1} {2}, {3}", counter.Address.CityName, counter.Address.StreetName, counter.Address.HouseNumber,
                counter.Address.ApartmentNumber);
        }



    }
}
