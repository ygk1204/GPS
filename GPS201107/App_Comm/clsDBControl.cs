using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace ASEWCFServiceLibrary.App_Code
{
    /// <summary>
    /// clsDBControl의 요약 설명입니다.
    /// </summary>
    public class clsDBControl
    {
        private OleDbConnection oConnect = null;
        private OleDbCommand oCommand = null;
        private clsConst.DBPROVIDER iDB;
        public clsDBControl(clsConst.DBPROVIDER DBServerNum)
        {
            iDB = DBServerNum;
            //String sConnectInfo = ConfigurationManager.AppSettings[(int)DBServerNum];
            String sConnectInfo = clsConst.DBPROVIDER_STRING[(int)DBServerNum]; 
            if (sConnectInfo == null || sConnectInfo.Length < 1) throw new Exception("Web Config Connect Info ERROR");
            oConnect = new OleDbConnection(sConnectInfo);
            oConnect.Open();
        }
        public clsDBControl(int iDbNum)
        {
            //String sConnectInfo = ConfigurationManager.AppSettings[iDbNum];
            String sConnectInfo = clsConst.DBPROVIDER_STRING[iDbNum];
            if (sConnectInfo == null || sConnectInfo.Length < 1) throw new Exception("Web Config Connect Info ERROR");
            oConnect = new OleDbConnection(sConnectInfo);
            oConnect.Open();
        }
        //~clsDBControl() //소멸자 정의
        //{
        //    this.Close();
        //}


        public bool ExcuteNonQuery(string sSql)
        {
            this.oCommand = new OleDbCommand(sSql, oConnect);
            oCommand.Transaction = oCommand.Connection.BeginTransaction();
            bool bResult = false;
            try
            {
                oCommand.ExecuteNonQuery();
                oCommand.Transaction.Commit();
                bResult = true;
            }
            catch (Exception e)
            {
                //clsMail.WriteLine(e.Message);
                oCommand.Transaction.Rollback();
                bResult = false;
            }
            finally
            {
                this.CloseCommand();
            }

            return bResult;

        }
        public bool ExcuteNonQuery(string[] sSql)
        {
            bool bResult = false;
            this.oCommand = new OleDbCommand();
            OleDbTransaction oTrans = null;


            oCommand.Connection = oConnect;
            oTrans = oConnect.BeginTransaction();
            this.oCommand.Transaction = oTrans;

            try
            {
                for (int i = 0; i < sSql.Length; i++)
                {
                    if ((sSql[i] != null) && (sSql[i].Trim().Length > 0))
                    {
                        oCommand.CommandText = sSql[i];
                        oCommand.ExecuteNonQuery();
                    }
                }

                oTrans.Commit();
                bResult = true;
            }
            catch (Exception ex)
            {
                oTrans.Rollback();
                //clsMail.WriteLine(ex.Message);
                bResult = false;
            }
            finally
            {
                this.CloseCommand();
            }

            return bResult;
        }
        public bool ExecuteScalar(String sSql)
        {
            bool bResult = false;

            oCommand = new OleDbCommand(sSql, oConnect);

            if (oCommand.ExecuteScalar() != null)
            {
                bResult = true;
            }
            else
            {
                bResult = false;
            }

            this.CloseCommand();

            return bResult;

        }
        public OleDbDataReader QueryDataReader(string sSql)
        {
            oCommand = new OleDbCommand(sSql, oConnect);

            System.Data.OleDb.OleDbDataReader reader = oCommand.ExecuteReader();

            return reader;
        }
        public DataSet QueryDataSet(string sSql)
        {

            DataSet oDataSet = new DataSet();

            OleDbDataAdapter oDataAdapter = new OleDbDataAdapter();
            oCommand = new OleDbCommand(sSql, oConnect);

            oDataAdapter.SelectCommand = oCommand;
            oDataAdapter.Fill(oDataSet);

            this.CloseCommand();
            return oDataSet;
        }
        public DataTable QueryDataTable(string sSql)
        {

            DataSet oDataSet = new DataSet();

            OleDbDataAdapter oDataAdapter = new OleDbDataAdapter();
            oCommand = new OleDbCommand(sSql, oConnect);

            oDataAdapter.SelectCommand = oCommand;
            oDataAdapter.Fill(oDataSet);
            this.CloseCommand();
            return oDataSet.Tables[0];
        }
        //added by skhan, 2010.08.17
        public DataSet QueryDataSetByBinding(string sSql, string[] aParam)
        {

            DataSet oDataSet = new DataSet();

            OleDbDataAdapter oDataAdapter = new OleDbDataAdapter();
            oCommand = new OleDbCommand(sSql, oConnect);

            //Binding arg count
            int iCountofBind = Regex.Matches(sSql, "[?]").Count;

            //Binding
            OleDbParameter odp = new OleDbParameter();
            for (int i = 0; i < iCountofBind; i++)
            {
                odp = new OleDbParameter("@param" + i.ToString(), aParam[0].ToString());
                oCommand.Parameters.Add(odp);
            }

            oDataAdapter.SelectCommand = oCommand;
            oDataAdapter.Fill(oDataSet);

            this.CloseCommand();
            return oDataSet;
        }
        public string QuerySingleData(string sSql)
        {
            oCommand = new OleDbCommand(sSql, oConnect);

            Object oResult = oCommand.ExecuteScalar();
            String sResult = "";
            if (oResult != null)
            {
                sResult = oResult.ToString();
            }

            oCommand.CommandText = "commit";
            oCommand.ExecuteScalar();
            oCommand.CommandText = "";
            this.CloseCommand();

            return sResult;
        }
        public void CloseCommand()
        {
            try
            {
                oCommand.Cancel();
                oCommand.Dispose();
                oCommand = null;

                return;
            }
            catch
            {
                return;
            }
        }
        public void Close()
        {
            if (oCommand != null)
            {
                oCommand.Cancel();
                oCommand.Dispose();
                oCommand = null;
            }

            if (oConnect.State == ConnectionState.Open)
            {
                oConnect.Close();
            }

            oConnect.Dispose();
            oConnect = null;
        }

    }
}