using System;
using System.Collections;
using System.Web;
using System.Text;
using System.Threading;
using TMV.Data.Entities;
using TMV.Utilities;

namespace TMV.Framework
{
    public class AdminSiteMap : SiteMapProvider
    {
        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            var i = rawUrl.IndexOf('?');
            var url = rawUrl;
            if (i > 0) url = rawUrl.Substring(0, i);
            var start = url.LastIndexOf('/') + 1;
            var stop = url.LastIndexOf('.');
            url = url.Substring(start, stop - start);

            var list = AdminUserController.GetCurrentAdminUser().Pages;
            if (list != null)
            {
                foreach (AdminPageInfo page in list)
                {
                    if (page.Visible && url.Equals(page.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        return new SiteMapNode(this, page.AdminPageID.ToString(), (page.Source == Null.NullString ? "" : page.Link), page.Name);
                    }
                }
            }
            return null;
        }

        public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
        {
            var col = new SiteMapNodeCollection();
            var id = int.Parse(node.Key);
            var list = AdminUserController.GetCurrentAdminUser().Pages;
            if (list != null)
            {
                foreach (AdminPageInfo page in list)
                {
                    if (page.Visible && page.ParentID == id)
                        col.Add(new SiteMapNode(this, page.AdminPageID.ToString(), (page.Source == Null.NullString ? "" : ("~" + page.Link)), page.Name));
                }
            }
            return col;
        }

        public override SiteMapNode GetParentNode(SiteMapNode node)
        {
            if (node == null)
                return null;

            var id = int.Parse(node.Key);
            var ctrl = new AdminPageController();
            var parentId = ctrl.GetAdminPage(id).ParentID;
            if (parentId != Null.NullInteger && parentId != id)
            {
                var parent = ctrl.GetAdminPage(parentId);
                if (parent.Visible)
                {
                    return new SiteMapNode(this, parent.AdminPageID.ToString(), (parent.Source == Null.NullString ? "" :parent.Link), parent.Name);
                }
            }

            return null;
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            return new SiteMapNode(this, Null.NullInteger.ToString(), "/Default.aspx", "Home");
        }
    }
}
