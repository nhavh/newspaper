using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class CustomerReviewController
    {
        public void InsertCustomerReview(CustomerReviewInfo info)
        {
            SQL.InsertCustomerReview(info.ArticleId, info.Title, info.Avatar, info.Content, info.PublishDate, info.RoleId, info.AuthorId, info.EditorId, info.ApproverId, info.AdminNote);
        }
        public void UpdateCustomerReview(CustomerReviewInfo info)
        {
            SQL.UpdateCustomerReview(info.CustomerReviewId, info.ArticleId, info.Title, info.Avatar, info.Content, info.PublishDate, info.RoleId, info.AuthorId, info.EditorId, info.ApproverId, info.AdminNote);
        }
        public void DeleteCustomerReview(CustomerReviewInfo info)
        {
            SQL.DeleteCustomerReview(info.CustomerReviewId);
        }
        public CustomerReviewInfo GetCustomerReview(int customerReviewId)
        {
            return CBO.FillObject<CustomerReviewInfo>(SQL.GetCustomerReview(customerReviewId));
        }      
        public DataTable SelectCustomerReview(int articleId)
        {
            var customerReviews = CBO.FillCollection<CustomerReviewInfo>(SQL.ListCustomerReview(articleId));
            return CBO.ConvertToDataTable(customerReviews, typeof(CustomerReviewInfo));
        }
        public List<CustomerReviewInfo> ListCustomerReviewByArticle(int articleId, int page, int pageSize, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_ListCustomerReviewByArticle_{0}_{1}_{2}", articleId, page, pageSize);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<CustomerReviewInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<CustomerReviewInfo>(SQL.ListCustomerReviewByArticle(articleId, page, pageSize));
            if (tmp == null || tmp.Count == 0) return new List<CustomerReviewInfo>();
            res = new List<CustomerReviewInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
    }
}
