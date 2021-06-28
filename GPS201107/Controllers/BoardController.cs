using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ASEWCFServiceLibrary.App_Code;
using GPS201107.Models;

namespace GPS201107.Controllers
{
    public class BoardController : Controller
    {

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
        //
        // GET: /Board/
        public ActionResult GetNotice()
        {
            IList<GPBOARD> resultList = new List<GPBOARD>();
            GPBOARD main = new GPBOARD();

            EntityMapper omapper = new EntityMapper();

            omapper.oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            omapper.Table_entity.Add("WEBBOARD", main);
            omapper.WhereCondition = " where af_check = '1' order by af_number desc ";
            omapper.Load(1, 3);
            for (int i = 0; i < omapper.Result[0].Count; i++)
            {
                resultList.Add((GPBOARD)omapper.Result[0][i]);
            }

            return View(resultList);
        }
        public ActionResult Index(string id, string page)
        {
            if (Session["Authority"] == null)
            {
                Session["Authority"] = GetAuthority(User.Identity.Name);
            }

            //menulist
            if ((id == "") || (id == null))
            {
                id = "NULL";
            }
            if ((page == "") || (page == null))
            {
                page = "1";
            }

            ViewData["id"] = id;

            //MenulistModels oMenulist = new MenulistModels();
            //MenuModels oMenu = oMenulist.GetMenuBySubmenu(boardname);
            //ViewData["menu"] = oMenu;

            BoardModels oBoard = new BoardModels(id);
            ViewData["paging"] = oBoard.GetPaging(Convert.ToInt32(page));

            List<Board> olist = oBoard.Getlist3(Convert.ToInt32(page));
            ViewData["list"] = olist;


            //[2021.03.11] GPSUser에 등록된 Admin, User 권한인 경우 권한부여 
            string Gps_User_Role = GetAuthority(User.Identity.Name); //입력한 id로 GPSUSER Table의 Authority 컬럼 조회

            //  string GPS_USER_Role = Session["Authority"].ToString();
            if (Gps_User_Role == "Admin" || Gps_User_Role == "User")
            {
                ViewData["AUTH"] = "T";
            }
            else 
            {
                ViewData["AUTH"] = "F";
            }

            return View();

        }
        public ActionResult Index_Search(string boardname, string page, string category, string keyword)
        {

            if ((boardname == "") || (boardname == null))
            {
                boardname = "NULL";
            }
            if ((page == "") || (page == null))
            {
                page = "1";
            }

            ViewData["id"] = boardname;
            ViewData["category"] = category;
            ViewData["keyword"] = keyword;
            ViewData["page"] = page;


            //MenulistModels oMenulist = new MenulistModels();
            //MenuModels oMenu = oMenulist.GetMenuBySubmenu(boardname);
            //ViewData["menu"] = oMenu;

            BoardModels oBoard = new BoardModels(boardname);

            if (category == "1") category = "AF_name";
            if (category == "2") category = "AF_title";
            if (category == "4") category = "AF_comment";

            ViewData["paging"] = oBoard.GetPaging_Search(Convert.ToInt32(page), category, keyword);


            List<Board> olist = oBoard.Getlist_Search(Convert.ToInt32(page), category, keyword);
            ViewData["list"] = olist;

            if (User.Identity.IsAuthenticated == true)
            {
                ViewData["AUTH"] = "T";
            }
            else
            {
                ViewData["AUTH"] = "F";
            }

            return View();
        }

