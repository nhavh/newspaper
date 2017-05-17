using System;
using System.Web.UI;
using TMV.Data.Entities;

namespace TMV.BackEnd
{
    public partial class ChangePassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            var control = new AdminUserController();
            var objUserInfo = AdminUserController.GetCurrentAdminUser();
            if (String.IsNullOrEmpty(txtConfirmPass.Text.Trim()) || String.IsNullOrEmpty(txtConfirmPass.Text.Trim()) || String.IsNullOrEmpty(txtConfirmPass.Text.Trim()))
            {
                lblResults.Text = "Bạn phải nhập đầy đủ và chính xác thông tin";
                return;
            }
            if (TMV.Utilities.Globals.SHA1Encryption(txtCurrentPass.Text.Trim()) != objUserInfo.Password)
            {
                lblResults.Text = "Mật khẩu hiện tại không chính xác";
                return;
            }
            if (txtNewPass.Text.Trim() != txtConfirmPass.Text.Trim())
            {
                lblResults.Text = "Mật khẩu mới và xác nhận mật khẩu khác nhau";
                return;
            }

            objUserInfo.Password = txtNewPass.Text.Trim();
            control.UpdateAdminUser(objUserInfo);
            lblResults.Text = "Thay đổi mật khẩu thành công. Click Login để đăng nhập vào hệ thống.";
            Response.Redirect("Login.aspx");
        }
    }
}
