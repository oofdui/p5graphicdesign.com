using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_Slider : System.Web.UI.Page
{
    #region CreateTable
    /*
    CREATE TABLE dbo.Slider(
	    UID INT IDENTITY(1,1) NOT NULL,
	    Photo VARCHAR(100) NOT NULL,
	    Name VARCHAR(200),
	    Detail VARCHAR(500),
	    CWhen DATETIME NOT NULL,
	    CUser INT NOT NULL,
	    MWhen DATETIME NOT NULL,
	    MUser INT NOT NULL,
	    Sort INT NOT NULL,
	    StatusFlag CHAR(1) NOT NULL,
    PRIMARY KEY(UID)
    );
    */
    #endregion
    #region GlobalVariable
    public clsDefault clsDefault = new clsDefault();
    private clsSQL clsSQL = new clsSQL(clsGlobal.dbType,clsGlobal.cs);
    private clsSecurity clsSecurity = new clsSecurity();
    private string parameterChar = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "?" : "@");
    private string functionGetDate = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "NOW()" : "GETDATE()");
    public string pathUpload = "/Upload/Slider/";
    public string tableDefault = "Slider";
    public string webDefault = "Slider.aspx";
    public string webManage = "SliderManage.aspx";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDefault();
        }
    }
    protected void BindDefault()
    {
        gvDefault.Visible = true; pnDGHeader.Visible = true;
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("A.UID,");
        strSQL.Append("A.Photo,");
        strSQL.Append("A.Name,");
        strSQL.Append("A.Detail,");
        strSQL.Append("A.MWhen,");
        strSQL.Append("A.Sort,");
        strSQL.Append("A.StatusFlag ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " A ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("A.Sort;");
        #endregion
        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString());
        strSQL.Length = 0; strSQL.Capacity = 0;
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
        #endregion
    }
    protected void btDGSubmit_Click(object sender, EventArgs e)
    {
        #region Authorize
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
        #region SQL Builder
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
        #endregion
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
}