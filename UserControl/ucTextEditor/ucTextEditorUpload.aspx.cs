using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class UserControl_ucTextEditorUpload : System.Web.UI.Page
{
    public int maxWidth = 800;
    //Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["webboard_maxwidth"]);
    public int maxSize = 1024;
    //Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["webboard_maxsize"]);
    private string webname = System.Configuration.ConfigurationManager.AppSettings["webname"];
    private string pathUpload = "Upload/PhotoInsert/";

    protected void Page_Load(object sender, EventArgs e)
    {
        //pathUpload = this.ResolveClientUrl(pathUpload);
        clsDefault clsDefault = new clsDefault();
        pathUpload = clsDefault.ApplicationPath(pathUpload);
    }

    protected void btInsertImages_Click(object sender, EventArgs e)
    {
        string strFileName;
        int i;
        clsIO clsIO = new clsIO();

        if (fuInsertImages.PostedFile.ContentLength > maxSize * 1000)
        {
            lblInsertImages.Text = "<div style='color:red;'>ขนาดของไฟล์ใหญ่เกินไป ซึ่งต้องไม่เกิน " + maxSize + " KB</div>";
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
                System.IO.Path.GetExtension(fuInsertImages.FileName).ToLower();

            if (!clsIO.FileExist(pathUpload + strFileName, false))
            {
                try
                {
                    fuInsertImages.SaveAs(Server.MapPath(pathUpload + strFileName));
                }
                catch (Exception ex)
                {
                    lblInsertImages.Text = "<div style='color:red;'>เกิดข้อผิดพลาดขณะอัพโหลดไฟล์ : " + ex.Message + "</div>";
                    return;
                }
                clsIO.ImageResize(maxWidth, 0, pathUpload + strFileName, "", webname, 0);

                lblInsertImages.Text = "<div style='font-size:9pt;margin:5px 0px 5px 0px;'>อัพโหลดไฟล์เสร็จสิ้น : " +
                    "<span style='' title='Copy ชื่อไฟล์นี้ วางในหน้าแทรกภาพ'>" +
                    "<input id='txtInsertImages' type='text' value='" +
                    pathUpload + strFileName.Trim() +
                    "' style='border:1px solid #dddddd;background-color:#fafafa;width:300px;'/>" +
                    "</span></div>";
                InsertTinyMCE("<img src=" + pathUpload + strFileName.Trim() + " />");
                ChangeIFrameHeight(70);
                break;
            }
        }
    }

    protected void InsertTinyMCE(string strText)
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        System.Text.StringBuilder strScript = new System.Text.StringBuilder();

        strScript.Append("parent.InsertTinyMCE('" + strText + "');");

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "InsertTinyMCE_Complete"))
        {
            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "InsertTinyMCE_Complete", strScript.ToString(), true);
            strScript = null;
        }
    }

    protected void ChangeIFrameHeight(int height)
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        System.Text.StringBuilder strScript = new System.Text.StringBuilder();

        strScript.Append("parent.document.getElementById('ifPhotoInsert').height = '" + height + "px';");

        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "jsChangeIFrameHeight"))
        {
            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "jsChangeIFrameHeight", strScript.ToString(), true);
            strScript = null;
        }
    }
}