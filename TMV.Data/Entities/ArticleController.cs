using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TMV.Utilities;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class ArticleController
    {
        public int InsertArticle(ArticleInfo info)
        {
            return SQL.InsertArticle(info.CategoryId, info.Title, info.Slug, info.Avatar, info.Description, info.Content, info.PublishDate, info.RoleId, info.AuthorId, info.EditorId, info.ApproverId, info.AdminNote, info.Tags, info.SeoTitle, info.SeoDescription, info.Intro, info.Advantage, info.Process, info.Qa, info.FrontBack, info.Video, info.Review, info.Result, info.IsTab);
        }
        public void UpdateArticle(ArticleInfo info)
        {
            SQL.UpdateArticle(info.ArticleId, info.CategoryId, info.Title, info.Slug, info.Avatar, info.Description, info.Content, info.PublishDate, info.RoleId, info.AuthorId, info.EditorId, info.ApproverId, info.AdminNote, info.Tags, info.SeoTitle, info.SeoDescription, info.Intro, info.Advantage, info.Process, info.Qa, info.FrontBack, info.Video, info.Review, info.Result, info.IsTab);
        }
        public void DeleteArticle(ArticleInfo info)
        {
            DeleteArticle(info.ArticleId);
        }
        public void DeleteArticle(int articleId)
        {
            SQL.DeleteArticle(articleId);
        }
        public ArticleInfo GetArticle(int articleId)
        {
            return CBO.FillObject<ArticleInfo>(SQL.GetArticle(articleId));
        }
        public ArticleInfo GetArticleBySlug(string slug, bool isClearCache = false)
        {
            //return CBO.FillObject<ArticleInfo>(SQL.GetArticleBySlug(slug));

            string strCacheKey = Globals.SHA1Encryption(string.Format("TMV_GetArticleBySlug_{0}", slug));
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            ArticleInfo res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as ArticleInfo;
            if (res != null) return res;
            var tmp = CBO.FillObject<ArticleInfo>(SQL.GetArticleBySlug(slug));
            if (tmp == null) return new ArticleInfo();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public ArticleInfo GetArticleByCategorySlug(string categorySlug, bool isClearCache = false)
        {
            //return CBO.FillObject<ArticleInfo>(SQL.GetArticleByCategorySlug(categorySlug));

            string strCacheKey = Globals.SHA1Encryption(string.Format("TMV_GetArticleByCategorySlug_{0}", categorySlug));
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            ArticleInfo res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as ArticleInfo;
            if (res != null) return res;
            var tmp = CBO.FillObject<ArticleInfo>(SQL.GetArticleByCategorySlug(categorySlug));
            if (tmp == null) return new ArticleInfo();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public List<ArticleInfo> ListArticleByRole(int status, int userId)
        {
            return CBO.FillCollection<ArticleInfo>(SQL.ListArticleByRole(status, userId));
        }
        public DataTable SelectArticleEditting()
        {
            var adminUser = AdminUserController.GetCurrentAdminUser();
            return CBO.ConvertToDataTable(ListArticleByRole((int)Globals.DocumentStatus.Editting, adminUser.UserID), typeof(ArticleInfo));
        }
        public DataTable SelectArticleWaittingApprove()
        {
            var adminUser = AdminUserController.GetCurrentAdminUser();
            return CBO.ConvertToDataTable(ListArticleByRole((int)Globals.DocumentStatus.WaittingApprove, adminUser.UserID), typeof(ArticleInfo));
        }
        public DataTable SelectArticleApproving()
        {
            var adminUser = AdminUserController.GetCurrentAdminUser();
            return CBO.ConvertToDataTable(ListArticleByRole((int)Globals.DocumentStatus.Approving, adminUser.UserID), typeof(ArticleInfo));
        }
        public DataTable SelectArticleOnline()
        {
            var adminUser = AdminUserController.GetCurrentAdminUser();
            return CBO.ConvertToDataTable(ListArticleByRole((int)Globals.DocumentStatus.Online, adminUser.UserID), typeof(ArticleInfo));
        }
        public List<ArticleInfo> ListArticleByCategory(int categoryId, int page, int pageSize, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_ListArticleByCategory_{0}_{1}_{2}", categoryId, page, pageSize);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            List<ArticleInfo> res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<ArticleInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<ArticleInfo>(SQL.ListArticleByCategory(categoryId, page, pageSize));
            if (tmp == null || tmp.Count == 0) return new List<ArticleInfo>();
            res = new List<ArticleInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(3), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public List<ArticleInfo> ListArticleByGroup(int categoryId, int group, int page, int pageSize, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_ListArticleByGroup_{0}_{1}_{2}_{3}", categoryId, group, page, pageSize);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            List<ArticleInfo> res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<ArticleInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<ArticleInfo>(SQL.ListArticleByGroup(categoryId, group, page, pageSize));
            if (tmp == null || tmp.Count == 0) return new List<ArticleInfo>();
            res = new List<ArticleInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(3), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public List<ArticleInfo> BlockNewsByGroup(int categoryId, int page, int pageSize, int group=-2, bool isClearCache = false)
        {
            string strCacheKey = string.Format("BlockNewsByGroup_{0}_{1}_{2}_{3}", categoryId, group, page, pageSize);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            List<ArticleInfo> res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<ArticleInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<ArticleInfo>(SQL.BlockNewsByGroup(categoryId, page, pageSize, group));
            if (tmp == null || tmp.Count == 0) return new List<ArticleInfo>();
            res = new List<ArticleInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(3), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public List<ArticleInfo> ListArticleByView(bool isClearCache = false)
        {
            string strCacheKey = "TMV_ListArticleByView";
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<ArticleInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<ArticleInfo>(SQL.ListArticleByView());
            if (tmp == null || tmp.Count == 0) return new List<ArticleInfo>();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(3), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public List<ArticleInfo> ListArticleByOther(int articleId, int categoryId, int pageSize, int page, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_ListArticleByOther_{0}_{1}_{2}_{3}", articleId, categoryId, pageSize, page);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            List<ArticleInfo> res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<ArticleInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<ArticleInfo>(SQL.ListArticleByOther(articleId, categoryId, pageSize, page));
            if (tmp == null || tmp.Count == 0) return new List<ArticleInfo>();
            res = new List<ArticleInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public List<ArticleInfo> ListArticleByTitle(string title)
        {
            return CBO.FillCollection<ArticleInfo>(SQL.ListArticleByTitle(title));
        }
    }
}
