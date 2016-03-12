<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTextEditor.ascx.cs" Inherits="UserControl_ucTextEditor"%>

<link href='<%=this.ResolveClientUrl("~/CSS/cssControl.css") %>' rel="stylesheet" type="text/css" />
<link href='<%=this.ResolveClientUrl("~/CSS/cssDefault.css") %>' rel="stylesheet" type="text/css" />
<style type="text/css">
    .mceToolbar td,.mceEditor td
    {
        background-color:#F0F0EE;
    }
</style>

<!-- TinyMCE -->
<script type="text/javascript" src='<%=ResolveClientUrl("TinyMCE/jscripts/tiny_mce/tiny_mce.js")%>'></script>
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

<asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine" 
    CssClass="mceEditor"></asp:TextBox>
