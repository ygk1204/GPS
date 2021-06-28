<%@ Page Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" ValidateRequest="false" %>
<html>
<head id="Head1" runat="server">
    <title>Editor Imageupload</title>
    <script type="text/javascript">        //<![CDATA[
        window.onload = function () {

            if (window.parent.xed) {
                window.parent.xed.fileUploadListener.onSuccess('<%= ViewData["FileName"] %>', '');
            }
        }
        //]]></script>
</head>
<body>
    <%=ViewData["body"]%>
</body>
</html>