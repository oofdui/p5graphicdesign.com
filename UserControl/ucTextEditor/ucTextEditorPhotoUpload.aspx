<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ucTextEditorPhotoUpload.aspx.cs" Inherits="ucTextEditorPhotoUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="<%=ResolveClientUrl("~/CSS/cssDefault.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveClientUrl("~/CSS/cssControl.css")%>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            background-color:#ffffff;
        }
        .fontWarn
        {
            color:red;
        }
        .hidDefault
        {
            visibility:hidden;
        }
    </style>

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
    <!-- /FileUpload Checker -->
</head>
<body>
    <form id="form1" runat="server">
        <div style="vertical-align:middle;text-align:left;">
            <div class="Icon32 Photo Normal" title="แทรกภาพ" style="cursor:pointer;"></div>
            <asp:FileUpload ID="fuInsertImages" runat="server" onchange="clickTheButton();"/>
            <asp:Button ID="btInsertImages" runat="server" onclick="btInsertImages_Click" 
                Text="Upload" ValidationGroup="InsertImages" Width="60px" CssClass="hidDefault"/>
            <span style="font-size:8pt;color:#E99E27;padding-left:5px;">
                * MaxWidth: <%=maxWidth %> px , MaxHeight: <%=maxHeight %> px , MaxSize: <%=maxSize.ToString("#,#")%> kb
            </span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="fuInsertImages" CssClass="validDefault" Display="Dynamic" 
                ErrorMessage=" กรุณาเลือกไฟล์ภาพ" ValidationGroup="InsertImages"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cvalAttachment" runat="server" 
                ClientValidationFunction="UploadFileCheck" ControlToValidate="fuInsertImages" 
                CssClass="validDefault" Display="Dynamic" 
                ErrorMessage="เลือกได้เฉพาะไฟล์ดังนี้ jpg jpeg gif png" SetFocusOnError="true" 
                ValidationGroup="InsertImages"></asp:CustomValidator>
            <asp:Label ID="lblInsertImages" runat="server"></asp:Label>
        </div>
    </form>

    <script type="text/javascript">
        function clickTheButton() {
            document.getElementById('<%= btInsertImages.ClientID %>').click();
        }
    </script>
</body>
</html>
