<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMail.aspx.cs" Inherits="SendMail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/CSS/cssControl.css" rel="stylesheet" />
    <link href="/Plugin/jQueryUI/jquery-ui.css" rel="stylesheet" />
    <script src="/Plugin/jQuery/jquery.min.js"></script>
    <script src="/Plugin/jQueryUI/jquery-ui.js"></script>
    <style>
        .valid{
            font-family:Tahoma;
            color:red;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnUpload").click(function (event) {
                var files = $("#FileUpload1")[0].files;
                if (files.length > 0) {
                    var formData = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        formData.append(files[i].name, files[i]);
                    }
                    formData.append("txtName", "<%=txtName.Text%>");

                    var progressbarLabel = $('#progressBar-label');
                    var progressbarDiv = $('#progressbar');

                    $.ajax({
                        url: '/UploadHandler.ashx',
                        method: 'post',
                        data: formData,
                        contentType: false,
                        processData: false,
                        success: function (result) {
                            progressbarLabel.text('Complete');
                            progressbarDiv.fadeOut(2000);
                            $('#<%=hidFileName.ClientID%>').val(result);
                        },
                        error: function (err) {
                            alert("Error : " + err);
                            alert(err.statusText);
                        }
                    });

                    progressbarLabel.text('Uploading...');
                    progressbarDiv.progressbar({
                        value: false
                    }).fadeIn(500);
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnSendMail" runat="server" DefaultButton="btSendMail">
            <table style="width:100%;">
                <tr>
                    <td style="width:150px;">
                        ไฟล์งาน
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <input type="button" id="btnUpload" value="Upload Files" /><span style="color:#FF3A3A;"> * กดปุ่มนี้ก่อนทำการส่งข้อมูล</span>
                        <div>
                            <div id="progressbar" style="position:relative;display:none;">
                                <span style="position: absolute; left: 35%; top: 20%" id="progressBar-label">
                                    Uploading...
                                </span>
                            </div>
                        </div>
                        <asp:HiddenField ID="hidFileName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="">
                        ชื่อลูกค้า / บริษัท
                    </td>
                    <td>
                        <asp:TextBox ID="txtContactName" runat="server" MaxLength="200"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtContactName" Display="Dynamic" ErrorMessage="โปรดกรอกชื่อ" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgSendMail"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="">
                        อีเมล์
                    </td>
                    <td>
                        <asp:TextBox ID="txtContactEmail" runat="server" MaxLength="200"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContactEmail" Display="Dynamic" ErrorMessage="โปรดกรอกอีเมล์" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgSendMail"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="">
                        เบอร์ติดต่อ
                    </td>
                    <td>
                        <asp:TextBox ID="txtContactPhone" runat="server" MaxLength="200"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContactPhone" Display="Dynamic" ErrorMessage="โปรดกรอกเบอร์ติดต่อ" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgSendMail"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="">
                        ชื่องาน
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" MaxLength="200"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="โปรดกรอกชื่องาน" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgSendMail"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="">
                        รายละเอียด
                    </td>
                    <td>
                        <asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine" Width="90%" Rows="5"/>
                    </td>
                </tr>
                <tr>
                    <td style="">
                        สถานที่
                    </td>
                    <td>
                        <asp:TextBox ID="txtLocation" runat="server" MaxLength="200"/>
                    </td>
                </tr>
                <tr>
                    <td style="">
                   
                    </td>
                    <td>
                        <asp:Button ID="btSendMail" runat="server" Text="Submit" ValidationGroup="vgSendMail" OnClick="btSendMail_Click"/>
                        <asp:Label ID="lblSendMailAlert" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
