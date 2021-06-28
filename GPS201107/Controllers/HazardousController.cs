using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPS201107.Models;
using GPS201107.Models.Grid;
using ASEWCFServiceLibrary.App_Code;
using GPS201107.Models.HazardousRequest;
using System.Data;
using System.IO;
using System.Reflection;
using System.Data.OleDb;
//using GPS201107.App_Comm;

namespace GPS201107.Controllers
{
    public class HazardousController : Controller
    {
        public const string m_ControlName = "Hazardous";
        public ActionResult Index()
        {
            return View();
        }

        //request시 ViewHazardousMaterial를 초기화 한다.
        public void InitViewHazardousMaterial(ViewHazardousMaterial viewModel)
        {
            //헤더 
            viewModel.gpshmrequest = new GpsHmRequest();
            viewModel.gpshmrequest._CREATEUSER = User.Identity.Name;
            viewModel.gpshmrequest._STATUS = "Open";
            viewModel.gpshmrequest._REQUESTUSERID = User.Identity.Name;

            // 요청자 사번, 요청자 이름, 요청자 이메일 자동생성                
            DataTable dtUserInfo = GPSUSER.Get_UserInfo(User.Identity.Name);
            viewModel.gpshmrequest._REQUESTUSERNAME = dtUserInfo.Rows[0]["k_nm"].ToString();
            viewModel.gpshmrequest._REQUESTUSEREMAIL = dtUserInfo.Rows[0]["e_mail"].ToString();

            //File Info
            viewModel.lstFileOther = new List<GpsHmFile>();
            viewModel.lstFileOther.Add(new GpsHmFile());

            viewModel.lstFileBom = new List<GpsHmFile>();
            viewModel.lstFileBom.Add(new GpsHmFile());

            viewModel.lstFileCustomer = new List<GpsHmFile>();
            viewModel.lstFileCustomer.Add(new GpsHmFile());

            viewModel.lstFIleBd = new List<GpsHmFile>();
            viewModel.lstFIleBd.Add(new GpsHmFile());

            viewModel.lstFilePod = new List<GpsHmFile>();
            viewModel.lstFilePod.Add(new GpsHmFile());

            viewModel.lstFileComponent = new List<GpsHmFile>();
            viewModel.lstFileComponent.Add(new GpsHmFile());

            viewModel.lstFileHandOutOnlyView = new List<GpsHmFile>();
            viewModel.lstFileHandOutOnlyView.Add(new GpsHmFile());
            viewModel.lstFileHandOutNew = new List<GpsHmFile>();
            viewModel.lstFileHandOutNew.Add(new GpsHmFile());

            //text 정보
            viewModel.lstItemDieThickness = new List<GPSHmRequestItem>();
            viewModel.lstItemDieThickness.Add(new GPSHmRequestItem());

            viewModel.lstItemPcbThickness = new List<GPSHmRequestItem>();
            viewModel.lstItemPcbThickness.Add(new GPSHmRequestItem());

            viewModel.lstItemPkgThickness = new List<GPSHmRequestItem>();
            viewModel.lstItemPkgThickness.Add(new GPSHmRequestItem());

            viewModel.lstItemShieldPart = new List<GPSHmRequestItem>();
            viewModel.lstItemShieldPart.Add(new GPSHmRequestItem());

            viewModel.lstItemBallPart = new List<GPSHmRequestItem>();
            viewModel.lstItemBallPart.Add(new GPSHmRequestItem());

            viewModel.lstItemBumpDie = new List<GPSHmRequestItem>();
            viewModel.lstItemBumpDie.Add(new GPSHmRequestItem());

        }

