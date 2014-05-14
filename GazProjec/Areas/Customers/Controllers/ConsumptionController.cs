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
using GazProjec.Services;
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
            return View(GetListOfCounters(User.Identity.Name));
        }

        //GET: 
        public ActionResult GetConsumptionTable(int counterId, DateTime startTime, DateTime endTime)
        {
            var data = GetConsumptionData(counterId, startTime, endTime);

            ViewBag.Month = GetValueByMonth(data);

            ViewBag.Year = GetValueByYear(GetConsumptionData(counterId, new DateTime(1900, 1, 1), DateTime.Now));

            return PartialView("_CounterReadTable", data);
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
                    Text = CounterUiSerivce.GetDetails(counter)
                }).ToList();
            }
        }

        private List<ChartModel> GetValueByMonth(List<CounterReadModel> data)
        {
            var dataByYear = data.GroupBy(o => o.CreateTime.Year);

            var list = new List<ChartModel>();

            foreach (var item in dataByYear)
            {
                var byMonth = item.GroupBy(o => o.CreateTime.Month);

                foreach (var month in byMonth)
                {
                    list.Add(new ChartModel
                    {
                        Date = month.First().CreateTime,
                        Value = month.Sum(o => o.ReadAmount)
                    });
                }

            }

            return list.OrderBy(o => o.Date).ToList();
        }

        private List<ChartModel> GetValueByYear(List<CounterReadModel> data)
        {

            var list = new List<ChartModel>();

            var dataByYear = data.GroupBy(o => o.CreateTime.Year);

            foreach (var year in dataByYear)
            {
                list.Add(new ChartModel
                {
                    Date = year.First().CreateTime,
                    Value = year.Sum(o => o.ReadAmount)
                });
            }

            return list.OrderBy(o => o.Date).ToList();
        }


    }
}
