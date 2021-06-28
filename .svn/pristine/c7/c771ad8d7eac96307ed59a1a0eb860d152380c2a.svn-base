using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASEWCFServiceLibrary.App_Code;
using GPS201107.Models;

namespace GPS201107.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        public ActionResult Index()
        {
            IList<GPBOARD> resultList = new List<GPBOARD>();
            GPBOARD main = new GPBOARD();

            EntityMapper omapper = new EntityMapper();

            omapper.oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            omapper.Table_entity.Add("WEBBOARD", main);
            omapper.WhereCondition = " where af_check in ('1','5') and af_boardname='notice' order by af_number desc ";
            omapper.Load(1, 3);
            for (int i = 0; i < omapper.Result[0].Count; i++)
            {
                resultList.Add((GPBOARD)omapper.Result[0][i]);
            }

            return View(resultList);
        }
        public ActionResult Index_Action()
        {
           // String sId =  Request.Form["txtId"].ToString();
           // String sPassword = Request.Form["txtPassword"].ToString();

            //로그인 처리.
            return RedirectToAction("Menu" ,"Main");
        }
        public ActionResult Menu()
        {
            return View();
        }
    }
}
