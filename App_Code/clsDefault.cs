using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for clsDefault
/// </summary>
public class clsDefault
{
    public enum AlertType
    {
        /// <summary>
        /// แสดงการเตือนสีเหลือง
        /// </summary>
        Warn,
        /// <summary>
        /// แสดงการเตือนสีฟ้า
        /// </summary>
        Info,
        /// <summary>
        /// แสดงการเตือนสีเขียว
        /// </summary>
        Success,
        /// <summary>
        /// แสดงการเตือนสีแดง
        /// </summary>
        Fail,
        /// <summary>
        /// แสดงการเตือนสีเหลือง
        /// </summary>
        Tips
    }
    private string csSQL;
    private SqlConnection myConn;

    public clsDefault()
    {

    }

    public void Redirect(string url, string msg, string url_redirect="/Complete.aspx",int time=5,bool fromIFrame=false)
    {
        #region Remark
        /*############################ Example ############################
        clsDefault.Redirect("http://www.goodesign.in.th","กำลังนำไป...");
        clsDefault.Redirect("http://www.goodesign.in.th","กำลังนำไป...",fromIFrame:true);
        #################################################################*/
        #endregion

        if (!string.IsNullOrEmpty(url))
        {
            System.Web.HttpContext.Current.Session["url"] = url;
        }
        if (!string.IsNullOrEmpty(msg))
        {
            System.Web.HttpContext.Current.Session["msg"] = msg;
        }
        System.Web.HttpContext.Current.Session["time"] = time.ToString();

        if (!fromIFrame)
        {
            System.Web.HttpContext.Current.Response.Redirect(url_redirect);
        }
        else
        {
            System.Web.UI.Page currentPage;
            currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "ReloadParent"))
            {
                currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "ReloadParent", "parent.location.href='" + url_redirect + "';", true);
            }
        }
    }

    /// <summary>
    /// Refresh หน้าเว็บปัจจุบัน โดยมี 2 ฟังชันคือ Redirect กลับไปหน้าปัจจุบัน หรือ สั่งให้ Parent Page Refresh
    /// </summary>
    /// <param name="fromIFrame">false=Response.Redirect(RawURL) , true=window.parent.location.reload(true);</param>
    /// <example>
    /// clsDefault.Refresh();
    /// clsDefault.Refresh(true);
    /// </example>
    public void Refresh(bool fromIFrame = false)
    {
        if (!fromIFrame)
        {
            System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.RawUrl);
        }
        else
        {
            System.Web.UI.Page currentPage;
            currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "ReloadParent"))
            {
                currentPage.ClientScript.RegisterClientScriptBlock(
                        currentPage.GetType(),
                        "ReloadParent",
                        "window.parent.location.reload(true);",
                        true);
            }
        }
    }

    public string Right(string strOri, int intLength)
    {
        if (strOri.Length <= intLength)
        {
            return strOri;
        }
        else
        {
            return strOri.Substring(strOri.Length - intLength);
        }
    }

    public string Left(string strOri, int intLength)
    {
        if (strOri.Length <= intLength)
        {
            return strOri;
        }
        else
        {
            return strOri.Substring(0, intLength);
        }
    }

    public bool IsDate(string strDate)
    {
        bool boolCheck = false;
        DateTime dtCheck;
        if (!string.IsNullOrEmpty(strDate))
        {
            boolCheck = DateTime.TryParse(strDate, out dtCheck);
        }
        return boolCheck;
    }

    public string MonthName(int intMonth, string strLang="th")
    {
        #region Remark
        /*############################ Example ############################
        clsDefault.MonthName(12);
        clsDefault.MonthName(12,"en");
        #################################################################*/
        #endregion

        string strReturn = "";
        string[] strMonthName = new string[12] { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
        switch (strLang.ToLower())
        {
            case "en":
                strMonthName = new string[12] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                strReturn = strMonthName[intMonth - 1];
                break;
            default:
                strReturn = strMonthName[intMonth - 1];
                break;
        }
        return strReturn;
    }

    public string DOB(string dt)
    {
        string rtnValue = "";
        int years = -1, months = -1, days = -1;
        DateTime dtBD = DateTime.Parse(dt);
        TimeSpanToDate(DateTime.Now, dtBD, out years, out months, out days);
        rtnValue = years + " ปี " + months + " เดือน " + days.ToString().Replace("-", "") + " วัน ";
        return rtnValue;
    }

    public void TimeSpanToDate(DateTime dtEnd, DateTime dtStart, out int years, out int months, out int days)
    {
        if (dtEnd < dtStart)
        {
            DateTime dtTemp = dtStart;
            dtStart = dtEnd;
            dtEnd = dtTemp;
        }
        months = 12 * (dtEnd.Year - dtStart.Year) + (dtEnd.Month - dtStart.Month);
        if (dtEnd < dtStart)
        {
            months--;
            days = DateTime.DaysInMonth(dtStart.Year, dtStart.Month) - dtStart.Day + dtEnd.Day;
        }
        else
        {
            days = dtEnd.Day - dtStart.Day;
        }
        years = months / 12;
        months -= years * 12;
    }

    public bool Search(string strKeyword, string strFullText)
    {
        #region Remark
        /*############################ Example ############################
        ใช้หาคำ จากประโยคที่กำหนด
        clsDefault.Search("ทดสอบ","ผลทดสอบจากการใช้งานคลาสนี้");   return true
        #################################################################*/
        #endregion

        bool rtnValue = false;
        if (strKeyword != "" && strFullText != "")
        {
            rtnValue = strFullText.Contains(strKeyword);
        }
        return rtnValue;
    }

    public bool Search(string strKeyword, string strFullText, string strSeparate)
    {
        #region Remark
        /*############################ Example ############################
        ใช้หาคำ จากข้อความที่มีเครื่องหมายขั้น (แปลงเป็น Array ก่อน)
        clsDefault.Search("อิอิ", "Test,อิอิ,ไม่อยู่หรอก,หรอ", ",");   return true
        #################################################################*/
        #endregion

        bool rtnValue = false;
        string[] strArray = strFullText.Split(char.Parse(strSeparate));
        foreach (string strTemp in strArray)
        {
            if (strKeyword == strTemp)
            {
                rtnValue = true;
                break;
            }
        }
        return rtnValue;
    }

    public bool Search(string strKeyword, DataTable dt, string strColumns="")
    {
        #region Remark
        /*############################ Example ############################
        ใช้หาคำจากคอลัมที่กำหนดใน DataTable
        clsDefault.Search("คำค้นหา",dt);
        clsDefault.Search("คำค้นหา",dt,"dtColumnName");
        #################################################################*/
        #endregion 

        bool rtnValue = false;
        int i = 0;
        if (dt != null && dt.Rows.Count>0)
        {
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (strColumns != "")
                {
                    if (dt.Rows[i][strColumns].ToString().Trim() == strKeyword)
                    {
                        return true;
                    }
                }
                else
                {
                    if (dt.Rows[i][0].ToString().Trim() == strKeyword)
                    {
                        return true;
                    }
                }
            }
        }
        return rtnValue;
    }

    public bool SessionChecker(string sessionName, string strTrueValue)
    {
        #region Remark
        /*############################ Example ############################
        ตรวจสอบ Session จากชื่อที่กำหนดว่ามีค่าตรงกับค่าที่เราต้องการหรือไม่
        SessionChecker("usern","offduiclub");   return true
        #################################################################*/
        #endregion

        bool rtnValue = false;
        if (HttpContext.Current.Session[sessionName] != null)
        {
            if (HttpContext.Current.Session[sessionName].ToString() == strTrueValue)
            {
                rtnValue = true;
            }
        }
        return rtnValue;
    }

    public bool CookieCreate(string cookieName,string cookieValue, int monthExpire=3)
    {
        #region Remark
        /*############################ Example ############################
        สร้าง Cookie ด้วยชื่อและค่าที่กำหนด พร้อมกำหนดระยะเวลาหมดอายุ
        clsDefault.CookieCreate("usern","offduiclub");
        clsDefault.CookieCreate("usern","offduiclub",10);
        #################################################################*/
        #endregion

        bool rtnValue = false;

        try
        {
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpCookie myCookie;
            myCookie = new HttpCookie(cookieName);
            myCookie.Expires = DateTime.Now.AddMonths(monthExpire);
            myCookie.Value = cookieValue;
            HttpContext.Current.Response.Cookies.Add(myCookie);

            rtnValue = true;
        }
        catch (Exception ex)
        {}

        return rtnValue;
    }

    public bool CookieDelete(string cookieName)
    {
        #region Remark
        /*############################ Example ############################
        สร้าง Cookie ด้วยชื่อและค่าที่กำหนด พร้อมกำหนดระยะเวลาหมดอายุ
        clsDefault.CookieDelete("usern");
        #################################################################*/
        #endregion

        bool rtnValue = false;

        if (HttpContext.Current.Request.Cookies[cookieName] != null)
        {
            HttpCookie myCookie;
            myCookie = new HttpCookie(cookieName);
            myCookie.Expires = DateTime.Now.AddDays(-10);
            HttpContext.Current.Response.Cookies.Add(myCookie);

            rtnValue = true;
        }

        return rtnValue;
    }

    public bool CookieChecker(string cookieName,out string cookieValue)
    {
        #region Remark
        /*############################ Example ############################
        ตรวจสอบ Cookie จากชื่อที่กำหนดว่ามีหรือไม่
        clsDefault.CookieChecker("usern",out strValue);
        #################################################################*/
        #endregion

        bool rtnValue = false; cookieValue = "";

        if (HttpContext.Current.Request.Cookies[cookieName] != null)
        {
            rtnValue = true;
            cookieValue = HttpContext.Current.Request.Cookies[cookieName].Value;
        }

        return rtnValue;
    }

    public string QueryStringChecker(string qsName)
    {
        #region Remark
        /*############################ Example ############################
        ตรวจสอบ QueryString ถ้ามีค่าให้คืนมา ถ้าไม่มีให้คืนค่าว่าง
        clsDefault.QueryStringChecker("id");
        #################################################################*/
        #endregion

        string strReturn = "";

        if (HttpContext.Current.Request.QueryString[qsName] != null)
        {
            strReturn = HttpContext.Current.Request.QueryString[qsName].ToString();
        }

        return strReturn;
    }

    public string QueryStringChecker(string qsName,string strNull)
    {
        #region Remark
        /*############################ Example ############################
        ตรวจสอบ QueryString ถ้ามีค่าให้คืนมา ถ้าไม่มีให้คืนค่าที่เรากำหนด
        clsDefault.QueryStringChecker("id","null");
        #################################################################*/
        #endregion

        string strReturn = strNull;

        if (HttpContext.Current.Request.QueryString[qsName] != null)
        {
            strReturn = HttpContext.Current.Request.QueryString[qsName].ToString();
        }

        return strReturn;
    }

    public string QueryStringRemover(string qsName)
    {
        #region Remark
        /*############################ Example ############################
        ลบตัวแปร QueryString จากชื่อที่กำหนด
        clsDefault.QueryStringRemover("id");
        #################################################################*/
        #endregion

        System.Text.StringBuilder strLink = new System.Text.StringBuilder();
        string strReturn = "";
        string[] strQueryString;
        int i;

        if (HttpContext.Current.Request.QueryString.Count > 0)
        {
            strLink.Append("?");
            strQueryString = HttpContext.Current.Request.QueryString.AllKeys;

            //####### สร้าง QueryString ใหม่ #######
            for (i = 0; i < strQueryString.Length; i++)
            {
                if (qsName != strQueryString[i])
                {
                    strLink.Append(strQueryString[i]);
                    strLink.Append("=");
                    strLink.Append(HttpContext.Current.Request.QueryString[strQueryString[i]].ToString());
                    strLink.Append("&");
                }
            }

            //####### ลบ & ตัวหลังสุด #######
            strReturn = LastStringRemover(strLink.ToString(), "&");
            if (strReturn.Length <= 1) strReturn = "";
        }

        return strReturn;
    }

    public string QueryStringRemover(string[] qsName)
    {
        #region Remark
        /*############################ Example ############################
        ลบตัวแปร QueryString หลายๆตัว จาก Array ที่ส่งเข้ามา
        clsDefault.QueryStringRemover(new string[] { "command", "topic" })
        Output : ถ้า QueryString เดิมเป็นดังนี้ xxx.aspx?id=2&command=delete&topic=1 ผลจะได้เป็น xxx.aspx?id=2
        #################################################################*/
        #endregion

        System.Text.StringBuilder strLink = new System.Text.StringBuilder();
        string strReturn = "";
        string[] strQueryString;
        int i;int j;
        bool boolCheck;

        if (HttpContext.Current.Request.QueryString.Count > 0)
        {
            strLink.Append("?");
            strQueryString = HttpContext.Current.Request.QueryString.AllKeys;

            //####### สร้าง QueryString ใหม่ #######
            for (i = 0; i < strQueryString.Length; i++)
            {
                //####### หาว่าชื่อ QueryString ที่ต้องการลบ ใน Array มีใน QueryString ปัจจุบันหรือไม่ #######
                boolCheck = false;
                for (j = 0; j < qsName.Length; j++)
                {
                    if (qsName[j] != "")
                    {
                        if (qsName[j] == strQueryString[i])
                        {
                            boolCheck = true;
                        }
                    }
                }

                if (!boolCheck)
                {
                    strLink.Append(strQueryString[i]);
                    strLink.Append("=");
                    strLink.Append(HttpContext.Current.Request.QueryString[strQueryString[i]].ToString());
                    strLink.Append("&");
                }
            }

            //####### ลบ & ตัวหลังสุด #######
            strReturn = LastStringRemover(strLink.ToString(), "&");
            if (strReturn.Length <= 1) strReturn = "";
        }

        return strReturn;
    }

    public string QueryStringMerge(string qsName="", string qsValue="")
    {
        #region Remark
        /*############################ Example ############################
        เพิ่ม QueryString โดยเพิ่มจากของเดิมที่มี
        clsDefault.QueryStringMerge("id","99");
        Output : ถ้า QueryString เดิมเป็นดังนี้ xxx.aspx?command=delete&topic=1 ผลจะได้เป็น xxx.aspx?command=delete&topic=1&id=99
        #################################################################*/
        #endregion

        System.Text.StringBuilder strReturn = new System.Text.StringBuilder();
        string[] strQueryString;
        int i;
        bool dupCheck = false;

        if (HttpContext.Current.Request.QueryString.Count > 0)
        {
            strReturn.Append("?");
            strQueryString = HttpContext.Current.Request.QueryString.AllKeys;

            for (i = 0; i <= strQueryString.Length - 1; i++)
            {
                if (qsName == strQueryString[i])
                {
                    strReturn.Append(strQueryString[i] + "=" + qsValue);
                    dupCheck = true;
                }
                else
                {
                    strReturn.Append(strQueryString[i]);
                    strReturn.Append("=");
                    strReturn.Append(HttpContext.Current.Request.QueryString[strQueryString[i]].ToString());
                }

                if (i < strQueryString.Length - 1)
                {
                    strReturn.Append("&");
                }
            }

            if (!dupCheck && !string.IsNullOrEmpty(qsName) && !string.IsNullOrEmpty(qsValue))
            {
                strReturn.Append("&");
                strReturn.Append(qsName + "=" + qsValue);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(qsName) && !string.IsNullOrEmpty(qsValue))
            {
                strReturn.Append("?");
                strReturn.Append(qsName + "=" + qsValue);
            }
        }

        return strReturn.ToString();
    }

    public string QueryStringMerge(string[,] qsNameWithValue)
    {
        #region Remark
        /*############################ Example ############################
        เพิ่ม QueryString โดยเพิ่มจากของเดิมที่มีด้วย Array 2 มิติ
        clsDefault.QueryStringMerge(new string[,] { { "id", "1" }, { "type", "2" },{"group","3"} });
        Output : ถ้า QueryString เดิมเป็นดังนี้ xxx.aspx?id=10 ผลจะได้เป็น xxx.aspx?id=1&type=2&group=3
        #################################################################*/
        #endregion

        System.Text.StringBuilder strReturn = new System.Text.StringBuilder();
        string[] strQueryString;
        int i; int j;
        bool dupCheck = false;

        if (HttpContext.Current.Request.QueryString.Count > 0)
        {
            strReturn.Append("?");
            strQueryString = HttpContext.Current.Request.QueryString.AllKeys;

            for (i = 0; i <= strQueryString.Length - 1; i++)
            {
                dupCheck = false;

                if (i > 0)
                {
                    strReturn.Append("&");
                }

                for (j = 0; j < qsNameWithValue.Length / qsNameWithValue.Rank; j++)
                {
                    if (qsNameWithValue[j, 0] == strQueryString[i])
                    {
                        strReturn.Append(strQueryString[i] + "=" + qsNameWithValue[j, 1]);
                        qsNameWithValue[j, 1] = "[ASSIGNED]";
                        dupCheck = true;
                        break;
                    }
                }

                if (!dupCheck)
                {
                    strReturn.Append(strQueryString[i]);
                    strReturn.Append("=");
                    strReturn.Append(HttpContext.Current.Request.QueryString[strQueryString[i]].ToString());
                }
            }

            for (j = 0; j < qsNameWithValue.Length / qsNameWithValue.Rank; j++)
            {
                if (qsNameWithValue[j, 1] != "[ASSIGNED]")
                {
                    if (Search("?", strReturn.ToString()))
                    {
                        strReturn.Append("&");
                    }
                    else
                    {
                        strReturn.Append("?");
                    }

                    strReturn.Append(qsNameWithValue[j, 0] + "=" + qsNameWithValue[j, 1]);
                }
            }
        }
        else
        {
            strReturn.Append("?");

            for (j = 0; j < qsNameWithValue.Length / qsNameWithValue.Rank; j++)
            {
                if (j > 0)
                {
                    strReturn.Append("&");
                }
                strReturn.Append(qsNameWithValue[j, 0] + "=" + qsNameWithValue[j, 1]);
            }
        }

        return strReturn.ToString();
    }

    public string ConvertByteToString(Object objByte)
    {
        #region Remark
        /*############################ Example ############################
        แปลง Object ที่มีค่าเป็น Byte เป็น String
        clsDefault.ConvertByteToString(obj);
        #################################################################*/
        #endregion

        string strReturn = "";

        if (objByte != null)
        {
            try
            {
                strReturn = Encoding.UTF8.GetString((byte[])objByte);
            }
            catch (Exception ex)
            {
                strReturn = ex.ToString();
            }
        }

        return strReturn;
    }

    /// <summary>
    /// แสดงข้อความเตือนด้วยสีและไอคอน
    /// </summary>
    /// <param name="Message">ข้อความเตือน</param>
    /// <param name="AlertType">ประเภทการเตือน</param>
    /// <param name="PathIcon">พาร์ธที่เก็บไอคอน</param>
    /// <param name="Width">ความกว้างของกรอบเตือน</param>
    /// <returns></returns>
    /// <remarks>2014-03-07</remarks>
    public string AlertMessageColor(string Message, AlertType AlertType = AlertType.Info, string PathIcon = "~/Images/Icon/", string Width = "")
    {
        #region Variable
        StringBuilder strReturn = new StringBuilder();
        System.Web.UI.Page currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        PathIcon = currentPage.ResolveClientUrl(PathIcon);

        string colorBorder = "";
        string colorBackground = "";
        string strHeader = "";
        string iconAlert = "";
        #endregion
        #region AlertType Checker
        switch (AlertType)
        {
            case AlertType.Warn:
                colorBorder = "#FFC237";
                colorBackground = "#FFEAA8";
                strHeader = "WARNING";
                iconAlert = "icWarn.png";
                break;
            case AlertType.Info:
                colorBorder = "#418ACC";
                colorBackground = "#D0E4F4";
                strHeader = "INFORMATION";
                iconAlert = "icInfo.png";
                break;
            case AlertType.Success:
                colorBorder = "#99C600";
                colorBackground = "#EFFFB9";
                strHeader = "SUCCESS";
                iconAlert = "icTrue.png";
                break;
            case AlertType.Fail:
                colorBorder = "#EB5339";
                colorBackground = "#FCCAC2";
                strHeader = "FAILURE";
                iconAlert = "icFalse.png";
                break;
            case AlertType.Tips:
                colorBorder = "#FFC237";
                colorBackground = "#FFEAA8";
                strHeader = "TIPS";
                iconAlert = "icTip.png";
                break;
            default:
                colorBorder = "#418ACC";
                colorBackground = "#D0E4F4";
                strHeader = "INFORMATION";
                iconAlert = "icInfo.png";
                break;
        }
        #endregion
        #region ScriptBuilder
        strReturn.Append("<div style='border:3px solid " + colorBorder +
            ";background-color:" + colorBackground +
            ";margin:10px 0px 10px 0px;padding:15px;text-align:left;width:" + Width + ";'>");
        strReturn.Append("<img src='" + PathIcon + iconAlert + "' title='" + strHeader + "' alt='" + strHeader + "'/>");
        strReturn.Append("<span style='margin-left:10px;'>");
        strReturn.Append("<strong>" + strHeader + " : </strong>");
        strReturn.Append(Message);
        strReturn.Append("</span>");
        strReturn.Append("</div>");
        #endregion

        return strReturn.ToString();
    }

    /// <summary>
    /// แสดงข้อความเตือนด้วยสี
    /// </summary>
    /// <param name="Message">ข้อความเตือน</param>
    /// <param name="AlertType">ประเภทการเตือน</param>
    /// <param name="PathIcon">พาร์ธที่เก็บไอคอน</param>
    /// <param name="Width">ความกว้างของกรอบเตือน</param>
    /// <returns></returns>
    /// <remarks>2014-03-07</remarks>
    public string AlertMessageGray(string Message, AlertType AlertType = AlertType.Info, string PathIcon = "~/Images/Icon/", string Width = "")
    {
        #region Variable
        StringBuilder strReturn = new StringBuilder();
        System.Web.UI.Page currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        PathIcon = currentPage.ResolveClientUrl(PathIcon);

        string strHeader = "";
        string iconAlert = "";
        #endregion
        #region AlertTypeChecker
        switch (AlertType)
        {
            case AlertType.Warn:
                strHeader = "WARNING";
                iconAlert = "icWarn.png";
                break;
            case AlertType.Info:
                strHeader = "INFORMATION";
                iconAlert = "icInfo.png";
                break;
            case AlertType.Success:
                strHeader = "SUCCESS";
                iconAlert = "icTrue.png";
                break;
            case AlertType.Fail:
                strHeader = "FAILURE";
                iconAlert = "icFail.png";
                break;
            case AlertType.Tips:
                strHeader = "TIPS";
                iconAlert = "icTip.png";
                break;
            default:
                strHeader = "INFORMATION";
                iconAlert = "icInfo.png";
                break;
        }
        #endregion
        #region ScriptBuilder
        strReturn.Append("<div style='background-color:#FAFAFA;border:1px solid #DDDDDD;width:" + Width + ";'>");
        strReturn.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
        strReturn.Append("<tr>");
        if (!string.IsNullOrEmpty(PathIcon))
        {
            strReturn.Append("<td width='43px' align='center' valign='middle' style='background-color:#EEEEEE;border-right:1px solid #DDDDDD;padding:5px;'>");
            strReturn.Append("<img src='" + currentPage.ResolveClientUrl(PathIcon) + iconAlert + "' title='" + strHeader + "' alt='" + strHeader + "'/>");
            strReturn.Append("</td>");
        }
        strReturn.Append("<td align='left' valign='middle' style='padding:10px;'>");
        strReturn.Append("<strong>" + strHeader + " : </strong>");
        strReturn.Append(Message);
        strReturn.Append("</td>");
        strReturn.Append("</tr>");
        strReturn.Append("</table>");
        strReturn.Append("</div>");
        #endregion

        return strReturn.ToString();
    }

	public string AlertMessageFlat(string Message, AlertType AlertType = AlertType.Info, string PathIcon = "~/Images/Icon/Notification/", string Width = "")
    {
        #region Variable
        var strReturn = new StringBuilder();
        var currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        PathIcon = currentPage.ResolveClientUrl(PathIcon);
        #endregion
        #region ScriptBuilder
        #region StyleBuilder
        strReturn.Append("<style>");
        strReturn.Append("div.alert-message {");
	    strReturn.Append("display: block;");
	    strReturn.Append("padding: 13px 12px 12px;");
	    strReturn.Append("font-weight: bold;");
	    strReturn.Append("font-size: 14px;");
	    strReturn.Append("color: white;");
	    strReturn.Append("background-color: #2ba6cb;");
	    strReturn.Append("border: 1px solid rgba(0, 0, 0, 0.1);");
	    strReturn.Append("margin-bottom: 12px;margin-top:12px;");
	    strReturn.Append("-webkit-border-radius: 3px;");
	    strReturn.Append("-moz-border-radius: 3px;");
	    strReturn.Append("-ms-border-radius: 3px;");
	    strReturn.Append("-o-border-radius: 3px;");
	    strReturn.Append("border-radius: 3px;");
	    strReturn.Append("text-shadow: 0 -1px rgba(0, 0, 0, 0.3);");
	    strReturn.Append("position: relative;");
        strReturn.Append("text-align: left;");
        strReturn.Append("}");
        strReturn.Append("div.alert-message .box-icon {");
	    strReturn.Append("display: block;");
	    strReturn.Append("float: left;");
        strReturn.Append("background-image: url('" + PathIcon + "icon.png');");
	    strReturn.Append("width: 30px;");
	    strReturn.Append("height: 25px;");
	    strReturn.Append("margin-top: -2px;");
	    strReturn.Append("background-position: -8px -8px;");
        strReturn.Append("}");
        strReturn.Append("div.alert-message p {");
	    strReturn.Append("margin: 0px;");
        strReturn.Append("}");
        strReturn.Append("div.alert-message.success {");
	    strReturn.Append("background-color: #5da423;");
	    strReturn.Append("color: #fff;");
	    strReturn.Append("text-shadow: 0 -1px rgba(0, 0, 0, 0.3);");
        strReturn.Append("}");
        strReturn.Append("div.alert-message.success .box-icon {");
	    strReturn.Append("background-position: -48px -8px;");
        strReturn.Append("}");
        strReturn.Append("div.alert-message.warning {");
	    strReturn.Append("background-color: #e3b000;");
	    strReturn.Append("color: #fff;");
	    strReturn.Append("text-shadow: 0 -1px rgba(0, 0, 0, 0.3);");
        strReturn.Append("}");
        strReturn.Append("div.alert-message.warning .box-icon {");
	    strReturn.Append("background-position: -88px -8px;");
        strReturn.Append("}");
        strReturn.Append("div.alert-message.error {");
	    strReturn.Append("background-color: #c60f13;");
	    strReturn.Append("color: #fff;");
	    strReturn.Append("text-shadow: 0 -1px rgba(0, 0, 0, 0.3);");
        strReturn.Append("}");
        strReturn.Append("div.alert-message.error .box-icon {");
	    strReturn.Append("background-position: -128px -8px;");
        strReturn.Append("}");
        strReturn.Append("div.alert-message a.close {");
	    strReturn.Append("color: #333;");
	    strReturn.Append("position: absolute;");
	    strReturn.Append("right: 4px;");
	    strReturn.Append("top: -1px;");
	    strReturn.Append("font-size: 17px;");
	    strReturn.Append("opacity: 0.2;");
	    strReturn.Append("padding: 4px;");
        strReturn.Append("}");
        strReturn.Append("div.alert-message a.close:hover, div.alert-box a.close:focus {");
	    strReturn.Append("opacity: 0.4;");
        strReturn.Append("}");
        strReturn.Append("</style>");
        #endregion
        #region AlertType Checker
        switch (AlertType)
        {
            case AlertType.Warn:
                strReturn.Append("<div class='alert-message warning'>");
                strReturn.Append("<div class='box-icon'></div>");
                strReturn.Append("<p>" + Message + "<a href='' class='close'>&times;</a>");
                strReturn.Append("</div>");
                break;
            case AlertType.Info:
                strReturn.Append("<div class='alert-message info'>");
                strReturn.Append("<div class='box-icon'></div>");
                strReturn.Append("<p>" + Message + "<a href='' class='close'>&times;</a>");
                strReturn.Append("</div>");
                break;
            case AlertType.Success:
                strReturn.Append("<div class='alert-message success'>");
                strReturn.Append("<div class='box-icon'></div>");
                strReturn.Append("<p>" + Message + "<a href='' class='close'>&times;</a>");
                strReturn.Append("</div>");
                break;
            case AlertType.Fail:
                strReturn.Append("<div class='alert-message error'>");
                strReturn.Append("<div class='box-icon'></div>");
                strReturn.Append("<p>" + Message + "<a href='' class='close'>&times;</a>");
                strReturn.Append("</div>");
                break;
            case AlertType.Tips:
                
                break;
            default:
                strReturn.Append("<div class='alert-message info'>");
                strReturn.Append("<div class='box-icon'></div>");
                strReturn.Append("<p>" + Message + "<a href='' class='close'>&times;</a>");
                strReturn.Append("</div>");
                break;
        }
        #endregion
        #endregion

        return strReturn.ToString();
    }
	
    public string DuplicateChecker(string strInput, string strSeparate)
    {
        #region Remark
        /*############################ Example ############################
        ลบค่าซ้ำๆ จาก Array
        clsDefault.DuplicateChecker("อะไรนะ,ทดสอบ,อะไรนะ",",");
         - Output : อะไรนะ,ทดสอบ
        #################################################################*/
        #endregion
        
        StringBuilder strReturn = new System.Text.StringBuilder();
        string[] strSearch = strInput.Split(char.Parse(strSeparate));
        int i;
        string strTemp = "";

        if (strSearch.Length > 0)
        {
            for (i = 0; i < strSearch.Length; i++)
            {
                if (!string.IsNullOrEmpty(strSearch[i]))
                {
                    if (!Search(strSearch[i], strReturn.ToString(), ";"))
                    {
                        strReturn.Append(strSearch[i]);
                        strReturn.Append(";");
                    }
                }
            }
        }

        strTemp = LastStringRemover(strReturn.ToString(), strSeparate);

        return strTemp;
    }

    public string LastStringRemover(string strInput, string strRemoveWord)
    {
        #region Remark
        /*############################ Example ############################
        ลบพยางค์สุดท้ายของประโยค
        clsDefault.LastStringRemover("ทดสอบลบคำสุดท้าย#","#");
         - Output : ทดสอบลบคำสุดท้าย
        #################################################################*/
        #endregion

        string strReturn = "";

        if (Right(strInput, 1) == strRemoveWord)
        {
            strReturn = Left(strInput, strInput.Length - 1);
        }
        else
        {
            strReturn = strInput;
        }

        return strReturn;
    }

    public DataTable Split(string strInput, string strSeparate)
    {
        #region Remark
        /*############################ Example ############################
        แยก String จากสัญลักษณ์ เก็บลงใน DataTable
        clsDefault.Split("ระยอง(,)จันทบุรี(,)ตราด","(,)");
        #################################################################*/
        #endregion

        string[] split = new string[] { strSeparate };
        string[] arrInput;
        int i;
        DataTable dt = new DataTable();
        DataColumn dt_c = new DataColumn();
        DataRow dt_r;

        dt.Columns.Add(dt_c);
        arrInput = strInput.Split(split,StringSplitOptions.None);

        for (i = 0; i < arrInput.Length; i++)
        {
            if (!string.IsNullOrEmpty(arrInput[i]))
            {
                dt_r = dt.NewRow();
                dt.Rows.Add(dt_r);
                dt.AcceptChanges();
                dt_r[0] = arrInput[i];
            }
        }

        return dt;
    }

    public string[] SplitArray(string strInput, string strSeparate)
    {
        #region Remark
        /*############################ Example ############################
        แยก String จากสัญลักษณ์ เก็บลงใน Array
        clsDefault.SplitArray("ระยอง(,)จันทบุรี(,)ตราด", "(,)");
        #################################################################*/
        #endregion

        string[] split = new string[] { strSeparate };
        string[] arrOutput;
        
        arrOutput = strInput.Split(split, StringSplitOptions.None);

        return arrOutput;
    }

	public bool isMobileBrowser()
    {
        //GETS THE CURRENT USER CONTEXT
        HttpContext context = HttpContext.Current;

        //FIRST TRY BUILT IN ASP.NT CHECK
        if (context.Request.Browser.IsMobileDevice)
        {
            return true;
        }
        //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
        if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
        {
            return true;
        }
        //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
        if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
            context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
        {
            return true;
        }
        //AND FINALLY CHECK THE HTTP_USER_AGENT 
        //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
        if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
        {
            //Create a list of all mobile types
            string[] mobiles =
                new[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone"
                };

            //Loop through each item in the list created above 
            //and check if the header contains that text
            foreach (string s in mobiles)
            {
                if (context.Request.ServerVariables["HTTP_USER_AGENT"].ToLower().Contains(s.ToLower()))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public string HighlightWord(string strFullText, string strHightlightWord, string strSeparate="", string strColor = "#FFFD2B")
    {
        #region Remark
        /*############################ Example ############################
        เปลี่ยน Background ในคำ จากข้อความที่กำหนด ด้วยสีที่กำหนด
        clsDefault.HighlightWord("ทดสอบทำไฮไลท์กับข้อความ","ทดสอบ");
        clsDefault.HighlightWord("ทดสอบทำไฮไลท์กับข้อความ","ทด(,)กับ","(,)");
        
        อธิบาย : HightlightWord(ข้อความเต็ม,ข้อความที่ต้องการทำไฮไลท์,สัญลักษณ์ที่ใช้แบ่งคำ กรณีมีหลายคำ เช่น ช่องว่าง,รหัสสี ถ้าไม่ระบุ เว้นว่างไว้);
        #################################################################*/
        #endregion

        string strReturn;
        DataTable dt;
        int i;

        strReturn = strFullText;
        if (!string.IsNullOrEmpty(strHightlightWord))
        {
            if (!string.IsNullOrEmpty(strSeparate))
            {
                dt = Split(strHightlightWord, strSeparate);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() != "")
                        {
                            strReturn = strReturn.Replace(
                                dt.Rows[i][0].ToString(), 
                                "<span style='background-color:" + strColor + "'>" + dt.Rows[i][0].ToString() + "</span>");
                        }
                    }
                }
            }
            else
            {
                strReturn = strFullText.Replace(strHightlightWord, "<span style='background-color:" + strColor + "'>" + strHightlightWord + "</span>");
            }
        }

        return strReturn;
    }
	
	public string CodeFilter(string strInput)
    {
        #region Remark
        /*############################ Example ############################
        ทำหน้าที่กรองอักขระที่อาจก่อให้เกิดความผิดพลาด เมื่อทำการบันทึกข้อมูลในฐานข้อมูล
        clsDefault.CodeFilter("สวัสดีครับ&nbsp;เราจะมาจัดการโค้ดต่างๆกันนะ เช่น 'โค้ดแบบนี้'");
        
        Output : สวัสดีครับ เราจะมาจัดการโค้ดต่างๆกันนะ เช่น ''โค้ดแบบนี้''
        #################################################################*/
        #endregion

        string strOutput;

        strOutput = strInput.Trim();
        if (!string.IsNullOrEmpty(strOutput))
        {
            strOutput = strOutput.Replace("&nbsp;", " ");
            strOutput = strOutput.Replace("''", "'"); // กันไว้ กรณีที่เรา Replace เป็น '' มาแล้วรอบนึง
            strOutput = strOutput.Replace("'", "''");
        }

        return strOutput;
    }
	
	public void ImportJS(string path)
    {
        #region Remark
        /*############################ Example ############################
        ใช้ Import ไฟล์ JavaScript ผ่าน CodeBehind
        clsDefault.ImportJS("Plugin/jcobbSlider/js/basic-jquery-slider.js");
        #################################################################*/
        #endregion

        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        HtmlGenericControl js = new HtmlGenericControl("script");
        js.Attributes["type"] = "text/javascript";
        js.Attributes["src"] = path;

        currentPage.Page.Header.Controls.Add(js);
    }
	
	public void ImportCSS(string path)
    {
        #region Remark
        /*############################ Example ############################
        ใช้ Import ไฟล์ CSS ผ่าน CodeBehind
        clsDefault.ImportCSS("Plugin/jcobbSlider/css/basic-jquery-slider.css");
        #################################################################*/
        #endregion

        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        HtmlLink htmlLink = new HtmlLink();
        htmlLink.Attributes.Add("rel", "stylesheet");
        htmlLink.Attributes.Add("type", "text/css");
        htmlLink.Href = path;

        currentPage.Page.Header.Controls.Add(htmlLink);
    }
	
	public string InjectionFilter(string strInput)
    {
        #region Remark
        /*############################ Example ############################
        ลบอักขระพิเศษ ที่เสี่ยงต่อการทำ SQL Injection ในเว็บของเรา
        clsDefault.InjjectionFilter("Default.aspx?id=1;Delete FROM member&command=edit");
        
        Output : Default.aspx?id=1DeleteFROMmember&command=edit
        #################################################################*/
        #endregion

        string strOutput = strInput.Trim();
        string[] strReplace = { " ", ";" ,"#","'"};
        int i;

        for (i = 0; i < strReplace.Length; i++)
        {
            strOutput = strOutput.Replace(strReplace[i], "");
        }

        return strOutput;
    }
	
	/// <summary>
    /// ใช้ตรวจสอบค่าของ URL Routing จาก Key ที่เราต้องการ
    /// </summary>
    /// <param name="strKey">ชื่อตัวแปร</param>
    /// <returns>ค่า</returns>
    /// <example>
    /// clsDefault.URLRouting("id");
    /// Input : www.goodesign.in.th/Article/12
    /// Output : 12
    /// </example>
	public string URLRouting(string strKey)
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        string strReturn = "";

        if (currentPage.RouteData.Values.Count > 0)
        {
            if (currentPage.RouteData.Values[strKey] != null)
            {
                strReturn = currentPage.RouteData.Values[strKey].ToString();
            }
        }

        return strReturn;
    }
	
	/// <summary>
    /// กรองข้อความที่ใช้ส่งไปกับ URL ของ URLRouting
    /// </summary>
    /// <param name="RoutingValue">ค่าที่ต้องการให้กรอง</param>
    /// <returns>ค่าที่กรองแล้ว</returns>
    /// <example>
    /// URLRoutingFilter("แผนก อายุรกรรม");
    /// output:"แผนก-อายุรกรรม"
    /// </example>
    public string URLRoutingFilter(object RoutingValue)
    {
        string rtnValue = "";

        #region Replacement
        rtnValue = RoutingValue.ToString();
        rtnValue = rtnValue.Replace(" ", "-");
        rtnValue = rtnValue.Replace("/", "-");
        rtnValue = rtnValue.Replace("'", "");
        rtnValue = rtnValue.Replace('"', '\0');
		rtnValue = rtnValue.Replace("*", "x");
		rtnValue = rtnValue.Replace("%", "percent");
		rtnValue = rtnValue.Replace("%%", "percent");
        #endregion

        return rtnValue;
    }
	
	public string GetDate(object objInput,string strDateFormat="dd/MM/yyyy",string strIfNullShow="")
    {
        #region Remark
        /*############################ Example ############################
        คืนค่าวันที่จาก Object ที่ใช้ใน GridView ตามรูปแบบที่กำหนด
        clsDefault.GetDate(DataBinder.Eval(Container.DataItem,"date"),strIfNullShow:"-")
        clsDefault.GetDate(DataBinder.Eval(Container.DataItem,"date"))
        #################################################################*/
        #endregion

        string strOutput = strIfNullShow;

        if (objInput != null)
        {
            DateTime dt;
            try
            {
                dt = Convert.ToDateTime(objInput);
                strOutput = dt.ToString(strDateFormat);
            }
            catch (Exception ex)
            {
                strOutput = ex.Message;
            }
        }

        return strOutput;
    }
	
	public string ColorRandom()
    {
        #region Remark
        /*############################ Example ############################
        สุ่มรหัสสี
        clsDefault.ColorRandom();
        #################################################################*/
        #endregion

        string rtnColor;

        Random rndColor = new Random();
        int red = rndColor.Next(0, 255);
        int green = rndColor.Next(0, 255);
        int blue = rndColor.Next(0, 255);
        rtnColor = String.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);

        return rtnColor;
    }
	
	public bool CheckBoxChecker(CheckBoxList cb)
    {
        #region Remark
        /*############################ Example ############################
        เช็คว่า CheckBoxList มีการเลือกหรือไม่
        clsDefault.CheckBoxChecker(cbOption);
        #################################################################*/
        #endregion

        bool rtnBool = false;

        if (cb != null && cb.Items.Count > 0)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (cb.Items[i].Selected)
                {
                    rtnBool = true;
                    break;
                }
            }
        }

        return rtnBool;
    }
	
	public bool CheckBoxSelecter(CheckBoxList cb, DataTable dt, string strFieldName="0")
    {
        #region Remark
        /*############################ Example ############################
        Select ค่าตาม DataTable และ ชื่อ Column ที่ส่งเข้ามา
        clsDefault.CheckBoxSelecter(cbOption,dt,"value");
        #################################################################*/
        #endregion

        bool rtnBool = false;
        int i = 0; int j = 0;

        if (cb != null && cb.Items.Count > 0 && dt != null && dt.Rows.Count > 0)
        {
            for (i = 0; i < dt.Rows.Count; i++)
            {
                for (j = 0; j < cb.Items.Count; j++)
                {
                    if (dt.Rows[i][strFieldName].ToString() == cb.Items[j].Value)
                    {
                        cb.Items[j].Selected = true;
                        break;
                    }
                }
            }
        }

        return rtnBool;
    }

    /// <summary>
    /// สร้างพาร์ธที่ใช้อ้างอิงถึงรูทของเว็บไซต์
    /// </summary>
    /// <param name="CombinePath">Sub Directory ที่ต้องการให้เชื่อมต่อ</param>
    /// <returns></returns>
    /// <example>
    /// clsDefault.ApplicationPath("/Upload/PhotoInsert/201422691732_1.jpg")
    /// clsDefault.ApplicationPath("Upload/PhotoInsert/201422691732_1.jpg")
    /// clsDefault.ApplicationPath("Upload/PhotoInsert/")
    /// clsDefault.ApplicationPath("Upload/PhotoInsert")
    /// </example>
    public string ApplicationPath(string CombinePath="")
    {
        #region Variable Declare
        string rtnValue = "";
        #endregion

        rtnValue = HttpContext.Current.Request.ApplicationPath;

        #region Combine Path
        if (!string.IsNullOrEmpty(CombinePath))
        {
            #region StartsWith Checker
            if (CombinePath.StartsWith("/"))
            {
                CombinePath = CombinePath.Substring(1, CombinePath.Length - 1);
            }
            #endregion

            rtnValue = System.IO.Path.Combine(rtnValue, CombinePath);
            rtnValue = rtnValue.Replace(@"\", "/");
        }
        #endregion
        #region EndsWith Checker
        if (string.IsNullOrEmpty(System.IO.Path.GetExtension(rtnValue)))
        {
            if (!rtnValue.EndsWith("/"))
            {
                rtnValue += "/";
            }
        }
        #endregion

        return rtnValue;
    }

    /// <summary>
    /// ดึงค่าใน web.config จากตัวแปรที่กำหนด
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string AppSetting(string name)
    {
        string value = "";

        if (System.Configuration.ConfigurationManager.AppSettings[name] != null)
        {
            value = System.Configuration.ConfigurationManager.AppSettings[name];
        }

        return value;
    }

    /// <summary>
    /// กำหนดค่าใน web.config จากชื่อและตัวแปรที่กำหนด
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public bool AppSetting(string name, string value)
    {
        bool rtnValue = false;

        try
        {
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            config.AppSettings.Settings.Remove(name);
            config.AppSettings.Settings.Add(name, value);
            config.Save();

            rtnValue = true;
        }
        catch (Exception ex)
        {
            
        }

        return rtnValue;
    }
}

