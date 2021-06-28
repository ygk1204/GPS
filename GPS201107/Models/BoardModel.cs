using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using ASEWCFServiceLibrary.App_Code;

namespace GPS201107.Models
{
    public class Board
    {
        public int iIden { get; set; }
        public int iNumber { get; set; }
        public int iVinumber { get; set; }
        public int iRenumber { get; set; }
        public string sId { get; set; }
        public string sName { get; set; }
        public string sEmail { get; set; }
        public string sFilename { get; set; }
        public string sTitle { get; set; }
        public string sDate { get; set; }
        public int iRead { get; set; }
        public string sComment { get; set; }
        public string sPass { get; set; }
        public string sIp { get; set; }
        public char cCheck { get; set; }
        public int iRealnumber { get; set; }
        public string sBoardname { get; set; }
        public string sEmpno { get; set; }
        public List<Board> oReplylist { get; set; }

        public Board()
        {
            iIden = 0;
            iNumber = 0;
            iVinumber = 0;
            iRenumber = 0;
            sId = "";
            sName = "";
            sEmail = "";
            sFilename = "";
            sTitle = "";
            sDate = "";
            iRead = 0;
            sComment = "";
            sPass = "";
            sIp = "";
            cCheck = ' ';
            iRealnumber = 0;
            sBoardname = "";
            sEmpno = "";
            oReplylist = new List<Board>();
        }
        public Board(string _sBoardname, int _iNumber)
        {
            string sSql = " select NVL(AF_IDEN,0)AF_IDEN, AF_NUMBER, NVL(AF_VINUMBER,0)AF_VINUMBER, NVL(AF_RENUMBER, 0) AF_RENUMBER, ";
            sSql += " AF_ID, AF_NAME, NVL(AF_EMAIL,'')AF_EMAIL, NVL(AF_FILENAME,'')AF_FILENAME, AF_TITLE, AF_DATE, ";
            sSql += " AF_READ,AF_COMMENT,NVL(AF_PASS,'')AF_PASS,AF_IP,AF_CHECK,NVL(AF_REALNUMBER, 0) AF_REALNUMBER, ";
            sSql += " AF_BOARDNAME, NVL(AF_EMPNO,'')AF_EMPNO ";
            sSql += " from WEBBOARD where af_boardname = '" + _sBoardname + "' and AF_NUMBER = '" + _iNumber.ToString() + "' and af_check in('0','1','5') ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            if (oDT.Rows.Count > 0)
            {
                iIden = Convert.ToInt32(oDT.Rows[0]["AF_IDEN"].ToString());
                iNumber = Convert.ToInt32(oDT.Rows[0]["AF_NUMBER"].ToString());
                iVinumber = Convert.ToInt32(oDT.Rows[0]["AF_VINUMBER"].ToString());
                iRenumber = Convert.ToInt32(oDT.Rows[0]["AF_RENUMBER"].ToString());
                sId = oDT.Rows[0]["AF_ID"].ToString();
                sName = oDT.Rows[0]["AF_NAME"].ToString();
                sEmail = oDT.Rows[0]["AF_EMAIL"].ToString();
                sFilename = oDT.Rows[0]["AF_FILENAME"].ToString();
                sTitle = oDT.Rows[0]["AF_TITLE"].ToString();
                sDate = oDT.Rows[0]["AF_DATE"].ToString();
                iRead = Convert.ToInt32(oDT.Rows[0]["AF_READ"].ToString());
                sComment = oDT.Rows[0]["AF_COMMENT"].ToString();
                sPass = oDT.Rows[0]["AF_PASS"].ToString();
                sIp = oDT.Rows[0]["AF_IP"].ToString();
                cCheck = Convert.ToChar(oDT.Rows[0]["AF_CHECK"].ToString());
                iRealnumber = Convert.ToInt32(oDT.Rows[0]["AF_REALNUMBER"].ToString());
                sBoardname = oDT.Rows[0]["AF_BOARDNAME"].ToString();
                sEmpno = oDT.Rows[0]["AF_EMPNO"].ToString();
                oReplylist = GetReply();
            }

        }
        public Board(int _iIden, int _iNumber, int _iVinumber, int _iRenumber,
            string _sId, string _sName, string _sEmail, string _sFilename, string _sTitle, string _sDate,
            int _iRead, string _sComment, string _sPass, string _sIp, char _cCheck, int _iRealnumber, string _sBoardname, string _sEmpno)
        {
            iIden = _iIden;
            iNumber = _iNumber;
            iVinumber = _iVinumber;
            iRenumber = _iRenumber;
            sId = _sId;
            sName = _sName;
            sEmail = _sEmail;
            sFilename = _sFilename;
            sTitle = _sTitle;
            sDate = _sDate;
            iRead = _iRead;
            sComment = _sComment;
            sPass = _sPass;
            sIp = _sIp;
            cCheck = _cCheck;
            iRealnumber = _iRealnumber;
            sBoardname = _sBoardname;
            sEmpno = _sEmpno;
            oReplylist = GetReply();
        }
        private List<Board> GetReply()
        {
            List<Board> rBoardlist = new List<Board>();

            string sSql = " select NVL(AF_IDEN,0)AF_IDEN, AF_NUMBER, NVL(AF_VINUMBER,0)AF_VINUMBER, NVL(AF_RENUMBER, 0) AF_RENUMBER, ";
            sSql += " AF_ID, AF_NAME, NVL(AF_EMAIL,'')AF_EMAIL, NVL(AF_FILENAME,'')AF_FILENAME, AF_TITLE, AF_DATE, ";
            sSql += " AF_READ,AF_COMMENT,NVL(AF_PASS,'')AF_PASS,AF_IP,AF_CHECK,NVL(AF_REALNUMBER, 0) AF_REALNUMBER, ";
            sSql += " AF_BOARDNAME, NVL(AF_EMPNO,'')AF_EMPNO ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' and af_check='1' and AF_RENUMBER = '" + iNumber.ToString() + "' order by AF_NUMBER desc ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];

            Board oBoard = new Board();
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                oBoard = new Board(Convert.ToInt32(oDT.Rows[i]["AF_IDEN"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_NUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_VINUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_RENUMBER"].ToString()),
                    oDT.Rows[i]["AF_ID"].ToString(), oDT.Rows[i]["AF_NAME"].ToString(), oDT.Rows[i]["AF_EMAIL"].ToString(), oDT.Rows[i]["AF_FILENAME"].ToString(), oDT.Rows[i]["AF_TITLE"].ToString(), oDT.Rows[i]["AF_DATE"].ToString(),
                    Convert.ToInt32(oDT.Rows[i]["AF_READ"].ToString()),
                    oDT.Rows[i]["AF_COMMENT"].ToString(), oDT.Rows[i]["AF_PASS"].ToString(), oDT.Rows[i]["AF_IP"].ToString(),
                    Convert.ToChar(oDT.Rows[i]["AF_CHECK"].ToString()),
                    Convert.ToInt32(oDT.Rows[i]["AF_REALNUMBER"].ToString()),
                    oDT.Rows[i]["AF_BOARDNAME"].ToString(), oDT.Rows[i]["AF_EMPNO"].ToString());
                rBoardlist.Add(oBoard);
            }
            return rBoardlist;
        }

        public String GetHtmlContents_first()
        {
            String sReturn = "";
            sReturn += "<tr class=\"frm\">";
            sReturn += "<td class=\"num\">" + this.iRealnumber.ToString() + "</td>";
            sReturn += "<td class=\"title\"><a href=\"/gps/board/readboard/" + sBoardname + "/" + iNumber.ToString() + "\">" + this.sTitle.ToString() + "</a></td>";
            sReturn += "<td> " + this.sName.ToString() + "</td>";
            sReturn += "<td class=\"date\">" + this.sDate.ToString().Substring(0, 10) + "</td>";
            sReturn += "<td class=\"hit\">" + this.iRead.ToString() + "</td>";
            if (this.sFilename.ToString() != "")
            {
                sReturn += "<td> <a href='/gps/Board/Download/this.sBoardname/" + this.iNumber.ToString() + "/" + this.sFilename + "'><img src='/Content/images/board/iconFile.gif'/></a></td>";
            }
            else
            {
                sReturn += "<td>&nbsp;" + this.sFilename.ToString() + "</td>";
            }
            sReturn += "</tr>";
            sReturn += GetHtmlReply(this.oReplylist);

            return sReturn;
        }
        public String GetHtmlContents()
        {
            String sReturn = "";
            sReturn += "<tr class=\"frm\">";
            sReturn += "<td class=\"num\">" + this.iRealnumber.ToString() + "</td>";
            sReturn += "<td class=\"title\"><a href=\"/gps/board/readboard/" + sBoardname + "/" + iNumber.ToString() + "\">" + this.sTitle.ToString() + "</a></td>";
            sReturn += "<td> " + this.sName.ToString() + "</td>";
            sReturn += "<td class=\"date\">" + this.sDate.ToString().Substring(0, 10) + "</td>";
            sReturn += "<td class=\"hit\">" + this.iRead.ToString() + "</td>";
            if (this.sFilename.ToString() != "")
            {
                sReturn += "<td> <a href='/gps/Board/Download/this.sBoardname/" + this.iNumber.ToString() + "/" + this.sFilename + "'><img src='/Content/images/board/iconFile.gif'/></a></td>";
            }
            else
            {
                sReturn += "<td>&nbsp;" + this.sFilename.ToString() + "</td>";
            }
            sReturn += "</tr>";
            sReturn += GetHtmlReply(this.oReplylist);

            return sReturn;
        }
        public String GetHtmlContents_first_Search(String category, String keyword, String page)
        {
            String sReturn = "";
            sReturn += "<tr class=\"frm\">";
            sReturn += "<td class=\"num\">" + this.iRealnumber.ToString() + "</td>";
            sReturn += "<td class=\"title\"><a href=\"/gps/board/readboard_search/" + sBoardname + "/" + iNumber.ToString() + "?category=" + category + "&keyword=" + keyword + "&page=" + page + "\">" + this.sTitle.ToString() + "</a></td>";
            sReturn += "<td> " + this.sName.ToString() + "</td>";
            sReturn += "<td class=\"date\">" + this.sDate.ToString().Substring(0, 10) + "</td>";
            sReturn += "<td class=\"hit\">" + this.iRead.ToString() + "</td>";
            sReturn += "<td>&nbsp; " + this.sFilename.ToString() + "</td>";
            sReturn += "</tr>";
            //sReturn += GetHtmlReply(this.oReplylist);

            return sReturn;
        }
        public String GetHtmlContents_Search(String category, String keyword, String page)
        {
            String sReturn = "";
            sReturn += "<tr class=\"frm\">";
            sReturn += "<td class=\"num\">" + this.iRealnumber.ToString() + "</td>";
            sReturn += "<td class=\"title\"><a href=\"/gps/board/readboard_search/" + sBoardname + "/" + iNumber.ToString() + "?category=" + category + "&keyword=" + keyword + "&page=" + page + "\">" + this.sTitle.ToString() + "</a></td>";
            sReturn += "<td> " + this.sName.ToString() + "</td>";
            sReturn += "<td class=\"date\">" + this.sDate.ToString().Substring(0, 10) + "</td>";
            sReturn += "<td class=\"hit\">" + this.iRead.ToString() + "</td>";
            sReturn += "<td>&nbsp;" + this.sFilename.ToString() + "</td>";
            sReturn += "</tr>";
            //sReturn += GetHtmlReply(this.oReplylist);

            return sReturn;
        }

        private String GetHtmlReply(List<Board> oBoardlist)
        {
            String sReturn = "";
            foreach (Board oBoard in oBoardlist)
            {
                sReturn += "<tr class=\"frm\">";
                sReturn += "<td class=\"bo_td_reply\"><img src=\"/gps/Content/images/board/re.gif\" alt=\"Re\"/></td>";
                sReturn += "<td class=\"title\"><a href=\"/gps/board/readboard/" + oBoard.sBoardname + "/" + oBoard.iNumber.ToString() + "\">" + oBoard.sTitle.ToString() + "</a></td>";
                sReturn += "<td >" + oBoard.sName.ToString() + "</td>";
                sReturn += "<td class=\"date\">" + oBoard.sDate.ToString().Substring(0, 10) + "</td>";
                sReturn += "<td class=\"hit\">" + oBoard.iRead.ToString() + "</td>";
                sReturn += "<td>&nbsp;" + oBoard.sFilename.ToString() + "</td>";
                sReturn += "</tr>";
                sReturn += GetHtmlReply(oBoard.oReplylist);
            }

            return sReturn;
        }

        public void IncreaseReadCount()
        {
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            try
            {
                string sSql = " update WEBBOARD set AF_READ = AF_READ + 1 where af_boardname = '" + sBoardname + "' and AF_NUMBER = '" + iNumber.ToString() + "' and af_check in ('0','1','5') ";

                oDB.ExcuteNonQuery(sSql);
                iRead++;
            }
            catch
            { }
            finally
            {
                oDB.Close();
            }
        }

        //Get Max(iNumber)
        public int GetMaxINumber()
        {
            int iReturn = 0;

            string sSql = " select NVL(Max(AF_NUMBER),1) maxnumber ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];

            if (oDT.Rows.Count > 0)
            {
                iReturn = Convert.ToInt32(oDT.Rows[0]["maxnumber"].ToString());
            }

            return iReturn;
        }
        //Get Max(iRealnumber)
        public int GetMaxRealNumber()
        {
            int iReturn = 0;

            string sSql = " select NVL(Max(AF_REALNUMBER),1) maxnumber ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];

            if (oDT.Rows.Count > 0)
            {
                iReturn = Convert.ToInt32(oDT.Rows[0]["maxnumber"].ToString());
            }

            return iReturn;
        }
        //Write
        public bool WriteBoard()
        {
            bool bReturn = false;
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);

            try
            {
                string sSql = " insert ";

                sSql += " into WEBBOARD ( ";
                //sSql += " AF_IDEN, ";
                sSql += " AF_NUMBER, ";
                //sSql += " AF_VINUMBER, ";
                //sSql += " AF_RENUMBER, ";
                //sSql += " AF_ID, ";
                sSql += " AF_NAME, ";
                //sSql += " AF_EMAIL, ";
                sSql += " AF_FILENAME, ";
                sSql += " AF_TITLE, ";
                sSql += " AF_DATE, ";
                sSql += " AF_READ, ";
                sSql += " AF_COMMENT, ";
                sSql += " AF_PASS, ";
                sSql += " AF_IP, ";
                sSql += " AF_CHECK, ";
                sSql += " AF_REALNUMBER, ";
                sSql += " AF_BOARDNAME, ";
                sSql += " AF_EMPNO ";
                sSql += " ) ";

                sSql += " values ( ";
                //sSql += iIden.ToString() + ", ";
                sSql += iNumber.ToString() + ", ";
                //sSql += iVinumber.ToString()+ ", ";
                //sSql += iRenumber.ToString()+ ", ";
                //sSql += " '" + sId+ "', ";
                sSql += " '" + sName.Replace("'", "`") + "', ";
                //sSql += " '" + sEmail + "', ";
                sSql += " '" + sFilename + "', ";
                sSql += " '" + sTitle.Replace("'", "`") + "', ";
                sSql += " '" + sDate + "', ";
                sSql += iRead.ToString() + ", ";
                sSql += " '" + sComment.Replace("'", "`") + "', ";
                sSql += " '" + sPass.Replace("'", "`") + "', ";
                sSql += " '" + sIp + "', ";
                sSql += " '" + cCheck + "', ";
                sSql += iRealnumber.ToString() + ", ";
                sSql += " '" + sBoardname + "', ";
                sSql += " '" + sEmpno + "' ";
                sSql += " ) ";

                bReturn = oDB.ExcuteNonQuery(sSql);
            }
            catch
            { }
            finally
            {
                oDB.Close();
            }

            return bReturn;
        }
        //Modify
        public bool ModifyBoard()
        {
            bool bReturn = false;
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);

