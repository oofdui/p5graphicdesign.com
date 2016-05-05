using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel;
using System.IO;
using System.Data;

public class clsSecurity
{
    #region SQLQuery
    /*SQLServer
    CREATE TABLE UserGroup(
	    UID INT IDENTITY(1,1) NOT NULL ,
	    Icon VARCHAR(100),
	    Name VARCHAR(100) NOT NULL,
	    Detail VARCHAR(100),
	    Authority VARCHAR(100),
	    CWhen DATETIME NOT NULL,
	    CUser INT NOT NULL,
	    MWhen DATETIME NOT NULL,
	    MUser INT NOT NULL,
	    Sort INT NOT NULL,
	    StatusFlag CHAR(1) NOT NULL,
    PRIMARY KEY(UID));

    CREATE TABLE [User](
	    UID INT IDENTITY(1,1) NOT NULL,
	    UserGroupUID INT NOT NULL,
	    Authority VARCHAR(100),
	    Username VARCHAR(100) NOT NULL,
	    Password VARCHAR(100) NOT NULL,
	    Photo VARCHAR(100),
	    PName VARCHAR(100),
	    FName VARCHAR(100),
	    LName VARCHAR(100),
	    BirthDate DATETIME,
	    Gender CHAR(1),
	    NID VARCHAR(13),
	    Phone VARCHAR(100),
	    Mobile VARCHAR(100),
	    Email VARCHAR(100),
	    Address VARCHAR(500),
	    AddressDistrict VARCHAR(100),
	    AddressPrefecture VARCHAR(100),
	    AddressProvince VARCHAR(100),
	    AddressPostal VARCHAR(10),
	    Profile text,
	    Signature text,
	    CWhen DATETIME NOT NULL,
	    CUser INT NOT NULL,
	    MWhen DATETIME NOT NULL,
	    MUser INT NOT NULL,
	    Sort INT NOT NULL,
	    StatusFlag CHAR(1) NOT NULL,
    PRIMARY KEY (UID),
    FOREIGN KEY (UserGroupUID) REFERENCES UserGroup(UID) ON UPDATE CASCADE ON DELETE CASCADE);
    */
    /*MySQL
    CREATE TABLE UserGroup(
	    UID INT NOT NULL AUTO_INCREMENT,
	    Icon VARCHAR(100),
	    Name VARCHAR(100) NOT NULL,
	    Detail VARCHAR(100),
	    Authority VARCHAR(100),
	    CWhen DATETIME NOT NULL,
	    CUser INT NOT NULL,
	    MWhen DATETIME NOT NULL,
	    MUser INT NOT NULL,
	    Sort INT NOT NULL,
	    StatusFlag CHAR(1) NOT NULL,
    PRIMARY KEY(UID));
		CREATE TABLE User(
	    UID INT NOT NULL AUTO_INCREMENT,
	    UserGroupUID INT NOT NULL,
	    Authority VARCHAR(100),
	    Username VARCHAR(100) NOT NULL,
	    Password VARCHAR(100) NOT NULL,
	    Photo VARCHAR(100),
	    PName VARCHAR(100),
	    FName VARCHAR(100),
	    LName VARCHAR(100),
	    BirthDate DATETIME,
	    Gender CHAR(1),
	    NID VARCHAR(13),
	    Phone VARCHAR(100),
	    Mobile VARCHAR(100),
	    Email VARCHAR(100),
	    Address VARCHAR(500),
	    AddressDistrict VARCHAR(100),
	    AddressPrefecture VARCHAR(100),
	    AddressProvince VARCHAR(100),
	    AddressPostal VARCHAR(10),
	    Profile text,
	    Signature text,
	    CWhen DATETIME NOT NULL,
	    CUser INT NOT NULL,
	    MWhen DATETIME NOT NULL,
	    MUser INT NOT NULL,
	    Sort INT NOT NULL,
	    StatusFlag CHAR(1) NOT NULL,
    PRIMARY KEY (UID),
    FOREIGN KEY (UserGroupUID) REFERENCES UserGroup(UID) ON UPDATE CASCADE ON DELETE CASCADE);
    */
    #endregion
    #region clsSecurity Test Section
    /*
    //สร้าง Session แบบไม่ผ่าน Class
    //Session["login"] = "1[,]offduiclub[,]admin";

    //สร้าง Session และ Cookie ผ่านการเช็ค Login
    //clsSecurity.LoginChecker("offduiclub", "offjunior",true);

    //เคลียร์ Session Cookie
    //clsSecurity.LoginDelete();

    //ทดสอบว่ามี Cookie อย่างเดียว
    //clsDefault.CookieCreate("login", "S7yYSLdHFFqDjesHG5c0+g==");

    string outCookie;
    lblDefault.Text += "<br/><b>UID</b>:" + (clsSecurity.LoginUID == "" ? "ว่าง" : clsSecurity.LoginUID);
    lblDefault.Text += "<br/><b>Username</b>:" + (clsSecurity.LoginUsername == "" ? "ว่าง" : clsSecurity.LoginUsername);
    lblDefault.Text += "<br/><b>Group</b>:" + (clsSecurity.LoginGroup == "" ? "ว่าง" : clsSecurity.LoginGroup);
    lblDefault.Text += "<br/><b>LoginChecker()</b>: " + clsSecurity.LoginChecker().ToString();
    lblDefault.Text += "<br/><b>LoginChecker('admin')</b>: " + clsSecurity.LoginChecker("admin").ToString();
    lblDefault.Text += "<br/><b>LoginChecker('member')</b>: " + clsSecurity.LoginChecker("member").ToString();
    lblDefault.Text += "<br/><b>Cookie</b>: " + clsDefault.CookieChecker("login", out outCookie).ToString() + " : " + outCookie + "(Decrypt:"+clsSecurity.Decrypt(outCookie)+")";

    lblDefault.Text += "<br/><br/><b>Old Session</b>: " + (Session["login"] != null ? Session["login"].ToString() : "ไม่มี Session");
    */
    #endregion
    #region Encrypt Decrypt Code
    //private static readonly byte[] _key = { 0xA1, 0xF1, 0xA6, 0xBB, 0xA2, 0x5A, 0x37, 0x6F, 0x81, 0x2E, 0x17, 0x41, 0x72, 0x2C, 0x43, 0x27 };
    //private static readonly byte[] _initVector = { 0xE1, 0xF1, 0xA6, 0xBB, 0xA9, 0x5B, 0x31, 0x2F, 0x81, 0x2E, 0x17, 0x4C, 0xA2, 0x81, 0x53, 0x61 };

