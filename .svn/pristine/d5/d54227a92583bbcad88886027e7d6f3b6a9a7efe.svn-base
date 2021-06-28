using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASEWCFServiceLibrary.App_Code;
using System.Data;

namespace GPS201107.Models
{
    public class GPCONTACTPERSON
    {
        public string _SUPPLIERCODE  {get;set;}  
	    public string _SUPPLIERNAME  {get;set;}  
        public string _PERSONNAME    {get;set;}  
	    public string _MAILTYPE      {get;set;}  
	    public string _MAILADDRESS   {get;set;}  
	    public string _PHONE        {get;set;}  
        
        public GPCONTACTPERSON()
        {

        }
        public IList<GPCONTACTPERSON> Get_Contacts(string Supplier_code)
        {
            IList<GPCONTACTPERSON> list_contacts = new List<GPCONTACTPERSON>();

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            string sql = string.Empty;
            sql += "select SUPPLIERCODE, SUPPLIERNAME, PERSONNAME,MAILTYPE ,MAILADDRESS,PHONE from GPCONTACTPERSON where suppliercode ='" + Supplier_code + "'";
            DataSet oDS = oDB.QueryDataSet(sql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                GPCONTACTPERSON oGPContacts = new GPCONTACTPERSON();
                oGPContacts._SUPPLIERCODE = oDT.Rows[i]["SUPPLIERCODE"].ToString();
                oGPContacts._SUPPLIERNAME = oDT.Rows[i]["SUPPLIERNAME"].ToString();
                oGPContacts._PERSONNAME = oDT.Rows[i]["PERSONNAME"].ToString();
                oGPContacts._MAILTYPE = oDT.Rows[i]["MAILTYPE"].ToString();
                oGPContacts._MAILADDRESS = oDT.Rows[i]["MAILADDRESS"].ToString();
                oGPContacts._PHONE = oDT.Rows[i]["PHONE"].ToString();
                list_contacts.Add(oGPContacts);
            }

            return list_contacts;

        }
    
        public object Copy()
        {
            return base.MemberwiseClone();
        }
    }
}