using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASEWCFServiceLibrary.App_Code;
using System.Data;

namespace GPS201107.Models.HazardousRequest
{
    public class GpsHmRequestList
    {
        public string _HMREQID { get; set; } //요청 id
        public string _REQUESTDATE { get; set; } //요청 날짜
        public string _CUSTOMER { get; set; } //고객사
        public string _REQUESTUSERNAME { get; set; } // 로그인한 사용자 정보
        public string _HAZARDOUSMATERIALTYPE { get; set; } // 유해물질항목
        public string _REQUESTCOMMENT { get; set; } // 설명
        public string _LEADTIME { get; set; } // LEAD TIME Description
        public string _EXPECTEDFINISHDATE { get; set; }
        public string _STATUS { get; set; } //상태
        public string _ADMINUSERNAME { get; set; } //담당자 이름
        public string _NO { get; set; } //담당자 이름

        public GpsHmRequestList()
        {
        }


        public int CompareTo(GpsHmRequestList c1)
        {
            return this._HMREQID.CompareTo(c1._HMREQID);
        }


        public object Copy()
        {
            return base.MemberwiseClone();
        }


        public List<GpsHmRequestList> List_Data(string WhereStmt, int page, int numofdata)
        {
            List<GpsHmRequestList> resultList = new List<GpsHmRequestList>();
            GpsHmRequest main = new GpsHmRequest();

            clsDBControl oDBCon = new clsDBControl(clsConst.DBPROVIDER.SCM); //test server      
            string sQuery = string.Empty;

            sQuery += "	SELECT header.HMREQID, ";
            sQuery += "	  header.REQUESTDATE,	";
            sQuery += "	  header.CUSTOMER,	";
            sQuery += "	  header.REQUESTUSERNAME,	";
            sQuery += "	  header.HAZARDOUSMATERIALTYPE,	";
            sQuery += "	  header.REQUESTCOMMENT,	";
            sQuery += "	  info.DESCRIPTION AS LEADTIME,	";
            sQuery += "	  CASE WHEN header.EXPECTEDFINISHDATE IS NULL THEN '계산중' ELSE header.EXPECTEDFINISHDATE END AS EXPECTEDFINISHDATE,	";
            sQuery += "	  header.STATUS,	";
            sQuery += "	  header.ADMINUSERNAME,	";
            sQuery += "	  ROWNUM AS NO	";
            sQuery += "	FROM GPSHMREQUEST header	";
            sQuery += "	INNER JOIN GPSCATEGORYinfo info	";
            sQuery += "	ON header.HAZARDOUSMATERIALTYPE = info.ITEMNAME	";
          

            if (!WhereStmt.Contains("header.STATUS"))
            {
                sQuery += WhereStmt;
            }

            DataSet oDS = oDBCon.QueryDataSet(sQuery);
            DataTable oDT = oDS.Tables[0];

            oDBCon.Close();

            if (oDS != null && oDS.Tables.Count > 0)
            {
                for (int i = 0; i < oDS.Tables[0].Rows.Count; i++)
                {
                    GpsHmRequestList _gpsHmRequest = new GpsHmRequestList();

                    _gpsHmRequest._HMREQID = oDS.Tables[0].Rows[i]["HMREQID"].ToString();
                    _gpsHmRequest._REQUESTDATE = oDS.Tables[0].Rows[i]["REQUESTDATE"].ToString();
                    _gpsHmRequest._CUSTOMER = oDS.Tables[0].Rows[i]["CUSTOMER"].ToString();
                    _gpsHmRequest._REQUESTUSERNAME = oDS.Tables[0].Rows[i]["REQUESTUSERNAME"].ToString();
                    _gpsHmRequest._HAZARDOUSMATERIALTYPE = oDS.Tables[0].Rows[i]["HAZARDOUSMATERIALTYPE"].ToString();
                    _gpsHmRequest._REQUESTCOMMENT = oDS.Tables[0].Rows[i]["REQUESTCOMMENT"].ToString();
                    _gpsHmRequest._LEADTIME = oDS.Tables[0].Rows[i]["LEADTIME"].ToString();
                    _gpsHmRequest._EXPECTEDFINISHDATE = oDS.Tables[0].Rows[i]["EXPECTEDFINISHDATE"].ToString();
                    _gpsHmRequest._STATUS = oDS.Tables[0].Rows[i]["STATUS"].ToString();
                    _gpsHmRequest._ADMINUSERNAME = oDS.Tables[0].Rows[i]["ADMINUSERNAME"].ToString();
                    _gpsHmRequest._NO = oDS.Tables[0].Rows[i]["NO"].ToString();

                    resultList.Add(_gpsHmRequest);
                }

            }

            return resultList;
        }

        public string GetTotalCount(string sQuery)
        {
            clsDBControl oDBCon = new clsDBControl(clsConst.DBPROVIDER.SCM); //test server            


            string sSql = "select count(hmreqid) from GPSHMREQUEST";
            sSql += sQuery;
            string TotalCount = oDBCon.QuerySingleData(sSql);
            return TotalCount;
        }

    }

}
