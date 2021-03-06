using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ASEWCFServiceLibrary.App_Code;

namespace GPS201107.Models.HazardousRequest
{
    public class ViewHazardousMaterial
    {

        public GpsHmRequest gpshmrequest { get; set; }//요청할 항목
        public GpsHmFile gpshmfile { get; set; } //파일 정보

        public List<GpsCategoryInfo> lstHazardousMaterialType { get; set; }// 콤보박스에 표시할 정보 
        public List<AWINVCUSTOMER> lstCustomer { get; set; }
        public List<SelectListItem> lstProduct { get; set; }
        public List<SelectListItem> lstStatus { get; set; }
        public List<SelectListItem> lstAdminUser { get; set; }
        //view및 Controller에서 사용하는 설정값들- 중요.
        public string authority { get; set; }
        public string editMode { get; set; } //Request, Modify ViewHazardousMaterial.aspx에서 신청 및 수정 모드를 저장.

        

        //파일 첨부 리스트
        public List<GpsHmFile> lstFileBom { get; set; }
        public List<GpsHmFile> lstFileCustomer { get; set; }
        public List<GpsHmFile> lstFilePod { get; set; }
        public List<GpsHmFile> lstFIleBd { get; set; }
        public List<GpsHmFile> lstFileOther { get; set; }
        public List<GpsHmFile> lstFileComponent { get; set; }
        public List<GpsHmFile> lstFileHandOutOnlyView { get; set; }      //저장된 정보표시        
        public List<GpsHmFile> lstFileHandOutNew { get; set; }   //HandOut 신규 저장시 사용....

        //text 입력 리스트
        public List<GPSHmRequestItem> lstItemDieThickness { get; set; }
        public List<GPSHmRequestItem> lstItemPcbThickness { get; set; }
        public List<GPSHmRequestItem> lstItemPkgThickness { get; set; }
        public List<GPSHmRequestItem> lstItemShieldPart { get; set; }
        public List<GPSHmRequestItem> lstItemBallPart { get; set; }
        public List<GPSHmRequestItem> lstItemBumpDie { get; set; }

        //public List<GPSHmRequestItem> RequestItemLists { get; set; }
        public string leadtime = "";
        public ViewHazardousMaterial()
        {
            gpshmrequest = new GpsHmRequest();
            gpshmfile = new GpsHmFile();

            //고객사 & 제품 리스트
            lstCustomer = new List<AWINVCUSTOMER>();
            lstProduct = new List<SelectListItem>();
            lstAdminUser = new List<SelectListItem>();

            //유해물질 항목 리스트
            lstHazardousMaterialType = new List<GpsCategoryInfo>();

            //파일 첨부 리스트

            lstFileBom = new List<GpsHmFile>();
            lstFileCustomer = new List<GpsHmFile>();
            lstFIleBd = new List<GpsHmFile>();
            lstFileOther = new List<GpsHmFile>();
            lstFilePod = new List<GpsHmFile>();
            lstFileComponent = new List<GpsHmFile>();
            lstFileHandOutOnlyView = new List<GpsHmFile>();
            //lstFileHandOutOnlyView.Add(new GpsHmFile());  //제거 표시
            lstFileHandOutNew = new List<GpsHmFile>();
            lstFileHandOutNew.Add(new GpsHmFile());

            //Item 저장 항목            
            lstItemDieThickness = new List<GPSHmRequestItem>();
            lstItemPcbThickness = new List<GPSHmRequestItem>();
            lstItemPkgThickness = new List<GPSHmRequestItem>();
            lstItemShieldPart = new List<GPSHmRequestItem>();
            lstItemBallPart = new List<GPSHmRequestItem>();
            lstItemBumpDie = new List<GPSHmRequestItem>();

            lstStatus = new List<SelectListItem>();
            lstStatus.Add(new SelectListItem { Text = "Open", Value = "Open" });
            lstStatus.Add(new SelectListItem { Text = "Process", Value = "Process" });
            lstStatus.Add(new SelectListItem { Text = "Close", Value = "Close" });
            lstStatus.Add(new SelectListItem { Text = "Delete", Value = "Delete" });

        }

