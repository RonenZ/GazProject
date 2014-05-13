using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Gaz.DAL.DbContexts;
using Gaz.DAL;
using Gaz.DAL.Repositories;
using GazProjec.Services;

namespace GazProjec.Areas.Admin.Controllers
{
    public class CounterUserReferenceController : Controller
    {
        //
        // GET: /Admin/CounterUserReference/

        public ActionResult Index()
        {
            ViewBag.Counters = GetListOfCounters();
            ViewBag.Users = GetListOfUsers();
            
            return View();
        }

        private List<SelectListItem> GetListOfCounters()
        {
            using (var db = new GazDbContext())
            {
                var counters = db.Counters.ToList();

                return counters.Select(counter => new SelectListItem
                {
                    Value = counter.ID.ToString(CultureInfo.InvariantCulture),
                    Text = CounterUiSerivce.GetDetails(counter)
                }).ToList();
            }
        }

        private List<SelectListItem> GetListOfUsers()
        {
            using (var db = new GazDbContext())
            {
                var users = db.Users.ToList();

                return users.Select(user => new SelectListItem
                {
                    Value = user.ID.ToString(CultureInfo.InvariantCulture),
                    Text = string.Format("{0} - {2},{1}",user.Username, user.FirstName, user.LastName)
                }).ToList();
            }
        }

        [HttpPost]
        public ActionResult AddReference(int counterId, int userId)
        {
            using (var db = new GazDbContext())
            {
                var userrepo = new UserRepository(db);
                var counterrepo = new CountersRepository(db);
                
                var user = userrepo.GetByID(userId);
                var counter = counterrepo.GetByID(counterId);

                user.User_Counters.Add(counter);
                
                userrepo.Commit();

                return Content("Ref added");
            }
        }

    }
}
