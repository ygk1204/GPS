using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASEWCFServiceLibrary.App_Code;
using System.Data.OleDb;

namespace GPS201107.Models
{
    public class WEB_MAILSENDHISTORY
    {
        public string _TRANSACTIONID { get; set; }
        public string _TOMAIL { get; set; }
        public string _SUBJECT { get; set; }
        public string _SERVICENAME { get; set; }
        public string _SENDFLAG { get; set; }
        public string _SENDDATE { get; set; }
        public string _REQID { get; set; }
        public string _MESSAGE { get; set; }
        public string _FROMMAIL { get; set; }
        public string _CREATEDATE { get; set; }
        public string _CCMAIL { get; set; }

        public WEB_MAILSENDHISTORY()
        {

        }

        public string SetMailBodyHazardous(dynamic oMaterial, string subject, string _UserRole, string status)
        {

            string sAddress = string.Empty;
            sAddress = clsConst.Get_Site_Address() + "Hazardous/HazardousMaterialReportDetail/" + oMaterial._HMREQID;
            string sHTMLstr = string.Empty;


            sHTMLstr += " <p style='font-weight:bold;'> 신청 번호 : &nbsp;<a href='" + sAddress + "'>" + oMaterial._HMREQID.ToString() + "</a></p> ";

            // 문서 상태가 Open, Process 일 경우
            if (status == "Modify" && oMaterial._STATUS == "Open" || oMaterial._STATUS == "Process")
            {
                USER_LIST updater = USER_LIST.GetUserList(oMaterial._MODIFYUSER);
                sHTMLstr += " <p style='font-weight:bold;'>" + updater._K_NM + "님이 수정하셨습니다. </p> ";
            }

           // 문서 상태가 Close일 경우
            else if (status == "Close" && oMaterial._STATUS == "Close")
            {
                USER_LIST updater = USER_LIST.GetUserList(oMaterial._ADMINUSERID);
                sHTMLstr += " <p style='font-weight:bold;'>" + updater._K_NM + "님이 최종 완료하셨습니다. </p> ";
            }

            //요청 아이디, 고객사, 요청자 사번, 요청자 이름, 유해물질 항목, 요청 내용, 상태

            if (status == "Request" || status == "Modify" || status == "Close")
            {
                sHTMLstr += "  <table style='width:800px;  border:1px solid;border-collapse: collapse; '>    ";
                sHTMLstr += "  	<tbody>    ";
                sHTMLstr += "  		<tr>    ";
                sHTMLstr += "  			<td style='background-color: #dff0d8;width:150px;border: 1px solid;padding:5px 5px 5px 5px; text-align:center;font-weight:bold;'>요청자 사번</td>    ";
                sHTMLstr += "  			<td style='width:300px;border: 1px solid;padding:5px 5px 5px 5px;'>" + oMaterial._REQUESTUSERID.ToString() + "</td>    ";
                sHTMLstr += "           <td style='background-color: #dff0d8;width:150px;border: 1px solid;padding:5px 5px 5px 5px; text-align:center;font-weight:bold;'>요청자 이름</td>    ";
                sHTMLstr += "  			<td style='width:300px;border: 1px solid;padding:5px 5px 5px 5px;'>" + oMaterial._REQUESTUSERNAME.ToString() + "</td>    ";

                sHTMLstr += "  		</tr>    ";
                sHTMLstr += "      ";

                sHTMLstr += "  		<tr>    ";
                sHTMLstr += "  			<td style='background-color: #dff0d8;width:150px;border: 1px solid;padding:5px 5px 5px 5px; text-align:center;font-weight:bold;'>요청 아이디</td>    ";
                sHTMLstr += "  			<td style='width:300px;border: 1px solid;padding:5px 5px 5px 5px;'>" + oMaterial._HMREQID.ToString() + "</td>    ";

                if (oMaterial._STATUS != null)
                {
                    sHTMLstr += "           <td style='background-color: #dff0d8;width:150px;border: 1px solid;padding:5px 5px 5px 5px; text-align:center;font-weight:bold;'>상태</td>    ";
                    sHTMLstr += "  			<td style='width:300px;border: 1px solid;padding:5px 5px 5px 5px;'>" + oMaterial._STATUS.ToString() + "</td>    ";
                }

                sHTMLstr += "  		</tr>    ";
                sHTMLstr += "      ";

                sHTMLstr += "  		<tr>    ";

                sHTMLstr += "           <td style='background-color: #dff0d8;width:150px;border: 1px solid;padding:5px 5px 5px 5px; text-align:center;font-weight:bold;'>고객사</td>    ";
                sHTMLstr += "  			<td style='width:300px;border: 1px solid;padding:5px 5px 5px 5px;'>" + oMaterial._CUSTOMER.ToString() + "</td>    ";
                sHTMLstr += "           <td style='background-color: #dff0d8;width:150px;border: 1px solid;padding:5px 5px 5px 5px; text-align:center;font-weight:bold;'>제품</td>    ";
                sHTMLstr += "  			<td style='width:300px;border: 1px solid;padding:5px 5px 5px 5px;'>" + oMaterial._PRODUCT.ToString() + "</td>    ";

                sHTMLstr += "  		</tr>    ";
                sHTMLstr += "      ";


                sHTMLstr += "  		<tr>    ";

                sHTMLstr += "           <td style='background-color: #dff0d8;width:150px;border: 1px solid;padding:5px 5px 5px 5px; text-align:center;font-weight:bold;'>유해물질 항목</td>    ";
                sHTMLstr += "  			<td style='width:300px;border: 1px solid;padding:5px 5px 5px 5px;' colspan='3'>" + oMaterial._HAZARDOUSMATERIALTYPE.ToString() + "</td>    ";
                sHTMLstr += "  		</tr>    ";
                sHTMLstr += "      ";

                sHTMLstr += "        <tr>    ";
                sHTMLstr += "           <td style='background-color: #dff0d8;width:150px;border: 1px solid;padding:5px 5px 5px 5px; text-align:center;font-weight:bold;'>요청 내용</td>    ";
                sHTMLstr += "  			<td style='width:300px;border: 1px solid;padding:5px 5px 5px 5px;' colspan='3'>" + oMaterial._REQUESTCOMMENT.ToString() + "</td>    ";
                sHTMLstr += "  		</tr>    ";
                sHTMLstr += "      ";

                sHTMLstr += "  </tbody>    ";
                sHTMLstr += "  </table>    ";

                sHTMLstr += "  <br/> ";
            }


            if (status == "Modify" || status == "Close" && _UserRole == "Admin")
            {
                //관리자가 수정, 최종완료 했을 경우
                if (oMaterial._ADMINUSERID != null && oMaterial._EXPECTEDFINISHDATE != null)
                {
                sHTMLstr += "  <table style='width:800px;  border:1px solid;border-collapse: collapse; '>    ";
                sHTMLstr += "  	<tbody>    ";
                sHTMLstr += "  		<tr>    ";
                sHTMLstr += "  			<td style='background-color: #dff0d8;width:150px;border: 1px solid;padding:5px 5px 5px 5px; text-align:center;font-weight:bold;'>책임자</td>    ";
                    sHTMLstr += "  			<td style='width:300px;border: 1px solid;padding:5px 5px 5px 5px;'>" + oMaterial._ADMINUSERID.ToString() + "</td>    ";



                    sHTMLstr += "           <td style='background-color: #dff0d8;width:150px;border: 1px solid;padding:5px 5px 5px 5px; text-align:center;font-weight:bold;'>예상 완료일</td>    ";
                    sHTMLstr += "  			<td style='width:300px;border: 1px solid;padding:5px 5px 5px 5px;'>" + oMaterial._EXPECTEDFINISHDATE.ToString() + "</td>    ";

                sHTMLstr += "  		</tr>    ";
                sHTMLstr += "      ";
                sHTMLstr += "  	</tbody>    ";
                sHTMLstr += "  </table>    ";
                }
            }

            //대체 문자
            sHTMLstr = sHTMLstr.Replace("'", "''");
            return sHTMLstr;
        }