        //1. 등록 및 수정 화면을 표시한다
        [Authorize]
        [ValidateInput(false)]
        public ActionResult ViewHazardousMaterial(ViewHazardousMaterial viewModel)
        {
            string id = null;
            if (Request["HMREQID"] != null)
            {//수정화면
                id = Request["HMREQID"].ToString();
                viewModel.editMode = clsConst.EditMode.Modify.ToString();
            }
            else
            {// 등록 화면                
                viewModel.editMode = clsConst.EditMode.Request.ToString();
            }

            viewModel.authority = GPSUSER.GetUserAuthority(User.Identity.Name);
            //Drop Down List 설정
            viewModel.setCustomerList();// 2. 고객사 리스트 띄우기 ( awinvcustomer)
            viewModel.setAdminList();// 2. 고객사 리스트 띄우기 ( awinvcustomer)
            viewModel.setHazadousMaterialType();            

            if (viewModel.editMode == clsConst.EditMode.Request.ToString()) // 신규 등록하는 경우.
            {
                InitViewHazardousMaterial(viewModel);
            }
            else  //Modify 하는 경우
            {
                //GpsHmrequest Table 항목 불러오기
                viewModel.setGpsHmRequest(id);
                //3. Gpshmfile Table
                viewModel.setFileData(id);
                //4. GpshmRequestItem Table
                viewModel.setRequestItemData(id);
                viewModel.setProductList(viewModel.gpshmrequest._CUSTOMER);
                viewModel.leadtime = GetHazardusMaterialTypeDescription(viewModel.gpshmrequest._HAZARDOUSMATERIALTYPE);
            }
            return View(viewModel);
        }

        //2. List 화면
        [Authorize]
        public ActionResult ViewRequestList(GridSettings grid)
        {
            GpsHmRequestList hmrequest = new GpsHmRequestList();
            List<GpsHmRequestList> list_result = new List<GpsHmRequestList>();
            string TotalQuery = "";
            string wherestmt = string.Empty;

            if (grid.IsSearch)
            {
                wherestmt = " where STATUS <> ' ' ";
                for (int i = 0; i < grid.Where.rules.Length; i++)
                {
                    if (grid.Where.rules[i].field == "STATUS" && grid.Where.rules[i].data == "ALL")
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
                wherestmt += " where STATUS <> 'Delete' ";
            }

            if (grid.SortColumn != null && grid.SortColumn != "")
                wherestmt += " order by " + grid.SortColumn + " " + grid.SortOrder;

            //jqgrid grid 타입의 json 생성

            TotalQuery = hmrequest.GetTotalCount(wherestmt);
            list_result = hmrequest.List_Data(wherestmt, grid.PageIndex, grid.PageSize);

            int totalcnt = int.Parse(TotalQuery);
            int pageIndex = Convert.ToInt32(grid.PageIndex);
            int pageSize = grid.PageSize;
            int totalRecords = totalcnt;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = grid.PageIndex,
                records = totalRecords,
                rows = (

                from GPShm in list_result
                select new
                {
                    i = GPShm._HMREQID,
                    cell = new string[] {
                    GPShm._HMREQID,
                    GPShm._REQUESTDATE,
                    GPShm._CUSTOMER,
                    GPShm._REQUESTUSERNAME,
                    GPShm._HAZARDOUSMATERIALTYPE,
                    GPShm._REQUESTCOMMENT,
                    GPShm._LEADTIME,
                    GPShm._EXPECTEDFINISHDATE,
                    GPShm._STATUS,
                    GPShm._ADMINUSERNAME,
                    GPShm._NO
                    
                    }
                }).ToArray()
            };
            return Json(jsonData);
        }


        // 유해물질 문서 등록 및 수정하는 화면
        [ValidateInput(false)]
        public ActionResult SaveViewHazardousMaterial(ViewHazardousMaterial viewModel, string editMode)
        {
            viewModel.authority = GPSUSER.GetUserAuthority(User.Identity.Name);
            //SetAdminuserInfo(viewModel); //AdminUserID값이 있으면 name고 Mail을 저장한다.
            if (editMode == clsConst.EditMode.Request.ToString())
            {
                string sConnectionStr = clsConst.DBPROVIDER_STRING[(int)clsConst.DBPROVIDER.SCM];
                clsDBControl dbControl = new clsDBControl(clsConst.DBPROVIDER.SCM);
                viewModel.gpshmrequest._HMREQID = dbControl.QuerySingleData(" SELECT 'HAZ'|| TO_CHAR(SYSDATE, 'yyyymmdd')||TO_CHAR( SEQ_COMMON3.nextval, '000')  from dual");
                viewModel.gpshmrequest._HMREQID = viewModel.gpshmrequest._HMREQID.Replace(" ", "");
                CreateRequest(viewModel);
            }
            else if (editMode == clsConst.EditMode.Modify.ToString())
            {

                ModifyRequest(viewModel);
            }

            return RedirectToAction("ViewHazardousMaterialDetail", m_ControlName, new { id = viewModel.gpshmrequest._HMREQID });
        }

