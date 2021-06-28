<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<GPS201107.Models.GPBOARD>>" %>

    <table style="width: 350px; padding-left: 30px;">
        <tr>
            

            <th style="width:80%; text-align:left;" >
            </th>
            <th style="width:20%;" >

            </th>
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td style="text-align:left;" >
            <%--<a class="bo_td_contents_a_title" href="/board/readboard/notice/<%:item._AF_NUMBER %>"><%: item._AF_TITLE %></a>--%>
             <a class="bo_td_contents_a_title" href="javascript:window.open('/board/readboard_popup/notice/<%:item._AF_NUMBER %>','','width=400,height=500');void(0);"><%: item._AF_TITLE %></a>
                
            </td>
            <td>
                <%: item._AF_DATE.ToString().Substring(0,10) %>
            </td>

        </tr>
    
    <% } %>

    </table>



