using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsLanguage
/// </summary>
public class clsLanguage
{
	public clsLanguage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _cookieName = "language";
    public string CookieName
    {
        get { return _cookieName; }
        set { _cookieName = value; }
    }

    private string _languageDefault = "th-TH";
    public string LanguageDefault
    {
        get { return _languageDefault; }
        set { _languageDefault = value; }
    }

    private string _languageCurrent;
    public string LanguageCurrent
    {
        get
        {
            #region Find Cookie
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[_cookieName];
            if (cookie != null && cookie.Value != null)
            {
                _languageCurrent = cookie.Value;
            }
            else
            {
                _languageCurrent = _languageDefault;
            }
            #endregion
            return _languageCurrent;
        }
    }

}