﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContentManage.aspx.cs" Inherits="Management_ContentManage" ValidateRequest="false"%>
<%@ Register src="/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="/UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc2" %>
<%@ Register src="/UserControl/ucTextEditor/ucTextEditorFull.ascx" tagname="ucTextEditorFull" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage</title>
    <link href="/Management/CSS/cssDefault.css" rel="stylesheet" type="text/css" />
    <link href="/Management/CSS/cssControl.css" rel="stylesheet" type="text/css" />
    <link href="/Plugin/TabbedContent/tabcontent.css" rel="stylesheet" />
    <script src="/Plugin/TabbedContent/tabcontent.js" type="text/javascript"></script>
</head>
<body style="background-color:#FFF;">
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
				            ชื่อ
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtName" runat="server" MaxLength="100" placeholder="ชื่ออ้างอิง"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtName" CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="กรุณากรอก" ValidationGroup="vgSubmit" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            คำอธิบาย</td>
                        <td style="text-align:left;padding-left:10px;">
                            <asp:TextBox ID="txtDetail" runat="server" MaxLength="500" width="99%" placeholder="คำอธิบาย"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            รายละเอียด</td>
                        <td style="text-align:left;padding-left:10px;">
                            <ul class="tabs">
                                <li><a href="#th">ภาษาไทย</a></li>
                                <li><a href="#en">English</a></li>
                            </ul>
                            <div class="tabcontents">
                                <div id="th">
                                    <uc3:ucTextEditorFull ID="ucContent" runat="server" Width="100%"/>
                                </div>
                                <div id="en">
                                    <uc3:ucTextEditorFull ID="ucContentEN" runat="server" Width="100%"/>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            ลำดับ
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtSort" runat="server" MaxLength="3" Width="30px" CssClass="txtCenter" placeholder="ลำดับ">0</asp:TextBox>
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
                                ValidationGroup="vgSubmit" onclick="btSubmit_Click" 
                                Text="Submit"/>
                            <asp:Button ID="btCancel" runat="server" 
                                Text="Cencel" CausesValidation="False" 
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
</body>
</html>