<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" ValidateRequest="false"%>
<%@ Import Namespace="GPS201107.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/css/table.css" rel="stylesheet" type="text/css" />
    <% var sId = "notice"; %>
    

          <div id="contentsArea" class="pt5">        
        <div id="bo_read_contents">
            <% var oBoard = (Board)ViewData["board"];%>
            <!--글읽기 -->
                <table class="tbl_type2" border="1" cellspacing="0" summary="글 내용을 표시">
                    <caption>글 읽기</caption>
                    <colgroup>
                    <col width="80">
                    <col>
                    <col width="80">
                    <col width="150">
                    <col width="80">
                    <col width="80">
                    </colgroup>
                    <thead>
                        <tr>
                            <th scope="row">제목</th>
                            <td colspan="5">&nbsp;<%=oBoard.sTitle%></td>
                        </tr>
                    </thead>
                    <tbody>
                    <tr>
                        <th scope="row">작성자</th>
                        <td>&nbsp;<%=oBoard.sName%></td>
                        <th scope="row">등록일자</th>
                        <td>&nbsp;<img src="/gps/Content/images/board/calendar.gif" /><%=oBoard.sDate%></td>
                        <th scope="row">조회수</th>
                        <td>&nbsp;<%=oBoard.iRead.ToString()%></td>
                    </tr>
                    <tr>
                        <th scope="row">파일</th>
                        <td colspan="5">&nbsp;<img src="/gps/Content/images/board/iconFile.gif" />
                        <%if (oBoard.sFilename == "")
                          { %>
                            첨부파일없음
                        <%}
                          else
                          {%> 
                            <a href="/gps/Board/Download/<%=oBoard.sBoardname %>/<%=oBoard.iNumber.ToString()%>/<%=oBoard.sFilename %>" ><%=oBoard.sFilename%></a>
                        <%} %></td>
                    </tr>
                    <tr>
                        <td class="cont" colspan="6"><textarea cols="140" rows="20" id="xqEditor" name="bo_w_content" readonly="readonly" ><%=oBoard.sComment%></textarea> </td>
                    </tr>
                    </tbody>
                </table>           
        </div>
        <br />
        <div id="bo_read_menu">
            <% var sBoardname = (string)ViewData["boardname"];
               var iListnumber = (int)ViewData["listnumber"];
               var iCurrentpage = (int)ViewData["currentpage"];  
               var iPreview = (int)ViewData["preview"];
               var iNextview = (int)ViewData["nextview"]; 
            %>
            <%if (iPreview != 0)
              { %>
                <a href="/gps/board/readboard/<%=sBoardname %>/<%=iPreview.ToString() %>"><img src="/gps/Content/image/common/b_btn_prev.gif" alt="이전" /></a>
            <% }
              else
              {%>
                <a class="bo_read_menu_item"><img src="/gps/Content/image/common/b_btn_prev.gif" alt="이전" /></a>
            <% } %>
            <a  href="/gps/board/index/<%=sBoardname %>/<%=iCurrentpage.ToString() %>"><img src="/gps/Content/image/common/btn_list.gif" alt="리스트" /></a>
            <%if (iNextview != 0)
              { %>
                <a href="/gps/board/readboard/<%=sBoardname %>/<%=iNextview.ToString() %>"><img src="/gps/Content/image/common/b_btn_next.gif" alt="다음" /></a>
            <% }
              else
              {%>
                <a class="bo_read_menu_item"><img src="/gps/Content/image/common/b_btn_next.gif" alt="다음" /></a>
            <% } %>    
           <%if (Session["Authority"].ToString() == "User" || Session["Authority"].ToString() == "Admin" )
                    { %>

                        <a href="/gps/board/modifyboard/<%=sBoardname %>/<%=iListnumber.ToString() %>"><img src="/gps/Content/image/common/b_btn_modify.gif" alt="수정" /></a>
                        <a href="/gps/board/deleteboard/<%=sBoardname %>/<%=iListnumber.ToString() %>"><img src="/gps/Content/image/common/b_btn_delete.gif" alt="삭제" /></a>
                        <a href="/gps/board/activate/<%=sBoardname %>/<%=iListnumber.ToString() %>"><img src="/Content/image/common/b_btn_activate.gif" alt="Activate" /></a>
                        <a href="/gps/board/Inactivate/<%=sBoardname %>/<%=iListnumber.ToString() %>"><img src="/Content/image/common/b_btn_hold.gif" alt="Hold" /></a>
                  <%}              
              else                        
                    {%>    
                        <div>&nbsp;</div>
                  <%} %>    
            
        </div>
      </div>  

 <script type="text/javascript">

</script>


</asp:Content>
