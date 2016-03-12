using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class ucTextEditorPhotoUpload : System.Web.UI.Page
{
    public int maxWidth = 0;
    public int maxHeight = 0;
    public int maxSize = 0;
    private string watermark = "";
    private string pathUpload = "Upload/";
    private string prefixName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Variable Assign
        if (Request.QueryString["MaxWidth"] != null)
        {
            if (Request.QueryString["MaxWidth"].ToString() != "")
            {
                maxWidth = int.Parse(Request.QueryString["MaxWidth"].ToString());
            }
        }
        if (Request.QueryString["MaxHeight"] != null)
        {
            if (Request.QueryString["MaxHeight"].ToString() != "")
            {
                maxHeight = int.Parse(Request.QueryString["MaxHeight"].ToString());
            }
        }
        if (Request.QueryString["MaxSize"] != null)
        {
            if (Request.QueryString["MaxSize"].ToString() != "")
            {
                maxSize = int.Parse(Request.QueryString["MaxSize"].ToString());
            }
        }
        if (Request.QueryString["Watermark"] != null)
        {
            if (Request.QueryString["Watermark"].ToString() != "")
            {
                watermark = Request.QueryString["Watermark"].ToString();
            }
        }
        if (Request.QueryString["PathUpload"] != null)
        {
            if (Request.QueryString["PathUpload"].ToString() != "")
            {
                pathUpload = Request.QueryString["PathUpload"].ToString();
            }
        }
        if (Request.QueryString["PrefixName"] != null)
        {
            if (Request.QueryString["PrefixName"].ToString() != "")
            {
                prefixName = Request.QueryString["PrefixName"].ToString();
            }
        }
        #endregion

        //pathUpload = this.ResolveClientUrl(pathUpload);
        clsDefault clsDefault = new clsDefault();
        pathUpload = clsDefault.ApplicationPath(pathUpload);
    }

    protected void btInsertImages_Click(object sender, EventArgs e)
    {
        #region Variable
        string strFileName;
        clsIO clsIO = new clsIO();
        string outErrorMessage="";
        string outFileName="";
        #endregion

        #region Data Checker
        if (fuInsertImages.PostedFile.ContentLength > maxSize * 1000)
        {
            lblInsertImages.Text = "<div class='fontWarn'>ขนาดไฟล์ใหญ่เกิน " + maxSize + " KB</div>";
            lblInsertImages.Focus();
            return;
        }
        #endregion

        for (int i = 1; i < 10; i++)
        {
            #region FileName Builder
            strFileName = prefixName +
                DateTime.Now.Year.ToString() +
                DateTime.Now.Month.ToString() +
                DateTime.Now.Day.ToString() +
                DateTime.Now.Hour.ToString() +
                DateTime.Now.Minute.ToString() +
                DateTime.Now.Second.ToString() + "_" + i.ToString();
            #endregion
            #region Upload
            if (!clsIO.FileExist(
                pathUpload + strFileName + fuInsertImages + Path.GetExtension(fuInsertImages.FileName).ToLower(), 
                false))
            {
                if(!clsIO.UploadPhoto(
                    fuInsertImages,
                    pathUpload,
                    strFileName,
                    out outErrorMessage,
                    out outFileName,
                    maxSize,
                    maxWidth,
                    maxHeight,
                    clsIO.ResizeAnchor.middlecenter,
                    clsIO.ResizeMode.max))
                {
                    lblInsertImages.Text = "<div class='fontWarn'>เกิดข้อผิดพลาดขณะอัพโหลดไฟล์ : " + outErrorMessage + "</div>";
                    return;
                }

                #region Show Detail
                /*
                lblInsertImages.Text = "<div style='font-size:9pt;margin:5px 0px 5px 0px;'>อัพโหลดไฟล์เสร็จสิ้น : " +
                    "<span style='' title='Copy ชื่อไฟล์นี้ วางในหน้าแทรกภาพ'>" +
                    "<input id='txtInsertImages' type='text' value='" +
                    pathUpload + strFileName.Trim() +
                    "' style='border:1px solid #dddddd;background-color:#fafafa;width:300px;'/>" +
                    "</span></div>";
                ChangeIFrameHeight(70);
                */
                #endregion

                InsertTinyMCE("<img src=" + pathUpload + outFileName + " />");
                break;
            }
            #endregion
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