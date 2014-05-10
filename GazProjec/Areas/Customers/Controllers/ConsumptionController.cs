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
using Microsoft.Ajax.Utilities;
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
                var counterReads = repo.GetCounterReadsPerPeriod(counterId, startTime, endTime).OrderByDescending(o => o.CreateTime).ToList();

               var list = counterReads.Select(o => new CounterReadModel
                {
                    CounterID = counterId,
                    CreateTime = o.CreateTime,
                    ReadAmount = o.ReadAmount,
                    ReadID = o.ID
                }).ToList();
                
                ViewBag.Overroll = GetChartModel(list.OrderBy(o => o.CreateTime).ToList());

                return list;
            }
        }

        private ChartModel[] GetChartModel(List<CounterReadModel> model)
        {
            if (model != null && model.Count > 0)
            {
                var list = new List<ChartModel>();
                var k = 0;
                var startDate = model[0].CreateTime.Day;
                
                list.Add(new ChartModel
                {
                    Date = model[0].CreateTime,
                    Value = model[0].ReadAmount
                });

                for (var i = 1; i < model.Count; i++)
                {
                    if (startDate != model[i].CreateTime.Day)
                    {
                        startDate = model[i].CreateTime.Day;
                        k++;
                        
                        list.Add(new ChartModel
                        {
                            Date = model[i].CreateTime,
                            Value = list[k - 1].Value + model[i].ReadAmount
                        });
                    }
                    else
                    {
                        list[k].Value += model[i].ReadAmount;
                    }
                }

                return list.OrderByDescending(o => o.Date).ToArray();
            }

            return null;
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
