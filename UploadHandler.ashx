<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;

public class UploadHandler : IHttpHandler {

    public void ProcessRequest (HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            HttpFileCollection selectedFiles = context.Request.Files;
            for (int i = 0; i < selectedFiles.Count; i++)
            {
                //System.Threading.Thread.Sleep(1000);
                HttpPostedFile PostedFile = selectedFiles[i];
                var name = "";
                var fullName = "";
                var fullNameDB = "";
                for(int j = 0; j < 5; j++)
                {
                    name = DateTime.Now.ToString("yyyyMMddHHmmss")+"_"+j+1+""+Path.GetExtension(PostedFile.FileName);
                    fullName = context.Server.MapPath("~/Upload/Job/" + name);
                    fullNameDB = "/Upload/Job/" + name;
                    if (!File.Exists(fullName))
                    {
                        break;
                    }
                }
                if (fullName != "")
                {
                    PostedFile.SaveAs(fullName);
                    context.Response.Write(fullNameDB);
                }
                break;
            }
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}