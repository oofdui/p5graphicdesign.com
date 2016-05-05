<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLanguageDB.ascx.cs" Inherits="ucLanguageDB" %>

<link href="<%=this.ResolveClientUrl("CSS/msdropdown/dd.css") %>" rel="stylesheet" type="text/css" />
<style type="text/css">
    #lang1{display:block;}
    #lang2{display:none;}
    @media screen and (max-width: 736px) 
    {
        #lang1{display:none;}
        #lang2{display:block;}
    }
</style>
<script type = "text/javascript">
    if (typeof jQuery == 'undefined') {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>
<script type = "text/javascript">
    if (typeof dd == 'undefined') {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("Plugin/msdropdown/jquery.dd.js") %>'><" + "/script>");
    }
</script>

<div id="lang1">
    <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
        <asp:ListItem Value="th-TH">ภาษาไทย</asp:ListItem>
        <asp:ListItem Value="en-US">English</asp:ListItem>
    </asp:DropDownList>
</div>
<div id="lang2">
    <asp:DropDownList ID="ddlLanguage2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLanguage2_SelectedIndexChanged">
        <asp:ListItem Value="th-TH">ไทย</asp:ListItem>
        <asp:ListItem Value="en-US">Eng</asp:ListItem>
    </asp:DropDownList>
</div>
<asp:Label ID="lblAlert" runat="server" Font-Size="10pt" ForeColor="Red" />
<script type="text/javascript">
    <%--$(document).ready(function (e) {
        $("#<%=ddlLanguage.ClientID %>").msDropdown({
            mainCSS: "dd",
            visibleRows: 7,
            animStyle: 'slideDown',
            enableCheckbox: false,
            enableAutoFilter:true,
            roundedBorder: true 
        });
    });--%>
</script>