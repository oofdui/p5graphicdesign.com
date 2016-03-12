﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_SliderManage : System.Web.UI.Page
{
    #region Global Variable
    private clsSecurity clsSecurity = new clsSecurity();
    private clsSQL clsSQL = new clsSQL(clsGlobal.dbType,clsGlobal.cs);
    private clsDefault clsDefault = new clsDefault();
    private string parameterChar = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "?" : "@");
    private string functionGetDate = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "NOW()" : "GETDATE()");
    public string tableDefault = "Slider";
    public string webDefault = "Slider.aspx";
    public string webManage = "SliderManage.aspx";
    public string pathUpload = "/Upload/Slider/";
    public int photoWidth = 1000;
    public int photoHeight = 340;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Procedure
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
                    ucColorBox1.Redirect("/Management/" + webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
                }
            }
            else
            {
                ucColorBox1.SizeChange();
            }
            #endregion
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
        strSQL.Append("A.Photo,");
        strSQL.Append("A.Name,");
        strSQL.Append("A.Detail,");
        strSQL.Append("A.Sort,");
        strSQL.Append("A.StatusFlag ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " A ");
        strSQL.Append("WHERE ");
        strSQL.Append("A.UID="+parameterChar+"ID;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "ID", id } });
        if (dt != null && dt.Rows.Count > 0)
        {
            #region Data Builder
            if (dt.Rows[0]["Photo"] != DBNull.Value)
            {
                lblPhoto.Text = "<img src='" + dt.Rows[0]["Photo"].ToString() + "' style='width:100%;'/><br/>";
            }
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtDetail.Text = dt.Rows[0]["Detail"].ToString();
            txtSort.Text = dt.Rows[0]["Sort"].ToString();
            cbActive.Checked = (dt.Rows[0]["StatusFlag"].ToString() == "A" ? true : false);
            #endregion
        }
        else
        {
            ucColorBox1.Redirect("/Management/" + webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
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
        var outSQL="";
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
                    {"Photo",(!string.IsNullOrEmpty(photoName)?"'"+pathUpload+photoName+"'":"Photo")},
                    {"Name","'"+txtName.Text.SQLQueryFilter()+"'"},
                    {"Detail","'"+txtDetail.Text.SQLQueryFilter()+"'"},
                    {"MUser",clsSecurity.LoginUID},
                    {"MWhen",functionGetDate},
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
                    {"Photo",(!string.IsNullOrEmpty(photoName)?"'"+pathUpload+photoName+"'":"null")},
                    {"Name","'"+txtName.Text.SQLQueryFilter()+"'"},
                    {"Detail","'"+txtDetail.Text.SQLQueryFilter()+"'"},
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