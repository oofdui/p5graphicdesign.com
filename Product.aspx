<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Product" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/CSS/cssDefault.css" rel="stylesheet" />
    <style>
        h1,h2,h3,h4,ol,ul,li,img,p{margin:0;padding:0;}
        ul,li{list-style-position:inside;}
        h1,h2,h3,h4{font-family:thaisans_neuebold,tahoma;line-height:1.2em;}
        h1{font-size:2.4em;}
        h2{font-size:2.0em;}
        h3{font-size:1.6em;}
        h4{font-size:1.2em;}
        img
        {
            border-style: none;border-width: 0;
            text-align:center;vertical-align:middle;
            max-width:100%; height:auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding:10px;">
            <div style="text-align:left;">
                <div style="float:left;">
                    <asp:Label ID="lblIcon" runat="server" />
                </div>
                <div style="float:left;padding-left:10px;">
                    <h1><asp:Label ID="lblName" runat="server" /></h1>
                    <p style="font-family:thaisans_neuebold;"><asp:Label ID="lblDetail" runat="server" /></p>
                </div>
                <div style="clear:both;"></div>
            </div>
            <div style="text-align:left;padding-top:10px;margin-top:10px;border-top:1px solid #DDD;">
                <asp:Label ID="lblContent" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
