using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using jQuery.Treeview;
using ASEWCFServiceLibrary.App_Code;
using GPS201107.Models;
using GPS201107.Models.Grid;

namespace GPS201107.Controllers
{
    public class EntityController : Controller
    {

        //
        // GET: /Entity/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetGPSuppliersData()
        {
            // 데이터베이스 연결

            GPSUPPLIER oGpsupplier = new GPSUPPLIER();
            IList<GPSUPPLIER> List_GPSUPPLIER = new List<GPSUPPLIER>();
            List_GPSUPPLIER = oGpsupplier.Get_Suppliers();


            int totalcnt = List_GPSUPPLIER.Count;
            int pageSize = 10;   //  defalut 3
            int totalRecords = totalcnt;
            string totalPages = ((int)Math.Ceiling((float)totalRecords / (float)pageSize)).ToString();

            // 데이터 조회(페이징&정렬)
            var jsonData = new
            {
                total = totalPages,
                page = "1",
                records = totalRecords.ToString(),
                rows = (

                  from oSupplier in List_GPSUPPLIER
                  select new
                  {

                      cell = new string[] {
                       oSupplier.SUPPLIERCODE,
                       oSupplier.SUPPLIERNAME.Replace(",",""), 
                       oSupplier.CONTACT, 
                       oSupplier.EMAIL
                      }
                  }).ToArray()
            };

            return Json(jsonData);
        }
        public ActionResult GetGPSuppliersDataToUpdate()
        {
            // 데이터베이스 연결

            GPSUPPLIER oGpsupplier = new GPSUPPLIER();
            IList<GPSUPPLIER> List_GPSUPPLIER = new List<GPSUPPLIER>();
            List_GPSUPPLIER = oGpsupplier.Get_Suppliers_All();


            int totalcnt = List_GPSUPPLIER.Count;
            int pageSize = 10;   //  defalut 3
            int totalRecords = totalcnt;
            string totalPages = ((int)Math.Ceiling((float)totalRecords / (float)pageSize)).ToString();

            // 데이터 조회(페이징&정렬)
            var jsonData = new
            {
                total = totalPages,
                page = "1",
                records = totalRecords.ToString(),
                rows = (

                  from oSupplier in List_GPSUPPLIER
                  select new
                  {

                      cell = new string[] {
                       oSupplier.SUPPLIERCODE,
                       oSupplier.SUPPLIERNAME.Replace(",",""), 
                       oSupplier.CONTACT, 
                       oSupplier.EMAIL,
                       oSupplier.ACTIVE,
                       oSupplier.DISABLEREASON
                      }
                  }).ToArray()
            };

            return Json(jsonData);
        }
        public ActionResult GetGPSuppliersMaterials(string id)
        {
            // 데이터베이스 연결

            GPMATERIAL oGpmaterial = new GPMATERIAL();
            IList<GPMATERIAL> List_GPMATERIAL = new List<GPMATERIAL>();
            List_GPMATERIAL = oGpmaterial.Get_Materials(id);

            int totalcnt = List_GPMATERIAL.Count;
            int pageSize = 10;   //  defalut 3
            int totalRecords = totalcnt;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);


            // 데이터 조회(페이징&정렬)
            var jsonData = new
            {
                total = totalPages,
                page = 1,
                records = totalRecords,
                rows = (

                  from oMaterial in List_GPMATERIAL
                  select new
                  {
                      i = oMaterial.PARTNUM,
                      cell = new string[] {
                        oMaterial._MATERIALNAME,
                        oMaterial.PARTNUM
                      }
                  }).ToArray()
            };
            return Json(jsonData);
        }
        public ActionResult GetGPSuppliersContact(string id)
        {
            // 데이터베이스 연결

            GPCONTACTPERSON oGcontact = new GPCONTACTPERSON();
            IList<GPCONTACTPERSON> List_GPCONTACT = new List<GPCONTACTPERSON>();
            List_GPCONTACT = oGcontact.Get_Contacts(id);

            int totalcnt = List_GPCONTACT.Count;
            int pageSize = 10;   //  defalut 3
            int totalRecords = totalcnt;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);


