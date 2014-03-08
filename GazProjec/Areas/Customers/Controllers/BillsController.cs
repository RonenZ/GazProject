using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GazProjec.Areas.Customers
{
    public class BillsController : Controller
    {
        //
        // GET: /Customers/Bills/

        public ActionResult Index()
        {
            return View();
        }

    }
}
