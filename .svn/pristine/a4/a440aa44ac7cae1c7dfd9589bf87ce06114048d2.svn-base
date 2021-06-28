using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lib.Web.Mvc;
using System.IO;
using Lib.Web.Mvc.JQuery.TreeView;
using jQuery.Treeview;
using ASEWCFServiceLibrary.App_Code;
using GPS201107.Models;
using GPS201107.Models.Grid;

namespace GPS201107.Controllers
{
    public class MenuController : Controller
    {
        private string[] register_categories = new string[] {"선택", "Test Report","MSDS","Non-use Letter" };
        
        public string GetAuthority(string id)
        {
            if (id == "")
            {
                return "Guest";
            }

            clsDBControl oDBCon = new clsDBControl(clsConst.DBPROVIDER.SCM); //test server            
            string sSql = "select authority from GPSUSER where emp_no ='" + id + "'";

            string Authrority = oDBCon.QuerySingleData(sSql);

            if (Authrority == "")
            {
                Authrority = "Guest";
            }

            return Authrority;

        }

        #region Page controller
        //
        // GET: /Menu/
 
        [NoCache2]
        [Authorize]// Admin과 User만 사용가능
        public ActionResult ExpiredList()
        {

            if (Session["Authority"] == null)
            {
                Session["Authority"] = GetAuthority(User.Identity.Name);
            }
            string AuThority = Session["Authority"].ToString();

            if (AuThority != "User" && AuThority != "Admin")
            {
                return RedirectToAction("Error_authority", "Return");
            }

            ViewData["Message"] = "ExpiredList";


            return View();
        }
        [NoCache2]
        [Authorize] // Admin과 User만 사용가능
        public ActionResult Register()
        {
            if (Session["Authority"] == null)
            {
                Session["Authority"] = GetAuthority(User.Identity.Name);
            }

            string AuThority = Session["Authority"].ToString();
            if (AuThority != "User" && AuThority != "Admin")
            {
                return RedirectToAction("Error_authority", "Return");
            }
    
            ViewData["Message"] = "Register";


            return View();
        }
        
        [NoCache2]
        [Authorize]// 로그인만 하면 사용가능
        public ActionResult Search()
        {

            ViewData["Message"] = "Search";
            return View();
        }

        [NoCache2]
        [Authorize]// 로그인만 하면 사용가능
        public ActionResult HazardousMaterialReportList()
        {
            ViewData["Message"] = "유해물질 문서 목록";
            return View();
        }


        [NoCache2]
        [Authorize] //Admin과 User만 사용가능
        public ActionResult SupplierInformation()
        {
            if (Session["Authority"] == null)
            {
                Session["Authority"] = GetAuthority(User.Identity.Name);
            }

            string AuThority = Session["Authority"].ToString();
            if (AuThority != "User" && AuThority != "Admin")
            {
                return RedirectToAction("Error_authority", "Return");
            }
            ViewData["Message"] = "SupplierInformation";
            return View();
        }
        [NoCache2]
        [Authorize] //Admin만 사용가능
        public ActionResult SystemManagement()
        {

            if (Session["Authority"] == null)
            {
                Session["Authority"] = GetAuthority(User.Identity.Name);
            }

            string AuThority = Session["Authority"].ToString();
            if (AuThority != "Admin")
            {
                return RedirectToAction("Error_authority", "Return");
            }

            ViewData["Message"] = "SystemManagement";
            return View();
        }
        [NoCache2]
        [Authorize]
        public ActionResult DocumentMangemet()
        {
            ViewData["Message"] = "DocumentMangemet";
            return View();
        }

        #endregion

