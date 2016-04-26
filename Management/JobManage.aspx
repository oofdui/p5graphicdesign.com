<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobManage.aspx.cs" Inherits="Management_JobManage" %>

<%@ Register src="../UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="../UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc2" %>
<%@ Register Src="~/UserControl/ucDateTime/ucDateTimeFlat.ascx" TagPrefix="uc1" TagName="ucDateTimeFlat" %>
<%@ Register Src="~/UserControl/ucTextEditor/ucTextEditorFull.ascx" TagPrefix="uc1" TagName="ucTextEditorFull" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage</title>
    <link href="/Management/CSS/cssDefault.css" rel="stylesheet" type="text/css" />
    <link href="/Management/CSS/cssControl.css" rel="stylesheet" type="text/css" />
    <link href="/Management/CSS/cssCustom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body{
            height:100%;
            overflow:scroll;
        }
        .rbDefault td{border:0 none;text-align:left;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:ucColorBox ID="ucColorBox1" runat="server" />
        <uc2:ucGridViewTemplate ID="ucGridViewTemplate1" runat="server"/>

        <asp:Panel ID="pnDetail" runat="server">
            <div class="GridView" style="padding:10px;">
                <div class="GridViewHeader">
                    <h2>แก้ไขข้อมูล</h2>
                </div>
                <table cellpadding="0" cellspacing="0">
                    <tr class="GridViewSubHeader">
                        <td style="width:150px;">
                            ชื่อ<span class="Arrow" />
                        </td>
                        <td>
                            ข้อมูล<span class="Arrow" />
                        </td>
                    </tr>
                    <!--Start Loop-->
                    <tr class="GridViewItemNormal">
                        <td>
				            File
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:Label ID="lblFileName" runat="server" />
                            <asp:HiddenField ID="hidFileName" runat="server" />
                            <asp:Button ID="btDelete" runat="server" Text="ลบไฟล์" OnClick="btDelete_Click"/>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>Name</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:Label ID="lblName" runat="server" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>Detail</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:Label ID="lblDetail" runat="server" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>ผู้ติดต่อ</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:Label ID="lblContactName" runat="server" /> | 
                            <asp:Label ID="lblContactPhone" runat="server" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>วันที่พรูฟ</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <uc1:ucDateTimeFlat ID="ucDateApprove" runat="server" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>วันที่ส่งงาน</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <uc1:ucDateTimeFlat ID="ucDateSubmit" runat="server" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>วันที่ติดตั้ง</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <uc1:ucDateTimeFlat ID="ucDateInstall" runat="server" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>วันที่รื้อถอน</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <uc1:ucDateTimeFlat ID="ucDateUninstall" runat="server" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>สถานที่</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:Label ID="lblLocation" runat="server" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>ผู้ผลิต</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtProducerName" runat="server" MaxLength="200" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>ผู้เช็คงาน</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtVerifyName" runat="server" MaxLength="200" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>ผู้ติดตั้ง</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtInstallName" runat="server" MaxLength="200" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            &nbsp;
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:Button ID="btSubmit" runat="server" 
                                ValidationGroup="vgSubmit" onclick="btSubmit_Click" Text="Submit"/>
                            <asp:Button ID="btCancel" runat="server" Text="Cencel" CausesValidation="False" 
                                onclick="btCancel_Click" />
			                <asp:Label ID="lblSQL" runat="server"></asp:Label>
			            </td>
		            </tr>
                    <!--End Loop-->
                </table>
                <div class="GridViewFooter">
                    -
                </div>
            </div>
        </asp:Panel>
    </form>

    <!-- FileUpload Checker -->
    <script type="text/javascript">
        function UploadFileCheck(source, arguments) {
            var sFile = arguments.Value;
            arguments.IsValid =
                ((sFile.match(/\.jpe?g$/i)) ||
                (sFile.match(/\.jpg$/i)) ||
                (sFile.match(/\.gif$/i)) ||
                (sFile.match(/\.png$/i)));
        }
    </script>
</body>
</html>