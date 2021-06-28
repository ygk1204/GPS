using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Data.OleDb;

namespace ASEWCFServiceLibrary.App_Code
{
    /// <summary>
    /// clsConst의 요약 설명입니다.
    public class clsConst
    {
        public const string SITEADDRESS = "http://asefront.asekr.com:8080";

        public enum DBPROVIDER
        {
            ASEFRONT,       //0
            SCM,             //1
            MES         //1
            
        };
        public static string[] DBPROVIDER_STRING =
        {
            getConnectionStringAseFront(),
            getConnectionStringSCM(),
            getConnectionStringMES()
            
        };

        public enum GpsHmFileType
        {
            otherfile,
            bomfile,
            customerfile,
            bdfile,
            podfile,
            componentspartfile,
            handoutfile
        };

        public enum GpshmrequestItems
        {
            diethickness,
            pcbthickness,
            pkgthickness,
            shieldpart,
            ballpart,
            bumpdie
        };

        public enum EditMode
        {
            Request,
            Modify
        }
        public enum HazardMaterialStatus
        {
            Open,
            Process,
            Close,
            Delete
        }


        public static string sHost = "antispam1.asekr.com";
        public static string sHost2 = "antispam2.asekr.com"; //mail server 2
        
        //public static clsConst()
        //{
        //    //
        //    // TODO: 생성자 논리를 여기에 추가합니다.
        //    //

        //}

        public static string getConnectionStringAseFront()
        {
            
            string dbConnectType =  System.Configuration.ConfigurationManager.AppSettings["DB_CONNECTION"].ToString();
            string connectionString= "";
            if (dbConnectType == "prod")
            {
                connectionString = "Provider=OraOleDb.Oracle;Data Source=amdb;Password=asefront88;User ID=asefront";
            }
            else if (dbConnectType == "test")
            {
                connectionString = "Provider=OraOleDb.Oracle;Data Source=amdb;Password=testweb01;User ID=testweb";
            }else if(dbConnectType=="local"){
                connectionString = "";
            }else {
                connectionString = "";
            }
            return connectionString;
        }

        public static string getConnectionStringSCM()
        {

            string dbConnectType = System.Configuration.ConfigurationManager.AppSettings["DB_CONNECTION"].ToString();
            string connectionString = "";
            if (dbConnectType == "prod")
            {
                connectionString = "Provider=OraOleDb.Oracle;Data Source=amdb;Password=scm01;User ID=SCM";
            }
            else if (dbConnectType == "test")
            {
                connectionString = "Provider=OraOleDb.Oracle;Data Source=amdb;Password=testweb01;User ID=testweb";
            }
            else if (dbConnectType == "local")
            {
                connectionString = "";
            }
            else
            {
                connectionString = "";
            }
            return connectionString;
        }
        public static string getConnectionStringMES()
        {

            string dbConnectType = System.Configuration.ConfigurationManager.AppSettings["DB_CONNECTION"].ToString();
            string connectionString = "";
            if (dbConnectType.ToLower() == "prod")
            {
                connectionString = "Provider=OraOleDb.Oracle;Data Source=afp;Password=awprod;User ID=awprod";
            }
            else if (dbConnectType.ToLower() == "test")
            {
                connectionString = "Provider=OraOleDb.Oracle;Data Source=afp;Password=awprod;User ID=awprod";
            }
            else if (dbConnectType.ToLower() == "local")
            {
                connectionString = "";
            }
            else
            {
                connectionString = "";
            }
            return connectionString;
        }

        
        public static bool IsRunningServer()
        {
            //prod : 운영, test : 테스트, local : 로컬
            string dbConnectType = System.Configuration.ConfigurationManager.AppSettings["DB_CONNECTION"].ToString();
            bool bResult = false;

            if (dbConnectType.ToLower() == "prod")
            {
                bResult = true;
            }

            return bResult;
        }

        public static string getNowDate()
        {
            string sNow = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            return sNow;
        }


        public static string Get_Site_Address()
        {
            string sSiteAddress = "";

            if (IsRunningServer())
            {
                sSiteAddress =
                    "http://asefront.asekr.com/GPS/";
            }
            else
            {
                sSiteAddress =
                    "http://10.50.9.220/GPS/";

            }

            return sSiteAddress;

        }

        public static string FROMMAIL = "HazarodusMaterial@asekr.com";

        public static string HazarodusItemSeparator = "¶¶";

    }
}