        public ActionResult Readboard(string boardname, string listnumber)
        {
            //menulist
            if ((boardname == "") || (boardname == null))
            {
                boardname = "NULL";
            }
            if ((listnumber == "") || (listnumber == null))
            {
                listnumber = "1";
            }

            if (Session["Authority"] == null)
            {
                Session["Authority"] = GetAuthority(User.Identity.Name);
            }
            ViewData["id"] = boardname;

            //MenulistModels oMenulist = new MenulistModels();
            //MenuModels oMenu = oMenulist.GetMenuBySubmenu(boardname);
            //ViewData["menu"] = oMenu;

            //본문관련
            Board oBoard = new Board(boardname, Convert.ToInt32(listnumber));
            //read count update ++1
            oBoard.IncreaseReadCount();
            ViewData["board"] = oBoard;

            //하위메뉴관련
            ViewData["boardname"] = boardname;
            ViewData["listnumber"] = Convert.ToInt32(listnumber);
            BoardModels oBoardmodel = new BoardModels(boardname);
            ViewData["preview"] = oBoardmodel.GetPreBoardnumber(Convert.ToInt32(listnumber));
            ViewData["nextview"] = oBoardmodel.GetNextBoardnumber(Convert.ToInt32(listnumber));
            ViewData["currentpage"] = oBoardmodel.GetCurrentPagenumber(Convert.ToInt32(listnumber));

            
            //[2021.03.11] GPSUser에 등록된 Admin, User 권한인 경우 권한부여 
            string Gps_User_Role = GetAuthority(User.Identity.Name); // 입력한 id로 GPSUSER Table의 Authority 컬럼 조회

            if (Gps_User_Role == "Admin" || Gps_User_Role == "User")
            {
                ViewData["AUTH"] = "T";
            }
            else 
            {
                ViewData["AUTH"] = "F";
            }

           

            return View();
        }
        public ActionResult Readboard_popup(string boardname, string listnumber)
        {
            //menulist
            if ((boardname == "") || (boardname == null))
            {
                boardname = "NULL";
            }
            if ((listnumber == "") || (listnumber == null))
            {
                listnumber = "1";
            }

            ViewData["id"] = boardname;

            //MenulistModels oMenulist = new MenulistModels();
            //MenuModels oMenu = oMenulist.GetMenuBySubmenu(boardname);
            //ViewData["menu"] = oMenu;

            //본문관련
            Board oBoard = new Board(boardname, Convert.ToInt32(listnumber));
            //read count update ++1
            oBoard.IncreaseReadCount();
            ViewData["board"] = oBoard;

            //하위메뉴관련
            ViewData["boardname"] = boardname;
            ViewData["listnumber"] = Convert.ToInt32(listnumber);
            BoardModels oBoardmodel = new BoardModels(boardname);
            ViewData["preview"] = oBoardmodel.GetPreBoardnumber(Convert.ToInt32(listnumber));
            ViewData["nextview"] = oBoardmodel.GetNextBoardnumber(Convert.ToInt32(listnumber));
            ViewData["currentpage"] = oBoardmodel.GetCurrentPagenumber(Convert.ToInt32(listnumber));


            //[2021.03.11] GPSUser에 등록된 Admin, User 권한인 경우 권한부여 
            string Gps_User_Role = GetAuthority(User.Identity.Name); // 입력한 id로 GPSUSER Table의 Authority 컬럼 조회

      
            if (Gps_User_Role == "Admin" || Gps_User_Role == "User")
            {
                ViewData["AUTH"] = "T";
            }
            else
            {
                ViewData["AUTH"] = "F";
            }

            return View();
        }
        public ActionResult Readboard_Search(string boardname, string listnumber, string category, string keyword, String page)
        {
            //menulist
            if ((boardname == "") || (boardname == null))
            {
                boardname = "NULL";
            }
            if ((listnumber == "") || (listnumber == null))
            {
                listnumber = "1";
            }

            ViewData["id"] = boardname;
            ViewData["category"] = category;
            ViewData["keyword"] = keyword;
            ViewData["page"] = page;

            if (category == "1") category = "AF_name";
            if (category == "2") category = "AF_title";
            if (category == "4") category = "AF_comment";


            //MenulistModels oMenulist = new MenulistModels();
            //MenuModels oMenu = oMenulist.GetMenuBySubmenu(boardname);
            //ViewData["menu"] = oMenu;

            //본문관련
            Board oBoard = new Board(boardname, Convert.ToInt32(listnumber));
            //read count update ++1
            oBoard.IncreaseReadCount();
            ViewData["board"] = oBoard;

            //하위메뉴관련
            ViewData["boardname"] = boardname;
            ViewData["listnumber"] = Convert.ToInt32(listnumber);
            BoardModels oBoardmodel = new BoardModels(boardname);
            ViewData["preview"] = oBoardmodel.GetPreBoardnumber_Search(Convert.ToInt32(listnumber), category, keyword);
            ViewData["nextview"] = oBoardmodel.GetNextBoardnumber_Search(Convert.ToInt32(listnumber), category, keyword);
            ViewData["currentpage"] = oBoardmodel.GetCurrentPagenumber(Convert.ToInt32(listnumber));


            if (User.Identity.IsAuthenticated == true)
            {
                ViewData["AUTH"] = "T";
            }
            else
            {
                ViewData["AUTH"] = "F";
            }

            return View();
        }

