using System;
using System.Collections.Generic;
using System.Linq;
using TMV.Data.Entities;

namespace TMV.BackEnd
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected string HtmlMenuTop = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UserAgent != null && Request.UserAgent.IndexOf("AppleWebKit", StringComparison.Ordinal) > 0)
            {
                Request.Browser.Adapters.Clear();
            }

            lnkName.Text = AdminUserController.GetCurrentAdminUser().Username;
            lnkName.NavigateUrl = "~/ChangePassword.aspx";

            BindMenuTop(AdminUserController.GetCurrentAdminUser().PagesEx);
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AdminUserController.AdminUserSignOut();
            Response.Redirect("~/Login.aspx", true);
        }

        private void BindMenuTop(List<AdminPageInfo> pages)
        {
            // Build Menu Items
            HtmlMenuTop = string.Empty;

            var rootPages = new List<AdminPageInfo>(pages.Where(item => item.ParentID == -1));
            rootPages.Sort(delegate(AdminPageInfo a, AdminPageInfo b) { return (a.OrderView == b.OrderView ? 0 : a.OrderView < b.OrderView ? -1 : 1); });

            foreach (var page in rootPages)
            {
                var subPages = new List<AdminPageInfo>(pages.Where(item => item.ParentID == page.AdminPageID && item.Visible));
                subPages.Sort
                (
                    delegate(AdminPageInfo a, AdminPageInfo b)
                    { return (a.OrderView == b.OrderView ? 0 : a.OrderView < b.OrderView ? -1 : 1); }
                );

                var subItem = string.Empty;
                foreach (AdminPageInfo subPage in subPages)
                {
                    subItem += CreateMenuItem(false, subPage, string.Empty);
                }

                var mItem = CreateMenuItem(true, page, subItem);
                HtmlMenuTop += mItem;
            }
        }

        private string CreateMenuItem(bool isRoot, AdminPageInfo page, string subItem)
        {
            const string strTemp = @" <li class=""sub""><a href=""{0}"">{1}</a>{2}</li>";
            return string.Format
            (
                strTemp,
                (isRoot ? "javascript:void(0);" : page.Source),
                page.Name,
                (
                    isRoot ?
                    string.Format
                    (
                        "<ul class=\"subnav\">{0}</ul>",
                        subItem
                    ) :
                    string.Empty
                )

            );
        }
    }
}
