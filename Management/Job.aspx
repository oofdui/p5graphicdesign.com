<%@ Page Title="" Language="C#" MasterPageFile="~/Management/MasterPage.master" AutoEventWireup="true" CodeFile="Job.aspx.cs" Inherits="Management_Job" %>

<%@ Register src="/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="/UserControl/ucGridView/ucGridVIewDataTables.ascx" tagname="ucGridVIewDataTables" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyHead" Runat="Server">
    <div style="float:left;margin-right:5px;">
        <img src="Images/icUser.png" alt="User Manage" title="User Manage" />
    </div>
    <div style="text-align:left;float:left">
        <h1>Job Manage</h1>
        ระบบจัดการข้อมูลงาน | <a href="/Management/">กลับสู่หน้าหลัก</a>
    </div>
    <%--<div style="float:right;margin-top:17px;margin-right:9px;">
        <a href="/Management/<%=webManage %>" class="cbIFrame">
            <div class="Button AddTH"></div>
        </a>
    </div>--%>
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
                <h3>
                    Job List
                </h3>
            </div>
        </asp:Panel>
        <asp:GridView id="gvDefault" runat="server" AutoGenerateColumns="false" 
            ShowHeader="true" ShowFooter="true" CellPadding="0" Width="100%" 
            GridLines="None">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                                    <th class="GridViewSubHeader" style="width:auto;">
                                        Job Detail
                                    </th>
			                        <th class="GridViewSubHeader">
				                        Contact
			                        </th>
                                    <th class="GridViewSubHeader" style="width:70px;">
				                        รับแจ้งเมื่อ
			                        </th>
                                    <th class="GridViewSubHeader" style="width:70px;">
				                        ส่งพรูฟเมื่อ
			                        </th>
                                    <th class="GridViewSubHeader" style="width:70px;">
				                        ส่งงานเมื่อ
			                        </th>
                                    <th class="GridViewSubHeader" style="width:70px;">
				                        ติดตั้งเมื่อ
			                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                                    <td class="GridViewItem" style="text-align:left;">
                                        <a href="/Management/<%#webManage %>?id=<%#Eval("UID") %>&command=edit" class="cbIFrame">
                                            <h4>
                                                <%#Eval("Name") %>
                                                <%#(Eval("FileName").ToString()!=""?" <a href='"+Eval("FileName").ToString()+"'><i class='fa fa-download' aria-hidden='true'></i></a>":"") %>
                                            </h4>
                                        </a>
                                        <%#Eval("Detail") %>
				                    </td>
                                    <td class="GridViewItem">
                                        <%#Eval("ContactName") %> / 
                                        <%#Eval("ContactPhone") %>
				                    </td>
                                    <td class="GridViewItem" style="font-size:9pt;">
                                        <%#Eval("CWhen") %>
				                    </td>
                                    <td class="GridViewItem" style="font-size:9pt;">
                                        <%#Eval("DateApprove") %>
				                    </td>
                                    <td class="GridViewItem" style="font-size:9pt;">
                                        <%#Eval("DateSubmit") %>
				                    </td>
                                    <td class="GridViewItem" style="font-size:9pt;">
                                        <%#Eval("DateInstall") %>
				                    </td>
                    </ItemTemplate>
                    <FooterTemplate>
                                    <th class="GridViewSubHeader">
                                        Job Detail
                                    </th>
			                        <th class="GridViewSubHeader">
				                        Contact
			                        </th>
                                    <th class="GridViewSubHeader" style="width:70px;">
				                        รับแจ้งเมื่อ
			                        </th>
                                    <th class="GridViewSubHeader">
				                        ส่งพรูฟเมื่อ
			                        </th>
                                    <th class="GridViewSubHeader">
				                        ส่งงานเมื่อ
			                        </th>
                                    <th class="GridViewSubHeader">
				                        ติดตั้งเมื่อ
			                        </th>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>