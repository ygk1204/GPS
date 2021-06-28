using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPS201107.Controllers
{
    public class ReturnController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Error_login()
        {
            return View();
        }
        public ActionResult Error_authority()
        {
            return View();
        }
        public ActionResult Return_DocumentManagement()
        {
            return View();
        }
        public ActionResult Return_ExpiredList()
        {
            return View();
        }
        public ActionResult Return_Register()
        {

            return View();
        }
        public ActionResult Return_SupplierInformation()
        {
            return View();
        }
    }
}
