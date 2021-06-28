<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<GPS201107.Models.HazardousRequest.ViewHazardousMaterial>" 
ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    유해물질 문서 상세보기
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="font-weight: bold;">
        유해물질 문서 상세보기</h2>
    
    <style type="text/css">
        table
        {
            font-size: 11px;
        }
    </style>
    
    <link href="/gps/Content/HazardousMaterial.css" rel="stylesheet" type="text/css" />
    
    <%using (Html.BeginForm("ViewHazardousMaterial", "Hazardous", new { hmreqid = Model.gpshmrequest._HMREQID },
         FormMethod.Post, new { id = "frmHazardousModify", enctype = "multipart/form-data" }))
      { %>
    <div>
        <table style="width: 100%;">
            <colgroup>
                <col width="15%" />
                <col width="35%" />
                <col width="15%" />
                <col width="35%" />
            </colgroup>
            <tr>
                <th>요청 ID</th>
                <td>
                    <%=Model.gpshmrequest._HMREQID%>
                    <input type="hidden" id="hmreqid", name="gpshmrequest._HMREQID" value="<%=Model.gpshmrequest._HMREQID%>" />
                </td>
                <th>상태</th>
                <td>
                    <%= Model.gpshmrequest._STATUS%>                    
                </td>
            </tr>
            <tr>
                <th>요청 날짜</th>
                <td>
                    <%= Model.gpshmrequest._REQUESTDATE%>                    
                </td><th>요청자 사번</th>
                <td><%= Model.gpshmrequest._REQUESTUSERID %></td>
            </tr>
            <tr>
                <th>이름</th>
                <td>
                    <div>
                        <input id="requestusername" readonly="readonly" name="gpshmrequest._REQUESTUSERNAME" value="<%= Model.gpshmrequest._REQUESTUSERNAME %>" class="form-control" />
                    </div>
                </td>
                
                <th>요청자 이메일</th>
                <td>
                    <div>
                        <input id="requestuseremail" readonly="readonly" name="gpshmrequest._REQUESTUSEREMAIL" value="<%= Model.gpshmrequest._REQUESTUSEREMAIL %>" class="form-control" />
                    </div>
                </td>
            </tr>
        </table>
        <h3 style="font-weight: bold;">
            Detail Information</h3>
        <table style="width: 100%;">
            <colgroup>
                <col width="15%" />
                <col width="35%" />
                <col width="15%" />
                <col width="35%" />
            </colgroup>
            <tr>
                <th>고객사</th>
                <td>
                    <div>
                        <input id="customer" readonly="readonly" name="gpshmrequest._CUSTOMER" value="<%= Model.gpshmrequest._CUSTOMER %>"
                            class="form-control" />
                    </div>
                </td>
                <th>제품</th>
                <td>
                    <div>
                        <input id="product" readonly="readonly" name="gpshmrequest._PRODUCT" style="width:300px;" value="<%= Model.gpshmrequest._PRODUCT%>"
                            class="form-control" />
                    </div>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <colgroup>
                <col width="15%" />
                <col width="85%" />
            </colgroup>
            <tr>
                <th>요청 내용</th>
                <td colspan="3">
                    <textarea class="form-control" id="requestcomment" readonly="readonly" rows="4" cols="90" name="gpshmrequest._REQUESTCOMMENT"><%=Model.gpshmrequest._REQUESTCOMMENT%></textarea>
                </td>
            </tr>
        </table>
                
        
        <table style="width: 100%;">
            <colgroup>
                <col width="15%" />
                <col width="35%" />
                <col width="15%" />
                <col width="35%" />
            </colgroup>
            <tr>
                <th>항목</th>
                <td>
                    <input id="hazardousmaterialtype" readonly="readonly" name="hazardousmaterialtype" value="<%=Model.gpshmrequest._HAZARDOUSMATERIALTYPE%>" class="form-control" />
                        &nbsp;&nbsp;
                        <input id="description" readonly="readonly" name="leadtime" value="<%=Model.leadtime %>" class="form-control " />
                </td>
                <th>
                    담당자
                </th>
                <td>
                    <%= Model.gpshmrequest._ADMINUSERID+ " / " + Model.gpshmrequest._ADMINUSERNAME%>
                </td>
            </tr>
        </table>
        
        
        <table style="width: 100%;">
            <colgroup>
                <col width="15%" />
                <col width="35%" />
                <col width="15%" />
                <col width="35%" />
            </colgroup>
            <tr>
                <th>
                    예상 완료일
                </th>
                <td>
                    <input id="expectedfinishdate" readonly="readonly" name="gpshmrequest._EXPECTEDFINISHDATE"
                        value="<%=Model.gpshmrequest._EXPECTEDFINISHDATE%>" class="form-control" />
                </td>
                <th>
                    조사 자료위치
                </th>
                <td>
                    <input id="hadnouturl" readonly="readonly" name="gpshmrequest._HADNOUTURL" style="width:300px;" value="<%=Model.gpshmrequest._HADNOUTURL%>"
                        class="form-control" />
                </td>
            </tr>
        </table>
        
        
        
        <table style="width: 100%;">
            <colgroup>
                <col width="15%" />
                <col width="85%" />
            </colgroup>
            <tr>
                <th>
                    유해물질 자료
                </th>
                <td>
                    <div class="handout_file">
                        <table class="handout_file">
                            <%for (int i = 0; i < Model.lstFileHandOutOnlyView.Count; i++)
                              {%>
                            <%if (Model.lstFileHandOutOnlyView[i]._FILENAME != null)
                              {%>
                            <tr>
                                <td>
                                    <div>
                                        <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                                            <%=Model.lstFileHandOutOnlyView[i]._FILENAME%></a>
                                        <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%=Model.lstFileHandOutOnlyView[i]._PHYSICALFILENAME%>" />
                                        <input type="hidden" name="filename" class="filename" value="<%=Model.lstFileHandOutOnlyView[i]._FILENAME%>" />
                                        <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%=Model.lstFileHandOutOnlyView[i]._PHYSICALFILELOCATION%>" />
                                    </div>
                                </td>
                            </tr>
                            <%}  }%>
                        </table>
                    </div>
                </td>
            </tr>
        </table>

        <table style="width: 100%;">
            <colgroup>
                <col width="15%" />
                <col width="85%" />
            </colgroup>
            <tr>
                <th>기타 파일</th>
                <td>
                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                        <%= Model.lstFileOther[0]._FILENAME%></a>
                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%= Model.lstFileOther[0]._PHYSICALFILENAME%>" />
                    <input type="hidden" name="filename" class="filename" value="<%=Model.lstFileOther[0]._FILENAME%>" />
                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%=Model.lstFileOther[0]._PHYSICALFILELOCATION%>" />
                </td>
            </tr>
        </table>
        
        <table style="width: 100%;">
            <colgroup>
                <col width="15%" />
                <col width="85%" />
            </colgroup>
            <%if (Model.lstFileBom[0]._FILENAME != null)
              {%>
            <tr>
                <th>
                    Bom 파일
                </th>
                <td>
                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                        <%=Model.lstFileBom[0]._FILENAME%></a>
                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%=Model.lstFileBom[0]._PHYSICALFILENAME%>" />
                    <input type="hidden" name="filename" class="filename" value="<%=Model.lstFileBom[0]._FILENAME%>" />
                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%=Model.lstFileBom[0]._PHYSICALFILELOCATION%>" />
                </td>
            </tr>
            <%}%>
            <%if (Model.lstFileCustomer[0]._FILENAME != null)
              {%>
            <tr>
                <th>
                    고객사 파일
                </th>
                <td>
                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                        <%= Model.lstFileCustomer[0]._FILENAME%></a>
                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%=Model.lstFileCustomer[0]._PHYSICALFILENAME%>" />
                    <input type="hidden" name="filename" class="filename" value="<%=Model.lstFileCustomer[0]._FILENAME%>" />
                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%= Model.lstFileCustomer[0]._PHYSICALFILELOCATION%>" />
                </td>
            </tr>
            <%}%>
            <%if (Model.lstFIleBd[0]._FILENAME != null)
              {%>
            <tr>
                <th>
                    BD 파일
                </th>
                <td>
                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                        <%= Model.lstFIleBd[0]._FILENAME%></a>
                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%=  Model.lstFIleBd[0]._PHYSICALFILENAME%>" />
                    <input type="hidden" name="filename" class="filename" value="<%= Model.lstFIleBd[0]._FILENAME%>" />
                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%=  Model.lstFIleBd[0]._PHYSICALFILELOCATION%>" />
                </td>
            </tr>
            <%}%>
            <%if (Model.lstFilePod[0]._FILENAME != null)
              {%>
            <tr>
                <th>
                    POD 파일
                </th>
                <td>
                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                        <%= Model.lstFilePod[0]._FILENAME%></a>
                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%= Model.lstFilePod[0]._PHYSICALFILENAME%>" />
                    <input type="hidden" name="filename" class="filename" value="<%=Model.lstFilePod[0]._FILENAME%>" />
                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%=Model.lstFilePod[0]._PHYSICALFILELOCATION%>" />
                </td>
            </tr>
            <%}%>
            <%if (Model.lstFileComponent[0]._FILENAME != null)
              {%>
            <tr>
                <th>
                    Components Part 파일
                </th>
                <td>
                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                        <%= Model.lstFileComponent[0]._FILENAME%></a>
                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%= Model.lstFileComponent[0]._PHYSICALFILENAME%>" />
                    <input type="hidden" name="filename" class="filename" value="<%=Model.lstFileComponent[0]._FILENAME%>" />
                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%= Model.lstFileComponent[0]._PHYSICALFILELOCATION%>" />
                </td>
            </tr>
            <%}%>
        </table>
        <br />

        <table>
            <tr>
                <%--Die Thickness --%>
                <td valign="top">                     
                    <table style="width: 100%;">
                        <colgroup>
                            <col width="5%" />
                            <col width="95%" />
                        </colgroup>
                        <tr><%--Die Thickness 헤더--%>
                            <th>No</th>
                            <th>Die Thickness</th>
                        </tr>
                        <%--Die Thicnkness Items--%>
                        <%for (int i = 0; i < Model.lstItemDieThickness.Count; i++){%>
                        <tr>
                            <td align="center" style="vertical-align: middle;">
                                <%=(i + 1).ToString()%>
                            </td>
                            <td>
                                <%--<input type="text" readonly="readonly" style="width: 250px; height: 20px;" value="<%=Model.lstItemDieThickness[i]._ITEMVALUE%>"/>--%>
                                <%=Model.lstItemDieThickness[i]._ITEMVALUE%>
                            </td>
                        </tr>
                        <%}%>                    
                    </table>                
                </td>
                <%--Pcb Thickness--%>
                <td valign="top">
                    <table style="width: 100%;">
                        <colgroup>
                            <col width="5%" />
                            <col width="95%" />
                        </colgroup>
                        <tr>
                            <th>No</th>
                            <th>Pcb Thickness </th>
                        </tr>
                        <%for (int i = 0; i < Model.lstItemPcbThickness.Count; i++){%>
                        <tr>
                            <td align="center" style="vertical-align: middle;">
                                <%=(i + 1).ToString()%>
                            </td>
                            <td>
                                <%--<input type="text" readonly="readonly" style="width: 250px; height: 20px;" value="<%=Model.lstItemPcbThickness[i]._ITEMVALUE%>"/>--%>
                                <%=Model.lstItemPcbThickness[i]._ITEMVALUE%>
                            </td>
                        </tr>
                        <%} %>
                    </table>
                </td>
                <%--Pkg Thinckness--%>
                <td valign="top">
                    <table style="width: 100%;">
                        <colgroup>
                            <col width="5%" />
                            <col width="95%" />
                        </colgroup>
                         <tr>
                            <th>No</th>
                            <th >Pkg Thickness</th>
                        </tr>
                        <%for (int i = 0; i < Model.lstItemPkgThickness.Count; i++){%>
                        <tr>
                            <td align="center" style="vertical-align: middle;">
                                <%=(i + 1).ToString()%>
                            </td>
                            <td>
                                <%--<input type="text" readonly="readonly" style="width: 250px; height: 20px;" value="<%=Model.lstItemPkgThickness[i]._ITEMVALUE%>"/>--%>
                                <%=Model.lstItemPkgThickness[i]._ITEMVALUE%>
                            </td>
                        </tr>
                        <%}%>
                    </table>
                </td>            
            </tr>
        </table>           
        <br />
        <%--shield Part--%>
        <table style="width: 100%;">
            <colgroup>
                <col width="5%" />
                <col width="35%" />
                <col width="60%" />
            </colgroup>
            <%%>
            <tr>
                <th>No</th>
                <th>Shield Part</th>
                <th>Supplier</th>
            </tr>
            <%if (Model.lstItemShieldPart[0]._ITEMVALUE != null)
              {
                  for (int i = 0; i < Model.lstItemShieldPart.Count; i++)
              {%>
            <tr>
                <td align="center" style="vertical-align: middle;">
                    <%=(i + 1).ToString()%>
                </td>
                <td>
                    <%--<input type="text" readonly="readonly" style="width: 190px; height: 20px;" value="<%=Model.lstItemShieldPart[i]._ITEMVALUE.Replace("¶¶","¶").Split('¶')[0]%>"/>--%>
                    <%=Model.lstItemShieldPart[i]._PARTNAME%>
                </td>
                <td>
                    <%--<input type="text" readonly="readonly" style="width: 190px; height: 20px;" value="<%=Model.lstItemShieldPart[i]._ITEMVALUE.Replace("¶¶","¶").Split('¶')[1]%>"/>--%>
                    <%=Model.lstItemShieldPart[i]._COMPANYNAME%>
                </td>
            </tr>
            <%} }%>
            
            <tr>
                <th>No</th>
                <th>Ball Part</th>
                <th>Supplier</th>
            </tr>
            <%if (Model.lstItemBallPart[0]._ITEMVALUE != null)
              {
                  for (int i = 0; i < Model.lstItemBallPart.Count; i++)
              {%>
            <tr>
                <td align="center" style="vertical-align: middle;">
                    <%=(i + 1).ToString()%>
                </td>
                <td>
                    <%--<input type="text" readonly="readonly" style="width: 190px; height: 20px;" value="<%=Model.lstItemBallPart[i]._ITEMVALUE.Replace("¶¶","¶").Split('¶')[0]%>"/>--%>
                    <%=Model.lstItemBallPart[i]._PARTNAME%>
                </td>
                <td>
                    <%--<input type="text" readonly="readonly" style="width: 190px; height: 20px;" value="<%=Model.lstItemBallPart[i]._ITEMVALUE.Replace("¶¶","¶").Split('¶')[1]%>"/>--%>
                    <%=Model.lstItemBallPart[i]._COMPANYNAME%>
                </td>
            </tr>
            <%}  }%>
        </table>
        <%--End shield Part--%>
        <br />
        
        <table style="width: 100%;">
            <colgroup>
                <col width="5%" />
                <col width="20%" />
                <col width="25% " />
                <col width="20%" />
                <col width="30%" />
            </colgroup>
            
            <tr>
                <th>No</th>
                <th>Bump Die 성분</th>
                <th>Bump Die Count</th>
                <th>Bump Die Density</th>
                <th>Bump Die Diameter</th>
            </tr>
            <%  if (Model.lstItemBumpDie[0]._ITEMVALUE != null)
                {
                    for (int i = 0; i < Model.lstItemBumpDie.Count; i++)
                    {%>
            <tr>
                <td align="center" style="vertical-align: middle;">
                    <%=(i + 1).ToString()%>
                </td>
                <td>
                    <%--<input type="text" readonly="readonly" style="width: 130px;height: 20px;" value="<%=Model.lstItemBumpDie[i]._ITEMVALUE.Replace("¶¶","¶").Split('¶')[0]%>"/>--%>
                    <%=Model.lstItemBumpDie[i]._INGREDIENT%>
                </td>
                <td>
                    <%--<input type="text" readonly="readonly" style="width: 130px;height: 20px;" value="<%=Model.lstItemBumpDie[i]._ITEMVALUE.Replace("¶¶","¶").Split('¶')[1]%>"/>--%>
                    <%=Model.lstItemBumpDie[i]._COUNT%>
                </td>
                <td>
                    <%--<input type="text" readonly="readonly" style="width: 130px;height: 20px;" value="<%=Model.lstItemBumpDie[i]._ITEMVALUE.Replace("¶¶","¶").Split('¶')[2]%>"/>--%>
                    <%=Model.lstItemBumpDie[i]._DENSITY%>
                </td>
                <td>
                    <%--<input type="text" readonly="readonly" style="width: 130px;height: 20px;" value="<%=Model.lstItemBumpDie[i]._ITEMVALUE.Replace("¶¶","¶").Split('¶')[3]%>"/>--%>
                    <%=Model.lstItemBumpDie[i]._DIAMETER%>
                </td>
            </tr>
            <%   } }   %>            
        </table>
    </div>
    <%}%>
    <br />
    
    <%if (Model.editMode == ASEWCFServiceLibrary.App_Code.clsConst.EditMode.Modify.ToString())
      {%>
    <div id="btn_form_area" align="center" style="width: 100%;">
    <button id="back_button" class="btn-btn-info" style="width: 76px; height: 24px;">
            <span></span> 목록으로
        </button>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <button type="button" id="btnModify" style="width: 76px; height: 24px;">
            <span></span>수정화면
        </button>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <button type="button" id="btndelete" style="width: 76px; height: 24px;">
            <span></span>삭제
        </button>
    </div>
    <%}%>
    <form id="downloadform" method="post" action="/gps/Hazardous/DownloadFile">
    <input type="hidden" id="physicalfilename" name="physicalfilename" />
    <input type="hidden" id="filename" name="filename" />
    <input type="hidden" id="physicalfilelocation" name="physicalfilelocation" />
    </form>
    <form id="ViewHazardousModify" action="<%=Url.Action("ViewHazardousModify", "Hazardous") %>"
    method="post" enctype="multipart/form-data">
    <input type="hidden" name="hmreqid" value="<%=Model.gpshmrequest._HMREQID%>" />
    </form>
    <script type="text/javascript">

        $("#back_button").click(function () {
            location.href = "/gps/Menu/HazardousMaterialReportList";
        });

        // INPUT, TEXTAREA 태그에서 백스페이스 눌렀을때 이전 페이지로 넘어가지 않도록
        $(document).keydown(function (e) {
            if (e.target.nodeName != "INPUT" && e.target.nodeName != "TEXTAREA") {
                if (e.keyCode == 8) {
                    return false;
                }
            }

            if (e.target.readOnly) { // readonly일 경우 true
                if (e.keyCode == 8) {
                    return false;
                }
            }
        });



        function downloadfile(obj) {
            var index = $('.downloadfile').index(obj);

            //form 으로 값 전송
            $("#physicalfilename").val($('.physicalfilename').eq(index).val());
            $("#filename").val($('.filename').eq(index).val());
            $("#physicalfilelocation").val($('.physicalfilelocation').eq(index).val());

            var form = $("#downloadform");
            form.submit();

        }

        //수정버튼 클릭 시
        $("#btnModify").click(function () {

            $("#frmHazardousModify").submit();

        });





        $("#btndelete").click(function () {

            if (confirm("정말 삭제하시겠습니까?")) {
                var hmreqid = $("#hmreqid").val();

                $.ajax({ url: '/gps/Hazardous/updateDeleteStatus',
                    type: "POST",
                    data: { hmreqid: hmreqid },
                    async: false,
                    dataType: 'json',
                    success: function (data) {

                        if (data) {
                            alert("삭제되었습니다.");
                            location.href = "/gps/Menu/HazardousMaterialReportList";
                        }


                    }, error: function () {

                        alert("삭제가 실패했습니다.");
                    }
                });
            }
        });

    </script>
</asp:Content>