        private string ConvertMailAddress(string email)
        {
            if (!String.IsNullOrWhiteSpace(email) && email.IndexOf('@') <= 0)
            {
                email = email + "@asekr.com";
            }
            return email;
        }

        public bool SaveMailData(OleDbCommand cmd)
        {
            bool bReturn = false;
            string sQuery = "";
            try
            {
                sQuery += " INSERT INTO WEB_MAILSENDHISTORY ( TRANSACTIONID, TOMAIL, SUBJECT, SERVICENAME, SENDFLAG, REQID, ";
                sQuery += " MESSAGE, FROMMAIL, CREATEDATE ) ";
                sQuery += " VALUES  ( TO_CHAR(SYSDATE, 'yymmddhh24miss') || SEQ_MAILSEQ.nextval  , '" + _TOMAIL + "', '" + _SUBJECT + "'," + "'";
                sQuery += _SERVICENAME + "', '" + _SENDFLAG + "'," + "'";
                sQuery += _REQID + "', '" + _MESSAGE.Replace("'", "''") + "', '" + _FROMMAIL + "', to_char(systimestamp, 'yyyymmdd hh24missff3') ) ";
                cmd.CommandText = sQuery;
                cmd.ExecuteNonQuery();
                bReturn = true;
            }
            catch (Exception e)
            {   
                throw e;
            }
            return bReturn;
        }
    }

}
