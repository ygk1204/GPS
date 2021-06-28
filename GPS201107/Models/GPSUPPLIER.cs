using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASEWCFServiceLibrary.App_Code;
using System.Data;

namespace GPS201107.Models
{
    public class GPSUPPLIER
    {
        public string SUPPLIERCODE { get; set; }
        public string SUPPLIERNAME { get; set; }
        public string CONTACT { get; set; }
        public string CONTACTTITLE { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public string ACTIVE { get; set; }
        public string DISABLEREASON { get; set; }

        public GPSUPPLIER()
        {
        }

        public IList<GPSUPPLIER> Get_Suppliers()
        {
            IList<GPSUPPLIER> list_suppliers = new List<GPSUPPLIER>();

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            string sql = string.Empty;
            sql += "select SUPPLIERCODE , SUPPLIERNAME, CONTACT, EMAIL from GPSUPPLIER where active ='T' ";
            DataSet oDS = oDB.QueryDataSet(sql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                GPSUPPLIER oGPSUPPLIER = new GPSUPPLIER();
                oGPSUPPLIER.SUPPLIERCODE = oDT.Rows[i]["SUPPLIERCODE"].ToString();
                oGPSUPPLIER.SUPPLIERNAME = oDT.Rows[i]["SUPPLIERNAME"].ToString();
                oGPSUPPLIER.CONTACT = oDT.Rows[i]["CONTACT"].ToString();
                oGPSUPPLIER.EMAIL = oDT.Rows[i]["EMAIL"].ToString();
                list_suppliers.Add(oGPSUPPLIER);
            }

            return list_suppliers;

        }
        public IList<GPSUPPLIER> Get_Suppliers_All()
        {
            IList<GPSUPPLIER> list_suppliers = new List<GPSUPPLIER>();

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            string sql = string.Empty;
            sql += "select distinct suppliercode , suppliername , contact,email, active, disablereason from gpsupplier ";


            DataSet oDS = oDB.QueryDataSet(sql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                GPSUPPLIER oGPSUPPLIER = new GPSUPPLIER();
                oGPSUPPLIER.SUPPLIERCODE = oDT.Rows[i]["SUPPLIERCODE"].ToString();
                oGPSUPPLIER.SUPPLIERNAME = oDT.Rows[i]["SUPPLIERNAME"].ToString();
                oGPSUPPLIER.CONTACT = oDT.Rows[i]["CONTACT"].ToString();
                oGPSUPPLIER.EMAIL = oDT.Rows[i]["EMAIL"].ToString();
                oGPSUPPLIER.ACTIVE = oDT.Rows[i]["ACTIVE"].ToString();
                oGPSUPPLIER.DISABLEREASON = oDT.Rows[i]["DISABLEREASON"].ToString();
                list_suppliers.Add(oGPSUPPLIER);
            }

            return list_suppliers;

        }

    }
}