        #region Data transaction
        [Authorize]
        [HttpPost]
        public ActionResult Register_Action(REGISTERDOCUMENT oGPSDocument)
        {
            
            string selected_device = Request["selected_device"];
            string supplier_name = Request["s_name"];
            string supplier_code = Request["s_code"];
            string supplier_mail = Request["s_mail"];
            string supplier_representative = Request["s_Representative"];
            string update_rows = Request["update_rows"];



            for (int i = 0; i < oGPSDocument.DocumenList.Count; i++)
            {
                oGPSDocument.DocumenList[i]._SUPPLIERNAME = supplier_name;
                oGPSDocument.DocumenList[i]._SUPPLIERCODE = supplier_code;
                oGPSDocument.DocumenList[i]._ACTIVE = "T";
            }


            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/GPSdocument/" + supplier_code));
            if (di.Exists == false)
            {
                di.Create();
                di.CreateSubdirectory("MSDS");
                di.CreateSubdirectory("TEST Report");
                di.CreateSubdirectory("Non-use Letter");
                di.CreateSubdirectory("Warranty letter");
                di.CreateSubdirectory("Conflict Mineral");
            }

            EntityMapper omapper = new EntityMapper();
            FileUploader uploader = new FileUploader();
            List<string> Sqls = new List<string>();
            omapper.oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);

            for (int i = 0; i < oGPSDocument.DocumenList.Count; i++)
            {
                string path = string.Empty;
                string suppliername = oGPSDocument.DocumenList[i]._SUPPLIERNAME;
                string filecategory = oGPSDocument.DocumenList[i]._FILECATEGORY;
                var fileupload = oGPSDocument.DocumenList[i]._FILENAME[0];

                string document_file_pathe = Server.MapPath("~/GPSdocument/" + supplier_code + "/" + filecategory);
                string ofilename = uploader.GenerateFilename(document_file_pathe, Path.GetFileName(fileupload.FileName));
                oGPSDocument.DocumenList[i]._FILENAME = ofilename;
                path = Path.Combine(document_file_pathe, ofilename);
                fileupload.SaveAs(path);
            }


            string file_id = "to_char(sysdate, 'yyyyMMdd')||'_" + supplier_code + "_'||GPS_SEQ.NEXTVAL";

            string[] materialnames_partnum = selected_device.Split('|');

            for (int i = 0; i < materialnames_partnum.Length - 1; i++)
            {
                string material_name = materialnames_partnum[i].Split('^')[0];
                string partnum = materialnames_partnum[i].Split('^')[1];

                for (int j = 0; j < oGPSDocument.DocumenList.Count; j++)
                {
                    oGPSDocument.DocumenList[j].FILEID = file_id;
                    oGPSDocument.DocumenList[j]._MATERIALNAME = material_name;
                    oGPSDocument.DocumenList[j]._PARTNUM = partnum;
                    Sqls.Add(omapper.Create(oGPSDocument.DocumenList[j], "GPSDOCUMENTLIST"));
                }

            }
            string[] sqls = Sqls.ToArray();
            omapper.oDB.ExcuteNonQuery(sqls);

            ViewData["Message"] = "Register";


            return RedirectToAction("Return_Register", "Return");
        }
        [Authorize]
        [HttpPost]
        public ActionResult SupplierInformation_Action(REGISTERDOCUMENT oGPSDocument)
        {
           

            EntityMapper omapper = new EntityMapper();
            FileUploader uploader = new FileUploader();
            List<string> Sqls = new List<string>();
            omapper.oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);

            string supplier_name = Request["s_name"];
            string supplier_code = Request["s_code"];

            string filecategory_name = oGPSDocument.DocumenList[0]._FILECATEGORY;
           
            //oGPSDocument.DocumenList[0]._FILECATEGORY = "Conflict Mineral";
            //oGPSDocument.DocumenList[1]._FILECATEGORY = "Warranty letter";

            
            


            for (int i = 0; i < oGPSDocument.DocumenList.Count; i++)
            {
                oGPSDocument.DocumenList[i]._SUPPLIERNAME = supplier_name;
                oGPSDocument.DocumenList[i]._SUPPLIERCODE = supplier_code;
                oGPSDocument.DocumenList[i]._ACTIVE = "T";
            }


            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/GPSdocument/"+supplier_code));
            if (di.Exists == false)
            {
                di.Create();
                di.CreateSubdirectory("MSDS");
                di.CreateSubdirectory("TEST Report");
                di.CreateSubdirectory("Non-use Letter");
                di.CreateSubdirectory("Warranty letter");
                di.CreateSubdirectory("Conflict Mineral");
            }

  

