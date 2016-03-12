<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLogon.ascx.cs" Inherits="UserControl_ucLogon_ucLogon" %>
<%@ Register Src="~/UserControl/ucColorBox/ucColorBox.ascx" TagPrefix="uc1" TagName="ucColorBox" %>

<uc1:ucColorBox runat="server" ID="ucColorBox" UID="cbLogon"/>
<link href="<%=ResolveClientUrl("CSS/cssControl.css")%>" rel="stylesheet" type="text/css" />
<style type="text/css">
    .dvLogon
    {
        text-align:left;
    }
    .lblLogon
    {
        font-size:10pt;
        font-weight:bold;
        padding:5px 0px 5px 0px;
    }
    .txtLogon
    {
        width:93%;
        padding:5px;
        border:1px solid #DDD;
        background-color:#FFF;
        color:#565656;
    }
    .fontValid
    {
        font-family:Tahoma;
        font-weight:normal;
        font-size:10pt;
        color:#FE4242;
    }
</style>
<div id="dvUCLogon" style="padding:5px;text-align:center;width:<%=Width%>;">
    <asp:Panel ID="pnLogin" runat="server" Visible="false" DefaultButton="btLogin">
        <div class="dvLogon lblLogon">
            Username
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                Display="Dynamic" ErrorMessage=" โปรดกรอก" 
                ControlToValidate="txtUsername" CssClass="fontValid" ValidationGroup="vdLogon"></asp:RequiredFieldValidator>
        </div>
        <div class="dvLogon">
            <asp:TextBox ID="txtUsername" runat="server" CssClass="txtLogon" TabIndex="1" placeholder="Username"/>
        </div>
        <div class="dvLogon lblLogon">
            Password
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtPassword" CssClass="fontValid" Display="Dynamic" 
                ErrorMessage=" โปรดกรอก" ValidationGroup="vdLogon"></asp:RequiredFieldValidator>
        </div>
        <div class="dvLogon">
            <asp:TextBox ID="txtPassword" runat="server" CssClass="txtLogon" 
                TextMode="Password" TabIndex="2" placeholder="Password"/>
        </div>
        <div class="dvLogon" style="padding:5px 0px 5px 0px;text-align:right;">
            <asp:CheckBox ID="cbEnableCookie" runat="server" Text="Remember" 
                TextAlign="Left" TabIndex="3" ToolTip="จดจำข้อมูลการล็อคอินในครั้งต่อไป" />
            <asp:Button ID="btLogin" runat="server" CssClass="Button LoginTH" 
                TabIndex="4" onclick="btLogin_Click" ValidationGroup="vdLogon" />
            <asp:Label ID="lblLogin" runat="server" />
        </div>
        <%--<div style="text-align:right;border-top:1px dashed #ddd;padding-top:5px;margin-top:5px;">
            <a href='<%=UrlRegister %>'>
                <asp:Label ID="lblRegister" runat="server" Text="สมัครสมาชิก" />
            </a>
        </div>--%>
    </asp:Panel>
    <asp:Panel ID="pnLogout" runat="server" Visible="false">
        <div title="Username" style="font-size:11pt;font-weight:bold;">
            <a href='<%=UrlProfile %>' title='group : <%=clsSecurity.LoginGroup %>'>
                <div class="Icon32 User Normal" style="margin-right:0px;"></div>
                <asp:Label ID="lblUsername" runat="server" />
            </a>
        </div>
        <asp:Label ID="lblGroupName" runat="server" />
        <asp:Label ID="lblAuthority" runat="server" />
        <div style="padding-top:5px;">
            <asp:Button ID="btLogout" runat="server" CssClass="Button LogoutTH" 
                CausesValidation="False" onclick="btLogout_Click" />
        </div>
    </asp:Panel>
</div>