using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ASEWCFServiceLibrary.App_Code;

namespace GPS201107.Models
{
    public class USER_LIST
    {
        public string _FL_NM { get; set; }
        public string _EMP_NO { get; set; }
        public string _K_NM { get; set; }
        public string _GRADE { get; set; }
        public string _DEPT_NM { get; set; }
        public string _SUP_EMP_NO { get; set; }
        public string _UPDATE_PASS { get; set; }
        public string _PERSK_NUM { get; set; }
        public string _PQW { get; set; }
        public string _JOB_CD { get; set; }
        public string _F_NM { get; set; }
        public string _L_NM { get; set; }
        public string _TEL_NO { get; set; }
        public string _GRP_CD { get; set; }
        public string _E_MAIL { get; set; }
        public string _E_PASS { get; set; }
        public string _PASS_COUNT { get; set; }
        public string _CARD_NO { get; set; }
        public string _LPO { get; set; }
        public string _LPO_NM { get; set; }
        public string _PERSG { get; set; }
        public string _RETDT { get; set; }
        public string _POSITION { get; set; }
        public string _PERSK { get; set; }
        public string _PERSK_DC { get; set; }
        public string _SACHZ { get; set; }
        public string _SCHKZ { get; set; }
        public string _JOBFAMILY { get; set; }
        public string _DEP_CD { get; set; }
        public string _LPOCHECK { get; set; }


        public USER_LIST()
        {

        }

        public static USER_LIST GetUserList(string empno)
        {
            
            USER_LIST result = new USER_LIST();

            //우리사원인지 확인
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            string sql = string.Empty;

            sql += " select * from USER_LIST  where EMP_NO = '" + empno + "' ";

            DataSet oDS = oDB.QueryDataSet(sql);
            DataTable oDT = oDS.Tables[0];

            oDB.Close();
            oDT = oDS.Tables[0];

                if (oDT.Rows.Count > 0)
                {
                result._FL_NM = oDT.Rows[0]["FL_NM"].ToString();
                result._EMP_NO = oDT.Rows[0]["EMP_NO"].ToString();
                result._K_NM = oDT.Rows[0]["K_NM"].ToString();
                result._GRADE = oDT.Rows[0]["GRADE"].ToString();
                result._DEPT_NM = oDT.Rows[0]["DEPT_NM"].ToString();
                result._SUP_EMP_NO = oDT.Rows[0]["SUP_EMP_NO"].ToString();
                result._UPDATE_PASS = oDT.Rows[0]["UPDATE_PASS"].ToString();
                result._PERSK_NUM = oDT.Rows[0]["PERSK_NUM"].ToString();
                result._PQW = oDT.Rows[0]["PQW"].ToString();
                result._JOB_CD = oDT.Rows[0]["JOB_CD"].ToString();
                result._F_NM = oDT.Rows[0]["F_NM"].ToString();
                result._L_NM = oDT.Rows[0]["L_NM"].ToString();
                result._TEL_NO = oDT.Rows[0]["TEL_NO"].ToString();
                result._GRP_CD = oDT.Rows[0]["GRP_CD"].ToString();
                result._E_MAIL = oDT.Rows[0]["E_MAIL"].ToString();
                result._E_PASS = oDT.Rows[0]["E_PASS"].ToString();
                result._PASS_COUNT = oDT.Rows[0]["PASS_COUNT"].ToString();
                result._CARD_NO = oDT.Rows[0]["CARD_NO"].ToString();
                result._LPO = oDT.Rows[0]["LPO"].ToString();
                result._LPO_NM = oDT.Rows[0]["LPO_NM"].ToString();
                result._PERSG = oDT.Rows[0]["PERSG"].ToString();
                result._RETDT = oDT.Rows[0]["RETDT"].ToString();
                result._POSITION = oDT.Rows[0]["POSITION"].ToString();
                result._PERSK = oDT.Rows[0]["PERSK"].ToString();
                result._PERSK_DC = oDT.Rows[0]["PERSK_DC"].ToString();
                result._SACHZ = oDT.Rows[0]["SACHZ"].ToString();
                result._SCHKZ = oDT.Rows[0]["SCHKZ"].ToString();
                result._JOBFAMILY = oDT.Rows[0]["JOBFAMILY"].ToString();
                result._DEP_CD = oDT.Rows[0]["DEP_CD"].ToString();
                result._LPOCHECK = oDT.Rows[0]["LPOCHECK"].ToString();
                
                }
       

                return result;

        }

     
        public static string GetMailAddressbyEmpno(string empno)
        {
            
            USER_LIST result = new USER_LIST();
          

            //우리사원인지 확인
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.ASEFRONT);
            string sql = string.Empty;