            try
            {
                string sSql = " update WEBBOARD set ";
                //sSql += " AF_IDEN = "+iIden+", ";                
                //sSql += " AF_VINUMBER = "+iVinumber+", ";
                //sSql += " AF_RENUMBER = "+iRenumber+", ";
                //sSql += " AF_ID = '"+sId+"', ";
                sSql += " AF_NAME = '" + sName.Replace("'", "`") + "', ";
                //sSql += " AF_EMAIL = '"+sEmail+"', ";
                sSql += " AF_FILENAME = '" + sFilename + "', ";
                sSql += " AF_TITLE = '" + sTitle.Replace("'", "`") + "', ";
                sSql += " AF_DATE = '" + sDate + "', ";
                //sSql += " AF_READ = " + iRead + ", ";
                sSql += " AF_COMMENT = '" + sComment.Replace("'", "`") + "', ";
                sSql += " AF_PASS = '" + sPass.Replace("'", "`") + "', ";
                //sSql += " AF_IP = '" + sIp + "', ";
                //sSql += " AF_CHECK = '" + cCheck + "', ";
                //sSql += " AF_REALNUMBER = " + iRealnumber + ", ";
                //sSql += " AF_BOARDNAME = '" + sBoardname + "', ";
                sSql += " AF_EMPNO = '" + sEmpno + "' ";
                sSql += " where AF_NUMBER = " + iNumber.ToString() + " ";
                sSql += " and AF_BOARDNAME = '" + sBoardname + "' ";
                bReturn = oDB.ExcuteNonQuery(sSql);
            }
            catch
            { }
            finally
            {
                oDB.Close();
            }


