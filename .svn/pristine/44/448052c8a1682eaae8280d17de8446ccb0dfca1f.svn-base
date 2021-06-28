<%@ Page Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DocumentFolder
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
<style type="text/css">
.ui-jqgrid {font-size:12px;}
.AlertReturn { background: #fc8294 url(images/ui-bg_glass_75_d0e5f5_1x400.png) 50% 50% repeat-x; }
.WarnReturn { background: #cff5b7 url(images/ui-bg_glass_75_d0e5f5_1x400.png) 50% 50% repeat-x; }

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
<!--파일 리스트 그리드-->
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

     $(function () {

         $("#single").jqGrid({
             url: '<%= Url.Action("GetGPSDocumentToUpdate", "Entity") %>',
             contentType: "application/json; charset=utf-8",
             datatype: 'json',
             mtype: 'POST',
             colNames: ['FILEID', 'SUPPLIERCODE', 'SUPPLIERNAME', 'MATERIALNAME', 'PARTNUM', 'FILENAME', 'FILECATEGORY', 'ISSUEDATE', 'EXPIREDATE'],

             // 컬럼 모델 셋팅 하기
             colModel: [
                  { name: 'FILEID', index: 'FILEID', width: 50, align: 'center', hidden: true, searchoptions: { sopt: ['cn', 'eq', 'ne'] }, search: false },
                  { name: 'SUPPLIERCODE', index: 'SUPPLIERCODE', width: 120,align: 'center', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne']} },
                  { name: 'SUPPLIERNAME', index: 'SUPPLIERNAME', width: 120, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'MATERIALNAME', index: 'MATERIALNAME', width: 120, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'PARTNUM', index: 'PARTNUM', width: 100, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'FILENAME', index: 'FILENAME', width: 90, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'FILECATEGORY', index: 'FILECATEGORY', width: 120, align: 'center', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne'] }, stype: 'select', searchoptions: { sopt: ['eq'], value: 'Conflict Mineral:Conflict Mineral;MSDS:MSDS;Non-use Letter:Non-use Letter;Test Report:Test Report;Warranty letter:Warranty letter;ALL:전체선택', defaultValue: 'ALL'} },
                  { name: 'ISSUEDATE', index: 'ISSUEDATE', width: 92, align: 'center', searchoptions: { dataInit: datePick, sopt: ['eq', 'ne', 'cn', 'lt', 'le', 'gt', 'ge']} },
                  { name: 'EXPIREDATE', index: 'EXPIREDATE', width: 92, align: 'center', searchoptions: { dataInit: datePick, sopt: ['eq', 'ne', 'cn', 'lt', 'le', 'gt', 'ge']} },
                  ],
             width: 900,   
             rowNum: 20,
             rowList: [20, 50, 100],
             viewrecords: true,
             height: 300,
             cache: false,
             multiselect: true,
             sortorder: "desc",
             pager: jQuery('#pager'),
             emptyrecords: "Nothing to display",
             sortname: 'FILEID',
             caption: 'Document list',
             afterInsertRow: dateCheck,
             ondblClickRow: function (rowid, iCol, cellcontent) {
                 location.href = "/gps/Menu/Download/" + jQuery("#single").getCell(rowid, 'SUPPLIERCODE').toString() + "/" + jQuery("#single").getCell(rowid, 'FILECATEGORY').toString() + "/" + jQuery("#single").getCell(rowid, 'FILENAME').toString();
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


    </script>
    <script type="text/javascript">
        reload_grid = function (xhr, status, error) {

            jQuery("#refresh_single").click();


        }
        //선택한 supplier를 사용자 입력폼에 보여줌
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
        //datepicker 사용하기
        datePick = function (elem) {
            $.datepicker.setDefaults($.datepicker.regional[""]);
            $(elem).datepicker($.datepicker.regional['ko']);
            $(elem).datepicker("option", "dateFormat", 'yy-mm-dd');
        }

        // 선택한 row에서 데이터 뽑아내기
        selected_rows = function () {

            var s;
            var k;
            var supplierscode;
            var categories;
            k = "";
            supplierscode = "";
            categories = "";
            s = jQuery("#single").jqGrid('getGridParam', 'selarrrow');

            $.each(s, function (i) {
                //파일 아이디가 ,를 구분자로 하여서 여러개가 들어감
                k += "'";
                k += jQuery("#single").getCell(s[i], 'FILEID').toString();
                k += "'";

                //supplier code가 ,를 구분자로 하여서 여러개가 들어감
                //categories가 ,를 구분자로 하여서 여러개가 들어감
                supplierscode += jQuery("#single").getCell(s[i], 'SUPPLIERCODE').toString();
                categories += jQuery("#single").getCell(s[i], 'FILECATEGORY').toString();


                if (i < s.length - 1) {
                    k += ",";
                    supplierscode += ",";
                    categories += ",";
                }
            });
            $('#delete_rows').val(k);
            $('#update_rows').val(k);
            $('#suppliercodes').val(supplierscode);
            $('#categories').val(categories);

        }


</script>
<script type="text/javascript">
//만료 10일전과 만료된 데이터 색깔 표시하는 자바스크립트
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
    
    // 중복 제거
    function arrayCheck(aaa) {
        var b = [];
        var j = 0;
        aaa.sort();
        while (aaa.length > 0) {
            var newKey = aaa.shift();
            if (j == 0) {
                b.push(newKey);
                j++;
            } else if (b[j - 1] != newKey) {
                b.push(newKey);
                j++;
            }
        }
        return b;

    }
    //날짜 차이구하는 함수
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
    // 내년 연도 구하는 함수
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
        if (Month == "4" || Month == "5" || Month == "6") // 4,5,6 월은 달수가 2자리이기 때문에 받아온 값을 그대로 넣어주고
        {
            nextMonthdate = start_dt[0] + "-" + nextMonth.toString() + "-" + start_dt[2];
            return nextMonthdate;
        }

        //4,5,6을 제외한 달은 달의 자리수가 2자리이기 때문에 달수앞에 0 을 붙인다.
        nextMonthdate = start_dt[0] + "-" + "0" + nextMonth.toString() + "-" + start_dt[2];

        return nextMonthdate;
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
 <!--사용자 입력란--> 
 <table style="width:300px;height:30px; float:left; margin-right:280px;" >
 <tr>
<td style="width:20%;" class="WarnReturn"></td>
<td style="width:40%;" >만료 10일전</td>
<td style="width:20%;" class="AlertReturn"></td>
<td style="width:40%;" >만료됨</td>
</tr>
</table>
  <span class="btn_pack medium icon"><span class="refresh"></span><input id="Expired_List" type="button" value="Load Expired Documents" /></span>
  <span class="btn_pack medium icon"><span class="refresh"></span><input id="All_List" type="button" value="Load All Documents" /></span>
  <!--document list table-->
  <div style="text-align:center; padding-right:0px;">
  <table id="single" class="scroll" cellpadding="0" cellspacing="0"></table>
  <div id="pager" class="scroll" style="text-align:center;"></div>
  </div>


  <form id="deleteform" name="deleteform" method="post" enctype="multipart/form-data" action="/gps/menu/Delete_action">
  <input id="delete_rows" name="delete_rows"  readonly="readonly" type="hidden" />
   <span id="delete_click"  style="margin-top:7px" class="btn_pack medium icon RemoveItem"><span class="delete"></span><button type="button">Delete selected documents</button></span>   
  </form>


  
  <br />

  <form id="updateform" name="updateform" method="post" enctype="multipart/form-data" action="/gps/menu/Update_action">

  
  <input id="update_rows" name="update_rows"  readonly="readonly" type="hidden" />
  <input id="categories" name="categories"  readonly="readonly" type="hidden" />
  <input id="suppliercodes" name="suppliercodes"  readonly="readonly" type="hidden" />
  

   <table class="itemhead" style="width:800px">
  <tr>
  <td style="width:30%;background-color:#cff5b7;font-weight:bold;">Issued date
  </td>
  <td style="width:30%;background-color:#cff5b7;font-weight:bold;">Expired date
  </td>
  <td style="width:40%;background-color:#cff5b7;font-weight:bold;">New Upload file
  </td>
  </tr>
  <tr>
  <td>    
  <div class="editor-field"> 
             <input id="issueddate" name="issueddate" type="text" readonly="readonly" class="datepicker issueddate"></input>      
  </div>
  </td>
  <td>              
  <div class="editor-field">
              <input id="expireddate" name="expireddate" readonly="readonly" class="expiredate" type="text"></input>      
  </div>
  </td>
   <td>              
  <div class="editor-field">
               <input id="uploadfile" name="uploadfile" type="file" class="uploadfile"></input>         
  </div>
  </td>
  </tr>
  </table>
  <span style="clear:both;" class="btn_pack medium icon"><span class="download"></span><input id="update_click" type="button" value="Update selected documents" /></span>
  </form>
 
 <!--사용자 입력값 validation -->
<script type="text/javascript">


    $('.datepicker').change(function () {
        var index = $(this).index('.datepicker');

        var selectedRow = jQuery("#single").getGridParam("selrow");
        var filecategory = jQuery("#single").getCell(selectedRow, 'FILECATEGORY');

        var expired_date_Month = GetNextMonth($(this).val());
        var expired_date = GetNextYear($(this).val());
        if (filecategory != 'Conflict Mineral') {
            if ($(this).val() == "") {
                $('.expiredate').eq(index).val("");
            } else {
                $('.expiredate').eq(index).val(expired_date);
            }
        }
        else {
            if ($(this).val() == "") {
                $('.expiredate').eq(index).val("");
            } else {
                // 20180530 JHLee: 송윤미씨 요청으로 만료일을 6개월후에서 1년 후로 제안하도록 수정
                //$('.expiredate').eq(index).val(expired_date_Month);
                $('.expiredate').eq(index).val(expired_date);
            }
        }

    });
    $("#delete_click").click(function () {
        var validation = true;
        var validation_issue = true;
        var validation_expired = true;
        var validation_file = true;

        selected_rows();
        if ($("#delete_rows").val() == "") {
            alert("삭제할 데이터를 선택해주세요");
            return false;
        }

        var confrinvalue = confirm("삭제하시겠습니까?");
        if (confrinvalue == false) {

            return false;
        }
        $('#deleteform').submit();

    });


    $("#update_click").click(function () {

        var issueddate = $('#issueddate').val();

        selected_rows();

        if ($("#update_rows").val() == "") {
            alert("수정할 데이터를 선택해주세요");
            return false;
        }

        if ($("#uploadfile").val() == "") {
            alert("업로드 파일을 선택해주세요");
            return false;
        }

        var supplierscodes = $("#suppliercodes").val();
        var categories = $("#categories").val();
        var supp_array = supplierscodes.split(",");
        var category_array = categories.split(",");

        supp_array = arrayCheck(supp_array);
        category_array = arrayCheck(category_array);

        var length_supplier = supp_array.length;
        var length_category = category_array.length;

        if (length_supplier != 1 || length_category != 1) {
            alert("각각 동일한 supplier와 파일종류를 선택하세요");
            return false;
        }

        $("#suppliercodes").val(supp_array[0]);
        $("#categories").val(category_array[0]);

        if (issueddate == "") {
            if (category_array[0] == "Non-use Letter" || category_array[0] == "MSDS") {

            } else {
                alert("issued date를 입력하세요");
                return false;
            }
        }

        var confrinvalue = confirm("수정하시겠습니까?");
        if (confrinvalue == false) {

            return false;
        }

        $('#updateform').submit();


    });

    $("#Expired_List").click(function () {
        jQuery("#single").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSDocumentToUpdate' }).trigger('reloadGrid');

    });
    $("#All_List").click(function () {
        jQuery("#single").jqGrid('setGridParam', { url: '/gps/Entity/Get_All_ActiveGPSDocument_' }).trigger('reloadGrid'); ;
    });

</script>

</asp:Content>
