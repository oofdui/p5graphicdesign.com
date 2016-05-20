using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portfolio : System.Web.UI.Page
{
    #region GlobalVariable
    public string portfolioIcon = "";
    public string portfolioName = "";
    public string portfolioDetail = "";
    public string portfolioContent = "";
    clsDefault clsDefault = new clsDefault();
    clsLanguage clsLanguage = new clsLanguage();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setDefault();
        }
    }
    private void setDefault()
    {
        var uid = clsDefault.URLRouting("id");
        if (string.IsNullOrEmpty(uid))
        {
            return;
        }
        #region Variable
        var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
        var dt = new DataTable();
        var strSQL = new StringBuilder();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("A.Photo GroupPhoto, A.Name GroupName,A.NameEN GroupNameEN, A.Detail GroupDetail,A.DetailEN GroupDetailEN, B.PhotoPreview,B.Photo,B.Name,B.Detail ");
        strSQL.Append("FROM ");
        strSQL.Append("photogallerygroup A ");
        strSQL.Append("LEFT JOIN photogallery B ON A.UID = B.PhotoGalleryGroupUID AND B.StatusFlag = 'A' ");
        strSQL.Append("WHERE ");
        strSQL.Append("A.StatusFlag = 'A' ");
        strSQL.Append("AND A.UID=@UID ");
        strSQL.Append("ORDER BY B.Sort;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { "@UID", uid } });
        if (dt != null && dt.Rows.Count > 0)
        {
            portfolioIcon = dt.Rows[0]["GroupPhoto"].ToString();
            if (clsLanguage.LanguageCurrent == "th-TH")
            {
                portfolioName = dt.Rows[0]["GroupName"].ToString();
                portfolioDetail = dt.Rows[0]["GroupDetail"].ToString();
            }
            else
            {
                portfolioName = dt.Rows[0]["GroupNameEN"].ToString();
                portfolioDetail = dt.Rows[0]["GroupDetailEN"].ToString();
            }

            this.Title = portfolioName;
            lblIcon.Text = "<img src='" + portfolioIcon + "' alt='" + portfolioName + "' title='" + portfolioName + "' style='width:100px;'/>";
            lblName.Text = portfolioName;
            lblDetail.Text = portfolioDetail;
            lblContent.Text = portfolioContent;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 4 == 0) lblContent.Text += "<br/>";
                lblContent.Text += "<div style='display:inline-block;text-align:center;margin:10px 15px;'>";
                lblContent.Text += "<a href='" + dt.Rows[i]["Photo"].ToString() + "' class='cbPhoto' title='" + dt.Rows[i]["Detail"].ToString() + "'>";
                //lblContent.Text += "<img src='" + dt.Rows[i]["PhotoPreview"].ToString() + "' style='border-radius:20px;' title='" + dt.Rows[i]["Detail"].ToString() + "'/>";
                lblContent.Text += "<img src='" + dt.Rows[i]["PhotoPreview"].ToString() + "' style='' title='" + dt.Rows[i]["Detail"].ToString() + "'/>";
                //lblContent.Text += "<p style='color:#4D4D4D;font-family:thaisans_neueregular;'>" + dt.Rows[i]["Name"].ToString() + "</p>";
                lblContent.Text += "</a>";
                lblContent.Text += "</div>";
            }
        }
        else
        {
            portfolioName = "- ไม่พบข้อมูล -";
        }
        #endregion
    }
}