    private static readonly byte[] _key = { 0xA2, 0xF2, 0xA7, 0xBB, 0xA3, 0x6A, 0x38, 0x7F, 0x82, 0x3E, 0x18, 0x42, 0x73, 0x3C, 0x44, 0x28 };
    private static readonly byte[] _initVector = { 0xE2, 0xF2, 0xA7, 0xBB, 0xA0, 0x6B, 0x32, 0x3F, 0x82, 0x3E, 0x18, 0x5C, 0xA3, 0x82, 0x54, 0x62 };
    #endregion
    #region Login Session Name and Index
    private string _sessionName = "login";
    private int _sessionUID = 0;
    private int _sessionUsername = 1;
    private int _sessionGroup = 2;
    private int _sessionGroupAuthority = 3;
    private int _sessionUserAuthority = 4;
    private string[] _sessionSeparate = {"[,]"};
    private string _parameterChar="?";
    #endregion

    #region Property
    private string _loginUID;
    public string LoginUID
    {
        get 
        {
            _loginUID = GetLoginSession(_sessionName, _sessionUID);
            return _loginUID; 
        }
        set { _loginUID = value; }
    }

    private string _loginUsername;
    public string LoginUsername
    {
        get 
        {
            _loginUsername = GetLoginSession(_sessionName, _sessionUsername);
            return _loginUsername; 
        }
        set { _loginUsername = value; }
    }

    private string _loginGroup;
    public string LoginGroup
    {
        get 
        {
            _loginGroup = GetLoginSession(_sessionName, _sessionGroup);
            return _loginGroup; 
        }
        set { _loginGroup = value; }
    }

