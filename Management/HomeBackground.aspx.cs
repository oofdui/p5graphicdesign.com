using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_HomeBackground : System.Web.UI.Page
{
    clsDefault clsDefault = new clsDefault();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getDefault();
        }
    }
    private void getDefault()
    {
        #region Variable
        var fi = new FileInfo(Server.MapPath("/Images/bgHome.jpg"));
        #endregion
        #region Procedure
        if (fi.Exists)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(fi.FullName);
            lblPhoto.Text = "<img src='/Images/bgHome.jpg' style='max-width:100%;'/>";
            lblPhoto.Text += "<div style='margin-top:10px;padding:5px;background-color:#FAFAFA;border:1px solid #DDD;text-align:center;'>" +
                "width : " + img.Width.ToString() + " x height : " + img.Height.ToString() + "</div>";
        }
        else
        {
            lblPhoto.Text = "ไม่พบภาพปัจจุบัน";
        }
        #endregion
    }
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        #region Variable

        #endregion
        #region Procedure
        if (fuPhoto.HasFile)
        {
            if (!Path.GetExtension(fuPhoto.FileName).Contains("jpg"))
            {
                lblSQL.Text = clsDefault.AlertMessageFlat("โปรดเลือกภาพ JPG เท่านั้น", clsDefault.AlertType.Warn);
                return;
            }
            try
            {
                fuPhoto.SaveAs(Server.MapPath("/Images/bgHome.jpg"));
                getDefault();
                lblSQL.Text = clsDefault.AlertMessageFlat("อัพโหลดรูปภาพเสร็จสิ้น * หากภาพไม่อัพเดท โปรดกดปุ่ม F5 เพื่อรีเฟรชหน้าเว็บ", clsDefault.AlertType.Success);
            }
            catch(Exception ex) { lblSQL.Text = clsDefault.AlertMessageFlat(ex.Message, clsDefault.AlertType.Fail); }
        }
        #endregion
    }
}