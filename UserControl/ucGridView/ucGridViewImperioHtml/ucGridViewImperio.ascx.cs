using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucGridViewImperio : System.Web.UI.UserControl
{
    #region Example
    /* Normal
        <uc1:ucGridViewImperio ID="ucGridViewImperio1" runat="server" />
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
        <uc1:ucGridViewImperio ID="ucGridViewImperio1" runat="server" 
            Class="gv" BackgroundColorLight="#71D5DE" BackgroundColorDark="#1DA6B2" BorderColor="#9D9D9D" HoverColor="#BAE5F1" ArrowClass="gvArrow"
            HeaderClass="gvHeader" HeaderFontColor="#FFF" HeaderStyle="text-align:right;padding-right:10px;" 
            SubHeaderClass="gvSubHeader" SubHeaderFontColor="#FFF" SubHeaderStyle="font-style:italic;" 
            ItemClass="gvItem" ItemFontColor="#1585A5" ItemStyle="font-weight:bold;" 
            ItemNormalClass="gvItemNormal" 
            FooterClass="gvFooter" FooterFontColor="#FFF" FooterStyle="font-style:italic;cursor:pointer;"
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
    #endregion
    #region Property
    #region Default
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

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}