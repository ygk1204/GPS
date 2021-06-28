<%@ Page Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<GPS201107.Models.REGISTERDOCUMENT>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DocumentFolder
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
<style type="text/css">
.ui-jqgrid {font-size:10px;}
table
{
    font-size:11px;
}
 </style>
  <script type="text/javascript">

      $(function () {
          $.datepicker.setDefaults($.datepicker.regional[""]);
          $(".datepicker").datepicker($.datepicker.regional['ko']);
          $(".datepicker").datepicker("option", "dateFormat", 'yy-mm-dd');
      });
</script>
 <script type="text/javascript">

     var suppliercode = "";
     var suppliername = "";

     $(document).ready(function () {

         $.jgrid.nav.addtext = "추가";
         $.jgrid.nav.edittext = "수정";
         $.jgrid.nav.deltext = "삭제";
         $.jgrid.edit.addCaption = "계정 추가";
         $.jgrid.edit.editCaption = "해당 정보 수정";
         $.jgrid.del.caption = "계정 삭제";
         $.jgrid.del.msg = "해당 사용자 정보를 삭제하시겠습니까?";
         
//---------------- supplier 
         var addDialog_supplier = {
             url: '<%= Url.Action("Update_supplier", "Entity") %>'
                , closeAfterAdd: true
                , closeAfterEdit: true
                , afterShowForm: function (formId) {

                }
                , afterclickPgButtons: function (whichbutton, formid, rowid) {
                    ;
                }
                , modal: true
                , beforeShowForm: function (form) {


                }
                , onclickSubmit: function (params) {
                    var ajaxData = {};

                    var list = $("#single");
                    var selectedRow = list.getGridParam("selrow");
                    rowData = list.getRowData(selectedRow);
                    ajaxData = { _EMP_NO: rowData._EMP_NO };

                    return ajaxData;
                }

                , width: "400"
         };
         var updateDialog_supplier = {
             url: '<%= Url.Action("Update_supplier", "Entity") %>'
                , closeAfterAdd: true
                , closeAfterEdit: true
                , afterShowForm: function (formId) {

                }
                , afterclickPgButtons: function (whichbutton, formid, rowid) {
                    ;
                }
                , modal: true
                , beforeShowForm: function (form) {


                }
                , onclickSubmit: function (params) {
                    var ajaxData = {};

                    var list = $("#single");
                    var selectedRow = list.getGridParam("selrow");
                    rowData = list.getRowData(selectedRow);
                    ajaxData = { SUPPLIERCODE: suppliercode };

                    return ajaxData;
                }

                , width: "400"
         };



//-----------------------supplier grid setting
         jQuery("#single").jqGrid({

             // supplier data grid
             url: '<%= Url.Action("GetGPSuppliersDataToUpdate", "Entity") %>',
             contentType: "application/json; charset=utf-8",
             datatype: 'json',
             mtype: 'POST',
             colNames: ['Code', 'Name', 'Representative', 'Mail', 'Active', 'Reason'],

             // 컬럼 모델 셋팅 하기
             colModel: [
                  { name: 'SUPPLIERCODE', index: 'SUPPLIERCODE', width: 70, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'SUPPLIERNAME', index: 'SUPPLIERNAME', width: 140, align: 'left', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne']} },
                  { name: 'CONTACT', index: 'CONTACT', width: 100, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'EMAIL', index: 'EMAIL', width: 100, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'ACTIVE', index: 'ACTIVE', width: 40, align: 'left', stype: 'select', searchoptions: { sopt: ['cn'], value: ':전체선택;T:T;F:F', defaultValue: '' }, editable: true, edittype: 'select', editoptions: { value: "T:T;F:F" }, formoptions: { elmsuffix: ' *'} },
                  { name: 'DISABLEREASON', index: 'DISABLEREASON', width: 10, align: 'left', editable: true, edittype: 'textarea', editoptions: { rows: "5", cols: "50" }, editrules: { required: false }, formoptions: { elmsuffix: ' '} }
                  ],
             scroll: 1,
             multiselect: true,
             rowNum: 200,
             rowList: [20, 50, 100],
             gridview: true,
             viewrecords: true,
             ignoreCase: true,
             height: 200,
             cache: false,
//             multikey: 'ctrlKey',
             sortorder: "desc",
             pager: jQuery('#pager'),
             emptyrecords: "Nothing to display",
             loadonce: true,
             height: 484,


             gridComplete: resetgrid,
             caption: 'Supplier List',
//             beforeSelectRow: function (rowid, e) {
//                 if (!e.ctrlKey) {
//                     $("#single").resetSelection();
//                 }
//                 return true;
//             },

             onSelectRow: function (ids, status) {

                 if (status == true) {

                     $('#add_list_contact').show();
                     $('#add_list_material').show();
                     suppliercode = jQuery("#single").getCell(ids, 'SUPPLIERCODE').toString();
                     suppliername = jQuery("#single").getCell(ids, 'SUPPLIERNAME').toString();

                     if (ids == null) {

                         ids = 0;
                         if (jQuery("#list_material").jqGrid('getGridParam', 'records') > 0) {
                             jQuery("#list_material").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSuppliersMaterialsToUpdate/' + jQuery("#single").getCell(ids, 'SUPPLIERCODE').toString() });
                             jQuery("#list_material").jqGrid('setGridParam', { datatype: 'json' });
                             jQuery("#list_material").jqGrid('setCaption', "Material Detail:" + jQuery("#single").getCell(ids, 'SUPPLIERNAME').toString().substr(0, 8)).trigger('reloadGrid');
                             select_supplier(ids);
                         }

                         jQuery("#list_contact").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSuppliersContact/' + jQuery("#single").getCell(ids, 0).toString() });
                         jQuery("#list_contact").jqGrid('setGridParam', { datatype: 'json' });
                         jQuery("#list_contact").jqGrid('setCaption', "Contact Info:" + jQuery("#single").getCell(ids, 'SUPPLIERNAME').toString().substr(0, 8)).trigger('reloadGrid');



                     } else {

                         jQuery("#list_material").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSuppliersMaterialsToUpdate/' + jQuery("#single").getCell(ids, 'SUPPLIERCODE').toString() });
                         jQuery("#list_material").jqGrid('setGridParam', { datatype: 'json' });
                         jQuery("#list_material").jqGrid('setCaption', "Material Detail:" + jQuery("#single").getCell(ids, 'SUPPLIERNAME').toString().substr(0, 8)).trigger('reloadGrid');


                         jQuery("#list_contact").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSuppliersContact/' + jQuery("#single").getCell(ids, 'SUPPLIERCODE').toString() });
                         jQuery("#list_contact").jqGrid('setGridParam', { datatype: 'json' });
                         jQuery("#list_contact").jqGrid('setCaption', "Contact Info:" + jQuery("#single").getCell(ids, 'SUPPLIERNAME').toString().substr(0, 8)).trigger('reloadGrid');

                         select_supplier(ids);
                     }
                 } else {
                     $('#add_list_contact').hide();
                     $('#add_list_material').hide();

                 }
             }
         });
//         $("#single").jqGrid('hideCol', 'cb');
         $("#single").jqGrid('filterToolbar', { stringResult: true, searchOnEnter: true, defaultSearch: "cn" });
         jQuery("#single").navGrid('#pager', { view: false, del: false, add: false, edit: true },
                     updateDialog_supplier,
                addDialog_supplier,
                updateDialog_supplier,
       { closeOnEscape: true, multipleSearch: true, closeAfterSearch: true }, // search options
       {} /* view parameters*/
     );


//------------------ contact Person list

         var addDialog_contact = {

             url: '<%= Url.Action("Update_contact", "Entity") %>'
                , closeAfterAdd: true
                , closeAfterEdit: true
                , afterShowForm: function (formId) {

                }
                , afterclickPgButtons: function (whichbutton, formid, rowid) {
                    ;
                }
                , modal: true
                , beforeShowForm: function (form) {
                    $('#tr__PERSONNAME', form).show();
                }
                , onclickSubmit: function (params) {
                    var ajaxData = {};

                    var list = $("#list_contact");
                    var selectedRow = list.getGridParam("selrow");
                    rowData = list.getRowData(selectedRow);
                    ajaxData = { _SUPPLIERCODE: suppliercode, _SUPPLIERNAME: suppliername };

                    return ajaxData;
                }

                , width: "400"
         };
         var updateDialog_contact = {



             url: '<%= Url.Action("Update_contact", "Entity") %>'
                , closeAfterAdd: true
                , closeAfterEdit: true
                , afterShowForm: function (formId) {

                }
                , afterclickPgButtons: function (whichbutton, formid, rowid) {
                    ;
                }
                , modal: true
                , beforeShowForm: function (form) {
                    // $('#tr__PERSONNAME', form).hide();
                }
                , onclickSubmit: function (params) {
                    var ajaxData = {};

                    var list = $("#list_contact");
                    var selectedRow = list.getGridParam("selrow");
                    rowData = list.getRowData(selectedRow);
                    ajaxData = { _SUPPLIERCODE: suppliercode, _SUPPLIERNAME: suppliername, old_mail: rowData._MAILADDRESS };

                    return ajaxData;
                }

                , width: "400"
         };


         $("#list_contact").jqGrid({
             url: '<%= Url.Action("GetGPSUsers", "Entity") %>',
             datatype: 'local',
             mtype: 'POST',
             colNames: ['CODE', 'SUPPLIER NAME', 'NAME', "MAIL", "PHONE", "TYPE"],
             colModel: [
                  { name: '_SUPPLIERCODE', index: '_SUPPLIERCODE', hidden: true, width: 50, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: '_SUPPLIERNAME', index: '_SUPPLIERNAME', hidden: true, width: 50, align: 'left', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne'] }, editable: true, edittype: 'text', formoptions: { elmsuffix: ' *'} },
                  { name: '_PERSONNAME', index: '_PERSONNAME', width: 100, align: 'left', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne'] }, editable: true, edittype: 'text', editrules: { required: true }, formoptions: { elmsuffix: ' *'} },
                  { name: '_MAILADDRESS', index: '_MAILADDRESS', width: 130, align: 'left', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne'] }, editable: true, edittype: 'text', editrules: {custom:true,custom_func:checkEmail,required: true }, formoptions: { elmsuffix: ' *'} },
                  { name: '_PHONE', index: '_PHONE', width: 150, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne'] }, editable: true, edittype: 'text', editrules: { required: false }, formoptions: { elmsuffix: ''} },
                  { name: '_MAILTYPE', index: '_MAILTYPE', width: 50, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne'] }, editable: true, edittype: 'select', editoptions: { value: "TO:TO;CC:CC" }, editrules: { required: true }, formoptions: { elmsuffix: ' *'} }
                  ],
             pager: $('#listPager_contact'),
             scroll: 1,
             rowNum: 1000,
             rowList: [1000],
             sortname: 'ContactId',
             sortorder: "desc",
             viewrecords: true,
             height: 180,
             edit: {
                 addCaption: "Add Record",
                 editCaption: "Supplier 수정",
                 bSubmit: "Submit",
                 bCancel: "Cancel",
                 bClose: "Close",
                 saveData: "Data has been changed! Save changes?",
                 bYes: "Yes",
                 bNo: "No",
                 bExit: "Cancel"
             }
                     ,
             imgpath: '/gps/Content/Themes/Redmond/Images',
             caption: 'Contact Info',
             ondblClickRow: function (rowid, iRow, iCol, e) {
                 $("#list_contact").editGridRow(rowid, prmGridDialog);
             }
         }).navGrid('#listPager_contact',
                {
                    edit: true, add: true, del: true, search: false, refresh: true
                },
                updateDialog_contact,
                addDialog_contact,
                updateDialog_contact
            );


//------------------------------------------------------- material data grid----------------------------------------
         var addDialog_material = {
             url: '<%= Url.Action("Update_material", "Entity") %>'
                , closeAfterAdd: true
                , closeAfterEdit: true
                , afterShowForm: function (formId) {

                }
                , afterclickPgButtons: function (whichbutton, formid, rowid) {
                    ;
                }
                , modal: true
                , beforeShowForm: function (form) {
                   $('#tr__MATERIALNAME', form).show();
                   $('#tr_PARTNUM', form).hide();
                }
                , onclickSubmit: function (params) {
                    var ajaxData = {};

                    var list = $("#list_material");
                    var selectedRow = list.getGridParam("selrow");
                    rowData = list.getRowData(selectedRow);
                    ajaxData = { _SUPPLIERCODE: suppliercode, _SUPPLIERNAME: suppliername };

                    return ajaxData;
                }

                , width: "400"
         };
         var updateDialog_material = {
             url: '<%= Url.Action("Update_material", "Entity") %>'
                , closeAfterAdd: true
                , closeAfterEdit: true
                , afterShowForm: function (formId) {

                }
                , afterclickPgButtons: function (whichbutton, formid, rowid) {
                    ;
                }
                , modal: true
                , beforeShowForm: function (form) {
                    $('#tr__MATERIALNAME', form).hide();
                    $('#tr_PARTNUM', form).hide();
                    

                }
                , onclickSubmit: function (params) {
                    var ajaxData = {};

                    var list = $("#list_material");
                    var selectedRow = list.getGridParam("selrow");
                    rowData = list.getRowData(selectedRow);
                    ajaxData = { _SUPPLIERCODE: suppliercode, _SUPPLIERNAME: suppliername };

                    return ajaxData;
                }

                , width: "400"
         };


         jQuery("#list_material").jqGrid({
             url: '/gps/Entity/GetGPSuppliersMaterials/',
             height: 320,
             datatype: 'local',
             mtype: 'POST',
             colNames: ['MATERIALNAME', 'PARTNUM', 'ACTIVE', 'Reason'],
             colModel: [
                            { name: '_MATERIALNAME', index: '_MATERIALNAME', width: 180, align: 'left', editable: true, edittype: 'text', editrules: { required: true }, formoptions: { elmsuffix: ' *'} },
                            { name: 'PARTNUM', index: 'PARTNUM', width: 131, align: 'left', editable: true, edittype: 'text', editrules: { required: false }, formoptions: { elmsuffix: ' *'} },
                            { name: '_ACTIVE', index: '_ACTIVE', width: 40, align: 'left', stype: 'select', searchoptions: { sopt: ['cn'], value: ':전체선택;T:T;F:F', defaultValue: '' }, editable: true, edittype: 'select', editoptions: { value: "T:T;F:F" }, formoptions: { elmsuffix: ' *'} },
                            { name: '_DISABLEREASON', index: '_DISABLEREASON', width: 60, align: 'left', editable: true, edittype: 'textarea', editoptions: { rows: "5", cols: "50" }, editrules: { required: false }, formoptions: { elmsuffix: ' '} }
                    ],
             scroll: 1,
             rowNum: 1000,
             rowList: [5, 10, 20],
             pager: '#listPager_material',
             sortname: 'PARTNUM',
             viewrecords: true,
             sortorder: "asc",
             height: 225,
             multiselect: true,
             gridview: true,
             ignoreCase: true,
             loadonce: true,
             caption: "Material Detail",
             ondblClickRow: function (rowid, iRow, iCol, e) {
                 $("#list_material").editGridRow(rowid, prmGridDialog);
             }

         }).navGrid('#listPager_material',
                {
                    edit: true, add: true, del: false, search: false, refresh: true
                },
                updateDialog_material,
                addDialog_material,
                updateDialog_material
            );
         $("#list_material").jqGrid('filterToolbar', { stringResult: true, searchOnEnter: true, defaultSearch: "cn" });
        
        //페이지 로딩시 contact 그리드와 material 그리드의 add버튼 삭제
         $('#add_list_contact').hide();
         $('#add_list_material').hide();


     });
  


    </script>
  
  <!--Supplier list table-->

  
  <!--Supplier list table-->
<div style="width:1000px; margin-bottom: -18px;overflow:hidden; height:auto;">

<div style="float:left; width:490px; height:600px;">
  <table id="single" class="scroll" cellpadding="0" cellspacing="0"></table>
  <div id="pager" class="scroll" style="text-align:center;"></div>
 
</div>

<div style="float:left;">
<div>
    <table id="list_contact" class="scroll" cellpadding="0" cellspacing="0"></table>
    <div id="listPager_contact" class="scroll" style="text-align:center;"></div>                                                            
    <div id="listPsetcols_conact" class="scroll" style="text-align:center;"></div>  
</div>

<div style="padding-top:5px">
    <table id="list_material" class="scroll" cellpadding="0" cellspacing="0"></table>
    <div id="listPager_material" class="scroll" style="text-align:center;"></div>                                                            
    <div id="listPsetcols_material" class="scroll" style="text-align:center;"></div>  
   
</div>
</div>
</div>
<!--active 및 inactive 버튼 area-->
 <div style="width:1000px; clear:both; text-align:left; overflow:hidden; height:auto; padding-bottom:0px;">
 <div style="width:490px;float:left;">
<span style="clear:both;" class="btn_pack small icon"><span class="check"></span><input id="activate_supplier" type="button" value="Activate suppliers&nbsp;&nbsp;&nbsp;" /></span>
<span style="clear:both;" class="btn_pack small icon"><span class="delete"></span><input id="inactivate_supplier" type="button" value="Inactivate suppliers" /></span>
<%-- <table style=" width:485px;clear:both; float:left; display:inline; text-align:left">
 <tr class='ui-widget-header'>
 <td colspan="2">asdfasd</td>
 </tr>
 <tr>
 <td rowspan="2"><textarea id="dd" name="sdf" cols="50" rows="4"></textarea></td>
 <td><span style="clear:both;" class="btn_pack small icon"><span class="check"></span><input id="activate_supplier" type="button" value="Activate Supplier&nbsp;&nbsp;&nbsp;" /></span></td>
 </tr>
 <tr>
 <td><span style="clear:both;" class="btn_pack small icon"><span class="delete"></span><input id="inactivate_supplier" type="button" value="Inactivate Supplier" /></span></td>
 </tr>
 </table>--%>
 </div>

 <div style="width:400px; float:left;">
<span style="clear:both;" class="btn_pack small icon"><span class="check"></span><input id="activate_material" type="button" value="Activate materials&nbsp;&nbsp;&nbsp;" /></span>
<span style="clear:both;" class="btn_pack small icon"><span class="delete"></span><input id="inactivate_material" type="button" value="Inactivate materials" /></span>
<%-- <table style=" width:440px;display:inline; text-align:left;">
 <tr class='ui-widget-header'>
 <td colspan="2">asdfasd</td>
 </tr>
 <tr>
  <td rowspan="2"><textarea id="Textarea1" name="sdf" cols="35" rows="4"></textarea></td>
  <td><span style="clear:both;" class="btn_pack small icon"><span class="check"></span><input id="activate_material" type="button" value="Activate material&nbsp;&nbsp;&nbsp;" /></span></td>
 </tr>
 <tr>
 <td><span style="clear:both;" class="btn_pack small icon"><span class="delete"></span><input id="inactivate_material" type="button" value="Inactivate material" /></span></td>
 </tr>
 </table>--%>
</div>

  </div>

 <form id="register_form" name="register_form" method="post" enctype="multipart/form-data" action="/gps/menu/SupplierInformation_action">
   <div style="width:900px; clear:both; text-align:left; padding-top:20px;">
  
  <!--Supplier informantion table-->
  <table class="itemhead" style="width:800px; clear:both;">
  <tr>
  <td style="width:20%;background-color:#cff5b7; font-weight:bold;">Supplier Name</td>
  <td><input id="s_name" name="s_name" size="20" readonly="readonly"></input></td>
  <td style="width:20%;background-color:#cff5b7; font-weight:bold;">Supplier Code</td>
  <td><input id="s_code" name="s_code" size="20" readonly="readonly"></input></td>
  </tr>
  </table>
  <br />
  <br />
   
    <!--Conflict Mineral-->
   <table class="itemhead" style="width:800px">
   <tr>
  <td style="width:20%;background-color:#cff5b7; font-weight:bold;">Document type
  </td>
  <td style="width:20%;background-color:#cff5b7;font-weight:bold;">Issued date
  </td>
  <td style="width:20%;background-color:#cff5b7;font-weight:bold;">Expired date
  </td>
  <td style="width:40%;background-color:#cff5b7;font-weight:bold;">Document file
  </td>
  </tr>
  </table>

  <div class="NsForm">
  <table class="ItemTable" style="width:800px"> 
  <tr>
  <td style="width:20%;">        
  <div class="editor-field">
 <select id="DocumenList_0___FILECATEGORY" name="DocumenList[0]._FILECATEGORY" class="category_doc">
                <option value="">선택</option>
                <option value="Conflict Mineral">Conflict Mineral</option>
                <option value="Warranty letter">Warranty letter</option>              
              </select>

  </div>             
  </td>
  <td style="width:20%">    
  <div class="editor-field">
              <%: Html.TextBoxFor(model => model.DocumenList[0]._ISSUEDATE, new Dictionary<string, object> { { "class", "datepicker" },{ "readonly", "true" }})%>
  </div>
  </td>
  <td style="width:20%">              
  <div class="editor-field">
                <%: Html.TextBoxFor(model => model.DocumenList[0]._EXPIREDATE, new Dictionary<string, object> { { "class", "expiredate" },{ "readonly", "true" } })%>

  </div>
  </td>
   <td style="width:40%">              
  <div class="editor-field">
                 <input id="DocumenList_0___FILENAME" name="DocumenList[0]._FILENAME" type="file"></input>         
  </div>
  </td>
  </tr>
  </table>
  <span  style="margin-top:7px" class="btn_pack medium icon RemoveItem"><span class="delete"></span><button type="button">삭제</button></span>
  </div>
  <span style="clear:both;" class="AddItem btn_pack medium icon"><span class="add"></span><button type="button">추가</button></span>
  
   
  <br />    
  <br />
  <br />
  <!--Warranty letter-->
  <%--<table class="itemhead" style="width:800px">
  <tr>
  <td style="width:20%;background-color:#cff5b7; font-weight:bold;">Document type
  </td>
  <td style="width:20%;background-color:#cff5b7;font-weight:bold;">Issued date
  </td>
  <td style="width:20%;background-color:#cff5b7;font-weight:bold;">Expired date
  </td>
  <td style="width:40%;background-color:#cff5b7;font-weight:bold;">Document file
  </td>
  </tr>
  </table>--%>

  <%--<table class="ItemTable" style="width:800px"> 
  <tr>
  <td style="width:20%;">        
  <div class="editor-field">
    Warranty letter

  </div>             
  </td>
  <td style="width:20%">    
  <div class="editor-field">
              <%: Html.TextBoxFor(model => model.DocumenList[1]._ISSUEDATE, new Dictionary<string, object> { { "class", "datepicker" },{ "readonly", "true" }})%>
  </div>
  </td>
  <td style="width:20%">              
  <div class="editor-field">
                <%: Html.TextBoxFor(model => model.DocumenList[1]._EXPIREDATE, new Dictionary<string, object> { { "class", "expiredate" },{ "readonly", "true" } })%>

  </div>
  </td>
   <td style="width:40%">              
  <div class="editor-field">
                 <input id="DocumenList_1___FILENAME" name="DocumenList[1]._FILENAME" type="file"></input>         
  </div>
  </td>
  </tr>
  </table>
  </div>--%>
  
  <div style="width:900px; clear:both; text-align:center; padding-top:20px;">
   

  <span style="clear:both;" class="btn_pack medium icon"><span class="check"></span><input id="registersummit" type="button" value="SAVE" /></span>
  </div>  

  </form>

    <div id="modal_reason" title="Inactive reason" style="text-align:center;">
            <form name="submit_active" id="submit_active" >
                <table>
                <tr>
                <td><textarea id="text_reason" name="text_reason" cols="50" rows="4"></textarea></td>
                </tr>
                </table>
			<span class="btn_pack medium icon"><span class="check"></span><input id="submit_button" type="button" value="Submit" /></span>
             <input id="selected_materials" type="hidden" name="selected_materials" size="10"/>
             <input id="selected_suppliers" type="hidden" name="selected_suppliers" size="10" />          
            </form>
    </div>
 
<!--expired date 자동입력과 선택된 supplier및 material 설정-->
<script type="text/javascript">

    $('.datepicker').change(function () {
        var index = $(this).index('.datepicker');

        var expired_date = GetNextYear($(this).val());
        var expired_date_Month = GetNextMonth($(this).val());

        var type = jQuery("#DocumenList_0___FILECATEGORY").val();

        //alert(expired_date);
        //alert(expired_date_Month);

        if ($(this).val() == "") {
            $('.expiredate').eq(index).val("");
        }
        else if (type == "Conflict Mineral") {
            //$('.expiredate').eq(index).val(expired_date_Month);
            $('.expiredate').eq(index).val(expired_date);
        }
        else {
            $('.expiredate').eq(index).val(expired_date);
        }


    });
    select_supplier = function (ids) {
        var s_code = jQuery("#single").getCell(ids, 'SUPPLIERCODE').toString();
        var s_name = jQuery("#single").getCell(ids, 'SUPPLIERNAME').toString();
        var s_Representative = jQuery("#single").getCell(ids,'CONTACT').toString();
        var s_mail = jQuery("#single").getCell(ids, 'ACTIVE').toString();
        $('#s_name').val(s_name);
        $('#s_code').val(s_code);

    }
    select_supplier_material = function () {
        var selected_materials;
        var selected_materials_partnum;
        var selected_suppliers;
        var selected_suppliers_code;

        selected_materials_partnum = "";
        selected_materials = jQuery("#list_material").jqGrid('getGridParam', 'selarrrow');

        selected_suppliers_code = "";
        selected_suppliers = jQuery("#single").jqGrid('getGridParam', 'selarrrow');


        $.each(selected_suppliers, function (i) {
            selected_suppliers_code += "'";
            selected_suppliers_code += jQuery("#single").getCell(selected_suppliers[i], 'SUPPLIERCODE').toString();
            selected_suppliers_code += "'";

            if (i < selected_suppliers.length - 1) {
                selected_suppliers_code += ",";
            }

        });


        $.each(selected_materials, function (i) {

            selected_materials_partnum += "'";
            selected_materials_partnum += jQuery("#list_material").getCell(selected_materials[i], 'PARTNUM').toString();
            selected_materials_partnum += "'";

            if (i < selected_materials.length - 1) {
                selected_materials_partnum += ",";
            }

        });

        $('#selected_suppliers').val(selected_suppliers_code);
        $('#selected_materials').val(selected_materials_partnum);
    }

</script>
<!--Supplier 및 Material 활성화 비활성화 Ajax script  -->
<!--1:N 폼 만들기-->
<script type="text/javascript">

    //자바스크립트 1:N 입력폼 만들기
    /*
    1:N 관계에서 N의 item을 입력폼을 생성과 제거 하는 jquery 
    */

    // items : items들의 담고 있는 set의 클래스 명
    // Removeitem : item 삭제 버튼의 클래스 명, items 안에 있어야 함
    // AddItem : item 추가 버튼의 id명

    //item을 삭제할 때
    $('.RemoveItem').click(function () {   //Remove 클릭할때

        var removeItem = $(this).parent()                  //삭제 할 아이템
        var removeItemParent = removeItem.parent();        //삭제 할 아이템을 감싸고 있는 DIV
        var removeItemclass = "." + removeItem.attr("class");

        if (removeItemParent.find(removeItemclass).length == 1) {
            alert("더 이상 삭제 할 수 없습니다.")
        }
        else {
            removeItem.remove();                                           //아이템 제거
            removeItemParent.find(removeItemclass).each(function (i) {     // 삭제와 추가로 인한 다시 아이템 배열 인덱스 번호 정리 
                $(this).find('[name*="["]').each(function () {             //아이템 안에 자식요소들을 각각 확인 --> name속성 안에 '['값이 있는 것들 찾기
                    var names = $(this).attr("name");                      //바뀌기 전의 네임속성의 값

                    // 대괄호 숫자 뽑아내기
                    var array_names_left = names.split('[');
                    var array_names_right = array_names_left[1].split(']')
                    var index = array_names_right[0];                  //바뀌기 전의 아이템 네임 속성의 배열 인덱스번호 가져오기 -> 문자열 중 숫자 뽑기
                    var newNames = names.split(index).join(i);         // 배열 인덱스 번호를 순서 맞게 바꾼 값

                    $(this).attr('name', newNames);                        // 정리된 익덱스 번호 값을 갖는 네임속성에 할당
                    $(this).attr('id', newNames);
                });
            });


        }


        //세부 사항 
        $(".datepicker").datepicker('destroy');
        $(".datepicker").datepicker($.datepicker.regional['ko']);
        $(".datepicker").datepicker("option", "dateFormat", 'yy-mm-dd');


    });
    //item을 추가할 때
    $('.AddItem').click(function () {

        var parentDiv = $(this).parent();          //N아이템의 상위 div
        var preitem = $(this).prev();              // 복사할 아이템
        var newItems = $(this).prev().clone(true); // 복사된 아이템

        newItems.insertAfter(preitem); // 복사한 item을 그 전의 마지막 item 다음으로 위치시킨다.

        newItems.find('[name*="["]').each(function () {            // item의 자식들 중에서 name 속성에 [가 있는 것들을 찾아 반복문을 사용한다.
            $(this).val('');                                       // 복사 되어 그 전에 있던 값들 없앤다.
        });

        /*name 속성 값을 0,1,2,3 식으로 정리*/
        var className = "." + newItems.attr("class");
        parentDiv.find(className).each(function (i) {              // 삭제와 추가로 인한 다시 아이템 배열 인덱스 번호 정리 
            $(this).find('[name*="["]').each(function () {         //아이템 안에 자식요소들을 각각 확인 --> name속성 안에 '['값이 있는 것들 찾기
                var names = $(this).attr("name");                  //바뀌기 전의 네임속성의 값

                // 대괄호 숫자 뽑아내기
                var array_names_left = names.split('[');
                var array_names_right = array_names_left[1].split(']')
                var index = array_names_right[0];                  //바뀌기 전의 아이템 네임 속성의 배열 인덱스번호 가져오기 -> 문자열 중 숫자 뽑기
                var newNames = names.split(index).join(i);         // 배열 인덱스 번호를 순서 맞게 바꾼 값

                $(this).attr('name', newNames);                    // 정리된 익덱스 번호 값을 갖는 네임속성에 할당
                $(this).attr('id', newNames);
            });
        });

        //세부 사항 
        $(".datepicker").datepicker('destroy');
        $(".datepicker").datepicker($.datepicker.regional['ko']);
        $(".datepicker").datepicker("option", "dateFormat", 'yy-mm-dd');

    });
</script>

    </script>
    <script type="text/javascript">
    $("#modal_reason").dialog({
        autoOpen: false,
        height: 160,
        width: 310,
        modal: true
    });
    $('#modal_reason').dialog('close');
    // 다이얼로그 닫았을 때
    $("#modal_reason").bind("dialogclose", function (event, ui) {
        $('#text_reason').val('');
        $(this).dialog({
            autoOpen: false,
            height: 160,
            width: 310,
            modal: true
        });
    });

    $('#activate_supplier').click(function () {
        select_supplier_material();
        if ($('#selected_suppliers').val() == "") {
            alert("Supplier를 선택하세요!");
            return false;
        }

        var confrinvalue = confirm("활성화를 하시겠습니까?");
        if (confrinvalue == false) {

            return false;
        }

        updatebyajax("Active_supplier");
    });

    $('#activate_material').click(function () {
        select_supplier_material();
        if ($('#selected_materials').val() == "") {
            alert("Material을 선택하세요!");
            return false;
        }

        var confrinvalue = confirm("활성화를 하시겠습니까?");
        if (confrinvalue == false) {

            return false;
        }

        updatebyajax("Active_material");
    });

    $('#inactivate_supplier').click(function () {
        select_supplier_material();
        if ($('#selected_suppliers').val() == "") {
            alert("Supplier를 선택하세요!");
            return false;
        }
        beginbutton = "inactivate_supplier";

        $("#modal_reason").dialog('open');
    });

    $('#inactivate_material').click(function () {
        select_supplier_material();
        if ($('#selected_materials').val() == "") {
            alert("Material을 선택하세요!");
            return false;
        };
        beginbutton = "inactivate_material";
        
        $("#modal_reason").dialog('open');
    });

    $('#submit_button').click(function () {
        select_supplier_material();
        var action = "";

        if (beginbutton == "inactivate_supplier") {
            if ($('#selected_suppliers').val() == "") {
                alert("Supplier를 선택하세요!");
                return false;
            };
            action = "Inactive_supplier";
        } else if (beginbutton == "inactivate_material") {
            if ($('#selected_materials').val() == "") {
                alert("Material을 선택하세요!");
                return false;
            }
            action = "Inactive_material";
        }

        var confrinvalue = confirm("비활성화를 하시겠습니까?");
        if (confrinvalue == false) {

            return false;
        }

        updatebyajax(action);
    });

    function updatebyajax(action) {
        $.ajax({ url: '/gps/Entity/' + action,
            type: 'post',
            data: ({
                supplier_code: suppliercode,
                selected_supplier: $('#selected_suppliers').val(),
                selected_material: $('#selected_materials').val(),
                disablereason: $('#text_reason').val()
            }),
            cache: false,
            beforeSend: function () {
            },
            success: function (data) {


                switch (action) {

                    case "Active_supplier":
                        {
                            
                            $('#text_reason').val('');
                            $('#modal_reason').dialog('close');
                            jQuery("#single").jqGrid('setGridParam', { datatype: 'json' });
                            jQuery("#single").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSuppliersDataToUpdate' }).trigger('reloadGrid');
                            alert("수정되었습니다.");
                        } break;
                    case "Active_material":
                        {
                            
                            $('#text_reason').val('');
                            $('#modal_reason').dialog('close');
                            jQuery("#list_material").jqGrid('setGridParam', { datatype: 'json' });
                            jQuery("#list_material").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSuppliersMaterialsToUpdate/' + suppliercode }).trigger('reloadGrid');
                            alert("수정되었습니다.");
                        } break;
                    case "Inactive_supplier":
                        {
                       
                            $('#text_reason').val('');
                            $('#modal_reason').dialog('close');
                            jQuery("#single").jqGrid('setGridParam', { datatype: 'json' });
                            jQuery("#single").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSuppliersDataToUpdate' }).trigger('reloadGrid');
                            alert("수정되었습니다.");
                        } break;
                    case "Inactive_material":
                        {
                            
                            $('#text_reason').val('');
                            $('#modal_reason').dialog('close');
                            jQuery("#list_material").jqGrid('setGridParam', { datatype: 'json' });
                            jQuery("#list_material").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSuppliersMaterialsToUpdate/' + suppliercode }).trigger('reloadGrid');
                            alert("수정되었습니다.");
                        } break;
                    default: 
                        {
                            
                            $('#text_reason').val('');
                            $('#modal_reason').dialog('close');
                            alert("수정되었습니다.");
                        } break;
                }

            }, error: function (status) {
                alert(status);
                alert('timeout.');
            }
        });
    }
</script>
<!--Jqgrid mulit search 버그 해결 코드  -->
<script type="text/javascript">

//이메일 주소 확인 vailidation
    function checkEmail(value, name) {
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(value)) {
            return [false, "올바른 이메일 주소가 아닙니다."];
        } else {
            return [true, ""];
        }
    }


    function GetNextYear(val1) {  
        var FORMAT = "-";
        var nextyeardate = "";
        // FORMAT을 포함한 길이 체크
        if (val1.length != 10)
            return null;
        // FORMAT이 있는지 체크
        if (val1.indexOf(FORMAT) < 0)
            return null;
        // 년도, 월, 일로 분리
        var start_dt = val1.split(FORMAT);        
        var nexyear = Number(start_dt[0]);        
        nexyear = nexyear + 1;
        nextyeardate = nexyear.toString() + "-" + start_dt[1] + "-" + start_dt[2];
        return nextyeardate;
    }

    function GetNextMonth(val1) {
        var FORMAT = "-";
        var nextyeardate = "";
        // FORMAT을 포함한 길이 체크
        if (val1.length != 10)
            return null;
        // FORMAT이 있는지 체크
        if (val1.indexOf(FORMAT) < 0)
            return null;
        // 년도, 월, 일로 분리
        var start_dt = val1.split(FORMAT);

        var Month = Number(start_dt[1]);
        nextMonth = Month + 6;

        var nexyear = Number(start_dt[0]);
        nexyear = nexyear + 1;

        // 12달이 넘어가면 해가 바뀌기 때문에 
        if (nextMonth == 13) {
            nextMonthdate = nexyear.toString() + "-" + "01" + "-" + start_dt[2];

            return nextMonthdate;
        }
        if (nextMonth == 14) {
            nextMonthdate = nexyear.toString() + "-" + "02" + "-" + start_dt[2];

            return nextMonthdate;
        }
        if (nextMonth == 15) {
            nextMonthdate = nexyear.toString() + "-" + "03" + "-" + start_dt[2];

            return nextMonthdate;
        }
        if (nextMonth == 16) {
            nextMonthdate = nexyear.toString() + "-" + "04" + "-" + start_dt[2];

            return nextMonthdate;
        }
        if (nextMonth == 17) {
            nextMonthdate = nexyear.toString() + "-" + "05" + "-" + start_dt[2];

            return nextMonthdate;
        }
        if (nextMonth == 18) {
            nextMonthdate = nexyear.toString() + "-" + "06" + "-" + start_dt[2];

            return nextMonthdate;
        }
        if (Month == "4" || Month == "4" || Month == "5") // 4,5,6 월은 달수가 2자리이기 때문에 받아온 값을 그대로 넣어주고
        {
            nextMonthdate = start_dt[0] + "-" + nextMonth.toString() + "-" + start_dt[2];
            return nextMonthdate;
        }

        //4,5,6을 제외한 달은 달의 자리수가 2자리이기 때문에 달수앞에 0 을 붙인다.
        nextMonthdate = start_dt[0] + "-" + "0" + nextMonth.toString() + "-" + start_dt[2];

        return nextMonthdate;
    }


    function resetgrid() {
        $('#add_list_contact').hide();
    }
    //multiple search bug solution
    jQuery.event.special.click = {
        setup: function () {
            if (jQuery(this).hasClass("ui-search")) {
                jQuery(this).bind("click", jQuery.event.special.click.handler);
            }
            return false;
        },
        teardown: function () {
            jQuery(this).unbind("click", jQuery.event.special.click.handler);
            return false;
        },
        handler: function (event) {
            jQuery(".ui-searchFilter td.ops select").filter(function () { return $(this).css("display") != "none"; }).attr("name", "op");
        }
    };

</script>
<!-- 사용자 입력값 validation -->
<script type="text/javascript">


    $('#registersummit').click(function () {
        var validation_supplier = $('#s_name').val();
        // var validation_issudedate1 = $('#DocumenList_0___ISSUEDATE').val();

        var validation_issudedate1 = $('#expired_date').val();
        //var validation_issudedate2 = $('#DocumenList_1___ISSUEDATE').val();
        var validation_file1 = $('#DocumenList_0___FILENAME').val();
        //var validation_file2 = $('#DocumenList_1___FILENAME').val();
        var validation = true;

        if (validation_supplier == "") {
            alert("Supplier를 선택하세요 ");
            return false;
        }


        if ((validation_file1 != "" && validation_issudedate1 != "")){ //|| (validation_file2 != "")) {

        } else {
            alert("Issue date와 업로드파일을 확인하세요 ");
            return false;
        }

        if (validation_issudedate1 != "") {

            if (validation_file1 == "") {
                alert("파일을 선택하세요 ");
                return false;
            }
        }
//        if (validation_issudedate2 != "") {
//            if (validation_file2 == "") {
//                alert("파일을 선택하세요 ");
//                return false;
//            }
//        }
        if (validation_file1 != "") {
            if (validation_issudedate1 == "") {
                alert("Issued date를 선택하세요 ");
                return false;
            }
        }


        var confrinvalue = confirm("제출하시겠습니까?");
        if (confrinvalue == false) {

            return false;
        }


        $('#register_form').submit();
    });

   
</script>
</asp:Content>
