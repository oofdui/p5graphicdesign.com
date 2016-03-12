using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucTextEditorAjax : System.Web.UI.UserControl
{
    public int maxWidth = 800;
    //Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["webboard_maxwidth"]);
    public int maxSize = 1024;
    //Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["webboard_maxsize"]);
    private string webname = System.Configuration.ConfigurationManager.AppSettings["webname"];
    private string pathUpload = "Upload/PhotoInsert/";

    protected void Page_Load(object sender, EventArgs e)
    {
        clsDefault clsDefault = new clsDefault();
        pathUpload = clsDefault.ApplicationPath(pathUpload);
    }

    protected void btUpload_Click(object sender, EventArgs e)
    {
        string strFileName;
        int i;
        clsIO clsIO = new clsIO();

        if (!fuUpload.HasFile)
        {
            lblUpload.Text = "โปรดเลือกรูปก่อน";
            return;
        }
        if (fuUpload.PostedFile.ContentLength > maxSize * 1000)
        {
            lblUpload.Text = "<div style='color:red;'>ขนาดของไฟล์ใหญ่เกินไป ซึ่งต้องไม่เกิน " + maxSize + " KB</div>";
            return;
        }

        for (i = 1; i < 10; i++)
        {
            strFileName = DateTime.Now.Year.ToString() +
                DateTime.Now.Month.ToString() +
                DateTime.Now.Day.ToString() +
                DateTime.Now.Hour.ToString() +
                DateTime.Now.Minute.ToString() +
                DateTime.Now.Second.ToString() + "_" + i.ToString() +
                System.IO.Path.GetExtension(fuUpload.FileName).ToLower();

            if (!clsIO.FileExist(pathUpload + strFileName, false))
            {
                try
                {
                    fuUpload.SaveAs(Server.MapPath(pathUpload + strFileName));
                }
                catch (Exception ex)
                {
                    lblUpload.Text = "<div style='color:red;'>เกิดข้อผิดพลาดขณะอัพโหลดไฟล์ : " + ex.Message + "</div>";
                    return;
                }
                clsIO.ImageResize(maxWidth, 0, pathUpload + strFileName, "", webname, 0);

                lblUpload.Text = "<div style='font-size:9pt;margin:5px 0px 5px 0px;'>อัพโหลดไฟล์เสร็จสิ้น : " +
                    "<span style='' title='Copy ชื่อไฟล์นี้ วางในหน้าแทรกภาพ'>" +
                    "<input id='txtInsertImages' type='text' value='" +
                    pathUpload + strFileName.Trim() +
                    "' style='border:1px solid #dddddd;background-color:#fafafa;width:300px;'/>" +
                    "</span></div>";
                InsertTinyMCE("<img src=" + pathUpload + strFileName.Trim() + " />");
                break;
            }
        }
    }

    protected void InsertTinyMCE(string strText)
    {
        #region Variable
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        string strScript;
        #endregion

        #region ScriptBuilder
        strScript = "parent.InsertTinyMCE('" + strText + "');";

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "InsertTinyMCE_Complete"))
        {
            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "InsertTinyMCE_Complete", strScript, true);
            strScript = null;
        }
        #endregion
    }
}