public static class clsDefaultExtension
{
    /// <summary>
    /// ตรวจสอบอีเมล์ว่ารูปแบบถูกต้องหรือไม่
    /// </summary>
    /// <param name="email">เช่น off_dui@Hotmail.com</param>
    /// <returns>true=ถูกต้อง , false=ผิด</returns>
    /// <example>
    /// string email = "off_dui.@hotmail.com";
    /// Response.Write(email + ":" + email.IsValidEmailAddress().ToString());
    /// </example>
    public static bool IsValidEmailAddress(this string email)
    {
        string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" +
            @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" +
            @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
        return regex.IsMatch(email);
    }

    /// <summary>
    /// ตรวจสอบเลขที่บัตรประชาชนว่าถูกต้องหรือไม่
    /// </summary>
    /// <param name="idCard">เลขที่บัตรประชาชน มีหรือไม่มี - ก็ได้</param>
    /// <returns>true=ถูก , false=ผิด</returns>
    /// <example>
    /// string idCard = "1529900340799";
    /// string idCard = "15-299003-40799";
    /// Response.Write(idCard + ":" + idCard.IsValidIDCard().ToString());
    /// </example>
    public static bool IsValidIDCard(this string idCard)
    {
        #region Variable
        int pin = 0;
        int j = 13;
        int pin_num = 0;
        bool isValid = true;
        #endregion
        #region Null Checker
        if (idCard.Replace("-", "").Trim() == "")
        {
            isValid = false;
            return isValid;
        }
        else
        {
            idCard = idCard.Replace("-", "").Trim();
        }
        #endregion
        #region ID Checker
        bool ChkPinID = true;
        if (ChkPinID == false) { isValid = false; return isValid; }
        if (idCard.Length == 13)
        {
            for (int i = 0; i < idCard.Length; i++)
            {
                if (i != 12)
                {
                    pin = Convert.ToInt16(idCard.Substring(i, 1)) * j + pin;
                }
                j--;
            }
            pin_num = (11 - (pin % 11)) % 10;
            if (Convert.ToInt16(idCard.Substring(12, 1)) != pin_num)
            {
                isValid = false;
                return isValid;
            }
        }
        else
        {
            isValid = false;
            return isValid;
        }
        #endregion

        return isValid;
    }

