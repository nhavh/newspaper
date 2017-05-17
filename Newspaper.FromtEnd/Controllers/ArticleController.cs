using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMV.Data;
using TMV.Data.Entities;
using TMV.FrontEnd.TMVHoangAnh.Com;
using TMV.Utilities;

namespace Newspaper.FromtEnd.Controllers
{
    public class ArticleController : Controller
    {
        private bool _isClearCache = false;
        private int _pageSize = 6;

        public ArticleController()
        {
            _isClearCache = System.Web.HttpContext.Current.Request.QueryString["ClearCache"] != null;
        }

        [WhitespaceFilter]
        public ActionResult Category(string categorySlug = "", int page = 1)
        {
            var isMobile = MvcApplication.IsMobileMode();
            var objCategory = new CategoryController().GetCategoryBySlug(categorySlug, _isClearCache);
            if (objCategory.CategoryId == -1) return Redirect("/404/");

            //Hiển thị bài chi tiết
            var articleCtrl = new TMV.Data.Entities.ArticleController();
            if (objCategory.IsShowDetail)
            {
                var objArticle = articleCtrl.GetArticleByCategorySlug(categorySlug, _isClearCache);
                if (objArticle == null || objArticle.ArticleId <= 0 || objArticle.CategorySlug != categorySlug) Response.Redirect("/404/");

                var objBanner = new BannerController().GetBannerByCategoryId(objArticle.CategoryId, isMobile, _isClearCache);
                ViewBag.UrlPrice = objBanner != null && objBanner.BannerId > 0 ? objBanner.NavigationUrl : "/bang-gia/";

                ViewBag.ObjCategory = objCategory;
                ViewBag.BreadCrumb = LoadBreadCrumb(objCategory);
                ViewBag.LineTitle = objCategory.CategoryName;
                return PartialView("Detail", objArticle);
            }

            //Hiển thị danh sách bài viết
            if (isMobile) _pageSize = 10;
            var articles = objCategory.ParentId <= 0
                ? articleCtrl.ListArticleByGroup(objCategory.CategoryId, objCategory.CategoryId, page, _pageSize, _isClearCache)
                : articleCtrl.ListArticleByCategory(objCategory.CategoryId, page, _pageSize, _isClearCache);

            var total = articles.Count > 0 ? articles.FirstOrDefault().Total : 0;
            var totalPage = (total % _pageSize == 0) ? (total / _pageSize) : (total / _pageSize + 1);
            var paging = new TMV.Utilities.Paging()
            {
                Url = "/" + categorySlug,
                Page = page,
                TotalPage = totalPage
            };

            ViewBag.Page = page;
            ViewBag.PageList = paging.LoadPaging();
            ViewBag.BreadCrumb = LoadBreadCrumb(objCategory);
            ViewBag.ObjCategory = objCategory;
            ViewBag.LineTitle = objCategory.CategoryName;

            return View(articles);
        }

        [WhitespaceFilter]
        public ActionResult Detail(string categorySlug = "", string slug = "")
        {
            var objArticle = new TMV.Data.Entities.ArticleController().GetArticleBySlug(slug, _isClearCache);
            if (objArticle == null || objArticle.CategorySlug != categorySlug) Response.Redirect("/404/");

            //var isMobile = MvcApplication.IsMobileMode();
            //var objBanner = new BannerController().GetBannerByCategoryId(objArticle.CategoryId, isMobile, _isClearCache);

            //ViewBag.UrlPrice = objBanner != null && objBanner.BannerId > 0 ? objBanner.NavigationUrl : "/bang-gia/";
            ViewBag.BreadCrumb = LoadBreadCrumbDetail(objArticle);
            ViewBag.LineTitle = objArticle.CategoryName;
            
            return View(objArticle);
        }
        public ActionResult BoxTab(ArticleInfo objArticle)
        {
            return PartialView(objArticle);
        }
        public ActionResult BoxOther(int articleId, int categoryId)
        {
            var articles = new TMV.Data.Entities.ArticleController().ListArticleByOther(articleId, categoryId, 4, 1, _isClearCache);
            return PartialView(articles);
        }
        private string LoadBreadCrumb(CategoryInfo categoryInfo)
        {
            string breadCrumb = "<section class=\"wrapper bread-crumb\"><div class=\"container\">";
            breadCrumb += string.Format("<h2 itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\" class=\"rf\"><a href=\"{0}\" itemprop=\"url\"><span itemprop=\"title\">{1}</span></a></h2>", Globals.FrontEndUrl, "Thẩm mỹ viện Hoàng Anh");
            breadCrumb += string.Format("<h2 itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\" class='rff'><a href=\"{0}\" itemprop=\"url\"><span itemprop=\"title\">{1}</span></a></h1>", categoryInfo.NavigationUrl, categoryInfo.CategoryName);
            breadCrumb += "</div></section>";
            return breadCrumb;
        }
        private string LoadBreadCrumbDetail(ArticleInfo info)
        {
            string breadCrumb = "<section class=\"wrapper bread-crumb\"><div class=\"container\">";
            breadCrumb += string.Format("<h2 itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\" class=\"rf\"><a href=\"{0}\" itemprop=\"url\"><span itemprop=\"title\">{1}</span></a></h2>", Globals.FrontEndUrl, "Thẩm mỹ viện Hoàng Anh");
            breadCrumb += string.Format("<h2 itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\" class=\"rf\"><a href=\"{0}\" itemprop=\"url\"><span itemprop=\"title\">{1}</span></a></h2>", info.NavigationCategoryUrl, info.CategoryName);
            breadCrumb += string.Format("<h2 itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\" class='rff'><a href=\"{0}\" itemprop=\"url\"><span itemprop=\"title\">{1}</span></a></h2>", info.NavigationUrl, info.Title);
            breadCrumb += "</div></section>";
            return breadCrumb;
        }

    }
}