        public ActionResult Writeboard(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "main");
            }

            if (Session["Authority"] == null)
            {
                Session["Authority"] = GetAuthority(User.Identity.Name);
            }

            string AuThority = Session["Authority"].ToString();
            if (AuThority != "User" && AuThority != "Admin")
            {
                return RedirectToAction("menu", "main");
            }


            ViewData["name"] = GPS201107.Models.LogOnDBTxn.GetUserKname(User.Identity.Name);
            ViewData["id"] = id;

            //MenulistModels oMenulist = new MenulistModels();
            //MenuModels oMenu = oMenulist.GetMenuBySubmenu(boardname);
            //ViewData["menu"] = oMenu;           

            return View();

        }
        [ValidateInput(false)]
        public ActionResult WriteboardAction()
        {
            try
            {
                string sBoardname = Request.Form["boardname"];
                Board oBoard = new Board();
                //upload 파일처리
                //var txtFile = Request.Files["bo_w_file"];
                //string sFilename = Path.GetFileName(txtFile.FileName);
                //if (sFilename != "")
                //{
                //    sFilename = oBoard.GenerateFilename(Server.MapPath("~/Upload/EHS/Board"), sFilename);
                //    var path = Path.Combine(Server.MapPath("~/Upload/EHS/Board"), sFilename);
                //    txtFile.SaveAs(path);
                //}

                // TODO: Add insert logic here

                oBoard.sBoardname = sBoardname;
                oBoard.sEmpno = User.Identity.Name;
                oBoard.sIp = Request.UserHostAddress;
                oBoard.iNumber = oBoard.GetMaxINumber() + 1;
                oBoard.iRealnumber = oBoard.GetMaxRealNumber() + 1;
                oBoard.sDate = DateTime.Now.ToString("yyyy/MM/dd-hh:mm:ss");
                oBoard.cCheck = '1';

                // 게시판에 올린 글을 체크하여 게시될 수 있도록 함. 
                // QnA의 경우에는 익명성이 있어 담당자의 확인이 필요하다. 
                if ((sBoardname == "통근버스QnA") || (sBoardname == "HRQnA"))
                {
                    oBoard.cCheck = '2';
                }

                // Post된 값으로 채운다. 
                oBoard.sName = Request.Form["bo_w_name"];
                oBoard.sTitle = Request.Form["bo_w_title"];
               // oBoard.sFilename = sFilename;
                oBoard.sPass = Request.Form["bo_w_password"];
                oBoard.sComment = Request.Form["bo_w_content"];

                oBoard.WriteBoard();

                return RedirectToAction("Index/" + sBoardname);
            }
            catch
            {
                //본문에 태그가 입력되어 있을 경우에..
                //알수 없는 오류로 인해 catch로 들어오기때문에 다음과 같이 처리한다. 
                //2010.09.08

                string sBoardname = Request.Form["boardname"];
                Board oBoard = new Board();
                ////upload 파일처리
                //var txtFile = Request.Files["bo_w_file"];
                //string sFilename = Path.GetFileName(txtFile.FileName);
                //if (sFilename != "")
                //{
                //    sFilename = oBoard.GenerateFilename(Server.MapPath("~/Upload/EHS/Board"), sFilename);
                //    var path = Path.Combine(Server.MapPath("~/Upload/EHS/Board"), sFilename);
                //    txtFile.SaveAs(path);
                //}

                // TODO: Add insert logic here

                oBoard.sBoardname = sBoardname;
                oBoard.sEmpno = User.Identity.Name;
                oBoard.sIp = Request.UserHostAddress;
                oBoard.iNumber = oBoard.GetMaxINumber() + 1;
                oBoard.iRealnumber = oBoard.GetMaxRealNumber() + 1;
                oBoard.sDate = DateTime.Now.ToString("yyyy/MM/dd-hh:mm:ss");
                oBoard.cCheck = '1';

                // 게시판에 올린 글을 체크하여 게시될 수 있도록 함. 
                // QnA의 경우에는 익명성이 있어 담당자의 확인이 필요하다. 
                if ((sBoardname == "통근버스QnA") || (sBoardname == "HRQnA"))
                {
                    oBoard.cCheck = '2';
                }

                // Post된 값으로 채운다. 
                oBoard.sName = Request.Form["bo_w_name"];
                oBoard.sTitle = Request.Form["bo_w_title"];
              //  oBoard.sFilename = sFilename;
                oBoard.sPass = Request.Form["bo_w_password"];
                oBoard.sComment = Request.Form["bo_w_content"];

                oBoard.WriteBoard();

                return RedirectToAction("Index/" + sBoardname);
            }
        }

        
        [ValidateInput(false)]
        public ActionResult ReplyboardAction()
        {
            try
            {
                string sBoardname = Request.Form["boardname"];
                string sListnumber = Request.Form["listnumber"];
                Board oBoard = new Board();
                
                oBoard.sBoardname = sBoardname;
                oBoard.sEmpno = User.Identity.Name;
                oBoard.sIp = Request.UserHostAddress;
                oBoard.iNumber = oBoard.GetMaxINumber() + 1;
                oBoard.iRenumber = Convert.ToInt32(sListnumber); //답글 대상 iNumber
                oBoard.sDate = DateTime.Now.ToString("yyyy/MM/dd-hh:mm:ss");
                oBoard.cCheck = '1';

                // Post된 값으로 채운다. 
                oBoard.sName = Request.Form["bo_w_name"];
                oBoard.sTitle = Request.Form["bo_w_title"];
               // oBoard.sFilename = sFilename;
                oBoard.sPass = Request.Form["bo_w_password"];
                oBoard.sComment = Request.Form["bo_w_content"];

                oBoard.ReplyBoard();

                return RedirectToAction("Index/" + sBoardname);
            }
            catch
            {
                //본문에 태그가 입력되어 있을 경우에..
                //알수 없는 오류로 인해 catch로 들어오기때문에 다음과 같이 처리한다. 
                //2010.09.08

                string sBoardname = Request.Form["boardname"];
                string sListnumber = Request.Form["listnumber"];
                Board oBoard = new Board();              

                oBoard.sBoardname = sBoardname;
                oBoard.sEmpno = User.Identity.Name;
                oBoard.sIp = Request.UserHostAddress;
                oBoard.iNumber = oBoard.GetMaxINumber() + 1;
                oBoard.iRenumber = Convert.ToInt32(sListnumber); //답글 대상 iNumber
                oBoard.sDate = DateTime.Now.ToString("yyyy/MM/dd-hh:mm:ss");
                oBoard.cCheck = '1';

                // Post된 값으로 채운다. 
                oBoard.sName = Request.Form["bo_w_name"];
                oBoard.sTitle = Request.Form["bo_w_title"];
               // oBoard.sFilename = sFilename;
                oBoard.sPass = Request.Form["bo_w_password"];
                oBoard.sComment = Request.Form["bo_w_content"];

                oBoard.ReplyBoard();

                return RedirectToAction("Index/" + sBoardname);
            }
        }

        public ActionResult Modifyboard(string boardname, string listnumber)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "main");
            }

            if (Session["Authority"] == null)
            {
                Session["Authority"] = GetAuthority(User.Identity.Name);
            }

            string AuThority = Session["Authority"].ToString();
            if (AuThority != "User" && AuThority != "Admin")
            {
                return RedirectToAction("menu", "main");
            }

            ViewData["id"] = boardname;

            //MenulistModels oMenulist = new MenulistModels();
            //MenuModels oMenu = oMenulist.GetMenuBySubmenu(boardname);
            //ViewData["menu"] = oMenu;

            ViewData["listnumber"] = listnumber;

            // 이전 값을 보여줘야 한다. 
            Board oBoard = new Board(boardname, Convert.ToInt32(listnumber));
            ViewData["parentboard"] = oBoard;

            return View();
        }
        [ValidateInput(false)]
        public ActionResult ModifyboardAction()
        {
            try
            {
                string sBoardname = Request.Form["boardname"];
                string sListnumber = Request.Form["listnumber"];
                Board oBoard = new Board(sBoardname, Convert.ToInt32(sListnumber));
                //upload 파일처리
                //var txtFile = Request.Files["bo_w_file"];
                //string sFilename = Path.GetFileName(txtFile.FileName);
                //if (sFilename != "")
                //{
                //    sFilename = oBoard.GenerateFilename(Server.MapPath("~/Upload/EHS/Board"), sFilename);
                //    var path = Path.Combine(Server.MapPath("~/Upload/EHS/Board"), sFilename);
                //    txtFile.SaveAs(path);
                //}

                // TODO: Add insert logic here                
                oBoard.sDate = DateTime.Now.ToString("yyyy/MM/dd-hh:mm:ss");
                oBoard.cCheck = '1';

                // Post된 값으로 채운다. 
                oBoard.sName = Request.Form["bo_w_name"];
                oBoard.sTitle = Request.Form["bo_w_title"];
             //   oBoard.sFilename = sFilename;
                oBoard.sPass = Request.Form["bo_w_password"];
                oBoard.sComment = Request.Form["bo_w_content"];
                //content = content.Replace("<script", "[script").Replace("</script>","[/script]");

                oBoard.ModifyBoard();

                return RedirectToAction("readboard/" + sBoardname + "/" + sListnumber);
            }
            catch
            {
                //본문에 태그가 입력되어 있을 경우에..
                //알수 없는 오류로 인해 catch로 들어오기때문에 다음과 같이 처리한다. 
                //2010.09.08

                string sBoardname = Request.Form["boardname"];
                string sListnumber = Request.Form["listnumber"];
                Board oBoard = new Board(sBoardname, Convert.ToInt32(sListnumber));
                //upload 파일처리
                //var txtFile = Request.Files["bo_w_file"];
                //string sFilename = Path.GetFileName(txtFile.FileName);
                //if (sFilename != "")
                //{
                //    sFilename = oBoard.GenerateFilename(Server.MapPath("~/Upload/EHS/Board"), sFilename);
                //    var path = Path.Combine(Server.MapPath("~/Upload/EHS/Board"), sFilename);
                //    txtFile.SaveAs(path);
                //}

                // TODO: Add insert logic here                
                oBoard.sDate = DateTime.Now.ToString("yyyy/MM/dd-hh:mm:ss");
                oBoard.cCheck = '1';

                // Post된 값으로 채운다. 
                oBoard.sName = Request.Form["bo_w_name"];
                oBoard.sTitle = Request.Form["bo_w_title"];
               // oBoard.sFilename = sFilename;
                oBoard.sPass = Request.Form["bo_w_password"];
                oBoard.sComment = Request.Form["bo_w_content"];
                //content = content.Replace("<script", "[script").Replace("</script>","[/script]");

                oBoard.ModifyBoard();

                return RedirectToAction("readboard/" + sBoardname + "/" + sListnumber);
            }
        }

        [Authorize]
        public ActionResult Deleteboard(string boardname, string listnumber)
        {


            //delete 처리
            Board oBoard = new Board();
            oBoard.DeleteBoard(boardname, Convert.ToInt32(listnumber));

            return RedirectToAction("Index/" + boardname);
        }
        [Authorize]
        public ActionResult Inactivate(string boardname, string listnumber)
        {


            //delete 처리
            Board oBoard = new Board();
            oBoard.Inactivate(boardname, Convert.ToInt32(listnumber));

            return RedirectToAction("Index/" + boardname);
        }
        [Authorize]
        public ActionResult activate(string boardname, string listnumber)
        {


            //delete 처리
            Board oBoard = new Board();
            oBoard.activate(boardname, Convert.ToInt32(listnumber));

            return RedirectToAction("Index/" + boardname);
        }

        public ActionResult Download(string boardname, string listnumber, string filename)
        {
            string sFilename = filename;
            return new DownloadResult(boardname, listnumber) { VirtualPath = "~/Upload/EHS/Board/" + sFilename, FileDownloadName = sFilename };
        }

        public ActionResult ImageUpload()
        {
            bool isSuccess = false;
            string sError = "";
            string imgPath = "";

            var txtFile = Request.Files[0];

            string sFilename = Path.GetFileName(txtFile.FileName);
            ViewData["FileName"] = sFilename;
            if (sFilename != "")
            {
                var path = Path.Combine(Server.MapPath("~/Content/images/sub"), sFilename);

                FileInfo fileinfo = new FileInfo(path);
                //if (fileinfo.Exists)
                //{
                //    isSuccess = false;
                //    sError = "There is the same named file.\n Change the file name and try again.";                    
                //}
                //else
                //{
                //    txtFile.SaveAs(path);
                //    // 정상처리
                //    isSuccess = true;
                //    sError = "";
                //}
                isSuccess = true;
                Board oBoard = new Board();
                sFilename = oBoard.GenerateFilename(Server.MapPath("~/Content/images/sub"), sFilename);
                path = Path.Combine(Server.MapPath("~/Content/images/sub"), sFilename);
                txtFile.SaveAs(path);

                // 에디터에서 보여질 경로 지정.
                imgPath = String.Format("{0}/{1}", "", sFilename);

                string result = string.Format(" success:{0}, file_url:'{1}', message:'{2}' ", isSuccess.ToString().ToLower(), imgPath, sError);
                ViewData["body"] = "{" + result + "}";

            }


            return View();
        }
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
                    FileName = HttpUtility.UrlEncode(this.FileDownloadName,
                              System.Text.Encoding.UTF8).Replace("+", "%20");
                    context.HttpContext.Response.ClearContent();

                    context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                    //context.HttpContext.Response.ClearHeaders();
                    //context.HttpContext.Response.Write("<script>alert('123456789'); window.location='/Board/Index/공지사항';</script>");
                    //context.HttpContext.Response.ContentType = "application/pdf";
                }
                string filePath = context.HttpContext.Server.MapPath(this.VirtualPath);
                context.HttpContext.Response.TransmitFile(filePath);

            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Hello')</script>");
                //context.HttpContext.Response.Clear();
                //context.HttpContext.Response.Write("<script>alert('"+ex.Message+"')</script>");
                string sMsg = this.FileDownloadName + " 이(가) 서버에 존재하지 않습니다.";
                CommonModels.MessageBoxShow(context, sMsg, "/EHS/board/Readboard/" + sBoardname + "/" + sListnumber);

            }
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
        public static void MessageBoxShow(HttpContextBase context, string Message, string Redirection)
        {
            string sMsg = Message;
            context.Response.ClearHeaders();
            context.Response.Write("<script>alert('" + sMsg + "'); window.location='" + Redirection + "';</script>");
        }
    }

}