    private string _loginGroupAuthority;
    public string LoginGroupAuthority
    {
        get 
        {
            _loginGroupAuthority = GetLoginSession(_sessionName, _sessionGroupAuthority);
            return _loginGroupAuthority; 
        }
        set { _loginGroupAuthority = value; }
    }

    private string _loginUserAuthority;
    public string LoginUserAuthority
    {
        get 
        {
            _loginUserAuthority = GetLoginSession(_sessionName, _sessionUserAuthority);
            return _loginUserAuthority; 
        }
        set { _loginUserAuthority = value; }
    }

    private clsSQL.DBType _dbType=clsSQL.DBType.SQLServer;
    public clsSQL.DBType DBType
    {
        get { return _dbType; }
        set { _dbType = value; }
    }

    private string _cs="cs";
    public string CS
    {
        get { return _cs; }
        set { _cs = value; }
    }
    #endregion

    public clsSecurity()
    {
        #region Find Parameter Char
        if (_dbType == clsSQL.DBType.MySQL)
        {
            _parameterChar = "?";
        }
        else if (_dbType == clsSQL.DBType.SQLServer)
        {
            _parameterChar = "@";
        }
        #endregion
    }

    /// <summary>
    /// ลบข้อมูล Session และ Cookie
    /// </summary>
    /// <param name="CookieDelete">true = ลบ Cookie ด้วย</param>
    /// <returns></returns>
    public bool LoginDelete(bool CookieDelete=true)
    {
        #region Variable
        var clsDefault = new clsDefault();
        var result = false;
        #endregion
        #region Procedure
        try
        {
            if (System.Web.HttpContext.Current.Session[_sessionName] != null)
            {
                System.Web.HttpContext.Current.Session.Remove(_sessionName);
            }
            if (CookieDelete)
            {
                clsDefault.CookieDelete(_sessionName);
            }

            result = true;
        }
        catch (Exception)
        {
            result = false;
        }
        #endregion
        return result;
    }

    /// <summary>
    /// ใช้ตรวจสอบสถานะสมาชิก จาก Session และ Cookie
    /// </summary>
    /// <param name="GroupName">ชื่อสถานะที่ต้องการตรวจสอบ</param>
    /// <param name="CreateSession">กรณีพบ Cookie ให้สร้าง Session ด้วยเลยไหม</param>
    /// <returns>true = พบข้อมูลการล็อคอิน , false = ไม่พบข้อมูลการล็อคอิน</returns>
    /// <example>
    /// clsSecurity.LoginChecker("admin");
    /// clsSecurity.LoginChecker();
    /// </example>
    public bool LoginChecker(string GroupName = "", bool CreateSession = true)
    {
        #region Variable
        var result = false;
        var clsDefault = new clsDefault();
        var clsSQL = new clsSQL(_dbType,_cs);
        var strSQL = new StringBuilder();
        var strCookie="";
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region Session
        if (HttpContext.Current.Session[_sessionName] != null)
        {
            if (!string.IsNullOrEmpty(GroupName))
            {
                if (GetLoginSession(_sessionName, _sessionGroup).ToLower() == GroupName.ToLower())
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = true;
            }
        }
        #endregion
        #region No Session Check Cookie
        else
        {
            if (clsDefault.CookieChecker(_sessionName, out strCookie))
            {
                strCookie = Decrypt(strCookie);
                #region SQLQuery
                strSQL.Append("SELECT ");
                strSQL.Append("A.UID,");
                strSQL.Append("A.Username,");
                strSQL.Append("B.Name AS UserGroupName,");
                strSQL.Append((_dbType==clsSQL.DBType.MySQL?"IFNULL":"ISNULL")+"(B.Authority,'') AS GroupAuthority,");
                strSQL.Append((_dbType==clsSQL.DBType.MySQL?"IFNULL":"ISNULL")+"(A.Authority,'') AS UserAuthority ");
                strSQL.Append("FROM ");
                strSQL.Append("[User] A ");
                strSQL.Append("INNER JOIN UserGroup B ");
                strSQL.Append("ON A.UserGroupUID=B.UID AND B.StatusFlag='A' ");
                strSQL.Append("WHERE ");
                strSQL.Append("A.UID=" + _parameterChar + "UID ");
                strSQL.Append("AND A.StatusFlag='A'");
                #endregion
                dt = clsSQL.Bind(
                    strSQL.ToString(),
                    new string[,] { { "" + _parameterChar + "UID", strCookie } }
                );
                if (dt != null && dt.Rows.Count > 0)
                {
                    #region FoundData
                    if (!string.IsNullOrEmpty(GroupName))
                    {
                        if (dt.Rows[0]["UserGroupName"].ToString().ToLower() == GroupName.ToLower())
                        {
                            if (CreateSession)
                            {
                                SetLoginSession(
                                    _sessionName,
                                    new string[] { 
                                        strCookie, 
                                        dt.Rows[0]["Username"].ToString(), 
                                        dt.Rows[0]["UserGroupName"].ToString(),
                                        dt.Rows[0]["GroupAuthority"].ToString(),
                                        dt.Rows[0]["UserAuthority"].ToString()
                                    }
                                );
                            }
                            result = true;
                        }
                    }
                    else
                    {
                        if (CreateSession)
                        {
                            SetLoginSession(
                                _sessionName,
                                new string[] { 
                                    strCookie, 
                                    dt.Rows[0]["Username"].ToString(), 
                                    dt.Rows[0]["UserGroupName"].ToString(),
                                    dt.Rows[0]["GroupAuthority"].ToString(),
                                    dt.Rows[0]["UserAuthority"].ToString()
                                }
                            );
                        }
                        result = true;
                    }
                    #endregion
                }
                else
                {
                    LoginDelete();
                }
            }
        }
        #endregion
        #endregion
        return result;
    }

