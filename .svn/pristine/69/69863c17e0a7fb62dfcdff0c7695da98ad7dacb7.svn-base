<%@ Page Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DocumentFolder
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h2><%: ViewData["Message"] %></h2>--%>

<style type="text/css">
.AlertReturn { background: #fc8294 url(images/ui-bg_glass_75_d0e5f5_1x400.png) 50% 50% repeat-x; }
.WarnReturn { background: #cff5b7 url(images/ui-bg_glass_75_d0e5f5_1x400.png) 50% 50% repeat-x; }
.ui-jqgrid {font-size:10px;}
table
{
    font-size:11px;
}
 </style>


 
     <table style="width:700px;height:30px;" >
        <tr>
            <td style="width:10%;" ><p style="font-weight: bold ;font-size: 1.5em;" ><%: ViewData["Message"] %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p></td>
            <td style="width:5%;" class="WarnReturn"></td>
            <td style="width:12%;" >만료 10일전</td>
            <td style="width:5%;" class="AlertReturn"></td>
            <td style="width:10%;" >만료됨</td>
            <td style="width:18%;" >(Assy)Prod.Name</td>
            <td style="width:30%;"><input type ="text" id="productname" style="width:100%;" class ="form-control"/></td>
            <td style="width:10%;"><button type="button" class="btn btn-info btn-fill" id="searchproductname">검색</button></td>
                                                                                               
        </tr>
    </table>

    
  <!--document list table-->
  <div style="text-align:center; padding-right:0px;">
  <table id="single" class="scroll" cellpadding="0" cellspacing="0"></table>
  <div id="pager" class="scroll" style="text-align:center;"></div>
  </div>

  <!--[2021.03.17] FileDownload를 위해추가. -->
    <form method="post" id="downloadform" action="/gps/Menu/Download">
        <input type="hidden" id="suppliercode" name="suppliercode" />
        <input type="hidden" id="category" name="category" />
        <input type="hidden" id="filename" name="filename" />
    </form>



 <script type="text/javascript">

     $(function () {
         $.datepicker.setDefaults($.datepicker.regional[""]);
         $(".datepicker").datepicker($.datepicker.regional['ko']);
         $(".datepicker").datepicker("option", "dateFormat", 'yy-mm-dd');

     });

     
</script>

 <script type="text/javascript">

     var today = new Date();
     var warn_period = 10; // 경고 표시 기준
     var now = new Date(today.valueOf());
     //날짜 조합
     var year = now.getFullYear();
     var mon = (now.getMonth() + 1) > 9 ? '' + (now.getMonth() + 1) : '0' + (now.getMonth() + 1);
     var day = now.getDate() > 9 ? '' + now.getDate() : '0' + now.getDate();
     //기준일
     var today_date = year + '-' + mon + '-' + day;
     
     jQuery(function () {

         jQuery("#single").jqGrid({
             url: '<%= Url.Action("GetGPSDocument", "Entity") %>',
             contentType: "application/json; charset=utf-8",
             datatype: 'json',
             mtype: 'POST',
             colNames: ['FILEID', 'SUPPLIERCODE', 'SUPPLIERNAME', 'MATERIALNAME', 'PARTNUM', 'FILENAME', 'FILECATEGORY', 'ISSUEDATE', 'EXPIREDATE', 'ACTIVE'],
             // 컬럼 모델 셋팅 하기
             colModel: [
                  { name: 'FILEID', index: 'FILEID', width: 50, align: 'center', hidden: true, searchoptions: { sopt: ['cn', 'eq', 'ne'] }, search: false },
                  { name: 'SUPPLIERCODE', index: 'SUPPLIERCODE', width: 120, align: 'center', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne']} },
                  { name: 'SUPPLIERNAME', index: 'SUPPLIERNAME', width: 120, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'MATERIALNAME', index: 'MATERIALNAME', width: 120, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'PARTNUM', index: 'PARTNUM', width: 100, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'FILENAME', index: 'FILENAME', width: 90, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'FILECATEGORY', index: 'FILECATEGORY', width: 120, align: 'center', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne'] }, stype: 'select', searchoptions: { sopt: ['eq'], value: 'Conflict Mineral:Conflict Mineral;MSDS:MSDS;Non-use Letter:Non-use Letter;Test Report:Test Report;Warranty letter:Warranty letter;ALL:전체선택', defaultValue: 'ALL'} },
                  { name: 'ISSUEDATE', index: 'ISSUEDATE', width: 92, align: 'center', searchoptions: { dataInit: datePick, sopt: ['eq', 'ne', 'cn', 'lt', 'le', 'gt', 'ge']} },
                  { name: 'EXPIREDATE', index: 'EXPIREDATE', width: 92, align: 'center', searchoptions: { dataInit: datePick, sopt: ['eq', 'ne', 'cn', 'lt', 'le', 'gt', 'ge']} },
                  { name: 'ACTIVE', index: 'ACTIVE', width: 70, align: 'center', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne'] }, stype: 'select', searchoptions: { sopt: ['eq'], value: 'T:T;F:F;ALL:전체선택', defaultValue: 'T'} }
                  ],

             rowNum: 20,
             rowList: [20, 50, 100],
             viewrecords: true,
             height: 480,
             sortorder: "desc",
             pager: jQuery('#pager'),
             emptyrecords: "Nothing to display",
             sortname: 'FILEID',
             caption: 'Supplier List',
             afterInsertRow: dateCheck,
             onCellSelect: function (rowid, iCol, cellcontent) {
                 var filename = jQuery("#single").getCell(rowid, 5).toString();
                 //[2021.03.17] FileDownload를 위해추가(변경)
                 downloadfile(jQuery("#single").getCell(rowid, 1).toString(), jQuery("#single").getCell(rowid, 6).toString(), filename);
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
     });

     //[2021.03.17] FileDownload를 위해추가.
     function downloadfile(supplier, category, filename) {
         //form 으로 값 전송
         $("#suppliercode").val(supplier);
         $("#category").val(category);
         $("#filename").val(filename);

         var form = $("#downloadform");
         form.submit();
     }


</script>

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
    reload_grid = function (xhr, status, error) {
        jQuery("#refresh_single").click();
    }
    select_supplier = function (ids) {
        var s_code = jQuery("#single").getCell(ids, 0).toString();
        var s_name = jQuery("#single").getCell(ids, 1).toString();
        var s_Representative = jQuery("#single").getCell(ids, 2).toString();
        var s_mail = jQuery("#single").getCell(ids, 3).toString();
        $('#s_name').val(s_name);
        $('#s_code').val(s_code);
        $('#s_Representative').val(s_Representative);
        $('#s_mail').val(s_mail);
    }
    datePick = function (elem) {
        $.datepicker.setDefaults($.datepicker.regional[""]);
        $(elem).datepicker($.datepicker.regional['ko']);
        $(elem).datepicker("option", "dateFormat", 'yy-mm-dd');
    }


</script>
<script type="text/javascript">

    dateCheck = function (rowid, rowdata, rowelem) {

        var DateToUpdate = jQuery("#single").getCell(rowid, 'EXPIREDATE').toString();
        var Document_type = jQuery("#single").getCell(rowid, 'FILECATEGORY').toString();
        var NumofDayLeft = getDateDiff(today_date.toString(), DateToUpdate);

        if (NumofDayLeft < warn_period && Document_type != "MSDS" && Document_type != "Non-use Letter" && Document_type != "Warranty letter") {
            jQuery("#single").setRowData(rowid, '', 'WarnReturn');
            if (NumofDayLeft < 0) {
                jQuery("#" + rowid).removeClass('WarnReturn');
                jQuery("#single").setRowData(rowid, '', 'AlertReturn');
            }

        }
    }
    function getDateDiff(val1, val2) {
        var FORMAT = "-";
        // FORMAT을 포함한 길이 체크
        if (val1.length != 10 || val2.length != 10)
            return null;
        // FORMAT이 있는지 체크
        if (val1.indexOf(FORMAT) < 0 || val2.indexOf(FORMAT) < 0)
            return null;
        // 년도, 월, 일로 분리
        var start_dt = val1.split(FORMAT);
        var end_dt = val2.split(FORMAT);
        // 월 - 1(자바스크립트는 월이 0부터 시작하기 때문에...)
        // Number()를 이용하여 08, 09월을 10진수로 인식하게 함.
        start_dt[1] = (Number(start_dt[1]) - 1) + "";
        end_dt[1] = (Number(end_dt[1]) - 1) + "";
        var from_dt = new Date(start_dt[0], start_dt[1], start_dt[2]);
        var to_dt = new Date(end_dt[0], end_dt[1], end_dt[2]);
        return (to_dt.getTime() - from_dt.getTime()) / 1000 / 60 / 60 / 24;

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




 <script type="text/javascript">

     //[2021.03.29] ProductName 입력엔터키 처리.
     $("#productname").keypress(function (e) {         
         if (e.keyCode == 13) { //text 입력 후 엔터키  눌렀다면            
             $("#searchproductname").click(); //검색 창이 클릭됨
             return false;
         }
     });

     //[2021.03.29] ProductName으로 조회시수행.
     // ProductName이ㅣ 입력되지 않으면 기본 쿼리 수행.
     $("#searchproductname").click(function () {
         var dataurl = '<%= Url.Action("GetGPSDocument", "Entity") %>';
         var productname = "";
         
         productname = $('#productname').val();
         if (productname == null || productname.replace(/(^\s*)|(\s*$)/g, "") == "") {
         } else {
             dataurl = dataurl + "?productname=" + productname.replace();
         }         
         jQuery("#single").jqGrid('setGridParam', {
             url: dataurl
         }).trigger("reloadGrid");
     });
 </script>
</asp:Content>