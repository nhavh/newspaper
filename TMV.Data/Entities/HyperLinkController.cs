using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class HyperLinkController
    {
        public void InsertHyperLink(HyperLinkInfo info)
        {
            SQL.InsertHyperLink(info.OldChar, info.NewChar);
        }
        public void UpdateHyperLink(HyperLinkInfo info)
        {
            SQL.UpdateHyperLink(info.HyperLinkId, info.OldChar, info.NewChar);
        }
        public void DeleteHyperLink(HyperLinkInfo info)
        {
            SQL.DeleteHyperLink(info.HyperLinkId);
        }
        public DataTable SelectHyperLink()
        {
            var hyperLinks = CBO.FillCollection<HyperLinkInfo>(SQL.ListHyperLink());
            return CBO.ConvertToDataTable(hyperLinks, typeof(HyperLinkInfo));
        }
        public List<HyperLinkInfo> ListHyperLink(bool isClearCache = false)
        {
            string strCacheKey = Globals.SHA1Encryption(string.Format("TMV_ListHyperLink"));
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<HyperLinkInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<HyperLinkInfo>(SQL.ListHyperLink());
            if (tmp == null) return new List<HyperLinkInfo>();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
    }
}
