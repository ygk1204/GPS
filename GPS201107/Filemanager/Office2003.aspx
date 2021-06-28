<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" %>
<%@ Register Assembly="IZ.WebFileManager" Namespace="IZ.WebFileManager" TagPrefix="iz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div style="margin-top: 8px; margin-bottom: 8px;">
    Use ImagesFolder, ...ImageUrl and SpecialFolders properties to customize your icon theme
    </div>
    <div>
        <iz:FileManager ID="FileManager1" runat="server" SkinId="Office2003" Height="400px" Width="600">
        <RootDirectories>
        <iz:RootDirectory DirectoryPath="~/Files/My Documents" Text="My Documents" />
        </RootDirectories>
        </iz:FileManager>
    </div>
</asp:Content>
