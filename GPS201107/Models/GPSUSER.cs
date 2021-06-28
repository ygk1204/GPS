using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASEWCFServiceLibrary.App_Code;
using System.Data;

namespace GPS201107.Models
{
    public class GPSUSER
    {	
        public string _EMP_NO  {get;set;}
	    public string _KNAME      {get;set;}
	    public string _AUTHORITY   {get;set;}
        public string _EMAIL { get; set; }
    
        public GPSUSER()
        {
        }
        public IList<GPSUSER> Get_Users()
        {
            IList<GPSUSER> list_users = new List<GPSUSER>();

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            string sql = string.Empty;
            sql += "select EMP_NO , KNAME, AUTHORITY, EMAIL from GPSUSER ";
            DataSet oDS = oDB.QueryDataSet(sql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                GPSUSER oUser = new GPSUSER();
                oUser._EMP_NO = oDT.Rows[i]["EMP_NO"].ToString();
                oUser._KNAME = oDT.Rows[i]["KNAME"].ToString();
                oUser._AUTHORITY = oDT.Rows[i]["AUTHORITY"].ToString();
                oUser._EMAIL = oDT.Rows[i]["EMAIL"].ToString();
                list_users.Add(oUser);
            }

            return list_users;

        }

        public List<GPSUSER> Get_Authority(string authority)
        {
            List<GPSUSER> list_users = new List<GPSUSER>();
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            string sql = string.Empty;
            sql += "select  * from GPSUSER where AUTHORITY = '" + authority + "' ";
            
            DataSet oDS = oDB.QueryDataSet(sql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                GPSUSER oUser = new GPSUSER();
                oUser._EMP_NO = oDT.Rows[i]["EMP_NO"].ToString();
                oUser._KNAME = oDT.Rows[i]["KNAME"].ToString();
                oUser._AUTHORITY = oDT.Rows[i]["AUTHORITY"].ToString();
                oUser._EMAIL = oDT.Rows[i]["EMAIL"].ToString();
                list_users.Add(oUser);
            }

            return list_users;

        }


        public static string GetUserID(string emp_no)
        {
            string sReturn = "";

            string sql = "select EMP_NO from GPSUSER where EMP_NO = '" + emp_no + "' ";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sql);
            oDB.Close();

            DataTable oDT = oDS.Tables[0];

            if (oDT.Rows.Count > 0)
            {
                sReturn = oDT.Rows[0]["EMP_NO"].ToString();
            }
            else
            {
                sReturn = "GUEST";
            }

            return sReturn;
        }

        public static string GetUserAuthority(string emp_no)
        {
            string sReturn = "";

            string sql = "select AUTHORITY from GPSUSER where EMP_NO = '" + emp_no + "' ";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sql);
            oDB.Close();

            DataTable oDT = oDS.Tables[0];

            if (oDT.Rows.Count > 0)
            {
                sReturn = oDT.Rows[0]["AUTHORITY"].ToString();
            }
            else
            {
                sReturn = "GUEST";
            }

            return sReturn;
        }



        public static string GetUserKname(string emp_no)
        {
            string sReturn = "";

            string sql = "select KNAME from GPSUSER where EMP_NO = '" + emp_no + "' ";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sql);
            oDB.Close();

            DataTable oDT = oDS.Tables[0];

            if (oDT.Rows.Count > 0)
            {
                sReturn = oDT.Rows[0]["KNAME"].ToString();
            }
            else
            {
                sReturn = "GUEST";
            }

            return sReturn;
        }

        //AseFront에서 user 정보를 가져온다.
        public static DataTable Get_UserInfo(string userid)
        {
            List<GPSUSER> list_users = new List<GPSUSER>();
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            string sql = string.Empty;
            sql += " select  *  from user_list where emp_no= '" + userid + "' ";

            DataTable dtUser = oDB.QueryDataTable(sql);
            oDB.Close();
            return dtUser;
        }

        public object Copy()
        {
            return base.MemberwiseClone();
        }
    }
}