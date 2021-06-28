<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>"  ValidateRequest="false" %>
<%@ Import Namespace="GPS201107.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link href="../../../../Content/board.css" rel="stylesheet" type="text/css" />

<link rel="stylesheet" type="text/css" href="http://asefront.asekr.com/webeditor/stylesheets/xq_ui.css" />
<script type="text/javascript" src="http://asefront.asekr.com/webeditor/javascripts/XQuared.js?load_others=1"></script>
<script type="text/javascript" src="http://asefront.asekr.com/webeditor/javascripts/plugin/FileUploadPlugin.js"></script>

<!-- xed 에디터 생성 스크립트 -->
<script type="text/javascript">
    var xed;
    window.onload = function () {
        xed = new xq.Editor("xqEditor");

        xed.isSingleFileUpload = true;
        xed.addPlugin('FileUpload');
        xed.setFileUploadTarget('/Board/ImageUpload', null);
        xed.setEditMode('wysiwyg');
        xed.setWidth("100%");
    };
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
        document.getElementById("xqEditor").value = xed.getCurrentContent(true);
        document.getElementById("confirm").submit();
    }
</script>

    <% var sId = ViewData["id"].ToString(); %>
    

    <div id="contentsWrap">
        
        <h1><%=sId %>&nbsp;Reply</h1>
        <div id="contentsArea">
            <form method="post" id="confirm" name="confirm" enctype="multipart/form-data" action="/board/ReplyboardAction">
                  <table width="680">
                     <tbody>
                        <tr>
                           <td>작성자</td>
                           <td><input type="text" id="name" name="bo_w_name" value="<%=(string)ViewData["name"]%>" /></td>                       
                        </tr>
                        <tr>
                           <td>제목</td>
                           <td><input type="text" id="subject" name="bo_w_title" size="100" value="<%=(string)ViewData["title"]%>" /></td>
                        </tr>
                        <tr>
                           <td>파일첨부</td>
                           <td><input type="file" name="bo_w_file" /></td>
                        </tr>
                        <tr>
                           <td>비밀번호</td>
                           <td><input type="text" name="bo_w_password" /></td>
                        </tr>
                        <tr>
                           <td>내용</td>
                           <%--<td><textarea cols="84" rows="20" name="bo_w_content" class="t"><%=(string)ViewData["comment"]%></textarea></td>--%>
                           <td><textarea cols="84" rows="20" id="xqEditor" name="bo_w_content" class="t"><%=(string)ViewData["comment"]%></textarea></td>
                        </tr>
                     </tbody>
                  </table>
                <div id="bo_replyboard_menu" class="tpad5">                
                <!--input type="hidden" name="bo_w_title" value="<%=(string)ViewData["title"]%>"/-->
                <input type="hidden" name="boardname" value="<%=sId %>"/>
                <input type="hidden" name="listnumber" value="<%=(String)ViewData["listnumber"] %>"/>
                <input class="bo_write_button" onclick="Validation();" type="button" value="확 인" />
                <a class="bo_write_button" href="/board/index/<%=sId %>"><img src="/../Content/images/common/btn_list2.gif" alt="리스트" /></a></div>
            </form>
        </div>  
    </div>


</asp:Content>

