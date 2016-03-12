<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTextEditorAjax.ascx.cs" Inherits="ucTextEditorAjax" %>
<!--
ถ้าใช้ร่วมกับ CSS ตัวอื่่น ต้องเอาตัวนี้ไว้ด้านบน Toolbar Panel จะได้ไม่เพี้ยน
-->
<link href='<%=this.ResolveClientUrl("~/CSS/cssControl.css") %>' rel="stylesheet" type="text/css" />
<link href='<%=this.ResolveClientUrl("~/CSS/cssDefault.css") %>' rel="stylesheet" type="text/css" />
<style type="text/css">
    .mceToolbar td,.mceEditor td
    {
        background-color:#F0F0EE;
    }
</style>

<script type="text/javascript">
    if (typeof jQuery == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>
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
<!-- TinyMCE -->
<script type="text/javascript" src='<%=this.ResolveClientUrl("TinyMCE/jscripts/tiny_mce/tiny_mce.js") %>'></script>
<script type="text/javascript">
    function InsertTinyMCE(param) {
        tinymce.activeEditor.execCommand('mceInsertContent', false, param);
    }
    tinyMCE.init({
        // General options
        mode: "specific_textareas",
        editor_selector: "mceEditor",
        theme: "advanced",
        plugins: "safari,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,inlinepopups",

        // Theme options
        theme_advanced_buttons1: "bold,italic,underline,|,justifyleft,justifycenter,justifyright,justifyfull,|,formatselect,fontselect,fontsizeselect,forecolor,backcolor",
        theme_advanced_buttons2: "emotions,|,bullist,numlist,|,outdent,indent,blockquote,hr,|,undo,redo,|,link,unlink,image,media,|,code,preview,fullscreen",
        theme_advanced_buttons3: "",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: false,

        // Font Size
        font_size_style_values: "x-small (8pt),small (9pt),medium (10pt),large (12pt),x-large (14pt),xx-large (16pt)",
        theme_advanced_font_sizes: "x-small (8pt)=8pt,small (9pt)=9pt,medium (10pt)=10pt,large (12pt)=12pt,x-large (14pt)=14pt,xx-large (16pt)=16pt",

        // Example word content CSS (should be your site CSS) this one removes paragraph margins
        content_css: '<%=this.ResolveClientUrl("CSS/cssDefault.css") %>',

        // Drop lists for link/image/media/template dialogs
        template_external_list_url: "lists/template_list.js",
        external_link_list_url: "lists/link_list.js",
        external_image_list_url: "lists/image_list.js",
        media_external_list_url: "lists/media_list.js",

        // ไม่ใช้แท็ก <p> ในการขึ้นบรรทัดใหม่
        forced_root_block: false,
        force_br_newlines: true,
        force_p_newlines: false
    });
</script>
<!-- /TinyMCE -->
<style type="text/css">
    .vldDefault
    {
        font-size:10pt;
        color:#FF0000;
    }
    .fuDefault
    {
        font-size:10pt;
    }
</style>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="margin-bottom:5px;">
            <span class="Icon24 Photo Normal"></span>
            <asp:FileUpload ID="fuUpload" runat="server" CssClass="fuDefault" onchange="clickTheButton();"/>
            <asp:CustomValidator ID="cvalAttachment" runat="server" 
                ClientValidationFunction="UploadFileCheck" ControlToValidate="fuUpload" 
                CssClass="vldDefault" Display="Dynamic" ValidationGroup="vgUpload" 
                ErrorMessage="เลือกได้เฉพาะไฟล์ดังนี้ jpg jpeg gif png" SetFocusOnError="true"></asp:CustomValidator>
            <asp:Label ID="lblUpload" runat="server"></asp:Label>
            <div>
                <asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine" 
                    CssClass="mceEditor"></asp:TextBox>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    Please wait image is getting uploaded....
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btUpload" />
    </Triggers>
</asp:UpdatePanel>

<asp:Button ID="btUpload" runat="server" Text=" " CssClass="Button UploadTH" ValidationGroup="vgUpload"
    onclick="btUpload_Click" />

<script type="text/javascript">
    function clickTheButton() {
        document.getElementById('<%= btUpload.ClientID %>').click();
    }
</script>