        // GPSHMRequest 테이블값 조회하여 gpshmrequest값 저장.
        public GpsHmRequest setGpsHmRequest(string hmreqid)
        {
            gpshmrequest = new GpsHmRequest();

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            string sql = string.Empty;
            sql += " select  a.*  ";
            sql += "      , (SELECT description        ";
            sql += "             FROM GPSCATEGORYINFO        ";
            sql += "            WHERE categoryname = 'HazardousMaterialType'        ";
            sql += "              AND itemname = a.HAZARDOUSMATERIALTYPE       ";
            sql += "              ) MATERIALTYPEDESCRIPTION  ";
            sql += " from GpsHmRequest a";
            sql += " where hmreqid = '" + hmreqid + "' ";

            DataSet oDS = oDB.QueryDataSet(sql);
            DataTable oDT = oDS.Tables[0];

            oDB.Close();
            oDT = oDS.Tables[0];

            if (oDT.Rows.Count > 0)
            {
                gpshmrequest._HMREQID = oDT.Rows[0]["HMREQID"].ToString();
                gpshmrequest._REQUESTDATE = oDT.Rows[0]["REQUESTDATE"].ToString();
                gpshmrequest._CUSTOMER = oDT.Rows[0]["CUSTOMER"].ToString();
                gpshmrequest._PRODUCT = oDT.Rows[0]["PRODUCT"].ToString();
                gpshmrequest._REQUESTUSERID = oDT.Rows[0]["REQUESTUSERID"].ToString();
                gpshmrequest._REQUESTUSERNAME = oDT.Rows[0]["REQUESTUSERNAME"].ToString();
                gpshmrequest._REQUESTUSEREMAIL = oDT.Rows[0]["REQUESTUSEREMAIL"].ToString();
                gpshmrequest._HAZARDOUSMATERIALTYPE = oDT.Rows[0]["HAZARDOUSMATERIALTYPE"].ToString();
                gpshmrequest._REQUESTCOMMENT = oDT.Rows[0]["REQUESTCOMMENT"].ToString();
                gpshmrequest._EXPECTEDFINISHDATE = oDT.Rows[0]["EXPECTEDFINISHDATE"].ToString();
                gpshmrequest._STATUS = oDT.Rows[0]["STATUS"].ToString();
                gpshmrequest._ADMINUSERID = oDT.Rows[0]["ADMINUSERID"].ToString();
                gpshmrequest._ADMINUSERNAME = oDT.Rows[0]["ADMINUSERNAME"].ToString();
                gpshmrequest._ADMINUSEREMAIL = oDT.Rows[0]["ADMINUSEREMAIL"].ToString();
                gpshmrequest._HADNOUTURL = oDT.Rows[0]["HADNOUTURL"].ToString();
                gpshmrequest._CREATEDATE = oDT.Rows[0]["CREATEDATE"].ToString();
                gpshmrequest._CREATEUSER = oDT.Rows[0]["CREATEUSER"].ToString();
                gpshmrequest._MODIFYDATE = oDT.Rows[0]["MODIFYDATE"].ToString();
                gpshmrequest._MODIFYUSER = oDT.Rows[0]["MODIFYUSER"].ToString();               
            }
            return gpshmrequest;
        }

        public void setFileData(string id)
        {
            lstFileOther = GetFileList(id, clsConst.GpsHmFileType.otherfile.ToString());
            lstFileBom = GetFileList(id, "bomfile");
            lstFileCustomer = GetFileList(id, "customerfile");
            lstFIleBd = GetFileList(id, "bdfile");
            lstFilePod = GetFileList(id, "podfile");
            lstFileComponent = GetFileList(id, "componentspartfile");
            lstFileHandOutOnlyView = GetFileList(id, "handoutfile");
            lstFileHandOutNew = new List<GpsHmFile>();
            lstFileHandOutNew.Add(new GpsHmFile());
        }

