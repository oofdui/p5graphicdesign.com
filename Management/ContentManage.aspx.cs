using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_ContentManage : System.Web.UI.Page
{
    #region Global Variable
    private clsSecurity clsSecurity = new clsSecurity();
    private clsSQL clsSQL = new clsSQL(clsGlobal.dbType,clsGlobal.cs);
    private clsDefault clsDefault = new clsDefault();
    private string parameterChar = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "?" : "@");
    private string functionGetDate = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "NOW()" : "GETDATE()");
    public string pathUpload = "/Upload/PhotoInsert/";
    public string tableDefault = "P5_Content";
    public string webDefault = "Content.aspx";
    public string webManage = "ContentManage.aspx";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                if (clsDefault.QueryStringChecker("command") == "edit")
                {
                    BindDetail(Request.QueryString["id"].ToString());
                }
                else
                {
                    ucColorBox1.Redirect("/Management/"+webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
                }
            }
            else
            {
                //ucColorBox1.Redirect("/Management/Content.aspx", "ไม่พบหน้าเว็บที่คุณต้องการ");
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
        strSQL.Append("A.Name,");
        strSQL.Append("A.Detail,");
        strSQL.Append("A.Content,A.ContentEN,");
        strSQL.Append("A.Sort,");
        strSQL.Append("A.StatusFlag ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault+" A ");
        strSQL.Append("WHERE ");
        strSQL.Append("A.UID=" + parameterChar + "ID");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar+"ID", id } });
        if (dt != null && dt.Rows.Count > 0)
        {
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtDetail.Text = dt.Rows[0]["Detail"].ToString();
            ucContent.Text = dt.Rows[0]["Content"].ToString();
            ucContentEN.Text = dt.Rows[0]["ContentEN"].ToString();
            txtSort.Text = dt.Rows[0]["Sort"].ToString();
            cbActive.Checked = (dt.Rows[0]["StatusFlag"].ToString() == "A" ? true : false);
        }
        else
        {
            ucColorBox1.Redirect("/Management/"+webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
        }
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
            if (clsSQL.Update("Content",
                new string[,]{
                    {"Content","'" + ucContent.Text.SQLQueryFilter() + "'"},
                    {"ContentEN","'" + ucContentEN.Text.SQLQueryFilter() + "'"},
                    {"MUser",clsSecurity.LoginUID},
                    {"MWhen",functionGetDate},
                    {"Sort",txtSort.Text.SQLQueryFilter()},
                    {"StatusFlag","'" + (cbActive.Checked ? "A" : "I") + "'"}
                }, new string[,] { { } },
                "UID=" + id.ToString(),
                out outSQL))
            {
                ucColorBox1.ReloadParent();
            }
            else
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", outSQL, AlertImage: ucColorBox.Alerts.Fail);
            }
        }
        #endregion
        #region Insert
        else
        {
            if (clsSQL.Insert("Content",
                new string[,]{
                    {"Name","'" + txtName.Text.SQLQueryFilter() + "'"},
                    {"Detail","'" + txtDetail.Text.SQLQueryFilter() + "'"},
                    {"Content","'" + ucContent.Text.SQLQueryFilter() + "'"},
                    {"ContentEN","'" + ucContentEN.Text.SQLQueryFilter() + "'"},
                    {"CUser",clsSecurity.LoginUID},
                    {"CWhen",functionGetDate},
                    {"MUser",clsSecurity.LoginUID},
                    {"MWhen",functionGetDate},
                    {"Sort",txtSort.Text.SQLQueryFilter()},
                    {"StatusFlag","'" + (cbActive.Checked ? "A" : "I") + "'"}
                }, new string[,] { { } },
                out outSQL))
            {
                ucColorBox1.ReloadParent();
            }
            else
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", outSQL, AlertImage: ucColorBox.Alerts.Fail);
            }
        }
        #endregion
        #endregion
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        ucColorBox1.Close();
    }
}