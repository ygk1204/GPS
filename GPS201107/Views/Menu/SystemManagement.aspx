<%@ Page Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>사용자 계정 관리</h2>

    <table id="list" class="scroll" cellpadding="0" cellspacing="0"></table>
    <div id="listPager" class="scroll" style="text-align:center;"></div>                                                            
    <div id="listPsetcols" class="scroll" style="text-align:center;"></div>  

    <script type="text/javascript">
        function isValidPhone(value, name) {
            console.log('isValidPhone');
            var errorMessage = name + ': Invalid Format';
            var success = value.length === 14;
            return [success, success ? '' : errorMessage];
        }

        $(document).ready(function () {
            var categories = ["Admin", "User"];
            var addDialog = {
                url: '<%= Url.Action("Update_user", "Entity") %>'
                , closeAfterAdd: true
                , closeAfterEdit: true
                , afterShowForm: function (formId) {

                }
                , afterclickPgButtons: function (whichbutton, formid, rowid) {
                    ;
                }
                , modal: true
                , beforeShowForm: function (form) {
                    $('#tr__EMP_NO', form).show();
                    $('#tr__KNAME', form).show();
                }
                , onclickSubmit: function (params) {
                    var ajaxData = {};

                    var list = $("#list");
                    var selectedRow = list.getGridParam("selrow");
                    rowData = list.getRowData(selectedRow);
                    ajaxData = { _EMP_NO: rowData._EMP_NO };

                    return ajaxData;
                }

                , width: "400"
            };
            var updateDialog = {
                url: '<%= Url.Action("Update_user", "Entity") %>'
                , closeAfterAdd: true
                , closeAfterEdit: true
                , afterShowForm: function (formId) {

                }
                , afterclickPgButtons: function (whichbutton, formid, rowid) {
                    ;
                }
                , modal: true
                , beforeShowForm: function (form) {
                    $('#tr__EMP_NO', form).hide();
                    $('#tr__KNAME', form).hide();

                }
                , onclickSubmit: function (params) {
                    var ajaxData = {};

                    var list = $("#list");
                    var selectedRow = list.getGridParam("selrow");
                    rowData = list.getRowData(selectedRow);
                    ajaxData = { _EMP_NO: rowData._EMP_NO };

                    return ajaxData;
                }

                , width: "400"
            };
            $.jgrid.nav.addtext = "추가";
            $.jgrid.nav.edittext = "수정";
            $.jgrid.nav.deltext = "삭제";
            $.jgrid.edit.addCaption = "계정 추가";
            $.jgrid.edit.editCaption = "계정 수정";
            $.jgrid.del.caption = "계정 삭제";
            $.jgrid.del.msg = "해당 사용자 정보를 삭제하시겠습니까?";
            $("#list").jqGrid({
                url: '<%= Url.Action("GetGPSUsers", "Entity") %>',
                datatype: 'json',
                mtype: 'POST',
                colNames: ['직번', '이름', '권한'],
                colModel: [

                    { name: '_EMP_NO', index: '_EMP_NO', width: 50, align: 'left', editable: true, edittype: 'text', editrules: { required: true }, formoptions: { elmsuffix: ' *'} },
                    { name: '_KNAME', index: '_EMP_NO', width: 50, align: 'left', editable: true, edittype: 'text', editrules: { required: true }, formoptions: { elmsuffix: ' *'} },
                    { name: '_AUTHORITY', index: '_EMP_NO', width: 50, align: 'left', editable: true, edittype: 'select', editoptions: { value: "Admin:Admin;User:User" }, editrules: { required: true}}],
                pager: $('#listPager'),
                scroll: 1,
                rowNum: 1000,
                rowList: [1000],
                height:250,
                sortname: 'ContactId',
                sortorder: "desc",
                viewrecords: true,
                imgpath: '/gps/Content/Themes/Redmond/Images',
                caption: 'User List',
                autowidth: true,
                ondblClickRow: function (rowid, iRow, iCol, e) {
                    $("#list").editGridRow(rowid, prmGridDialog);
                }
            }).navGrid('#listPager',
                {
                    edit: true, add: true, del: true, search: false, refresh: true
                },
                updateDialog,
                addDialog,
                updateDialog
            );
        });       
    </script>
</asp:Content>
