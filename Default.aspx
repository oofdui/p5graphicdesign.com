<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/UserControl/ucContent/ucContent.ascx" TagPrefix="uc1" TagName="ucContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <!-- Banner -->
	<section id="banner" style="background-image:url('/Images/bgHome.jpg');">
		<div class="inner">
			<h2>P 5 Graphic Design</h2>
            <p><img src="/Images/logo2.png" alt="P 5 Graphic Design" style="max-width:100%;height:auto;"/></p>
			<p>งามพิมพ์ Inkjet Indoor & Outdoor Display อะคริลิค โครงสร้างสำเร็จรูป</p>
		</div>
		<a href="#Product" class="more scrolly"></a>
	</section>
	<section id="Product" class="wrapper style1 special" style="background-color:#FFF100;">
		<div class="inner">
			<header class="major" style="padding:0!important;margin:0!important;">
				<h2 style="font-family:thaisans_neuebold;color:#2E3842;">Product</h2>
				<p style="padding:0;margin:0;color:#A39A01;">
                    <uc1:ucContent runat="server" ID="ucContentProduct" ContentName="Product"/>
				</p>
			</header>
			<ul class="icons major" style="padding:0!important;margin:0!important;">
				<asp:Label ID="lblProduct" runat="server" />
			</ul>
		</div>
	</section>
    <section id="Portfolio" class="wrapper style3 special" style="background-color:#FAFAFA;color:#444;">
		<div class="inner">
			<header class="major" style="padding:0!important;margin:0!important;">
				<h2 style="color:#FFC90E;">Portfolio</h2>
				<p>ผลงานที่ผ่านมา</p>
			</header>
            <div style="margin:0!important;padding:0!important;">
                <%=portfolioOut %>
            </div>
		</div>
	</section>
    <section id="CompanyProfile" class="wrapper style4" style="background-color:#797979;">
		<div class="inner">
			<header>
				<h2>Company Profile</h2>
				<p style="padding:0;margin:0;">ประวัติบริษัท</p>
			</header>
            <uc1:ucContent runat="server" ID="ucContentCompanyProfile" ContentName="CompanyProfile" />
		</div>
	</section>
    <section id="ContactUs" class="wrapper style4">
		<div class="inner">
			<header>
				<h2>Contact us</h2>
				<p style="padding:0;margin:0;">ติดต่อเรา</p>
			</header>
            <uc1:ucContent runat="server" ID="ucContentContactUs" ContentName="ContactUs" />
		</div>
	</section>
</asp:Content>