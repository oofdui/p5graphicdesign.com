using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_PhotoGallery : System.Web.UI.Page
{
    #region Global Variable
    public clsDefault clsDefault = new clsDefault();
    private clsSecurity clsSecurity = new clsSecurity();
    private string parameterChar = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "?" : "@");
    private string functionGetDate = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "NOW()" : "GETDATE()");
    public string pathUpload = "/Upload/PhotoGallery/";
    public string tableDefault = "PhotoGallery";
    public string webDefault = "PhotoGallery.aspx";
    public string tableParrent = "PhotoGalleryGroup";
    public string webParrent = "PortfolioGroup.aspx";
    public string webManage = "PhotoGalleryManage.aspx";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["group"] != null)
            {
                BindDefault();
            }
            else
            {
                ucColorBox1.Redirect(
                    (Request.QueryString["globalpage"] != null ?
                    Request.QueryString["globalpage"]:
                    "/"),
                    "ไม่พบข้อมูลที่คุณต้องการ");
            }
        }
    }
    protected void BindDefault()
    {
        dlDefault.Visible = true; pnDGHeader.Visible = true;
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
        #endregion
        #region Procedure
        #region Header Builder
        lblHeader.Text = clsSQL.Return("SELECT Name FROM " + tableParrent + " WHERE UID=" + clsDefault.QueryStringChecker("group"));
        #endregion
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("A.UID,");
        strSQL.Append("A.PhotoPreview,");
        strSQL.Append("A.Photo,");
        strSQL.Append("A.Name,");
        strSQL.Append("A.Detail,");
        strSQL.Append("A.[View],");
        strSQL.Append("A.MWhen,");
        strSQL.Append("A.Sort,");
        strSQL.Append("A.StatusFlag ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " A ");
        #region Where
        strSQL.Append("WHERE ");
        strSQL.Append("PhotoGalleryGroupUID=" + clsDefault.QueryStringChecker("group") + " ");
        #endregion
        strSQL.Append("ORDER BY ");
        strSQL.Append("A.Sort");
        #endregion
        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString());
        strSQL.Length = 0; strSQL.Capacity = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            lblDG.Text = "";
            dlDefault.DataSource = dt;
            dlDefault.DataBind();
            dt = null;
        }
        else
        {
            pnDGHeader.Visible = false;
            lblDG.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูลที่ต้องการ", clsDefault.AlertType.Info);
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
        var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
        #endregion
        #region Procedure
        #region SQL Builder
        for (int i = 0; i < dlDefault.Items.Count; i++)
        {
            Label lblDGID = (Label)dlDefault.Items[i].FindControl("lblDGID");
            CheckBox cbDGActive = (CheckBox)dlDefault.Items[i].FindControl("cbDGActive");
            TextBox txtDGSort = (TextBox)dlDefault.Items[i].FindControl("txtDGSort");

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
            ucColorBox1.Redirect("/Management/" + webDefault + clsDefault.QueryStringMerge(), "ดำเนินการเสร็จสิ้น", "แก้ไขข้อมูลเสร็จเรียบร้อย");
        }
        else
        {
            ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะบันทึกข้อมูลลงฐานข้อมูล<br/>" + strSQL.ToString(), AlertImage: ucColorBox.Alerts.Fail);
        }
        #endregion
    }
}