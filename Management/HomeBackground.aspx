<%@ Page Title="" Language="C#" MasterPageFile="~/Management/MasterPage.master" AutoEventWireup="true" CodeFile="HomeBackground.aspx.cs" Inherits="Management_HomeBackground" %>

<%@ Register src="/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="/UserControl/ucGridView/ucGridVIewDataTables.ascx" tagname="ucGridVIewDataTables" tagprefix="uc2" %>
<%@ Register src="/UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <link href="/Management/CSS/cssDefault.css" rel="stylesheet" type="text/css" />
    <link href="/Management/CSS/cssControl.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyHead" Runat="Server">
    <div style="float:left;margin-right:5px;">
        <img src="Images/icPhotoGallery.png" alt="HomeBackground Manage" title="HomeBackground Manage" />
    </div>
    <div style="float:left;text-align:left;">
        <h1>HomeBackground Manage</h1>
        ระบบจัดการภาพพื้นหลังหน้าหลัก | <a href="/Management/">กลับสู่หน้าหลัก</a>
    </div>
    <div style="clear:both;"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="Child" ColorBoxIframeName="cbIFrame" Height="95%" Width="800px"/>
    <asp:Label ID="lblSQL" runat="server" />

    <div class="GridView" style="padding:10px;">
        <table style="padding:0;margin:0;width:100%;">
            <!--Start Loop-->
            <tr class="GridViewItemNormal">
                <td style="padding:10px;">
				    <b>ภาพปัจจุบัน</b>
			    </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td style="padding:10px;">
				    <asp:Label ID="lblPhoto" runat="server" />
			    </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td style="padding:10px;">
				    <b>อัพโหลดภาพใหม่</b>
			    </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td style="padding:10px;">
				    <asp:FileUpload ID="fuPhoto" runat="server" />
			    </td>
		    </tr>
            <tr class="GridViewItemNormal">
			    <td style="padding:10px;text-align:center;">
				    <asp:Button ID="btSubmit" runat="server" 
                        Text="Submit" OnClick="btSubmit_Click"/>
			    </td>
		    </tr>
            <!--End Loop-->
        </table>
        <div class="GridViewFooter">
            -
        </div>
    </div>
</asp:Content>