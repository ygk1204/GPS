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
          $(".datepicker").datepicker("option", "dateFormat", 'yymmdd');
      });

</script>
<link href="../../Content/uploadify/uploadify.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="../../Content/uploadify/swfobject.js"></script>
<script type="text/javascript" src="../../Content/uploadify/jquery.uploadify.v2.1.4.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#file_upload').uploadify({
            'uploader': './../Content/uploadify/uploadify.swf',
            'script': '/Menu/Uploadfiles',
            'scriptData' :{'first':'sss'},
            'cancelImg': './../Content/uploadify/cancel.png',
            'folder': '/uploads',            
            'multi' :true,
            'removeCompleted' : false,

            });

    });
</script>
 <script type="text/javascript">

     jQuery(function () {


         jQuery("#single").jqGrid({
             url: '<%= Url.Action("GetGPSuppliersData", "Menu") %>',
             datatype: 'json',
             mtype: 'POST',
             colNames: ['Code', 'Name', 'Representative', 'Mail'],

             // 컬럼 모델 셋팅 하기
             colModel: [
                  { name: 'SUPPLIERCODE', index: 'SUPPLIERCODE', width: 100, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne'] }, search: false },
                  { name: 'SUPPLIERNAME', index: 'SUPPLIERNAME', width: 200, align: 'left', searchoptions: { sopt: ['bw', 'cn', 'eq', 'ne']} },
                  { name: 'CONTACT', index: 'CONTACT', width: 100, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} },
                  { name: 'EMAIL', index: 'EMAIL', width: 100, align: 'left', searchoptions: { sopt: ['cn', 'eq', 'ne']} }
                  ],
             scroll: 1,
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
                         jQuery("#list10_d").jqGrid('setGridParam', { url: '/Menu/GetGPSuppliersMaterials/' + jQuery("#single").getCell(ids, 0).toString() });
                         jQuery("#list10_d").jqGrid('setCaption', "Invoice Detail: " + jQuery("#single").getCell(ids, 1).toString().substr(0, 8))
				.trigger('reloadGrid');

                         select_supplier(ids);
                     }
                 } else {

                     jQuery("#list10_d").jqGrid('setGridParam', { url: '/Menu/GetGPSuppliersMaterials/' + jQuery("#single").getCell(ids, 0).toString() });
                     jQuery("#list10_d").jqGrid('setCaption', "Invoice Detail: " + jQuery("#single").getCell(ids, 1).toString().substr(0, 8))
			.trigger('reloadGrid');
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
             url: '/Menu/GetGPSuppliersMaterials/',
             height: 320,
             datatype: "json",
             mtype: 'POST',
             colNames: ['MATETIALNAME', 'PARTNUM'],
             colModel: [
   		{ name: 'MATETIALNAME', index: 'MATETIALNAME', width: 180 },
   		{ name: 'PARTNUM', index: 'PARTNUM', width: 130 }

   	],
             rowNum: 10,
             rowList: [5, 10, 20],
             pager: '#pager10_d',
             sortname: 'PARTNUM',
             viewrecords: true,
             sortorder: "asc",
             multiselect: true,
             caption: "Device Detail"
         }).navGrid('#pager10_d', { add: false, edit: false, del: false });

         jQuery("#m1").click(function () {
             select_device();


         });
     });


    </script>
<div style="height:450px; width: 900px;">
<div style="float:left; width:500px;">
<table id="single" class="scroll" cellpadding="0" cellspacing="0"></table>
<div id="pager" class="scroll" style="text-align:center;"></div>
</div>
<div style="float:left; padding-left:30px; width:200px; ">
<table id="list10_d" ></table>
<div id="pager10_d"></div>
</div>
</div>
 <form id="register_form" name="register_form" method="post" enctype="multipart/form-data" action="/menu/Register_action">
<table>
<tr>
<td>Supplier Name</td>
<td><input id="s_name" name="s_name" size="10" readonly="readonly"></input></td>
<td>Suupplier Code</td>
<td><input id="s_code" name="s_code" size="10" readonly="readonly"></input></td>
</tr>
<tr>
<td>Representative</td>
<td><input id="s_Representative" name="s_Representative" size="10" readonly="readonly"></input></td>
<td>Mail</td>
<td><input id="s_mail" name="s_mail" size="10" readonly="readonly"></input></td>
</tr>
</table>
               <table class="itemhead">
                <tr>
               <td style="width:15%;background-color:#cff5b7; font-weight:bold;">Document type
               </td>
               <td style="width:30%;background-color:#cff5b7;font-weight:bold;">Issued Date
               </td>
               <td style="width:30%;background-color:#cff5b7;font-weight:bold;">Expired date
               </td>
               <td style="width:15%;background-color:#cff5b7;font-weight:bold;">Issue date
               </td>
               </tr>
               </table>
  <div class="NsForm">
  <table class="ItemTable"> 
  <tr>
  <td style="width:15%;">        
  <div class="editor-field">
              <select id="DocumenList_0___FILECATEGORY" name="DocumenList[0]._FILECATEGORY">
                <option value="">선택</option>
                <option value="Test Report">Test Report</option>
                <option value="MSDS">MSDS</option>
                <option value="Non-use Letter">Non-use Letter</option>
              </select>
  </div>             
  </td>
  <td style="width:30%">    
  <div class="editor-field">
                <%: Html.TextBoxFor(model => model.DocumenList[0]._ISSUEDATE, new Dictionary<string, object> { { "class", "datepicker" } })%>
  </div>
  </td>
  <td style="width:30%">              
  <div class="editor-field">
                <%: Html.TextBoxFor(model => model.DocumenList[0]._EXPIREDATE, new Dictionary<string, object> { { "class", "expiredate" } })%>

  </div>
  </td>
   <td style="width:30%">              
  <div class="editor-field">
                 <input id="DocumenList_0___FILENAME" name="DocumenList[0]._FILENAME" type="file"></input>         
  </div>
  </td>
  </tr>
  </table>
  <span  style="margin-top:7px" class="btn_pack medium icon RemoveItem"><span class="delete"></span><button type="button">삭제</button></span>   
  </div>
  <span style="clear:both;" class="AddItem btn_pack medium icon"><span class="add"></span><button type="button">추가</button></span>
  <span style="clear:both;" class="btn_pack medium icon"><span class="check"></span><button type="button">SAVE</button></span>
  </form>

