<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server" >
유해물질 문서 목록
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style type="text/css">
.ui-jqgrid {font-size:10px;}

table
{
    font-size:11px;
}
 </style>

<table style="width:900px;">

<tr>
<td style="width:30%;" >
<p style="font-weight: bold;font-size: 20px;" ><%: ViewData["Message"] %></p>
</td>

<td>
<a href="/gps/Hazardous/ViewHazardousMaterial"><img src="/gps/Content/images/Hazardous_Register.png" 
alt="Register" width="130px;" height="40px;" align="right" /></a>
</td>
    
</tr>
</table>
<br />

<!--Request list table-->
<div style="text-align:center; padding-right:0px;">
<table id="single" class="scroll" cellpadding="0" cellspacing="0"></table>
<div id="pager" class="scroll" style="text-align:center;"></div>
</div>

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


    function grid_hmreqid_db_click(rowid) {

        var sURL = "";
        var hmreqid = jQuery("#single").getCell(rowid, 'HMREQID').toString();

        var sURL = '<%= Url.Action("ViewHazardousMaterialDetail","Hazardous") %>' + '/' + hmreqid;

        location.href = sURL;

    }




    jQuery(function () {

        jQuery("#single").jqGrid({

            url: '<%= Url.Action("ViewRequestList", "Hazardous") %>',
            contentType: "application/json; charset=utf-8",
            datatype: 'json',
            mtype: 'POST',
            colNames: ['요청ID', '요청날짜', '요청 고객사', '요청인', '요청 항목', '요청 내용', 'Lead Time', '예상 완료', '상태', '담당자', '대기'],
         
            //컬럼 모델 setting
            colModel: [

                { name: 'HMREQID', index: 'HMREQID', width: 35, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne'] }, search: false },
                { name: 'REQUESTDATE', index: 'REQUESTDATE', width: 22, align: 'left', searchoptions: { dataInit: datePick, sopt: ['eq', 'ne', 'cn', 'lt', 'le', 'gt', 'ge']} },
                { name: 'CUSTOMER', index: 'CUSTOMER', width: 30, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                { name: 'REQUESTUSERNAME', index: 'REQUESTUSERNAME', width: 17, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                { name: 'HAZARDOUSMATERIALTYPE', index: 'HAZARDOUSMATERIALTYPE', width: 25, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                { name: 'REQUESTCOMMENT', index: 'REQUESTCOMMENT', width: 100, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                { name: 'LEADTIME', index: 'LEADTIME', width: 30, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']}, hidden: true  },
                { name: 'EXPECTEDFINISHDATE', index: 'EXPECTEDFINISHDATE', width: 23, align: 'left', searchoptions: { dataInit: datePick, sopt: ['eq', 'ne', 'cn', 'lt', 'le', 'gt', 'ge']} },
                { name: 'STATUS', index: 'STATUS', width: 20, align: 'left', align: 'left', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne'] },
                 stype: 'select', searchoptions: { sopt: ['eq'], value: 'Open:Open;Process:Process;Close:Close;Delete:Delete;ALL:전체선택', defaultValue: 'ALL' }}, 
                { name: 'ADMINUSERNAME', index: 'ADMINUSERNAME', width: 17, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                { name: 'NO', index: 'NO', width: 10, align: 'center', align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne'] }, hidden: true }
             
             
                ],

            rowNum: 20,
            rowList: [20, 50, 100],
            viewrecords: true,
            width: 900,
            height: 480,
            sortorder: "desc",
            pager: jQuery('#pager'),
            emptyrecords: "Nothing to display",
            sortname: 'hmreqid',
            caption: '유해물질 문서 목록',
            ondblClickRow: grid_hmreqid_db_click


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
</script>


<script type="text/javascript">
    $('.datepicker').change(function () {
        var index = $(this).index('.datepicker');
        var request_date = GetYear($(this).val());

        if ($(this).val() == "") {
            $('.requestdate').eq(index).val("");
        } else {
            $('.requestdate').eq(index).val(request_date);
        }

    });

    reload_grid = function (xhr, status, error) {
        jQuery("#refresh_single").click();
    }

    datePick = function (elem) {
        $.datepicker.setDefaults($.datepicker.regional[""]);
        $(elem).datepicker($.datepicker.regional['ko']);
        $(elem).datepicker("option", "dateFormat", 'yy-mm-dd');
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











</asp:Content>
