using System.Linq;
using System.Web.Mvc;
using TMV.Data.Entities;
using TMV.Utilities;

namespace Newspaper.FromtEnd.Controllers
{
    public class PagesController : Controller
    {
        private bool _isClearCache = false;
        private int _pageSize = 5;

        public PagesController()
        {
            _isClearCache = System.Web.HttpContext.Current.Request.QueryString["ClearCache"] != null;
        }
        public ActionResult Picture(int page = 1)
        {
            var objCategory = new CategoryController().GetCategoryBySlug("hinh-anh", _isClearCache);
            if (objCategory == null || objCategory.CategoryId == -1) Response.Redirect("/404/");

            var pictures = new PictureController().ListPictureByPaging(page, _pageSize, _isClearCache);
            var total = pictures.Count > 0 ? pictures.FirstOrDefault().Total : 0;
            var totalPage = (total % _pageSize == 0) ? (total / _pageSize) : (total / _pageSize + 1);
            var paging = new Paging()
            {
                Url = "/hinh-anh",
                Page = page,
                TotalPage = totalPage
            };

            ViewBag.Page = page;
            ViewBag.PageList = paging.LoadPaging();
            ViewBag.BreadCrumb = LoadBreadCrumb(objCategory);
            ViewBag.ObjCategory = objCategory;
            ViewBag.LineTitle = "Hình ảnh";

            return MvcApplication.IsMobileMode() ? View("Picture.M", pictures) : View(pictures);
        }
        public ActionResult PictureDetail(string slug)
        {
            var objPicture = new PictureController().GetPictureBySlug(slug);
            if (objPicture == null) Response.Redirect("/404/");

            ViewBag.BreadCrumb = LoadBreadCrumbDetail("/hinh-anh/", "Hình ảnh", objPicture.NavigationUrl, objPicture.Title);
            return MvcApplication.IsMobileMode() ? View("PictureDetail.M", objPicture) : View(objPicture);
        }
        public ActionResult Video(int page = 1)
        {
            var objCategory = new CategoryController().GetCategoryBySlug("video", _isClearCache);
            if (objCategory == null || objCategory.CategoryId == -1) Response.Redirect("/404/");

            var pictures = new VideoController().ListVideoByPaging(page, _pageSize, _isClearCache);
            var total = pictures.Count > 0 ? pictures.FirstOrDefault().Total : 0;
            var totalPage = (total % _pageSize == 0) ? (total / _pageSize) : (total / _pageSize + 1);
            var paging = new Paging()
            {
                Url = "/video",
                Page = page,
                TotalPage = totalPage
            };

            ViewBag.Page = page;
            ViewBag.PageList = paging.LoadPaging();
            ViewBag.BreadCrumb = LoadBreadCrumb(objCategory);
            ViewBag.ObjCategory = objCategory;
            ViewBag.LineTitle = "Video";

            return MvcApplication.IsMobileMode() ? View("Video.M", pictures) : View(pictures);
        }
        public ActionResult VideoDetail(string slug)
        {
            var objVideo = new VideoController().GetVideoBySlug(slug);
            if (objVideo == null) Response.Redirect("/404/");

            ViewBag.BreadCrumb = LoadBreadCrumbDetail("/video/", "Video", objVideo.NavigationUrl, objVideo.Title);
            return MvcApplication.IsMobileMode() ? View("VideoDetail.M", objVideo) : View(objVideo);
        }
        public ActionResult BoxOtherVideo(int videoId)
        {
            var video = new VideoController().ListVideoByOther(videoId, 6 ,_isClearCache);
            return MvcApplication.IsMobileMode() ? PartialView("BoxOtherVideo.M", video) : PartialView(video);
        }

        public ActionResult PriceListNew()
        {
            var objCategory = new CategoryController().GetCategoryBySlug("bang-gia", _isClearCache);
            if (objCategory == null || objCategory.CategoryId == -1) Response.Redirect("/404/");

            var prices = new BannerController().ListBannerByPriority((byte)Globals.PriorityBanner.PriceList, _isClearCache);
            if (prices != null && prices.Count > 0)
                prices = prices.Where(p => p.IsMobile == false).OrderBy(c => c.Position).ToList();

            ViewBag.BreadCrumb = LoadBreadCrumb(objCategory);
            ViewBag.ObjCategory = objCategory;

            return View(prices);
        }

