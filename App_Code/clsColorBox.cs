using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for clsColorBox
/// </summary>
public class clsColorBox
{
	public clsColorBox()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Close ColorBox
    /// </summary>
    public void Close()
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "ColorBoxClose"))
        {
            currentPage.ClientScript.RegisterClientScriptBlock(
                    currentPage.GetType(),
                    "ColorBoxClose",
                    "parent.$.colorbox.close();",
                    true);
        }
    }

    /// <summary>
    /// Refresh Parent Page
    /// </summary>
    public void Refresh()
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "ColorBoxRefresh"))
        {
            currentPage.ClientScript.RegisterClientScriptBlock(
                    currentPage.GetType(),
                    "ColorBoxRefresh",
                    "window.parent.location.reload(true);",
                    true);
        }
    }

    /// <summary>
    /// Show ColorBox with URL
    /// </summary>
    /// <param name="url">URL ที่ต้องการเรียก</param>
    /// <param name="functionUnique">ชื่อฟังชัน JS ที่สร้างและเรียก กรณีมีหลายตัวต้องตั้งให้ไม่ซ้ำกัน</param>
    /// <param name="width">กว้าง 800 , 100%</param>
    /// <param name="height">สูง 800 , 100%</param>
    /// <param name="follow">true=เปิดฟังชัน Follow คือ ให้เลื่อนดูเพจอื่นในกรุ๊ปได้ด้วย , false=ปิดฟังชัน Follow</param>
    public void Show(string url,string functionUnique="ColorBoxShow",string width="800",string height="95%",bool follow=false,bool iframe=false)
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        #region Regis
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "Regis_" + functionUnique))
        {
            StringBuilder strScript = new System.Text.StringBuilder();

            strScript.Append("function " + functionUnique + "(link) {");
            strScript.Append("    $.colorbox({ iframe: " + iframe.ToString().ToLower() + ", href: link, width: '" + width + "', height: '" + height + "',transition: 'none'" + (!follow ? ",rel:'nofollow'" : "") + " });");
            strScript.Append("}");

            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "Regis_" + functionUnique, strScript.ToString(), true);
        }
        #endregion
        #region Call
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "Call_" + functionUnique))
        {
            currentPage.ClientScript.RegisterStartupScript(
                currentPage.GetType(),
                "Call_" + functionUnique,
                functionUnique + "('" + url + "');",
                true);
        }
        #endregion
    }

    /// <summary>
    /// สั่งให้ ColorBox ที่ตัวเองอยู่ ปรับขนาดให้เท่ากับข้อมูลในหน้าของตนเอง
    /// </summary>
    /// <param name="widthAdd">ความกว้างที่ให้เพิ่มจากข้อมูลที่มี</param>
    /// <param name="heightAdd">ความสูงที่ให้เพิ่มจากข้อมูลที่มี</param>
    /// <example>
    /// var clsColorBox =new clsColorBox();
    /// clsColorBox.Resize();
    /// clsColorBox.Resize("20","20");
    /// </example>
    public void Resize(string widthAdd = "10", string heightAdd = "10")
    {
        System.Web.UI.Page currentPage;
        currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        if (!currentPage.ClientScript.IsStartupScriptRegistered(currentPage.GetType(), "ColorBoxResize"))
        {
            currentPage.ClientScript.RegisterStartupScript(
                    currentPage.GetType(),
                    "ColorBoxResize",
                    "$(document).ready(function () {parent.$.colorbox.resize({innerWidth: $('body').width() + " + widthAdd + ",innerHeight: $('body').height() + " + heightAdd + "});});",
                    true);
        }
    }
}