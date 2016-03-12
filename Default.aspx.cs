﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setProduct();
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
        strSQL.Append("UID,Icon,Name,Detail ");
        strSQL.Append("FROM ");
        strSQL.Append("productgroup ");
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
                lblProduct.Text += "<p>"+dt.Rows[i]["Name"].ToString()+"</p></a>";
                lblProduct.Text += "</li>";
            }
        }
        #endregion
    }
}