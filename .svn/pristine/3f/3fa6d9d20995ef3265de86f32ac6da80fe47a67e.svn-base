using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPS201107.Models.HazardousRequest;
using System.Data.OleDb;

namespace GPS201107.Models
{
    public class GpsMail
    {
        private GpsHmRequest oRequestInfo = null;
        String style = "<style type='text/css'>th{background-color: #dff0d8; border: 1px solid; padding: 5px 5px 5px 5px; text-align: center; font-weight: bold;}td { border: 1px solid; padding: 5px 5px 5px 5px;}p{font-weight: bold;}table {width: 800px; border: 1px solid; border-collapse: collapse;}</style>";
        public String url;
        public String headerContent;
        public string toMail {get;set;}
        public string ccMail {get;set;}
        
        public GpsMail(GPS201107.Models.HazardousRequest.GpsHmRequest request, string toMail, string ccMail)
        {
            this.oRequestInfo = request;
            this.toMail = toMail;
            this.ccMail = ccMail;
        }
      /// <summary>
      /// Html 문서를 만듬.
      /// MakeMainContents() + MakeDataTable() 의 앞 뒤에 <html><header> <style>  </style> </header></html> 를 완성시킨다.
      /// </summary>
      /// <returns></returns>
      private String MakeMailBody()
      {
          String mailBody = "<html lang='ko'><header><meta charset='utf-8'>" + this.style + "</header><body> ";

          mailBody += MakeMainContents() + MakeDataTable() + "</body> </html>";
          return mailBody;
      }

      public GPS201107.Models.WEB_MAILSENDHISTORY MakeWebMailSendHistory(String sSubject)
      {

          GPS201107.Models.WEB_MAILSENDHISTORY oWebMailSendHistory = new GPS201107.Models.WEB_MAILSENDHISTORY();
          oWebMailSendHistory._CCMAIL = "";
          oWebMailSendHistory._TOMAIL = toMail;
          oWebMailSendHistory._REQID = oRequestInfo._HMREQID;
          oWebMailSendHistory._FROMMAIL = "GPS@asekr.com";
          oWebMailSendHistory._MESSAGE = MakeMailBody();
          oWebMailSendHistory._SENDFLAG = "NO";
          oWebMailSendHistory._SERVICENAME = "GPS";
          oWebMailSendHistory._SUBJECT = sSubject;

          //Validate tomail If invalid mail address , return null; 
          if (oWebMailSendHistory._TOMAIL == null || oWebMailSendHistory._TOMAIL.Length <= 0)
              oWebMailSendHistory = null;

          return oWebMailSendHistory;
      }

      private String MakeMainContents()
      {
          String bodyHeader = "";
          bodyHeader += "<p > 유해물질 자료 요청 발신 전용 메일입니다. </p><br>";          
          
          return bodyHeader;
      }

      /// <summary>
      /// dicMailData 를 이용하여 <table><tr><th></th><td></td><th></th><td></td></tr></table> Html을 만들어 반환한다.
      /// </summary>
      /// <returns></returns>
      private String MakeDataTable()
      {
          String dataTable = "<table><tbody><tr><th width='200'>항목</th><th width='500'>내용</th> </tr>";
     
              dataTable = dataTable + "<tr><th>요청 ID</th><td>" + oRequestInfo._HMREQID+ "</td>";
              dataTable = dataTable + "<tr><th>상태</th><td>" + oRequestInfo._STATUS+ "</td>";
              dataTable = dataTable + "<tr><th>요청날짜</th><td>" + oRequestInfo._REQUESTDATE+ "</td>";
              dataTable = dataTable + "<tr><th>요청자</th><td>" + oRequestInfo._REQUESTUSERID +"/"+ oRequestInfo._REQUESTUSERNAME+ "</td>";
              dataTable = dataTable + "<tr><th>고객사</th><td>" + oRequestInfo._CUSTOMER+ "</td>";
              dataTable = dataTable + "<tr><th>제품</th><td>" + oRequestInfo._PRODUCT+ "</td>";
              
              dataTable = dataTable + "<tr><th>요청내용</th><td>" + oRequestInfo._REQUESTCOMMENT+ "</td>";             
              
              dataTable = dataTable + "<tr><th>유해물질 항목</th><td>" + oRequestInfo._HAZARDOUSMATERIALTYPE+ "</td>";
              dataTable = dataTable + "<tr><th>예상완료일</th><td>" + oRequestInfo._EXPECTEDFINISHDATE + "</td>";         
              dataTable = dataTable + "</tr>";

          dataTable = dataTable + "</tbody></table>";
          return dataTable;
      }
    }
}