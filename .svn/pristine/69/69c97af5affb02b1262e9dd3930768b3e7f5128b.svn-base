<%@ Page Language="C#" MasterPageFile="~/Filemanager/Shared/GPS.Master" Title="Untitled Page" %>

<%@ Import Namespace="System.Reflection" %>
<%@ Register Assembly="IZ.WebFileManager" Namespace="IZ.WebFileManager" TagPrefix="iz" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

	<div>
		<iz:FileManager ID="FileManager1" runat="server" Height="400" Width="850">
			<RootDirectories>
				<iz:RootDirectory DirectoryPath="/gps/Files/My Documents/" Text="My Documents" />
			</RootDirectories>
		</iz:FileManager>
	</div>
<%
    // 권한이 없는 유저는 파일업로드 및 폴더 생성 삭제 없애기
    string id = User.Identity.Name;
    string Role = "";
    if (Session["Authority"] == null)
    {

        ASEWCFServiceLibrary.App_Code.clsDBControl oDBCon = new ASEWCFServiceLibrary.App_Code.clsDBControl(ASEWCFServiceLibrary.App_Code.clsConst.DBPROVIDER.SCM); //test server            
        string sSql = "select authority from GPSUSER where emp_no ='" + id + "'";
        string Authrority = "";
        if (id == "" || id == null)
        {
            Role = "Guest";
        }
        else
        {

          Authrority = oDBCon.QuerySingleData(sSql);
        }
        if (Authrority == "")
        {
            Role = "Guest";
        }
    }
    else
    {
        Role = Session["Authority"].ToString();
    }
        
     %>
<% if (Role == "Guest")
   {
       %>
              <script type="text/javascript">
                  $(document).ready(function () {

                              $('#MainContent_FileManager1_tb0').remove();
                              $('#MainContent_FileManager1_tb1').remove();
                              $('#MainContent_FileManager1_tb2').remove();
                              $('#MainContent_FileManager1_tb3').remove();
                              $('#MainContent_FileManager1_tb4').remove();


                              $('#MainContent_FileManager1_FileViewCMDCopy').parent().prev('div').remove();
                              $('#MainContent_FileManager1_FileViewCMDDelete').parent().prev('div').remove();

                              $('#MainContent_FileManager1_FileViewCMDCreate').parent().remove();
                              $('#MainContent_FileManager1_FileViewCMDCopy').parent().remove();
                              $('#MainContent_FileManager1_FileViewCMDMove').parent().remove();
                              $('#MainContent_FileManager1_FileViewCMDDelete').parent().remove();
                              $('#MainContent_FileManager1_FileViewCMDRename').parent().remove();

                              $('#MainContent_FileManager1_UploadBar').remove();
                              WFM_MainContent_FileManager1_Controller.OnFileViewChangeView(WFM_MainContent_FileManager1_FileView, 'Details'); return false;


                  });
</script>

<%}  else
   {%>
   <!--admin 및 user 권한-->
       <script type="text/javascript">
           $(document).ready(function () {


               WFM_MainContent_FileManager1_Controller.OnFileViewChangeView(WFM_MainContent_FileManager1_FileView, 'Details'); return false;



           });
</script>
       
       
   <%} %>

</asp:Content>