            if (empno == null)
            {
                empno = string.Empty;
            }

            sql += " select * from USER_LIST  where PERSG not in ('2','9') and EMP_NO = '" + empno + "' ";

            DataSet oDS = oDB.QueryDataSet(sql);
            DataTable oDT = oDS.Tables[0];

            oDB.Close();
            oDT = oDS.Tables[0];

            if (oDT.Rows.Count > 0)
            {
                result._FL_NM = oDT.Rows[0]["FL_NM"].ToString();
                result._EMP_NO = oDT.Rows[0]["EMP_NO"].ToString();
                result._K_NM = oDT.Rows[0]["K_NM"].ToString();
                result._GRADE = oDT.Rows[0]["GRADE"].ToString();
                result._DEPT_NM = oDT.Rows[0]["DEPT_NM"].ToString();
                result._SUP_EMP_NO = oDT.Rows[0]["SUP_EMP_NO"].ToString();
                result._UPDATE_PASS = oDT.Rows[0]["UPDATE_PASS"].ToString();
                result._PERSK_NUM = oDT.Rows[0]["PERSK_NUM"].ToString();
                result._PQW = oDT.Rows[0]["PQW"].ToString();
                result._JOB_CD = oDT.Rows[0]["JOB_CD"].ToString();
                result._F_NM = oDT.Rows[0]["F_NM"].ToString();
                result._L_NM = oDT.Rows[0]["L_NM"].ToString();
                result._TEL_NO = oDT.Rows[0]["TEL_NO"].ToString();
                result._GRP_CD = oDT.Rows[0]["GRP_CD"].ToString();
                result._E_MAIL = oDT.Rows[0]["E_MAIL"].ToString();
                result._E_PASS = oDT.Rows[0]["E_PASS"].ToString();
                result._PASS_COUNT = oDT.Rows[0]["PASS_COUNT"].ToString();
                result._CARD_NO = oDT.Rows[0]["CARD_NO"].ToString();
                result._LPO = oDT.Rows[0]["LPO"].ToString();
                result._LPO_NM = oDT.Rows[0]["LPO_NM"].ToString();
                result._PERSG = oDT.Rows[0]["PERSG"].ToString();
                result._RETDT = oDT.Rows[0]["RETDT"].ToString();
                result._POSITION = oDT.Rows[0]["POSITION"].ToString();
                result._PERSK = oDT.Rows[0]["PERSK"].ToString();
                result._PERSK_DC = oDT.Rows[0]["PERSK_DC"].ToString();
                result._SACHZ = oDT.Rows[0]["SACHZ"].ToString();
                result._SCHKZ = oDT.Rows[0]["SCHKZ"].ToString();
                result._JOBFAMILY = oDT.Rows[0]["JOBFAMILY"].ToString();
                result._DEP_CD = oDT.Rows[0]["DEP_CD"].ToString();
                result._LPOCHECK = oDT.Rows[0]["LPOCHECK"].ToString();
            }
    
            
            string sEmail = result._E_MAIL;

            if (sEmail != null)
            {

                if (sEmail.Contains("@") == false)
                {
                    sEmail += sEmail + "@asekr.com";
                }

            }
            return sEmail;

        }

    }
}