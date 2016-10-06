using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_Product : System.Web.UI.Page
{
    #region GlobalVariable
    public clsDefault clsDefault = new clsDefault();
    private clsSQL clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
    private clsSecurity clsSecurity = new clsSecurity();
    private string parameterChar = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "?" : "@");

    public string pathUpload = "/Upload/Product/";
    public string tableDefault = "P5_ProductGroup";
    public string webDefault = "Product.aspx";
    public string webManage = "ProductManage.aspx";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDefault();
            if (Request.QueryString["id"] != null)
            {
                var clsColorBox = new clsColorBox();
                clsColorBox.Show("/Management/" + webManage + "?id=" + Request.QueryString["id"].ToString() + "&command=edit", iframe: true);
            }
        }
    }
    protected void BindDefault()
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("A.UID,");
        strSQL.Append("A.Icon,");
        strSQL.Append("A.Name,");
        strSQL.Append("A.Detail,");
        strSQL.Append("A.MWhen,");
        strSQL.Append("A.Sort,");
        strSQL.Append("A.StatusFlag ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " A ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("A.Sort,A.Name;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString());
        strSQL.Length = 0; strSQL.Capacity = 0;
        gvDefault.Visible = true; pnDGHeader.Visible = true;
        if (dt != null && dt.Rows.Count > 0)
        {
            lblDG.Text = "";
            gvDefault.DataSource = dt;
            #region PageBuilder
            if (Request.QueryString["page"] != null)
            {
                try
                {
                    gvDefault.PageIndex = int.Parse(Request.QueryString["page"].ToString());
                }
                catch (Exception)
                {
                    gvDefault.PageIndex = int.Parse(Request.QueryString["page"].ToString()) - 1;
                }
            }
            #endregion
            gvDefault.DataBind();
            dt = null;
        }
        else
        {
            pnDGHeader.Visible = false;
            lblDG.Text = clsDefault.AlertMessageGray("ไม่พบข้อมูลที่ต้องการ", clsDefault.AlertType.Info);
        }
        #endregion
    }
    protected void btDGSubmit_Click(object sender, EventArgs e)
    {
        #region AdminChecker
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect("/", "เกิดข้อผิดพลาด", "คุณไม่ได้รับสิทธิ์ในการบันทึกข้อมูล กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion
        #region Variable
        var strSQL = new StringBuilder();
        #endregion
        #region Procedure
        for (int i = 0; i < gvDefault.Rows.Count; i++)
        {
            #region CurrentPageChecker
            var cbDGCurrentPage = (CheckBox)gvDefault.Rows[i].FindControl("cbDGCurrentPage");
            if (!cbDGCurrentPage.Checked) continue;
            #endregion
            Label lblDGID = (Label)gvDefault.Rows[i].FindControl("lblDGID");
            CheckBox cbDGActive = (CheckBox)gvDefault.Rows[i].FindControl("cbDGActive");
            TextBox txtDGSort = (TextBox)gvDefault.Rows[i].FindControl("txtDGSort");
            if (lblDGID != null && cbDGActive != null)
            {
                #region SQL Query
                strSQL.Append("UPDATE ");
                strSQL.Append(tableDefault + " ");
                strSQL.Append("SET ");
                strSQL.Append("Sort=" + clsSQL.CodeFilter(txtDGSort.Text) + ",");
                strSQL.Append("StatusFlag='" + (cbDGActive.Checked ? "A" : "I") + "' ");
                strSQL.Append("WHERE ");
                strSQL.Append("UID=" + lblDGID.Text);
                strSQL.Append(";");
                #endregion
            }
        }

        if (clsSQL.Execute(strSQL.ToString()))
        {
            ucColorBox1.Redirect("/Management/" + webDefault, "ดำเนินการเสร็จสิ้น", "แก้ไขข้อมูลเสร็จเรียบร้อย");
        }
        else
        {
            ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะบันทึกข้อมูลลงฐานข้อมูล<br/>" + strSQL.ToString(), AlertImage: ucColorBox.Alerts.Fail);
        }
        #endregion
    }
    protected void gvDefault_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDefault.PageIndex = e.NewPageIndex;
        BindDefault();
    }
}