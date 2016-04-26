using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    #region Variable
    clsSecurity clsSecurity = new clsSecurity();
    clsLanguage clsLanguage = new clsLanguage();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(clsLanguage.LanguageCurrent== "th-TH")
            {
                mnHome.Text = "หน้าแรก";
                mnProductService.Text = "สินค้า / บริการ";
                mnPortfolio.Text = "ผลงานที่ผ่านมา";
                mnCompanyProfile.Text = "เกี่ยวกับ P5";
                mnSendMail.Text = "ส่งไฟล์งาน / ขอใบเสนอราคา";
                mnContactUs.Text = "ติดต่อ P5";
            }
            else
            {

            }
            setProductGroup();
        }
    }
    private void setProductGroup()
    {
        #region Variable
        var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
        var dt = new DataTable();
        var strSQL = new StringBuilder();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("UID,Name,Detail ");
        strSQL.Append("FROM ");
        strSQL.Append("ProductGroup ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort,Name;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString());
        if(dt!=null && dt.Rows.Count > 0)
        {
            menuProductGroup.Text += "<div class='submenu'><ul>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                menuProductGroup.Text += "<li style='padding-left:20px;'>";
                //menuProductGroup.Text += "<a href='/Product/"+ dt.Rows[i]["UID"].ToString() + "/"+
                //    dt.Rows[i]["Name"].ToString().Replace(" ","-").Replace("/","-").Replace(@"\", "-").Replace("<", "-").Replace(">", "-") + 
                //    "/' alt='"+ dt.Rows[i]["Detail"].ToString() + "'>"+ 
                //    "   - " + dt.Rows[i]["Name"].ToString() + 
                //    "</a>";
                menuProductGroup.Text += "<a href='#product' class='more scrolly'>" + "   - " + dt.Rows[i]["Name"].ToString() + "</a>";
                menuProductGroup.Text += "</li>";
            }
            menuProductGroup.Text += "</ul></div>";
        }
        #endregion
    }
}