            return bReturn;
        }
        //Reply
        public bool ReplyBoard()
        {
            bool bReturn = false;
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);

            try
            {
                string sSql = " insert ";

                sSql += " into WEBBOARD ( ";
                //sSql += " AF_IDEN, ";
                sSql += " AF_NUMBER, ";
                //sSql += " AF_VINUMBER, ";
                sSql += " AF_RENUMBER, ";
                //sSql += " AF_ID, ";
                sSql += " AF_NAME, ";
                //sSql += " AF_EMAIL, ";
                sSql += " AF_FILENAME, ";
                sSql += " AF_TITLE, ";
                sSql += " AF_DATE, ";
                sSql += " AF_READ, ";
                sSql += " AF_COMMENT, ";
                sSql += " AF_PASS, ";
                sSql += " AF_IP, ";
                sSql += " AF_CHECK, ";
                //sSql += " AF_REALNUMBER, ";
                sSql += " AF_BOARDNAME, ";
                sSql += " AF_EMPNO ";
                sSql += " ) ";

                sSql += " values ( ";
                //sSql += iIden.ToString() + ", ";
                sSql += iNumber.ToString() + ", ";
                //sSql += iVinumber.ToString()+ ", ";
                sSql += iRenumber.ToString() + ", ";
                //sSql += " '" + sId+ "', ";
                sSql += " '" + sName.Replace("'", "`") + "', ";
                //sSql += " '" + sEmail + "', ";
                sSql += " '" + sFilename + "', ";
                sSql += " '" + sTitle.Replace("'", "`") + "', ";
                sSql += " '" + sDate + "', ";
                sSql += iRead.ToString() + ", ";
                sSql += " '" + sComment.Replace("'", "`") + "', ";
                sSql += " '" + sPass.Replace("'", "`") + "', ";
                sSql += " '" + sIp + "', ";
                sSql += " '" + cCheck + "', ";
                //sSql += iRealnumber.ToString() + ", ";
                sSql += " '" + sBoardname + "', ";
                sSql += " '" + sEmpno + "' ";
                sSql += " ) ";

                bReturn = oDB.ExcuteNonQuery(sSql);
            }
            catch
            { }
            finally
            {
                oDB.Close();
            }

