using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class UserControl_ucLogon_ucLogon : System.Web.UI.UserControl
{
    #region Property
    private string _width = "200px";
    public string Width
    {
        get { return _width; }
        set { _width = value; }
    }
    private string _urlProfile = "/Profile";
    public string UrlProfile
    {
        get { return _urlProfile; }
        set { _urlProfile = value; }
    }
    private string _urlRegister = "/Register";
    public string UrlRegister
    {
        get { return _urlRegister; }
        set { _urlRegister = value; }
    }
    private string _urlManagement = "/Management";
    public string UrlManagement
    {
        get { return _urlManagement; }
        set { _urlManagement = value; }
    }
    private bool _showGroup = false;
    public bool ShowGroup
    {
        get { return _showGroup; }
        set { _showGroup = value; }
    }
    #endregion
    #region GlobalVariable
    public clsSecurity clsSecurity = new clsSecurity();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoginChecker();
        }
    }

    private void LoginChecker()
    {
        #region Login Checker
        if (clsSecurity.LoginChecker())
        {
            pnLogin.Visible = false;
            pnLogout.Visible = true;

            lblUsername.Text = clsSecurity.LoginUsername;

            if (_showGroup)
            {
                lblGroupName.Text="<div title='Group' style='padding:5px;'>"+
                    "<span class='Icon16 Info'></span>"+
                    clsSecurity.LoginGroup+
                    "</div>";
            }
            if (UrlManagement.Trim().Length > 0) 
            { 
                if (clsSecurity.LoginGroup.ToLower()=="admin" || clsSecurity.GetAuthority(clsSecurity.LoginGroupAuthority, "Admin") == "1")
                {
                    lblAuthority.Text = "<div style=''><a href='"+UrlManagement+"'>Management</a></div>";
                }
            }
        }
        else
        {
            pnLogin.Visible = true;
            pnLogout.Visible = false;
        }
        #endregion
    }
    protected void btLogin_Click(object sender, EventArgs e)
    {
        #region Variable
        var clsSQL = new clsSQL();
        var clsDefault = new clsDefault();
        var clsColorBox = new clsColorBox();
        #endregion
        #region Procedure
        if (clsSecurity.LoginChecker(
            clsSQL.CodeFilter(txtUsername.Text),
            clsSQL.CodeFilter(txtPassword.Text),
            cbEnableCookie.Checked))
        {
            //ucColorBox.Redirect(Request.RawUrl, "เข้าสู่ระบบแล้ว");
            Response.Redirect(Request.RawUrl);
        }
        else
        {
            var script = "document.getElementById('dvUCLogon').scrollIntoView(true);";
            Page.ClientScript.RegisterStartupScript(Page.GetType(),
                "ucColorBoxScroller",
                script,
                true);
            ucColorBox.Alert("Login Alert", "ไม่พบข้อมูลที่คุณกรอก", AlertImage: global::ucColorBox.Alerts.Fail);
            lblLogin.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูลที่คุณกรอก", clsDefault.AlertType.Warn);
        }
        #endregion
    }
    protected void btLogout_Click(object sender, EventArgs e)
    {
        clsSecurity.LoginDelete();
        Response.Redirect(Request.RawUrl);
        //Response.Redirect(Request.RawUrl);
    }
}