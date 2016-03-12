<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucColorBox.ascx.cs" Inherits="ucColorBox" %>

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
    }
</script>

<link href="<%=this.ResolveClientUrl("~/CSS/cssDefault.css") %>" rel="stylesheet" type="text/css" />
<link href="<%=this.ResolveClientUrl("~/Plugin/ColorBox/colorbox.css") %>" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    $(document).ready(function () {
        $(".<%=ColorBoxIframeName %>").colorbox({ 
			fixed:true,
            iframe: true, 
            rel: 'Default', 
            width: "<%=Width %>", height: "<%=Height %>",
            rel: "nofollow"
        });
        $(".<%=ColorBoxIframeRefreshOnCloseName %>").colorbox({
			fixed:true,
            iframe: true,
            rel: 'Default',
            width: "<%=Width %>", height: "<%=Height %>",
            rel: "nofollow",
            onClosed: function () { location.reload(true); }
        });
        $(".<%=ColorBoxPhotoName %>").colorbox({ rel: '<%=ColorBoxPhotoName %>' });
    });
</script>
<script type="text/javascript">
    function ColorBoxRedirect<%=UID %>() {
        $.colorbox({
            href: "#dvColorBoxRedirect<%=UID %>",
            inline: true,
            closeButton: false,
            overlayClose: false,
            transition: "fade"/*elastic,fade,none*/,
            opacity: "0.6"
        });
    }
    function ColorBoxAlert<%=UID %>() {
        $.colorbox({
            href: "#dvColorBoxAlert<%=UID %>",
            inline: true,
            closeButton: true,
            overlayClose: true,
            transition: "fade"/*elastic,fade,none*/,
            opacity: "0.6",
            scrolling: false,
        });
    }
    function ColorBoxIFrame<%=UID %>() {
        $.colorbox({
            href: "<%=url %>",
            iframe: true,
            closeButton: true,
            overlayClose: true,
            transition: "fade"/*elastic,fade,none*/,
            opacity: "0.6",
            scrolling: true,
            width:"<%=Width %>",
            height:"<%=Height %>"<%=refreshOnClose %>
        });
    }
    function ColorBoxRedirectResize<%=UID %>() {
        //alert($('#dvColorBoxRedirect<%=UID %>').width() + ":" + $('#dvColorBoxRedirect<%=UID %>').height());
        $.colorbox.resize({
            innerWidth: $('#dvColorBoxRedirect<%=UID %>').width() + <%=widthAdd %>,
            innerHeight: $('#dvColorBoxRedirect<%=UID %>').height() + <%=heightAdd %>,
        });
    }
    function ColorBoxAlertResize<%=UID %>() {
        //alert($('#dvColorBoxAlert<%=UID %>').width() + ":" + $('#dvColorBoxAlert<%=UID %>').height());
        $.colorbox.resize({
            innerWidth: $('#dvColorBoxAlert<%=UID %>').width()+<%=widthAdd %>,
            innerHeight: $('#dvColorBoxAlert<%=UID %>').height()+<%=heightAdd %>,
        });
    }
</script>
<script type="text/javascript">
    var count = <%=seconds %>
    var redirect = "<%=url %>"

    function CountDown<%=UID %>() {
        if (count <= 0) 
        {
            window.parent.location = redirect;
        }
        else 
        {
            count--;
            document.getElementById("timer<%=UID %>").innerHTML = "กรุณารอ <span style='color:#12CCA7;'>" + count + "</span> วินาที"
            setTimeout("CountDown<%=UID %>()", 1000)
        }
    }  
</script>

<div style="display:none;">
	<div id="dvColorBoxRedirect<%=UID %>" style="text-align:center;font-family:tahoma;vertical-align:middle;display:table;height:<% =Height %>;width:<% =Width %>;">
        <div style="display:table-cell;vertical-align:middle;">
            <div style="padding:10px;"><%=preloader %></div>
            <div id="dvRedirectContent<%=UID %>">
                <h2><%=redirectHeader %></h2>
                <p style="font-size:12pt;">
                    <%=redirectMessage %>
                </p>
            </div>
            <div id="timer<%=UID %>" style="font-weight:bold;font-size:10pt;padding-top:10px;margin-top:10px;border-top:1px dashed #ddd;"></div>
        </div>
    </div>
</div>
<div style="display:none;">
    <div id="dvColorBoxAlert<%=UID %>" style="text-align:center;font-family:tahoma;vertical-align:middle;display:table;height:<% =Height %>;width:<% =Width %>;">
        <div style="display:table-cell;vertical-align:middle;">
            <div style="padding:10px;"><%=alert %></div>
            <div id="dvAlertContent<%=UID %>">
                <h2><%=alertHeader %></h2>
                <p style="font-size:12pt;">
                    <%=alertMessage %>
                </p>
            </div>
        </div>
    </div>
</div>