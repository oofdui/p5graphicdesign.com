using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_PhotoGalleryManage : System.Web.UI.Page
{
    #region Global Variable
    private clsSecurity clsSecurity = new clsSecurity();
    private clsDefault clsDefault = new clsDefault();
    private string parameterChar = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "?" : "@");
    private string functionGetDate = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "NOW()" : "GETDATE()");
    public string tableDefault = "P5_PhotoGallery";
    public string webDefault = "PhotoGallery.aspx";
    public string webManage = "PhotoGalleryManage.aspx";
    public string pathUpload = "/Upload/PhotoGallery/";
    public int photoWidth = 800;
    public int photoHeight = 600;
    public int photoPreviewWidth = 200;
    public int photoPreviewHeight = 150;
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
                    vdPhoto.Enabled = false;
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
            else if (Request.QueryString["group"] != null)
            {
                ucColorBox1.SizeChange();
            }
            else
            {
                ucColorBox1.Redirect("/", "ไม่พบหน้าเว็บที่คุณต้องการ");
            }
        }
    }
    protected void BindDetail(string id)
    {
        #region Variable
        var clsSQL = new clsSQL(clsGlobal.dbType,clsGlobal.cs);
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("A.PhotoPreview,");
        strSQL.Append("A.Photo,");
        strSQL.Append("A.Name,");
        strSQL.Append("A.Detail,");
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
            #region Data Builder
            if (dt.Rows[0]["PhotoPreview"] != DBNull.Value)
            {
                lblPhoto.Text = "<img src='" + dt.Rows[0]["PhotoPreview"].ToString() + "'/><br/>";
            }
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtDetail.Text = dt.Rows[0]["Detail"].ToString();
            txtSort.Text = dt.Rows[0]["Sort"].ToString();
            cbActive.Checked = (dt.Rows[0]["StatusFlag"].ToString() == "A" ? true : false);
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
        var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
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
        strSQL.Append("UID=" + id+";");
        #endregion
        string photoDelete = clsSQL.Return(strSQL.ToString());
        if (!string.IsNullOrEmpty(photoDelete))
        {
            clsIO.FileExist(photoDelete, true);
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
        #region Delete PhotoPreview
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("PhotoPreview ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + id + ";");
        #endregion
        string photoPreviewDelete = clsSQL.Return(strSQL.ToString());
        if (!string.IsNullOrEmpty(photoPreviewDelete))
        {
            clsIO.FileExist(photoPreviewDelete, true);
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
        var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
        var strSQL = new StringBuilder();
        var id = 0;
        var outSQL="";
        var photoName = "";
        var photoPreviewName = "";
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
                string outErrorMessage;
                string outFilename;
                string outFilenamePreview;

                #region Photo
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
                #endregion
                #region Photo Preview
                if (clsIO.UploadPhoto(
                    fuPhoto, pathUpload,
                    tableDefault + id.ToString() + "Preview",
                    out outErrorMessage,
                    out outFilenamePreview,
                    maxWidth: photoPreviewWidth,
                    maxHeight: photoPreviewHeight))
                {
                    photoPreviewName = outFilenamePreview;
                }
                else
                {
                    ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะอัพโหลดไฟล์รูปภาพ<br/>" + outErrorMessage, AlertImage: ucColorBox.Alerts.Fail);
                    return;
                }
                #endregion
            }
            #endregion
            if (clsSQL.Update(tableDefault,
                new string[,]{
                    {"PhotoPreview",(!string.IsNullOrEmpty(photoPreviewName)?"'"+pathUpload+photoPreviewName+"'":"PhotoPreview")},
                    {"Photo",(!string.IsNullOrEmpty(photoName)?"'"+pathUpload+photoName+"'":"Photo")},
                    {"Name","'"+clsSQL.CodeFilter(txtName.Text)+"'"},
                    {"Detail","'"+clsSQL.CodeFilter(txtDetail.Text)+"'"},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen","GETDATE()"},
                    {"Sort",clsSQL.CodeFilter(txtSort.Text)},
                    {"StatusFlag","'" + (cbActive.Checked ? "A" : "I") + "'"}
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
        #region Insert
        else
        {
            #region Find New ID
            id = clsSQL.GetNewIDAutoIncrement(tableDefault);
            if (id == 0)
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", "ไม่สามารถหา UID ใหม่ได้", AlertImage: ucColorBox.Alerts.Fail);
                return;
            }
            #endregion
            #region Photo Upload
            if (fuPhoto.HasFile)
            {
                var clsIO = new clsIO();
                string outErrorMessage;
                string outFilename;
                string outFilenamePreview;
                #region Photo
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
                #endregion
                #region PhotoPreview
                if (clsIO.UploadPhoto(
                    fuPhoto, pathUpload,
                    tableDefault + id.ToString() + "Preview",
                    out outErrorMessage,
                    out outFilenamePreview,
                    maxWidth: photoPreviewWidth,
                    maxHeight: photoPreviewHeight))
                {
                    photoPreviewName = outFilenamePreview;
                }
                else
                {
                    ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะอัพโหลดไฟล์รูปภาพ<br/>" + outErrorMessage, AlertImage: ucColorBox.Alerts.Fail);
                    return;
                }
                #endregion
            }
            #endregion
            if (clsSQL.Insert(tableDefault,
                new string[,]{
                    {"PhotoGalleryGroupUID",clsDefault.QueryStringChecker("group")},
                    {"PhotoPreview",(!string.IsNullOrEmpty(photoPreviewName)?"'"+pathUpload+photoPreviewName+"'":"null")},
                    {"Photo",(!string.IsNullOrEmpty(photoName)?"'"+pathUpload+photoName+"'":"null")},
                    {"Name","'"+clsSQL.CodeFilter(txtName.Text)+"'"},
                    {"Detail","'"+clsSQL.CodeFilter(txtDetail.Text)+"'"},
                    {"[View]","0"},
                    {"CUser","'" + clsSecurity.LoginUID + "'"},
                    {"CWhen","GETDATE()"},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen","GETDATE()"},
                    {"Sort",clsSQL.CodeFilter(txtSort.Text)},
                    {"StatusFlag","'" + (cbActive.Checked ? "A" : "I") + "'"}
                }, new string[,] { { } },
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
}