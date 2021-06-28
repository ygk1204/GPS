<%@ Page Language="C#" MasterPageFile="~/Demo/Shared/GPS.Master" Title="Untitled Page" %>

<%@ Import Namespace="System.Reflection" %>
<%@ Register Assembly="IZ.WebFileManager" Namespace="IZ.WebFileManager" TagPrefix="iz" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

	<div>
		<iz:FileManager ID="FileManager1" runat="server" Height="400" Width="850">
			<RootDirectories>
				<iz:RootDirectory DirectoryPath="~/Files/My Documents/" Text="My Documents" />
			</RootDirectories>
		</iz:FileManager>
	</div>


<script type="text/javascript">
    $(document).ready(function () {

//        $('#MainContent_FileManager1_tb0').hide();
//        $('#MainContent_FileManager1_tb1').hide();
//        $('#MainContent_FileManager1_tb2').hide();
//        $('#MainContent_FileManager1_tb3').hide();
//        $('#MainContent_FileManager1_tb4').hide();


//        $('#MainContent_FileManager1_FileViewCMDCopy').parent().prev('div').hide();
//        $('#MainContent_FileManager1_FileViewCMDDelete').parent().prev('div').hide();

//        $('#MainContent_FileManager1_FileViewCMDCreate').parent().hide();
//        $('#MainContent_FileManager1_FileViewCMDCopy').parent().hide();
//        $('#MainContent_FileManager1_FileViewCMDMove').parent().hide();
//        $('#MainContent_FileManager1_FileViewCMDDelete').parent().hide();
//        $('#MainContent_FileManager1_FileViewCMDRename').parent().hide();

//        $('#MainContent_FileManager1_UploadBar').hide();


    });
</script>

</asp:Content>
