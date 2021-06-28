<%@ Page Language="C#"MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="GPS201107.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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

         
        <div id="contentsWrap" class="pl30 pt20">

            
            <div id="contentsArea">
               <!-- table  -->                    
                       
                        <!-- 게시판 리스트 -->
                        <table class="tbl_type4" border="1" cellspacing="0" summary="게시판의 글제목 리스트">
                        <caption>게시판 리스트</caption>
                        <colgroup>
                            <col width="40" />
                            <col width="320" />
                            <col width="40" />
                            <col width="70" />
                            <col width="45" />
                            <col width="40" />
                        </colgroup>
                        <thead>
                            <tr>
                                <th scope="col"><img alt="" src="/gps/Content/image/sub05/b_txt_01.gif" /></th>
                                <th scope="col"><img alt="" src="/gps/Content/image/sub05/b_txt_02.gif" /></th>
                                <th scope="col"><img alt="" src="/gps/Content/image/sub05/b_txt_03.gif" /></th>
                                <th scope="col"><img alt="" src="/gps/Content/image/sub05/b_txt_04.gif" /></th>
                                <th scope="col"><img alt="" src="/gps/Content/image/sub05/b_txt_05.gif" /></th>
                                <th scope="col"><img alt="" src="/gps/Content/image/sub05/b_txt_06.gif" /></th>
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
                                        %><%=oBoard.GetHtmlContents_first()%><%
            }
                                    else
                                    {
                                        %><%=oBoard.GetHtmlContents()%><%
                                    }
                         
                                 } %>
                            
                        </tbody>
                        </table>   

                <!--//ui object -->  
                <div>
<%--                              <form method="get" id="search_keyword" action="/Board/Index_Search">        
                                      <fieldset class="srch" size="1">
                                        <legend>검색영역</legend>
                                        <select name="category"> 
                                          <option value="1">작성자</option>
                                          <option value="2">제 목</option>
                                          <option value="4">내 용</option>
                                        </select>
                                        <input type="text" name="keyword" size="20" id="search_board"/>
                                        <input style="display:none;" type="text" name="boardname" size="20" value="notice" id="boardname"/>
                                        <input type="image"  src="/Content/image/common/btn_srch.gif"  value="확 인" onclick="return Validation();"/>
                                     </fieldset> 
                                </form>--%>
                     </div>                                    
            </div> 
                                   
                          <div style="float:right; margin-right:15px; margin-top:10px;">
                          <%if (Session["Authority"].ToString() == "User" || Session["Authority"].ToString() == "Admin" )
                            { %>
                                <a href="/gps/board/writeboard/"><img src="/gps/Content/images/common/btn_list.gif" alt="글쓰기" /></a>
                                <%}
                            else
                            {%>
                                <%} %>
                          </div>                  
            <p class="h30 fs11 pt10 center"><%=(string)ViewData["paging"] %></p>    
        </div>

 <script type="text/javascript">
   

   
</script>

</asp:Content>