        //AdminUserID가 존재하면 이름과 이메일 정보를 저장하다.
        private void SetAdminuserInfo(ViewHazardousMaterial viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.gpshmrequest._ADMINUSERID) == false)
            {                
                string sql = string.Empty;
                clsDBControl dbControl = null;
                try
                {
                    dbControl = new clsDBControl(clsConst.DBPROVIDER.SCM);
                    sql = "";
                    sql += " select * from gpsuser  ";
                    sql += " where emp_no= '" + viewModel.gpshmrequest._ADMINUSERID + "'";
                    DataTable dt =  dbControl.QueryDataTable(sql);
                    viewModel.gpshmrequest._ADMINUSERNAME = dt.Rows[0]["KNAME"].ToString();
                    viewModel.gpshmrequest._ADMINUSEREMAIL = dt.Rows[0]["EMAIL"].ToString();
                }
                catch (Exception e)
                {                    
                    throw e;
                }
            }
        }

        //[2021.06.21] 신규 신청 처리 로직. .GPSHMRequest, GpsHmFile, GpsHmItem, Web_MailSendHistory 저장.
        private void CreateRequest(ViewHazardousMaterial viewModel)
        {
            try
            {
                string sConnectionStr = clsConst.DBPROVIDER_STRING[(int)clsConst.DBPROVIDER.SCM];
                using (OleDbConnection con = new OleDbConnection(sConnectionStr))
                {
                    con.Open();
                    OleDbTransaction tran = con.BeginTransaction();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = con;
                    cmd.Transaction = tran;
                    try
                    {
                        viewModel.gpshmrequest.CreateGpsHmRequest(cmd, viewModel);
                        UpdateFiles(cmd, viewModel);
                        UpdateItems(cmd, viewModel);
                        SendMail(cmd, viewModel);

                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        throw e;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private void SendMail(OleDbCommand cmd, ViewHazardousMaterial oViewHazardousMaterial)
        {
            string subject = "[유해물질자료요청][" + oViewHazardousMaterial.gpshmrequest._HMREQID + "] ";

            if (oViewHazardousMaterial.editMode == clsConst.EditMode.Request.ToString())
            {
                subject += " 등록되었습니다.";
            }
            else
            {
                subject += "의 정보가 변경되었습니다..[현재상태 : " + oViewHazardousMaterial.gpshmrequest._STATUS + "] ";
            }

            string toMail = oViewHazardousMaterial.getAdminUserMailList();
            GpsMail gpsMail = new GpsMail(oViewHazardousMaterial.gpshmrequest, toMail, oViewHazardousMaterial.gpshmrequest._REQUESTUSEREMAIL);
            WEB_MAILSENDHISTORY oMailHistory = gpsMail.MakeWebMailSendHistory(subject);

            oMailHistory.SaveMailData(cmd);



        }

        private void ModifyRequest(ViewHazardousMaterial viewModel)
        {
            // 요청자는 문서 상태가 Open 일 경우에만 수정/삭제 가능. 관리자는 모든 권한 가짐.
            string sConnectionStr = clsConst.DBPROVIDER_STRING[(int)clsConst.DBPROVIDER.SCM];
            using (OleDbConnection con = new OleDbConnection(sConnectionStr))
            {
                con.Open();
                OleDbTransaction tran = con.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.Transaction = tran;
                try
                {
                    viewModel.gpshmrequest._MODIFYUSER = User.Identity.Name;
                    //GpshmRequest Table 정보 수정
                    viewModel.gpshmrequest.Update(cmd, viewModel.gpshmrequest);
                    UpdateFiles(cmd, viewModel);
                    UpdateItems(cmd, viewModel);
                    SendMail(cmd, viewModel);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    //App_Comm.LogHelper.WriteErrorLog("ExecuteResult: " + ex.ToString());
                    string sMsg = ex.Message;
                    CommonModels.MessageBoxShow(HttpContext, sMsg, "/gps/menu/search");
                }
                finally
                {
                    con.Close();
                }
            }

        }

        //그리드에서 더블클릭한 경우 -> 상세화면으로 이동
        [Authorize]
        [ValidateInput(false)]
        public ActionResult ViewHazardousMaterialDetail(string id)
        {
            try
            {
                ViewHazardousMaterial viewModel = new ViewHazardousMaterial();
                viewModel.authority = GPSUSER.GetUserAuthority(User.Identity.Name);
                viewModel.setGpsHmRequest(id);
                viewModel.setFileData(id);
                viewModel.setRequestItemData(id);                
                viewModel.leadtime = GetHazardusMaterialTypeDescription(viewModel.gpshmrequest._HAZARDOUSMATERIALTYPE);
                //수정 권한 부여하기
                if (viewModel.authority.Contains("Admin") || viewModel.gpshmrequest._STATUS == "Open" && viewModel.authority == "User")
                {
                    viewModel.editMode = clsConst.EditMode.Modify.ToString();
                }
                else
                {
                    viewModel.editMode = "";
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                //App_Comm.LogHelper.WriteErrorLog("ExecuteResult: " + ex.ToString());
                string sMsg = ex.Message;
                CommonModels.MessageBoxShow(HttpContext, sMsg, "/gps/Menu/HazardousMaterialReportList");
                return View();
            }
        }

        //Detail 화면에서 파일 다운로드
        public ActionResult DownloadFile(string fileName, string physicalfilename, string physicalfilelocation)
        {
            bool bResult = false;
            try
            {
                string sServerFilePath = Server.MapPath(physicalfilelocation + "/" + physicalfilename);
                if (System.IO.File.Exists(Server.MapPath(physicalfilelocation + "/" + physicalfilename)) == true)
                {
                    System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=\"{0}\"", Server.UrlEncode(fileName)));//한글 깨짐 방지 Server.UrlEncode 사용
                    response.TransmitFile(sServerFilePath);
                    response.Flush();
                    response.End();
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'> alert('Error. Not exist file on server . ');window.history.back();</script>");
                }
            }
            catch (Exception ex)
            {
                return Content("<script language='javascript' type='text/javascript'> alert('Error." + ex.Message + "');window.history.back();</script>");
            }

            return Json(bResult);

        }

        #region Update File Request / Modify 둘다 사용.
        //[2021.06.21] 파일정보를 저장
        private void UpdateFileData(OleDbCommand cmd, string reqID, string relateivePath, string drectioryPath, GpsHmFile file, string fileType)
        {
            if (file._DELETECHECK == "DELETED") // 삭제체크된경우 그냥 삭제하고 아무것도 저장안함.
            {
                file.Delete(cmd);
            }
            else if (file._FILE_CONTAINER != null && file._FILE_CONTAINER[0] != null)
            {
                var uploadFile = file._FILE_CONTAINER[0];
                file._HMREQID = reqID;
                file._FILENAME = System.IO.Path.GetFileName(uploadFile.FileName);
                file._PHYSICALFILENAME = file._FILENAME;
                file._PHYSICALFILELOCATION = relateivePath;
                file._FILETYPE = fileType;
                //서버저장.
                uploadFile.SaveAs(System.IO.Path.Combine(drectioryPath, file._FILENAME));
                file.Delete(cmd);
                file.Save(cmd);
            }
        }

        /// <summary>
        /// [2021.06.21] FileType 6개(HandOut 제외) 에 대한 업데이트: Insert , Delete
        /// 삭제 체크된 경우 삭제 처리.
        /// 삭제 체크 되지 않고 Container가 Null이 아니면 Insert 처리 및 서버 파일 업로드.( 업로드된 파일은 삭제하지 않는다. )
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="viewModel"></param>
        private void UpdateFiles(OleDbCommand cmd, ViewHazardousMaterial viewModel)
        {
            string sRootFolder = " ~/HazardousMaterialFiles";
            string year = viewModel.gpshmrequest._HMREQID.Substring(3, 4);
            string relativePath = sRootFolder + "/" + year + "/" + viewModel.gpshmrequest._HMREQID;
            string directoryPath = System.Web.HttpContext.Current.Server.MapPath(relativePath);
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(directoryPath);

            if (di.Exists == false)
            {
                di.Create();
            }
            // Delete 파일.
            string reqid = viewModel.gpshmrequest._HMREQID;
            UpdateFileData(cmd, reqid, relativePath, directoryPath, viewModel.lstFileOther[0], clsConst.GpsHmFileType.otherfile.ToString());
            UpdateFileData(cmd, reqid, relativePath, directoryPath, viewModel.lstFileBom[0], clsConst.GpsHmFileType.bomfile.ToString());
            UpdateFileData(cmd, reqid, relativePath, directoryPath, viewModel.lstFileCustomer[0], clsConst.GpsHmFileType.customerfile.ToString());
            UpdateFileData(cmd, reqid, relativePath, directoryPath, viewModel.lstFIleBd[0], clsConst.GpsHmFileType.bdfile.ToString());
            UpdateFileData(cmd, reqid, relativePath, directoryPath, viewModel.lstFilePod[0], clsConst.GpsHmFileType.podfile.ToString());
            UpdateFileData(cmd, reqid, relativePath, directoryPath, viewModel.lstFileComponent[0], clsConst.GpsHmFileType.componentspartfile.ToString());

            if (viewModel.authority == "Admin")
            {
                for (int i = 0; i < viewModel.lstFileHandOutOnlyView.Count; i++)
                {
                    //Container 가 null 이므로 삭제만 수행됨
                    UpdateFileData(cmd, reqid, relativePath, directoryPath, viewModel.lstFileHandOutOnlyView[i], clsConst.GpsHmFileType.handoutfile.ToString());
                }
                for (int i = 0; i < viewModel.lstFileHandOutNew.Count; i++)
                {
                    UpdateFileData(cmd, reqid, relativePath, directoryPath, viewModel.lstFileHandOutNew[i], clsConst.GpsHmFileType.handoutfile.ToString());
                }
            }
        }
        #endregion

        #region Update Item Request/Modify 둘다 사용
        /// <summary>
        /// [2021.06.21] GpsRequestItem에 GetValue함수를 통해서 ItemType에 맞는 ItemValue에 조합하여 저장하고 저장싱 _ItemValue를 저장한다.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="oGpsRequestItem"></param>
        /// <param name="reqid"></param>
        /// <param name="ItemType"></param>
        private void UpdateItem(OleDbCommand cmd, List<GPSHmRequestItem> oGpsRequestItem, string reqid, string ItemType)
        {

            for (int i = 0; i < oGpsRequestItem.Count; i++)
            {
                oGpsRequestItem[i]._HMREQID = reqid;
                oGpsRequestItem[i]._ITEMNAME = ItemType;
                //Item Type이 먼저 설정되어야 GetItemValue가 정상 동작을 한다.
                string Itemvalue = oGpsRequestItem[i].GetItemValue();

                oGpsRequestItem[i]._SEQ = (i + 1).ToString();
                oGpsRequestItem[i]._CREATEUSER = User.Identity.Name;

                if (string.IsNullOrEmpty(Itemvalue) == false)
                {
                    oGpsRequestItem[i]._ITEMVALUE = Itemvalue.Replace("'", "''");
                    oGpsRequestItem[i].Save(cmd);
                }
            }
        }

        /// <summary>
        /// [2021.06.21] 저장하기위해서 GPSHmrquestItem 의 데이타를 삭제한후 개별 ItemName을 입력한다.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="viewModel"></param>
        private void UpdateItems(OleDbCommand cmd, ViewHazardousMaterial viewModel)
        {

            string reqID = viewModel.gpshmrequest._HMREQID;
            cmd.CommandText = " Delete  Gpshmrequestitem  WHERE  HMREQID   = '" + viewModel.gpshmrequest._HMREQID + "' ";
            cmd.ExecuteNonQuery();

            UpdateItem(cmd, viewModel.lstItemDieThickness, reqID, clsConst.GpshmrequestItems.diethickness.ToString());
            UpdateItem(cmd, viewModel.lstItemPcbThickness, reqID, clsConst.GpshmrequestItems.pcbthickness.ToString());
            UpdateItem(cmd, viewModel.lstItemPkgThickness, reqID, clsConst.GpshmrequestItems.pkgthickness.ToString());
            UpdateItem(cmd, viewModel.lstItemShieldPart, reqID, clsConst.GpshmrequestItems.shieldpart.ToString());
            UpdateItem(cmd, viewModel.lstItemBallPart, reqID, clsConst.GpshmrequestItems.ballpart.ToString());
            UpdateItem(cmd, viewModel.lstItemBumpDie, reqID, clsConst.GpshmrequestItems.bumpdie.ToString());
        }

        #endregion

        #region

        //VIew 화면에서 materialType을 선택하때 Ajax로 호출되때만 사용
        public string GetHazardusMaterialTypeDescription(string itemname)
        {
            clsDBControl oDB = null;
            string description = "";

            try
            {
                oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);

                DataTable oDT = oDB.QueryDataTable("select description FROM GPSCATEGORYINFO WHERE itemname = '" + itemname + "' ");
                oDB.Close();

                if (oDT.Rows.Count > 0)
                {
                    description = oDT.Rows[0]["DESCRIPTION"].ToString();
                }

                return description;
            }
            catch (Exception e)
            {
                throw new Exception("GPS DB 조회 중 오류 발생", e);
            }
        }

        // ViewHazardousMasterial 에서 Ajax로 호출.
        public ActionResult GetProductName(string customer)
        {
            List<SelectListItem> list_result = new List<SelectListItem>();
            //공백 제거
            customer = customer.Replace(" ", "");

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.MES);
            string sql = "";
            DataTable dtProduct = null;

            try
            {
                sql += " SELECT DISTINCT PRODUCTNAME FROM  AWPDDPRODUCT  ";
                sql += " WHERE activeflag='T'  ";
                sql += " AND customername = '" + customer + "' ";
                sql += " AND productfamily = 'Assembly' ORDER BY PRODUCTNAME ";
                dtProduct = oDB.QueryDataTable(sql);

                //첫번째공백
                SelectListItem oAwPddProduct = new SelectListItem();
                oAwPddProduct.Value = oAwPddProduct.Text = "";
                list_result.Add(oAwPddProduct);
                for (int i = 0; i < dtProduct.Rows.Count; i++)
                {
                    oAwPddProduct = new SelectListItem();
                    oAwPddProduct.Value = oAwPddProduct.Text = dtProduct.Rows[i]["PRODUCTNAME"].ToString();
                    list_result.Add(oAwPddProduct);
                }
                return Json(list_result);
            }
            catch (Exception e)
            {
                throw new Exception("Product DB 조회 중 오류 발생 " + e);
            }
        }

        // AdminUser 선택시 Name과 Email 정보를 가져와 표시한다.
        // gpsUser 테이블 말고 ASE Front의 User_list에서 조회
        public ActionResult GetAdminUserInfo(string empno)
        {
            //공백 제거            
            string[] result = null;
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            string sql = "";
            DataTable dtProduct = null;

            try
            {
                sql += " SELECT emp_no, k_nm, e_mail FROM  User_list  ";
                sql += " WHERE emp_no = '" + empno.Trim() + "'";

                dtProduct = oDB.QueryDataTable(sql);
                result = new string[2];

                for (int i = 0; i < dtProduct.Rows.Count; i++)
                {
                    result[0] = dtProduct.Rows[i]["k_nm"].ToString();
                    result[1] = dtProduct.Rows[i]["e_mail"].ToString();
                }
                return Json(result);
            }
            catch (Exception e)
            {
                throw new Exception("Product DB 조회 중 오류 발생 " + e);
            }
        }
        #endregion
        
        // Detail화면에서 삭제 버튼 클릭하면 Status가 Delete 상태로 update
        [Authorize]
        public ActionResult updateDeleteStatus(string hmreqid)
        {
            bool bResult = false;
            string sql = string.Empty;
            clsDBControl dbControl = null;
            try
            {
                dbControl = new clsDBControl(clsConst.DBPROVIDER.SCM);
                sql = "";
                sql += " UPDATE GPSHMREQUEST SET ";
                sql += " STATUS = 'Delete' ";
                sql += " WHERE HMREQID = '" + hmreqid + "'";
                dbControl.ExcuteNonQuery(sql);
                bResult = true;
            }
            catch (Exception e)
            {
                bResult = false;
                throw e;
            }
            return Json(bResult);
        }
    }
}



