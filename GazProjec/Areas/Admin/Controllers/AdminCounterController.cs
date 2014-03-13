using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GazProjec.Areas.Admin.Controllers
{
    public class AdminCounterController : Controller
    {
        //
        // GET: /Admin/AdminCounter/

        public ActionResult Index()
        {
            return View();
        }

    }
}
