using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class QuestionAnswerController
    {
        public void InsertQuestionAnswer(QuestionAnswerInfo info)
        {
            SQL.InsertQuestionAnswer(info.ArticleId, info.Title, info.Content, info.PublishDate, info.RoleId, info.AuthorId, info.EditorId, info.ApproverId, info.AdminNote, info.Question, info.Slug);
        }
        public void UpdateQuestionAnswer(QuestionAnswerInfo info)
        {
            SQL.UpdateQuestionAnswer(info.QuestionAnswerId, info.ArticleId, info.Title, info.Content, info.PublishDate, info.RoleId, info.AuthorId, info.EditorId, info.ApproverId, info.AdminNote, info.Question, info.Slug);
        }
        public void DeleteQuestionAnswer(QuestionAnswerInfo info)
        {
            SQL.DeleteQuestionAnswer(info.QuestionAnswerId);
        }
        public QuestionAnswerInfo GetQuestionAnswer(int questionAnswerId)
        {
            return CBO.FillObject<QuestionAnswerInfo>(SQL.GetQuestionAnswer(questionAnswerId));
        }
        public DataTable SelectQuestionAnswer(int articleId)
        {
            var questionAnswers = CBO.FillCollection<QuestionAnswerInfo>(SQL.ListQuestionAnswer(articleId));
            return CBO.ConvertToDataTable(questionAnswers, typeof(QuestionAnswerInfo));
        }
        public List<QuestionAnswerInfo> ListQuestionAnswerByArticle(int articleId, int page, int pageSize, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_ListQuestionAnswerByArticle_{0}_{1}_{2}", articleId, page, pageSize);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<QuestionAnswerInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<QuestionAnswerInfo>(SQL.ListQuestionAnswerByArticle(articleId, page, pageSize));
            if (tmp == null || tmp.Count == 0) return new List<QuestionAnswerInfo>();
            res = new List<QuestionAnswerInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public List<QuestionAnswerInfo> ListQuestionAnswerByOther(int articleId, int questionAnswerId, int page, int pageSize, bool isClearCache = false)
        {
            string strCacheKey = string.Format("TMV_ListQuestionAnswerByOther_{0}_{1}_{2}_{3}", articleId, questionAnswerId, page, pageSize);
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<QuestionAnswerInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<QuestionAnswerInfo>(SQL.ListQuestionAnswerByOther(articleId, questionAnswerId, page, pageSize));
            if (tmp == null || tmp.Count == 0) return new List<QuestionAnswerInfo>();
            res = new List<QuestionAnswerInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
        public QuestionAnswerInfo GetQuestionAnswerBySlug(string slug, bool isClearCache = false)
        {
            string strCacheKey = Globals.SHA1Encryption(string.Format("TMV_GetQuestionAnswerBySlug_{0}", slug));
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as QuestionAnswerInfo;
            if (res != null) return res;
            var tmp = CBO.FillObject<QuestionAnswerInfo>(SQL.GetQuestionAnswerBySlug(slug));
            if (tmp == null) return new QuestionAnswerInfo();
            res = tmp;
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
    }
}
