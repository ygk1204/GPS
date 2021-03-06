<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/GPS.Master" Inherits="System.Web.Mvc.ViewPage<GPS201107.Models.HazardousRequest.ViewHazardousMaterial>"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    유해물질 문서
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%  
        //string material_status = Model.gpshmrequest._STATUS;
        //string product = Model.gpshmrequest._PRODUCT;
        //string description = Model.categoryinfo._DESCRIPTION;
        //string customer = Model.gpshmrequest._CUSTOMER;
        //string hazardousmaterialtype = Model.gpshmrequest._HAZARDOUSMATERIALTYPE;
        //int filenum = 0;       
    %>       
    <h2 style="font-weight: bold;">유해 물질 자료 요청</h2>
    <style type="text/css">
        table
        {
            font-size: 11px;
        }
    </style>
    <script src="/gps/Scripts/jquery.Nsform.js" type="text/javascript"></script>
    <link href="/gps/Content/HazardousMaterial.css" rel="stylesheet" type="text/css" />
    
    <form id="frmHazardousMaterial" action="<%=Url.Action("SaveViewHazardousMaterial", "Hazardous", new { editMode = Model.editMode } )%>"
    method="post" enctype="multipart/form-data">l
    <div id="hazardous_material">
        <table style="width: 100%">
            <colgroup>
                <col width="15%" />
                <col width="35%" />
                <col width="15%" />
                <col width="35%" />
            </colgroup>
            <tr>
                <th>요청 ID</th>
                <td>
                    <div>
                        <input id="gpshmrequest._HMREQID" readonly="readonly" name="gpshmrequest._HMREQID"
                            value="<%= Model.gpshmrequest._HMREQID %>" placeholder="자동생성" class="form-control" />
                    </div>
                </td>                
                 <th>상태</th>
                 <td>
                    <div>
                        <%if (Model.editMode == "Modify" && Model.authority == "Admin")
                          {%>
                            <%=Html.DropDownList("gpshmrequest._STATUS", Model.lstStatus, new Dictionary<string, object>() { { "class", "form-control status required_field" } })%>
                        
                         <%}
                          else
                          {  %> <input id="gpshmrequest._STATUS" name="gpshmrequest._STATUS" readonly="readonly"  value="<%= Model.gpshmrequest._STATUS%>" class="form-control" />  <%}%>
                    </div>
                 </td>
            </tr>
            <tr>
                <th>요청 날짜</th>
                <td>
                    <div>
                        <input id="requestdate" name="gpshmrequest._REQUESTDATE" readonly="readonly" placeholder="자동생성" value="<%= Model.gpshmrequest._REQUESTDATE%>" class="form-control" />
                    </div>
                </td>
                <th>요청자 사번</th>
                <td>
                    <div>
                        <input id="requestempno" name="gpshmrequest._REQUESTUSERID" readonly="readonly" value="<%= Model.gpshmrequest._REQUESTUSERID%>"
                            class="form-control" />
                    </div>
                </td>
            </tr>
            <tr>
                <th>요청자 이름</th>
                <td>
                    <div>
                        <input id="requestusername" name="gpshmrequest._REQUESTUSERNAME" readonly="readonly"
                            value="<%= Model.gpshmrequest._REQUESTUSERNAME%>" class="form-control" />
                    </div>
                </td>
                <th>요청자 이메일</th>
                <td>
                    <div>
                        <input id="requestuseremail" name="gpshmrequest._REQUESTUSEREMAIL" readonly="readonly"
                            value="<%= Model.gpshmrequest._REQUESTUSEREMAIL%>" class="form-control" />
                    </div>
                </td>
            </tr>
        </table>
        <h3 style="font-weight: bold;">
            Detail Information</h3>
        <table style="width: 100%">
            <colgroup>
                <col width="15%" />
                <col width="35%" />
                <col width="15%" />
                <col width="35%" />
            </colgroup>
            <tr>
                <th>고객사</th>
                <td width="35%">
                    <div>
                        <%=Html.DropDownList("gpshmrequest._CUSTOMER", new SelectList(Model.lstCustomer, "_CUSTOMERNAME", "_CUSTOMERNAME", Model.gpshmrequest._CUSTOMER),
                         new Dictionary<string, object>() { { "class", "form-control customer required_field" } } ) %>
                    </div>
                </td>
                <th>제품</th>
                <td width="35%">
                    <div>
                        <%=Html.DropDownList("gpshmrequest._PRODUCT", Model.lstProduct, new Dictionary<string, object>() { { "class", "form-control product required_field" } })%>
                  
                    </div>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <colgroup>
                <col width="15%" />
                <col width="85%" />
            </colgroup>
            <tr>
                <th>요청 내용<br /><font size="1">(최대 200자)</font></th>
                <td >
                    <div>
                        <textarea class="form-control requestcomment" id="requestcomment" rows="4" cols="90"
                            name="gpshmrequest._REQUESTCOMMENT" class="form-control"><%=Model.gpshmrequest._REQUESTCOMMENT%></textarea>
                    </div>
                </td>
            </tr>
        </table>
        
        <table style="width: 100%">
            <colgroup>
                <col width="15%" />
                <col width="35%" />
                <col width="15%" />
                <col width="35%" />
            </colgroup>
            <tr>
                <th>유해물질 항목</th>
                <td>
                    <div>
                    
                        <%=Html.DropDownList("gpshmrequest._HAZARDOUSMATERIALTYPE",
                                            new SelectList(Model.lstHazardousMaterialType, "_ITEMNAME", "_ITEMNAME", Model.gpshmrequest._HAZARDOUSMATERIALTYPE),
                                            new Dictionary<string, object>() { { "class", "form-control hazardousmaterialtype required_field" } })
                        %>
                        &nbsp;&nbsp;
                        <input id="leadtime" readonly="readonly" name="leadtime" value="<%=Model.leadtime %>" class="form-control " />
                    </div>                    
                </td>
                <th>담당자</th>
                <td>
                    <%if (Model.authority=="Admin")
                      { %>
                        <%=Html.DropDownList("gpshmrequest._ADMINUSERID", Model.lstAdminUser, new Dictionary<string, object>() { { "class", "form-control adminuserid"}, {"style", "width=200px;"}})%>

                        <input type="hidden" id="adminusername" name="gpshmrequest._ADMINUSERNAME" value="<%= Model.gpshmrequest._ADMINUSERNAME%>" class="form-control " />
                        <input type="hidden" id="adminuseremail" name="gpshmrequest._ADMINUSEREMAIL" value="<%= Model.gpshmrequest._ADMINUSEREMAIL%>" class="form-control " />
                        
                    <%} %>
                </td>
            </tr>
        </table>
        
            <table style="width: 100%">
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
                    <div>
                    <%if (Model.authority == "Admin")
                      {%>   
                        <input id="expectedfinishdate" readonly="readonly" name="gpshmrequest._EXPECTEDFINISHDATE"
                            value="<%= Model.gpshmrequest._EXPECTEDFINISHDATE%>" class="form-control" />
                        <span id="finishdateBtn" class="input-group-addon btn"></span>
                     <%}else{ %>
                        <%} %>
                    </div>
                </td>
                <th>
                    조사자료위치
                </th>
                <td>
                    <div>
                    <%if (Model.authority == "Admin")
                      {%>   
                        <input type="text" class="form-control" style="width:300px" id="hadnouturl" name="gpshmrequest._HADNOUTURL" value="<%=Model.gpshmrequest._HADNOUTURL%>"/>
                        <%} %>
                    </div>
                </td>
            </tr>
        </table> 
        <%if (Model.editMode == "Modify" && Model.authority == "Admin")
          {%>   
           <table class="table_handout_file_list" style="width: 100%">
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
                        <table class="table_handout_file">
                            <colgroup>
                                <col width="15%" />
                                <col width="85%" />
                            </colgroup>
                            <%for (int i = 0; i < Model.lstFileHandOutOnlyView.Count; i++)
                          {%>
                            <tr>
                                <td>
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" name="lstFileHandOutOnlyView[<%=i.ToString()%>]._DELETECHECK" value="DELETED" class="item_handout_file item_handout_file_del_flag" />
                                            삭제
                                        </label>
                                    </div>                          
                                </td>
                                <td>
                                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                                        <%=Model.lstFileHandOutOnlyView[i]._FILENAME%>
                                    </a>
                                    <br />
                                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%=Model.lstFileHandOutOnlyView[i]._PHYSICALFILENAME %>" />
                                    <input type="hidden" name="filename" class="filename" value="<%=Model.lstFileHandOutOnlyView[i]._FILENAME %>" />
                                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%=Model.lstFileHandOutOnlyView[i]._PHYSICALFILELOCATION %>" />
                                    <input type="hidden" name="lstFileHandOutOnlyView[0]._HMREQID" value="<%=Model.lstFileHandOutOnlyView[0]._HMREQID %>" />
                                    <input type="hidden" name="lstFileHandOutOnlyView[0]._FILETYPE" value="<%=Model.lstFileHandOutOnlyView[0]._FILETYPE %>" />
                                    <input type="hidden" name="lstFileHandOutOnlyView[0]._SEQ" value="<%=Model.lstFileHandOutOnlyView[0]._SEQ %>" />
                                    
                                </td>
                            </tr>
                            <%}%>
                        </table>
                        
                        <table class="table_handout_file">
                            <colgroup>
                                <col width="85%" />
                                <col width="15%" />
                            </colgroup>
                            <tr>
                                <td>
                                    <div>
                                        <input size="50" id="lstFileHandOutNew[0]" name="lstFileHandOutNew[0]._FILE_CONTAINER"
                                            class="request_handout_file " type="file" />
                                    </div>
                                </td>
                                <td align="center">
                                    <span class="btn_pack_h medium icon btn_delete_handout_file"><span class="delete"></span>
                                        <button type="button">
                                            삭제</button>
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <span class="btn_pack_h medium icon btn_add_handout_file" style="clear: both;"><span class="add"></span>
                            <button type="button">추가</button>
                        </span>
                    </div>
                </td>
            </tr>
        </table>
        <%}%>

        <table style="width: 100%">
            <colgroup>
                <col width="15%" />
                <col width="85%" />
            </colgroup>
            <tr>
                <th>기타 파일</th>
                <td>
                <label><input type="checkbox" name="lstFileOther[0]._DELETECHECK" value="DELETED"/>삭제</label>
                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);"><%=Model.lstFileOther[0]._FILENAME%></a>
                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%=Model.lstFileOther[0]._PHYSICALFILENAME %>" />
                    <input type="hidden" name="filename" class="filename" value="<%=Model.lstFileOther[0]._FILENAME%>" />
                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%=Model.lstFileOther[0]._PHYSICALFILELOCATION %>" />                   
                    <input type="hidden" name="lstFileOther[0]._HMREQID" value="<%=Model.lstFileOther[0]._HMREQID %>" class="request_other_file" />
                    <input type="hidden" name="lstFileOther[0]._FILETYPE" value="<%=Model.lstFileOther[0]._FILETYPE %>" class="request_other_file" />
                    <input type="hidden" name="lstFileOther[0]._SEQ" value="<%=Model.lstFileOther[0]._SEQ %>" class="request_other_file" />        
                    <input id="other_file" name="lstFileOther[0]._FILE_CONTAINER" value="<%=Model.lstFileOther[0]._FILENAME%>" size="50" class="request_other_file" type="file" />
                </td>
            </tr>
        </table>
            <table style="width: 100%">
            <colgroup>
                <col width="15%" />
                <col width="85%" />
            </colgroup>
            <tr>
                <th>
                    BOM 파일
                </th>
                <td>
                    
                      <label>
                           <input type="checkbox" name="lstFileBom[0]._DELETECHECK" value="DELETED"/>삭제
                       </label>
                    
                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                        <%=Model.lstFileBom[0]._FILENAME%>
                    </a>
                    <br />
                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%=Model.lstFileBom[0]._PHYSICALFILENAME %>" />
                    <input type="hidden" name="filename" class="filename" value="<%=Model.lstFileBom[0]._FILENAME%>" />
                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%=Model.lstFileBom[0]._PHYSICALFILELOCATION %>" />                    
                    <input type="hidden" name="lstFileBom[0]._HMREQID" value="<%=Model.lstFileBom[0]._HMREQID %>" class="request_bom_file" />
                    <input type="hidden" name="lstFileBom[0]._FILETYPE" value="<%=Model.lstFileBom[0]._FILETYPE %>" class="request_bom_file" />
                    <input type="hidden" name="lstFileBom[0]._SEQ" value="<%=Model.lstFileBom[0]._SEQ %>" class="request_bom_file" />        
                    <input id="bom_file" name="lstFileBom[0]._FILE_CONTAINER" value="<%=Model.lstFileBom[0]._FILENAME%>" size="50" class="request_bom_file " type="file" onchange="bom_file_Check()" />
                    <span class="bom_file_box">Excel 파일만 첨부 가능합니다.</span>
                </td>
            </tr>
            <tr>
                <th>
                    고객사 파일
                </th>
                <td>
                    <label>
                           <input type="checkbox" name="lstFileCustomer[0]._DELETECHECK" value="DELETED"/>삭제
                       </label>
                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                        <%=Model.lstFileCustomer[0]._FILENAME%>
                    </a>
                    <br />
                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%=Model.lstFileCustomer[0]._PHYSICALFILENAME %>" />
                    <input type="hidden" name="filename" class="filename" value="<%=Model.lstFileCustomer[0]._FILENAME%>" />
                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%=Model.lstFileCustomer[0]._PHYSICALFILELOCATION %>" />                 
                    <input type="hidden" name="lstFileCustomer[0]._HMREQID" value="<%=Model.lstFileCustomer[0]._HMREQID %>" class="request_customer_file" />
                    <input type="hidden" name="lstFileCustomer[0]._FILETYPE" value="<%=Model.lstFileCustomer[0]._FILETYPE %>" class="request_customer_file" />
                    <input type="hidden" name="lstFileCustomer[0]._SEQ" value="<%=Model.lstFileCustomer[0]._SEQ %>" class="request_customer_file" />                    
                    <input id="customer_file" name="lstFileCustomer[0]._FILE_CONTAINER" value="<%=Model.lstFileCustomer[0]._FILENAME%>" size="50" class="request_customer_file " type="file" />
                </td>
            </tr>
            <tr>
                <th>
                    BD 파일
                </th>
                <td>
                    <label>
                     <input type="checkbox" name="lstFIleBd[0]._DELETECHECK" value="DELETED"/>삭제
                       </label>
                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                        <%=Model.lstFIleBd[0]._FILENAME%>
                    </a>
                    <br />
                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%=Model.lstFIleBd[0]._PHYSICALFILENAME %>" />
                    <input type="hidden" name="filename" class="filename" value="<%=Model.lstFIleBd[0]._FILENAME%>" />
                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%=Model.lstFIleBd[0]._PHYSICALFILELOCATION %>" />
                    <input type="hidden" name="lstFIleBd[0]._HMREQID" value="<%=Model.lstFIleBd[0]._HMREQID %>" class="request_bd_file" />
                    <input type="hidden" name="lstFIleBd[0]._FILETYPE" value="<%=Model.lstFIleBd[0]._FILETYPE %>" class="request_bd_file" />
                    <input type="hidden" name="lstFIleBd[0]._SEQ" value="<%=Model.lstFIleBd[0]._SEQ %>" class="request_bd_file" />
                    <input id="bd_file" name="lstFIleBd[0]._FILE_CONTAINER" value="<%=Model.lstFIleBd[0]._FILENAME%>" size="50" class="request_bd_file "
                        type="file" />
                </td>
            </tr>
            <tr>
                <th>
                    POD 파일
                </th>
                <td>
                <label>
                     <input type="checkbox" name="lstFilePod[0]._DELETECHECK" value="DELETED"/>삭제
                       </label>
                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                        <%=Model.lstFilePod[0]._FILENAME%>
                    </a>
                    <br />
                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%=Model.lstFilePod[0]._PHYSICALFILENAME %>" />
                    <input type="hidden" name="filename" class="filename" value="<%=Model.lstFilePod[0]._FILENAME%>" />
                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%=Model.lstFilePod[0]._PHYSICALFILELOCATION %>" />
                    <input type="hidden" name="lstFilePod[0]._HMREQID" value="<%=Model.lstFilePod[0]._HMREQID %>" class="request_pod_file" />
                    <input type="hidden" name="lstFilePod[0]._FILETYPE" value="<%=Model.lstFilePod[0]._FILETYPE %>" class="request_pod_file" />
                    <input type="hidden" name="lstFilePod[0]._SEQ" value="<%=Model.lstFilePod[0]._SEQ %>" class="request_pod_file" />                    
                    <input id="pod_file" name="lstFilePod[0]._FILE_CONTAINER" value="<%=Model.lstFilePod[0]._FILENAME%>" size="50" class="request_pod_file" type="file" />
                </td>
            </tr>
            <tr>
                <th>
                    Components Part 파일
                </th>
                <td>
                      <label>
                     <input type="checkbox" name="lstFileComponent[0]._DELETECHECK" value="DELETED"/>삭제
                       </label>
                    <a class="downloadfile" href='javascript:void(0);' onclick="downloadfile(this);">
                        <%=Model.lstFileComponent[0]._FILENAME%>
                    </a>
                    <br />
                    <input type="hidden" name="physicalfilename" class="physicalfilename" value="<%=Model.lstFileComponent[0]._PHYSICALFILENAME %>" />
                    <input type="hidden" name="filename" class="filename" value="<%=Model.lstFileComponent[0]._FILENAME%>" />
                    <input type="hidden" name="physicalfilelocation" class="physicalfilelocation" value="<%=Model.lstFileComponent[0]._PHYSICALFILELOCATION %>" />
                    <input type="hidden" name="lstFileComponent[0]._HMREQID" value="<%=Model.lstFileComponent[0]._HMREQID %>" class="request_components_part_file" />
                    <input type="hidden" name="lstFileComponent[0]._FILETYPE" value="<%=Model.lstFileComponent[0]._FILETYPE %>" class="request_components_part_file" />
                    <input type="hidden" name="lstFileComponent[0]._SEQ" value="<%=Model.lstFileComponent[0]._SEQ %>" class="request_components_part_file" />                    
                    <input id="components_part_file" name="lstFileComponent[0]._FILE_CONTAINER" value="<%=Model.lstFileComponent[0]._FILENAME%>" size="50" class="request_components_part_file " type="file" onchange="components_part_file_Check()" />                     
                    <span class="components_file_box">Excel 파일만 첨부 가능합니다.</span>
                </td>
            </tr>
        </table>
        <br />

        <table>
            <tr>
                <!--Start :Die Thickness -->
                <td valign="top">
                    <div class="die_thickness_action">
                        <table style="width: 100%">
                            <colgroup>
                                <col width="5%" />
                                <col width="85%" />
                                <col width="10%" />
                            </colgroup>
                            <tr>
                                <th>No</th>
                                <th align="left" > Die Thickness</th>
                                <th>
                                    <span class="btn_pack_h medium icon btn_add_die_thickness" style="clear: both; ">
                                        <span class="add"></span>
                                        <button type="button">추가</button>
                                    </span>
                                </th>
                            </tr>
                            
                            <%for (int i = 0; i < Model.lstItemDieThickness.Count; i++)
                              {%>
                            <tr class="tr_die_thickness_action">
                                    <td align="center" style="vertical-align: middle;" class="die_thickness_seq">
                                        <%= (i + 1).ToString()%>
                                    </td>
                                    <td>
                                        <input type="text" id="die_thickness" name="lstItemDieThickness[<%=i.ToString()%>]._ITEMVALUE" value="<%=Model.lstItemDieThickness[i]._ITEMVALUE%>" style="width: 50px; height: 20px;" class="request_die_thickness" />
                                    </td>
                                    <td align="center">
                                        <span class="btn_pack_h medium icon btn_delete_die_thickness"><span class="delete"></span>
                                            <button type="button">삭제</button></span>
                                    </td>
                                </tr>
                            <%}%>
                        </table>
            
                </div>
                </td>
                <!--Start : PCB Thickness -->
                <td valign="top">
                    <div class="pcb_thickness_action">
                        <table class="table_pcb_thickness" style="width: 100%">
                <colgroup>
                    <col width="5%" />
                    <col width="85%" />
                    <col width="10%" />
                </colgroup>
                <tr>
                    <th style="background-color: #cff5b7; font-weight: bold;">
                        No
                    </th>
                    <th style="background-color: #cff5b7; font-weight: bold;" align="left">
                        Pcb Thickness                        
                    </th>
                    <th>
                        <span class="btn_pack_h medium icon btn_add_pcb_thickness" style="clear: both;">
                            <span class="add"></span>
                            <button type="button">추가</button>
                        </span></th>
                </tr>
                <%for (int i = 0; i < Model.lstItemPcbThickness.Count; i++)
                  {%>
                <tr class="tr_pcb_thickness_action">
                    <td align="center" style="vertical-align: middle;" class="pcb_thickness_seq">
                        <%= (i + 1).ToString()%>
                    </td>
                    <td>
                        <input type="text" style="width: 50px; height: 20px;" id="pcb_thickness" name="lstItemPcbThickness[<%=i.ToString()%>]._ITEMVALUE" value="<%=Model.lstItemPcbThickness[i]._ITEMVALUE%>" class="request_pcb_thickness" />
                    </td>
                    <td align="center">
                        <span class="btn_pack_h medium icon btn_delete_pcb_thickness">
                            <span class="delete"></span>
                            <button type="button">삭제</button>
                        </span>
                    </td>
                </tr>
                <%}%>
            </table>            
                    </div>
                </td>
                <!--Start : Package Thickness -->
                <td valign="top">
                <div class="pkg_thickness_action">
            <table class="table_pkg_thickness" style="width: 100%">
                <colgroup>
                    <col width="5%" />
                    <col width="85%" />
                    <col width="10%" />
                </colgroup>
                <tr>
                    <th>
                        No
                    </th>
                    <th align="left">
                        Pkg Thickness
                        
                    </th>
                    <th>
                        <span class="btn_pack_h medium icon btn_add_pkg_thickness" style="clear: both; ">
                            <span class="add"></span>
                            <button type="button">추가</button>
                        </span>                        
                    </th>
                </tr>
                <%for (int i = 0; i < Model.lstItemPkgThickness.Count; i++)
                  {%>
                <tr class="tr_pkg_thickness_action">
                    <td align="center" style="vertical-align: middle;" class="pkg_thickness_seq">
                        <%= (i + 1).ToString()%>
                    </td>
                    <td>                      
                        <input type="text" style="width: 50px; height: 20px;" id="pkg_thickness" name="lstItemPkgThickness[<%=i.ToString()%>]._ITEMVALUE" value="<%=Model.lstItemPkgThickness[i]._ITEMVALUE%>" class="request_pkg_thickness" />
                    </td>
                    <td align="center">
                        <span class="btn_pack_h medium icon btn_delete_pkg_thickness">
                            <span class="delete"></span>
                            <button type="button">삭제</button>
                        </span>
                    </td>
                </tr>
                <%}%>
            </table>            
        </div>
                </td>
            </tr>
        </table>
        <br />
        
        
        
        <div class="shield_part_action">
            <table class="table_shield_part" style="width: 100%">
                <colgroup>
                    <col width="5%" />
                    <col width="35%" />
                    <col width="50%" />
                    <col width="10%" />
                </colgroup>
                <tr>
                    <th>
                        No
                    </th>
                    <th align="left">
                        Shield Part
                    </th>
                    <th align="left">
                        Supplier
                    </th>
                    <th >
                        <span class="btn_pack_h medium icon btn_add_shield_part" style="clear: both; ">
                            <span class="add"></span>
                            <button type="button">추가</button>
                        </span>                        
                    </th>
                </tr>
                <%for (int i = 0; i < Model.lstItemShieldPart.Count; i++)
                  {%>
                     <%-- string[] slstItemShieldPart = null;

                      if (Model.lstItemShieldPart[i]._ITEMVALUE != null)
                          slstItemShieldPart = Model.lstItemShieldPart[i]._ITEMVALUE.ToString().Replace("¶¶", "¶").Split('¶');
                      string sPartName = "", sCompanyName = "";

                      if (slstItemShieldPart != null && slstItemShieldPart.Length > 1)
                      {
                          sPartName = slstItemShieldPart[0];
                          sCompanyName = slstItemShieldPart[1];
                      }--%>

                
                <tr class="tr_shield_part_action">
                    <td align="center" style="vertical-align: middle;" class="shield_part_seq">
                        <%= (i + 1).ToString()%>
                    </td>
                    <td>
                        <input type="text" id="shield_partnum" name="lstItemShieldPart[<%=i.ToString()%>]._PARTNAME" value="<%=Model.lstItemShieldPart[i]._PARTNAME%>" style="width: 250px; height: 20px;" class="request_shield_part" />
                    </td>
                    <td>
                        <input type="text" id="shield_companyname" name="lstItemShieldPart[<%=i.ToString()%>]._COMPANYNAME" value="<%=Model.lstItemShieldPart[i]._COMPANYNAME%>" style="width: 250px; height: 20px;" class="request_shield_part"/>
                    </td>
                    <td align="center">
                        <span class="btn_pack_h medium icon btn_delete_shield_part">
                            <span class="delete"></span>
                            <button type="button">삭제</button>
                        </span>
                    </td>
                </tr>
                <%}%>
            </table>            
        </div>
        <br />
        <div class="ball_part_action">
            <table class="table_ball_part" style="width: 100%">
                <colgroup>
                    <col width="5%" />
                    <col width="35%" />
                    <col width="50%" />
                    <col width="10%" />
                </colgroup>
                <tr>
                    <th>No</th>
                    <th align="left">Ball Part</th>
                    <th align="left">Ball Part Supplier</th>
                    <th>
                        <span class="btn_pack_h medium icon btn_add_ball_part" style="clear: both; ">
                            <span class="add"></span>
                            <button type="button">추가</button>
                        </span>
                    </th>
                </tr>
                <%for (int i = 0; i < Model.lstItemBallPart.Count; i++)
                  {%>
                    <tr class="tr_ball_part_action">
                        <td align="center" style="vertical-align: middle;" class="ball_part_seq">
                            <%= (i + 1).ToString()%>
                        </td>
                        <td>                           
                            <input type="text" id="ball_partnum" name="lstItemBallPart[<%=i.ToString()%>]._PARTNAME" value="<%=Model.lstItemBallPart[i]._PARTNAME%>" style="width: 250px; height: 20px;" class="request_ball_part"  />
                        </td>
                        <td>
                            <input type="text" id="ball_part_companyname" name="lstItemBallPart[<%=i.ToString()%>]._COMPANYNAME" value="<%=Model.lstItemBallPart[i]._COMPANYNAME%>" style="width: 250px; height: 20px;" class="request_ball_part"  />
                        </td>
                        <td align="center">
                            <span class="btn_pack_h medium icon btn_delete_ball_part"><span class="delete"></span>
                                <button type="button">
                                    삭제</button></span>
                        </td>
                    </tr>
                <%}%>
            </table>            
        </div>
        <br />
        <div class="bump_die_action">
            <table class="table_bump_die" style="width: 100%">
                <colgroup>
                    <col width="5%" />
                    <col width="20%" />
                    <col width="20%" />
                    <col width="20%" />
                    <col width="25%" />
                    <col width="10%" />
                </colgroup>
                <tr>
                    <th>
                        No
                    </th>
                    <th align="left"> Bump Die 성분
                    </th>
                    <th align="left"> Bump Die Count
                    </th>
                    <th align="left"> Bump Die  Density
                    </th>
                    <th align="left"> Bump Die Diameter
                    </th>                    
                    <th>
                        <span class="btn_pack_h medium icon btn_add_bump_die" style="clear: both;">
                            <span class="add"></span>
                            <button type="button">추가</button>
                        </span>
                    </th>
                </tr>
               <%for (int i = 0; i < Model.lstItemBumpDie.Count; i++)
                  {%>          
                <tr class="tr_bump_die_action">
                    <td align="center" style="vertical-align: middle;" class="bump_die_seq">
                        <%= (i + 1).ToString()%>
                    </td>
                    <td><input type="text" id="bump_die" name="lstItemBumpDie[<%=i.ToString()%>]._INGREDIENT" value="<%=Model.lstItemBumpDie[i]._INGREDIENT%>" style="width: 130px; height: 20px;" class="request_bump_die" /></td>
                    <td><input type="text" id="count" name="lstItemBumpDie[<%=i.ToString()%>]._COUNT" value="<%=Model.lstItemBumpDie[i]._COUNT%>" style="width: 130px; height: 20px;" class="request_bump_die" /></td>
                    <td><input type="text" id="density" name="lstItemBumpDie[<%=i.ToString()%>]._DENSITY" value="<%=Model.lstItemBumpDie[i]._DENSITY%>" style="width: 130px; height: 20px;" class="request_bump_die" /></td>
                    <td><input type="text" id="diameter" name="lstItemBumpDie[<%=i.ToString()%>]._DIAMETER" value="<%=Model.lstItemBumpDie[i]._DIAMETER%>" style="width: 130px; height: 20px;" class="request_bump_die" /></td>
                    <td align="center">
                        <span class="btn_pack_h medium icon btn_delete_bump_die">
                            <span class="delete"></span>
                            <button type="button">삭제</button>
                        </span>
                    </td>
                </tr>
                <%}%>
            </table>            
        </div>
    </div>
    </form>
    <br />    
      
    <div id="btn_form_area" align="center" style="width: 100%">
        <button id="back_button" class="btn-btn-info" style="width: 76px; height: 24px;"><span></span> 목록으로</button>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <%if (Model.editMode == "Request")
        {%>
            <button type="button" id="btnRequest" class="btn btn-info" style="width: 76px; height: 24px;"><span></span>제출</button>
        <%} else if (Model.editMode== "Modify")
        {%>
            <button type="button" id="btnModify" class="btn-btn-info" style="width: 76px; height: 24px;"><span></span>수정</button>
        <%}%>
    </div>
    
    <form method="post" id="downloadform" action="/gps/Hazardous/DownloadFile">
    <input type="hidden" id="physicalfilename" name="physicalfilename" />
    <input type="hidden" id="filename" name="filename" />
    <input type="hidden" id="physicalfilelocation" name="physicalfilelocation" />
    </form>

    <script type="text/javascript">
        $(".hazardousmaterialtype").change(function () {
            var itemname = $(this).val();
            if (itemname == "") {
                alert("유해물질 항목을 선택하세요");
                return false;
            }

            $.ajax({ url: '/gps/Hazardous/GetHazardusMaterialTypeDescription',
                type: "POST",
                data: { itemname: itemname },
                datatype: 'json',
                success: function (data) {

                    if (data == "") {
                        $("#leadtime").empty();
                        return false;
                    }
                    else {

                        $("#leadtime").val(data);
                        return true;
                    }
                }, error: function (data) {

                    alert('lead Time 조회에 실패했습니다.');
                }
            });
        });
        $(".adminuserid").change(function () {
            var adminuserid = $(this).val();            
            if (adminuserid == "") {
                alert("담당자을 선택하세요.");
                return false;
            }

            $.ajax({ url: '/gps/Hazardous/GetAdminUserInfo',
                type: "POST",
                data: { empno: adminuserid },
                datatype: 'json',
                success: function (data) {

                    if (data == "") {
                        $("#adminusername").val("");
                        $("#adminuseremail").val("");
                        return false;
                    }
                    else {                        
                        $("#adminusername").val(data[0]);
                        $("#adminuseremail").val(data[1]);
                        return true;
                    }
                }, error: function (data) {
                    alert('담당자 조회를 할수 없습니다.');
                }
            });
        });



    </script>
    <script type="text/javascript">
        var getTextLength = function (str) {
            var len = 0;
            for (var i = 0; i < str.length; i++) {
                if (escape(str.charAt(i)).length == 6) {
                    len++;
                }
                len++;
            }
            return len;
        }

        // 최대값에 해당하는 텍스트를 구해준다
        var getText = function (str, maxlength) {
            var len = 0;
            var input = '';
            for (var i = 0; i < str.length; i++) {
                if (escape(str.charAt(i)).length == 6) {
                    input += str.charAt(i)
                    len++;
                } else {
                    input += str.charAt(i)
                }
                len++;

                if (len == maxlength) {
                    break;
                }
            }
            return input;
        }



    </script>
    <script type="text/javascript">

        var text = '';

        //불완성형 한글 정규식 제외시키기
        var replaceNotFullKorean = /[ㄱ-ㅎㅏ-ㅣ]/gi;


        //요청 내용 (최대 50자 까지 제한)
        $(".requestcomment").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);
            }

        }).keyup(function (e) {

            if (getTextLength($(this).val()) > 200) {

                alert("최대 200자까지 입력 가능합니다.");
                text = getText($(this).val(), 50);
                $(this).val($(this).val().substring(0, text.length));

            }

        });

        //조사 자료위치
        $("#hadnouturl").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);
            }
        });


        //Text 입력 항목
        $("#die_thickness").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);

            }

        });

        $("#pcb_thickness").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);
            }
        });

        $("#pkg_thickness").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);
            }
        });


        $("#shield_partnum").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);
            }

        });



        $("#shield_companyname").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);
            }

        });


        $("#ball_partnum").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);
            }
        });


        $("#ball_part_companyname").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);
            }
        });


        $("#bump_die").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);
            }
        });



        $("#count").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);
            }
        });


        $("#density").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);
            }
        });


        $("#diameter").focusout(function () {
            var x = $(this).val();
            if (x.length > 0) {
                if (x.match(replaceNotFullKorean)) {
                    x = x.replace(replaceNotFullKorean, "");
                }
                $(this).val(x);
            }

        });
    </script>
    <script type="text/javascript">
        function validate_action_Request() {
            var hazardousmaterialtype = $(".hazardousmaterialtype").val();
            var bomfile = $("#bom_file").val();
            var customerfile = $("#customer_file").val();
            var podfile = $("#pod_file").val();
            var bdfile = $("#bd_file").val();
            var components_part_file = $("#components_part_file").val();

            var die_thickness = $("#die_thickness").val();
            var pcb_thickness = $("#pcb_thickness").val();
            var pkg_thickness = $("#pkg_thickness").val();

            var shield_partnum = $("#shield_partnum").val();
            var shield_companyname = $("#shield_companyname").val();

            var ball_partnum = $("#ball_partnum").val();
            var ball_companyname = $("#ball_part_companyname").val();

            var bump_die = $("#bump_die").val();  //성분
            var count = $("#count").val();  //카운트
            var density = $("#density").val();
            var diameter = $("#diameter").val();


            if (hazardousmaterialtype == "") {
                alert("유해물질 항목을 입력하세요");
                return false;

            }
            if (hazardousmaterialtype == "유해물질_TRF" || hazardousmaterialtype == "MD_환경문서" || hazardousmaterialtype == "MD_ASE" || hazardousmaterialtype == "유해물질_SDC") {

                if (bomfile == "") {
                    alert(hazardousmaterialtype + " 선택 시 BOM 파일은 반드시 첨부해 주세요.");
                    return false;
                }

                if (customerfile == "") {
                    alert(hazardousmaterialtype + " 선택 시  고객사 파일은 반드시 첨부해 주세요.");
                    return false;
                }
                if (bdfile == "") {
                    alert(hazardousmaterialtype + " 선택 시  BD 파일은 반드시 첨부해 주세요.");
                    return false;
                }
                if (podfile == "") {
                    alert(hazardousmaterialtype + " 선택 시  POD 파일은 반드시 첨부해 주세요.");
                    return false;
                }
                if (components_part_file == "") {
                    alert(hazardousmaterialtype + " 선택 시  Components Part 파일은 반드시 첨부해 주세요.");
                    return false;
                }
                if (die_thickness == "") {
                    alert(hazardousmaterialtype + " 선택 시 Die thickness 정보를 반드시 입력해 주세요.");
                    return false;
                }
                if (pcb_thickness == "") {
                    alert(hazardousmaterialtype + " 선택 시 Pcb thickness 정보를 반드시 입력해 주세요.");
                    return false;
                }
                if (pkg_thickness == "") {
                    alert(hazardousmaterialtype + " 선택 시 Pkg thickness 정보를 반드시 입력해 주세요.");
                    return false;
                }

                if (shield_partnum == "") {

                    alert(hazardousmaterialtype + " 선택 시 Shield part 의 파트 정보를 반드시 입력해 주세요.");
                    return false;
                }

                if (shield_companyname == "") {
                    alert(hazardousmaterialtype + " 선택 시 Shield part 의 업체명 정보를 반드시 입력해 주세요.");
                    return false;

                }

                if (ball_partnum == "") {

                    alert(hazardousmaterialtype + " 선택 시 Ball part 의 파트 정보를 반드시 입력해 주세요.");
                    return false;
                }

                if (ball_companyname == "") {
                    alert(hazardousmaterialtype + " 선택 시 Ball part 의 업체명 정보를 반드시 입력해 주세요.");
                    return false;

                }

                if (bump_die == "") {
                    alert(hazardousmaterialtype + " 선택 시 Bump die 의 성분 정보를 반드시 입력해 주세요.");
                    return false;
                }

                if (count == "") {
                    alert(hazardousmaterialtype + " 선택 시 Bump die 의 수량 정보를 반드시 입력해 주세요.");
                    return false;
                }


                if (density == "") {
                    alert(hazardousmaterialtype + " 선택 시 Bump die 의 density 정보를 반드시 입력해 주세요.");
                    return false;
                }

                if (diameter == "") {
                    alert(hazardousmaterialtype + " 선택 시 Bump die 의 diameter 정보를 반드시 입력해 주세요.");
                    return false;
                }

            } else if (hazardousmaterialtype == "유해물질_SOA") {
                if (bomfile == "" || customerfile == "") {
                    alert(hazardousmaterialtype + " 선택 시, Bom 파일과 고객사 파일을 반드시 첨부해 주세요 ");
                    return false;
                }
            } else {
                if (bomfile == "") {
                    alert(hazardousmaterialtype + " 선택 시, Bom 파일은 반드시 첨부해 주세요");
                    return false;
                }
            }
            return true;
        } //end function  validate_action_Request()  
    </script>
    <script type="text/javascript">
        //저장 버튼클릭시
        $("#btnRequest").click(function () {

            //필수값 체크
            if (validate_action_Request() == false) { return false; }
            if (validation_form() == false) { return false; }
            if (components_part_file_Check() == false) { return false; }
            if (bom_file_Check() == false) { return false; }

            var confirmvalue = confirm("제출하시겠습니까?");
            if (confirmvalue == false) { return false; }

            $("#btnRequest").attr("disabled", true);
            $("#btnRequest").html("등록중");
            $('#frmHazardousMaterial').submit();

        });

        $("#btnModify").click(function () {
            var sRequest_Result = false;
            var bom_file = $("#bom_file").val();
            var components_file = $("#components_part_file").val();

            if (validation_form() == false) { return false; }
            if (bom_file != "") {
                if (bom_file_Check() == false) { return false; }
            }
            if (components_file != "") {
                if (components_part_file_Check() == false) { return false; }
            }
            var confirmvalue = confirm("수정하시겠습니까?");

            if (confirmvalue == false) { return false; }
            $("#btnModify").attr("disabled", true);
            $("#btnModify").html("수정중");
            $('#frmHazardousMaterial').submit();

            return true;
        });

    </script>
    <script type="text/javascript">
        // Nsform 항목
        $(".die_thickness_action").Nsform({
            area: "die_thickness_action"
                , form: "tr_die_thickness_action"
                , item: "request_die_thickness"
                , btn_add: "btn_add_die_thickness"
                , btn_delete: "btn_delete_die_thickness"
                , BeforeAdd: function () {
                    var inputSeq = $(".tr_die_thickness_action").length;
                    if (inputSeq >= 3) {
                        alert("3개 이상 입력 하실 수 없습니다.");
                        return false;
                    }
                }
            , AfterAdd: function (newItems) {

                var index = $("tr_die_thickness_action").length - 1;
                $(".die_thickness_seq").eq(index).text(Number($(".die_thickness_seq").eq(index).text()) + 1);

            }
            , BeforeDelete: function () {

                if ($(".btn_delete_die_thickness:visible").length == 1) {
                    alert("더 이상 삭제 할 수 없습니다.");
                    return false;
                }

            }
            , AfterDelete: function () {
                var formcount = $(".tr_die_thickness_action").length;
                for (var i = 0; i < formcount; i++) {
                    $(".die_thickness_seq").eq(i).text(i + 1);
                }
            }
        });

        $(".pcb_thickness_action").Nsform({
            area: "pcb_thickness_action"
            , form: "tr_pcb_thickness_action"
            , item: "request_pcb_thickness"
            , btn_add: "btn_add_pcb_thickness"
            , btn_delete: "btn_delete_pcb_thickness"
            , BeforeAdd: function () {
                var inputSeq = $(".tr_pcb_thickness_action").length;
                if (inputSeq >= 3) {
                    alert("3개 이상 입력 하실 수 없습니다.");
                    return false;
                }
            }
            , AfterAdd: function (newItems) {

                var index = $("tr_pcb_thickness_action").length - 1;
                $(".pcb_thickness_seq").eq(index).text(Number($(".pcb_thickness_seq").eq(index).text()) + 1);
            }
            , BeforeDelete: function () {

                if ($(".btn_delete_pcb_thickness:visible").length == 1) {
                    alert("더 이상 삭제 할 수 없습니다.");
                    return false;
                }
            }
            , AfterDelete: function () {
                var formcount = $(".tr_pcb_thickness_action").length;
                for (var i = 0; i < formcount; i++) {
                    $(".pcb_thickness_seq").eq(i).text(i + 1);
                }
            }
        });


        $(".pkg_thickness_action").Nsform({

            area: "pkg_thickness_action"
            , form: "tr_pkg_thickness_action"
            , item: "request_pkg_thickness"
            , btn_add: "btn_add_pkg_thickness"
            , btn_delete: "btn_delete_pkg_thickness"
            , BeforeAdd: function () {
                var inputSeq = $(".tr_pkg_thickness_action").length;
                if (inputSeq >= 3) {
                    alert("3개 이상 입력 하실 수 없습니다.");
                    return false;
                }
            }
            , AfterAdd: function (newItems) {

                var index = $("tr_pkg_thickness_action").length - 1;
                $(".pkg_thickness_seq").eq(index).text(Number($(".pkg_thickness_seq").eq(index).text()) + 1);

            }
            , BeforeDelete: function () {

                if ($(".btn_delete_pkg_thickness:visible").length == 1) {
                    alert("더 이상 삭제 할 수 없습니다.");
                    return false;
                }
            }
            , AfterDelete: function () {
                var formcount = $(".tr_pkg_thickness_action").length;
                for (var i = 0; i < formcount; i++) {
                    $(".pkg_thickness_seq").eq(i).text(i + 1);
                }
            }
        });

        $(".shield_part_action").Nsform({

            area: "shield_part_action"
            , form: "tr_shield_part_action"
            , item: "request_shield_part"
            , btn_add: "btn_add_shield_part"
            , btn_delete: "btn_delete_shield_part"
            , BeforeAdd: function () {
                var inputSeq = $(".tr_shield_part_action").length;


                if (inputSeq >= 3) {
                    alert("3개 이상 입력 하실 수 없습니다.");
                    return false;
                }
            }
            , AfterAdd: function (newItems) {
                var index = $("tr_shield_part_action").length - 1;
                $(".shield_part_seq").eq(index).text(Number($(".shield_part_seq").eq(index).text()) + 1);
            }
            , BeforeDelete: function () {

                if ($(".btn_delete_shield_part:visible").length == 1) {
                    alert("더 이상 삭제 할 수 없습니다.");
                    return false;
                }

            }
            , AfterDelete: function () {
                var formcount = $(".tr_shield_part_action").length;
                for (var i = 0; i < formcount; i++) {
                    $(".shield_part_seq").eq(i).text(i + 1);
                }
            }
        });

        $(".ball_part_action").Nsform({
            area: "ball_part_action"
            , form: "tr_ball_part_action"
            , item: "request_ball_part"
            , btn_add: "btn_add_ball_part"
            , btn_delete: "btn_delete_ball_part"
            , BeforeAdd: function () {
                var inputSeq = $(".tr_ball_part_action").length;

                if (inputSeq >= 3) {
                    alert("3개 이상 입력 하실 수 없습니다.");
                    return false;
                }
            }
            , AfterAdd: function (newItems) {
                var index = $("tr_ball_part_action").length - 1;
                $(".ball_part_seq").eq(index).text(Number($(".ball_part_seq").eq(index).text()) + 1);

            }
            , BeforeDelete: function () {

                if ($(".btn_delete_ball_part:visible").length == 1) {
                    alert("더 이상 삭제 할 수 없습니다.");
                    return false;
                }
            }
            , AfterDelete: function () {
                var formcount = $(".tr_ball_part_action").length;
                for (var i = 0; i < formcount; i++) {
                    $(".ball_part_seq").eq(i).text(i + 1);
                }
            }
        });

        $(".bump_die_action").Nsform({

            area: "bump_die_action"
            , form: "tr_bump_die_action"
            , item: "request_bump_die"
            , btn_add: "btn_add_bump_die"
            , btn_delete: "btn_delete_bump_die"
            , BeforeAdd: function () {
                var inputSeq = $(".tr_bump_die_action").length;
                if (inputSeq >= 3) {
                    alert("3개 이상 입력 하실 수 없습니다.");
                    return false;
                }
            }
            , AfterAdd: function (newItems) {
                var index = $("tr_bump_die_action").length - 1;
                $(".bump_die_seq").eq(index).text(Number($(".bump_die_seq").eq(index).text()) + 1);
            }
            , BeforeDelete: function () {

                if ($(".btn_delete_bump_die:visible").length == 1) {
                    alert("더 이상 삭제 할 수 없습니다.");
                    return false;
                }
            }
            , AfterDelete: function () {
                var formcount = $(".tr_bump_die_action").length;
                for (var i = 0; i < formcount; i++) {
                    $(".bump_die_seq").eq(i).text(i + 1);
                }
            }
        });

        $(".handout_file").Nsform({
            area: "handout_file"
            , form: "table_handout_file"
            , item: "request_handout_file"
            , btn_add: "btn_add_handout_file"
            , btn_delete: "btn_delete_handout_file"
            , BeforeAdd: function () {
                var totalFileCount = 0;
                var deleteFileCount = $(".item_handout_file_del_flag:checked").length;
                var inputFileCount = $(".table_handout_file").length;
                totalFileCount = inputFileCount - deleteFileCount;
                if (totalFileCount >= 5) {
                    alert("5개 이상 첨부 하실 수 없습니다.");
                    return false;
                }
            }
            , AfterAdd: function (newItems) {
                return true;
            }
            , BeforeDelete: function () {
                if ($(".btn_delete_handout_file:visible").length == 1) {
                    alert("더 이상 삭제 할 수 없습니다.");
                    return false;
                }
            }
            , AfterDelete: function () {
            }
        }); 
    </script>
    <script type="text/javascript">
        //Components Excel 파일 검증

        function components_part_file_Check() {
            var fileName = $("#components_part_file").val();
            var pathFileName = fileName.lastIndexOf(".") + 1;    //확장자 제외한 경로+파일명
            var fileNameExt = fileName.lastIndexOf("\\") + 1;    //파일경로를 제외한 파일명+확장자
            var extension = (fileName.substr(pathFileName, fileName.length)).toLowerCase(); //확장자명

            //파일명.확장자
            var fileNameCheck = fileName.substring(fileNameExt, fileName.length).toLowerCase();

            if (fileNameCheck.length != 0) {
                if (extension == "xlsx" || extension == "xls") {
                    return true;
                }
                else {

                    alert("Components Part File은 엑셀 파일만 업로드 할 수 있습니다.");
                    $("#components_part_file").val("");
                    return false;
                }
            } else return true;
        }
        function bom_file_Check() {
            var fileName = $("#bom_file").val();
            var pathFileName = fileName.lastIndexOf(".") + 1;    //확장자 제외한 경로+파일명
            var fileNameExt = fileName.lastIndexOf("\\") + 1;    //파일경로를 제외한 파일명+확장자
            var extension = (fileName.substr(pathFileName, fileName.length)).toLowerCase(); //확장자명

            //파일명.확장자
            var fileNameCheck = fileName.substring(fileNameExt, fileName.length).toLowerCase();

            if (fileNameCheck.length != 0) {
                if (extension == "xlsx" || extension == "xls") {
                    return true;
                }
                else {
                    alert("Bom File은 엑셀 파일만 업로드 할 수 있습니다.");
                    $("#bom_file").val("");
                    return false;
                }
            } else
                return true;
        }

        //뒤로가기
        $("#back_button").click(function () {
            location.href = "/gps/Menu/HazardousMaterialReportList";
        });

        //파일 다운로드
        function downloadfile(obj) {
            var index = $('.downloadfile').index(obj);

            //form 으로 값 전송
            $("#physicalfilename").val($('.physicalfilename').eq(index).val());
            $("#filename").val($('.filename').eq(index).val());
            $("#physicalfilelocation").val($('.physicalfilelocation').eq(index).val());

            var form = $("#downloadform");
            form.submit();
        }
    </script>
    <script type="text/javascript">
        var editMode = '<%=Model.editMode%>';
        $(function () {
            if (editMode == "Modify") {
                var customer = $(".customer").val();
                if (customer == "") {
                    alert("고객사를 선택해주세요");
                    return false;
                } //end if(customer=="")
            }
        });
        // 예상완료날짜 선택 (Admin 권한)

        $(function () {
            $.datepicker.regional['ko'].dateFormat = "yy-mm-dd";
            $.datepicker.setDefaults($.datepicker.regional[""]);
            $("#expectedfinishdate").datepicker($.datepicker.regional['ko']);
        });

        //요청날짜 선택
        $("#finishdateBtn").click(function () {
            $("#expectedfinishdate").datepicker("show");
        });

        //파일 첨부 크기 제한
        $('input:file').bind('change', function () {
            if (this.files[0] != null) {
                var filesize = this.files[0].size;
                if (filesize > 20971520) {
                    alert("20 MB 이상파일은 업로드 할 수 없습니다.");
                    $(this).val('');
                    return;
                }
            }
        });
    </script>
    <script type="text/javascript">
        //필수값 미입력시 ! 표시
        var ErrorMessage = "<span class='warning1'>!!</span>";
        function validation_form() {
            var result = true;
            $(".required_field:enabled").each(function () {
                var formValues = $.trim($(this).val());
                if (formValues == "" || formValues == null) {
                    result = false;
                    if ($(this).parent().find('.warning1').length == 0) {
                        alert("필수값을 입력하세요");
                        $(this).parent().append(ErrorMessage);
                    }
                }
            });
            return result;
        }

        /*고객 정보 변경시 Product 정보를 갱신한다.*/
        $(".customer").change(function () {
            var customer = $(this).val();
            if (customer == "") {
                alert("고객사를 선택하세요");
                return false;
            }

            $.ajax({ url: '/gps/Hazardous/GetProductName',
                type: "POST",
                data: { customer: customer },
                datatype: 'json',
                success: function (data) {
                    if (data == "") {
                        alert('Customer에 해당하는 ProductName이 없습니다.');
                        $(".product").empty();
                        return false;
                    } else {
                        $(".product").empty();
                        for (var i = 0; i < data.length; i++) {
                            $(".product").append($('<option></option>').val(data[i].Value).html(data[i].Text));
                        }
                    }

                }, error: function (data) {
                    alert('ProductName 조회에 실패하였습니다.');
                }
            });
        });

        //customer를 선택하지 않고 product를 먼저 선택했을 경우
        $(".product").click(function () {

            var customer = $(".customer").val();

            if ((customer == "" || customer.length == 0)) {
                alert("Product 선택 전, Customer를 먼저 선택해주세요");
            }

        });
    </script>
</asp:Content>
