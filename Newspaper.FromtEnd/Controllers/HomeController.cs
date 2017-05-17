using System.Web.Mvc;
using TMV.Data.Entities;

namespace Newspaper.FromtEnd.Controllers
{
    public class HomeController : Controller
    {
        private bool _isClearCache = false;
        public HomeController()
        {
            _isClearCache = System.Web.HttpContext.Current.Request.QueryString["ClearCache"] != null;
        }

        [WhitespaceFilter]
        public ActionResult Index()
        {
            var articleController = new TMV.Data.Entities.ArticleController();
            ViewBag.DichVuNoiBat = articleController.GetArticle(827).Intro;
            ViewBag.TaiSaoNenChon = articleController.GetArticle(826).Intro;
            //ViewBag.LineTitle = "Trang chủ";
            return View();
        }
        public ActionResult BoxArticleHome()
        {
            var article = new TMV.Data.Entities.ArticleController().ListArticleByGroup(1,4,1,4, _isClearCache);
            return PartialView(article);
        }
        public ActionResult BoxSponsor()
        {
            var sponsors = new SponsorController().ListSponsorByType(0, _isClearCache);
            return MvcApplication.IsMobileMode()
                ? PartialView("BoxSponsor.M", sponsors)
                : PartialView(sponsors);
        }
        public ActionResult BoxVideo()
        {
            var videos = new VideoController().ListVideoByHome(_isClearCache);
            //return MvcApplication.IsMobileMode()
            //    ? PartialView("BoxVideo.M", videos)
            //    : PartialView(videos);
            return PartialView(videos);
        }
        public ActionResult BoxPicture()
        {
            var pictures = new PictureController().ListPictureByHome(_isClearCache);
            return PartialView(pictures);
        }
        public ActionResult BoxCustomer()
        {
            var customers = new CustomerController().ListCustomerByHome(_isClearCache);

            return PartialView(customers);
        }
        //public ActionResult BoxSlider()
        //{
        //    var banners = new BannerController().ListBannerByPriority((int)TMV.Utilities.Globals.PriorityBanner.Home, _isClearCache);
        //    return PartialView(banners);
        //}
    }
}
