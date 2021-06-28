<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" ValidateRequest="false"%>
<%@ Import Namespace="GPS201107.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link href="../../../../Content/board.css" rel="stylesheet" type="text/css" />

    <% var sId = ViewData["id"].ToString(); %>
    <% var category = ViewData["category"].ToString(); %>
    <% var keyword = ViewData["keyword"].ToString(); %>
    <% var page = ViewData["page"].ToString(); %>
    

    <div id="contentsWrap">
      
      <h1><%=sId %>&nbsp;내용</h1>
      <div id="contentsArea">
        <div id="bo_read_contents">
            <% var oBoard = (Board)ViewData["board"];%>
           <table class="bo_read_table">
        <tr>
            <td class="bo_read_title"><%=oBoard.sTitle%></td>
        </tr>
        <tr>
            <td class="bo_read_info">
                <%=oBoard.sName%>
                <span style="color:Black">|
                조회 <%=oBoard.iRead.ToString()%>|<img src="/Content/images/board/calendar.gif" /><%=oBoard.sDate%></span>
                <div style="width: 120px; float :right;">
                    <img src="/Content/images/board/iconFile.gif" />
                    <%if (oBoard.sFilename == "")
                      { %>
                        첨부파일없음
                    <%}
                      else
                      {%> 
                        <a href="/board/Download/<%=oBoard.sBoardname %>/<%=oBoard.iNumber.ToString()%>/<%=oBoard.sFilename %>" ><%=oBoard.sFilename%></a>
                    <%} %>
                </div>
            </td>
        </tr>
             
        <tr>
            <td class="bo_read_content">
             <%=oBoard.sComment%></td>
        </tr>
    </table>
        </div>
        <div id="bo_read_menu">
            <% var sBoardname = (string)ViewData["boardname"];
               var iListnumber = (int)ViewData["listnumber"];
               var iCurrentpage = (int)ViewData["currentpage"];  
               var iPreview = (int)ViewData["preview"];
               var iNextview = (int)ViewData["nextview"]; 
            %>
            <%if (iPreview != 0)
              { %>
                <a href="/board/readboard_search/<%=sBoardname %>/<%=iPreview.ToString() %>?category=<%=category%>&keyword=<%=keyword%>&page=<%=page %>"><img src="/../Content/images/common/btn_prev.gif" alt="이전" /></a>
            <% }
              else
              {%>
                <a class="bo_read_menu_item"><img src="/../Content/images/common/btn_prev.gif" alt="이전" /></a>
            <% } %>
            <a  href="/board/index_search/<%=sBoardname %>/?page=<%=page %>&category=<%=category%>&keyword=<%=keyword%>"><img src="/../Content/images/common/btn_list2.gif" alt="리스트" /></a>
            <%if (iNextview != 0)
              { %>
                <a href="/board/readboard_search/<%=sBoardname %>/<%=iNextview.ToString() %>?category=<%=category%>&keyword=<%=keyword%>&page=<%=page%>"><img src="/../Content/images/common/btn_next.gif" alt="다음" /></a>
            <% }
              else
              {%>
                <a class="bo_read_menu_item"><img src="/../Content/images/common/btn_next.gif" alt="다음" /></a>
            <% } %>    
            <%if (ViewData["AUTH"].ToString() == "T")
                    { %>
                        <a href="/board/replyboard/<%=sBoardname %>/<%=iListnumber.ToString() %>"><img src="/../Content/images/common/btn_reply.gif" alt="답변" /></a>
                        <a href="/board/modifyboard/<%=sBoardname %>/<%=iListnumber.ToString() %>"><img src="/../Content/images/common/btn_modify.gif" alt="수정" /></a>
                        <a href="/board/deleteboard/<%=sBoardname %>/<%=iListnumber.ToString() %>"><img src="/../Content/images/common/btn_delete.gif" alt="삭제" /></a>
                  <%}              
              else                        
                    {%>    
                        <div>&nbsp;</div>
                  <%} %>    
            
        </div>
      </div>  
    </div>


</asp:Content>
