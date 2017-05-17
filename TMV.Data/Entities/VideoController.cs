using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class VideoController
    {
        public void InsertVideo(VideoInfo info)
        {
            SQL.InsertVideo(info.Url, info.Title, info.Description, info.Tags, info.SeoTitle, info.SeoDescription, info.IsHome, info.StartDate, info.EndDate, info.CreatedBy, info.Slug);
        }
        public void UpdateVideo(VideoInfo info)
        {
            SQL.UpdateVideo(info.VideoId, info.Url, info.Title, info.Description, info.Tags, info.SeoTitle, info.SeoDescription, info.IsHome, info.StartDate, info.EndDate, info.CreatedBy, info.UpdatedBy, info.Slug);
        }
        public void DeleteVideo(VideoInfo info)
        {
            SQL.DeleteVideo(info.VideoId);
        }
        public VideoInfo GetVideo(int videoId)
        {
            return CBO.FillObject<VideoInfo>(SQL.GetVideo(videoId));
        }
        public List<VideoInfo> ListVideo()
        {
            return CBO.FillCollection<VideoInfo>(SQL.ListVideo());
        }
        public DataTable SelectVideo()
        {
            return CBO.ConvertToDataTable(ListVideo(), typeof(VideoInfo));
        }
        public List<VideoInfo> ListVideoByHome(bool isClearCache = false)
        {
            string strCacheKey = "TMV_ListVideoByHome";
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<VideoInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<VideoInfo>(SQL.ListVideoByHome());
            if (tmp == null || tmp.Count == 0) return new List<VideoInfo>();
            res = new List<VideoInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }

        public List<VideoInfo> ListVideoByPaging(int page, int pageSize, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_ListVideoByPaging_{0}{1}", page, pageSize);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<VideoInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<VideoInfo>(SQL.ListVideoByPaging(page, pageSize));
            if (tmp == null || tmp.Count == 0) return new List<VideoInfo>();
            res = new List<VideoInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public VideoInfo GetVideoBySlug(string slug, bool isClearCache = false)
        {
            string strCacheKey = Globals.SHA1Encryption(string.Format("TMV_GetVideoBySlug_{0}", slug));
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as VideoInfo;
            if (res != null) return res;
            var tmp = CBO.FillObject<VideoInfo>(SQL.GetVideoBySlug(slug));
            if (tmp == null) return new VideoInfo();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }

        public List<VideoInfo> ListVideoByOther(int videoId,int takeNum, bool isClearCache = false)
        {
            string strCacheKey = Globals.SHA1Encryption(string.Format("TMV_ListVideoByOther_{0}", videoId));
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<VideoInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<VideoInfo>(SQL.ListVideo()).Where(c=>c.VideoId != videoId).OrderByDescending(c=>c.CreatedDate).Take(takeNum).ToList();
            if (!tmp.Any()) return new List<VideoInfo>();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
    }
}
