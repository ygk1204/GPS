using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASEWCFServiceLibrary.App_Code;

namespace GPS201107.Models
{
    public class GPSDOCUMENTLIST
    {
        public string FILEID         {get;set;}
	    public string _SUPPLIERCODE   {get;set;}
	    public string _SUPPLIERNAME   {get;set;}
	    public string _FILECATEGORY   {get;set;}
	    public string _MATERIALNAME	  {get;set;}
	    public string _PARTNUM        {get;set;}
	    public dynamic _FILENAME       {get;set;}
	    public string _ISSUEDATE      {get;set;}
	    public string _EXPIREDATE     {get;set;}
        public string _ACTIVE { get; set; }
        
        
        public GPSDOCUMENTLIST()
        {
        }

        public int CompareTo(GPSDOCUMENTLIST c1)
        {
            return this.FILEID.CompareTo(c1.FILEID);
        }

        public object Copy()
        {
            return base.MemberwiseClone();
        }
        public IList<GPSDOCUMENTLIST> List_Data(string WhereStmt, int page, int numofdata)
        {
            IList<GPSDOCUMENTLIST> resultList = new List<GPSDOCUMENTLIST>();
            GPSDOCUMENTLIST main = new GPSDOCUMENTLIST();

            EntityMapper omapper = new EntityMapper();

            omapper.oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            omapper.Table_entity.Add("GPSDOCUMENTLIST", main);
            omapper.WhereCondition = WhereStmt;
            omapper.Load(page, numofdata);
            for (int i = 0; i < omapper.Result[0].Count; i++)
            {
                resultList.Add((GPSDOCUMENTLIST)omapper.Result[0][i]);
            }


            return resultList;
        }
        public IList<GPSDOCUMENTLIST> List_Data_by_material(string WhereStmt)
        {
            IList<GPSDOCUMENTLIST> resultList = new List<GPSDOCUMENTLIST>();
            GPSDOCUMENTLIST main = new GPSDOCUMENTLIST();

            EntityMapper omapper = new EntityMapper();

            omapper.oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            omapper.Table_entity.Add("GPSDOCUMENTLIST", main);
            omapper.WhereCondition = WhereStmt;
            omapper.Load();
            for (int i = 0; i < omapper.Result[0].Count; i++)
            {
                resultList.Add((GPSDOCUMENTLIST)omapper.Result[0][i]);
            }


            return resultList;
        }
        public string GetTotalCount(string WhereStmt)
        {
            clsDBControl oDBCon = new clsDBControl(clsConst.DBPROVIDER.SCM); //test server            
            string sSql = "select count(fileid) from GPSDOCUMENTLIST";
            sSql += WhereStmt;
            string TotalCount = oDBCon.QuerySingleData(sSql);
            return TotalCount;
        }   
    
    }

}