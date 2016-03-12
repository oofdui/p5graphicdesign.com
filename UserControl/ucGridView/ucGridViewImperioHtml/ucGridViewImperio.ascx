<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucGridViewImperio.ascx.cs"
    Inherits="ucGridViewImperio" %>

<link href="<%=this.ResolveClientUrl("/Plugin/BootStrap/css/imperio.css") %>" rel="stylesheet" type="text/css" />
<!--<link href="Plugin/BootStrap/css/imperio.css" rel="stylesheet" type="text/css" />-->
<style type="text/css">
    .<%=Class%> table
    {
        font-family:Tahoma;
        font-size:10pt;
        border-collapse:collapse;
        width:100%;
        font-weight:normal;
        border-left: 1px solid <%=BorderColor%>;
    }
    .box-title 
    {
        cursor:pointer;
        text-align:center;
        padding: 3px;
        border: 1px solid <%=BorderColor%>;
        color:<%=HeaderFontColor%>;
        background-color: <%=BackgroundColorDark%>;
        background-image: -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorLight%>), to(<%=BackgroundColorDark%>));
        background-image: -webkit-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -moz-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -ms-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -o-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        -moz-border-radius:8px 8px 0 0;
        -webkit-border-radius: 8px 8px 0 0;
        border-radius: 8px 8px 0 0;
        <%=HeaderStyle%>
    }
        
    .dataTables_length{
        background-color: <%=BackgroundColorDark%>;
        background-image: -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorLight%>), to(<%=BackgroundColorDark%>));
        background-image: -webkit-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -moz-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -ms-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -o-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        border-left:1px solid <%=BorderColor%>;
        border-right:1px solid <%=BorderColor%>;
        border-bottom:1px solid <%=BorderColor%>;
        padding: 8px;
    }
    .<%=HeaderClass%>
    {
        border:1px solid <%=BorderColor%>;
        padding:3px;
        color:<%=HeaderFontColor%>;
        text-align:center;
        cursor:pointer;
        background-color: <%=BackgroundColorDark%>;
        background-image: -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorLight%>), to(<%=BackgroundColorDark%>));
        background-image: -webkit-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -moz-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -ms-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -o-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        -moz-border-radius:8px 8px 0 0;
        -webkit-border-radius: 8px 8px 0 0;
        border-radius: 8px 8px 0 0;
        text-shadow: 0 1px 0 rgba(0, 0 ,0, .2);
        <%=HeaderStyle%>
    }
    .<%=HeaderClass%>:hover
    {
        background-color: <%=BackgroundColorLight%>;
        background-image: -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorDark%>), to(<%=BackgroundColorLight%>));
        background-image: -webkit-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -moz-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -ms-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -o-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
    }
    .<%=SubHeaderClass%> th
    {
        cursor:pointer;
        border-right:1px solid <%=BorderColor%>;
        border-bottom:1px solid <%=BorderColor%>;
        text-align:center;
        padding:3px;
        color:<%=SubHeaderFontColor%>;
        background-color: <%=BackgroundColorDark%>;
        background-image: -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorLight%>), to(<%=BackgroundColorDark%>));
        background-image: -webkit-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -moz-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -ms-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -o-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        <%=SubHeaderStyle%>
    }
    .<%=SubHeaderClass%> th:hover
    {
        background-color: <%=BackgroundColorLight%>;
        background-image: -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorDark%>), to(<%=BackgroundColorLight%>));
        background-image: -webkit-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -moz-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -ms-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -o-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
    }
    .<%=SubHeaderClass%> .<%=ArrowClass%>
    {
        margin-left:5px;
        margin-top:6px;
        position:absolute;
	    overflow:hidden;
	    border-top:6px solid #777; /* ลูกศร */
	    border-left:4px dashed transparent;
	    border-right:4px dashed transparent;
    }
    .<%=SubHeaderClass%> th:hover .<%=ArrowClass%>
    {
        overflow:hidden;
	    border-top:6px solid #FFF; /* ลูกศร */
	    border-left:4px dashed transparent;
	    border-right:4px dashed transparent;
    }
    .<%=ItemClass%>
    {
        font-family:Tahoma;
        font-size:10pt;
        color:<%=ItemFontColor%>;
        <%=ItemStyle%>
    }
    .<%=ItemClass%> td
    {
        border-right:1px solid <%=BorderColor%>;
        border-bottom:1px solid <%=BorderColor%>;
        text-align:center;
        padding:3px;
        background-color: #FFF;
    }
    .<%=ItemClass%>:hover td
    {
        background-color: <%=HoverColor%>;
    }
    .<%=ItemNormalClass%> td
    {
        border-right:1px solid <%=BorderColor%>;
        border-bottom:1px solid <%=BorderColor%>;
        text-align:center;
        padding:3px;
        background-color: #FFF;
    }
    .<%=FooterClass%>,.dataTables_paginate
    {
        border:1px solid <%=BorderColor%>;
        border-top:0px;
        padding:5px;
        color:<%=FooterFontColor%>;
        cursor:pointer;
        background-color: <%=BackgroundColorDark%>;
        background-image: -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorLight%>), to(<%=BackgroundColorDark%>));
        background-image: -webkit-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -moz-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -ms-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: -o-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background-image: linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        -moz-border-radius: 0 0 8px 8px;
        -webkit-border-radius:  0 0 8px 8px;
        border-radius:  0 0 8px 8px;
        text-shadow: 0 1px 0 rgba(0, 0 ,0, .2);
        <%=FooterStyle%>
    }
    .<%=FooterClass%>:hover
    {
        background-color: <%=BackgroundColorLight%>;
        background-image: -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorDark%>), to(<%=BackgroundColorLight%>));
        background-image: -webkit-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -moz-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -ms-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -o-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
    }
</style>

<script type="text/javascript">
    if (typeof jQuery == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>
<script type="text/javascript">
    if (typeof bootstrap == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("Plugin/Imperio/js/bootstrap.min.js") %>'><" + "/script>");
    }
</script>
<script type="text/javascript" src="<%=this.ResolveClientUrl("Plugin/Imperio/js/jquery.dataTables.min.js") %>"></script>
<script type="text/javascript" src="<%=this.ResolveClientUrl("Plugin/Imperio/js/imperio.tables.js") %>"></script>