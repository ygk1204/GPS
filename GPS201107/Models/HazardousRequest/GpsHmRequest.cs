using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASEWCFServiceLibrary.App_Code;
using System.Data;
using System.Data.OleDb;

namespace GPS201107.Models.HazardousRequest
{
    public class GpsHmRequest
    {
        public string _HMREQID { get; set; } //요청 id
        public string _REQUESTDATE { get; set; } //요청 날짜
        public string _CUSTOMER { get; set; } //고객사
        public string _PRODUCT { get; set; } //제품
        public string _REQUESTUSERID { get; set; } //로그인한 사용자 정보
        public string _REQUESTUSERNAME { get; set; } // 로그인한 사용자 정보
        public string _REQUESTUSEREMAIL { get; set; } //로그인한 사용자 정보
        public string _HAZARDOUSMATERIALTYPE { get; set; } //유해물질 항목
        public string _REQUESTCOMMENT { get; set; } // 설명
        public string _EXPECTEDFINISHDATE { get; set; }
        public string _STATUS { get; set; } //상태
        public string _ADMINUSERID { get; set; } //담당자 정보
        public string _ADMINUSERNAME { get; set; } //담당자 이름
        public string _ADMINUSEREMAIL { get; set; } //담당자 이메일
        public string _HADNOUTURL { get; set; } //파일 경로 직접 입력
        public string _CREATEDATE { get; set; } // 최초 요청날짜
        public string _CREATEUSER { get; set; } //최초 요청 사용자
        public string _MODIFYDATE { get; set; } //요청문서 수정날짜
        public string _MODIFYUSER { get; set; } // 요청문서 수정한 사용자



        public GpsHmRequest()
        {
        }



        public int CompareTo(GpsHmRequest c1)
        {
            return this._HMREQID.CompareTo(c1._HMREQID);
        }


        public object Copy()
        {
            return base.MemberwiseClone();
        }



        //public List<GpsHmRequest> get_Hmrequest_Sequence(string hmreqid)
        //{
        //    List<GpsHmRequest> list_result = new List<GpsHmRequest>();

        //    clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
        //    string sql = string.Empty;

        //    sql += " SELECT SUBSTR(hmreqid, 10, 3) AS SEQ ";
        //    sql += "   FROM gpshmrequest  ";
        //    sql += "  where hmreqid like '%" + hmreqid + "%' ";
        //    sql += "  order by hmreqid desc ";

        //    DataSet oDS = oDB.QueryDataSet(sql);
        //    DataTable oDT = oDS.Tables[0];
        //    oDB.Close();
        //    for (int i = 0; i < oDT.Rows.Count; i++)
        //    {
        //        GpsHmRequest oHmRequest = new GpsHmRequest();
        //        oHmRequest._HMREQID = oDT.Rows[i]["SEQ"].ToString();

        //        list_result.Add(oHmRequest);
        //    }
        //    return list_result;
        //}