            // 데이터 조회(페이징&정렬)
            var jsonData = new
            {
                total = totalPages,
                page = 1,
                records = totalRecords,
                rows = (

                  from oSupplierContact in List_GPCONTACT
                  select new
                  {
                      i = oSupplierContact._SUPPLIERCODE,
                      cell = new string[] {
                        oSupplierContact._SUPPLIERCODE,
                        oSupplierContact._SUPPLIERNAME,
                        oSupplierContact._PERSONNAME,
                        oSupplierContact._MAILADDRESS,
                        oSupplierContact._PHONE,
                        oSupplierContact._MAILTYPE
                      }
                  }).ToArray()
            };
            return Json(jsonData);
        }
        public ActionResult GetGPSuppliersMaterialsToUpdate(string id)
        {
            // 데이터베이스 연결

            GPMATERIAL oGpmaterial = new GPMATERIAL();
            IList<GPMATERIAL> List_GPMATERIAL = new List<GPMATERIAL>();
            List_GPMATERIAL = oGpmaterial.Get_Materials_All(id);

            int totalcnt = List_GPMATERIAL.Count;
            int pageSize = 10;   //  defalut 3
            int totalRecords = totalcnt;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);


            // 데이터 조회(페이징&정렬)
            var jsonData = new
            {
                total = totalPages,
                page = 1,
                records = totalRecords,
                rows = (

                  from oMaterial in List_GPMATERIAL
                  select new
                  {
                      i = oMaterial.PARTNUM,
                      cell = new string[] {
                        oMaterial._MATERIALNAME,
                        oMaterial.PARTNUM,
                        oMaterial._ACTIVE,
                        oMaterial._DISABLEREASON
                      }
                  }).ToArray()
            };
            return Json(jsonData);
        }
        public ActionResult Get_All_ActiveGPSDocument_(GridSettings grid)
        {
            // 데이터베이스 연결
            GPSDOCUMENTLIST oDocument = new GPSDOCUMENTLIST();

            string wherestmt = string.Empty;

            //Search가 있을 경우
            if (grid.IsSearch)
            {

                wherestmt = " where ACTIVE = 'T'  ";

                for (int i = 0; i < grid.Where.rules.Length; i++)
                {
                    if ((grid.Where.rules[i].field == "FILECATEGORY" || grid.Where.rules[i].field == "ACTIVE") && grid.Where.rules[i].data == "ALL")
                    {
                    }
                    else
                    {
                        wherestmt += " " + grid.Where.groupOp + " " + grid.Where.rules[i].ConditionStmt();
                    }
                }
                wherestmt += " order by " + grid.SortColumn + " " + grid.SortOrder;

            }
            else
            {
                wherestmt = " where active ='T' " + " order by " + grid.SortColumn + " " + grid.SortOrder;
            }


            // 페이징 변수 세팅
            IList<GPSDOCUMENTLIST> GPS_LIST = new List<GPSDOCUMENTLIST>();
            string Total_GPSdocumnet = string.Empty;
            GPS_LIST = oDocument.List_Data(wherestmt, grid.PageIndex, grid.PageSize);
            Total_GPSdocumnet = oDocument.GetTotalCount(wherestmt);

            int totalcnt = int.Parse(Total_GPSdocumnet);
            int pageIndex = Convert.ToInt32(grid.PageIndex);
            int pageSize = grid.PageSize;    //  defalut 3
            int totalRecords = totalcnt;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);


            // 데이터 조회(페이징&정렬)
            var jsonData = new
            {
                total = totalPages,
                page = grid.PageIndex,
                records = totalRecords,
                rows = (
                  from oGPS in GPS_LIST
                  select new
                  {
                      i = oGPS.FILEID,
                      cell = new string[] {
                        oGPS.FILEID,
                        oGPS._SUPPLIERCODE, 
                        oGPS._SUPPLIERNAME, 
                        oGPS._MATERIALNAME,
                        oGPS._PARTNUM,
                        oGPS._FILENAME,                  
                        oGPS._FILECATEGORY, 
                        oGPS._ISSUEDATE,
                        oGPS._EXPIREDATE,
                        oGPS._ACTIVE
                      }
                  }).ToArray()
            };
            return Json(jsonData);
        }

        //[2021.03.29] [ASWR_27613_20210325142254] 매개변수로 productname 추가
        public ActionResult GetGPSDocument(GridSettings grid)
        {
            // 데이터베이스 연결
            GPSDOCUMENTLIST oDocument = new GPSDOCUMENTLIST();            
            string wherestmt = string.Empty;

            //[2021.03.29] [ASWR_27613_20210325142254] ProductName으로 조회하는 경우 MES에서 조회하여 
            string wherePartList = "and 1=1 "; //기본값으로 쿼리에 영향을 미치지 않도록 항상 True인 조건.
            string productname = Request["productname"];
            if(productname != null && productname.Trim()!=""){
                AWDBModels oAwDbModels = new AWDBModels();
                wherePartList = oAwDbModels.getWhereFIlterWithPartList(productname);
            }

            //Search가 있을 경우
            if (grid.IsSearch)
            {
                wherestmt = " where ACTIVE <> ' '  ";

                for (int i = 0; i < grid.Where.rules.Length; i++)
                {                
                    if ((grid.Where.rules[i].field == "FILECATEGORY" || grid.Where.rules[i].field == "ACTIVE") && grid.Where.rules[i].data == "ALL")                        
                    {                      
                    }                        
                    else                        
                    {                          
                        wherestmt += " " + grid.Where.groupOp + " " + grid.Where.rules[i].ConditionStmt();                       
                    }
                }
            }
            else
            {                
                wherestmt = " where active ='T' ";
            }
            //[2021.03.29] [ASWR_27613_20210325142254] wherePartList  를 추가.
            wherestmt += wherePartList + " order by " + grid.SortColumn + " " + grid.SortOrder;


            // 페이징 변수 세팅
            IList<GPSDOCUMENTLIST> GPS_LIST = new List<GPSDOCUMENTLIST>();
            string Total_GPSdocumnet = string.Empty;
            GPS_LIST = oDocument.List_Data(wherestmt, grid.PageIndex, grid.PageSize);
            Total_GPSdocumnet = oDocument.GetTotalCount(wherestmt);

            int totalcnt = int.Parse(Total_GPSdocumnet);
            int pageIndex = Convert.ToInt32(grid.PageIndex);
            int pageSize = grid.PageSize;    //  defalut 3
            int totalRecords = totalcnt;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);


            // 데이터 조회(페이징&정렬)
            var jsonData = new
            {
                total = totalPages,
                page = grid.PageIndex,
                records = totalRecords,
                rows = (
                  from oGPS in GPS_LIST
                  select new
                  {
                      i = oGPS.FILEID,
                      cell = new string[] {
                        oGPS.FILEID,
                        oGPS._SUPPLIERCODE, 
                        oGPS._SUPPLIERNAME, 
                        oGPS._MATERIALNAME,
                        oGPS._PARTNUM,
                        oGPS._FILENAME,                  
                        oGPS._FILECATEGORY, 
                        oGPS._ISSUEDATE,
                        oGPS._EXPIREDATE,
                        oGPS._ACTIVE
                      }
                  }).ToArray()
            };
            return Json(jsonData);
        }
        public ActionResult GetGPSDocumentToUpdate(GridSettings grid)
        {
            // 데이터베이스 연결
            GPSDOCUMENTLIST oDocument = new GPSDOCUMENTLIST();
            string Target_Date = DateTime.Now.AddMonths(2).ToString("yyyy-MM-dd");

            string wherestmt = string.Empty;

            //Search가 있을 경우
            if (grid.IsSearch)
            {


                wherestmt = " where ACTIVE = 'T' and EXPIREDATE < '" + Target_Date +"'";

                for (int i = 0; i < grid.Where.rules.Length; i++)
                {
                    if ((grid.Where.rules[i].field == "FILECATEGORY" || grid.Where.rules[i].field == "ACTIVE") && grid.Where.rules[i].data == "ALL")
                    {
                    }
                    else
                    {
                        wherestmt += " " + grid.Where.groupOp + " " + grid.Where.rules[i].ConditionStmt();
                    }
                }
                wherestmt += " order by " + grid.SortColumn + " " + grid.SortOrder;

            }
            else
            {
                wherestmt = " where active ='T'  and EXPIREDATE < '" + Target_Date +"'" + " order by " + grid.SortColumn + " " + grid.SortOrder;
            }


            // 페이징 변수 세팅
            IList<GPSDOCUMENTLIST> GPS_LIST = new List<GPSDOCUMENTLIST>();
            string Total_GPSdocumnet = string.Empty;
            GPS_LIST = oDocument.List_Data(wherestmt, grid.PageIndex, grid.PageSize);
            Total_GPSdocumnet = oDocument.GetTotalCount(wherestmt);

            int totalcnt = int.Parse(Total_GPSdocumnet);
            int pageIndex = Convert.ToInt32(grid.PageIndex);
            int pageSize = grid.PageSize;    //  defalut 3
            int totalRecords = totalcnt;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);


            // 데이터 조회(페이징&정렬)
            var jsonData = new
            {
                total = totalPages,
                page = grid.PageIndex,
                records = totalRecords,
                rows = (
                  from oGPS in GPS_LIST
                  select new
                  {
                      i = oGPS.FILEID,
                      cell = new string[] {
                        oGPS.FILEID,
                        oGPS._SUPPLIERCODE, 
                        oGPS._SUPPLIERNAME, 
                        oGPS._MATERIALNAME,
                        oGPS._PARTNUM,
                        oGPS._FILENAME,                  
                        oGPS._FILECATEGORY, 
                        oGPS._ISSUEDATE,
                        oGPS._EXPIREDATE,
                        oGPS._ACTIVE
                      }
                  }).ToArray()
            };
            return Json(jsonData);
        }
        public ActionResult GetGPSDocumentByMaterial(string id , string subid)
        {
            // 데이터베이스 연결
            GPSDOCUMENTLIST oDocument = new GPSDOCUMENTLIST();

            string wherestmt = string.Empty;
            wherestmt = " where active ='T' and Suppliercode ='" + id + "'";        


            // 페이징 변수 세팅
            IList<GPSDOCUMENTLIST> GPS_LIST = new List<GPSDOCUMENTLIST>();

            GPS_LIST = oDocument.List_Data_by_material(wherestmt);
            int Total_GPSdocumnet = GPS_LIST.Count;

            int totalcnt = Total_GPSdocumnet;
            int pageSize = 10;    //  defalut 3
            int totalRecords = totalcnt;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);


            // 데이터 조회(페이징&정렬)
            var jsonData = new
            {
                total = totalPages,
                page = 1,
                records = totalRecords,
                rows = (
                  from oGPS in GPS_LIST
                  select new
                  {
                      i = oGPS.FILEID,
                      cell = new string[] {
                        oGPS.FILEID,
                        oGPS._SUPPLIERCODE, 
                        oGPS._SUPPLIERNAME, 
                        oGPS._MATERIALNAME,
                        oGPS._PARTNUM,
                        oGPS._FILENAME,                  
                        oGPS._FILECATEGORY, 
                        oGPS._ISSUEDATE,
                        oGPS._EXPIREDATE,
                        oGPS._ACTIVE
                      }
                  }).ToArray()
            };
            return Json(jsonData);
        }

        public ActionResult GetGPSUsers(GridSettings grid)
        {
            // 데이터베이스 연결
            GPSUSER gps_user = new GPSUSER();
            // 페이징 변수 세팅
            IList<GPSUSER> gps_user_list = new List<GPSUSER>();
            string Total_Users = string.Empty;
            gps_user_list = gps_user.Get_Users();
            Total_Users = gps_user_list.Count.ToString();

            int totalcnt = int.Parse(Total_Users);
            int pageIndex = Convert.ToInt32(grid.PageIndex);
            int pageSize = grid.PageSize;    //  defalut 3
            int totalRecords = totalcnt;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);


            // 데이터 조회(페이징&정렬)
            var jsonData = new
            {
                total = totalPages,
                page = grid.PageIndex,
                records = totalRecords,
                rows = (
                  from user in gps_user_list
                  select new
                  {
                      i = user._EMP_NO,
                      cell = new string[] {
                        user._EMP_NO,
                        user._KNAME, 
                        user._AUTHORITY

                      }
                  }).ToArray()
            };
            return Json(jsonData);
        }

        public ActionResult Update_user(FormCollection formCollection ,GPSUSER user)
        {
            var operation = formCollection["oper"];

            EntityMapper omapper = new EntityMapper();
            List<string> Sqls = new List<string>();
            
            omapper.oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            if (operation.Equals("add"))
            {
               Sqls.Add(omapper.Create(user, "GPSUSER"));                        
            }
            else if (operation.Equals("edit"))
            {
                string wherestmt = " where emp_no ='"+user._EMP_NO+"'";
                Sqls.Add(omapper.Save(user, "GPSUSER",wherestmt)); 
            }
            else if (operation.Equals("del"))
            {
                string wherestmt = " where emp_no ='"+user._EMP_NO+"'";
                Sqls.Add(omapper.Delete("GPSUSER", wherestmt));
            }

            string[] sqls = Sqls.ToArray();
            omapper.oDB.ExcuteNonQuery(sqls);
            return Content("success");
        }
        public ActionResult Update_supplier(FormCollection formCollection, GPSUPPLIER user)
        {
            var operation = formCollection["oper"];

            EntityMapper omapper = new EntityMapper();
            List<string> Sqls = new List<string>();

            omapper.oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            if (operation.Equals("add"))
            {
                Sqls.Add(omapper.Create(user, "GPSUPPLIER"));
            }
            else if (operation.Equals("edit"))
            {
                string update_material = "update GPSUPPLIER set DISABLEREASON ='" + user.DISABLEREASON + "' , ACTIVE ='" + user.ACTIVE + "' ";
                update_material += " where SUPPLIERCODE ='" + user.SUPPLIERCODE + "' ";
                Sqls.Add(update_material);
            }
            else if (operation.Equals("del"))
            {
                string wherestmt = " where suppliercode ='" + user.SUPPLIERCODE + "'";
                Sqls.Add(omapper.Delete("GPCONTACTPERSON", wherestmt));
            }

            string[] sqls = Sqls.ToArray();
            omapper.oDB.ExcuteNonQuery(sqls);
            return Content("success");
        }
        public ActionResult Update_contact(FormCollection formCollection, GPCONTACTPERSON user, string old_mail)
        {
            var operation = formCollection["oper"];

            EntityMapper omapper = new EntityMapper();
            List<string> Sqls = new List<string>();

            omapper.oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            if (operation.Equals("add"))
            {
                Sqls.Add(omapper.Create(user, "GPCONTACTPERSON"));
 
            }
            else if (operation.Equals("edit"))
            {
                string wherestmt = " where suppliercode ='" + user._SUPPLIERCODE + "' and MAILADDRESS ='" + old_mail + "'";
                Sqls.Add(omapper.Save(user, "GPCONTACTPERSON", wherestmt));
            }
            else if (operation.Equals("del"))
            {
                string wherestmt = " where suppliercode ='" + user._SUPPLIERCODE + "' and MAILADDRESS ='" + old_mail + "'";
                Sqls.Add(omapper.Delete("GPCONTACTPERSON", wherestmt));
            }

            string[] sqls = Sqls.ToArray();
            omapper.oDB.ExcuteNonQuery(sqls);
            return Content("success");
        }
        public ActionResult Update_material(FormCollection formCollection, GPMATERIAL material)
        {
            var operation = formCollection["oper"];

            EntityMapper omapper = new EntityMapper();
            List<string> Sqls = new List<string>();

            omapper.oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            if (operation.Equals("add"))
            {
                string partnum = "to_char(sysdate, 'yyMMdd')||'_" + material._SUPPLIERCODE.Substring(5,5) + "_'||GPS_SEQ.NEXTVAL";
                material.PARTNUM = partnum;
                Sqls.Add(omapper.Create(material, "GPMATERIAL"));
            }
            else if (operation.Equals("edit"))
            {
                string update_material = "update GPMATERIAL set DISABLEREASON ='" + material._DISABLEREASON + "' , ACTIVE ='" + material._ACTIVE + "' ";
                update_material += " where PARTNUM ='" + material.PARTNUM + "' and SUPPLIERCODE ='" + material._SUPPLIERCODE + "' ";
                Sqls.Add(update_material);
            }
            else if (operation.Equals("del"))
            {
                string wherestmt = " where PARTNUM ='" + material.PARTNUM + "' and SUPPLIERCODE ='" + material._SUPPLIERCODE + "' ";
                Sqls.Add(omapper.Delete("GPSUSER", wherestmt));
            }

            string[] sqls = Sqls.ToArray();
            omapper.oDB.ExcuteNonQuery(sqls);
            return Content("success");
        }

        public ActionResult Active_supplier()
        {
            string selected_supplier = Request["selected_supplier"];

            if (selected_supplier != "")
            {

                string update_query = "update GPSUPPLIER set active='T'  where SUPPLIERCODE in (";
                update_query += selected_supplier;
                update_query += ")";
                clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
                oDB.ExcuteNonQuery(update_query);

                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }
        public ActionResult Inactive_supplier()
        {
            string selected_supplier = Request["selected_supplier"];
            string disablereason = Request["disablereason"];

            if (selected_supplier != "")
            {

                string update_query = "update GPSUPPLIER set ACTIVE='F', DISABLEREASON ='" + disablereason + "'  where SUPPLIERCODE in (";
                update_query += selected_supplier;
                update_query += ")";
                clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
                oDB.ExcuteNonQuery(update_query);

                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        public ActionResult Active_material()
        {
            string suppliercode = Request["supplier_code"];
            string selected_material = Request["selected_material"];

            if (selected_material != "")
            {

                string update_query = "update GPMATERIAL set active='T' where SUPPLIERCODE ='" + suppliercode + "' and PARTNUM in (";
                update_query += selected_material;
                update_query += ")";
                clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
                oDB.ExcuteNonQuery(update_query);

                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }
        public ActionResult Inactive_material()
        {
            string suppliercode = Request["supplier_code"];
            string selected_material = Request["selected_material"];
            string disablereason = Request["disablereason"];

            if (selected_material != "")
            {
                string update_query = "update GPMATERIAL set active='F', DISABLEREASON ='" + disablereason + "' WHERE SUPPLIERCODE ='" + suppliercode + "' and PARTNUM in (";
                update_query += selected_material;
                update_query += ")";
                clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
                oDB.ExcuteNonQuery(update_query);

                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }



    }
}