    /// <summary>
    /// ตัดข้อความเริ่มจากซ้ายมือไปขวามือตามจำนวนที่ต้องการ เช่น 12345 ตัดซ้าย 2 ได้ 12
    /// </summary>
    /// <param name="text">ข้อความที่ต้องการตัด</param>
    /// <param name="length">จำนวนตัวอักษรที่ต้องการ</param>
    /// <returns>ข้อความที่ต้องการ</returns>
    /// <example>
    /// MessageBox.Show(("12345678901234567890").Left(10));
    /// จะได้ 1234567890
    /// </example>
    public static string Left(this string text, int length)
    {
        #region Variable
        var result = "";
        #endregion
        #region Procedure
        if (text.Trim().Length > length)
        {
            result = text.Substring(0, length);
        }
        #endregion

        return result;
    }

    /// <summary>
    /// ตัดข้อความเริ่มจากขวามือไปซ้ายมือตามจำนวนที่ต้องการ เช่น 12345 ตัดขวา 2 ได้ 45
    /// </summary>
    /// <param name="text">ข้อความที่ต้องการตัด</param>
    /// <param name="length">จำนวนตัวอักษรที่ต้องการ</param>
    /// <returns>ข้อความที่ต้องการ</returns>
    /// <example>
    /// MessageBox.Show(("12345678901234567890").Right(2));
    /// จะได้ 90
    /// </example>
    public static string Right(this string text, int length)
    {
        #region Variable
        var result = "";
        #endregion
        #region Procedure
        if (text.Trim().Length > length)
        {
            result = text.Substring(text.Length - length);
        }
        #endregion

        return result;
    }

