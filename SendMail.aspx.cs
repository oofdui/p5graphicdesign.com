using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SendMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btSendMail_Click(object sender, EventArgs e)
    {
        #region Variable
        var strSQL = new StringBuilder();
        var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
        var clsDefault = new clsDefault();
        #endregion
        #region Procedure
        if (hidFileName.Value != "")
        {
            #region SQLQuery
            strSQL.Append("INSERT INTO ");
            strSQL.Append("job");
            strSQL.Append("(FileName,Name,Detail,ContactName,ContactPhone,ContactEmail,Location,CWhen,MWhen)");
            strSQL.Append("VALUES(");
            strSQL.Append("'" + hidFileName.Value.Trim() + "',");
            strSQL.Append("'" + txtName.Text.SQLQueryFilter() + "',");
            strSQL.Append("'" + txtDetail.Text.SQLQueryFilter() + "',");
            strSQL.Append("'" + txtContactName.Text.SQLQueryFilter() + "',");
            strSQL.Append("'" + txtContactPhone.Text.SQLQueryFilter() + "',");
            strSQL.Append("'" + txtContactEmail.Text.SQLQueryFilter() + "',");
            strSQL.Append("'" + txtLocation.Text.SQLQueryFilter() + "',");
            strSQL.Append("GETDATE(),");
            strSQL.Append("GETDATE()");
            strSQL.Append(");");
            #endregion
            if (clsSQL.Execute(strSQL.ToString()))
            {
                #region MailSender
                var clsMail = new clsMail();
                var outMessage = "";

                //Send to Admin
                try
                {
                    if (!clsMail.SendByGmail(
                    "goodesign.vps@gmail.com",
                    "G00des1gn",
                    System.Configuration.ConfigurationManager.AppSettings["mailTo"],
                    "P5GraphicDesign : มีใบงานใหม่ '" + txtName.Text.SQLQueryFilter() + "'",
                    string.Format("<h1>มีใบงานใหม่ : {0}</h1><div><b>จาก</b> : {1}</div><div><b>เบอร์โทร</b> : {2}</div><div><b>รายละเอียด</b> : {3}</div><hr/><a href='http://www.p5graphicdesign.com/Management/Job.aspx'>คลิกที่นี่เพื่อดูข้อมูล</a>",
                        txtName.Text.SQLQueryFilter(),
                        txtContactName.Text.SQLQueryFilter(),
                        txtContactPhone.Text.SQLQueryFilter(),
                        txtDetail.Text.SQLQueryFilter()),
                    out outMessage,
                    "P5GraphicDesign : มีใบงานใหม่ '" + txtName.Text.SQLQueryFilter() + "'",
                    "off.dui@gmail.com", "", "", System.Net.Mail.MailPriority.High))
                    {
                        Response.Write("Send to Admin : " + outMessage);
                        //ucColorBox1.Alert("พบข้อผิดพลาดขณะส่งเมล์", outMessage, AlertImage: ucColorBox.Alerts.Fail);
                        return;
                    }
                }
                catch(Exception exMailToAdmin) { Response.Write(exMailToAdmin.Message); }
                if (txtContactEmail.Text.Trim() != "" && txtContactEmail.Text.Contains("@") && txtContactEmail.Text.Contains("."))
                {
                    //Send to Customer
                    try
                    {
                        if (!clsMail.SendByGmail(
                        "goodesign.vps@gmail.com",
                        "G00des1gn",
                        txtContactEmail.Text.Trim(),
                        "P5GraphicDesign : ได้รับใบงาน '" + txtName.Text.SQLQueryFilter() + "' ของคุณแล้ว",
                        string.Format("<h1>ได้รับใบงานใหม่เรียบร้อยแล้ว : {0}</h1><div><b>จาก</b> : {1}</div><div><b>เบอร์โทร</b> : {2}</div><div><b>รายละเอียด</b> : {3}</div>",
                            txtName.Text.SQLQueryFilter(),
                            txtContactName.Text.SQLQueryFilter(),
                            txtContactPhone.Text.SQLQueryFilter(),
                            txtDetail.Text.SQLQueryFilter()),
                        out outMessage,
                        "P5GraphicDesign : ได้รับใบงาน '" + txtName.Text.SQLQueryFilter() + "' ของคุณแล้ว",
                        "", "", "", System.Net.Mail.MailPriority.High))
                        {
                            Response.Write(outMessage);
                            //ucColorBox1.Alert("พบข้อผิดพลาดขณะส่งเมล์", outMessage, AlertImage: ucColorBox.Alerts.Fail);
                            return;
                        }
                    }
                    catch(Exception exMailToCustomer) { Response.Write("Send to Customer : " + exMailToCustomer.Message); }
                }
                #endregion
                txtName.Text = "";txtDetail.Text = "";txtContactName.Text = "";txtContactPhone.Text = "";txtLocation.Text = "";hidFileName.Value = "";
                lblSendMailAlert.Text = clsDefault.AlertMessageFlat("บันทึกข้อมูลเสร็จสมบูรณ์", clsDefault.AlertType.Success);
                lblSendMailAlert.Focus();
            }
            else
            {
                lblSendMailAlert.Text = clsDefault.AlertMessageFlat("เกิดข้อผิดพลาดขณะบันทึกข้อมูล<br/>"+strSQL.ToString(), clsDefault.AlertType.Fail);
                lblSendMailAlert.Focus();
            }
        }
        else
        {
            lblSendMailAlert.Text = clsDefault.AlertMessageFlat("โปรดเลือกอัพโหลดไฟล์ก่อนทำการส่งข้อมูล", clsDefault.AlertType.Fail);
            lblSendMailAlert.Focus();
        }
        #endregion
    }
}