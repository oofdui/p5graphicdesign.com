using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ucTextEditor : System.Web.UI.UserControl
{
    private string _text;
    public string Text
    {
        get
        {
            return txtDetail.Text.Replace("''","'").Replace("'","''");
        }
        set
        {
            _text = value;
            txtDetail.Text = value;
        }
    }

    private int _row=20;
    public int Row
    {
        get { return _row; }
        set { _row = value; }
    }

    private string _width="100%";
    public string Width
    {
        get { return _width; }
        set { _width = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDetail.Rows = _row;
            txtDetail.Attributes.CssStyle.Add("width", _width);
        }
    }
}