        public void setRequestItemData(string id)
        {
            lstItemDieThickness = GetItemList(id, "diethickness");
            lstItemPcbThickness = GetItemList(id, "pcbthickness");
            lstItemPkgThickness = GetItemList(id, "pkgthickness");
            lstItemShieldPart = GetItemList(id, "shieldpart");
            lstItemBallPart = GetItemList(id, "ballpart");
            lstItemBumpDie = GetItemList(id, "bumpdie");
        }

        //고객사 리스트 띄워주기
        public void setCustomerList()
        {
            clsDBControl oDB = null;
            lstCustomer = new List<AWINVCUSTOMER>();
            string sql = "";

            try
            {
                oDB = new clsDBControl(clsConst.DBPROVIDER.MES);
                sql += " SELECT customername FROM AWINVCUSTOMER WHERE activeflag='T' ORDER BY CUSTOMERNAME";
                DataSet oDS = oDB.QueryDataSet(sql);
                oDB.Close();

                DataTable oDT = oDS.Tables[0];
                AWINVCUSTOMER oAwInVCustomer = new AWINVCUSTOMER();
                oAwInVCustomer._CUSTOMERNAME = "";
                lstCustomer.Add(oAwInVCustomer);

                for (int i = 0; i < oDT.Rows.Count; i++)
                {
                    oAwInVCustomer = new AWINVCUSTOMER();
                    oAwInVCustomer._CUSTOMERNAME = oDT.Rows[i]["CUSTOMERNAME"].ToString();
                    lstCustomer.Add(oAwInVCustomer);
                }
            }
            catch (Exception e)
            {
                throw new Exception("GPS DB 조회 중 오류 발생 " + e);
            }
        }

        // AdminUser 선택을위해서 필요.
        public void setAdminList()
        {
            clsDBControl oDB = null;
            lstAdminUser = new List<SelectListItem>();
            try
            {
                oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
                DataTable oDT = oDB.QueryDataTable(" SELECT emp_no, kname, Email FROM GPSUSER WHERE Authority = 'Admin'");

                for (int i = 0; i < oDT.Rows.Count; i++)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = oDT.Rows[i]["emp_no"].ToString();
                    item.Text = oDT.Rows[i]["kname"].ToString();
                    lstAdminUser.Add(item);
                }
            }
            catch (Exception e)
            {
                throw new Exception("GPS DB 조회 중 오류 발생 " + e);
            }
            finally
            {
                if (oDB != null) oDB.Close();
            }
        }

        //제품명 리스트
        public void setProductList(string customer)
        {
            clsDBControl odbControl = null;
            lstProduct = new List<SelectListItem>();
            string sql = "";

            try
            {
                odbControl = new clsDBControl(clsConst.DBPROVIDER.MES);
                sql += " SELECT DISTINCT PRODUCTNAME FROM  AWPDDPRODUCT  ";
                sql += " WHERE activeflag='T'  ";
                sql += " AND customername = '" + customer + "' ";
                sql += " AND productfamily = 'Assembly' ORDER BY PRODUCTNAME ";
                DataTable dtProduct = odbControl.QueryDataTable(sql);


                SelectListItem oAwPddProduct = new SelectListItem();
                oAwPddProduct.Text = oAwPddProduct.Value = "";
                lstProduct.Add(oAwPddProduct);
                oAwPddProduct = new SelectListItem();
                oAwPddProduct.Text = oAwPddProduct.Value = "ALL";
                lstProduct.Add(oAwPddProduct);

                for (int i = 0; i < dtProduct.Rows.Count; i++)
                {
                    oAwPddProduct = new SelectListItem();
                    oAwPddProduct.Text = oAwPddProduct.Value = dtProduct.Rows[i]["PRODUCTNAME"].ToString();
                    lstProduct.Add(oAwPddProduct);
                }
            }
            catch (Exception e)
            {
                throw new Exception("GPS DB 조회 중 오류 발생 " + e);
            }
            finally
            {
                if (odbControl != null) odbControl.Close();
            }
        }