        public bool SaveHmrequestData(OleDbCommand cmd, ViewHazardousMaterial viewModel, string _UserRole, string status)
        {
            bool bReturn = false;
            string sQuery = "";
            try
            {
                if (status == "Request")
                {
                    //hmreqid, requestdate, customer, product, requestuserid, requestusername, requestuseremail, HAZARDOUSMATERIALTYPE, requestcomment,
                    // status, createdate, createuser
                    //요청 내용(text)
                    if (viewModel.gpshmrequest._REQUESTCOMMENT != null && viewModel.gpshmrequest._REQUESTCOMMENT.Contains("'") == true)
                    {
                        viewModel.gpshmrequest._REQUESTCOMMENT = viewModel.gpshmrequest._REQUESTCOMMENT.Replace("'", "''");
                    }

                    sQuery += " INSERT INTO GPSHMREQUEST ( HMREQID, REQUESTDATE, CUSTOMER, PRODUCT, REQUESTUSERID, REQUESTUSERNAME, REQUESTUSEREMAIL, ";
                    sQuery += " HAZARDOUSMATERIALTYPE, REQUESTCOMMENT, STATUS, CREATEDATE, CREATEUSER ) ";
                    sQuery += " VALUES  ( '" + viewModel.gpshmrequest._HMREQID + "', '" + viewModel.gpshmrequest._REQUESTDATE + "', '" + viewModel.gpshmrequest._CUSTOMER + "'," + "'";
                    sQuery += viewModel.gpshmrequest._PRODUCT + "', '" + viewModel.gpshmrequest._REQUESTUSERID + "', '" + viewModel.gpshmrequest._REQUESTUSERNAME + "', '" + viewModel.gpshmrequest._REQUESTUSEREMAIL + "'," + "'";
                    sQuery += viewModel.gpshmrequest._HAZARDOUSMATERIALTYPE + "', '" + viewModel.gpshmrequest._REQUESTCOMMENT + "', '" + viewModel.gpshmrequest._STATUS + "', '" + viewModel.gpshmrequest._CREATEDATE + "'," + "'";
                    sQuery += viewModel.gpshmrequest._CREATEUSER + "' " + " ) ";
                    cmd.CommandText = sQuery;
                    cmd.ExecuteNonQuery();
                }
                else if (status == "Modify")
                {
                    if (_UserRole == "Admin")
                    {
                        //조사 자료위치 (text)
                        if (viewModel.gpshmrequest._HADNOUTURL != null && viewModel.gpshmrequest._HADNOUTURL.Contains("'") == true)
                        {
                            viewModel.gpshmrequest._HADNOUTURL = viewModel.gpshmrequest._HADNOUTURL.Replace("'", "''");
                        }

                        sQuery = " update GPSHMREQUEST set EXPECTEDFINISHDATE = '" + viewModel.gpshmrequest._EXPECTEDFINISHDATE + "', STATUS = '" + viewModel.gpshmrequest._STATUS
                        + "', HADNOUTURL = '" + viewModel.gpshmrequest._HADNOUTURL + "', MODIFYDATE = '" + viewModel.gpshmrequest._MODIFYDATE + "', MODIFYUSER = '"
                        + viewModel.gpshmrequest._MODIFYUSER + "', ADMINUSERID = '" + viewModel.gpshmrequest._ADMINUSERID + "', ADMINUSERNAME = '" + viewModel.gpshmrequest._ADMINUSERNAME + "' ";
                        sQuery += " , ADMINUSEREMAIL = '" + viewModel.gpshmrequest._ADMINUSEREMAIL + "' ";
                        sQuery += " where HMREQID = '" + viewModel.gpshmrequest._HMREQID + "' ";

                        cmd.CommandText = sQuery;
                        cmd.ExecuteNonQuery();
                    }
                    else if (_UserRole == "User")
                    {

                        //요청 내용 (text)
                        if (viewModel.gpshmrequest._REQUESTCOMMENT != null && viewModel.gpshmrequest._REQUESTCOMMENT.Contains("'") == true)
                        {
                            viewModel.gpshmrequest._REQUESTCOMMENT = viewModel.gpshmrequest._REQUESTCOMMENT.Replace("'", "''");
                        }

                        sQuery += " update GPSHMREQUEST set CUSTOMER = '" + viewModel.gpshmrequest._CUSTOMER + "', PRODUCT = '" + viewModel.gpshmrequest._PRODUCT
                       + "', REQUESTCOMMENT = '" + viewModel.gpshmrequest._REQUESTCOMMENT + "' ";
                        sQuery += " , MODIFYDATE = '" + viewModel.gpshmrequest._MODIFYDATE + "', MODIFYUSER = '" + viewModel.gpshmrequest._MODIFYUSER + "' ";
                        sQuery += " where HMREQID = '" + viewModel.gpshmrequest._HMREQID + "' ";

                        cmd.CommandText = sQuery;
                        cmd.ExecuteNonQuery();
                    }
                }
                return bReturn;
            }
            catch (Exception e)
            {
                bReturn = false;
                throw e;

            }

        }

        public bool CreateGpsHmRequest(OleDbCommand cmd, ViewHazardousMaterial viewModel)
        {
            string insertSql = GetInsertSql(viewModel);
            
            cmd.CommandText = insertSql;
            cmd.ExecuteNonQuery();

            return false;
        }

        public bool Update(OleDbCommand cmd, GpsHmRequest oGpsHmRequest)
        {
            cmd.CommandText = GetUpdateSql(oGpsHmRequest);
            cmd.ExecuteNonQuery();

            return true;

        }


