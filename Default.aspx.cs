using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    #region GlobalVariable
    public string portfolioOut = "";
    clsLanguage clsLanguage = new clsLanguage();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setProduct();
            setPortfolio();
        }
    }
    private void setProduct()
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("UID,Icon,Name,Detail,NameEN,DetailEN ");
        strSQL.Append("FROM ");
        strSQL.Append("P5_ProductGroup ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString());
        if(dt!=null && dt.Rows.Count > 0)
        {
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                lblProduct.Text += "<li style='font-family:thaisans_neuebold;color:#2E3842;'>";
                lblProduct.Text += "<a href='/Product/"+dt.Rows[i]["UID"].ToString()+"/"+dt.Rows[i]["Name"].ToString().Trim().Replace(" ","-").Replace("/","-").Replace(@"\","-")+ "/' class='cbDefault'>";
                lblProduct.Text += "<img src='" + dt.Rows[i]["Icon"].ToString() + "' style='width:150px;' alt='"+dt.Rows[i]["Name"].ToString()+"'/>";
                if (clsLanguage.LanguageCurrent == "th-TH")
                {
                    lblProduct.Text += "<p>" + dt.Rows[i]["Name"].ToString() + "</p></a>";
                }
                else
                {
                    lblProduct.Text += "<p>" + dt.Rows[i]["NameEN"].ToString() + "</p></a>";
                }
                lblProduct.Text += "</li>";
            }
        }
        #endregion
    }
    private void setPortfolio()
    {
        #region Variable
        var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
        var dt = new DataTable();
        var strSQL = new StringBuilder();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("UID,Photo,Name,Detail,NameEN,DetailEN ");
        strSQL.Append("FROM ");
        strSQL.Append("P5_PhotoGalleryGroup ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort ASC;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString());
        if(dt!=null && dt.Rows.Count > 0)
        {
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                var name = "";var detail = "";
                if (clsLanguage.LanguageCurrent == "th-TH")
                {
                    name = dt.Rows[i]["Name"].ToString();
                    detail = dt.Rows[i]["Detail"].ToString();
                }
                else
                {
                    name = dt.Rows[i]["NameEN"].ToString();
                    detail = dt.Rows[i]["DetailEN"].ToString();
                }
                if (i % 4 == 0) portfolioOut += "<br/>";
                portfolioOut += "<div style='display:inline-block;text-align:center;margin:0 15px;'>";
                portfolioOut += "<a href='/Portfolio/"+ dt.Rows[i]["UID"].ToString() + "/"+ name.Trim().Replace(" ", "-").Replace("/", "-").Replace(@"\", "-") + "/' class='cbPortfolio' title='" + detail + "'>";
                portfolioOut += "<img src='" + dt.Rows[i]["Photo"].ToString() + "' style='b' title='" + detail + "'/>";
                portfolioOut += "<p style='color:#4D4D4D;font-family:thaisans_neueregular;'>"+ name + "</p>";
                portfolioOut += "</a>";
                portfolioOut += "</div>";
            }
        }
        #endregion
    }
}