        /// <summary>
        /// GpsHmReqeustItem에서 ItemName(DieThickness, Bump Die 등)에 따라 값을 가져온다.
        /// _ItemValue의 값을 파싱하여 각 속성에 저장해준다. ( ParsingItemValue 함수)
        /// </summary>
        /// <param name="hmreqid"></param>
        /// <param name="itemname"></param>
        /// <returns></returns>
        public List<GPSHmRequestItem> GetItemList(string hmreqid, string itemname)
        {
            clsDBControl odbControl = null;
            List<GPSHmRequestItem> RequestItemLists = new List<GPSHmRequestItem>();

            try
            {
                string sql = "";
                odbControl = new clsDBControl(clsConst.DBPROVIDER.SCM);
                sql += " select  *  from GPSHMREQUESTITEM ";
                sql += " where HMREQID = '" + hmreqid + "' and ITEMNAME = '" + itemname + "' ";

                DataTable oDT = odbControl.QueryDataTable(sql);

                if (oDT.Rows.Count > 0)
                {
                    for (int i = 0; i < oDT.Rows.Count; i++)
                    {
                        GPSHmRequestItem requestitem = new GPSHmRequestItem();
                        requestitem._HMREQID = oDT.Rows[i]["HMREQID"].ToString();
                        requestitem._ITEMNAME = oDT.Rows[i]["ITEMNAME"].ToString();
                        requestitem._SEQ = oDT.Rows[i]["SEQ"].ToString();
                        requestitem._ITEMVALUE = oDT.Rows[i]["ITEMVALUE"].ToString();
                        requestitem._DESCRIPTION = oDT.Rows[i]["DESCRIPTION"].ToString();
                        requestitem._CREATEDATE = oDT.Rows[i]["CREATEDATE"].ToString();
                        requestitem._CREATEUSER = oDT.Rows[i]["CREATEUSER"].ToString();
                        requestitem._MODIFYDATE = oDT.Rows[i]["MODIFYDATE"].ToString();
                        requestitem._MODIFYUSER = oDT.Rows[i]["MODIFYUSER"].ToString();
                        requestitem.ParsingItemValue();  //[2021.06.22]
                        RequestItemLists.Add(requestitem);
                    }
                }
                else
                {
                    GPSHmRequestItem requestitem = new GPSHmRequestItem();
                    requestitem._ITEMNAME = itemname;
                    requestitem._SEQ = "1";
                    RequestItemLists.Add(requestitem);
                }
                return RequestItemLists;
            }
            catch (Exception e)
            {
                throw new Exception("GPS DB 조회 중 오류 발생", e);
            }
            finally
            {
                if (odbControl != null) odbControl.Close();
            }
        }

