<%@ Page Title="" Language="C#" MasterPageFile="~/Management/MasterPage.master" AutoEventWireup="true" CodeFile="PhotoGallery.aspx.cs" Inherits="Management_PhotoGallery" %>

<%@ Register src="../UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="../UserControl/ucLoader/ucLoader.ascx" tagname="ucLoader" tagprefix="uc3" %>
<%@ Register src="../UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .dvPhotoFrame
        {
            border:1px solid #ddd;
            margin:5px;
            float:left;
            display:block;
            width:200px;
        }
        .dvPhotoFrameControl
        {
            padding:3px;
            background-color:#EEE;
            text-align:center;
        }
        .dvPhotoFramePhoto
        {
            border-bottom:1px solid #E6E6E6;
            width:200px;/*height:150px;*/
            overflow:hidden;float:left;margin-right:10px;
            background-color:#fff;
        }
        .dvPhotoFramePhoto img
        {
            filter:alpha(opacity=80);
            -moz-opacity:.80;opacity:.80;
        }
        .dvPhotoFramePhoto:hover img
        {
            filter:alpha(opacity=100);
            -moz-opacity:1;opacity:1;
        }
        .dvPhotoFrameName
        {
            background-color:#fafafa;
            padding:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyHead" Runat="Server">
    <div style="float:left;margin-right:5px;">
        <img src="Images/icPhotoGallery.png" alt="PhotoGallery Manage" title="PhotoGallery Manage" />
    </div>
    <div style="text-align:left;float:left;">
        <h1><asp:Label ID="lblHeader" runat="server"/></h1>
        ระบบจัดการข้อมูลรูปภาพ | <a href="/Management/<%=webParrent %>">กลับสู่หน้าหลัก</a>
    </div>
    <div style="float:right;margin-top:17px;margin-right:9px;">
        <a href="/Management/<%=webManage %>?group=<%=clsDefault.QueryStringChecker("group") %>" class="cbIFrame">
            <div class="Button AddTH"></div>
        </a>
    </div>
    <div style="clear:both;"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="Child" ColorBoxIframeRefreshOnCloseName="cbIFrame" Height="90%" ColorBoxPhotoName="cbPhoto"/>
    <uc3:ucLoader ID="ucLoader1" runat="server" />
    <asp:Label ID="lblSQL" runat="server"></asp:Label>
    <asp:Label ID="lblDG" runat="server" />

    <uc4:ucGridViewTemplate ID="ucGridViewTemplate1" runat="server" />
    <div class="GridView">
        <asp:Panel ID="pnDGHeader" runat="server">
            <div class="GridViewHeader">
            <div style="float:right;">
                <asp:Button ID="btDGSubmit" runat="server" ValidationGroup="vgDGSubmit" OnClick="btDGSubmit_Click" CssClass="Button SaveTH" />
            </div>
            <h3 style="margin-left:90px;">
                Photo Gallery&nbsp;</h3>
            <div style="clear:both;"></div>
        </div>
        <table cellpadding="0" cellspacing="0">
            <tr class="GridViewSubHeader">
                <td>
                    Photo<span class="Arrow"></span>
                </td>
            </tr>
            <tr class="GridViewItemNormal">
                <td>
                    <asp:DataList ID="dlDefault" runat="server" BorderStyle="None" CellPadding="0" 
                        RepeatDirection="Horizontal" ShowFooter="False" ShowHeader="False" 
                        RepeatColumns="3" RepeatLayout="Flow" Visible="false" HorizontalAlign="Justify">
                        <ItemTemplate>
                            <div class="dvPhotoFrame">
                                <div class="dvPhotoFrameControl">
                                    <asp:Label ID="lblDGID" runat="server" Text='<%#Eval("UID") %>' Visible="false"/>
                                    <asp:CheckBox ID="cbDGActive" runat="server" Checked='<%#(Eval("StatusFlag").ToString()=="A"?true:false) %>' ToolTip="เปิด/ปิด การแสดงผล"/>
                                    <asp:TextBox ID="txtDGSort" runat="server" Text='<%#Eval("Sort")%>' width="40px" CssClass="txtCenter"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="txtDGSort" CssClass="validDefault" Display="Dynamic" 
                                        ErrorMessage="<br/>กรุณากรอก" ValidationGroup="vgDGSubmit"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                        ControlToValidate="txtDGSort" CssClass="validDefault" Display="Dynamic" 
                                        ErrorMessage="<br/>กรอกเฉพาะตัวเลข" MaximumValue="999" MinimumValue="0" 
                                        Type="Integer" ValidationGroup="vgDGSubmit"></asp:RangeValidator>
                                    <a href="/Management/<%=webManage %>?id=<%#Eval("UID") %>&command=edit" title="แก้ไขข้อมูล" class="cbIFrame">
                                        <div class="Icon16 Edit"></div>
                                    </a>
				                    <a onclick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')" href="/Management/<%=webManage %><%#clsDefault.QueryStringMerge(new string[,]{{"id",Eval("UID").ToString()},{"command","delete"}}) %>" class="Icon16 Delete" title="ลบข้อมูล"></a>
                                </div>
                                <div class="dvPhotoFramePhoto">
                                    <a href='<%#Eval("Photo") %>' class="cbPhoto">
                                        <img src="<%#Eval("PhotoPreview")%>" alt="<%#Eval("Name") %>" title="<%#Eval("Name") %>"/>
                                    </a>
                                </div>
                                <div class="dvPhotoFrameName">
                                    <%#Eval("Name") %>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
        <div class="GridViewFooter">
            -
        </div>
        </asp:Panel>
    </div>
</asp:Content>