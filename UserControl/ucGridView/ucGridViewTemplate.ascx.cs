using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucGridViewTemplate : System.Web.UI.UserControl
{
    #region Example
    /* Normal Use
    <uc1:ucGridViewTemplate ID="ucGridViewTemplate1" runat="server" />
    <div class="GridView">
        <div class="GridViewHeader">
            <h2>ทดสอบ GridViewHeader</h2>
        </div>
        <table cellpadding="0" cellspacing="0">
            <tr class="GridViewSubHeader">
                <td style="width:100px;">
                    SubHeader 1<span class="Arrow"></span>
                </td>
                <td style="">
                    SubHeader 2<span class="Arrow"></span>
                </td>
                <td style="width:100px;">
                    SubHeader 3<span class="Arrow"></span>
                </td>
            </tr>
            <tr class="GridViewItem">
                <td>
                    Item 1
                </td>
                <td>
                    Item 2
                </td>
                <td>
                    Item 3
                </td>
            </tr>
            <tr class="GridViewItem">
                <td>
                    Item 1
                </td>
                <td>
                    Item 2
                </td>
                <td>
                    Item 3
                </td>
            </tr>
            <tr class="GridViewItemNormal">
                <td>
                    NormalItem 1
                </td>
                <td>
                    NormalItem 2
                </td>
                <td>
                    NormalItem 3
                </td>
            </tr>
            <tr class="GridViewItem">
                <td>
                    Item 1
                </td>
                <td>
                    Item 2
                </td>
                <td>
                    Item 3
                </td>
            </tr>
        </table>
        <div class="GridViewFooter">
            ทดสอบ GridViewFooter
        </div>
    </div>
    */
    /* Full Customize
    <div class="gv">
        <div class="gvHeader">
            <h2>ทดสอบ GridViewHeader</h2>
        </div>
        <table cellpadding="0" cellspacing="0">
            <tr class="gvSubHeader">
                <td style="width:100px;">
                    SubHeader 1<span class="gvArrow"></span>
                </td>
                <td style="">
                    SubHeader 2<span class="gvArrow"></span>
                </td>
                <td style="width:100px;">
                    SubHeader 3<span class="gvArrow"></span>
                </td>
            </tr>
            <tr class="gvItem">
                <td>
                    Item 1
                </td>
                <td>
                    Item 2
                </td>
                <td>
                    Item 3
                </td>
            </tr>
            <tr class="gvItem">
                <td>
                    Item 1
                </td>
                <td>
                    Item 2
                </td>
                <td>
                    Item 3
                </td>
            </tr>
            <tr class="gvItemNormal">
                <td>
                    NormalItem 1
                </td>
                <td>
                    NormalItem 2
                </td>
                <td>
                    NormalItem 3
                </td>
            </tr>
            <tr class="gvItem">
                <td>
                    Item 1
                </td>
                <td>
                    Item 2
                </td>
                <td>
                    Item 3
                </td>
            </tr>
        </table>
        <div class="gvFooter">
            ทดสอบ GridViewFooter
        </div>
    </div>
    <uc1:ucGridViewTemplate ID="ucGridViewTemplate1" runat="server"
            Class="gv" BackgroundColorDark="#6394AB" BackgroundColorLight="#94BDD0" 
            ArrowClass="gvArrow" BorderColor="#949494" HoverColor="#9CD2EB"
            HeaderClass="gvHeader" HeaderFontColor="#FFF" HeaderStyle="text-align:right;padding:5px;" 
            SubHeaderClass="gvSubHeader" SubHeaderFontColor="#FFF" SubHeaderStyle="font-style:italic;" 
            ItemClass="gvItem" ItemFontColor="#000" ItemStyle="text-align:left;" 
            ItemNormalClass="gvItemNormal" 
            FooterClass="gvFooter" FooterFontColor="#FFF" FooterStyle="padding:10px;"
    />
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