        public List<GpsHmFile> GetFileList(string hmreqid, string filetype)
        {
            clsDBControl odbControl = null;
            List<GpsHmFile> GpsHmFileList = new List<GpsHmFile>();
            try
            {
                string sql = "";
                odbControl = new clsDBControl(clsConst.DBPROVIDER.SCM);
                sql += " select * from GPSHMFILE ";
                sql += " where HMREQID = '" + hmreqid + "' and FILESTATUS = 'active' and FILETYPE = '" + filetype + "' ";
                DataTable oDT = odbControl.QueryDataTable(sql);

                if (oDT.Rows.Count > 0)
                {
                    for (int i = 0; i < oDT.Rows.Count; i++)
                    {
                        gpshmfile = new GpsHmFile();
                        gpshmfile._HMREQID = oDT.Rows[i]["HMREQID"].ToString();
                        gpshmfile._FILETYPE = oDT.Rows[i]["FILETYPE"].ToString();
                        gpshmfile._SEQ = oDT.Rows[i]["SEQ"].ToString();
                        gpshmfile._FILENAME = oDT.Rows[i]["FILENAME"].ToString();
                        gpshmfile._PHYSICALFILENAME = oDT.Rows[i]["PHYSICALFILENAME"].ToString();
                        gpshmfile._PHYSICALFILELOCATION = oDT.Rows[i]["PHYSICALFILELOCATION"].ToString();
                        gpshmfile._FILESTATUS = oDT.Rows[i]["FILESTATUS"].ToString();
                        gpshmfile._FILECOMMENT = oDT.Rows[i]["FILECOMMENT"].ToString();
                        gpshmfile._CREATEDATE = oDT.Rows[i]["CREATEDATE"].ToString();
                        gpshmfile._MODIFYDATE = oDT.Rows[i]["MODIFYDATE"].ToString();
                        GpsHmFileList.Add(gpshmfile);
                    }
                }
                else
                {
                    gpshmfile = new GpsHmFile();
                    gpshmfile._FILETYPE = filetype;
                    GpsHmFileList.Add(gpshmfile);
                }
                return GpsHmFileList;
            }
            catch (Exception e)
            {
                throw new Exception("GPS DB 조회 중 오류 발생", e);
            }
            finally
            {
                if (odbControl != null) odbControl.Close();
            }
        }

        // Category 테이블에서 MaterialType을 설정
        public void setHazadousMaterialType()
        {
            clsDBControl oDB = null;
            lstHazardousMaterialType = new List<GpsCategoryInfo>();
            string sql = "";
            try
            {
                oDB = new clsDBControl(clsConst.DBPROVIDER.SCM); //test server
                sql += "  SELECT ITEMNAME, DESCRIPTION  FROM GPSCATEGORYINFO WHERE categoryname='HazardousMaterialType' ";
                DataSet oDS = oDB.QueryDataSet(sql);
                oDB.Close();

                DataTable oDT = oDS.Tables[0];
                GpsCategoryInfo categoryinfo = new GpsCategoryInfo();
                lstHazardousMaterialType.Add(categoryinfo);
                for (int i = 0; i < oDT.Rows.Count; i++)
                {

                    categoryinfo = new GpsCategoryInfo();
                    categoryinfo._ITEMNAME = oDT.Rows[i]["ITEMNAME"].ToString();
                    categoryinfo._DESCRIPTION = oDT.Rows[i]["DESCRIPTION"].ToString();
                    lstHazardousMaterialType.Add(categoryinfo);
                }
            }
            catch (Exception e)
            {
                throw new Exception("GPS DB 조회 중 오류 발생 " + e);
            }
        }
        /// <summary>
        /// 어드민 전체 메일 리스트를 스트링으로 반환.
        /// ddk@asekr.com
        /// </summary>
        /// <returns></returns>
        public string getAdminUserMailList()
        {
            clsDBControl oDB = null;
            string toMailList = "";
            try
            {
                oDB = new clsDBControl(clsConst.DBPROVIDER.SCM); //test server
                DataSet oDS = oDB.QueryDataSet("  SELECT EMAIL FROM GPSUSER WHERE Authority = 'Admin'  ");
                DataTable oDT = oDS.Tables[0];
                for (int i = 0; i < oDT.Rows.Count; i++)
                {
                    toMailList += ConvertMailAddress(oDT.Rows[i]["EMAIL"].ToString()) + ",";
                }
            }
            catch (Exception e)
            {
                throw new Exception("GPS DB 조회 중 오류 발생 " + e);
            }
            finally
            {
                if (oDB != null) oDB.Close();
            }

            return toMailList.TrimEnd(',');
        }

        private string ConvertMailAddress(string email)
        {
            if (!String.IsNullOrWhiteSpace(email) && email.IndexOf('@') <= 0)
            {
                email = email + "@asekr.com";
            }
            return email;
        }

    }
}