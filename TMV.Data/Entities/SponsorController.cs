using System;
using System.Collections.Generic;
using System.Data;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class SponsorController
    {
        public int InsertSponsor(SponsorInfo info)
        {
            return SQL.InsertSponsor(info.ItemId, info.ItemType, info.StartDate, info.EndDate);
        }
        public void UpdateSponsor(SponsorInfo info)
        {
            SQL.UpdateSponsor(info.SponsorId, info.ItemId, info.ItemType, info.StartDate, info.EndDate);
        }
        public void DeleteSponsor(SponsorInfo info)
        {
            DeleteSponsor(info.SponsorId);
        }
        public void DeleteSponsor(int sponsorId)
        {
            SQL.DeleteSponsor(sponsorId);
        }
        public SponsorInfo GetSponsor(int sponsorId)
        {
            return CBO.FillObject<SponsorInfo>(SQL.GetSponsor(sponsorId));
        }
        public List<SponsorInfo> ListSponsor()
        {
            return CBO.FillCollection<SponsorInfo>(SQL.ListSponsor());
        }
        public DataTable SelectSponsor()
        {
            return CBO.ConvertToDataTable(ListSponsor(), typeof(SponsorInfo));
        }
        public List<SponsorInfo> ListSponsorByType(int categoryId, bool isClearCache = false)
        {
            string strCacheKey = "TMV_ListSponsorByHome";
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<SponsorInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<SponsorInfo>(SQL.ListSponsorByType(categoryId));
            if (tmp == null) return new List<SponsorInfo>();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(3), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
    }
}
