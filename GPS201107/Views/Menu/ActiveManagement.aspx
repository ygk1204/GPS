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
          $(".datepicker").datepicker({
              changeMonth: true,
              changeYear: true
          });
          $(".datepicker").datepicker("option", "dateFormat", 'yy-mm-dd');

      });
</script>
 <script type="text/javascript">

     jQuery(function () {


         jQuery("#single").jqGrid({
             url: '<%= Url.Action("GetGPSuppliersDataToUpdate", "Entity") %>',
             contentType: "application/json; charset=utf-8",
             datatype: 'json',
             mtype: 'POST',
             colNames: ['Code', 'Name', 'Representative', 'Mail','Active','disablereason'],

             // 컬럼 모델 셋팅 하기
             colModel: [
                  { name: 'SUPPLIERCODE', index: 'SUPPLIERCODE', width: 100, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'SUPPLIERNAME', index: 'SUPPLIERNAME', width: 200, align: 'left', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne']} },
                  { name: 'CONTACT', index: 'CONTACT', width: 100, align: 'left', hidden: true, searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'EMAIL', index: 'EMAIL', width: 100, align: 'left', hidden: true, searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'ACTIVE', index: 'ACTIVE', width: 70, align: 'center', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne'] }, stype: 'select', searchoptions: { sopt: ['nc'], value: 'F:T;T:F;ALL:전체선택', defaultValue: 'ALL'} },
                  { name: 'DISABLEREASON', index: 'DISABLEREASON', width: 100, hidden: true, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} }
                  ],
             scroll: 1,
             ignoreCase: true,
             rowNum: 20,
             rowList: [20, 50, 100],
             gridview: true,
             viewrecords: true,
             height: 300,
             cache: false,
             sortorder: "desc",
             pager: jQuery('#pager'),
             emptyrecords: "Nothing to display",
             loadonce: true,

             caption: 'Supplier List',

             onSelectRow: function (ids) {

                 if (ids == null) {

                     ids = 0;
                     if (jQuery("#list10_d").jqGrid('getGridParam', 'records') > 0) {
                         jQuery("#list10_d").jqGrid('setGridParam', { url: '/Entity/GetGPSuppliersMaterials/' + jQuery("#single").getCell(ids, 0).toString() });
                         jQuery("#list10_d").jqGrid('setGridParam', { datatype: 'json' });
                         jQuery("#list10_d").jqGrid('setCaption', "Invoice Detail: " + jQuery("#single").getCell(ids, 1).toString().substr(0, 8)).trigger('reloadGrid');
                         select_supplier(ids);
                     }
                 } else {

                     jQuery("#list10_d").jqGrid('setGridParam', { url: '/Entity/GetGPSuppliersMaterials/' + jQuery("#single").getCell(ids, 0).toString() });
                     jQuery("#list10_d").jqGrid('setGridParam', { datatype: 'json' });
                     jQuery("#list10_d").jqGrid('setCaption', "Invoice Detail: " + jQuery("#single").getCell(ids, 1).toString().substr(0, 8)).trigger('reloadGrid');
                     select_supplier(ids);
                 }
             }
         });

         $("#single").jqGrid('filterToolbar', { stringResult: true, searchOnEnter: true, defaultSearch: "cn" });
         jQuery("#single").navGrid('#pager', { view: false, del: false, add: false, edit: false },
       {}, // default settings for edit
       {}, // default settings for add
       {}, // delete instead that del:false we need this
       {closeOnEscape: true, multipleSearch: true, closeAfterSearch: true }, // search options
       {} /* view parameters*/
     );
         jQuery("#list10_d").jqGrid({
             url: '/Entity/GetGPSuppliersMaterials/',
             height: 320,
             datatype: 'local',
             mtype: 'POST',
             colNames: ['MATETIALNAME', 'PARTNUM'],
             colModel: [
   		{ name: 'MATETIALNAME', index: 'MATETIALNAME', width: 180 },
   		{ name: 'PARTNUM', index: 'PARTNUM', width: 130 }

   	],
             scroll: 1,
             rowNum: 10,
             rowList: [5, 10, 20],
             pager: '#pager10_d',
             sortname: 'PARTNUM',
             viewrecords: true,
             sortorder: "asc",
             multiselect: true,
             gridview: true,
             ignoreCase: true,
             loadonce: true,
             caption: "Device Detail"
         }).navGrid('#pager10_d', { add: false, edit: false, del: false });

     });


    </script>
  
  <!--Supplier list table & Material number list table-->
  <div style="height:450px; width: 900px;">
  
  <!--Supplier list table-->
  <div style="float:left; width:500px;">
  <table id="single" class="scroll" cellpadding="0" cellspacing="0"></table>
  <div id="pager" class="scroll" style="text-align:center;"></div>

  </div>
  



  <!--Material number list table-->
  <div style="float:left; padding-left:30px; width:200px; ">
  <table id="list10_d" ></table>
  <div id="pager10_d"></div>
  </div>
  
  </div>

 <form id="activate_form" name="activate_form" method="post" enctype="multipart/form-data" action="/menu/Register_action">

  <!--Supplier informantion table-->
  <table class="itemhead" style="width:800px">
  <tr>
  <td style="width:20%;background-color:#cff5b7; font-weight:bold;">Inactive reason</td>
  <td colspan="3"><textarea id="s_disablereason" name="s_disablereason" rows='3' cols="50" ></textarea></td>
  </tr>
  </table>

  <input id="s_name" name="s_name" size="15" readonly="readonly"/>
  <input id="s_code" name="s_code" size="15" readonly="readonly"/>
  <input id="s_Representative" name="s_Representative" size="10" readonly="readonly"/>
  <input id="s_mail" name="s_mail" size="15" readonly="readonly"/>
  <input id="activeflag" name="activeflag" size="15" readonly="readonly"/>
  <input id="selected_device" name="selected_device" size="10" readonly="readonly" />
 
  <div style="width:900px; text-align:center; padding-top:20px;">
  <span style="clear:both;" class="btn_pack medium icon"><span class="check"></span><input id="activate_supplier" type="button" value="Activate Supplier" /></span>
  <span style="clear:both;" class="btn_pack medium icon"><span class="refresh"></span><input id="inactivate_supplier" type="button" value="Inactivate Supplier" /></span>
  <span style="clear:both;" class="btn_pack medium icon"><span class="check"></span><input id="activate_material" type="button" value="Activate material" /></span>
  <span style="clear:both;" class="btn_pack medium icon"><span class="refresh"></span><input id="inactivate_material" type="button" value="Inactivate material" /></span>
  </div>  
  </form>

 

<script type="text/javascript">

    $('.datepicker').change(function () {
        var index = $(this).index('.datepicker');
        var expired_date = GetNextYear($(this).val());

        if ($(this).val() == "") {
            $('.expiredate').eq(index).val("");
        } else {
            $('.expiredate').eq(index).val(expired_date);
        }

    });
    select_supplier = function (ids) {
        var s_code = jQuery("#single").getCell(ids, 0).toString();
        var s_name = jQuery("#single").getCell(ids, 1).toString();
        var s_Representative = jQuery("#single").getCell(ids, 2).toString();
        var s_mail = jQuery("#single").getCell(ids, 3).toString();
        var s_disablereason = jQuery("#single").getCell(ids, 5).toString();
        $('#s_name').val(s_name);
        $('#s_code').val(s_code);
        $('#s_Representative').val(s_Representative);
        $('#s_mail').val(s_mail);
        $('#s_disablereason').val(s_disablereason);
    }
    select_device = function () {
        var s;
        var k;
        k = "";
        s = jQuery("#list10_d").jqGrid('getGridParam', 'selarrrow');

        $.each(s, function (i) {
            
            k += jQuery("#list10_d").getCell(s[i], 2).toString();

            if (i < s.length - 1) {
                k += ",";
            }

        });
        $('#selected_device').val(k);
    }

</script>
<script type="text/javascript">
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

</script>
<script type="text/javascript">
    $('#activate_supplier').click(function () {
        var validation = true;

        var confrinvalue = confirm("Supplier를 활성화하시겠습니까?");
        if (confrinvalue == false) {

            return false;
        }

        if ($('#s_code').val() == "") {

            alert("Supplier를 선택하세요");
            return false;
        }

        $("#activate_form").attr("action", "/menu/activateSupplier_Action");
        $('#activate_form').submit();

    });
    $('#inactivate_supplier').click(function () {
        var validation = true;


        var confrinvalue = confirm("Supplier를 비활성화하시겠습니까?");
        if (confrinvalue == false) {

            return false;
        }

        if ($('#s_disablereason').val() == "") {
            alert("비활성화 사유를 쓰세요");
            return false;
        }

        $("#activate_form").attr("action", "/menu/inactivateSupplier_Action");
        $('#activate_form').submit();
    });
    $('#activate_material').click(function () {

        var validation = true;

        var confrinvalue = confirm("Material을 활성화하시겠습니까?");
        if (confrinvalue == false) {

            return false;
        }

        select_device();

        if ($('#s_code').val() == "") {

            alert("Supplier를 선택하세요");
            return false;
        }
        if ($('#selected_device').val() == "") {
            alert("비활설화 시킬 Material을 선택해주세요");
            return false;
        }


        $("#activate_form").attr("action", "/menu/activateMaterial_Action");
        $('#activate_form').submit();


    });
    $('#inactivate_material').click(function () {

        var validation = true;


        var confrinvalue = confirm("Material을 비활성화하시겠습니까");
        if (confrinvalue == false) {

            return false;
        }

        select_device();

        if ($('#s_code').val() == "") {

            alert("Supplier를 선택하세요");
            return false;
        }
        if ($('#selected_device').val() == "") {
            alert("비활설화 시킬 Material을 선택해주세요");
            return false;
        }

        if ($('#s_disablereason').val() == "") {
            alert("비활성화 사유를 쓰세요");
            return false;
        }

        $("#activate_form").attr("action", "/menu/inactivateMaterial_Action");
        $('#activate_form').submit();
    });
</script>
</asp:Content>
