<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" ValidateRequest="false"%>
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
        
        <h1><%=sId %>&nbsp;Modify</h1>
        <div id="contentsArea">
            <form id="confirm" method="post" enctype="multipart/form-data" action="/gps/board/ModifyboardAction">
                <% var oParentboard = (Board)ViewData["parentboard"]; %>
                <!--글 수정 -->
                <table class="tbl_type2" border="1" cellspacing="0" summary="글 내용을 표시">
                    <caption>글 수정</caption>
                    <colgroup>
                    <col width="80">
                    <col>
                    </colgroup>
                    <thead>
                        <tr>
                            <th scope="row">작성자</th>
                            <td>&nbsp;<input id="name" type="text" name="bo_w_name" value="<%=oParentboard.sName %>" /></td>
                        </tr>
                    </thead>
                    <tbody>
                    <tr>
                        <th scope="row">제목</th>
                        <td>&nbsp;<input id ="subject"type="text" size="80" name="bo_w_title"value="<%=oParentboard.sTitle %>"/></td>
                    </tr>
<%--                    <tr>
                        <th scope="row">파일첨부</th>
                        <td>&nbsp;<input type="file" name="bo_w_file"/><a href="/Upload/<%=oParentboard.sFilename %>"><%=oParentboard.sFilename %></a>
                        <input type="hidden"  name="ex_file" value="<%=oParentboard.sFilename %>" />
                        <% if (oParentboard.sFilename != "")
                           { %>
                        삭제<input type="checkbox" name="deletefile" value="true" /> 
                        <%} %>
                        </td>
                    </tr>--%>
                    <tr>
                        <th scope="row">비밀번호</th>
                        <td>&nbsp;<input type="text" name="bo_w_password" value="<%=oParentboard.sPass %>"/></td>
                    </tr>
                    <tr>
                        <td class="cont" colspan="2"><textarea cols="140" rows="20" id="xqEditor" name="bo_w_content" class="t"><%=oParentboard.sComment %></textarea></td>
                    </tr>
                    </tbody>
                </table>          
       
                
               
                  
                <div id="bo_write_menu" class="tp5"> 
                <input type="hidden" name="boardname" value="<%=sId %>"/>
                <input type="hidden" name="listnumber" value="<%=(String)ViewData["listnumber"] %>"/>
                <img onclick="Validation();" style="margin-left: 405px; cursor:pointer;" src="/gps/Content/image/common/b_btn_ok.gif" />
                <a class="bo_write_button" href="/gps/board/index/<%=sId %>">
                    <img src="/gps/Content/image/common/btn_list.gif" alt="리스트" /></a></div>
            </form>
        </div>  
    </div>


</asp:Content>