<%--<div>
<div style="float:left">
TEST Files
<div style="display:block;">
<input id="issue_date01" class="datepicker" name="issue_date01" />
<input id="expired_date01" name="expired_date01" />
<input type="file" id="testfile1" name="testfile1" />
</div>
<div style="display:block;">
<input id="issue_date01" class="datepicker" name="issue_date01" />
<input id="expired_date01" name="expired_date01" />
<input type="file" id="testfile2" name="testfile2" />
</div>
<div style="display:block;">
<input id="issue_date01" class="datepicker" name="issue_date01" />
<input id="Text4" name="expired_date01" />
<input type="file" id="testfile3" name="testfile3" />
</div>
<div style="display:block;">
<input id="issue_date01" class="datepicker" name="issue_date01" />
<input id="Text6" name="expired_date01" />
<input type="file" id="testfile4" name="testfile4" />
</div>
<div style="display:block;">
<input id="issue_date01" class="datepicker" name="issue_date01" />
<input id="Text8" name="expired_date01" />
<input type="file" id="testfile5" name="testfile5" />
</div>
</div>--%>

<%--<div>
MSDS FILES
<div style="display:block;">
<input id="issue_date01" class="datepicker" name="issue_date01" />
<input id="Text10" name="expired_date01" />
<input type="file" id="msdsfile1" name="msdsfile1" />
</div>
<div style="display:block;">
<input id="issue_date01" class="datepicker" name="issue_date01" />
<input id="Text12" name="expired_date01" />
<input type="file" id="msdsfile2" name="msdsfile2" />
</div>
<div style="display:block;">
<input id="issue_date01" class="datepicker" name="issue_date01" />
<input id="Text14" name="expired_date01" />
<input type="file" id="msdsfile3" name="msdsfile3" />
</div>
<div style="display:block;">
<input type="file" id="msdsfile4" name="msdsfile4" />
</div>
<div style="display:block;">
<input id="issue_date01" class="datepicker" name="issue_date01" />
<input id="Text16" name="expired_date01" />
<input type="file" id="msdsfile5" name="msdsfile5" />
</div>
</div>
</div>--%>



<%--<input id="selected_device" name="selected_device" size="10" readonly="readonly"></input>--%>

<%--<input id="issue_date01" class="datepicker" name="issue_date01" />
<input id="expired_date01" name="expired_date01" />
<input type="file" name="test_file1" />

<input id="issue_date02" class="datepicker" name="issue_date02" />
<input id="expired_date02" name="expired_date02" />
<input type="file" name="test_file2" />3

<input id="issue_date03" class="datepicker" name="issue_date03" />
<input id="expired_date03" name="expired_date03" />
<input type="file" name="test_file3" />

<input id="issue_date04" class="datepicker" name="issue_date04" />
<input id="expired_date04" name="expired_date04" />
<input type="file" name="msds_file1" />

</form>--%>
<%--
<input id="file_upload" name="file_upload" type="file" />
<a href="javascript:$('#file_upload').uploadifyUpload();">Upload Files</a> | <a href="javascript:$('#file_upload').uploadifyClearQueue();">Clear Queue</a>


--%>
<%--
<a href="javascript:void(0)" id="m1">Get Selected id's</a>--%>

<script type="text/javascript">
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
        s = jQuery("#list10_d").jqGrid('getGridParam', 'selrow');
        alert(s);
        $.each(s, function (i) {
            k += jQuery("#list10_d").getCell(s[i], 2).toString();
            k += ",";
        });
        $('#selected_device').val(k);
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
                    var index = names.match(/\d/g);                        //바뀌기 전의 아이템 네임 속성의 배열 인덱스번호 가져오기 -> 문자열 중 숫자 뽑기
                    var newNames = names.split(index).join(i);             // 배열 인덱스 번호를 순서 맞게 바꾼 값
                    $(this).attr('name', newNames);                        // 정리된 익덱스 번호 값을 갖는 네임속성에 할당
                });
            });


        }
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
                var index = names.match(/\d/g);                    //바뀌기 전의 아이템 네임 속성의 배열 인덱스번호 가져오기 -> 문자열 중 숫자 뽑기
                var newNames = names.split(index).join(i);         // 배열 인덱스 번호를 순서 맞게 바꾼 값
                $(this).attr('name', newNames);                    // 정리된 익덱스 번호 값을 갖는 네임속성에 할당
            });
        });

        //세부 사항 

    });
</script>
</asp:Content>