    /// <summary>
    /// แปลง Object เป็น Integer
    /// </summary>
    /// <param name="obj">ข้อมูลที่ต้องการแปลง</param>
    /// <returns>null=แปลงไม่ได้ / ไม่ใช่ Integer,Integer=แปลงได้</returns>
    /// <example>
    /// string toINT = "10300";
    /// หรือ
    /// string toINT = "10,300";
    /// Response.Write("<br/>" + toINT + " > " + toINT.ToINT().ToString());
    /// </example>
    public static int? ToINT(this object obj)
    {
        #region Variable
        int _int;
        #endregion
        #region Error Checker
        if (obj == null)
        {
            return null;
        }
        #endregion
        #region TryParse
        if (int.TryParse(obj.ToString().Replace(",", ""), out _int))
        {
            return _int;
        }
        else
        {
            return null;
        }
        #endregion
    }

    /// <summary>
    /// แปลง Object เป็น Double
    /// </summary>
    /// <param name="obj">ข้อมูลที่ต้องการแปลง</param>
    /// <returns>null=แปลงไม่ได้ / ไม่ใช่ Double,Double=แปลงได้</returns>
    /// <example>
    /// string toDouble = "10300.50";
    /// หรือ
    /// string toDouble = "10,300.50";
    /// Response.Write("<br/>" + toDouble + " > " + toDouble.ToDouble().ToString());
    /// </example>
    public static double? ToDouble(this object obj)
    {
        #region Variable
        double _double;
        #endregion
        #region Error Checker
        if (obj == null)
        {
            return null;
        }
        #endregion
        #region TryParse
        if (double.TryParse(obj.ToString().Replace(",", ""), out _double))
        {
            return _double;
        }
        else
        {
            return null;
        }
        #endregion
    }
	
