<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ucTextEditorUpload.aspx.cs" Inherits="UserControl_ucTextEditorUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Text Editor FileUpload</title>
    
    <link href="<%=ResolveClientUrl("~/CSS/cssDefault.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveClientUrl("~/CSS/cssControl.css")%>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            background-color:#ffffff;
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
        <div style="width:666px;height:50px;vertical-align:middle;text-align:left;">
            <div class="Icon32 Photo Normal"></div>
            <asp:FileUpload ID="fuInsertImages" runat="server"/>
            <asp:Button ID="btInsertImages" runat="server" onclick="btInsertImages_Click" 
                Text="Upload" ValidationGroup="InsertImages" Width="60px" />
            <span style="font-size:8pt;color:#E99E27;padding-left:5px;">
                (max width: <%=maxWidth %> px , max size: <%=maxSize %> kb)
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
</body>
</html>
