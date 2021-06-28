<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<GPS201107.Models.GPBOARD>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Index</title>
    <link href="/gps/Content/GPS201107.css" rel="stylesheet" type="text/css" />
    <link href="/gps/Content/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="/gps/Content/treeview/jquery-treeview.css" rel="stylesheet" type="text/css" />
    <link href="/gps/Content/redmond/jquery-ui-1.8.9.custom.css" rel="stylesheet"
        type="text/css" />
    <link href="/gps/Content/css/input.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/css/table.css" rel="stylesheet" type="text/css" />

    <script src="/gps/Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="/gps/Scripts/jquery-ui-1.8.9.custom.min.js" type="text/javascript"></script>
    <script src="/gps/Scripts/jquery.ui.datepicker-ko.js" type="text/javascript"></script>
    <script src="/gps/Scripts/jquery.validate.js" type="text/javascript"></script>


</head>

<body style="padding-left:108px;padding-top:62px; background-color: #e7f2e4;background-image: url(/gps/Content/images/bg.gif);">
 <form id="form1" action="/gps/account/LogOn"method="post">
   <table border="0" cellpadding="0" cellspacing="0" width="792">        
        <tr>
            <td valign="top"><table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>                        
                        &nbsp;                        
                    </td>
                </tr>
                     <tr>
                         <td style="height: 50px"><img src="/gps/Content/images/login2.gif" alt="" id="IMG1" style="height: 101px;width:750px;margin-bottom: -4px;" /></td>
                     </tr>
                                          
                     <tr>
                        <td style="background-image: url(/gps/Content/images/line06-1.gif); background-repeat: repeat-y;height:296px;padding-left:30px; text-align: center;" align="left">
                            <table border="0" cellpadding="0" cellspacing="0" >
                                <tr>
                                    <td colspan="3" valign="bottom" style="height:37px;">
                                      <img src="/gps/Content/images/pic04.gif" alt="" width="340"/></td>
                                    <td valign="bottom">               
                                    </td>
                                    <td style="width: 302px; vertical-align: top; text-align: center;" valign="bottom" rowspan="6">
                                        <img style="padding-left: 48px;" alt="" src="/gps/Content/images/im19-1.gif" />&nbsp;</td>
                                    <td style="text-align: right" valign="bottom">
                                    </td>
                                </tr>
                                <tr style="height: 163px;">
                                     <td colspan="3" style="background-image: url(/gps/Content/images/pic05.gif); background-repeat: repeat-y; width: 350px;" align="center"><table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td><img src="/gps/Content/images/pic07.gif" alt="" /></td>
                                                    <td style="padding-right:5px;">                           
                                                        <div class="editor-field">
   
                                                             <input type="text" value="" name="UserName" id="UserName">

                                                        </div>             
                                                    </td>
                                                    <td rowspan="2" style="width:100px;">
                                                       
                                                        <%--<input type="submit" value="submit" />--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><img src="/gps/Content/images/pic08.gif" alt="" /></td>
                                                    <td style="padding-right:5px;">                     
                                                         <div class="editor-field">
                           
                                                             <input type="password" name="Password" id="Password1"/>
                                                         </div>
                                                    </td>
                                                </tr>
                                    </table>
                                     <input style="position:absolute; left:390px; top: 260px;" type="image" style=" cursor:pointer" ID="Webimagebutton1"  src="/gps/Content/images/btn12.gif"/>
                                       <asp:Label ID="lblLogin" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="Red" Visible="False"></asp:Label></td>
                                    <td>&nbsp;          </td>
                                </tr>                                               
                                <tr style="height:37px">
                                    <td colspan="3" valign="top">
                                        <img alt="" src="/gps/Content/images/pic12.gif" style="width: 260px; height:25;" />
                                        <a href="/gps/Board/Index/notice">more</a></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" valign="top">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="height:190px">
                                    <td colspan="3" valign="top" style="height: 12px; text-align: center">
                                        <table style="width: 350px; padding-left: 30px;">
        <tr>
            

            <th style="width:80%; text-align:left;" >
            </th>
            <th style="width:20%;" >

            </th>
            
        </tr>


    <% int start_loop = 0;
       foreach (var item in Model)
       {
        %>
               
        <tr>
            <td style="text-align:left;" >
            <%--<a class="bo_td_contents_a_title" href="/board/readboard/notice/<%:item._AF_NUMBER %>"><%: item._AF_TITLE %></a>--%>
            <%-- <a class="bo_td_contents_a_title" href="javascript:window.open('/gps/board/readboard_popup/notice/<%:item._AF_NUMBER %>','','width=400,height=500');void(0);"><%: item._AF_TITLE %></a>--%>



            <a class="bo_td_contents_a_title" href="/gps/board/readboard/notice/<%:item._AF_NUMBER %>"><%: item._AF_TITLE%></a>
            <span style="display:none;" class='notice_row'><%:item._AF_NUMBER%></span>
                
            </td>
            <td>
                <%: item._AF_DATE.ToString().Substring(0, 10)%>
            </td>

        </tr>         
<%} %> 

    </table>

                                    </td>
                                    <td style="height: 12px">
                                    </td>
                                </tr>
                            </table>
                         </td>
                     </tr>
                     <tr>
                         <td style="height: 35px"><img src="/gps/Content/images/pic03-1.gif" alt="" style="width: 750px"/></td>
                     </tr>
                </table>
            </td>
            
        </tr>
        <tr><td colspan="2"><img src="/gps/Content/images/bottom.gif" /></td></tr>
    </table>
 </form>

 <!--팝업 공지사항-->
<%
    foreach (var item in Model)
    {%>
    <%
          if (start_loop == 0 && item._AF_CHECK =="1")
            {%>
            <div id="gps_pop" title="공지 사항">
                <table class="tbl_type2" border="1" cellspacing="0" summary="글 내용을 표시">
                    <caption>글 읽기</caption>
                    <colgroup>
                    <col width="20%">
                    <col width="10%">
                    <col width="20%">
                    <col width="28%">
                    <col width="14%">
                    <col width="8%">
                    </colgroup>
                    <thead>
                        <tr>
                            <th scope="row">제목</th>
                            <td colspan="5">&nbsp;<%=item._AF_TITLE%></td>
                        </tr>
                    </thead>
                    <tbody>
                    <tr>
                        <th scope="row">작성자</th>
                        <td colspan='5'>&nbsp;<%=item._AF_NAME%></td>
                    </tr>
                    <tr>
                        <th scope="row">등록일자</th>
                        <td colspan='3'>&nbsp;<%=item._AF_DATE%></td>
                        <th scope="row">조회수</th>
                        <td>&nbsp;<%=item._AF_READ.ToString()%></td>
                    </tr>
                  
                    <tr>
                        <td style="text-align:center;" class="cont" colspan="6"><textarea cols="54" rows="20" id="xqEditor" name="bo_w_content" readonly="readonly" ><%= item._AF_COMMENT%></textarea> </td>
                    </tr>
                    </tbody>
                </table>           
           </div>
        
            <% 
                start_loop++;               
            }
            
                 
        
        } %>






 <script type="text/javascript">
</script>
</body>
    <script type="text/JavaScript">
        //팝업창
        $(document).ready(function () {

          
            $("#gps_pop").dialog({
                autoOpen: true,
                height: 500,
                width:400,
                modal: false
            });
            
        });

  </script>   

</html>

