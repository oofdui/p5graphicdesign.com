<%@ Page Title="" Language="C#" MasterPageFile="~/Management/MasterPage.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Management_Product" %>
<%@ Register src="/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="/UserControl/ucGridView/ucGridVIewDataTables.ascx" tagname="ucGridVIewDataTables" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyHead" Runat="Server">
    <div style="float:left;margin-right:5px;">
        <img src="Images/icContent.png" alt="Content Manage" title="Content Manage" />
    </div>
    <div style="float:left;text-align:left;">
        <h1>Product Manage</h1>
        ระบบจัดการข้อมูลสินค้าและบริการ | <a href="/Management/">กลับสู่หน้าหลัก</a>
    </div>
    <div style="float:right;margin-top:17px;margin-right:9px;">
        <a href="/Management/<%=webManage %>" class="cbIFrame">
            <div class="Button AddTH"></div>
        </a>
    </div>
    <div style="clear:both;"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="Child" ColorBoxIframeName="cbIFrame" Height="95%" Width="800px"/>
    <asp:Label ID="lblSQL" runat="server"></asp:Label>
    <asp:Label ID="lblDG" runat="server" />

    <uc2:ucGridVIewDataTables ID="ucGridVIewDataTables1" runat="server" GridViewID="gvDefault"/>
    <div class="GridView">
        <asp:Panel ID="pnDGHeader" runat="server">
            <div class="GridViewHeader">
                <div style="float:right;">
                    <asp:Button ID="btDGSubmit" runat="server" ValidationGroup="vgDGSubmit" OnClick="btDGSubmit_Click" CssClass="Button SaveTH" />
                </div>
                <h3 style="margin-left:90px;">
                    DataList
                </h3>
                <div style="clear:both;"></div>
            </div>
        </asp:Panel>
        <asp:GridView id="gvDefault" runat="server" AutoGenerateColumns="false" 
            ShowHeader="true" ShowFooter="true" CellPadding="0" Width="100%" GridLines="None">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <th class="GridViewSubHeader" style="width:30px;">
				                    
			            </th>
                        <th class="GridViewSubHeader" style="width:100px;">
				            ภาพ
			            </th>
			            <th class="GridViewSubHeader">
				            ชื่อ / รายละเอียด
			            </th>
                        <th class="GridViewSubHeader" style="width:100px;">
				            วันเวลา
			            </th>
                        <th class="GridViewSubHeader" style="width:30px;">
				                    
			            </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <td class="GridViewItem">
                            <!--### CurrentPageChecker:START ###-->
                            <div style="display:none;">
                                <asp:CheckBox ID="cbDGCurrentPage" runat="server" Checked="true"/>
                            </div>
                            <!--### CurrentPageChecker:END ###-->
				            <asp:Label ID="lblDGID" runat="server" Text='<%#Eval("UID") %>' Visible="false"/>
                            <asp:CheckBox ID="cbDGActive" runat="server" Checked='<%#(Eval("StatusFlag").ToString()=="A"?true:false) %>' ToolTip="เปิด/ปิด การแสดงผล"/>
			            </td>
                        <td class="GridViewItem">
                            <img src='<%#Eval("Icon") %>' style="width:100px;"/>
                        </td>
                        <td class="GridViewItem">
                            <div style="text-align:left;padding:10px;">
                                <a href="/Management/ContentManage.aspx?id=<%#Eval("UID") %>&command=edit" class="cbIFrame">
                                    <h3><%#Eval("Name") %></h3>
                                </a>
                                <div>
                                    <%#Eval("Detail") %>
                                </div>
                            </div>
			            </td>
                        <td class="GridViewItem">
				            <%#DateTime.Parse(Eval("MWhen").ToString()).ToString("dd/MM/yyyy HH:mm") %>
			            </td>
                        <td class="GridViewItem">
                            <a href="/Management/<%=webManage %>?id=<%#Eval("UID") %>&command=edit" title="ดูข้อมูล" class="cbIFrame">
                                <div class="Icon16 Edit"></div>
                            </a>
				            <a onclick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')" href="/Management/<%=webManage %>?id=<%#Eval("UID") %>&command=delete" class="Icon16 Delete" title="ลบข้อมูล"></a>
			            </td>
                    </ItemTemplate>
                    <FooterTemplate>
                        <th class="GridViewSubHeader">
				                    
			            </th>
                        <th class="GridViewSubHeader">
				            ภาพ
			            </th>
			            <th class="GridViewSubHeader">
				            ชื่อ / รายละเอียด
			            </th>
                        <th class="GridViewSubHeader">
				            วันเวลา
			            </th>
                        <th class="GridViewSubHeader">
				                    
			            </th>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>