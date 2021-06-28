using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASEWCFServiceLibrary.App_Code;
using System.Data;


namespace GPS201107.Models
{
    public class GPMATERIAL
    {
        public string PARTNUM { get; set; }
        public string _SUPPLIERCODE { get; set; }
        public string _SUPPLIERNAME { get; set; }
        public string _MATERIALNAME { get; set; }
        public string _ACTIVE { get; set; }
        public string _DISABLEREASON { get; set; }

        public GPMATERIAL()
        {
        }

        public IList<GPMATERIAL> Get_Materials(string Supplier_code)
        {
            IList<GPMATERIAL> list_materials = new List<GPMATERIAL>();

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            string sql = string.Empty;
            sql += "select MATERIALNAME, PARTNUM from GPMATERIAL where suppliercode ='" + Supplier_code + "' and  active ='T'";
            DataSet oDS = oDB.QueryDataSet(sql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                GPMATERIAL oGPSUPPLIER = new GPMATERIAL();
                oGPSUPPLIER._MATERIALNAME = oDT.Rows[i]["MATERIALNAME"].ToString();
                oGPSUPPLIER.PARTNUM = oDT.Rows[i]["PARTNUM"].ToString();              
                list_materials.Add(oGPSUPPLIER);
            }

            return list_materials;

        }
        public IList<GPMATERIAL> Get_Materials_All(string Supplier_code)
        {
            IList<GPMATERIAL> list_materials = new List<GPMATERIAL>();

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            string sql = string.Empty;
            sql += "select MATERIALNAME, PARTNUM ,ACTIVE, DISABLEREASON from GPMATERIAL where suppliercode ='" + Supplier_code + "' ";
            DataSet oDS = oDB.QueryDataSet(sql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                GPMATERIAL oGPSUPPLIER = new GPMATERIAL();
                oGPSUPPLIER._MATERIALNAME = oDT.Rows[i]["MATERIALNAME"].ToString();
                oGPSUPPLIER.PARTNUM = oDT.Rows[i]["PARTNUM"].ToString();
                oGPSUPPLIER._ACTIVE = oDT.Rows[i]["ACTIVE"].ToString();
                oGPSUPPLIER._DISABLEREASON = oDT.Rows[i]["DISABLEREASON"].ToString();
                list_materials.Add(oGPSUPPLIER);
            }

            return list_materials;

        }
    }
}