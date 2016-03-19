using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class ucLoader : System.Web.UI.UserControl
{
    #region Example
    /*
        <uc1:ucLoader ID="ucLoader1" runat="server" OnClickName="ucOnClick"/>
        <button onclick="ucOnClick('');">Click Loader</button>
     
        <uc1:ucLoader ID="ucLoader1" runat="server" Enable="true" LoaderImage="Moses" BackgroundColor="#FFF" Opacity="0.8"/>
        <uc1:ucLoader ID="ucLoader1" runat="server" Enable="true" LoaderImage="Circle" BackgroundColor="#000" Opacity="0.5"/>
        <uc1:ucLoader ID="ucLoader1" runat="server" Enable="true" LoaderImage="OverlapCircle" BackgroundColor="#e0e0e0" Opacity="0.8"/>
        <uc1:ucLoader ID="ucLoader1" runat="server" Enable="true" LoaderImage="CircleBar" BackgroundColor="#fff" Opacity="0.8"/>
        <uc1:ucLoader ID="ucLoader1" runat="server" Enable="true" LoaderImage="Clock" BackgroundColor="#fff" Opacity="0.8"/>
        <uc1:ucLoader ID="ucLoader1" runat="server" Enable="true" LoaderImage="CircleBold" BackgroundColor="#02c4fc" Opacity="0.6"/>
     
        ifLoaded() = ใช้กรณีนำไประบุกับ onload ใน Control ต่างๆ
        <iframe onload="ifLoaded()" frameborder="0" id="ifDefault" name="ifDefault"  src="Application.aspx"></iframe>
    
        ucLoader('') = ใช้กับ Event onclick ต่างๆ โดยสามารถระบุชื่อ ValidateGroup หรือ ไม่ระบุก็ได้
        <asp:Button ID="btBlack" runat="server" Text="ปุ่มนี้สีดำ" 
            OnClientClick="ucLoader('');" onclick="btBlack_Click"/>
        <input id="Button1" type="button" onclick="ucLoader('');" value="button" />
        <input id="Button1" type="button" onclick="ucLoader('vgDefault');" value="button" />
    */
	/*#### ตัวอย่างกรณีนำไปใช้กับ ASP.NET Button ที่มี ValidateGroup ####
    <asp:Button ID="btSubmit" runat="server" CssClass="Button SaveTH" 
        ValidationGroup="vgSave" onclick="btSubmit_Click" OnClientClick="ucLoader('vgSave');"/>
    */
    #endregion

    #region Property
    public enum LoaderImages
    {
        Circle,
        Moses,
        OverlapCircle,
        CircleBar,
        Clock,
        CircleBold
    }

    private bool _enable = true;
    public bool Enable
    {
        get { return _enable; }
        set { _enable = value; }
    }

    private LoaderImages _loaderImage = LoaderImages.Circle;
    public LoaderImages LoaderImage
    {
        get { return _loaderImage; }
        set { _loaderImage = value; }
    }

    private string _backgroundColor="#000";
    public string BackgroundColor
    {
        get { return _backgroundColor; }
        set { _backgroundColor = value; }
    }

    private double _opacity=0.5;
    public double Opacity
    {
        get { return _opacity; }
        set { _opacity = value; }
    }

    private string _onClickName="ucLoader";
    public string OnClickName
    {
        get { return _onClickName; }
        set { _onClickName = value; }
    }

    #endregion
    #region Variable
    private string _displayDiv = "dvLoader";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (_enable)
        {
            PreLoading();
            ClickLoading();
        }
    }

    private void PreLoading()
    {
        StringBuilder strScript = new StringBuilder();
        clsIO clsIO = new clsIO();
        string loaderImage = "Images/" + _loaderImage.ToString() + ".gif";
        string loaderImageWidth = (clsIO.ImageWidth(ResolveClientUrl(loaderImage)) / 2).ToString();
        string loaderImageHeight = (clsIO.ImageHeight(ResolveClientUrl(loaderImage)) / 2).ToString();

        #region Create Display
        if (!Page.ClientScript.IsStartupScriptRegistered(Page.GetType(), "PreLoading_BindDiv"))
        {
            StringBuilder strDIV = new StringBuilder();

            strDIV.Append("<div id='" + _displayDiv + "' style='text-align:center;position:fixed;left:0;right:0;top:0;bottom:0;width:100%;height:100%;z-index: 99999;overflow:auto;");
            #region Style
            strDIV.Append("background-color:" + _backgroundColor + ";");
            strDIV.Append("filter:alpha(opacity=" + (_opacity * 100).ToString() + ");");
            strDIV.Append("opacity:" + _opacity.ToString() + ";");
            #endregion
            strDIV.Append("'>");
            strDIV.Append("<div style='position:absolute;top: 50%;left: 50%;margin-top:-" + loaderImageHeight + "px;margin-left:-" + loaderImageWidth + "px;'>");
            strDIV.Append("<img src='" + this.ResolveClientUrl(loaderImage) + "' alt=''/>");
            strDIV.Append("</div>");
            strDIV.Append("</div>");

            Page.ClientScript.RegisterClientScriptBlock(
                Page.GetType(),
                "PreLoading_BindDiv",
                strDIV.ToString()
                );
        }
        #endregion

        #region Create JavaScript
        strScript.Append("<script type='text/javascript'>");
        strScript.Append("window.onload = function PreLoading() {");
        strScript.Append("if (document.getElementById)");
        strScript.Append("{");
        strScript.Append("document.getElementById('" + _displayDiv + "').style.visibility='hidden';");
        strScript.Append("}");
        strScript.Append("else");
        strScript.Append("{");
        strScript.Append("if (document.layers)");
        strScript.Append("{");
        strScript.Append("document." + _displayDiv + ".visibility = 'hidden';");
        strScript.Append("}");
        strScript.Append("else");
        strScript.Append("{");
        strScript.Append("document.all." + _displayDiv + ".style.visibility = 'hidden';");
        strScript.Append("}");
        strScript.Append("}");
        strScript.Append("}");
        strScript.Append("</script>");

        if (!Page.ClientScript.IsStartupScriptRegistered(Page.GetType(), "PreLoading_Onload"))
        {
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "PreLoading_Onload", strScript.ToString());
            strScript = null;
        }
        #endregion
    }

    private void ClickLoading()
    {
        StringBuilder strScript = new StringBuilder();

        #region Create JavaScript
        strScript.Append("<script type='text/javascript'>");


        strScript.Append("function ifLoaded(){");
        strScript.Append("document.getElementById('" + _displayDiv + "').style.visibility='hidden';");
        strScript.Append("}");

        strScript.Append("function " + _onClickName + "(strValidationGroup){");
        strScript.Append("if (strValidationGroup != ''){");
        strScript.Append("if(Page_ClientValidate(strValidationGroup)==true){");
        //strScript.Append("alert('เข้าลูปเช็ค Validate: ' + strValidationGroup);");
        strScript.Append("document.getElementById('" + _displayDiv + "').style.visibility='visible';");
        strScript.Append("}");
        strScript.Append("}");
        strScript.Append("else");
        strScript.Append("{");
        //strScript.Append("alert('ไม่เข้าลูปเช็ค Validate' + strValidationGroup);");
        strScript.Append("document.getElementById('" + _displayDiv + "').style.visibility='visible';");
        strScript.Append("}");
        strScript.Append("}");

        strScript.Append("</script>");

        if (!Page.ClientScript.IsStartupScriptRegistered(Page.GetType(), "ClickLoading_Onload"))
        {
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClickLoading_Onload", strScript.ToString());
            strScript = null;
        }
        #endregion
    }
}