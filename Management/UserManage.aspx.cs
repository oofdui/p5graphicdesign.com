using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_UserManage : System.Web.UI.Page
{
    #region Global Variable
    private clsSecurity clsSecurity = new clsSecurity();
    private clsSQL clsSQL = new clsSQL(clsGlobal.dbType,clsGlobal.cs);
    private clsDefault clsDefault = new clsDefault();
    private string parameterChar = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "?" : "@");
    private string functionGetDate = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "NOW()" : "GETDATE()");
    public string tableDefault = "[P5_User]";
    public string webDefault = "User.aspx";
    public string webManage = "UserManage.aspx";
    public string pathUpload = "/Upload/User/";
    public int photoWidth = 150;
    public int photoHeight = 150;
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
                    UserGroupBuilder();
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
            else
            {
                txtPassword.Enabled = true; vdPassword.Enabled = true;
                UserGroupBuilder();
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
        strSQL.Append("UID,");
        strSQL.Append("UserGroupUID,");
        strSQL.Append("Authority,");
        strSQL.Append("Username,");
        strSQL.Append("Password,");
        strSQL.Append("Photo,");
        strSQL.Append("PName,");
        strSQL.Append("FName,");
        strSQL.Append("LName,");
        strSQL.Append("BirthDate,");
        strSQL.Append("Gender,");
        strSQL.Append("NID,");
        strSQL.Append("Phone,");
        strSQL.Append("Mobile,");
        strSQL.Append("Email,");
        strSQL.Append("Address,");
        strSQL.Append("AddressDistrict,");
        strSQL.Append("AddressPrefecture,");
        strSQL.Append("AddressProvince,");
        strSQL.Append("AddressPostal,");
        strSQL.Append("Profile,");
        strSQL.Append("Signature,");
        strSQL.Append("CWhen,");
        strSQL.Append("CUser,");
        strSQL.Append("MWhen,");
        strSQL.Append("MUser,");
        strSQL.Append("Sort,");
        strSQL.Append("StatusFlag ");
        strSQL.Append("FROM "+tableDefault+" A ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + parameterChar + "UID;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "UID", id } });
        if (dt != null && dt.Rows.Count > 0)
        {
            #region Data Builder
            if (dt.Rows[0]["Photo"] != DBNull.Value)
            {
                lblPhoto.Text = "<img src='" + dt.Rows[0]["Photo"].ToString() + "'/><br/>";
            }
            ddlUserGroup.SelectedValue = dt.Rows[0]["UserGroupUID"].ToString();
            txtUsername.Text = dt.Rows[0]["Username"].ToString();
            ddlPName.SelectedValue = dt.Rows[0]["PName"].ToString();
            txtFName.Text = dt.Rows[0]["FName"].ToString();
            txtLName.Text = dt.Rows[0]["LName"].ToString();
            if (dt.Rows[0]["Birthdate"] != DBNull.Value)
            {
                ucBirthdate.DateTime = DateTime.Parse(dt.Rows[0]["Birthdate"].ToString());
            }
            if (dt.Rows[0]["Gender"] != DBNull.Value)
            {
                rbGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
            }
            txtNID.Text = dt.Rows[0]["NID"].ToString();
            txtPhone.Text = dt.Rows[0]["Phone"].ToString();
            txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            txtAddressDistrict.Text = dt.Rows[0]["AddressDistrict"].ToString();
            txtAddressPrefecture.Text = dt.Rows[0]["AddressPrefecture"].ToString();
            txtAddressProvince.Text = dt.Rows[0]["AddressProvince"].ToString();
            txtAddressPostal.Text = dt.Rows[0]["AddressPostal"].ToString();
            ucProfile.Text = dt.Rows[0]["Profile"].ToString();
            ucSignature.Text = dt.Rows[0]["Signature"].ToString();
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
        var photoName = "";
        #endregion
        #region Procedure
        try
        {
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
                }
                #endregion
                if (clsSQL.Update(tableDefault,
                    new string[,]{
                    {"Photo",(!string.IsNullOrEmpty(photoName)?"'"+pathUpload+photoName+"'":"Photo")},
                    {"UserGroupUID",ddlUserGroup.SelectedItem.Value},
                    {"PName","'"+ddlPName.SelectedItem.Value+"'"},
                    {"FName","'"+txtFName.Text.SQLQueryFilter()+"'"},
                    {"LName","'"+txtLName.Text.SQLQueryFilter()+"'"},
                    {"BirthDate",(ucBirthdate.Text!=""?"'"+ucBirthdate.DateTime.ToString("yyyy-MM-dd")+"'":"null")},
                    {"Gender","'"+rbGender.SelectedItem.Value+"'"},
                    {"NID","'"+txtNID.Text.SQLQueryFilter()+"'"},
                    {"Phone","'"+txtPhone.Text.SQLQueryFilter()+"'"},
                    {"Mobile","'"+txtMobile.Text.SQLQueryFilter()+"'"},
                    {"Email","'"+txtEmail.Text.SQLQueryFilter()+"'"},
                    {"Address","'"+txtAddress.Text.SQLQueryFilter()+"'"},
                    {"AddressDistrict","'"+txtAddressDistrict.Text.SQLQueryFilter()+"'"},
                    {"AddressPrefecture","'"+txtAddressPrefecture.Text.SQLQueryFilter()+"'"},
                    {"AddressProvince","'"+txtAddressProvince.Text.SQLQueryFilter()+"'"},
                    {"AddressPostal","'"+txtAddressPostal.Text.SQLQueryFilter()+"'"},
                    {"Profile","'"+ucProfile.Text.SQLQueryFilter()+"'"},
                    {"Signature","'"+ucSignature.Text.SQLQueryFilter()+"'"},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
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
                    var clsIO = new clsIO();
                    string outErrorMessage;
                    string outFilename;
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
                }
                #endregion
                if (clsSQL.Insert(tableDefault,
                    new string[,]{
                    {"UserGroupUID",ddlUserGroup.SelectedItem.Value},
                    {"[Username]","'"+txtUsername.Text.SQLQueryFilter()+"'"},
                    {"Password","'"+clsSecurity.Encrypt(txtPassword.Text)+"'"},
                    {"PName","'"+ddlPName.SelectedItem.Value+"'"},
                    {"FName","'"+txtFName.Text.SQLQueryFilter()+"'"},
                    {"LName","'"+txtLName.Text.SQLQueryFilter()+"'"},
                    {"BirthDate",(ucBirthdate.Text!=""?"'"+ucBirthdate.DateTime.ToString("yyyy-MM-dd")+"'":"null")},
                    {"Gender","'"+rbGender.SelectedItem.Value+"'"},
                    {"NID","'"+txtNID.Text.SQLQueryFilter()+"'"},
                    {"Phone","'"+txtPhone.Text.SQLQueryFilter()+"'"},
                    {"Mobile","'"+txtMobile.Text.SQLQueryFilter()+"'"},
                    {"Email","'"+txtEmail.Text.SQLQueryFilter()+"'"},
                    {"[Address]","'"+txtAddress.Text.SQLQueryFilter()+"'"},
                    {"AddressDistrict","'"+txtAddressDistrict.Text.SQLQueryFilter()+"'"},
                    {"AddressPrefecture","'"+txtAddressPrefecture.Text.SQLQueryFilter()+"'"},
                    {"AddressProvince","'"+txtAddressProvince.Text.SQLQueryFilter()+"'"},
                    {"AddressPostal","'"+txtAddressPostal.Text.SQLQueryFilter()+"'"},
                    {"Profile","'"+ucProfile.Text.SQLQueryFilter()+"'"},
                    {"Signature","'"+ucSignature.Text.SQLQueryFilter()+"'"},
                    {"CUser","'" + clsSecurity.LoginUID + "'"},
                    {"CWhen",functionGetDate},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen",functionGetDate},
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
        }
        catch(Exception ex)
        {
            lblSQL.Text = "<div style='color:red;'>เกิดข้อผิดพลาด : " + ex.Message + "</div>";
            lblSQL.Focus();
        }
        #endregion
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        ucColorBox1.Close();
    }
    private void UserGroupBuilder()
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("UID,");
        strSQL.Append("Name ");
        strSQL.Append("FROM ");
        strSQL.Append("P5_UserGroup ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort ASC;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlUserGroup.DataSource = dt;
            ddlUserGroup.DataValueField = "UID";
            ddlUserGroup.DataTextField = "Name";
            ddlUserGroup.DataBind();
        }
        #endregion
    }
}