            string file_id = "to_char(sysdate, 'yyyyMMdd')||'_" + supplier_code + "_'||GPS_SEQ.NEXTVAL";

            for (int i = 0; i < oGPSDocument.DocumenList.Count; i++)
            {
                if (oGPSDocument.DocumenList[i]._FILENAME[0] != null)
                {
                    string path = string.Empty;
                    string suppliername = oGPSDocument.DocumenList[i]._SUPPLIERNAME;
                    string filecategory = oGPSDocument.DocumenList[i]._FILECATEGORY;

                    //파일 카테고리 추가시  아래 소스 사용
                    //DirectoryInfo subDi = new DirectoryInfo(Server.MapPath("~/GPSdocument/" + supplier_code + "/" + filecategory));

                    //if (subDi.Exists == false)
                    //{
                    //    subDi.Create();

                    //}

                    oGPSDocument.DocumenList[i].FILEID = file_id;
                    var fileupload = oGPSDocument.DocumenList[i]._FILENAME[0];

                    string document_file_pathe = Server.MapPath("~/GPSdocument/"+supplier_code+"/"+filecategory);
                    string ofilename = uploader.GenerateFilename(document_file_pathe, Path.GetFileName(fileupload.FileName));
                    oGPSDocument.DocumenList[i]._FILENAME = ofilename;
                    path = Path.Combine(document_file_pathe, ofilename);
                    fileupload.SaveAs(path);
                    Sqls.Add(omapper.Create(oGPSDocument.DocumenList[i], "GPSDOCUMENTLIST"));
                }
            }

            string[] sqls = Sqls.ToArray();
            omapper.oDB.ExcuteNonQuery(sqls);



