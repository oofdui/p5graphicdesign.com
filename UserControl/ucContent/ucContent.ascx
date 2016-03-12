<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucContent.ascx.cs" Inherits="ucContent" %>

<style type="text/css">
    .dvContent,.dvContentNormal
    {
        padding:10px;
        position:relative;
    }
    .dvContent:hover
    {
        padding:9px;
        border:1px solid #DDD;
        box-shadow:3px 3px 5px #A3A3A3;
    }
    .dvContent:hover .dvContentMenu
    {
        visibility:visible;
    }
    .dvContentMenu
    {
        border-left:1px solid #DDD;border-bottom:1px solid #DDD;
        background-color:#FAFAFA;
        position:absolute;top:0;right:0;
        padding:5px;
        visibility:hidden;
    }
</style>

<script type="text/javascript">
    if (typeof jQuery == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>
<script type="text/javascript">
    if (typeof colorbox == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/ColorBox/jquery.colorbox.js") %>'><" + "/script>");
        document.write("<link rel='stylesheet' type='text/css' href='<%=this.ResolveClientUrl("~/Plugin/ColorBox/colorbox.css") %>'" + "/>");
    }
</script>

<asp:Label ID="lblContent" runat="server" />

<script type="text/javascript">
    $(document).ready(function () {
        $(".cbIFrame").colorbox({
            fixed:true,
            iframe: true,
            rel: 'Default',
            width: "<%=ModalWidth %>", height: "<%=ModalHeight %>",
            rel: "nofollow"
        });
        $(".cbIFrameRefreshOnClose").colorbox({
            fixed: true,
            iframe: true,
            rel: 'Default',
            width: "<%=ModalWidth %>", height: "<%=ModalHeight %>",
            rel: "nofollow",
            onClosed: function () { location.reload(true); }
        });
    });
</script>