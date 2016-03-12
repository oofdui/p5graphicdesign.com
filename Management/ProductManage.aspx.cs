using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_ProductManage : System.Web.UI.Page
{
    #region Global Variable
    private clsSecurity clsSecurity = new clsSecurity();
    private clsSQL clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
    private clsDefault clsDefault = new clsDefault();
    private string parameterChar = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "?" : "@");
    private string functionGetDate = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "NOW()" : "GETDATE()");
    public string pathUpload = "/Upload/Product/";
    public string tableDefault = "ProductGroup";
    public string webDefault = "Product.aspx";
    public string webManage = "ProductManage.aspx";
    public int photoWidth = 200;
    public int photoHeight = 200;
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
                else if (clsDefault.QueryStringChecker("command") == "delete")
                {
                    Delete(Request.QueryString["id"].ToString());
                }
                else
                {
                    ucColorBox1.Redirect("/Management/" + webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
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
        strSQL.Append("A.Content,");
        strSQL.Append("A.Sort,");
        strSQL.Append("A.StatusFlag ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " A ");
        strSQL.Append("WHERE ");
        strSQL.Append("A.UID=" + parameterChar + "ID");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "ID", id } });
        if (dt != null && dt.Rows.Count > 0)
        {
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtDetail.Text = dt.Rows[0]["Detail"].ToString();
            ucContent.Text = dt.Rows[0]["Content"].ToString();
            txtSort.Text = dt.Rows[0]["Sort"].ToString();
            cbActive.Checked = (dt.Rows[0]["StatusFlag"].ToString() == "A" ? true : false);
        }
        else
        {
            ucColorBox1.Redirect("/Management/" + webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
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
        var photoName = "";
        #endregion
        #region Procedure
        #region Update
        if (Request.QueryString["id"] != null && clsDefault.QueryStringChecker("command") == "edit")
        {
            id = int.Parse(Request.QueryString["id"].ToString());
            #region Photo Upload
            if (fuPhoto.HasFile)
            {
                var clsIO = new clsIO();
                string outErrorMessage; string outFilename;
                if (clsIO.UploadPhoto(
                    fuPhoto, pathUpload,
                    tableDefault + id.ToString(),
                    out outErrorMessage,
                    out outFilename,
                    maxWidth: photoWidth,
                    maxHeight: photoHeight))
                {
                    photoName = outFilename;
                }
                else
                {
                    ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะอัพโหลดไฟล์รูปภาพ<br/>" + outErrorMessage, AlertImage: ucColorBox.Alerts.Fail);
                    return;
                }
            }
            #endregion
            if (clsSQL.Update(tableDefault,
                new string[,]{
                    {"Icon",(!string.IsNullOrEmpty(photoName)?"'"+pathUpload+photoName+"'":"Icon")},
                    {"Name","'"+txtName.Text.SQLQueryFilter()+"'"},
                    {"Detail","'"+txtDetail.Text.SQLQueryFilter()+"'"},
                    {"Content","'" + ucContent.Text.SQLQueryFilter() + "'"},
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
            id = int.Parse(clsSQL.Return("SELECT IFNULL(MAX(UID),0)+1 FROM " + tableDefault));
            #region Photo Upload
            if (fuPhoto.HasFile)
            {
                clsIO clsIO = new clsIO();
                string outErrorMessage; string outFilename;
                if (clsIO.UploadPhoto(
                    fuPhoto, pathUpload,
                    tableDefault + id.ToString(),
                    out outErrorMessage,
                    out outFilename,
                    maxWidth: photoWidth,
                    maxHeight: photoHeight))
                {
                    photoName = outFilename;
                }
                else
                {
                    ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะอัพโหลดไฟล์รูปภาพ<br/>" + outErrorMessage, AlertImage: ucColorBox.Alerts.Fail);
                    return;
                }
            }
            #endregion
            if (clsSQL.Insert(tableDefault,
                new string[,]{
                    {"Icon",(!string.IsNullOrEmpty(photoName)?"'"+pathUpload+photoName+"'":"null")},
                    {"Name","'" + txtName.Text.SQLQueryFilter() + "'"},
                    {"Detail","'" + txtDetail.Text.SQLQueryFilter() + "'"},
                    {"Content","'" + ucContent.Text.SQLQueryFilter() + "'"},
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
        strSQL.Append("Icon ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + id + ";");
        #endregion
        var photoDelete = clsSQL.Return(strSQL.ToString());
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
            ucColorBox1.Redirect(webDefault);
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
}