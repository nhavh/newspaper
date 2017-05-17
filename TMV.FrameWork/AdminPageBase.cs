using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Drawing;
using TMV.Data.Entities;
using TMV.Utilities;

namespace TMV.Framework
{
    public class AdminPageBase : System.Web.UI.Page
    {
        private int _userId = Null.NullInteger;

        public AdminUserInfo UserInfo
        {
            get
            {
                return AdminUserController.GetCurrentAdminUser();
            }
        }

        public int UserId
        {
            get { return UserInfo.UserID; }
        }

        protected override void OnPreInit(EventArgs e)
        {
            var page = Request.CurrentExecutionFilePath;
            if (Request.QueryString["xml"] != null)
                page += "?xml=" + Request.QueryString["xml"];
            if (UserInfo.IsAdministrator == false)
            {
                if (!(page.ToLower().Contains("/default") || page.ToLower().Contains("/changepassword")))
                {
                    if (UserInfo.IsInPage(page) == false)
                    {
                        Response.Redirect("/AccessDeny.aspx", true);
                        return;
                    }
                }
            }
            MasterPageFile = "/MasterPage.Master";
            base.OnPreInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
        }

        private void LoadAdminMenu()
        {
            var menuTop = (ContentPlaceHolder)Master.FindControl("placeHolderMenuTop");
            var sm = new SiteMapDataSource {Provider = SiteMap.Providers["PageSiteMap"], ShowStartingNode = false};
            var webMenu = new Menu();            
            webMenu.StaticMenuItemStyle.Font.Bold = true;            
            webMenu.StaticMenuItemStyle.ForeColor = ColorTranslator.FromHtml("#456305");
            webMenu.Orientation = Orientation.Horizontal;
            webMenu.StaticMenuItemStyle.BorderColor = ColorTranslator.FromHtml("#F0F0F0");

            webMenu.DynamicHorizontalOffset = 1;
            webMenu.DynamicMenuItemStyle.Height = Unit.Pixel(20);
            webMenu.DynamicMenuItemStyle.BorderWidth = Unit.Point(1);
            webMenu.DynamicMenuItemStyle.BorderStyle = BorderStyle.Solid;
            webMenu.DynamicMenuItemStyle.BorderColor = ColorTranslator.FromHtml("#F0F0F0");
            webMenu.DynamicMenuItemStyle.Font.Name = "Arial, Helvetica, sans-serif";
            webMenu.DynamicMenuItemStyle.Font.Size = 9;
            webMenu.StaticMenuItemStyle.HorizontalPadding = 0;
            webMenu.DynamicMenuItemStyle.Font.Bold = true;
            webMenu.DynamicMenuItemStyle.BackColor = ColorTranslator.FromHtml("#F0F0F0");            

            webMenu.MaximumDynamicDisplayLevels = 5;
            webMenu.DataSource = sm;
            webMenu.DataBind();
            menuTop.Controls.Add(webMenu);
        }        
    }
}
