using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASEWCFServiceLibrary.App_Code;
using System.Data;
namespace GPS201107.Models
{
    public class AWDBModels
    {
        public string _PRODUCTNAME { get; set; }
        public string _PARTNUM { get; set; }

        public AWDBModels()
        {
        }
        //[2021.03.29] [ASWR_27613_20210325142254]
        // MES에서 BP에서 Part List를 가져온다. 'PartList'는 무조건 포함된다.
        // 반환값 샘플:  and AND PARTNUM IN ( 'PartList', 'add' ) 
        public string getWhereFIlterWithPartList(string productname)
        {   
            clsDBControl oDB = null;            
            try
            {
                string sql = "";
                string result = " and  PARTNUM IN ( 'PartList' "; //무조건 1개는 추가해줌.                        
                oDB = new clsDBControl(clsConst.DBPROVIDER.MES);
                sql += " SELECT BI.VALUE AS PARTNUM ";
                sql += " FROM AWPDDBPHEADER BH, AWPDDBPITEM BI   ";
                sql += " WHERE BH.PRODUCTNAME = BI.PRODUCTNAME   ";
                sql += " AND BH.VERSION >= BI.STARTVERSION ";
                sql += " AND BH.VERSION < BI.STOPVERSION    ";
                sql += " AND BH.ITEMTYPE = BI.KEYTYPE  ";
                sql += " AND BH.STATUS = 'Active' ";
                sql += " AND BH.ITEMTYPE = 'Routing' ";
                sql += " AND BH.PRODUCTNAME = '" + productname + "'  ";
                DataTable oDt = oDB.QueryDataTable(sql);

                for (int i = 0; i < oDt.Rows.Count; i++)
                {
                    result += ",'" + oDt.Rows[i]["PARTNUM"].ToString() + "'";                    
                }
                return result + " )";
            }
            catch (Exception e)
            {
                throw new Exception("MES DB 조회중 오류 발생", e);
            }
            finally
            {
                if( oDB !=null) oDB.Close();
            }
        }

    
    }





}