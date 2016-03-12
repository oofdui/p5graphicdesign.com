using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var clsSecurity = new clsSecurity();

            if (clsSecurity.LoginGroup.ToLower() == "admin")
            {
                pnDefault.Visible = true;
                pnLogin.Visible = true;
            }
            else
            {
                pnDefault.Visible = false;
                pnLogin.Visible = true;
            }
        }
    }
}
