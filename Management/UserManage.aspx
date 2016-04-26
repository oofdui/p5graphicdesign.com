<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManage.aspx.cs" Inherits="Management_UserManage" ValidateRequest="false"%>

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
				            <b>UserGroup</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:DropDownList ID="ddlUserGroup" runat="server" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            Photo
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:Label ID="lblPhoto" runat="server"></asp:Label>
                            <asp:FileUpload ID="fuPhoto" runat="server" />
                            <span class="fontComment">* Width=<%=photoWidth.ToString() %>Height=<%=photoHeight.ToString()%></span>
                            <asp:RequiredFieldValidator 
                                ID="vdPhoto" runat="server" Enabled="false" ControlToValidate="fuPhoto" CssClass="vldDefault" 
                                Display="Dynamic" ErrorMessage="กรุณากรอก" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalAttachment0" runat="server" 
                                ClientValidationFunction="UploadFileCheck" ControlToValidate="fuPhoto" 
                                CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="เลือกได้เฉพาะไฟล์ดังนี้ jpg jpeg gif png" SetFocusOnError="true" 
                                ValidationGroup="vgSubmit"></asp:CustomValidator>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>Username</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtUsername" runat="server" MaxLength="100" placeholder="ชื่อล็อคอิน"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtUsername" CssClass="vldDefault" Display="Dynamic" 
                                ErrorMessage="กรุณากรอก" ValidationGroup="vgSubmit" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            <b>Password</b>
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtPassword" runat="server" MaxLength="100" TextMode="Password" placeholder="รหัสผ่าน" Enabled="false"/>
                            <asp:RequiredFieldValidator ID="vdPassword" runat="server" 
                                ControlToValidate="txtPassword" CssClass="vldDefault" Display="Dynamic" 
                                ErrorMessage="กรุณากรอก" ValidationGroup="vgSubmit" Enabled="false" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            Name
			            </td>
			            <td style="text-align:left;padding-left:10px;">
                            <asp:DropDownList ID="ddlPName" runat="server">
                                <asp:ListItem>นาย</asp:ListItem>
                                <asp:ListItem>นาง</asp:ListItem>
                                <asp:ListItem>นางสาว</asp:ListItem>
                            </asp:DropDownList>
				            <asp:TextBox ID="txtFName" runat="server" MaxLength="100" placeholder="ชื่อ"/> 
                            <asp:TextBox ID="txtLName" runat="server" MaxLength="100" placeholder="นามสกุล"/>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            Birthdate
			            </td>
			            <td style="text-align:left;padding-left:10px;">
                            <uc1:ucDateTimeFlat runat="server" ID="ucBirthdate" EnableTimePicker="false"/>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            Gender
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:RadioButtonList ID="rbGender" runat="server" RepeatDirection="Horizontal" CssClass="rbDefault">
                                <asp:ListItem Value="M" Selected="True">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
				            </asp:RadioButtonList>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            ID Card Number
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtNID" runat="server" MaxLength="13" placeholder="เลขที่บัตรประจำตัวประชาชน"/>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            Phone
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtPhone" runat="server" MaxLength="100" placeholder="เบอร์โทรศัพท์"/>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            Mobile
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtMobile" runat="server" MaxLength="100" placeholder="เบอร์โทรศัพท์มือถือ"/>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            E-Mail
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" placeholder="อีเมล์"/>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            Address
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtAddress" runat="server" MaxLength="500" width="90%" placeholder="ที่อยู่"/>
                            <div style="padding-top:3px;">
                                <asp:TextBox ID="txtAddressDistrict" runat="server" MaxLength="100" placeholder="ตำบล"/>
                            </div>
                            <div style="padding-top:3px;">
                                <asp:TextBox ID="txtAddressPrefecture" runat="server" MaxLength="100" placeholder="อำเภอ"/>
                            </div>
                            <div style="padding-top:3px;">
                                <asp:TextBox ID="txtAddressProvince" runat="server" MaxLength="100" placeholder="จังหวัด"/>
                            </div>
                            <div style="padding-top:3px;">
                                <asp:TextBox ID="txtAddressPostal" runat="server" MaxLength="100" placeholder="รหัสไปรษณีย์"/>
                            </div>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            Profile
			            </td>
			            <td style="text-align:left;padding-left:10px;">
                            <uc1:ucTextEditorFull runat="server" ID="ucProfile" Row="4"/>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            Signature
			            </td>
			            <td style="text-align:left;padding-left:10px;">
                            <uc1:ucTextEditorFull runat="server" ID="ucSignature" Row="4"/>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            ลำดับ
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtSort" runat="server" MaxLength="3" Width="30px" CssClass="txtCenter" Enable="false">0</asp:TextBox>
                            <span class="font_comment">* ระบุตัวเลข</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ControlToValidate="txtSort" CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="กรุณากรอก" SetFocusOnError="True" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator2" runat="server" 
                                ControlToValidate="txtSort" CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="กรอกเฉพาะตัวเลข" MaximumValue="999" MinimumValue="0" 
                                SetFocusOnError="True" Type="Integer" ValidationGroup="vgSubmit"></asp:RangeValidator>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            แสดงผล
                        </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:CheckBox ID="cbActive" runat="server" Checked="True" Text="เปิด" Enable="false"/>
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