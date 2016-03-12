using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class UserControl_ucDateTime_ucDateTimeFlat : System.Web.UI.UserControl
{
    #region Example
    /*
    http://xdsoft.net/jqplugins/datetimepicker
    <uc2:ucDateTimeFlat ID="ucDateTimeFlat1" runat="server" 
            EnableDatePicker="true" 
            EnableTimePicker="true" 
            Format="yyyy-MM-dd HH:mm" 
            Step="5" 
            MinDate="2014-02-01" MaxDate="2014-03-05" 
            EnableMask="true" 
            Lang="th" 
            EnableInline="true"/>
    
    Response.Write(ucDateTimeFlat1.DateTime.ToString("dd/MM/yyyy HH:mm"));
    Response.Write(ucDateTimeFlat1.Text);
    
    <uc1:ucDateTimeFlat runat="server" ID="ucActiveTo" PlaceHolder="วันที่สิ้นสิ้นแสดงผล"/>
    */
    #endregion
    #region Property
    private DateTime _dateTime;
    public DateTime DateTime
    {
        get
        {
            if (!string.IsNullOrEmpty(txtDateTime.Text.Trim()))
            {
                try
                {
                    _dateTime = DateTime.Parse(txtDateTime.Text.Trim());
                }
                catch (Exception ex)
                {
                    
                }
            }
            return _dateTime;
        }
        set
        {
            _dateTime = value;
            txtDateTime.Text = _enableTimePicker ? _dateTime.ToString("yyyy-MM-dd HH:mm") : _dateTime.ToString("yyyy-MM-dd");
        }
    }
    private string _text;
    public string Text
    {
        get
        {
            _text = txtDateTime.Text.Trim();
            return _text;
        }
        set
        {
            _text = value;
            txtDateTime.Text = _text;
        }
    }
    private bool _validateRequire = false;
    public bool ValidateRequire
    {
        get { return _validateRequire; }
        set { _validateRequire = value; }
    }
    private string _validateGroup;
    public string ValidateGroup
    {
        get { return _validateGroup; }
        set { _validateGroup = value; }
    }
    private string _errorMessage;
    public string ErrorMessage
    {
        get { return _errorMessage; }
        set { _errorMessage = value; }
    }
    private string _minDate = DateTime.Now.AddYears(-60).ToString("yyyy") + "-01-01";
    public string MinDate
    {
        get { return _minDate; }
        set { _minDate = value; }
    }
    private string _maxDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
    public string MaxDate
    {
        get { return _maxDate; }
        set { _maxDate = value; }
    }
    private bool _enableDatePicker = true;
    public bool EnableDatePicker
    {
        get { return _enableDatePicker; }
        set { _enableDatePicker = value; }
    }
    private bool _enableTimePicker = true;
    public bool EnableTimePicker
    {
        get { return _enableTimePicker; }
        set { _enableTimePicker = value; }
    }
    private string _format;
    public string Format
    {
        get
        {
            #region Condition
            if (string.IsNullOrEmpty(_format))
            {
                if (_enableDatePicker == true && _enableTimePicker == true)
                {
                    _format = "Y-m-d H:i";
                }
                else if (_enableDatePicker == true && _enableTimePicker == false)
                {
                    _format = "Y-m-d";
                }
                else if (_enableDatePicker == false && _enableTimePicker == true)
                {
                    _format = "H:i";
                }
                else
                {
                    _format = "Y-m-d H:i";
                }
            }
            #endregion
            return _format;
        }
        set { _format = FormatConverter(value); }
    }
    private int _step = 10;
    public int Step
    {
        get { return _step; }
        set { _step = value; }
    }
    private bool _enableMask=false;
    public bool EnableMask
    {
        get { return _enableMask; }
        set { _enableMask = value; }
    }
    private string _lang="th";
    public string Lang
    {
        get { return _lang; }
        set { _lang = value; }
    }
    private bool _enableInline=false;
    public bool EnableInline
    {
        get { return _enableInline; }
        set { _enableInline = value; }
    }
    private int _width=140;
    public int Width
    {
        get { return _width; }
        set { _width = value; }
    }
    private string _placeHolder = "";
    public string PlaceHolder
    {
        get { return _placeHolder; }
        set
        {
            _placeHolder = value;
            txtDateTime.Attributes.Add("placeholder", _placeHolder);
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        vldDateTime.Enabled = _validateRequire;
        if (!string.IsNullOrEmpty(_validateGroup))
        {
            vldDateTime.ValidationGroup = _validateGroup;
        }

        if (!string.IsNullOrEmpty(_errorMessage))
        {
            vldDateTime.ErrorMessage = _errorMessage;
        }
    }

    private string FormatConverter(string format)
    {
        //"Y-m-d H:i" = "yyyy-MM-dd HH:mm"
        string rtnValue = format.Trim();

        rtnValue = rtnValue.Replace("yyyy", "Y");
        rtnValue = rtnValue.Replace("MM", "m");
        rtnValue = rtnValue.Replace("dd", "d");
        rtnValue = rtnValue.Replace("HH", "H");
        rtnValue = rtnValue.Replace("mm", "i");

        return rtnValue;
    }

    private string FormatReconverter(string format)
    {
        //"Y-m-d H:i" = "yyyy-MM-dd HH:mm"
        string rtnValue = format.Trim();

        rtnValue = rtnValue.Replace("Y", "yyyy");
        rtnValue = rtnValue.Replace("m", "MM");
        rtnValue = rtnValue.Replace("d", "dd");
        rtnValue = rtnValue.Replace("H", "HH");
        rtnValue = rtnValue.Replace("i", "mm");

        return rtnValue;
    }
}