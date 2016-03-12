using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_User : System.Web.UI.Page
{
    #region GlobalVariable
    public clsDefault clsDefault = new clsDefault();
    private clsSQL clsSQL = new clsSQL(clsGlobal.dbType,clsGlobal.cs);
    private clsSecurity clsSecurity = new clsSecurity();
    private string parameterChar = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "?" : "@");
    private string functionGetDate = (clsGlobal.dbType == clsSQL.DBType.MySQL ? "NOW()" : "GETDATE()");
    public string pathUpload = "/Upload/User/";
    public string tableDefault = "User";
    public string webDefault = "User.aspx";
    public string webManage = "UserManage.aspx";
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
        strSQL.Append("A.UserGroupUID,");
        strSQL.Append("B.Name AS UserGroupName,");
        strSQL.Append("A.UID,");
        strSQL.Append("A.Photo,");
        strSQL.Append("A.Username,");
        strSQL.Append("A.PName,");
        strSQL.Append("A.FName,");
        strSQL.Append("A.LName,");
        strSQL.Append("A.Phone,");
        strSQL.Append("A.Mobile,");
        strSQL.Append("A.Email,");
        strSQL.Append("A.Sort,");
        strSQL.Append("A.StatusFlag ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " A ");
        strSQL.Append("INNER JOIN UserGroup B ");
        strSQL.Append("ON A.UserGroupUID=B.UID ");
        strSQL.Append("ORDER BY B.Sort,A.Sort;");
        #endregion
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
            #region Bind UserGroup
            #region SQL Query
            strSQL.Append("SELECT ");
            strSQL.Append("UID,");
            strSQL.Append("Name ");
            strSQL.Append("FROM ");
            strSQL.Append("UserGroup ");
            strSQL.Append("WHERE ");
            strSQL.Append("StatusFlag='A' ");
            strSQL.Append("ORDER BY ");
            strSQL.Append("Sort;");
            #endregion
            dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { } });
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < gvDefault.Rows.Count; i++)
                {
                    Label lblDGUserGroupUID = (Label)gvDefault.Rows[i].FindControl("lblDGUserGroupUID");
                    DropDownList ddlDGUserGroup = (DropDownList)gvDefault.Rows[i].FindControl("ddlDGUserGroup");

                    if (lblDGUserGroupUID != null && ddlDGUserGroup != null)
                    {
                        ddlDGUserGroup.DataSource = dt;
                        ddlDGUserGroup.DataTextField = "Name";
                        ddlDGUserGroup.DataValueField = "UID";
                        ddlDGUserGroup.DataBind();

                        ddlDGUserGroup.SelectedValue = lblDGUserGroupUID.Text;
                    }
                }
            }
            #endregion
        }
        else
        {
            pnDGHeader.Visible = false;
            lblDG.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูลที่ต้องการ", clsDefault.AlertType.Info);
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
    }
    protected void btDGSubmit_Click(object sender, EventArgs e)
    {
        #region Authentication
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
            DropDownList ddlDGUserGroup = (DropDownList)gvDefault.Rows[i].FindControl("ddlDGUserGroup");
            CheckBox cbDGActive = (CheckBox)gvDefault.Rows[i].FindControl("cbDGActive");

            if (lblDGID != null && ddlDGUserGroup != null && cbDGActive != null)
            {
                #region SQL Query
                strSQL.Append("UPDATE ");
                strSQL.Append(tableDefault+" ");
                strSQL.Append("SET ");
                strSQL.Append("UserGroupUID=" + ddlDGUserGroup.SelectedItem.Value + ",");
                strSQL.Append("StatusFlag='" + (cbDGActive.Checked ? "A" : "I") + "' ");
                strSQL.Append("WHERE ");
                strSQL.Append("UID=" + lblDGID.Text);
                strSQL.Append(";");
                #endregion
            }
        }
        if (clsSQL.Execute(strSQL.ToString()))
        {
            ucColorBox1.Redirect("/Management/User.aspx", "ดำเนินการเสร็จสิ้น", "แก้ไขข้อมูลเสร็จเรียบร้อย");
        }
        else
        {
            ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะบันทึกข้อมูลลงฐานข้อมูล<br/>" + strSQL.ToString(), AlertImage: ucColorBox.Alerts.Fail);
        }
        #endregion
    }
}