    /// <summary>
    /// ใช้ตรวจสอบ Username Password และสร้าง Session Cookie
    /// </summary>
    /// <param name="Username">Login Username</param>
    /// <param name="Password">Login Password</param>
    /// <param name="CreateCookie">สร้าง Cookie ด้วยไหม</param>
    /// <returns>ผลการล็อคอิน</returns>
    /// <example>
    /// clsSecurity.LoginChecker("offduiclub","off1234",false);
    /// clsSecurity.LoginChecker("offduiclub","off1234");
    /// </example>
    public bool LoginChecker(string Username, string Password, bool CreateCookie = false)
    {
        #region Variable
        var result = false;
        var dt = new DataTable();
        var clsDefault = new clsDefault();
        var clsSQL = new clsSQL(_dbType,_cs);
        var strSQL = new StringBuilder();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("A.UID,");
        strSQL.Append("A.Username,");
        strSQL.Append("B.Name AS UserGroupName,");
        strSQL.Append((_dbType==clsSQL.DBType.MySQL?"IFNULL":"ISNULL")+"(B.Authority,'') AS GroupAuthority,");
        strSQL.Append((_dbType==clsSQL.DBType.MySQL?"IFNULL":"ISNULL")+"(A.Authority,'') AS UserAuthority ");
        strSQL.Append("FROM ");
        strSQL.Append("[User] A ");
        strSQL.Append("INNER JOIN UserGroup B ");
        strSQL.Append("ON A.UserGroupUID=B.UID AND B.StatusFlag='A' ");
        strSQL.Append("WHERE ");
        strSQL.Append("A.Username=" + _parameterChar + "Username ");
        strSQL.Append("AND A.Password=" + _parameterChar + "Password ");
        strSQL.Append("AND A.StatusFlag='A'");
        #endregion
        dt = clsSQL.Bind(
            strSQL.ToString(),
            new string[,] { { "" + _parameterChar + "Username", Username }, { "" + _parameterChar + "Password", Encrypt(Password) } }
        );

        if (dt != null && dt.Rows.Count > 0)
        {
            result = true;

            SetLoginSession(
                _sessionName,
                new string[] { 
                    dt.Rows[0]["UID"].ToString(), 
                    dt.Rows[0]["Username"].ToString(), 
                    dt.Rows[0]["UserGroupName"].ToString(),
                    dt.Rows[0]["GroupAuthority"].ToString(), 
                    dt.Rows[0]["UserAuthority"].ToString()
                }
            );

            if (CreateCookie)
            {
                clsDefault.CookieCreate(_sessionName, Encrypt(dt.Rows[0]["UID"].ToString()));
            }
        }
        #endregion
        return result;
    }

