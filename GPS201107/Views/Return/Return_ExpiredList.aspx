﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ViewPage1
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    alert("수정되었습니다.");
    location.href = '/gps/menu/ExpiredList'
   </script>"
    <h2>ViewPage1</h2>

</asp:Content>
