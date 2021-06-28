 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASEWCFServiceLibrary.App_Code;
using System.Data;
using System.Data.OleDb;

namespace GPS201107.Models.HazardousRequest
{
    public class GpsHmFile
    {
        public string _HMREQID { get; set; }
        public string _FILETYPE { get; set; }
        public string _SEQ { get; set; }
        public string _FILENAME { get; set; }
        public string _PHYSICALFILENAME { get; set; }
        public string _PHYSICALFILELOCATION { get; set; }
        public string _FILESTATUS { get; set; }
        public string _FILECOMMENT { get; set; }
        public string _CREATEDATE { get; set; }
        public string _MODIFYDATE { get; set; }

        public dynamic _FILE_CONTAINER { get; set; }
        public string _DELETECHECK { get; set; }



        public GpsHmFile()
        {
        }
   
        public bool SaveGPSFileData(OleDbCommand cmd, GpsHmFile hmfile, string status)
        {
            bool bReturn = false;
            string sQuery = "";

            try
            {
                if (status == "Request")
                {
                  
                      // hmreqid, filetype, seq, filename, physicalfilename, physicalfilelocation, filestatus, createdate
                      sQuery += " INSERT INTO GPSHMFILE ( HMREQID, FILETYPE, SEQ, FILENAME, PHYSICALFILENAME, PHYSICALFILELOCATION, FILESTATUS, CREATEDATE) ";
                      sQuery += " VALUES ( '" + hmfile._HMREQID + "', '" + hmfile._FILETYPE + "', '" + hmfile._SEQ + "','" + hmfile._FILENAME + "'," + "'";
                      sQuery += hmfile._PHYSICALFILENAME + "','" + hmfile._PHYSICALFILELOCATION + "','" + hmfile._FILESTATUS + "','" + hmfile._CREATEDATE + "' " + " ) ";                    
                    
                    cmd.CommandText = sQuery;
                    cmd.ExecuteNonQuery();

                }

                //파일 삭제 시..
                else if (status == "Delete")
                {
                    sQuery += " Delete from Gpshmfile where HMREQID ='" + hmfile._HMREQID + "' and SEQ = '" + hmfile._SEQ + "' and FILETYPE = '" + hmfile._FILETYPE + "' ";
                    cmd.CommandText = sQuery;
                    cmd.ExecuteNonQuery();
                }

                return bReturn;

            }

            catch (Exception e)
            {

                bReturn = false;
                throw e;
            }
        }

        //public int CompareTo(GpsHmFile c1)
        //{
        //    return this._HMREQID.CompareTo(c1._HMREQID);
        //}


        //public object Copy()
        //{
        //    return base.MemberwiseClone();
        //}


        //현재 객체정보를 GpsHmFile에 저장한다.        
        // seq와 date는 자동으로 저장한다.
        public void Save(OleDbCommand cmd)
        {
            string sQuery = "";
            sQuery += " INSERT INTO GPSHMFILE ( HMREQID, FILETYPE, SEQ, FILENAME, PHYSICALFILENAME, PHYSICALFILELOCATION, FILESTATUS, CREATEDATE) ";
            sQuery += " VALUES ( '" + _HMREQID + "', '" + _FILETYPE + "', " + getMaxSeqQuery() + ",'" + _FILENAME + "'," + "'";
            sQuery += _PHYSICALFILENAME + "','" + _PHYSICALFILELOCATION + "','active',   to_char(sysdate, 'yyyy-mm-dd hh24:mi:ss') ) ";
            cmd.CommandText = sQuery;
            cmd.ExecuteNonQuery();
        }

        private string getMaxSeqQuery()
        {
            string sQuery = "";
            sQuery += " (SELECT DECODE(MAX(seq), NULL , 1,MAX(seq)+1) seq  ";
            sQuery += " FROM GPSHMFILE   ";
            sQuery += " WHERE filetype = '" + _FILETYPE + "' ";
            sQuery += " AND hmreqid = '" + _HMREQID + "')   ";
            return sQuery;

        }


        /// <summary>
        /// REQID, SEQ, FileType으로 삭제
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public void Delete(OleDbCommand cmd)
        {
            string sQuery = " Delete from Gpshmfile where HMREQID ='" + _HMREQID + "' and FILETYPE = '" + _FILETYPE + "' and seq='" + _SEQ+"'";
            cmd.CommandText = sQuery;
            cmd.ExecuteNonQuery();
        }


    }
}