	/// <summary>
    /// แปลงค่า Object เป็นวันที่ในรูปแบบ String ที่กำหนด Format ได้
    /// </summary>
    /// <param name="obj">Object ที่ต้องการแปลง</param>
    /// <param name="format">รูปแบบวันเวลา</param>
    /// <returns>String ที่ได้จากการแปลงวันที่ กรณีแปลงไม่ได้ จะคืนค่าว่าง</returns>
    /// <example>
    /// <%#DataBinder.Eval(Container.DataItem,"CWhen").ToDateTime() %>
    /// </example>
    public static string ToDateTime(this object obj, string format = "dd/MM/yyyy HH:mm")
    {
        #region Variable
        var result = "";
        DateTime dttm;
        #endregion
        #region Procedure
        #region Error Checker
        if (obj == null)
        {
            return "";
        }
        #endregion
        if (DateTime.TryParse(obj.ToString(), out dttm))
        {
            result = dttm.ToString(format);
        }
        else
        {
            result = "";
        }
        #endregion
        return result;
    }

    /// <summary>
    /// แปลงข้อความรูปแบบธรรมดาเป็นคาเมลเคส (nithi.re=Nithi.Re , niThi rERKWICHA=Nithi Rerkwicha
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <example>
    /// "nithi.re testing".ToCamelCase();
    /// =Nithi.Re Testing
    /// </example>
    private static string ToCamelCase(this string text)
    {
        #region Variable
        var result = "";
        #endregion
        #region Procedure
        result = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
        #endregion
        return result;
    }
}