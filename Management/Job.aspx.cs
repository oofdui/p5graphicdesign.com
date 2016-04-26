using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_Job : System.Web.UI.Page
{
    #region GlobalVariable
    public clsDefault clsDefault = new clsDefault();
    private clsSQL clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
    private clsSecurity clsSecurity = new clsSecurity();
    private string parameterChar = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "?" : "@");
    private string functionGetDate = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "NOW()" : "GETDATE()");
    public string pathUpload = "/Upload/Job/";
    public string tableDefault = "Job";
    public string webDefault = "Job.aspx";
    public string webManage = "JobManage.aspx";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDefault();

            if (Request.QueryString["id"] != null)
            {
                //clsColorBox clsColorBox = new clsColorBox();
                //clsColorBox.Show("/Management/UserManage.aspx?id=" + Request.QueryString["id"].ToString() + "&command=edit", iframe: true);
            }
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
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " A ");
        strSQL.Append("ORDER BY A.UID DESC;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString());
        strSQL.Length = 0; strSQL.Capacity = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            lblDG.Text = "";
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    FileInfo fi = new FileInfo(Server.MapPath(dt.Rows[i]["FileName"].ToString()));
                    if (!fi.Exists)
                    {
                        dt.Rows[i]["FileName"] = "";
                    }
                }
                catch (Exception)
                {
                    dt.Rows[i]["FileName"] = "";
                }
            }
            dt.AcceptChanges();
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
        }
        else
        {
            pnDGHeader.Visible = false;
            lblDG.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูลที่ต้องการ", clsDefault.AlertType.Info);
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
    }
}
