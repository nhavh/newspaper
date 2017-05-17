using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class PictureController
    {
        public void InsertPicture(PictureInfo info)
        {
            SQL.InsertPicture(info.ImagePath, info.Title, info.Description, info.Tags, info.SeoTitle, info.SeoDescription, info.IsHome, info.StartDate, info.EndDate, info.CreatedBy, info.Slug, info.UrlPath);
        }
        public void UpdatePicture(PictureInfo info)
        {
            SQL.UpdatePicture(info.PictureId, info.ImagePath, info.Title, info.Description, info.Tags, info.SeoTitle, info.SeoDescription, info.IsHome, info.StartDate, info.EndDate, info.CreatedBy, info.UpdatedBy, info.Slug, info.UrlPath);
        }
        public void DeletePicture(PictureInfo info)
        {
            SQL.DeletePicture(info.PictureId);
        }
        public PictureInfo GetPicture(int pictureId)
        {
            return CBO.FillObject<PictureInfo>(SQL.GetPicture(pictureId));
        }
        public DataTable SelectPicture()
        {
            var pictures = CBO.FillCollection<PictureInfo>(SQL.ListPicture());
            return CBO.ConvertToDataTable(pictures, typeof(PictureInfo));
        }
        public List<PictureInfo> ListPictureByHome(bool isClearCache = false)
        {
            string strCacheKey = "TMV_ListPictureByHome";
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<PictureInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<PictureInfo>(SQL.ListPictureByHome());
            if (tmp == null || tmp.Count == 0) return new List<PictureInfo>();
            res = new List<PictureInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public List<PictureInfo> ListPictureByPaging(int page, int pageSize, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_ListPictureByPaging_{0}{1}", page, pageSize);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<PictureInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<PictureInfo>(SQL.ListPictureByPaging(page, pageSize));
            if (tmp == null || tmp.Count == 0) return new List<PictureInfo>();
            res = new List<PictureInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public PictureInfo GetPictureBySlug(string slug, bool isClearCache = false)
        {
            string strCacheKey = Globals.SHA1Encryption(string.Format("TMV_GetPictureBySlug_{0}", slug));
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as PictureInfo;
            if (res != null) return res;
            var tmp = CBO.FillObject<PictureInfo>(SQL.GetPictureBySlug(slug));
            if (tmp == null) return new PictureInfo();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
    }
}
