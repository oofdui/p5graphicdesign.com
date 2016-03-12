<%@ Page Title="" Language="C#" MasterPageFile="~/Management/MasterPage.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="Management_User"%>

<%@ Register src="/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="/UserControl/ucGridView/ucGridVIewDataTables.ascx" tagname="ucGridVIewDataTables" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyHead" Runat="Server">
    <div style="float:left;margin-right:5px;">
        <img src="Images/icUser.png" alt="User Manage" title="User Manage" />
    </div>
    <div style="text-align:left;float:left">
        <h1>User Manage</h1>
        ระบบจัดการข้อมูลสมาชิก | <a href="/Management/">กลับสู่หน้าหลัก</a>
    </div>
    <div style="float:right;margin-top:17px;margin-right:9px;">
        <a href="/Management/<%=webManage %>" class="cbIFrame">
            <div class="Button AddTH"></div>
        </a>
    </div>
    <div style="clear:both;"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="Child" ColorBoxIframeName="cbIFrame" Height="95%"/>
    <uc2:ucGridVIewDataTables ID="ucGridVIewDataTables1" runat="server" GridViewID="gvDefault"/>

    <asp:Label ID="lblSQL" runat="server"></asp:Label>
    <asp:Label ID="lblDG" runat="server" />
    <div class="GridView">
        <asp:Panel ID="pnDGHeader" runat="server">
            <div class="GridViewHeader">
                <div style="float:right;">
                    <asp:Button ID="btDGSubmit" runat="server" ValidationGroup="vgDGSubmit" OnClick="btDGSubmit_Click" CssClass="Button SaveTH" />
                </div>
                <h3 style="margin-left:90px;">
                    User List
                </h3>
                <div style="clear:both;"></div>
            </div>
        </asp:Panel>
        <asp:GridView id="gvDefault" runat="server" AutoGenerateColumns="false" 
            ShowHeader="true" ShowFooter="true" CellPadding="0" Width="100%" 
            GridLines="None">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                                    <th class="GridViewSubHeader" style="width:30px;">
				                    
			                        </th>
                                    <th class="GridViewSubHeader" style="width:100px;">
                                        ภาพ
                                    </th>
			                        <th class="GridViewSubHeader">
				                        รายละเอียด
			                        </th>
                                    <th class="GridViewSubHeader" style="width:100px;">
				                        กลุ่ม
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
                                        <div class="imgMouse">
                                            <a href="/Management/<%#webManage %>?id=<%#Eval("UID") %>&command=edit" class="cbIFrame">
                                                <%#(Eval("Photo")!=DBNull.Value?
                                                    "<img src='" + Eval("Photo") + "' title='" + Eval("Username") + "'/>" :
                                                    "")%>
                                            </a>
                                        </div>
                                    </td>
                                    <td class="GridViewItem">
                                        <div style="text-align:left;padding:10px;">
                                            <a href="/Management/<%#webManage %>?id=<%#Eval("UID") %>&command=edit" class="cbIFrame">
                                                <h3><%#Eval("Username") %></h3>
                                            </a>
                                            <%#Eval("PName") %> <%#Eval("FName") %> <%#Eval("LName") %>
                                            <div>
                                                Phone : <%#Eval("Phone") %>
                                            </div>
                                            <div>
                                                Mobile : <%#Eval("Mobile") %>
                                            </div>
                                            <div>
                                                Email : <%#Eval("Email") %>
                                            </div>
                                        </div>
			                        </td>
                                    <td class="GridViewItem">
				                        <asp:DropDownList ID="ddlDGUserGroup" runat="server" />
                                        <asp:Label ID="lblDGUserGroupUID" runat="server" Text='<%#Eval("UserGroupUID") %>' Visible="false"/>
			                        </td>
                                    <td class="GridViewItem">
                                        <a href="/Management/<%#webManage %>?id=<%#Eval("UID") %>&command=edit" title="ดูข้อมูล" class="cbIFrame">
                                            <div class="Icon16 Edit"></div>
                                        </a>
				                        <a onClick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')" href="/Management/<%#webManage %>?id=<%#Eval("UID") %>&command=delete" class="Icon16 Delete" title="ลบข้อมูล"></a>
			                        </td>
                    </ItemTemplate>
                    <FooterTemplate>
                                    <th class="GridViewSubHeader" style="width:30px;">
				                    
			                        </th>
                                    <th class="GridViewSubHeader" style="width:100px;">
                                        ภาพ
                                    </th>
			                        <th class="GridViewSubHeader">
				                        รายละเอียด
			                        </th>
                                    <th class="GridViewSubHeader" style="width:100px;">
				                        กลุ่ม
			                        </th>
                                    <th class="GridViewSubHeader" style="width:30px;">
				                    
			                        </th>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>