using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class ucGridViewDataTables : System.Web.UI.UserControl
{
    #region Example
    /* Normal
        <uc1:ucGridViewDataTables ID="ucGridViewDataTable1" runat="server" />
        <div class='GridView'>
            <div class='GridViewHeader'>
                <h2>Box Title ภาษาไทย</h2>
            </div>
            <table id='datatable' cellpadding="0" cellspacing="0">
                <thead>
                    <tr class="GridViewSubHeader">
                        <th>
                            ชื่อเล่น<span class="Arrow"></span>
                        </th>
                        <th>
                            ชื่อจริง<span class="Arrow"></span>
                        </th>
                        <th>
                            นามสกุล<span class="Arrow"></span>
                        </th>
                        <th>
                            ฉายา 1<span class="Arrow"></span>
                        </th>
                        <th>
                            ฉายา 2<span class="Arrow"></span>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class='GridViewItem'>
                        <td>
                            อ๊อฟ
                        </td>
                        <td>
                            นิธิ
                        </td>
                        <td>
                            ฤกษ์วิชา
                        </td>
                        <td>
                            นุ่ม
                        </td>
                        <td>
                            ตูด
                        </td>
                    </tr>
                    <tr class='GridViewItem'>
                        <td>
                            เอม
                        </td>
                        <td>
                            เอมอร
                        </td>
                        <td>
                            ศิลมัฐ
                        </td>
                        <td>
                            นิ่ม
                        </td>
                        <td>
                            หอย
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="GridViewSubHeader">
                        <th>
                            ชื่อเล่น
                        </th>
                        <th>
                            ชื่อจริง
                        </th>
                        <th>
                            นามสกุล
                        </th>
                        <th>
                            ฉายา 1
                        </th>
                        <th>
                            ฉายา 2
                        </th>
                    </tr>
                </tfoot>
            </table>
        </div>
    */
    /* Full Customize
        <uc1:ucGridViewDataTables ID="ucGridViewDataTables1" runat="server" 
            Class="gv" BackgroundColorLight="#71D5DE" BackgroundColorDark="#1DA6B2" BorderColor="#9D9D9D" HoverColor="#BAE5F1" ArrowClass="gvArrow"
            HeaderClass="gvHeader" HeaderFontColor="#FFF" HeaderStyle="text-align:right;padding-right:10px;" 
            SubHeaderClass="gvSubHeader" SubHeaderFontColor="#FFF" SubHeaderStyle="font-style:italic;" 
            ItemClass="gvItem" ItemFontColor="#1585A5" ItemStyle="font-weight:bold;" 
            ItemNormalClass="gvItemNormal" 
            FooterClass="gvFooter" FooterFontColor="#FFF" FooterStyle="font-style:italic;cursor:pointer;" 
            GridViewID="gvDefault" PageSize="12" PageSizeList="6,12,24" Search="คำค้นหาเริ่มต้น" 
            StateSave="true" StateSaveSecond="30"
        />
        <div class='gv'>
            <div class='gvHeader'>
                <h2>Box Title ภาษาไทย</h2>
            </div>
            <table id="datatable" cellpadding="0" cellspacing="0">
                <thead>
                    <tr class="gvSubHeader">
                        <th>
                            ชื่อเล่น<span class="gvArrow"></span>
                        </th>
                        <th>
                            ชื่อจริง<span class="gvArrow"></span>
                        </th>
                        <th>
                            นามสกุล<span class="gvArrow"></span>
                        </th>
                        <th>
                            ฉายา 1<span class="gvArrow"></span>
                        </th>
                        <th>
                            ฉายา 2<span class="gvArrow"></span>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class='gvItem'>
                        <td>
                            อ๊อฟ
                        </td>
                        <td>
                            นิธิ
                        </td>
                        <td>
                            ฤกษ์วิชา
                        </td>
                        <td>
                            นุ่ม
                        </td>
                        <td>
                            ตูด
                        </td>
                    </tr>
                    <tr class='gvItemNormal'>
                        <td>
                            เอมอ๊อฟ
                        </td>
                        <td>
                            เอมอรนิธิ
                        </td>
                        <td>
                            ศิลมัฐฤกษ์วิชา
                        </td>
                        <td>
                            นิ่มนุ่ม
                        </td>
                        <td>
                            หอยตูด
                        </td>
                    </tr>
                    <tr class='gvItem'>
                        <td>
                            เอม
                        </td>
                        <td>
                            เอมอร
                        </td>
                        <td>
                            ศิลมัฐ
                        </td>
                        <td>
                            นิ่ม
                        </td>
                        <td>
                            หอย
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="gvSubHeader">
                        <th>
                            ชื่อเล่น
                        </th>
                        <th>
                            ชื่อจริง
                        </th>
                        <th>
                            นามสกุล
                        </th>
                        <th>
                            ฉายา 1
                        </th>
                        <th>
                            ฉายา 2
                        </th>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
    */
    /* ASP.NET GridView
    <uc1:ucGridViewDataTables ID="ucGridViewDataTables1" runat="server" GridViewID="gvDefault" PageSize="25" PageSizeList="10,20" Page="10" Search="เอมอร"/>
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
                                        ภาพ<span class="Arrow" />
                                    </th>
			                        <th class="GridViewSubHeader">
				                        รายละเอียด<span class="Arrow" />
			                        </th>
                                    <th class="GridViewSubHeader" style="width:100px;">
				                        กลุ่ม<span class="Arrow" />
			                        </th>
                                    <th class="GridViewSubHeader" style="width:30px;">
				                    
			                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                                    <td class="GridViewItem">
				                        <asp:Label ID="lblDGID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UID") %>' Visible="false"/>
                                        <asp:CheckBox ID="cbDGActive" runat="server" Checked='<%#(DataBinder.Eval(Container.DataItem, "Active")).ToString()=="1"?true:false %>' ToolTip="เปิด/ปิด การแสดงผล"/>
			                        </td>
                                    <td class="GridViewItem">
                                        <div class="imgMouse">
                                            <a href="/Management/UserManage.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>" class="cbMaxHeight">
                                                <%#(DataBinder.Eval(Container.DataItem, "Photo")!=DBNull.Value?
                                                    "<img src='" + pathUpload + DataBinder.Eval(Container.DataItem, "Photo") + "' title='" + DataBinder.Eval(Container.DataItem, "Username") + "'/>" :
                                                    "")%>
                                            </a>
                                        </div>
                                    </td>
                                    <td class="GridViewItem">
                                        <div style="text-align:left;padding:10px;">
                                            <a href="/Management/UserManage.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>" class="cbMaxHeight">
                                                <h3><%#DataBinder.Eval(Container.DataItem,"Username") %></h3>
                                            </a>
                                            <%#DataBinder.Eval(Container.DataItem,"PName") %> <%#DataBinder.Eval(Container.DataItem,"FName") %> <%#DataBinder.Eval(Container.DataItem,"LName") %>
                                            <div>
                                                Phone : <%#DataBinder.Eval(Container.DataItem,"Phone") %>
                                            </div>
                                            <div>
                                                Mobile : <%#DataBinder.Eval(Container.DataItem,"Mobile") %>
                                            </div>
                                            <div>
                                                Email : <%#DataBinder.Eval(Container.DataItem,"Email") %>
                                            </div>
                                        </div>
			                        </td>
                                    <td class="GridViewItem">
				                        <asp:DropDownList ID="ddlDGUserGroup" runat="server" />
                                        <asp:Label ID="lblDGUserGroupUID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UserGroupUID") %>' Visible="false"/>
			                        </td>
                                    <td class="GridViewItem">
                                        <a href="/Management/UserManage.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>" title="ดูข้อมูล" class="cbMaxHeight">
                                            <div class="Icon16 Edit"></div>
                                        </a>
				                        <a onClick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')" href="/Management/UserManage.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=delete" class="Icon16 Delete" title="ลบข้อมูล"></a>
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
    */
    /* Code Behind
        //เปลี่ยนหน้าจาก CodeBehind
        ucGridViewDataTables1.PageChange(gvDefault,10);
    */
    #endregion
    #region Property
    #region Default
    private string _gridViewID="gvDefault";
    public string GridViewID
    {
        get { return _gridViewID; }
        set { _gridViewID = value; }
    }
    private string _pageSize = "10"; /*10,25,50,100,-1*/
    public string PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value; }
    }
    private string _pageSizeList = "[10,25,50,100,-1],[10,25,50,100,'All']"; /*หรือใช้แค่ 10,20,50,100,-1 ก็ได้*/
    public string PageSizeList
    {
        get { return _pageSizeList; }
        set { _pageSizeList = value; }
    }
    /// <summary>
    /// คำค้นหาเริ่มต้น
    /// </summary>
    private string _search = "";
    public string Search
    {
        get { return _search; }
        set { _search = value; }
    }
    /// <summary>
    /// Enable / Disable ระบบจดจำ Search , Sort , Page
    /// </summary>
    private bool _stateSave = true;
    public bool StateSave
    {
        get { return _stateSave; }
        set { _stateSave = value; }
    }
    /// <summary>
    /// ระยะเวลา Cookie ที่ใช้เก็บข้อมูลการ Search , Sort , Page
    /// </summary>
    private int _stateSaveSecond = 30;
    public int StateSaveSecond
    {
        get { return _stateSaveSecond; }
        set { _stateSaveSecond = value; }
    }
    private string _class = "GridView";
    public string Class
    {
        get { return _class; }
        set { _class = value; }
    }
    private string _backgroundColorLight = "#FAFAFA";
    public string BackgroundColorLight
    {
        get { return _backgroundColorLight; }
        set { _backgroundColorLight = value; }
    }
    private string _backgroundColorDark = "#EDEDED";
    public string BackgroundColorDark
    {
        get { return _backgroundColorDark; }
        set { _backgroundColorDark = value; }
    }
    private string _borderColor = "#DDD";
    public string BorderColor
    {
        get { return _borderColor; }
        set { _borderColor = value; }
    }
    private string _hoverColor = "#F4F4F4";
    public string HoverColor
    {
        get { return _hoverColor; }
        set { _hoverColor = value; }
    }
    private string _arrowClass = "Arrow";
    public string ArrowClass
    {
        get { return _arrowClass; }
        set { _arrowClass = value; }
    }
    #endregion
    #region Header
    private string _headerClass = "GridViewHeader";
    public string HeaderClass
    {
        get { return _headerClass; }
        set { _headerClass = value; }
    }

    private string _headerFontColor = "#424242";
    public string HeaderFontColor
    {
        get { return _headerFontColor; }
        set { _headerFontColor = value; }
    }

    private string _headerStyle = "";
    public string HeaderStyle
    {
        get { return _headerStyle; }
        set { _headerStyle = value; }
    }
    #endregion
    #region SubHeader
    private string _subHeaderClass = "GridViewSubHeader";
    public string SubHeaderClass
    {
        get { return _subHeaderClass; }
        set { _subHeaderClass = value; }
    }

    private string _subHeaderFontColor = "#424242";
    public string SubHeaderFontColor
    {
        get { return _subHeaderFontColor; }
        set { _subHeaderFontColor = value; }
    }

    private string _subHeaderStyle = "";
    public string SubHeaderStyle
    {
        get { return _subHeaderStyle; }
        set { _subHeaderStyle = value; }
    }
    #endregion
    #region Item
    private string _itemClass = "GridViewItem";
    public string ItemClass
    {
        get { return _itemClass; }
        set { _itemClass = value; }
    }

    private string _itemFontColor = "#424242";
    public string ItemFontColor
    {
        get { return _itemFontColor; }
        set { _itemFontColor = value; }
    }

    private string _itemStyle = "";
    public string ItemStyle
    {
        get { return _itemStyle; }
        set { _itemStyle = value; }
    }
    #endregion
    #region ItemNormal
    private string _itemNormalClass = "GridViewItemNormal";
    public string ItemNormalClass
    {
        get { return _itemNormalClass; }
        set { _itemNormalClass = value; }
    }
    #endregion
    #region Footer
    private string _footerClass = "GridViewFooter";
    public string FooterClass
    {
        get { return _footerClass; }
        set { _footerClass = value; }
    }

    private string _footerFontColor = "#424242";
    public string FooterFontColor
    {
        get { return _footerFontColor; }
        set { _footerFontColor = value; }
    }

    private string _footerStyle = "";
    public string FooterStyle
    {
        get { return _footerStyle; }
        set { _footerStyle = value; }
    }
    #endregion
    #endregion
    #region Global Variable
    public GridView gv;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// เปลี่ยนหน้าปัจจุบันไปยังหน้าที่ต้องการ
    /// </summary>
    /// <param name="gv">ชื่อ GridView ที่ใช้</param>
    /// <param name="Index">หน้าที่ต้องการไป เริ่มต้นจาก 0-...</param>
    /// <example>
    /// ucGridViewDataTables1.PageChange(gvDefault,2);
    /// </example>
    public void PageChange(GridView gv, int Index)
    {
        #region Variable
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        StringBuilder strScript = new StringBuilder();
        #endregion

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "GridViewPageChange"))
        {
            #region Script Builder
            strScript.Append("$(document).ready(function () {");
            strScript.Append("var oTable = $('#" + (gv != null ? gv.ClientID : "") + "').dataTable();");
            strScript.Append("oTable.fnPageChange(" + Index.ToString() + ");");
            strScript.Append("});");
            #endregion

            currentPage.ClientScript.RegisterStartupScript(
                currentPage.GetType(),
                "GridViewPageChange",
                strScript.ToString(), true);
        }
    }

    /// <summary>
    /// เพิ่มแท็ก thead และ tbody ใน GridView.Header และ GridView.Item
    /// </summary>
    /// <param name="Grid">GridView Control ID</param>
    public static void MakeAccessible(GridView Grid)
    {
        if (Grid != null)
        {
            if (Grid.Rows.Count <= 0) return;
            Grid.UseAccessibleHeader = true;
            Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (Grid.ShowFooter)
                Grid.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        try
        {
            gv = (GridView)this.Parent.FindControl(_gridViewID);
            MakeAccessible(gv);
        }
        catch (Exception ex)
        {
            lblDefault.Text = "<b>ucGridViewDataTables</b> : "+ex.Message;
        }
    }
}