            return bReturn;
        }
        //Delete
        public bool DeleteBoard(string _sBoardname, int _iNumber)
        {

            bool bReturn = false;

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            try
            {
                //af_check 가 "4"일 경우에 삭제된 게시물로 간주함.
                string sSql = " update WEBBOARD set af_check = '4' where af_boardname = '" + _sBoardname + "' and AF_NUMBER = '" + _iNumber.ToString() + "' ";

                bReturn = oDB.ExcuteNonQuery(sSql);
            }
            catch
            { }
            finally
            {
                oDB.Close();
            }

            return bReturn;
        }
        public bool Inactivate(string _sBoardname, int _iNumber)
        {

            bool bReturn = false;

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            try
            {
                //af_check 가 "5"일 경우에 팝업을 띄우지 않음
                string sSql = " update WEBBOARD set af_check = '5' where af_boardname = '" + _sBoardname + "' and AF_NUMBER = '" + _iNumber.ToString() + "' ";

                bReturn = oDB.ExcuteNonQuery(sSql);
            }
            catch
            { }
            finally
            {
                oDB.Close();
            }

            return bReturn;
        }
        public bool activate(string _sBoardname, int _iNumber)
        {

            bool bReturn = false;

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            try
            {
                //af_check 가 "5"일 경우에 팝업을 띄우지 않음
                string sSql = " update WEBBOARD set af_check = '1' where af_boardname = '" + _sBoardname + "' and AF_NUMBER = '" + _iNumber.ToString() + "' ";

                bReturn = oDB.ExcuteNonQuery(sSql);
            }
            catch
            { }
            finally
            {
                oDB.Close();
            }

            return bReturn;
        }
        public string GenerateFilename(string _sPath, string _sFilename)
        {
            string sFilenameHead = "";
            string sFilenameTail = "";
            string[] lFilename = _sFilename.Split('.');
            sFilenameHead = lFilename[0];
            if (lFilename.Length > 1)
            {
                for (int i = 1; i < lFilename.Length; i++)
                {
                    sFilenameTail += lFilename[i];
                }
            }
            string path = Path.Combine(_sPath, sFilenameHead + "." + sFilenameTail);
            FileInfo fileinfo = new FileInfo(path);
            while (fileinfo.Exists == true) //같은 이름의 파일이 있을 경우에 처리방법 : 기존파일이름에 "-1"을 붙인다.
            {
                sFilenameHead += "-1";
                path = Path.Combine(_sPath, sFilenameHead + "." + sFilenameTail);
                fileinfo = new FileInfo(path);
            }

            return sFilenameHead + "." + sFilenameTail;
        }

    }

    public class BoardModels
    {
        private string sBoardname = "";
        private int iListCount = 0; //쪽당 표시하는 글의 갯수.
        private int iPaging = 0; //페이징 갯수.

        public BoardModels()
        {
            sBoardname = "";
            iListCount = 10;
            iPaging = 10;
        }
        public BoardModels(string _sBoardname)
        {
            sBoardname = _sBoardname;
            iListCount = 10;
            iPaging = 10;
        }
        public BoardModels(string _sBoardname, int _iListCount, int _iPaging)
        {
            sBoardname = _sBoardname;
            iListCount = _iListCount;
            iPaging = _iPaging;
        }

        public Board GetRandomItem()
        {
            Board oBoard = new Board();
            int iRandom = 0;
            int iCount = GetlistCount();
            List<Board> lBoardlist = Getlist();

            Random rRandom = new Random();
            iRandom = rRandom.Next(0, iCount);

            oBoard = lBoardlist[iRandom];

            return oBoard;
        }

        //화면에 보여줄 해당 page 목록을 iPaging, iListCount을 반영해 보여준다. 
        //태그로 해당 데이터를 넘겨줌.
        public string GetPaging(int iCurrentPage)
        {
            string sReturn = string.Empty;
            int iTotalListCnt = GetlistCount();
            int iTotalPageCnt = iTotalListCnt / iListCount;
            if (iTotalListCnt % iListCount != 0)
                iTotalPageCnt++;
            int iStartPage = ((iCurrentPage - 1) / iPaging) * iPaging + 1;

            int iEndPage = iStartPage + iPaging - 1;
            if (iEndPage > iTotalPageCnt)
                iEndPage = iTotalPageCnt;

            int iPreStartPage = iStartPage - iPaging;

            int iNextStartPage = iEndPage + 1;

            string sTemp = string.Empty;

            //10. 이전글 페이지
            if (iPreStartPage < 1)
                sTemp = "<span class=\"bo_a_nopage\">이전글없음</span> ";
            else
                sTemp = "<a class=\"bo_a_pagelist\" href=\"/board/Index/" + sBoardname + "/" + iPreStartPage.ToString() + "\">이전글" + iPaging.ToString() + "개</a> ";

            sReturn += sTemp;
            //20. 페이지 리스트            
            for (int i = iStartPage; i <= iEndPage; i++)
            {
                if (i == iCurrentPage)
                {
                    sTemp = "<span class=\"bo_a_currentpage\">" + i.ToString() + "</span> ";
                    sReturn += sTemp;
                }
                else
                {
                    sTemp = "<a class=\"bo_a_pagelist\" href=\"/board/Index/" + sBoardname + "/" + i.ToString() + "\">" + i.ToString() + "</a> ";

                    sReturn += sTemp;
                }
            }
            //30. 다음글 페이지
            if (iNextStartPage > iTotalPageCnt)
                sTemp = "<span class=\"bo_a_nopage\">다음글없음</span> ";
            else
                sTemp = "<a class=\"bo_a_pagelist\" href=\"/board/Index/" + sBoardname + "/" + iNextStartPage.ToString() + "\">다음글" + iPaging.ToString() + "개</a> ";

            sReturn += sTemp;

            return sReturn;
        }
        public string GetPaging_Search(int iCurrentPage, string category, string keyword) // 검색 결과 페이지 가져 오기
        {
            string sReturn = string.Empty;
            int iTotalListCnt = GetlistCount_Search(category, keyword);
            int iTotalPageCnt = iTotalListCnt / iListCount;
            if (iTotalListCnt % iListCount != 0)
                iTotalPageCnt++;
            int iStartPage = ((iCurrentPage - 1) / iPaging) * iPaging + 1;

            int iEndPage = iStartPage + iPaging - 1;
            if (iEndPage > iTotalPageCnt)
                iEndPage = iTotalPageCnt;

            int iPreStartPage = iStartPage - iPaging;

            int iNextStartPage = iEndPage + 1;

            string sTemp = string.Empty;

            //10. 이전글 페이지
            if (iPreStartPage < 1)
                sTemp = "<span class=\"bo_a_nopage\">이전글없음</span> ";
            else
                sTemp = "<a class=\"bo_a_pagelist\" href=\"/board/Index_Search/?boardname=" + sBoardname + "&page=" + iPreStartPage.ToString() + "&category=" + category + "&keyword=" + keyword + "\">이전글" + iPaging.ToString() + "개</a> ";

            sReturn += sTemp;
            //20. 페이지 리스트            
            for (int i = iStartPage; i <= iEndPage; i++)
            {
                if (i == iCurrentPage)
                {
                    sTemp = "<span class=\"bo_a_currentpage\">" + i.ToString() + "</span> ";
                    sReturn += sTemp;
                }
                else
                {
                    sTemp = "<a class=\"bo_a_pagelist\" href=\"/board/Index_Search/?boardname=" + sBoardname + "&page=" + i.ToString() + "&category=" + category + "&keyword=" + keyword + "\">" + i.ToString() + "</a> ";

                    sReturn += sTemp;
                }
            }
            //30. 다음글 페이지
            if (iNextStartPage > iTotalPageCnt)
                sTemp = "<span class=\"bo_a_nopage\">다음글없음</span> ";
            else
                sTemp = "<a class=\"bo_a_pagelist\" href=\"/board/Index_Search/?boardname=" + sBoardname + "&page=" + iNextStartPage.ToString() + "&category=" + category + "&keyword=" + keyword + "\">이전글" + iPaging.ToString() + "개</a> ";

            sReturn += sTemp;

            return sReturn;
        }

        private int GetlistCount()
        {
            int iReturn = 0;
            string sSql = " select NVL(count(AF_NUMBER),0) listcount ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' and af_check='1' and AF_RENUMBER is null ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            if (oDT.Rows.Count > 0)
                iReturn = Convert.ToInt32(oDT.Rows[0]["listcount"].ToString());

            return iReturn;
        }
        private int GetlistCount_Search(String category, String keyword)
        {
            int iReturn = 0;
            string sSql = " select NVL(count(AF_NUMBER),0) listcount ";
            sSql += "from WEBBOARD";
            sSql += " where af_boardname = '" + sBoardname + "' and af_check='1' and " + category + " like '%" + keyword + "%' or af_check='0'  and " + category + " like '%" + keyword + "%'"; // 검색 조건
            sSql += " order by AF_NUMBER asc";
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            if (oDT.Rows.Count > 0)
                iReturn = Convert.ToInt32(oDT.Rows[0]["listcount"].ToString());

            return iReturn;
        }

        //화면에 보여줄 해당 page의 board리스트를 보여준다. 
        public List<Board> Getlist(int iCurrentPage)
        {
            List<Board> rBoardlist = new List<Board>();
            string sSql = " ";
            sSql += " select NVL(AF_IDEN,0)AF_IDEN, AF_NUMBER, NVL(AF_VINUMBER,0)AF_VINUMBER, NVL(AF_RENUMBER, 0) AF_RENUMBER, ";
            sSql += " AF_ID, AF_NAME, NVL(AF_EMAIL,'')AF_EMAIL, NVL(AF_FILENAME,'')AF_FILENAME, AF_TITLE, AF_DATE, ";
            sSql += " AF_READ,AF_COMMENT,NVL(AF_PASS,'')AF_PASS,AF_IP,AF_CHECK,NVL(AF_REALNUMBER, 0) AF_REALNUMBER, ";
            sSql += " AF_BOARDNAME, NVL(AF_EMPNO,'')AF_EMPNO ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' and af_check='1' and AF_RENUMBER is null ";
            sSql += " order by AF_NUMBER asc ";

            string sSql_first = " ";
            sSql_first += " select NVL(AF_IDEN,0)AF_IDEN, AF_NUMBER, NVL(AF_VINUMBER,0)AF_VINUMBER, NVL(AF_RENUMBER, 0) AF_RENUMBER, ";
            sSql_first += " AF_ID, AF_NAME, NVL(AF_EMAIL,'')AF_EMAIL, NVL(AF_FILENAME,'')AF_FILENAME, AF_TITLE, AF_DATE, ";
            sSql_first += " AF_READ,AF_COMMENT,NVL(AF_PASS,'')AF_PASS,AF_IP,AF_CHECK,NVL(AF_REALNUMBER, 0) AF_REALNUMBER, ";
            sSql_first += " AF_BOARDNAME, NVL(AF_EMPNO,'')AF_EMPNO ";
            sSql_first += " from WEBBOARD where af_boardname = '" + sBoardname + "' and af_check='0' ";
            sSql_first += " order by AF_NUMBER asc ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql_first);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            Board oBoard = new Board();
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                oBoard = new Board(Convert.ToInt32(oDT.Rows[i]["AF_IDEN"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_NUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_VINUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_RENUMBER"].ToString()),
                    oDT.Rows[i]["AF_ID"].ToString(), oDT.Rows[i]["AF_NAME"].ToString(), oDT.Rows[i]["AF_EMAIL"].ToString(), oDT.Rows[i]["AF_FILENAME"].ToString(), oDT.Rows[i]["AF_TITLE"].ToString(), oDT.Rows[i]["AF_DATE"].ToString(),
                    Convert.ToInt32(oDT.Rows[i]["AF_READ"].ToString()),
                    oDT.Rows[i]["AF_COMMENT"].ToString(), oDT.Rows[i]["AF_PASS"].ToString(), oDT.Rows[i]["AF_IP"].ToString(),
                    Convert.ToChar(oDT.Rows[i]["AF_CHECK"].ToString()),
                    Convert.ToInt32(oDT.Rows[i]["AF_REALNUMBER"].ToString()),
                    oDT.Rows[i]["AF_BOARDNAME"].ToString(), oDT.Rows[i]["AF_EMPNO"].ToString());
                rBoardlist.Add(oBoard);
            }

            oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            oDS = oDB.QueryDataSet(sSql); //이부분이 특히 느림
            oDB.Close();
            oDT = oDS.Tables[0];

            // 화면에 보여줄 iCurrentPage page에대한 list number를 계산한다. 
            // iListCount(쪽당 표시글 갯수) 값을 이용해서 상대적인 리스트 값을 계산한다. 
            int iRowsCount = oDT.Rows.Count;
            int iMaxListnum = iRowsCount - iListCount * (iCurrentPage - 1);
            int iminListnum = iMaxListnum - iListCount;
            if (iminListnum < 0)
                iminListnum = 0;

            oBoard = new Board();
            for (int i = iMaxListnum - 1; i >= iminListnum; i--)
            {
                oBoard = new Board(Convert.ToInt32(oDT.Rows[i]["AF_IDEN"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_NUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_VINUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_RENUMBER"].ToString()),
                    oDT.Rows[i]["AF_ID"].ToString(), oDT.Rows[i]["AF_NAME"].ToString(), oDT.Rows[i]["AF_EMAIL"].ToString(), oDT.Rows[i]["AF_FILENAME"].ToString(), oDT.Rows[i]["AF_TITLE"].ToString(), oDT.Rows[i]["AF_DATE"].ToString(),
                    Convert.ToInt32(oDT.Rows[i]["AF_READ"].ToString()),
                    oDT.Rows[i]["AF_COMMENT"].ToString(), oDT.Rows[i]["AF_PASS"].ToString(), oDT.Rows[i]["AF_IP"].ToString(),
                    Convert.ToChar(oDT.Rows[i]["AF_CHECK"].ToString()),
                    Convert.ToInt32(oDT.Rows[i]["AF_REALNUMBER"].ToString()),
                    oDT.Rows[i]["AF_BOARDNAME"].ToString(), oDT.Rows[i]["AF_EMPNO"].ToString());
                rBoardlist.Add(oBoard);
            }

            return rBoardlist;
        }

        //Getlist3(int iCurrentPage) 개선 using rownum from Justin
        public List<Board> Getlist3(int iCurrentPage)
        {
            List<Board> rBoardlist = new List<Board>();

            string sSql_first = " ";
            sSql_first += " select NVL(AF_IDEN,0)AF_IDEN, AF_NUMBER, NVL(AF_VINUMBER,0)AF_VINUMBER, NVL(AF_RENUMBER, 0) AF_RENUMBER, ";
            sSql_first += " AF_ID, AF_NAME, NVL(AF_EMAIL,'')AF_EMAIL, decode(NVL(AF_FILENAME,''),'','','파일있음') AF_FILENAME, AF_TITLE, AF_DATE, ";
            sSql_first += " AF_READ,AF_COMMENT,NVL(AF_PASS,'')AF_PASS,AF_IP,AF_CHECK,NVL(AF_REALNUMBER, 0) AF_REALNUMBER, ";
            sSql_first += " AF_BOARDNAME, NVL(AF_EMPNO,'')AF_EMPNO ";
            sSql_first += " from WEBBOARD where af_boardname = '" + sBoardname + "' and af_check='0' ";
            sSql_first += " order by AF_NUMBER asc ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql_first);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            Board oBoard = new Board();
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                oBoard = new Board(Convert.ToInt32(oDT.Rows[i]["AF_IDEN"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_NUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_VINUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_RENUMBER"].ToString()),
                    oDT.Rows[i]["AF_ID"].ToString(), oDT.Rows[i]["AF_NAME"].ToString(), oDT.Rows[i]["AF_EMAIL"].ToString(), oDT.Rows[i]["AF_FILENAME"].ToString(), oDT.Rows[i]["AF_TITLE"].ToString(), oDT.Rows[i]["AF_DATE"].ToString(),
                    Convert.ToInt32(oDT.Rows[i]["AF_READ"].ToString()),
                    oDT.Rows[i]["AF_COMMENT"].ToString(), oDT.Rows[i]["AF_PASS"].ToString(), oDT.Rows[i]["AF_IP"].ToString(),
                    Convert.ToChar(oDT.Rows[i]["AF_CHECK"].ToString()),
                    Convert.ToInt32(oDT.Rows[i]["AF_REALNUMBER"].ToString()),
                    oDT.Rows[i]["AF_BOARDNAME"].ToString(), oDT.Rows[i]["AF_EMPNO"].ToString());
                rBoardlist.Add(oBoard);
            }


            string sSql_rowCount = "";
            sSql_rowCount += " select count(*) rowcount ";
            sSql_rowCount += " from WEBBOARD where af_boardname = '" + sBoardname + "' and af_check in ('1','5') and AF_RENUMBER is null ";
            oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            oDS = oDB.QueryDataSet(sSql_rowCount);
            oDB.Close();
            oDT = oDS.Tables[0];
            // 화면에 보여줄 iCurrentPage page에대한 list number를 계산한다. 
            // iListCount(쪽당 표시글 갯수) 값을 이용해서 상대적인 리스트 값을 계산한다. 
            int iRowsCount = Convert.ToInt32(oDT.Rows[0]["rowcount"].ToString());
            int iMaxListnum = iRowsCount - iListCount * (iCurrentPage - 1);
            int iminListnum = iMaxListnum - iListCount;
            if (iminListnum < 0)
                iminListnum = 0;

            string sSql = " ";
            sSql += " SELECT * FROM ( SELECT A.*, ROWNUM RNUM FROM ( ";
            sSql += " select NVL(AF_IDEN,0)AF_IDEN, AF_NUMBER, NVL(AF_VINUMBER,0)AF_VINUMBER, NVL(AF_RENUMBER, 0) AF_RENUMBER, ";
            sSql += " AF_ID, AF_NAME, NVL(AF_EMAIL,'')AF_EMAIL, decode(NVL(AF_FILENAME,''),'','','파일있음') AF_FILENAME, AF_TITLE, AF_DATE, ";
            sSql += " AF_READ,AF_COMMENT,NVL(AF_PASS,'')AF_PASS,AF_IP,AF_CHECK,NVL(AF_REALNUMBER, 0) AF_REALNUMBER, ";
            sSql += " AF_BOARDNAME, NVL(AF_EMPNO,'')AF_EMPNO ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' and af_check in ('1','5') and AF_RENUMBER is null ";
            sSql += " order by AF_NUMBER asc ";
            sSql += " ) A ";
            sSql += " WHERE ROWNUM <= " + iMaxListnum.ToString() + " ) ";
            sSql += " WHERE RNUM > " + iminListnum.ToString() + " ";
            sSql += " order by AF_REALNUMBER asc ";

            oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            oDT = oDS.Tables[0];

            oBoard = new Board();
            for (int i = oDT.Rows.Count - 1; i >= 0; i--)
            {
                oBoard = new Board(Convert.ToInt32(oDT.Rows[i]["AF_IDEN"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_NUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_VINUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_RENUMBER"].ToString()),
                    oDT.Rows[i]["AF_ID"].ToString(), oDT.Rows[i]["AF_NAME"].ToString(), oDT.Rows[i]["AF_EMAIL"].ToString(), oDT.Rows[i]["AF_FILENAME"].ToString(), oDT.Rows[i]["AF_TITLE"].ToString(), oDT.Rows[i]["AF_DATE"].ToString(),
                    Convert.ToInt32(oDT.Rows[i]["AF_READ"].ToString()),
                    oDT.Rows[i]["AF_COMMENT"].ToString(), oDT.Rows[i]["AF_PASS"].ToString(), oDT.Rows[i]["AF_IP"].ToString(),
                    Convert.ToChar(oDT.Rows[i]["AF_CHECK"].ToString()),
                    Convert.ToInt32(oDT.Rows[i]["AF_REALNUMBER"].ToString()),
                    oDT.Rows[i]["AF_BOARDNAME"].ToString(), oDT.Rows[i]["AF_EMPNO"].ToString());
                rBoardlist.Add(oBoard);
            }

            return rBoardlist;
        }//Getlist3(int iPage) 개선 끝.


        public List<Board> Getlist_Search(int iCurrentPage, String category, String keyword)
        {
            List<Board> rBoardlist = new List<Board>();
            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            Board oBoard = new Board();

            string sSql_rowCount = "";
            //sSql_rowCount += " select count(*) rowcount ";
            //sSql_rowCount += " from WEBBOARD where af_boardname = '" + sBoardname + "' and af_check='1' and AF_RENUMBER is null ";

            sSql_rowCount += " select count(*) rowcount";
            sSql_rowCount += " from WEBBOARD ";
            sSql_rowCount += " where af_boardname = '" + sBoardname + "' and af_check='1' and " + category + " like '%" + keyword + "%' or af_check='0'  and " + category + " like '%" + keyword + "%'"; // 검색 조건
            sSql_rowCount += " order by AF_NUMBER asc";

            oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql_rowCount);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            // 화면에 보여줄 iCurrentPage page에대한 list number를 계산한다. 
            // iListCount(쪽당 표시글 갯수) 값을 이용해서 상대적인 리스트 값을 계산한다. 
            int iRowsCount = Convert.ToInt32(oDT.Rows[0]["rowcount"].ToString());
            int iMaxListnum = iRowsCount - iListCount * (iCurrentPage - 1);
            int iminListnum = iMaxListnum - iListCount;
            if (iminListnum < 0)
                iminListnum = 0;

            string sSql = " ";
            sSql += " SELECT * FROM ( SELECT A.*, ROWNUM RNUM FROM ( ";
            sSql += " select NVL(AF_IDEN,0)AF_IDEN, AF_NUMBER, NVL(AF_VINUMBER,0)AF_VINUMBER, NVL(AF_RENUMBER, 0) AF_RENUMBER, ";
            sSql += " AF_ID, AF_NAME, NVL(AF_EMAIL,'')AF_EMAIL, NVL(AF_FILENAME,'')AF_FILENAME, AF_TITLE, AF_DATE, ";
            sSql += " AF_READ,AF_COMMENT,NVL(AF_PASS,'')AF_PASS,AF_IP,AF_CHECK,NVL(AF_REALNUMBER, 0) AF_REALNUMBER, ";
            sSql += " AF_BOARDNAME, NVL(AF_EMPNO,'')AF_EMPNO ";
            sSql += " from WEBBOARD ";
            sSql += " where af_boardname = '" + sBoardname + "' and af_check='1' and " + category + " like '%" + keyword + "%' or af_check='0'  and " + category + " like '%" + keyword + "%'"; // 검색 조건
            sSql += " order by AF_NUMBER asc ";
            sSql += " ) A ";
            sSql += " WHERE ROWNUM <= " + iMaxListnum.ToString() + " ) ";
            sSql += " WHERE RNUM > " + iminListnum.ToString() + " ";
            sSql += " order by AF_REALNUMBER asc ";

            oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            oDT = oDS.Tables[0];

            oBoard = new Board();
            for (int i = oDT.Rows.Count - 1; i >= 0; i--)
            {
                oBoard = new Board(Convert.ToInt32(oDT.Rows[i]["AF_IDEN"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_NUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_VINUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_RENUMBER"].ToString()),
                    oDT.Rows[i]["AF_ID"].ToString(), oDT.Rows[i]["AF_NAME"].ToString(), oDT.Rows[i]["AF_EMAIL"].ToString(), oDT.Rows[i]["AF_FILENAME"].ToString(), oDT.Rows[i]["AF_TITLE"].ToString(), oDT.Rows[i]["AF_DATE"].ToString(),
                    Convert.ToInt32(oDT.Rows[i]["AF_READ"].ToString()),
                    oDT.Rows[i]["AF_COMMENT"].ToString(), oDT.Rows[i]["AF_PASS"].ToString(), oDT.Rows[i]["AF_IP"].ToString(),
                    Convert.ToChar(oDT.Rows[i]["AF_CHECK"].ToString()),
                    Convert.ToInt32(oDT.Rows[i]["AF_REALNUMBER"].ToString()),
                    oDT.Rows[i]["AF_BOARDNAME"].ToString(), oDT.Rows[i]["AF_EMPNO"].ToString());
                rBoardlist.Add(oBoard);
            }


            return rBoardlist;

        }

        //Getlist2(int iCurrentPage) 개선.
        public List<Board> Getlist2(int iCurrentPage)
        {
            List<Board> rBoardlist = new List<Board>();

            string sSql_first = " ";
            sSql_first += " select NVL(AF_IDEN,0)AF_IDEN, AF_NUMBER, NVL(AF_VINUMBER,0)AF_VINUMBER, NVL(AF_RENUMBER, 0) AF_RENUMBER, ";
            sSql_first += " AF_ID, AF_NAME, NVL(AF_EMAIL,'')AF_EMAIL, NVL(AF_FILENAME,'')AF_FILENAME, AF_TITLE, AF_DATE, ";
            sSql_first += " AF_READ,AF_COMMENT,NVL(AF_PASS,'')AF_PASS,AF_IP,AF_CHECK,NVL(AF_REALNUMBER, 0) AF_REALNUMBER, ";
            sSql_first += " AF_BOARDNAME, NVL(AF_EMPNO,'')AF_EMPNO ";
            sSql_first += " from WEBBOARD where af_boardname = '" + sBoardname + "' and af_check='0' ";
            sSql_first += " order by AF_NUMBER asc ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql_first);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            Board oBoard = new Board();
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                oBoard = new Board(Convert.ToInt32(oDT.Rows[i]["AF_IDEN"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_NUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_VINUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_RENUMBER"].ToString()),
                    oDT.Rows[i]["AF_ID"].ToString(), oDT.Rows[i]["AF_NAME"].ToString(), oDT.Rows[i]["AF_EMAIL"].ToString(), oDT.Rows[i]["AF_FILENAME"].ToString(), oDT.Rows[i]["AF_TITLE"].ToString(), oDT.Rows[i]["AF_DATE"].ToString(),
                    Convert.ToInt32(oDT.Rows[i]["AF_READ"].ToString()),
                    oDT.Rows[i]["AF_COMMENT"].ToString(), oDT.Rows[i]["AF_PASS"].ToString(), oDT.Rows[i]["AF_IP"].ToString(),
                    Convert.ToChar(oDT.Rows[i]["AF_CHECK"].ToString()),
                    Convert.ToInt32(oDT.Rows[i]["AF_REALNUMBER"].ToString()),
                    oDT.Rows[i]["AF_BOARDNAME"].ToString(), oDT.Rows[i]["AF_EMPNO"].ToString());
                rBoardlist.Add(oBoard);
            }


            string sSql_rowCount = "";
            sSql_rowCount += " select count(*) rowcount ";
            sSql_rowCount += " from WEBBOARD where af_boardname = '" + sBoardname + "' and af_check='1' and AF_RENUMBER is null ";
            oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            oDS = oDB.QueryDataSet(sSql_rowCount);
            oDB.Close();
            oDT = oDS.Tables[0];
            // 화면에 보여줄 iCurrentPage page에대한 list number를 계산한다. 
            // iListCount(쪽당 표시글 갯수) 값을 이용해서 상대적인 리스트 값을 계산한다. 
            int iRowsCount = Convert.ToInt32(oDT.Rows[0]["rowcount"].ToString());
            int iMaxListnum = iRowsCount - iListCount * (iCurrentPage - 1);
            int iminListnum = iMaxListnum - iListCount;
            if (iminListnum < 0)
                iminListnum = 0;

            string sSql = " ";
            sSql += " select NVL(AF_IDEN,0)AF_IDEN, AF_NUMBER, NVL(AF_VINUMBER,0)AF_VINUMBER, NVL(AF_RENUMBER, 0) AF_RENUMBER, ";
            sSql += " AF_ID, AF_NAME, NVL(AF_EMAIL,'')AF_EMAIL, NVL(AF_FILENAME,'')AF_FILENAME, AF_TITLE, AF_DATE, ";
            sSql += " AF_READ,AF_COMMENT,NVL(AF_PASS,'')AF_PASS,AF_IP,AF_CHECK,NVL(AF_REALNUMBER, 0) AF_REALNUMBER, ";
            sSql += " AF_BOARDNAME, NVL(AF_EMPNO,'')AF_EMPNO ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' and af_check='1' and AF_RENUMBER is null ";
            sSql += " and AF_REALNUMBER > " + iminListnum.ToString() + " ";
            sSql += " and AF_REALNUMBER <= " + iMaxListnum.ToString() + " ";
            sSql += " order by AF_NUMBER asc ";

            oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            oDT = oDS.Tables[0];

            oBoard = new Board();
            for (int i = oDT.Rows.Count - 1; i >= 0; i--)
            {
                oBoard = new Board(Convert.ToInt32(oDT.Rows[i]["AF_IDEN"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_NUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_VINUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_RENUMBER"].ToString()),
                    oDT.Rows[i]["AF_ID"].ToString(), oDT.Rows[i]["AF_NAME"].ToString(), oDT.Rows[i]["AF_EMAIL"].ToString(), oDT.Rows[i]["AF_FILENAME"].ToString(), oDT.Rows[i]["AF_TITLE"].ToString(), oDT.Rows[i]["AF_DATE"].ToString(),
                    Convert.ToInt32(oDT.Rows[i]["AF_READ"].ToString()),
                    oDT.Rows[i]["AF_COMMENT"].ToString(), oDT.Rows[i]["AF_PASS"].ToString(), oDT.Rows[i]["AF_IP"].ToString(),
                    Convert.ToChar(oDT.Rows[i]["AF_CHECK"].ToString()),
                    Convert.ToInt32(oDT.Rows[i]["AF_REALNUMBER"].ToString()),
                    oDT.Rows[i]["AF_BOARDNAME"].ToString(), oDT.Rows[i]["AF_EMPNO"].ToString());
                rBoardlist.Add(oBoard);
            }

            return rBoardlist;
        }//Getlist2(int iPage) 개선 끝.

        public List<Board> Getlist()
        {
            List<Board> rBoardlist = new List<Board>();

            string sSql = " select NVL(AF_IDEN,0)AF_IDEN, AF_NUMBER, NVL(AF_VINUMBER,0)AF_VINUMBER, NVL(AF_RENUMBER, 0) AF_RENUMBER, ";
            sSql += " AF_ID, AF_NAME, NVL(AF_EMAIL,'')AF_EMAIL, NVL(AF_FILENAME,'')AF_FILENAME, AF_TITLE, AF_DATE, ";
            sSql += " AF_READ,AF_COMMENT,NVL(AF_PASS,'')AF_PASS,AF_IP,AF_CHECK,NVL(AF_REALNUMBER, 0) AF_REALNUMBER, ";
            sSql += " AF_BOARDNAME, NVL(AF_EMPNO,'')AF_EMPNO ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' and af_check='1' and AF_RENUMBER is null order by AF_NUMBER asc ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];

            // 화면에 보여줄 iCurrentPage page에대한 list number를 계산한다. 
            // iListCount(쪽당 표시글 갯수) 값을 이용해서 상대적인 리스트 값을 계산한다. 

            Board oBoard = new Board();
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                oBoard = new Board(Convert.ToInt32(oDT.Rows[i]["AF_IDEN"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_NUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_VINUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_RENUMBER"].ToString()),
                    oDT.Rows[i]["AF_ID"].ToString(), oDT.Rows[i]["AF_NAME"].ToString(), oDT.Rows[i]["AF_EMAIL"].ToString(), oDT.Rows[i]["AF_FILENAME"].ToString(), oDT.Rows[i]["AF_TITLE"].ToString(), oDT.Rows[i]["AF_DATE"].ToString(),
                    Convert.ToInt32(oDT.Rows[i]["AF_READ"].ToString()),
                    oDT.Rows[i]["AF_COMMENT"].ToString(), oDT.Rows[i]["AF_PASS"].ToString(), oDT.Rows[i]["AF_IP"].ToString(),
                    Convert.ToChar(oDT.Rows[i]["AF_CHECK"].ToString()),
                    Convert.ToInt32(oDT.Rows[i]["AF_REALNUMBER"].ToString()),
                    oDT.Rows[i]["AF_BOARDNAME"].ToString(), oDT.Rows[i]["AF_EMPNO"].ToString());
                rBoardlist.Add(oBoard);
            }

            return rBoardlist;
        }
        public List<Board> GetlistTop10()
        {
            List<Board> rBoardlist = new List<Board>();



            string sSql = " select NVL(AF_IDEN,0)AF_IDEN, AF_NUMBER, NVL(AF_VINUMBER,0)AF_VINUMBER, NVL(AF_RENUMBER, 0) AF_RENUMBER, ";
            sSql += " AF_ID, AF_NAME, NVL(AF_EMAIL,'')AF_EMAIL, NVL(AF_FILENAME,'')AF_FILENAME, AF_TITLE, AF_DATE, ";
            sSql += " AF_READ,AF_COMMENT,NVL(AF_PASS,'')AF_PASS,AF_IP,AF_CHECK,NVL(AF_REALNUMBER, 0) AF_REALNUMBER, ";
            sSql += " AF_BOARDNAME, NVL(AF_EMPNO,'')AF_EMPNO ";

            sSql += " from( select NVL(AF_IDEN,0)AF_IDEN, AF_NUMBER, NVL(AF_VINUMBER,0)AF_VINUMBER, NVL(AF_RENUMBER, 0) AF_RENUMBER, ";
            sSql += " AF_ID, AF_NAME, NVL(AF_EMAIL,'')AF_EMAIL, NVL(AF_FILENAME,'')AF_FILENAME, AF_TITLE, AF_DATE, ";
            sSql += " AF_READ,AF_COMMENT,NVL(AF_PASS,'')AF_PASS,AF_IP,AF_CHECK,NVL(AF_REALNUMBER, 0) AF_REALNUMBER, ";
            sSql += " AF_BOARDNAME, NVL(AF_EMPNO,'')AF_EMPNO ";
            sSql += " from WEBBOARD ";
            sSql += " where af_boardname = '" + sBoardname + "' and af_check='1' and AF_RENUMBER is null order by AF_NUMBER desc ";
            sSql += " ) ";
            sSql += " where rownum < 11 ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];

            // 화면에 보여줄 iCurrentPage page에대한 list number를 계산한다. 
            // iListCount(쪽당 표시글 갯수) 값을 이용해서 상대적인 리스트 값을 계산한다. 

            string strRegexp = "<(/)?([a-zA-Z]*)(\\s[a-zA-Z]*=[^>]*)?(\\s)*(/)?>"; // 태그를 없애는 정규식

            Regex reg = new Regex(strRegexp);


            Board oBoard = new Board();
            for (int i = 0; i < oDT.Rows.Count; i++)
            {
                string sTitle = reg.Replace(oDT.Rows[i]["AF_TITLE"].ToString(), "");
                string sDate = reg.Replace(oDT.Rows[i]["AF_DATE"].ToString(), "");
                string sComment = reg.Replace(oDT.Rows[i]["AF_COMMENT"].ToString(), "");

                oBoard = new Board(Convert.ToInt32(oDT.Rows[i]["AF_IDEN"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_NUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_VINUMBER"].ToString()), Convert.ToInt32(oDT.Rows[i]["AF_RENUMBER"].ToString()),
                    oDT.Rows[i]["AF_ID"].ToString(), oDT.Rows[i]["AF_NAME"].ToString(), oDT.Rows[i]["AF_EMAIL"].ToString(), oDT.Rows[i]["AF_FILENAME"].ToString(), sTitle, sDate,
                    Convert.ToInt32(oDT.Rows[i]["AF_READ"].ToString()),
                    sComment, oDT.Rows[i]["AF_PASS"].ToString(), oDT.Rows[i]["AF_IP"].ToString(),
                    Convert.ToChar(oDT.Rows[i]["AF_CHECK"].ToString()),
                    Convert.ToInt32(oDT.Rows[i]["AF_REALNUMBER"].ToString()),
                    oDT.Rows[i]["AF_BOARDNAME"].ToString(), oDT.Rows[i]["AF_EMPNO"].ToString());
                rBoardlist.Add(oBoard);
            }

            return rBoardlist;
        }
        //해당 글의 이전(윗쪽) 글의 af_iNumber를 넘겨줌.
        //기능 개선이 필요함.!
        public int GetPreBoardnumber(int _iNumber)
        {
            int iReturn = 0;

            string sSql = " select NVL(min(af_number),0) prenumber ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' ";
            sSql += " and af_check in ('0','1') ";
            sSql += " and af_number > " + _iNumber.ToString() + " ";
            sSql += " order by AF_VINUMBER desc, AF_NUMBER ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            iReturn = Convert.ToInt32(oDT.Rows[0]["prenumber"].ToString());

            return iReturn;
        }
        //해당 글의 다음(아랫쪽) 글의 af_iNumber를 넘겨줌.
        //기능 개선이 필요함.!
        public int GetNextBoardnumber(int _iNumber)
        {
            int iReturn = 0;

            string sSql = " select NVL(MAX(af_number),0) nextnumber ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' ";
            sSql += " and af_check in ('0','1') ";
            sSql += " and af_number < " + _iNumber.ToString() + " ";
            sSql += " order by AF_VINUMBER desc, AF_NUMBER ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            iReturn = Convert.ToInt32(oDT.Rows[0]["nextnumber"].ToString());

            return iReturn;
        }
        //해당 iNumber에대한 Currentpage 를 넘겨줌.
        public int GetPreBoardnumber_Search(int _iNumber, String category, String keyword)
        {
            int iReturn = 0;

            string sSql = " select NVL(min(af_number),0) prenumber ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' ";
            sSql += " and " + category + " like '%" + keyword + "%'";
            sSql += " and af_check in ('0','1') ";
            sSql += " and af_number > " + _iNumber.ToString() + " ";
            sSql += " order by AF_VINUMBER desc, AF_NUMBER ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            iReturn = Convert.ToInt32(oDT.Rows[0]["prenumber"].ToString());

            return iReturn;
        }

        public int GetNextBoardnumber_Search(int _iNumber, String category, String keyword)
        {
            int iReturn = 0;

            string sSql = " select NVL(MAX(af_number),0) nextnumber ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' ";
            sSql += " and " + category + " like '%" + keyword + "%'";
            sSql += " and af_check in ('0','1') ";
            sSql += " and af_number < " + _iNumber.ToString() + " ";
            sSql += " order by AF_VINUMBER desc, AF_NUMBER ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            iReturn = Convert.ToInt32(oDT.Rows[0]["nextnumber"].ToString());

            return iReturn;
        }
        //해당 iNumber에대한 Currentpage 를 넘겨줌.
        public int GetCurrentPagenumber(int _iNumber)
        {
            int iReturn = 0;

            string sSql = " select NVL(count(af_number),0) overcount ";
            sSql += " from WEBBOARD where af_boardname = '" + sBoardname + "' ";
            sSql += " and af_check in ('0','1') and AF_RENUMBER is null ";
            sSql += " and af_number >= " + _iNumber.ToString() + " ";

            clsDBControl oDB = new clsDBControl(clsConst.DBPROVIDER.SCM);
            DataSet oDS = oDB.QueryDataSet(sSql);
            oDB.Close();
            DataTable oDT = oDS.Tables[0];
            int iOvercount = 0; //현재 글포함 최근 글의 갯수. 
            iOvercount = Convert.ToInt32(oDT.Rows[0]["overcount"].ToString());

            //page 를 iListCount을 반영해 보여준다. 
            int iCurrentpage = 0; //현재 글이 포함되어있는 페이지.
            iCurrentpage = iOvercount / iListCount;
            if (iOvercount % iListCount != 0 || iCurrentpage == 0)
                iCurrentpage++;
            iReturn = iCurrentpage;

            return iReturn;
        }

    }


}