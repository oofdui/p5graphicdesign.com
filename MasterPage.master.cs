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
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setProductGroup();
        }
    }
    private void SendMail()
    {
        #region MailSender
        var clsMail = new clsMail();
        var outMessage = "";

        if (!clsMail.SendByGmail(
            "GooDesign.in.th@gmail.com",
            "G00des1gn",
            System.Configuration.ConfigurationManager.AppSettings["mailAlert"],
            "Article : มีบทความใหม่ 'xxx'",
            string.Format("<h1>ส่งบทความออนไลน์ : มีบทความใหม่</h1><div><b>ชื่อบทความ</b> : {0}</div><div><b>ผู้เขียน</b> : {1}</div><div><b>อีเมล์</b> : {2}</div><div><b>เบอร์โทรศัพท์</b> : {3}</div>",
                ""),
            out outMessage,
            "AutoSystem : วารสารบริหารการศึกษาบัวบัณฑิต",
            System.Configuration.ConfigurationManager.AppSettings["mailCc"], "", "", System.Net.Mail.MailPriority.High))
        {
            //ucColorBox1.Alert("พบข้อผิดพลาดขณะส่งเมล์", outMessage, AlertImage: ucColorBox.Alerts.Fail);
            return;
        }
        #endregion
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
