using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class LinkWebsiteController
    {
        public int InsertLinkWebsite(LinkWebsiteInfo info)
        {
            return SQL.InsertLinkWebsite(info.AnchorText, info.Url, info.OrderView, info.Target, info.Priority);
        }
        public void UpdateLinkWebsite(LinkWebsiteInfo info)
        {
            SQL.UpdateLinkWebsite(info.LinkWebsiteID, info.AnchorText, info.Url, info.OrderView, info.Target, info.Priority);
        }
        public void DeleteLinkWebsite(LinkWebsiteInfo info)
        {
            DeleteLinkWebsite(info.LinkWebsiteID);
        }
        public void DeleteLinkWebsite(int linkWebsiteId)
        {
            SQL.DeleteLinkWebsite(linkWebsiteId);
        }
        public List<LinkWebsiteInfo> ListLinkWebsite()
        {
            return CBO.FillCollection<LinkWebsiteInfo>(SQL.ListLinkWebsite());
        }
        public DataTable SelectLinkWebsite()
        {
            return CBO.ConvertToDataTable(ListLinkWebsite(), typeof(LinkWebsiteInfo));
        }
        public List<LinkWebsiteInfo> ListLinkWebsiteByPriority(byte priority, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_ListLinkWebsiteByPriority_{0}", priority);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);

            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<LinkWebsiteInfo>;
            if (res != null) return res;

            var tmp = CBO.FillCollection<LinkWebsiteInfo>(SQL.ListLinkWebsiteByPriority(priority));
            if (tmp == null || tmp.Count == 0) return null;
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
    }
}
