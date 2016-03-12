using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class ucColorBox : System.Web.UI.UserControl
{
    #region Example
    /*
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="MasterPage" ColorBoxIframeName="cbIframe" ColorBoxIframeRefreshOnCloseName="cbIframeRefresh" ColorBoxPhotoName="cbPhoto"/>
    */
    /* Inline
    <a href="Default.aspx" class="cbIframe">ColorBoxIFrame</a>
    <a href="Default.aspx" class="cbIframeRefresh">ColorBoxIFrameRefreshOnClose</a>
    <a href="Images/Image1.png" class="cbPhoto">Image1</a>
    /*
    /* Code Behind
        ucColorBox1.Redirect(
            "/RedirectTest.aspx",
            Header:"ดำเนินการเสร็จสิ้น",
            Message:"กรุณารอสักครู่ ระบบจะพาคุณไปยังหน้าต่อไป",
            Seconds:"5",
            Width:"300px",
            Height:"230px",
            PreloaderImage:ucColorBox.Preloaders.Balloon
        );
    */
    /* Code Behind
        ucColorBox1.Alert(
            "ข้อความหัวเรื่อง",
            "รายละเอียดของการเตือน",
            "300px",
            "150px",
            AlertImage: ucColorBox.Alerts.Warn
        );
    */
    /* Code Behind
        ucColorBox1.IFrame("Default.aspx","500px","500px",false);
    */
    #endregion

    #region Public Variable
    public enum Preloaders
    {
        None,
        Google1,
        Google2,
        Google3,
        Google4,
        Amorphous,
        Balloon,
        Moses,
        Clock,
        Round8,
        Recycle,
        WaterFlow
    }
    public enum Alerts
    {
        None,
        Alert,
        Fail,
        Help,
        Info,
        Info2,
        Success,
        Tips,
        Warn,
        Warn2
    }
    public string url;
    public string seconds = "5";
    public string preloader = Preloaders.Clock.ToString();
    public string alert = Alerts.Info.ToString();
    public string alertHeader;
    public string alertMessage;
    public string redirectHeader;
    public string redirectMessage;
    public string refreshOnClose = "";
    public string widthAdd = "80";
    public string heightAdd = "80";//Original=30 but not work for multiple use in MasterPage and ChildPage
    #endregion
    #region Property
    private string _uid="";
    public string UID
    {
        get { return _uid; }
        set { _uid = value; }
    }
    private string _colorBoxIframeName = "ColorBoxIFrame";
    public string ColorBoxIframeName
    {
        get { return _colorBoxIframeName; }
        set { _colorBoxIframeName = value; }
    }
    private string _colorBoxIframeRefreshOnCloseName = "ColorBoxIFrameRefreshOnClose";
    public string ColorBoxIframeRefreshOnCloseName
    {
        get { return _colorBoxIframeRefreshOnCloseName; }
        set { _colorBoxIframeRefreshOnCloseName = value; }
    }
    private string _colorBoxPhotoName="ColorBoxPhoto";
    public string ColorBoxPhotoName
    {
        get { return _colorBoxPhotoName; }
        set { _colorBoxPhotoName = value; }
    }
    private string _width="80%";
    public string Width
    {
        get { return _width; }
        set { _width = value; }
    }
    private string _height="80%";
    public string Height
    {
        get { return _height; }
        set { _height = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    /// <summary>
    /// แสดง ColorBox แบบหน่วงเวลาเพื่อพาไปยังหน้าต่อไป
    /// </summary>
    /// <param name="Url">URL หน้าที่ให้พาไป</param>
    /// <param name="Header">ข้อความ</param>
    /// <param name="Message">ข้อความ</param>
    /// <param name="Seconds">หน่วงเวลา หน่วยวินาที</param>
    /// <param name="Width">กว้าง</param>
    /// <param name="Height">สูง</param>
    /// <param name="PreloaderImage">ภาพ</param>
    public void Redirect(string Url, string Header = "ดำเนินการเสร็จสิ้น", string Message = "กรุณารอสักครู่ ระบบจะพาคุณไปยังหน้าต่อไป", string Seconds = "5", string Width = "inherit", string Height = "inherit", Preloaders PreloaderImage = Preloaders.Clock)
    {
        #region Variable
        redirectHeader = Header;
        redirectMessage = Message;
        url = Url;
        seconds = Seconds;
        _width = Width;
        _height = Height;
        if (PreloaderImage != Preloaders.None)
        {
            preloader = "<img src='" + ResolveClientUrl("Images/Preloader/" + PreloaderImage.ToString() + ".gif") + "' alt='Redirect'/>";
        }
        else
        {
            preloader = "";
        }
        #endregion

        if (Width != "inherit" || Height != "inherit")
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(),
                "ColorBoxRedirect" + _uid + "",
                "CountDown" + _uid + "();ColorBoxRedirect" + _uid + "();",
                true);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(),
                "ColorBoxRedirect" + _uid + "",
                "CountDown" + _uid + "();ColorBoxRedirect" + _uid + "();ColorBoxRedirectResize" + _uid + "();",
                true);
        }
    }

    /// <summary>
    /// แสดง ColorBox เป็นกล่องข้อความเตือน
    /// </summary>
    /// <param name="Header">ข้อความ</param>
    /// <param name="Message">ข้อความ</param>
    /// <param name="Width">กว้าง</param>
    /// <param name="Height">สูง</param>
    /// <param name="AlertImage">ภาพ</param>
    public void Alert(string Header = "ข้อความเตือน", string Message = "โปรดกรอกรายละเอียด", string Width = "inherit", string Height = "inherit", Alerts AlertImage = Alerts.Info)
    {
        #region Variable
        alertHeader = Header;
        alertMessage = Message;
        _width = Width;
        _height = Height;
        if (AlertImage != Alerts.None)
        {
            alert = "<img src='" + ResolveClientUrl("Images/Alert/" + AlertImage.ToString() + ".png") + "' alt='Alert'/>";
        }
        else
        {
            alert = "";
        }
        #endregion

        if (Width != "inherit" || Height != "inherit")
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(),
                "ColorBoxAlert" + _uid + "",
                "ColorBoxAlert" + _uid + "();",
                true);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(),
                "ColorBoxAlert" + _uid + "",
                "ColorBoxAlert" + _uid + "();ColorBoxAlertResize" + _uid + "();",
                true);
        }
    }

    /// <summary>
    /// แสดง ColorBox จาก URL ที่กำหนดด้วย IFrame
    /// </summary>
    /// <param name="Url">URL ของเว็บที่ให้แสดง</param>
    /// <param name="Width">กว้าง</param>
    /// <param name="Height">สูง</param>
    /// <param name="RefreshOnClose">true=Refresh OnClosed , false=Nothing</param>
    public void IFrame(string Url,string Width = "inherit", string Height = "inherit",bool RefreshOnClose=true)
    {
        #region Variable
        url = Url;
        _width = Width;
        _height = Height;
        if (RefreshOnClose)
        {
            refreshOnClose = ",onClosed:function(){location.reload(true);}";
        }
        #endregion

        Page.ClientScript.RegisterStartupScript(Page.GetType(),
            "ColorBoxIFrame" + _uid + "",
            "ColorBoxIFrame" + _uid + "();",
            true);
    }

    /// <summary>
    /// ปิด ColorBox
    /// </summary>
    public void Close()
    {
        Page.ClientScript.RegisterStartupScript(
            Page.GetType(), 
            "closeWindow", 
            "parent.$.colorbox.close();",
            true);
    }

    /// <summary>
    /// Reload หน้าหลักที่เรียก ColorBox
    /// </summary>
    public void ReloadParent()
    {
        Page.ClientScript.RegisterStartupScript(
            Page.GetType(), 
            "reloadWindow",
            "window.parent.location.reload();",
            true);
    }

    /// <summary>
    /// ปรับขนาด ColorBox โดยอ้างอิงจากขนาดเพจนั้นๆ
    /// </summary>
    /// <param name="PixelAdd">ขนาดที่ต้องการให้เพิ่มมากกว่าขนาดเพจ (หน่วย Pixel)</param>
    /// <example>
    /// //ไว้บริเวณ PageLoad
    /// ucColorBox1.SizeChange("10");
    /// </example>
    public void SizeChange(string PixelAdd = "10")
    {
        StringBuilder strScript = new StringBuilder();

        #region Script Builder
        strScript.Append("$(document).ready(function () {");
        strScript.Append("parent.$.colorbox.resize({");
        strScript.Append("innerWidth: $('body').width() + " + PixelAdd + ",");
        strScript.Append("innerHeight: $('body').height() + " + PixelAdd + "");
        strScript.Append("});");
        strScript.Append("});");
        #endregion

        Page.ClientScript.RegisterStartupScript(
            Page.GetType(),
            "SizeChange",
            strScript.ToString(),
            true);
    }
}