    /// <summary>
    /// ดึงค่า Session จาก Index ที่กำหนด
    /// </summary>
    /// <param name="SessionName">ชื่อ Session ที่ใช้เก็บการล็อคอิน</param>
    /// <param name="ArrayIndex">Index ของตัวแปรที่ต้องการ</param>
    /// <returns></returns>
    private string GetLoginSession(string SessionName, int ArrayIndex)
    {
        #region Variable
        var clsDefault = new clsDefault();
        var clsSQL = new clsSQL(_dbType,_cs);
        var strSQL = new StringBuilder();
        var result = "";
        var strCookie="";
        var dt = new DataTable();
        #endregion
        #region Procedure
        if (System.Web.HttpContext.Current.Session[SessionName] != null)
        {
            #region Find Session Login Value
            string[] arrLogin = System.Web.HttpContext.Current.Session[_sessionName].ToString().Split(_sessionSeparate, StringSplitOptions.None);

            if (ArrayIndex < arrLogin.Count())
            {
                result = arrLogin[ArrayIndex];
            }
            #endregion
        }
        else
        {
            #region No Session
            if (clsDefault.CookieChecker(_sessionName, out strCookie))
            {
                strCookie = Decrypt(strCookie);

                #region SQLQuery
                strSQL.Append("SELECT ");
                strSQL.Append("[User].UID,");
                strSQL.Append("[User].Username,");
                strSQL.Append("UserGroup.Name AS UserGroupName,");
                strSQL.Append("IFNULL(UserGroup.Authority,'') AS GroupAuthority,");
                strSQL.Append("IFNULL([User].Authority,'') AS UserAuthority ");
                strSQL.Append("FROM ");
                strSQL.Append("[User] ");
                strSQL.Append("INNER JOIN UserGroup ");
                strSQL.Append("ON [User].UserGroupUID=UserGroup.UID AND UserGroup.StatusFlag='A' ");
                strSQL.Append("WHERE ");
                strSQL.Append("[User].UID=" + _parameterChar + "UID ");
                strSQL.Append("AND [User].StatusFlag='A'");
                #endregion

                dt = clsSQL.Bind(
                    strSQL.ToString(),
                    new string[,] { { "" + _parameterChar + "UID", strCookie } }
                );

                if (dt != null && dt.Rows.Count > 0)
                {
                    SetLoginSession(
                        _sessionName,
                        new string[] { 
                            strCookie, 
                            dt.Rows[0]["Username"].ToString(), 
                            dt.Rows[0]["UserGroupName"].ToString(),
                            dt.Rows[0]["GroupAuthority"].ToString(), 
                            dt.Rows[0]["UserAuthority"].ToString()
                        }
                    );

                    if (System.Web.HttpContext.Current.Session[_sessionName] != null)
                    {
                        #region Find Session Login Value
                        string[] arrLogin = System.Web.HttpContext.Current.Session[_sessionName].ToString().Split(_sessionSeparate, StringSplitOptions.None);

                        if (ArrayIndex < arrLogin.Count())
                        {
                            result = arrLogin[ArrayIndex];
                        }
                        #endregion
                    }
                }
                else
                {
                    LoginDelete();
                }
            }
            #endregion
        }
        #endregion
        return result;
    }

    /// <summary>
    /// กำหนดค่าให้ Session การล็อคอิน
    /// </summary>
    /// <param name="SessionName">ชื่อ Session ที่ใช้</param>
    /// <param name="SessionValue">ค่าที่จะบันทึก โดยกำหนดเป็น Array</param>
    /// <returns>True=สำเร็จ , False=ไม่สำเร็จ</returns>
    private bool SetLoginSession(string SessionName, string[] SessionValue)
    {
        #region Variable
        var result = false;
        var strSession = new StringBuilder();
        #endregion
        #region Procedure
        try
        {
            if (System.Web.HttpContext.Current.Session[SessionName] != null)
            {
                LoginDelete(false);
            }

            for (int i = 0; i < SessionValue.Count(); i++)
            {
                if (i > 0) strSession.Append(_sessionSeparate[0]);

                strSession.Append(SessionValue[i]);
            }

            System.Web.HttpContext.Current.Session[_sessionName] = strSession.ToString();
            strSession.Length = 0; strSession.Capacity = 0;

            result = true;
        }
        catch (Exception)
        {
            result = false;
        }
        #endregion
        return result;
    }