        private string GetInsertSql(ViewHazardousMaterial viewModel)
        {
            string sql = "";
            string comments = viewModel.gpshmrequest._REQUESTCOMMENT;
            if (viewModel.gpshmrequest._REQUESTCOMMENT != null && viewModel.gpshmrequest._REQUESTCOMMENT.Contains("'") == true)
            {
                comments = viewModel.gpshmrequest._REQUESTCOMMENT.Replace("'", "''");
            }

            sql += "  INSERT INTO GPSHMREQUEST  ";
            sql += "  (  ";
            sql += "      HMREQID, ";
            sql += "      REQUESTDATE, ";
            sql += "      CUSTOMER, ";
            sql += "      PRODUCT, ";
            sql += "      REQUESTUSERID,  ";
            sql += "      REQUESTUSERNAME,  ";
            sql += "      REQUESTUSEREMAIL,	 ";
            sql += "      HAZARDOUSMATERIALTYPE, ";
            sql += "      REQUESTCOMMENT,  ";
            sql += "      STATUS,  ";
            sql += "      CREATEDATE, 			 ";
            sql += "      CREATEUSER  ";
            sql += "  )  ";
            sql += "  VALUES  ";
            sql += "  (       '" + viewModel.gpshmrequest._HMREQID + "'        ,  ";
            sql += "         (SELECT TO_CHAR(SYSDATE , 'yyyy-mm-dd') FROM dual  ) ,  ";
            sql += "         '" + viewModel.gpshmrequest._CUSTOMER + "',  ";
            sql += "         '" + viewModel.gpshmrequest._PRODUCT + "',  ";
            sql += "         '" + viewModel.gpshmrequest._REQUESTUSERID + "',  ";
            sql += "         '" + viewModel.gpshmrequest._REQUESTUSERNAME + "',  ";
            sql += "         '" + viewModel.gpshmrequest._REQUESTUSEREMAIL + "',  ";
            sql += "         '" + viewModel.gpshmrequest._HAZARDOUSMATERIALTYPE + "',  ";
            sql += "         '" + comments + "',  ";
            sql += "         'Open',  ";
            sql += "         (SELECT TO_CHAR(SYSDATE , 'yyyy-mm-dd hh24:mi:ss') FROM dual ), ";
            sql += "         '" + viewModel.gpshmrequest._CREATEUSER + "'   ";
            sql += "                                  )";
            return sql;
        }

        // Admin 변경하는 경우에대한 밥업 수정해야함.
        private string GetUpdateSql(GpsHmRequest oGpsHmrequest)
        {
            //싱글쿼테이션 제거.
            if (oGpsHmrequest._REQUESTCOMMENT != null && oGpsHmrequest._REQUESTCOMMENT.Contains("'") == true)
            {
                oGpsHmrequest._REQUESTCOMMENT = oGpsHmrequest._REQUESTCOMMENT.Replace("'", "''");
            }
            if (oGpsHmrequest._HADNOUTURL != null && oGpsHmrequest._HADNOUTURL.Contains("'") == true)
            {
                oGpsHmrequest._HADNOUTURL = oGpsHmrequest._HADNOUTURL.Replace("'", "''");
            }

            string sql = "";
            sql += "  UPDATE GPSHMREQUEST ";
            sql += "         SET ";
            sql += "         CUSTOMER = '" + oGpsHmrequest._CUSTOMER + "', ";
            sql += "         PRODUCT = '" + oGpsHmrequest._PRODUCT + "', ";
            sql += "         HAZARDOUSMATERIALTYPE = '" + oGpsHmrequest._HAZARDOUSMATERIALTYPE + "', ";
            sql += "         REQUESTCOMMENT = '" + oGpsHmrequest._REQUESTCOMMENT + "', ";
            sql += "         EXPECTEDFINISHDATE = '" + oGpsHmrequest._EXPECTEDFINISHDATE + "', ";
            sql += "         STATUS = '" + oGpsHmrequest._STATUS + "', ";
            sql += "         ADMINUSERID = '" + oGpsHmrequest._ADMINUSERID + "', ";
            sql += "         ADMINUSERNAME = '" + oGpsHmrequest._ADMINUSERNAME + "', ";
            sql += "         ADMINUSEREMAIL = '" + oGpsHmrequest._ADMINUSEREMAIL + "', ";
            sql += "         HADNOUTURL = '" + oGpsHmrequest._HADNOUTURL + "', ";
            sql += "         MODIFYDATE =  to_char(sysdate, 'yyyy-mm-dd hh24:mi:ss') , ";
            sql += "         MODIFYUSER = '" + oGpsHmrequest._MODIFYUSER + "' ";
            sql += "   WHERE hmreqid = '" + oGpsHmrequest._HMREQID + "' ";
            sql += "  ";

            return sql;
        }

        private string getRequestID(OleDbCommand cmd)
        {
            cmd.CommandText = " SELECT 'HAZ'|| TO_CHAR(SYSDATE, 'yyyymmdd')||TO_CHAR( SEQ_COMMON3.nextval, '000')  from dual  ";
            string reqid = cmd.ExecuteScalar().ToString();
            return reqid;
        }

    }
}