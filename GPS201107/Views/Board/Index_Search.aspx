<%@ Page Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="GPS201107.Models" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<link href="../../../../Content/board.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">

    function Validation() {
        if (document.getElementById("search_board").value == "") {
            alert("검색어를 입력하세요");
            document.getElementById("search_board").focus();
            return (false);
        }
        document.getElementById("search_keyword").submit();

    }
</script>

        <% string sId = "notice"; %>
        <% var category = ViewData["category"].ToString(); //검색 카테고리  %>
        <% var keyword = ViewData["keyword"].ToString(); // 검색어 %>
        <% var page = ViewData["page"].ToString(); //검색시 현재 페이지 수 %>
                
        <div id="contentsWrap">
            
            <h1><%=sId %></h1>
            <div id="contentsArea">
                
                
                <table>
                  <caption>
                  <%if ((ViewData["AUTH"].ToString() == "T") )
                    { %>
                        <a href="/board/writeboard/<%=sId %>"><img src="/Content/images/common/btn_list.gif" alt="글쓰기" /></a>
                        <%}
                    else
                    {%>
                        <p>&nbsp;</p>
                        <%} %>
                   </caption>
                  <thead>
                    <tr> 
                        <th id="bo_no">번호</th>
                        <th id="bo_title">제목</th>
                        <th id="bo_name">작성자</th>
                        <th id="bo_date">일자</th>
                        <th id="bo_readcount">조회</th>
                        <th id="bo_file">화일</th>
                    </tr>
                  </thead>
                  <tbody>
                  <% var BoardList = (List<Board>)ViewData["list"];%>
                  <% Board oBoard = new Board(); %>
                  <% for (int i = 0; i < BoardList.Count; i++)
                     { 
                        oBoard = BoardList[i];
                        if (i == 0)
                        {
                            %><%=oBoard.GetHtmlContents_first_Search(category,keyword,page)%><%
}
                        else
                        {
                            %><%=oBoard.GetHtmlContents_Search(category,keyword,page)%><%
                        }
                         
                     } %>

                <%--<% foreach(Board oBoard in (List<Board>)ViewData["list"] ) { %>
                    <tr class="bo_tr_contents">
                        <td class="bo_td_contents"><%=oBoard.iRealnumber.ToString() %></td>
                        <td class="bo_td_contents"><%=oBoard.sTitle.ToString() %></td>
                        <td class="bo_td_contents"><%=oBoard.sName.ToString() %></td>
                        <td class="bo_td_contents"><%=oBoard.sDate.ToString() %></td>
                        <td class="bo_td_contents"><%=oBoard.iRead.ToString() %></td>
                        <td class="bo_td_contents"><%=oBoard.sFilename.ToString() %></td>
                    </tr>
                    <% foreach (Board oReply in oBoard.oReplylist) 
                       { 
                    %>
                       <tr class="bo_tr_reply">
                        <td class="bo_td_reply">[Re]</td>
                        <td class="bo_td_reply"><%=oReply.sTitle.ToString()%></td>
                        <td class="bo_td_reply"><%=oReply.sName.ToString()%></td>
                        <td class="bo_td_reply"><%=oReply.sDate.ToString()%></td>
                        <td class="bo_td_reply"><%=oReply.iRead.ToString()%></td>
                        <td class="bo_td_reply"><%=oReply.sFilename.ToString()%></td>
                       </tr>
                    <% } %>
                <%} %>--%>
                  </tbody>
                  <tfoot>
                      <tr>
                         <td colspan="6"><%=(string)ViewData["paging"] %></td>
                      </tr>
                  </tfoot> 
                </table>
                <div class="bo_search">
                  <form method="get" id="search_keyword" action="/board/Index_Search">            
                        <select name="category" size="1" >
                            <option <%if (category == "1") {%> selected <%} %> value="1">작성자</option>
                            <option <%if (category == "2") {%> selected <%} %> value="2">제 목</option>
                            <option <%if (category == "4") {%> selected <%} %> value="4">내 용</option>
                        </select>
                        <input type="text" name="keyword" size="20"  value="<%=keyword %>" id="search_board"/>
                        <input style="display:none;" type="text" name="boardname" size="20" value="<%=sId%>"  id="boardname"/>
                        <input type="button" onclick="Validation();" value="확 인"/>
                    </form>
                </div>
          </div>  
        </div>


</asp:Content>