    /// <summary>
    /// ดึงค่าที่ได้จาก Authority ที่เรากำหนด
    /// </summary>
    /// <param name="Authority">ข้อมูล Authority ทั้งหมด เช่น Admin=1,Manager=0,Enable=1</param>
    /// <param name="Name">ชื่อของค่าที่ต้องการ เช่น Admin หรือ Enable</param>
    /// <returns></returns>
    /// <example>
    /// GetAuthority("Admin=0,Manager=1,Enable=1","Enable"); ได้ค่า 1
    /// GetAuthority(LoginUserAuthority,"Enable");
    /// 
    /// if (clsSecurity.GetAuthority(clsSecurity.LoginGroupAuthority, "Admin") == "1")
    /// {
    ///     lblAuthority.Text = "<div>คุณได้รับสิทธิ์ Admin</div>";
    /// }
    /// else
    /// {
    ///     lblAuthority.Text = "<div>คุณไม่ได้รับสิทธิ์ Admin</div>";
    /// }
    /// </example>
    public string GetAuthority(string Authority,string Name)
    {
        #region Variable
        var result = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(Authority))
        {
            string[] authority = Authority.Split(',');
            if (authority.Length > 0)
            {
                for (int i = 0; i < authority.Length; i++)
                {
                    if (authority[i].Trim() != "")
                    {
                        string[] value = authority[i].Split('=');
                        if (value.Length == 2)
                        {
                            if (value[0].Trim().ToLower() == Name.Trim().ToLower())
                            {
                                result = value[1].Trim();
                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion
        return result;
    }

    /// <summary>
    /// ถอดรหัสข้อมูล
    /// </summary>
    /// <param name="Value">ข้อมูลที่ต้องการให้ถอดรหัส</param>
    /// <returns>ข้อมูลหลังจากถอดรหัส</returns>
    /// <example>
    /// clsSecurity.Decrypt("e0NDKIlUhHF3qcIdkmGpZw==");
    /// </example>
    public string Decrypt(string Value)
    {
        #region Variable
        SymmetricAlgorithm mCSP;
        ICryptoTransform ct = null;
        MemoryStream ms = null;
        CryptoStream cs = null;
        byte[] byt;
        byte[] result;
        #endregion
        #region Procedure
        mCSP = new RijndaelManaged();

        try
        {
            mCSP.Key = _key;
            mCSP.IV = _initVector;
            ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);


            byt = Convert.FromBase64String(Value);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();
            result = ms.ToArray();
        }
        catch
        {
            result = null;
        }
        finally
        {
            if (ct != null)
                ct.Dispose();
            if (ms != null)
                if (ms.CanRead)
                {
                    ms.Dispose();
                }
            if (cs != null)
                if (cs.CanRead)
                {
                    cs.Dispose();
                }
        }
        try
        {
            return ASCIIEncoding.UTF8.GetString(result);
        }
        catch (Exception)
        {
            return "";
        }
        #endregion
    }

    /// <summary>
    /// เข้ารหัสข้อมูล
    /// </summary>
    /// <param name="Password">ข้อมูลที่ต้องการให้เข้ารหัส</param>
    /// <returns>ข้อมูลหลังจากเข้ารหัส</returns>
    /// <example>
    /// clsSecurity.Encrypt("offjunior");
    /// </example>
    public string Encrypt(string Password)
    {
        if (string.IsNullOrEmpty(Password))
            return string.Empty;

        #region Variable
        byte[] Value = Encoding.UTF8.GetBytes(Password);
        SymmetricAlgorithm mCSP = new RijndaelManaged();
        #endregion
        #region Procedure
        mCSP.Key = _key;
        mCSP.IV = _initVector;
        using (ICryptoTransform ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                {
                    cs.Write(Value, 0, Value.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                    try
                    {
                        return Convert.ToBase64String(ms.ToArray());
                    }
                    catch (Exception)
                    {
                        return "";
                    }
                }
            }
        }
        #endregion
    }
}