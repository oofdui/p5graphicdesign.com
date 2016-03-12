using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Product : System.Web.UI.Page
{
    #region GlobalVariable
    public string productIcon = "";
    public string productName = "";
    public string productDetail = "";
    public string productContent = "";
    clsDefault clsDefault = new clsDefault();
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
        strSQL.Append("Icon,Name,Detail,Content ");
        strSQL.Append("FROM ");
        strSQL.Append("ProductGroup ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND UID=?UID;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { "?UID", uid } });
        if (dt != null && dt.Rows.Count > 0)
        {
            productIcon = dt.Rows[0]["Icon"].ToString();
            productName = dt.Rows[0]["Name"].ToString();
            productDetail = dt.Rows[0]["Detail"].ToString();
            productContent = dt.Rows[0]["Content"].ToString().Replace("../Upload/","/Upload/");

            this.Title = productName;
            lblIcon.Text = "<img src='"+productIcon+"' alt='"+productName+"' title='"+productName+"' style='width:100px;'/>";
            lblName.Text = productName;
            lblDetail.Text = productDetail;
            lblContent.Text = productContent;
        }
        else
        {
            productName = "- ไม่พบข้อมูล -";
        }
        #endregion
    }
}