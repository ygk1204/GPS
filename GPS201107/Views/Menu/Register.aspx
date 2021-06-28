<%@ Page Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<GPS201107.Models.REGISTERDOCUMENT>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DocumentFolder
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
<style type="text/css">
.AlertReturn { background: #fc8294 url(images/ui-bg_glass_75_d0e5f5_1x400.png) 50% 50% repeat-x; }
.WarnReturn { background: #cff5b7 url(images/ui-bg_glass_75_d0e5f5_1x400.png) 50% 50% repeat-x; }
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
     var today = new Date();
     var warn_period = 10; // 경고 표시 기준
     var now = new Date(today.valueOf());
     //날짜 조합
     var year = now.getFullYear();
     var mon = (now.getMonth() + 1) > 9 ? '' + (now.getMonth() + 1) : '0' + (now.getMonth() + 1);
     var day = now.getDate() > 9 ? '' + now.getDate() : '0' + now.getDate();
     //기준일
     var today_date = year + '-' + mon + '-' + day;

</script>
<!--single은 supplier 데이터 그리드
    list10_d은 해당 supplier의 material의 데이터 그리드
    list_file은 supplier의 material의 GPS문서 데이터 그리드 
 -->
 <!--Supplier 데이터 그리드-->
 <script type="text/javascript">
     jQuery(function () {
     
         jQuery("#single").jqGrid({
             url: '<%= Url.Action("GetGPSuppliersData", "Entity") %>',
             contentType: "application/json; charset=utf-8",
             datatype: 'json',
             mtype: 'POST',
             colNames: ['Code', 'Name', 'Representative', 'Mail'],

             // 컬럼 모델 셋팅 하기
             colModel: [
                  { name: 'SUPPLIERCODE', index: 'SUPPLIERCODE', width: 100, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'SUPPLIERNAME', index: 'SUPPLIERNAME', width: 200, align: 'left', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne']} },
                  { name: 'CONTACT', index: 'CONTACT', width: 100, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'EMAIL', index: 'EMAIL', width: 100, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} }
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
                     if (
                         jQuery("#list10_d").jqGrid('getGridParam', 'records') > 0) {
                         jQuery("#list10_d").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSuppliersMaterials/' + jQuery("#single").getCell(ids, 0).toString() });
                         jQuery("#list10_d").jqGrid('setGridParam', { datatype: 'json' });
                         jQuery("#list10_d").jqGrid('setCaption', "Invoice Detail: " + jQuery("#single").getCell(ids, 1).toString().substr(0, 8)).trigger('reloadGrid');

                         jQuery("#list_file").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSDocumentByMaterial/' + jQuery("#single").getCell(ids, 0).toString() });
                         jQuery("#list_file").jqGrid('setGridParam', { datatype: 'json' });
                         jQuery("#list_file").jqGrid('setCaption', "GPS Document List: " + jQuery("#single").getCell(ids, 0).toString().substr(0, 8)).trigger('reloadGrid');


                         select_supplier(ids);

                     }
                 } else {

                     jQuery("#list10_d").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSuppliersMaterials/' + jQuery("#single").getCell(ids, 0).toString() });
                     jQuery("#list10_d").jqGrid('setGridParam', { datatype: 'json' });
                     jQuery("#list10_d").jqGrid('setCaption', "Invoice Detail: " + jQuery("#single").getCell(ids, 1).toString().substr(0, 8)).trigger('reloadGrid');

                     jQuery("#list_file").jqGrid('setGridParam', { url: '/gps/Entity/GetGPSDocumentByMaterial/' + jQuery("#single").getCell(ids, 0).toString() });
                     jQuery("#list_file").jqGrid('setGridParam', { datatype: 'json' });
                     jQuery("#list_file").jqGrid('setCaption', "GPS Document List: " + jQuery("#single").getCell(ids, 0).toString().substr(0, 8)).trigger('reloadGrid');


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
             url: '/gps/Entity/GetGPSuppliersMaterials/',
             height: 300,
             datatype: 'local',
             mtype: 'POST',
             colNames: ['MATERIALNAME', 'PARTNUM'],
             colModel: [
   		{ name: 'MATERIALNAME', index: 'MATERIALNAME', width: 180 },
   		{ name: 'PARTNUM', index: 'PARTNUM', width: 130 }

   	],
             scroll: 1,
             rowNum: 1000,
             rowList: [5, 10, 20],
             pager: '#pager10_d',
             sortname: 'PARTNUM',
             viewrecords: true,
             sortorder: "asc",
             multiselect: true,
             gridview: true,
             ignoreCase: true,
             loadonce: true,
             caption: "Material Detail",
             onSelectRow: function (ids, status) {

                 if (status == true) {

                     jQuery("#list_file").jqGrid('setCaption', "GPS Document List: " + jQuery("#list10_d").getCell(ids, 'MATERIALNAME').toString().substr(0, 8))
                     var text = $("#list10_d").getCell(ids, 'PARTNUM').toString();
                     var grid = $("#list_file");
                     var postdata = grid.jqGrid('getGridParam', 'postData');
                     $.extend(postdata, { filters: '', searchField: 'PARTNUM', searchOper: 'cn', searchString: text });
                     grid.jqGrid('setGridParam', { datatype: 'local' });
                     grid.jqGrid('setGridParam', { search: text.length > 0, postData: postdata });
                     grid.trigger("reloadGrid", [{ page: 1}]);
                 }
             }
         }).navGrid('#pager10_d', { add: false, edit: false, del: false });
         $("#list10_d").jqGrid('filterToolbar', { stringResult: true, searchOnEnter: true, defaultSearch: "cn" });

         jQuery("#list_file").jqGrid({
             url: '<%= Url.Action("GetGPSDocument", "Entity") %>',
             contentType: "application/json; charset=utf-8",
             datatype: 'local',
             mtype: 'POST',
             colNames: ['FILEID', 'SUPPLIERCODE', 'SUPPLIERNAME', 'MATERIALNAME', 'PARTNUM', 'FILENAME', 'FILECATEGORY', 'ISSUEDATE', 'EXPIREDATE', 'ACTIVE'],

             // 컬럼 모델 셋팅 하기
             colModel: [
                  { name: 'FILEID', index: 'FILEID', width: 50, align: 'center', hidden: true, searchoptions: { sopt: ['cn', 'eq', 'ne'] }, search: false },
                  { name: 'SUPPLIERCODE', index: 'SUPPLIERCODE', width: 120, align: 'center', hidden: true, searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne']} },
                  { name: 'SUPPLIERNAME', index: 'SUPPLIERNAME', width: 120, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'MATERIALNAME', index: 'MATERIALNAME', width: 120, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'PARTNUM', index: 'PARTNUM', width: 100, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'FILENAME', index: 'FILENAME', width: 145, align: 'center', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'FILECATEGORY', index: 'FILECATEGORY', width: 120, align: 'center', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne'] }, stype: 'select', searchoptions: { sopt: ['eq'], value: 'Conflict Mineral:Conflict Mineral;MSDS:MSDS;Non-use Letter:Non-use Letter;Test Report:Test Report;Warranty letter:Warranty letter;ALL:전체선택', defaultValue: 'ALL'} },
                  { name: 'ISSUEDATE', index: 'ISSUEDATE', width: 92, align: 'center', searchoptions: { dataInit: datePick, sopt: ['eq', 'ne', 'cn', 'lt', 'le', 'gt', 'ge']} },
                  { name: 'EXPIREDATE', index: 'EXPIREDATE', width: 92, align: 'center', searchoptions: { dataInit: datePick, sopt: ['eq', 'ne', 'cn', 'lt', 'le', 'gt', 'ge']} },
                  { name: 'ACTIVE', index: 'ACTIVE', width: 70, align: 'center', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne'] }, stype: 'select', searchoptions: { sopt: ['eq'], value: 'T:T;F:F;ALL:전체선택', defaultValue: 'T'} }
                  ],
             scroll: 1,
             rowNum: 200,
             rowList: [20, 50, 100],
             viewrecords: true,
             height: 80,
             loadonce: true,
             sortorder: "desc",
             pager: jQuery('#pager_file'),
             emptyrecords: "Nothing to display",
             sortname: 'FILEID',
             caption: 'GPS Document List',
             afterInsertRow: dateCheck,
             onCellSelect: function (rowid, iCol, cellcontent) {
                 location.href = "/gps/Menu/Download/" + jQuery("#list_file").getCell(rowid, 1).toString() + "/" + jQuery("#list_file").getCell(rowid, 6).toString() + "/" + jQuery("#list_file").getCell(rowid, 5).toString();
             }

         });

         $("#list_file").jqGrid('filterToolbar', { stringResult: true, searchOnEnter: true, defaultSearch: "cn" });
         jQuery("#list_file").navGrid('#pager_file', { view: false, del: false, add: false, edit: false, search: true },
       {}, // default settings for edit
       {}, // default settings for add
       {}, // delete instead that del:false we need this
       {closeOnEscape: true, multipleSearch: true, closeAfterSearch: true }, // search options
       {} /* view parameters*/
     );




     });


    </script>
  
  <!--Supplier list table & Material number list table-->
  <div style="height:410px; width: 900px;">
  
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

 <table style="border: 1px solid silver;width:400px;height:30px;" >
 <tr>
 <td style="width:40%;">해당 Material GPS 문서</td>
<td style="width:10%;" class="WarnReturn"></td>
<td style="width:20%;" >만료 10일전</td>
<td style="width:10%;" class="AlertReturn"></td>
<td style="width:20%;" >만료됨</td>
</tr>
</table>

  <!--GPS Document list-->
  <div style="text-align:center; padding-right:0px;">
  <table id="list_file" class="scroll" cellpadding="0" cellspacing="0"></table>
  <div id="pager_file" class="scroll" style="text-align:center;"></div>
  </div>
  <br />

   <form id="register_form" name="register_form" method="post" enctype="multipart/form-data" action="/gps/menu/Register_action">

  <!--Supplier informantion table-->
  <table class="itemhead" style="width:800px">
  <tr>
  <td style="width:20%;background-color:#cff5b7; font-weight:bold;">Supplier Name</td>
  <td><input id="s_name" name="s_name" size="20" readonly="readonly"></input></td>
  <td style="width:20%;background-color:#cff5b7; font-weight:bold;">Supplier Code</td>
  <td><input id="s_code" name="s_code" size="20" readonly="readonly"></input></td>
  </tr>
  <tr>
  <td style="width:20%;background-color:#cff5b7; font-weight:bold;">Representative</td>
  <td><input id="s_Representative" name="s_Representative" size="20" readonly="readonly"></input></td>
  <td style="width:20%;background-color:#cff5b7; font-weight:bold;">Mail</td>
  <td><input id="s_mail" name="s_mail" size="20" readonly="readonly"></input></td>
  </tr>
  </table>
  <br />
  <input id="selected_device" name="selected_device" size="10" readonly="readonly" type="hidden" >
  
  <!--Uploading list-->
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
                <option value="Test Report">Test Report</option>
                <option value="MSDS">MSDS</option>
                <option value="Non-use Letter">Non-use Letter</option>
              </select>
  </div>             
  </td>
  <td style="width:20%">    
  <div class="editor-field">
              <%: Html.TextBoxFor(model => model.DocumenList[0]._ISSUEDATE, new Dictionary<string, object> { { "class", "datepicker issueddate" },{ "readonly", "true" }})%>
  </div>
  </td>
  <td style="width:20%">              
  <div class="editor-field">
                <%: Html.TextBoxFor(model => model.DocumenList[0]._EXPIREDATE, new Dictionary<string, object> { { "class", "expiredate" }, { "readonly", "true" } })%>

  </div>
  </td>
   <td style="width:40%">              
  <div class="editor-field">
                 <input id="DocumenList_0___FILENAME" name="DocumenList[0]._FILENAME" type="file" class="uploadfile"></input>         
  </div>
  </td>
  </tr>
  </table>
  <span  style="margin-top:7px" class="btn_pack medium icon RemoveItem"><span class="delete"></span><button type="button">삭제</button></span>   
  </div>
  <span style="clear:both;" class="AddItem btn_pack medium icon"><span class="add"></span><button type="button">추가</button></span>
  <div style="width:900px; text-align:center; padding-top:20px;">
  <span style="clear:both;" class="btn_pack medium icon"><span class="check"></span><input id="registersummit" type="button" value="SAVE" /></span>
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
        $('#s_name').val(s_name);
        $('#s_code').val(s_code);
        $('#s_Representative').val(s_Representative);
        $('#s_mail').val(s_mail);
    }
    select_device = function () {
        var s;
        var k;
        k = "";
        s = jQuery("#list10_d").jqGrid('getGridParam', 'selarrrow');

        $.each(s, function (i) {
            k += jQuery("#list10_d").getCell(s[i], 1).toString() + "^" + jQuery("#list10_d").getCell(s[i], 2).toString();
            k += "|";
        });        
        $('#selected_device').val(k);
    }
    datePick = function (elem) {
        $.datepicker.setDefaults($.datepicker.regional[""]);
        $(elem).datepicker($.datepicker.regional['ko']);
        $(elem).datepicker("option", "dateFormat", 'yy-mm-dd');
    }

</script>

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
<script type="text/javascript">
    dateCheck = function (rowid, rowdata, rowelem) {

        var DateToUpdate = jQuery("#list_file").getCell(rowid, 'EXPIREDATE').toString();
        var Document_type = jQuery("#list_file").getCell(rowid, 'FILECATEGORY').toString();
        var NumofDayLeft = getDateDiff(today_date.toString(), DateToUpdate);

        if (NumofDayLeft < warn_period && Document_type != "MSDS" && Document_type != "Non-use Letter") {
            jQuery("#list_file").setRowData(rowid, '', 'WarnReturn');
            if (NumofDayLeft < 0) {
                jQuery("#list_file").find("#" + rowid).removeClass('WarnReturn');
                jQuery("#list_file").setRowData(rowid, '', 'AlertReturn');
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

    function GetNextYear(val1)
    {
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
    $('#registersummit').click(function () {
        var validation = true;
        var validation_issue = true;
        var validation_file = true;
        var validation_category = true;


        $(".issueddate").each(function () {
            var validation_doctype = $(this).parent().parent().parent().find('.category_doc').val()

            if ($(this).val() == "") {
                if (validation_doctype == "Non-use Letter" || validation_doctype == "MSDS") {

                } else {
                    validation_issue = false;
                }

            }

        });
        $(".uploadfile").each(function () {

            if ($(this).val() == "") {

                validation_file = false;

            }
        });
        $(".category_doc").each(function () {

            if ($(this).val() == "") {

                validation_category = false;
            }
        });

        if (validation_issue == false) {
            alert("Issued date를 입력하세요");
            return false;
        }
        if (validation_file == false) {
            alert("파일을 선택해주세요");
            return false;
        }

        if (validation_category == false) {
            alert("Document분류를 입력해주세요");
            return false;
        }



        select_device();

        if ($('#selected_device').val() == "") {
            alert("Material을 선택해주세요");
            return false;
        }

        var confrinvalue = confirm("제출하시겠습니까?");
        if (confrinvalue == false) {

            return false;
        }
        $('#register_form').submit();
    });
</script>
</asp:Content>
