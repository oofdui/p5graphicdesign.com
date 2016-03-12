using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Data;

/// <summary>
/// Summary description for clsMail
/// </summary>
public class clsMail
{
    #region Configuration
    private string _smtpMailHost = System.Configuration.ConfigurationManager.AppSettings["smtpMailHost"];
    public string SmtpMailHost
    {
        get { return _smtpMailHost; }
        set { _smtpMailHost = value; }
    }
    private string _smtpMailUsername = System.Configuration.ConfigurationManager.AppSettings["smtpMailUsername"];
    public string SmtpMailUsername
    {
        get { return _smtpMailUsername; }
        set { _smtpMailUsername = SmtpMailUsername; }
    }
    private string _smtpMailPassword = System.Configuration.ConfigurationManager.AppSettings["smtpMailPassword"];
    public string SmtpMailPassword
    {
        get { return _smtpMailPassword; }
        set { _smtpMailPassword = SmtpMailPassword; }
    }
    private bool _smtpMailAuthentication=true;
    public bool SmtpMailAuthentication
    {
        get { return _smtpMailAuthentication; }
        set { _smtpMailAuthentication = value; }
    }

    #endregion
    public clsMail()
    {
    }
    public clsMail(string smtpMailHost,string smtpMailUsername,string smtpMailPassword,bool smtpMailAuthentication)
    {
        _smtpMailHost = smtpMailHost;
        _smtpMailUsername = smtpMailUsername;
        _smtpMailPassword = smtpMailPassword;
        _smtpMailAuthentication = smtpMailAuthentication;
    }
    public bool Send(string From, string To, string Subject, string Message, out string outMessage, string FromAliasName = "", string Cc = "", string Bcc = "",string Signature="",MailPriority Priority=MailPriority.Normal)
    {
        #region Variable
        MailMessage myMail = new MailMessage();
        StringBuilder strMailBody = new StringBuilder();
        outMessage = "";
        #endregion
        #region DataChecker
        if (string.IsNullOrEmpty(To))
        {
            outMessage = "โปรดระบุเมล์ที่จะส่งหา";
            return false;
        }
        if (string.IsNullOrEmpty(From))
        {
            outMessage = "โปรดระบุเมล์ที่จะใช้ส่ง";
            return false;
        }
        #endregion

        #region Mail Builder
        #region Address
        myMail.From = new MailAddress(From, FromAliasName, System.Text.Encoding.GetEncoding("windows-874"));
        myMail.To.Add(To);
        if (!string.IsNullOrEmpty(Cc))
        {
            myMail.CC.Add(Cc);
        }
        if (!string.IsNullOrEmpty(Bcc))
        {
            myMail.Bcc.Add(Bcc);
        }
        #endregion
        #region Detail
        #region Subject
        if (!string.IsNullOrEmpty(Subject))
        {
            myMail.Subject = Subject;
        }
        else
        {
            myMail.Subject = "";
        }
        #endregion
        #region Body
        strMailBody.Append("<html>");
        strMailBody.Append("<head></head>");
        strMailBody.Append("<body>");
        if (!string.IsNullOrEmpty(Message))
        {
            strMailBody.Append(Message);
        }
        if (!string.IsNullOrEmpty(Signature))
        {
            strMailBody.Append("<hr/>");
            strMailBody.Append(Signature);
        }
        strMailBody.Append("</body>");
        strMailBody.Append("</html>");

        myMail.Body = strMailBody.ToString();
        #endregion
        myMail.Priority = Priority;
        myMail.IsBodyHtml = true;
        myMail.BodyEncoding = System.Text.Encoding.GetEncoding("windows-874");
        #endregion
        #endregion
        #region Server Config
        SmtpClient smtpMail = new SmtpClient();
        if (_smtpMailAuthentication)
        {
            NetworkCredential Auth = new NetworkCredential(_smtpMailUsername, _smtpMailPassword);
            smtpMail.UseDefaultCredentials = false;
            smtpMail.Credentials = Auth;
        }
        else
        {
            smtpMail.UseDefaultCredentials = true;
        }
        smtpMail.Host = _smtpMailHost;
        #endregion
        #region Send Mail
        try
        {
            smtpMail.Send(myMail);
            return true;
        }
        catch (Exception ex)
        {
            outMessage = ex.Message;
            return false;
        }
        #endregion
    }
    /// <summary>
    /// ส่งเมล์โดยใช้ SMTP Server ของ Gmail (เปิดสิทธิ์ที่นี่ https://www.google.com/settings/security/lesssecureapps)
    /// </summary>
    /// <param name="FromMail">ชื่อเมล์ Gmail ที่จะใช้ส่ง</param>
    /// <param name="FromMailPassword">รหัสผ่านของ Gmail นี้</param>
    /// <param name="To">เมล์ที่จะส่งหา</param>
    /// <param name="Subject">หัวข้อ</param>
    /// <param name="Message">ข้อความ</param>
    /// <param name="outMessage">กรณีเกิดข้อผิดพลาดจะคืนค่าที่นี่</param>
    /// <param name="FromAliasName">AliasName ในช่อง From</param>
    /// <param name="Cc"></param>
    /// <param name="Bcc"></param>
    /// <param name="Signature"></param>
    /// <param name="Priority"></param>
    /// <returns>True=สำเร็จ , False=ไม่สำเร็จ</returns>
    /// <example>
    /// var clsMail = new clsMail();
    /// var outMailMessage="";
    /// clsMail.SendByGmail("off.dui@gmail.com", "offjuniorgma", "off_dui@hotmail.com", "ทดสอบส่งเมล์จ้า", "ทดสอบจ้า<b>เย้</b>", out outMailMessage, "AliasName");
    ///         if (!clsMail.SendByGmail(
    ///             "GooDesign.in.th@gmail.com",
    ///             "G00des1gn",
    /// System.Configuration.ConfigurationManager.AppSettings["mailAlert"],
    ///             "Article : มีบทความใหม่ '" + txtName.Text + "'",
    /// string.Format("<h1>ส่งบทความออนไลน์ : มีบทความใหม่</h1><div><b>ชื่อบทความ</b> : {0}</div><div><b>ผู้เขียน</b> : {1}</div><div><b>อีเมล์</b> : {2}</div><div><b>เบอร์โทรศัพท์</b> : {3}</div>",
    /// txtName.Text, txtOwner.Text, txtEmail.Text, txtPhone.Text),
    ///             out outMessage,
    ///             "AutoSystem : วารสารบริหารการศึกษาบัวบัณฑิต",
    /// System.Configuration.ConfigurationManager.AppSettings["mailCc"], "", "", MailPriority.High))
    ///         {
    /// ucColorBox1.Alert("พบข้อผิดพลาดขณะส่งเมล์", outMessage, AlertImage: ucColorBox.Alerts.Fail);
    ///             return;
    /// }
    /// </example>
    public bool SendByGmail(string FromMail, string FromMailPassword, string To, string Subject, string Message, out string outMessage, string FromAliasName = "", string Cc = "", string Bcc = "", string Signature = "", MailPriority Priority = MailPriority.Normal)
    {
        #region Variable
        var myMail = new MailMessage();
        var strMailBody = new StringBuilder();
        outMessage = "";
        #endregion
        #region DataChecker
        if (string.IsNullOrEmpty(To))
        {
            outMessage = "โปรดระบุเมล์ที่จะส่งหา";
            return false;
        }
        if (string.IsNullOrEmpty(FromMail) || string.IsNullOrEmpty(FromMailPassword))
        {
            outMessage = "โปรดระบุชื่อและรหัสผ่านของอีเมล์ที่จะใช้ส่ง";
            return false;
        }
        #endregion

        #region Mail Builder
        #region Address
        myMail.From = new MailAddress(FromMail, FromAliasName, System.Text.Encoding.GetEncoding("windows-874"));
        myMail.To.Add(To);
        if (!string.IsNullOrEmpty(Cc))
        {
            myMail.CC.Add(Cc);
        }
        if (!string.IsNullOrEmpty(Bcc))
        {
            myMail.Bcc.Add(Bcc);
        }
        #endregion
        #region Detail
        #region Subject
        if (!string.IsNullOrEmpty(Subject))
        {
            myMail.Subject = Subject;
        }
        else
        {
            myMail.Subject = "";
        }
        #endregion
        #region Body
        strMailBody.Append("<html>");
        strMailBody.Append("<head></head>");
        strMailBody.Append("<body>");
        if (!string.IsNullOrEmpty(Message))
        {
            strMailBody.Append(Message);
        }
        if (!string.IsNullOrEmpty(Signature))
        {
            strMailBody.Append("<hr/>");
            strMailBody.Append(Signature);
        }
        strMailBody.Append("</body>");
        strMailBody.Append("</html>");

        myMail.Body = strMailBody.ToString();
        #endregion
        myMail.Priority = Priority;
        myMail.IsBodyHtml = true;
        myMail.BodyEncoding = System.Text.Encoding.GetEncoding("windows-874");
        #endregion
        #endregion
        #region Server Config
        SmtpClient smtpMail = new SmtpClient();
        smtpMail.Host = "smtp.gmail.com";
        smtpMail.EnableSsl = true;
        NetworkCredential NetworkCred = new NetworkCredential(FromMail, FromMailPassword);
        smtpMail.UseDefaultCredentials = true;
        smtpMail.Credentials = NetworkCred;
        smtpMail.Port = 587;
        #endregion
        #region Send Mail
        try
        {
            smtpMail.Send(myMail);
            return true;
        }
        catch (Exception ex)
        {
            outMessage = ex.Message;
            return false;
        }
        #endregion
    }
}