            return RedirectToAction("Return_SupplierInformation", "Return");

        }
        [Authorize]
        [HttpPost]
        public ActionResult Update_Action(REGISTERDOCUMENT oGPSDocument)
        {
            string new_issueddate = Request["issueddate"];
            string new_expireddate = Request["expireddate"];
            string update_rows = Request["update_rows"];
            string suppliercodes = Request["suppliercodes"];
            string categories = Request["categories"];
            var new_file = Request.Files["uploadfile"];
            string path = string.Empty;
            FileUploader uploader = new FileUploader();
            List<string> Sqls = new List<string>();


            string document_file_path = Server.MapPath("~/GPSdocument/" + suppliercodes + "/" + categories);
            string ofilename = uploader.GenerateFilename(document_file_path, Path.GetFileName(new_file.FileName));

            path = Path.Combine(document_file_path, ofilename);
            new_file.SaveAs(path);

            string update_query1 = "update GPSDOCUMENTLIST set active='F' where fileid in (";
            update_query1 += update_rows;
            update_query1 += ")";

            string update_query2 ="insert ";
                   update_query2 +="into GPSDOCUMENTLIST ";
                   update_query2 +="select to_char(sysdate, 'yyyyMMdd')||'_'||suppliercode||'_'||GPS_SEQ.NEXTVAL,";
                   update_query2 +="  suppliercode,";
                   update_query2 +="  suppliername,";
                   update_query2 +="  filecategory,";
                   update_query2 +="  materialname,";
                   update_query2 +="  partnum,";
                   update_query2 += "  '" + ofilename + "' ,";
                   update_query2 += "  '" + new_issueddate + "' ,";
                   update_query2 += "  '" + new_expireddate + "' ,";
                   update_query2 +="  'T' ";
                   update_query2 += "from GPSDOCUMENTLIST ";
                   update_query2 += "where fileid in ("+update_rows+")";
                   Sqls.Add(update_query1);
                   Sqls.Add(update_query2);

            
                   clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
                   oDB.ExcuteNonQuery(Sqls.ToArray());

                   return RedirectToAction("Return_ExpiredList", "Return");
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete_Action()
        {
            string delete_rows = Request["delete_rows"];
            string update_query = "update GPSDOCUMENTLIST set active='F' where fileid in (";
            update_query += delete_rows;                   
            update_query +=")";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            oDB.ExcuteNonQuery(update_query);
            return RedirectToAction("Return_ExpiredList", "Return");
        }
        #endregion




        #region Actions
        public ActionResult Treeview()
        {
            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FileSystemInfos(string root)
        {
            int? nodeId = (root == "source") ? (int?)null : Convert.ToInt32(root);
            IEnumerable<FileSystemInfo> children = FileSystemInfosRepository.GetFileSystemInfos(nodeId);
            
            List<TreeViewNode> nodes = new List<TreeViewNode>();
            foreach (FileSystemInfo child in children)
            {
                bool leaf = child is FileInfo;
                nodes.Add(new TreeViewNode()
                {
                    id = Convert.ToString(FileSystemInfosRepository.GetNodeId(child)),
                    text = child.Name,
                    classes = leaf ? "file" : "folder",
                    hasChildren = !leaf
                });
            }

            return Json(nodes);
        }
        
        public ActionResult Download(string suppliercode,string category, string filename)
        {

            string logmessage = "~/GPSdocument/" + suppliercode + "/" + category + "/" + filename, FileDownloadName = filename;
            //App_Comm.LogHelper.WriteLog(logmessage);
            return new DownloadResult() { VirtualPath = "~/GPSdocument/" + suppliercode + "/" + category + "/" + filename, FileDownloadName = filename };
        }
    
        public ActionResult DownloadMsdsList(string id)
        {
            string sFilename = id;
            return new DownloadResult() { VirtualPath = "~/GPSdocument/" + sFilename, FileDownloadName = sFilename };
        }
        public class DownloadResult : ActionResult
        {
            string sBoardname { get; set; }
            string sListnumber { get; set; }

            public DownloadResult()
            {
                sBoardname = "";
                sListnumber = "";
            }
            public DownloadResult(string _sBoardname, string _sListnumber)
            {
                sBoardname = _sBoardname;
                sListnumber = _sListnumber;
            }
            public DownloadResult(string virtualPath)
            {
                this.VirtualPath = virtualPath;
            }

            public string VirtualPath
            {
                get;
                set;
            }

            public string FileDownloadName
            {
                get;
                set;
            }

            
            public override void ExecuteResult(ControllerContext context)
            {
                string FileName = "";
                try
                {
                    if (!String.IsNullOrEmpty(FileDownloadName))
                    {
                        //한글이 깨지는 문제로.. 파일 다운로드 전에 UTF8로 바꿔줌.
                        FileName = HttpUtility.UrlEncode(this.FileDownloadName, System.Text.Encoding.UTF8);
                        context.HttpContext.Response.ClearContent();
                        context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);                        
                    }                    
                    string filePath = context.HttpContext.Server.MapPath(this.VirtualPath);

                    //App_Comm.LogHelper.WriteLog("download file path : " + filePath);
                    context.HttpContext.Response.TransmitFile(filePath);

                }
                catch (Exception ex)
                {
                    //App_Comm.LogHelper.WriteErrorLog("ExecuteResult: " + ex.ToString());
                    string sMsg = this.FileDownloadName + " 이(가) 서버에 존재하지 않습니다.";
                    CommonModels.MessageBoxShow(context, sMsg, "/gps/menu/search");

                }
            }
            public class CommonModels
            {
                public static void MessageBoxShow(ControllerContext context, string Message, string Redirection)
                {
                    string sMsg = Message;

                    context.HttpContext.Response.ClearHeaders();
                    context.HttpContext.Response.Write("<script>alert('" + sMsg + "'); window.location='" + Redirection + "';</script>");
                }
            }
        }


        #endregion
    }
}
