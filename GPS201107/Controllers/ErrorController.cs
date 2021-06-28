using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPS201107.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Error_login()
        {
            ViewData["Message"] = "SupplierInformation";
            return View();
        }

    }
}
