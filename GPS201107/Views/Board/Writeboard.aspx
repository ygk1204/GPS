<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" ValidateRequest="false" %>
<%@ Import Namespace="GPS201107.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



<!-- xed 에디터 생성 스크립트 -->
<script type="text/javascript">

    function Validation() {


        if (document.getElementById("name").value == "") {
            alert("이름을 입력하세요");
            document.getElementById("name").focus();
            return (false);
        }
        if (document.getElementById("subject").value == "") {
            alert("제목을 입력하세요");
            document.getElementById("subject").focus();
            return (false);
        }
        document.getElementById("confirm").submit();
    }
</script>
    
    <% var sId = "notice"; %>
    <div id="contentsWrap">
               
        <h1>Write</h1>
        <form id="confirm" name="confirm" method="post" enctype="multipart/form-data" action="/gps/board/WriteboardAction">
                 <!--글쓰기 -->
                    <table class="tbl_type2" border="1" cellspacing="0" summary="글 내용을 표시">
                        <caption>글 쓰기</caption>
                        <colgroup>
                        <col width="80">
                        <col>
                        </colgroup>
                        <thead>
                            <tr>
                                <th scope="row">작성자</th>
                                <td>&nbsp;<input id="name" type="text" name="bo_w_name" value="<%=(string)ViewData["name"]%>" /></td>
                            </tr>
                        </thead>
                        <tbody>
                        <tr>
                            <th scope="row">제목</th>
                            <td>&nbsp;<input id="subject" size="80" class="bo_w_menu_input" type="text" name="bo_w_title" /></td>
                        </tr>
<%--                        <tr>
                            <th scope="row">파일첨부</td>
                            <td>&nbsp;<input type="file" class="bo_w_menu_input" name="bo_w_file" /></td>
                        </tr>--%>
                        <tr>
                            <th scope="row">비밀번호</th>
                            <td>&nbsp;<input type="text" name="bo_w_password" /></td>
                        </tr>
                                                      
                             <%--<td><textarea cols="84" rows="20" name="bo_w_content" ></textarea></td>--%>
                         </tbody>
                      </table>
                     <textarea cols="150" rows="20" id="xqEditor" name="bo_w_content"></textarea>                
               <div id="bo_write_menu" class="tp5"> 
                <input type="hidden" name="boardname" id="boardname" class="t" value="<%=sId %>"/>
                <img onclick="Validation();"class="bo_write_button" src="/gps/Content/image/common/b_btn_ok.gif"  value="확 인" style="margin-left: 405px; cursor:pointer;" />
                <a class="bo_write_button" href="/board/index/<%=sId %>"><img src="/gps/Content/image/common/b_btn_list.gif" alt="리스트" /></a></div>
            </form>          
    </div>


</asp:Content>

