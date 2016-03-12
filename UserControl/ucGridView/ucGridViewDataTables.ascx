<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucGridViewDataTables.ascx.cs" Inherits="ucGridViewDataTables" %>

<link href="<%=this.ResolveClientUrl("Plugin/DataTables/css/imperio.css")%>" rel="stylesheet" type="text/css" />

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
    .<%=Class%> table tr:hover .<%=ItemClass%>
    {
        background-color: <%=HoverColor%>;
    }
    .dataTables_length /*Search & PageSize Panel*/
    {
        text-align:left;
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
    .dataTables_length:hover
    {
        background-color: ;
        background-image: -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorDark%>), to(<%=BackgroundColorLight%>));
        background-image: -webkit-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -moz-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -ms-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -o-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
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
    .<%=SubHeaderClass%>
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
    .<%=SubHeaderClass%>:hover
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
    .<%=SubHeaderClass%>:hover .<%=ArrowClass%>
    {
        overflow:hidden;
	    border-top:6px solid #FFF; /* ลูกศร */
	    border-left:4px dashed transparent;
	    border-right:4px dashed transparent;
    }
    .<%=ItemClass%>
    {
        color:<%=ItemFontColor%>;
        border-right:1px solid <%=BorderColor%>;
        border-bottom:1px solid <%=BorderColor%>;
        text-align:center;
        padding:3px;
        background-color: #FFF;
        <%=ItemStyle%>
    }
    .<%=ItemNormalClass%>
    {
        color:<%=ItemFontColor%>;
        border-right:1px solid <%=BorderColor%>;
        border-bottom:1px solid <%=BorderColor%>;
        text-align:center;
        padding:3px;
        background-color: #FFF;
        <%=ItemStyle%>
    }
    .<%=FooterClass%>,.dataTables_paginate /* Pager & Summary */
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
    .<%=FooterClass%>:hover,.dataTables_paginate:hover
    {
        background-color: <%=BackgroundColorLight%>;
        background-image: -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorDark%>), to(<%=BackgroundColorLight%>));
        background-image: -webkit-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -moz-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -ms-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: -o-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background-image: linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
    }
    
    .<%=SubHeaderClass%>.sorting_asc 
    {
        background: <%=BackgroundColorLight%>;
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc.png") %>') no-repeat right center , -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorDark%>), to(<%=BackgroundColorLight%>));
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc.png") %>') no-repeat right center , -webkit-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc.png") %>') no-repeat right center , -moz-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc.png") %>') no-repeat right center , -ms-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc.png") %>') no-repeat right center , -o-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc.png") %>') no-repeat right center , linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
    }
    .<%=SubHeaderClass%>.sorting_asc:hover
    {
        background: <%=BackgroundColorDark%>;
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat right center , -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorLight%>), to(<%=BackgroundColorDark%>));
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat right center , -webkit-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat right center , -moz-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat right center , -ms-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat right center , -o-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat right center , linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
    }

    .<%=SubHeaderClass%>.sorting_desc 
    {
        background: <%=BackgroundColorLight%>;
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat right center , -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorDark%>), to(<%=BackgroundColorLight%>));
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat right center , -webkit-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat right center , -moz-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat right center , -ms-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat right center , -o-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat right center , linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
    }
    
    .<%=SubHeaderClass%>.sorting_desc:hover
    {
        background: <%=BackgroundColorDark%>;
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc.png") %>') no-repeat right center , -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorLight%>), to(<%=BackgroundColorDark%>));
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc.png") %>') no-repeat right center , -webkit-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc.png") %>') no-repeat right center , -moz-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc.png") %>') no-repeat right center , -ms-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc.png") %>') no-repeat right center , -o-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc.png") %>') no-repeat right center , linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
    }

    .<%=SubHeaderClass%>.sorting 
    {
        background: <%=BackgroundColorDark%>;
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat right center , -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorLight%>), to(<%=BackgroundColorDark%>));
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat right center , -webkit-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat right center , -moz-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat right center , -ms-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat right center , -o-linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat right center , linear-gradient(top, <%=BackgroundColorLight%>, <%=BackgroundColorDark%>);
	}
	.<%=SubHeaderClass%>.sorting:hover
    {
        background: <%=BackgroundColorLight%>;
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat right center , -webkit-gradient(linear, left top, left bottom, from(<%=BackgroundColorDark%>), to(<%=BackgroundColorLight%>));
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat right center , -webkit-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat right center , -moz-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat right center , -ms-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat right center , -o-linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
        background: url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat right center , linear-gradient(top, <%=BackgroundColorDark%>, <%=BackgroundColorLight%>);
	}
    /*
    .sorting_asc {
	    background: <%=BackgroundColorDark%> url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat center right;
    }
    .sorting_desc {
	    background: <%=BackgroundColorDark%> url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc.png") %>') no-repeat center right;
    }
    .sorting {
	    background: <%=BackgroundColorDark%> url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_both.png") %>') no-repeat center right;
    }
    .sorting_asc_disabled {
	    background: <%=BackgroundColorDark%> url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_asc_disabled.png") %>') no-repeat center right;
    }
    .sorting_desc_disabled {
	    background: <%=BackgroundColorDark%> url('<%=this.ResolveClientUrl("Plugin/DataTables/images/sort_desc_disabled.png") %>') no-repeat center right;
    }
    */
</style>

<script type="text/javascript">
    if (typeof jQuery == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>
<script type="text/javascript">
    if (typeof datatables == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("Plugin/DataTables/js/jquery.dataTables.min.js") %>'><" + "/script>");
        //document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("Plugin/DataTables/js/jquery.dataTables10.min.js") %>'><" + "/script>");
    }
</script>

<asp:Label ID="lblDefault" runat="server" />

<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=(gv!=null?gv.ClientID:"") %>').dataTable({
            "sPaginationType": "full_numbers",
            "iDisplayLength":<%=PageSize %>,
            "aLengthMenu":[<%=PageSizeList %>],
            "oSearch":{"sSearch":"<%=Search %>"},
            "bStateSave":<%=StateSave.ToString().ToLower() %>,
            "iCookieDuration":<%=StateSaveSecond.ToString() %>
        });
    });
</script>