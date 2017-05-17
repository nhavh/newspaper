using System;
using System.Web.UI;
using TMV.Data.Entities;
using System.Web.Security;

namespace TMV.BackEnd
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            var control = new AdminUserController();
            var objUserInfo = control.AdminUserLogin(txtUsername.Text.Trim(), txtPassword.Text);
            
            if (objUserInfo == null)
            {
                lblResults.Text = "Tên đăng nhập hoặc Mật khẩu không chính xác";
                return;
            }

            AdminUserController.AdminUserLogin(objUserInfo, true);
            FormsAuthentication.RedirectFromLoginPage(txtUsername.Text.Trim(), chkRemember.Checked);
        }
    }
}
