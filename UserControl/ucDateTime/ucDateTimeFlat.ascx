<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDateTimeFlat.ascx.cs" Inherits="UserControl_ucDateTime_ucDateTimeFlat" %>

<link href="<%=this.ResolveClientUrl("Plugin/jquery.datetimepicker.css") %>" rel="stylesheet" type="text/css" />

<style type="text/css">
    .ucDateTime
    {
        background-image:url('<%=this.ResolveClientUrl("Images/icCalendar.png")%>');
        background-repeat: no-repeat;
        background-position:left center;
        background-color:#FFF;
        /*border:1px solid #CCC;
        padding:5px;*/
        padding-left:22px !important;
        width:<%=Width.ToString()%>px;
    }
</style>

<asp:TextBox ID="txtDateTime" runat="server" CssClass="ucDateTime"/>
<asp:RequiredFieldValidator
    ID="vldDateTime" runat="server"     ErrorMessage="โปรดกรอก"     Display="Dynamic"     CssClass="vldDefault"     ControlToValidate="txtDateTime"     Enabled="false"></asp:RequiredFieldValidator>
<script type = "text/javascript">
    if (typeof jQuery == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>
<script type = "text/javascript">
    if (typeof jQuery.datetimepicker == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("Plugin/jquery.datetimepicker.js") %>'><" + "/script>");
    }
</script>
<script type="text/javascript">
    $('#<%=txtDateTime.ClientID %>').datetimepicker({ 
        step: <%=Step %>,
        lang: '<%=Lang %>',
        format: '<%=Format %>',
        formatDate:'Y-m-d',
        minDate:'<%=MinDate %>',
        maxDate: '<%=MaxDate %>',
        datepicker: <%=EnableDatePicker.ToString().ToLower() %>,
        timepicker:<%=EnableTimePicker.ToString().ToLower() %>,
        mask:<%=EnableMask.ToString().ToLower() %>,
        inline:<%=EnableInline.ToString().ToLower() %>,
		allowBlank:true
    });
</script>