        public ActionResult PriceList()
        {
            var objCategory = new CategoryController().GetCategoryBySlug("bang-gia", _isClearCache);
            if (objCategory == null || objCategory.CategoryId == -1) Response.Redirect("/404/");

            var isMobile = MvcApplication.IsMobileMode();
            var prices = new BannerController().ListBannerByPriority((byte)Globals.PriorityBanner.PriceList, _isClearCache);
            if (prices != null && prices.Count > 0)
                prices = prices.Where(p => p.IsMobile == isMobile).ToList();

            ViewBag.BreadCrumb = LoadBreadCrumb(objCategory);
            ViewBag.ObjCategory = objCategory;

            return isMobile ? View("PriceList.M", prices) : View(prices);
        }
        public ActionResult PriceDetail(string slug)
        {
            var objBanner = new BannerController().GetBannerBySlug(slug, _isClearCache);
            if (objBanner == null) Response.Redirect("/404/");

            var isMobile = MvcApplication.IsMobileMode();
            var prices = new BannerController().ListBannerByPriority((byte)Globals.PriorityBanner.PriceList, _isClearCache);
            if (prices != null && prices.Count > 0)
                prices = prices.Where(p => p.IsMobile == isMobile).ToList();

            ViewBag.Prices = prices;
            ViewBag.BreadCrumb = LoadBreadCrumbDetail("/bang-gia/", "Bảng giá", objBanner.NavigationUrl, objBanner.Title);
            return View(objBanner);
        }
        public ActionResult QADetail(string slug)
        {
            var objQA = new QuestionAnswerController().GetQuestionAnswerBySlug(slug, _isClearCache);
            if (objQA == null) Response.Redirect("/404/");

            var questionAnswers = new QuestionAnswerController().ListQuestionAnswerByArticle(objQA.ArticleId, 0, -1, _isClearCache);

            ViewBag.QuestionAnswers = questionAnswers;
            ViewBag.BreadCrumb = LoadBreadCrumbDetail("/hoi-dap/", "Hỏi đáp", objQA.NavigationUrl, objQA.Title);
            return MvcApplication.IsMobileMode() ? View("QADetail.M", objQA) : View(objQA);
        }
        public ActionResult BoxQAOther(int articleId, int questionAnswerId)
        {
            var questionAnswers = new QuestionAnswerController().ListQuestionAnswerByOther(articleId, questionAnswerId, 0, -1, _isClearCache);

            return PartialView(questionAnswers);
        }
        public ActionResult SiteMap()
        {
            var objCategory = new CategoryController().GetCategoryBySlug("site-map", _isClearCache);
            if (objCategory == null || objCategory.CategoryId == -1) Response.Redirect("/404/");

            var categories = new CategoryController().ListCategoryByGroup();

            ViewBag.BreadCrumb = LoadBreadCrumb(objCategory);
            ViewBag.ObjCategory = objCategory;
            return PartialView(categories);
        }
        
        private string LoadBreadCrumb(CategoryInfo categoryInfo)
        {
            string breadCrumb = "<section class=\"wrapper bread-crumb\"><div class=\"container\">";
            breadCrumb += string.Format("<h2 itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\" class=\"rf\"><a href=\"{0}\" itemprop=\"url\"><span itemprop=\"title\">{1}</span></a></h2>", Globals.FrontEndUrl, "Thẩm mỹ viện Hoàng Anh");
            breadCrumb += string.Format("<h2 itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\" class='rff'><a href=\"{0}\" itemprop=\"url\"><span itemprop=\"title\">{1}</span></a></h2>", categoryInfo.NavigationUrl, categoryInfo.CategoryName);
            breadCrumb += "</div></section>";
            return breadCrumb;
        }
        private string LoadBreadCrumbDetail(string categoryUrl, string categoryName, string url, string text)
        {
            string breadCrumb = "<section class=\"wrapper bread-crumb\"><div class=\"container\">";
            breadCrumb += string.Format("<h2 itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\" class=\"rf\"><a href=\"{0}\" itemprop=\"url\"><span itemprop=\"title\">{1}</span></a></h2>", Globals.FrontEndUrl, "Thẩm mỹ viện Hoàng Anh");
            breadCrumb += string.Format("<h2 itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\" class=\"rf\"><a href=\"{0}\" itemprop=\"url\"><span itemprop=\"title\">{1}</span></a></h2>", categoryUrl, categoryName);
            breadCrumb += string.Format("<h2 itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\" class='rff'><a href=\"{0}\" itemprop=\"url\"><span itemprop=\"title\">{1}</span></a></h2>", url, text);
            breadCrumb += "</div></section>";
            return breadCrumb;
        }
    }
}
