<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" ValidateRequest="false"%>
<%@ Import Namespace="GPS201107.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


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
       


</asp:Content>
