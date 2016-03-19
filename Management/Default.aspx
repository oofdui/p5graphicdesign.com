<%@ Page Title="" Language="C#" MasterPageFile="~/Management/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Management_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .Menu
        {
            display:inline-block;
            padding:5px 8px 5px 8px;
            background-color:#ffffff;
            border:0px;
        }
        .Menu:hover
        {
            display:inline-block;
            padding:4px 7px 4px 7px;
            background-color:#fafafa;
            border:1px solid #dddddd;
        }
        .GroupMenu
        {
            text-align:left;border:1px solid #ddd;
        }
        .GroupMenu legend
        {
            font-family:thaisans_neuebold,tahoma;
            font-size:12pt;
            padding:0px 5px 0px 5px;
            color:#666666;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyHead" runat="server">
    <div style="float:left;margin-right:5px;">
        <img src="Images/icManagement.png" alt="Management System" title="Management System" />
    </div>
    <div style="text-align:left;">
        <h1>Management System</h1>
        ระบบจัดการข้อมูลบนเว็บไซต์ | <a href="/">กลับสู่หน้าหลัก</a>
    </div>
    <div style="clear:both;"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" Runat="Server">
    <fieldset class="GroupMenu">
        <legend>
            จัดการข้อมูลเว็บไซต์
        </legend>
        <div class="Menu">
            <a href="Content.aspx">
                <img src="Images/icContent.png" alt="Content" title="Content" width="32px"/>  Content Manage
            </a>
        </div>
        <div class="Menu">
            <a href="Product.aspx">
                <img src="Images/icSlider.png" alt="Product" title="Product" width="32px"/>  Product Manage
            </a>
        </div>
        <div class="Menu">
            <a href="PortfolioGroup.aspx">
                <img src="Images/icPhotoGallery.png" alt="Portfolio" title="Portfolio" width="32px"/>  Portfolio Manage
            </a>
        </div>
    </fieldset>
    <fieldset class="GroupMenu">
        <legend>
            จัดการข้อมูลการสื่อสารกับลูกค้า
        </legend>
        <div class="Menu">
            <a href="User.aspx">
                <img src="Images/icUser.png" alt="User" title="User" width="32px"/>  User Manage
            </a>
        </div>
    </fieldset>
</asp:Content>