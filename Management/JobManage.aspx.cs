using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_JobManage : System.Web.UI.Page
{
    #region Global Variable
    private clsSecurity clsSecurity = new clsSecurity();
    private clsSQL clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
    private clsDefault clsDefault = new clsDefault();
    private string parameterChar = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "?" : "@");
    private string functionGetDate = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "NOW()" : "GETDATE()");
    public string tableDefault = "P5_Job";
    public string webDefault = "Job.aspx";
    public string webManage = "JobManage.aspx";
    public string pathUpload = "/Upload/Job/";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                if (clsDefault.QueryStringChecker("command") == "edit")
                {
                    ucColorBox1.SizeChange();
                    BindDetail(Request.QueryString["id"].ToString());
                }
                else if (clsDefault.QueryStringChecker("command") == "delete")
                {
                    Delete(Request.QueryString["id"].ToString());
                }
                else
                {
                    ucColorBox1.Redirect("/", "ไม่พบหน้าเว็บที่คุณต้องการ");
                }
            }
        }
    }
    protected void BindDetail(string id)
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("A.UID,");
        strSQL.Append("A.FileName,");
        strSQL.Append("A.Name,");
        strSQL.Append("A.Detail,");
        strSQL.Append("A.ContactName,");
        strSQL.Append("A.ContactPhone,");
        strSQL.Append("A.DateApprove,");
        strSQL.Append("A.DateSubmit,");
        strSQL.Append("A.DateInstall,");
        strSQL.Append("A.DateUninstall,");
        strSQL.Append("A.Location,");
        strSQL.Append("A.ProducerName,");
        strSQL.Append("A.VerifyName,");
        strSQL.Append("A.InstallName,");
        strSQL.Append("A.CWhen ");
        strSQL.Append("FROM "+ tableDefault + " A ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + parameterChar + "UID;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "UID", id } });
        if (dt != null && dt.Rows.Count > 0)
        {
            #region Data Builder
            if (dt.Rows[0]["FileName"] != DBNull.Value)
            {
                var fi = new FileInfo(Server.MapPath(dt.Rows[0]["FileName"].ToString()));

                hidFileName.Value = dt.Rows[0]["FileName"].ToString();
                lblFileName.Text = "<a href='"+ dt.Rows[0]["FileName"].ToString() + "'>"+Path.GetFileName(dt.Rows[0]["FileName"].ToString())+"</a>";
                if (fi.Exists)
                {
                    var fileSize = fi.Length / 1024;
                    lblFileName.Text += " (" + fileSize.ToString("#,#.00") + " KB)";
                }
                else
                {
                    lblFileName.Text += " <span style='color:red;'>(Deleted)</span>";
                    btDelete.Visible = false;
                }
            }
            lblName.Text = dt.Rows[0]["Name"].ToString();
            lblDetail.Text = dt.Rows[0]["Detail"].ToString();
            lblContactName.Text = dt.Rows[0]["ContactName"].ToString();
            lblContactPhone.Text = dt.Rows[0]["ContactPhone"].ToString();
            ucDateApprove.Text = dt.Rows[0]["DateApprove"].ToString();
            ucDateSubmit.Text = dt.Rows[0]["DateSubmit"].ToString();
            ucDateInstall.Text = dt.Rows[0]["DateInstall"].ToString();
            ucDateUninstall.Text = dt.Rows[0]["DateUninstall"].ToString();
            lblLocation.Text = dt.Rows[0]["Location"].ToString();
            txtProducerName.Text = dt.Rows[0]["ProducerName"].ToString();
            txtVerifyName.Text = dt.Rows[0]["VerifyName"].ToString();
            txtInstallName.Text = dt.Rows[0]["InstallName"].ToString();
            #endregion
        }
        else
        {
            ucColorBox1.Redirect("/", "ไม่พบหน้าเว็บที่คุณต้องการ");
        }
        #endregion
    }
    private void Delete(string id)
    {
        pnDetail.Visible = false;
        #region Authorize
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect("/", "กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion
        #region Variable
        var clsIO = new clsIO();
        var strSQL = new StringBuilder();
        #endregion
        #region Procedure
        #region Delete Photo
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Photo ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + id + ";");
        #endregion
        string photoDelete = clsSQL.Return(strSQL.ToString());
        if (!string.IsNullOrEmpty(photoDelete))
        {
            clsIO.FileExist(photoDelete, true);
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
        #region Delete Database
        #region SQL Query
        strSQL.Append("DELETE FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + id + ";");
        #endregion
        if (clsSQL.Execute(strSQL.ToString()))
        {
            ucColorBox1.Redirect(webDefault + clsDefault.QueryStringRemover(new string[] { "id", "command" }));
        }
        else
        {
            ucColorBox1.Redirect(webDefault, "เกิดข้อผิดพลาดขณะลบข้อมูล");
            return;
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
        #endregion
    }
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        #region Authorize
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect("/", "กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion
        #region Variable
        var strSQL = new StringBuilder();
        var id = 0;
        var outSQL = "";
        #endregion
        #region Procedure
        #region Update
        if (Request.QueryString["id"] != null && clsDefault.QueryStringChecker("command") == "edit")
        {
            id = int.Parse(Request.QueryString["id"].ToString());
            if (clsSQL.Update(tableDefault,
                new string[,]{
                    {"DateApprove",(!string.IsNullOrEmpty(ucDateApprove.Text)?"'"+ucDateApprove.Text+"'":"NULL")},
                    {"DateSubmit",(!string.IsNullOrEmpty(ucDateSubmit.Text)?"'"+ucDateSubmit.Text+"'":"NULL")},
                    {"DateInstall",(!string.IsNullOrEmpty(ucDateInstall.Text)?"'"+ucDateInstall.Text+"'":"NULL")},
                    {"DateUninstall",(!string.IsNullOrEmpty(ucDateUninstall.Text)?"'"+ucDateUninstall.Text+"'":"NULL")},
                    {"ProducerName",(!string.IsNullOrEmpty(txtProducerName.Text.SQLQueryFilter())?"'"+txtProducerName.Text.SQLQueryFilter()+"'":"NULL")},
                    {"VerifyName",(!string.IsNullOrEmpty(txtVerifyName.Text.SQLQueryFilter())?"'"+txtVerifyName.Text.SQLQueryFilter()+"'":"NULL")},
                    {"InstallName",(!string.IsNullOrEmpty(txtInstallName.Text.SQLQueryFilter())?"'"+txtInstallName.Text.SQLQueryFilter()+"'":"NULL")},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen",functionGetDate}
                }, new string[,] { { parameterChar + "UID", id.ToString() } },
                "UID=" + parameterChar + "UID",
                out outSQL))
            {
                ucColorBox1.ReloadParent();
            }
            else
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล<br/>", outSQL, AlertImage: ucColorBox.Alerts.Fail);
            }
        }
        #endregion
        #endregion
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        ucColorBox1.Close();
    }

    protected void btDelete_Click(object sender, EventArgs e)
    {
        var clsDefault = new clsDefault();
        try
        {
            var fi = new FileInfo(Server.MapPath(hidFileName.Value));
            if (fi.Exists)
            {
                fi.Delete();
                BindDetail(Request.QueryString["id"].ToString());
                lblSQL.Text = clsDefault.AlertMessageFlat("ลบไฟล์เสร็จสมบูรณ์", clsDefault.AlertType.Success);
            }
            else
            {
                lblSQL.Text = clsDefault.AlertMessageFlat("ไม่พบไฟล์ : "+fi.FullName, clsDefault.AlertType.Fail);
            }
        }
        catch(Exception ex)
        {
            lblSQL.Text = clsDefault.AlertMessageFlat("เกิดข้อผิดพลาดขณะลบไฟล์ : "+ex.Message, clsDefault.AlertType.Fail);
        }
    }
}