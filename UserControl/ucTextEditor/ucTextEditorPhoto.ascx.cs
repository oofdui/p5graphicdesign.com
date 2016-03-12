using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucTextEditorPhoto : System.Web.UI.UserControl
{
    #region Example
    /*
    กำหนดทุกอย่าง และ เปิดให้อัพโหลดรูป
    <uc1:ucTextEditorPhoto ID="ucTextEditorPhoto1" runat="server" 
        PhotoMaxWidth="200" 
        PhotoMaxHeight="200" 
        PhotoMaxSize="1000" 
        PhotoPrefixName="Test" 
        PhotoPathUpload="Upload/PhotoInsert/" 
        PhotoUploadEnable="true" 
        Row="20" 
        Width="100%"/>
    */
    /*
    กำหนดขนาดกว้างรูป
    <uc1:ucTextEditorPhoto ID="ucTextEditorPhoto1" runat="server" 
        PhotoMaxWidth="200" 
        PhotoMaxSize="1000" 
        PhotoPathUpload="Upload/PhotoInsert/" 
        PhotoUploadEnable="true" />
    */
    /*
    ปิด ไม่ให้อัพโหลดรูป
    <uc1:ucTextEditorPhoto ID="ucTextEditorPhoto1" runat="server" 
        PhotoMaxWidth="200" 
        PhotoMaxSize="1000"  
        PhotoUploadEnable="false" />
    */
    #endregion
    #region Property
    private string _Text;
    public string Text
    {
        get
        {
            return txtDetail.Text.Replace("''", "'").Replace("'", "''");
        }
        set
        {
            _Text = value;
            txtDetail.Text = value;
        }
    }
    private int _row = 20;
    public int Row
    {
        get { return _row; }
        set { _row = value; }
    }
    private string _width = "100%";
    public string Width
    {
        get { return _width; }
        set { _width = value; }
    }

    private bool _photoUploadEnable=true;
    public bool PhotoUploadEnable
    {
        get { return _photoUploadEnable; }
        set { _photoUploadEnable = value; }
    }
    private int _photoMaxWidth = 0;
    public int PhotoMaxWidth
    {
        get { return _photoMaxWidth; }
        set { _photoMaxWidth = value; }
    }
    private int _photoMaxHeight = 0;
    public int PhotoMaxHeight
    {
        get { return _photoMaxHeight; }
        set { _photoMaxHeight = value; }
    }
    private int _photoMaxSize = 0;
    public int PhotoMaxSize
    {
        get { return _photoMaxSize; }
        set { _photoMaxSize = value; }
    }
    private string _photoWatermark = "";
    public string PhotoWatermark
    {
        get { return _photoWatermark; }
        set { _photoWatermark = value; }
    }
    private string _photoPathUpload = "Upload/PhotoInsert/";
    public string PhotoPathUpload
    {
        get { return _photoPathUpload; }
        set { _photoPathUpload = value; }
    }
    private string _photoPrefixName="";
    public string PhotoPrefixName
    {
        get { return _photoPrefixName; }
        set { _photoPrefixName = value; }
    }
    #endregion
    #region Global Variable
    public string linkPhotoUpload = "?MaxWidth={0}&MaxHeight={1}&MaxSize={2}&Watermark={3}&PathUpload={4}&PrefixName={5}";
    public string photoUploadStyle = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (_photoUploadEnable)
            {
                linkPhotoUpload = this.ResolveClientUrl("ucTextEditorPhotoUpload.aspx" + string.Format(linkPhotoUpload, _photoMaxWidth, _photoMaxHeight, _photoMaxSize, _photoWatermark, Server.UrlEncode(_photoPathUpload), _photoPrefixName));
                photoUploadStyle = "display:block;";
            }
            else
            {
                linkPhotoUpload = "";
                photoUploadStyle = "display:none;";
            }
            
            txtDetail.Rows = _row;
            txtDetail.Attributes.CssStyle.Add("width", _width);
        }
    }
}