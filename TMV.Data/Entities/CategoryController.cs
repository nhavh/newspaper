using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class CategoryController
    {
        public int InsertCategory(CategoryInfo info)
        {
            return SQL.InsertCategory(info.ParentId, info.CategoryName, info.Slug, info.SeoH1, info.SeoTitle, info.SeoKeyword, info.SeoDescription, info.Group, info.Avatar, info.IsShowDetail, info.IsShowMenuTop, info.IsShowMenuBot, info.OrderBy);
        }
        public void UpdateCategory(CategoryInfo info)
        {
            SQL.UpdateCategory(info.CategoryId, info.ParentId, info.CategoryName, info.Slug, info.SeoH1, info.SeoTitle, info.SeoKeyword, info.SeoDescription, info.Group, info.Avatar, info.IsShowDetail, info.IsShowMenuTop, info.IsShowMenuBot, info.OrderBy);
        }
        public void DeleteCategory(CategoryInfo info)
        {
            DeleteCategory(info.CategoryId);
        }
        public void DeleteCategory(int categoryId)
        {
            SQL.DeleteCategory(categoryId);
        }
        public CategoryInfo GetCategory(int categoryId)
        {
            return CBO.FillObject<CategoryInfo>(SQL.GetCategory(categoryId));
        }
        public List<CategoryInfo> ListCategory()
        {
            return CBO.FillCollection<CategoryInfo>(SQL.ListCategory());
        }
        public List<CategoryInfo> ListCategoryByGroup(int group = 0, bool isClearCache = false)
        {
            //return CBO.FillCollection<CategoryInfo>(SQL.ListCategoryByGroup(group));

            string strCacheKey = string.Format("TMV_ListCategoryByGroup_{0}", group);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<CategoryInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<CategoryInfo>(SQL.ListCategoryByGroup(group));
            if (tmp == null) return new List<CategoryInfo>();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public List<CategoryInfo> ListCategory(int type = 1, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_ListCategoryByGroup_{0}{1}", Globals.HomeName, type);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<CategoryInfo>;
            if (res != null) return res;
            var tmp = type == 1
                ? CBO.FillCollection<CategoryInfo>(SQL.ListCategoryTop())
                : CBO.FillCollection<CategoryInfo>(SQL.ListCategoryBot());
            if (tmp == null) return new List<CategoryInfo>();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public DataTable SelectCategory()
        {
            return CBO.ConvertToDataTable(ListCategory(), typeof(CategoryInfo));
        }
        public Dictionary<object, object> ListCategoryLookup()
        {
            var pages = ListCategory();
            pages = HelpBindCategory(pages);
            var dicpages = new Dictionary<object, object>();
            foreach (var info in pages)
                dicpages.Add(info.CategoryId, info.CategoryName);
            return dicpages;
        }
        public List<CategoryInfo> HelpBindCategory(List<CategoryInfo> categories)
        {
            var list = new List<CategoryInfo>();
            foreach (var info in categories)
            {
                if (info.CategoryId != info.ParentId) continue;

                list.Add(info);
                categories.Remove(info);
                break;
            }
            if (categories.Count > 0)
                SetIndent(categories, -1, 0, ref list);

            return list;
        }
        private static void SetIndent(List<CategoryInfo> pages, int categoryId, int level, ref List<CategoryInfo> listResult)
        {
            foreach (var info in pages)
            {
                if (info.ParentId != categoryId) continue;

                if (level != 0)
                    info.CategoryName = new string('.', level * 3) + info.CategoryName;

                listResult.Add(info);
                SetIndent(pages, info.CategoryId, level + 1, ref listResult);
            }
        }
        public CategoryInfo GetCategoryBySlug(string slug, bool isClearCache)
        {
            //return CBO.FillObject<CategoryInfo>(SQL.GetCategoryBySlug(slug));

            string strCacheKey = Globals.SHA1Encryption(string.Format("TMV_GetCategoryBySlug_{0}", slug));
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            CategoryInfo res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as CategoryInfo;
            if (res != null) return res;
            var tmp = CBO.FillObject<CategoryInfo>(SQL.GetCategoryBySlug(slug));
            if (tmp == null) return new CategoryInfo();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
    }
}
