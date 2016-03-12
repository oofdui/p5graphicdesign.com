using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class ucContent : System.Web.UI.UserControl
{
    #region DatabaseCreate
    /*MySQL
    CREATE TABLE Content(
        UID INT NOT NULL AUTO_INCREMENT,
        Name VARCHAR(100) NOT NULL,
        Detail VARCHAR(200),
        Content TEXT,
        CWhen DATETIME NOT NULL,
        CUser INT NOT NULL,
        MWhen DATETIME NOT NULL,
        MUser INT NOT NULL,
        Sort INT NOT NULL,
        StatusFlag CHAR(1) NOT NULL,
    PRIMARY KEY(UID)
    );
    */
    #endregion
    #region Example
    /*
    <uc2:ucContent ID="ucContentDefault" runat="server" ContentName="AboutHospital" ModalHeight="95%" ModalWidth="80%" ModalRefreshOnClose="false"/>
    */
    #endregion
    #region Property
    private string _contentName;
    public string ContentName
    {
        get { return _contentName; }
        set { _contentName = value; }
    }
    private string _modalWidth="800px";
    public string ModalWidth
    {
        get { return _modalWidth; }
        set { _modalWidth = value; }
    }
    private string _modalHeight="90%";
    public string ModalHeight
    {
        get { return _modalHeight; }
        set { _modalHeight = value; }
    }
    private bool _modalRefreshOnClose=false;
    public bool ModalRefreshOnClose
    {
        get { return _modalRefreshOnClose; }
        set { _modalRefreshOnClose = value; }
    }
    #endregion
    #region Database Config
    string parameterChar = "?"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.MySQL;
    string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !string.IsNullOrEmpty(_contentName))
        {
            ContentBuilder();
        }
    }
    private void ContentBuilder()
    {
        #region Variable
        var strSQL = new StringBuilder();
        var strScript = new StringBuilder();
        var dt = new DataTable();
        var dtIndex=0;

        var clsSQL = new clsSQL(clsGlobal.dbType,clsGlobal.cs);
        var clsSecurity = new clsSecurity();
        #endregion
        #region Procedure
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Content.UID,");
        strSQL.Append("Content.Name,");
        strSQL.Append("Content.Detail,");
        strSQL.Append("Content.Content ");
        strSQL.Append("FROM ");
        strSQL.Append("Content ");
        strSQL.Append("WHERE ");
        strSQL.Append("Content.StatusFlag='A' ");
        strSQL.Append("AND Content.Name='"+_contentName.Trim()+"';");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            strScript.Append("<div class='" + (clsSecurity.LoginChecker("Admin") ? "dvContent" : "dvContentNormal") + "'>");
            #region Default Builder
            if (dt.Rows[0]["Content"] != DBNull.Value)
            {
                strScript.Append(dt.Rows[0]["Content"].ToString());
            }
            #endregion
            #region Admin Builder
            if (clsSecurity.LoginChecker("Admin"))
            {
                strScript.Append("<div class='dvContentMenu'>");
                strScript.Append("<a href='/Management/ContentManage.aspx?id="+dt.Rows[dtIndex]["UID"].ToString()+"&command=edit' ");
                strScript.Append("title='แก้ไขข้อมูล' ");
                if (_modalRefreshOnClose)
                {
                    strScript.Append("class='cbIFrameRefreshOnClose'");
                }
                else
                {
                    strScript.Append("class='cbIFrame'");
                }
                strScript.Append(">");
                strScript.Append("<span class='Icon16 Edit' />");
                strScript.Append("</a>");
                strScript.Append("</div>");
            }
            #endregion
            strScript.Append("</div>");
            lblContent.Text = strScript.ToString();
        }
        #endregion
    }
}