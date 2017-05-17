using System;
using System.Web;
using TMV.Data.Entities;
using System.Web.Security;
using TMV.Utilities;

namespace TMV.Services.HttpModules
{
    public class AdminMembership : IHttpModule
    {
        public void Init(HttpApplication application)
        {
            application.AuthenticateRequest += OnAuthenticateRequest;
        }

        public void OnAuthenticateRequest(object s, EventArgs e)
        {
            var context = ((HttpApplication)s).Context;
            var request = context.Request;
            var response = context.Response;

            if (!request.Url.LocalPath.ToLower().EndsWith(Globals.InstanceExtension)) return;

            if (request.IsAuthenticated)
            {
                var objUser = AdminUserController.GetCachedAdminUser(context.User.Identity.Name);

                if (objUser == null || objUser.Username.ToLower() != context.User.Identity.Name.ToLower())
                {
                    AdminUserController.AdminUserSignOut();
                    response.Redirect(request.RawUrl, true);
                    return;
                }

                if (request.Cookies["username"] == null)
                {
                    var currentDateTime = DateTime.Now;
                    var userTicket = new FormsAuthenticationTicket(1, context.User.Identity.Name, currentDateTime, currentDateTime.AddHours(1), false, objUser.Username);
                    var username = FormsAuthentication.Encrypt(userTicket);

                    var httpCookie = response.Cookies["username"];
                    if (httpCookie != null)
                    {
                        httpCookie.Value = username;
                        httpCookie.Path = "/";
                        httpCookie.Expires = currentDateTime.AddMinutes(1);
                    }
                }
                context.Items.Add("AdminUserInfo", objUser);
            }

            if (HttpContext.Current.Items["AdminUserInfo"] == null)
            {
                context.Items.Add("AdminUserInfo", new AdminUserInfo());
            }
        }

        public void Dispose()
        {
        }
    }
}
