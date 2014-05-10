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
using WebMatrix.WebData;

namespace GazProjec.Areas.Customers.Controllers
{
    public class ConsumptionController : Controller
    {
        //
        // GET: /Customers/Consumption/

        public ActionResult Index()
        {
            return View(this.GetListOfCounters(User.Identity.Name));
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
                var repo = new CountersRepository(db);
                var counterReads = repo.GetCounterReadsPerPeriod(counterId, startTime, endTime).ToList();

                var readsPerDayGroup = counterReads.GroupBy(g => g.CreateTime.ToString("01/MM/yyyy")).OrderByDescending(o => o.Key);

                return readsPerDayGroup.Select(readsPerDay => new CounterReadModel()
                {
                    CounterID = counterId,
                    CreateTime = DateTime.Parse(readsPerDay.Key),
                    Date = DateTime.Parse(readsPerDay.Key).ToString("dd/MM"),
                    ReadAmount = readsPerDay.Sum(s => s.ReadAmount),
                    ReadID = 0
                }).ToList();
            }
        }

        private IEnumerable<SelectListItem> GetListOfCounters(string userName)
        {
            using (var db = new GazDbContext())
            {
                var urepo = new UserRepository(db);
                var counters = urepo.GetCountersByUserName(userName);

                return counters.Select(counter => new SelectListItem()
                {
                    Value = counter.ID.ToString(CultureInfo.InvariantCulture), 
                    Text = CounterDetails(counter)
                }).ToList();
            }
        }

        private string CounterDetails(Counter counter)
        {
            return string.Format("{0} {1} {2}, {3}", counter.Address.CityName, counter.Address.StreetName, counter.Address.HouseNumber,
                counter.Address.ApartmentNumber);
        }
    }
}
