<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLanguageDB.ascx.cs" Inherits="ucLanguageDB" %>

<link href="<%=this.ResolveClientUrl("CSS/msdropdown/dd.css") %>" rel="stylesheet" type="text/css" />

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

<asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" 
    onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
</asp:DropDownList>
<asp:Label ID="lblAlert" runat="server" Font-Size="10pt" ForeColor="Red" />
<script type="text/javascript">
    $(document).ready(function (e) {
        $("#<%=ddlLanguage.ClientID %>").msDropdown({
            mainCSS: "dd",
            visibleRows: 7,
            animStyle: 'slideDown',
            enableCheckbox: false,
            enableAutoFilter:true,
            roundedBorder: true 
        });
    });
</script>