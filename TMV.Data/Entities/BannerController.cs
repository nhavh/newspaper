using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class BannerController
    {
        public void InsertBanner(BannerInfo info)
        {
            SQL.InsertBanner(info.Title, info.ImagePath, info.Priority, info.StartDate, info.EndDate, info.IsMobile, info.Content, info.Type, info.Slug, info.Url, info.CategoryId,info.Position);
        }
        public void UpdateBanner(BannerInfo info)
        {
            SQL.UpdateBanner(info.BannerId, info.Title, info.ImagePath, info.Priority, info.StartDate, info.EndDate, info.IsMobile, info.Content, info.Type, info.Slug, info.Url, info.CategoryId, info.Position);
        }
        public void DeleteBanner(BannerInfo info)
        {
            SQL.DeleteBanner(info.BannerId);
        }
        public BannerInfo GetBanner(int bannerId)
        {
            return CBO.FillObject<BannerInfo>(SQL.GetBanner(bannerId));
        }
        public BannerInfo GetBannerBySlug(string slug, bool isClearCache = false)
        {
            string strCacheKey = Globals.SHA1Encryption(string.Format("TMV_GetBannerBySlug_{0}", slug));
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as BannerInfo;
            if (res != null) return res;
            var tmp = CBO.FillObject<BannerInfo>(SQL.GetBannerBySlug(slug));
            if (tmp == null) return new BannerInfo();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public DataTable SelectBanner()
        {
            var banners = CBO.FillCollection<BannerInfo>(SQL.ListBanner());
            return CBO.ConvertToDataTable(banners, typeof(BannerInfo));
        }
        public DataTable SelectBannerPrice()
        {
            var banners = CBO.FillCollection<BannerInfo>(SQL.ListBannerPrice());
            return CBO.ConvertToDataTable(banners, typeof(BannerInfo));
        }
        public List<BannerInfo> ListBannerByPriority(byte priority, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_ListBannerByPriority_{0}", priority);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<BannerInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<BannerInfo>(SQL.ListBannerByPriority(priority));
            if (tmp == null || tmp.Count == 0) return new List<BannerInfo>();
            res = new List<BannerInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public BannerInfo GetBannerById(int bannerId, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_GetBannerById_{0}", bannerId);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as BannerInfo;
            if (res != null) return res;
            var tmp = CBO.FillObject<BannerInfo>(SQL.GetBanner(bannerId));
            if (tmp == null) return new BannerInfo();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public BannerInfo GetBannerByCategoryId(int categoryId, bool isMobile, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_GetBannerByCategoryId_{0}{1}", categoryId, isMobile);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as BannerInfo;
            if (res != null) return res;
            var tmp = CBO.FillObject<BannerInfo>(SQL.GetBannerByCategoryId(categoryId, isMobile));
            if (tmp == null) return new BannerInfo();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
    }
}
