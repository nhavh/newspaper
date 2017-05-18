using System;
using System.Linq;
using System.Web.Mvc;
using TMV.Data.Entities;
using TMV.Utilities;

namespace Newspaper.FromtEnd.Com.Controllers
{
    public class CommonController : Controller
    {
        private Random _rd = new Random();
        private bool _isClearCache = false;
        private string _cookieName = "CK_RIGHT_ANGLE";
        public CommonController()
        {
            _isClearCache = System.Web.HttpContext.Current.Request.QueryString["ClearCache"] != null;
        }
        public ActionResult BoxArticleView()
        {
            var articles = new ArticleController().ListArticleByView(_isClearCache);
            return PartialView(articles);
        }

        public ActionResult BoxArticleViewV2()
        {
            ViewBag.NoiBat2 = new ArticleController().GetArticle(901).Intro;
            //var articles = new TMV.Data.Entities.ArticleController().ListArticleByView(_isClearCache);
            return PartialView();
        }


        public ActionResult BoxQuestionAnswer()
        {
            var questionAnswers = new QuestionAnswerController().ListQuestionAnswerByArticle(0, 1, 10, _isClearCache);
            return MvcApplication.IsMobileMode()
                ? PartialView("BoxQuestionAnswer.M", questionAnswers)
                : PartialView(questionAnswers);
        }
        public ActionResult BoxCustomerReview()
        {
            var customerReviews = new CustomerReviewController().ListCustomerReviewByArticle(0, 1, 10, _isClearCache);
            return PartialView(customerReviews);
        }
        public ActionResult BoxLinkWebsite(byte priority = 0)
        {
            var linkWebsites = new LinkWebsiteController().ListLinkWebsiteByPriority(priority, _isClearCache);
            return PartialView(linkWebsites);
        }
        public ActionResult BoxFanpage()
        {
            return MvcApplication.IsMobileMode()
                ? PartialView("BoxFanpage.M")
                : PartialView();
        }
        public ActionResult BoxSponsor()
        {
            var sponsors = new SponsorController().ListSponsorByType(0, _isClearCache);
            return MvcApplication.IsMobileMode() ? PartialView("BoxSponsor.M", sponsors) : PartialView(sponsors);
        }
        public ActionResult BoxGoogleSearch()
        {
            return PartialView();
        }
        public ActionResult BoxRating(ArticleInfo objArticle)
        {
            return PartialView(objArticle);
        }
        public ActionResult BoxBanner(byte priority = 2)
        {
            if (priority == (byte)Globals.PriorityBanner.RightAngle)
            {
                if (Request.Cookies[_cookieName] != null) return null;

                //CreateCookie();
            }

            var banners = new BannerController().ListBannerByPriority(priority, _isClearCache);
            if (banners == null || banners.Count == 0) return null;

            if (priority == (byte)Globals.PriorityBanner.Rating)
            {
                var tmp = banners.ToArray();
                var bannerId = tmp[_rd.Next(0, tmp.Length)].BannerId;
                return PartialView(banners.Where(p => p.BannerId == bannerId).ToList());
            }
            return PartialView(banners);
        }
        public ActionResult BoxCategory(int categoryId = 4)
        {
            var categorys = new CategoryController().ListCategoryByGroup(categoryId, _isClearCache);
            return MvcApplication.IsMobileMode()
                ? PartialView("BoxCategory.M", categorys)
                : PartialView(categorys);
        }
        public ActionResult CatchAll404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult BoxSlider_V2()
        {
            var banners = new BannerController().ListBannerByPriority((int)Globals.PriorityBanner.Home, _isClearCache);
            return PartialView(banners);
        }
        

        public ActionResult Menu_V2(int type = 1)
        {
            var categories = new CategoryController().ListCategory(type, true);
            return PartialView(categories);
        }

        public ActionResult BlockLogin()
        {
            return PartialView();
        }
        public ActionResult BlockBannerTop()
        {
            return PartialView();
        }

        public ActionResult BlockMenu()
        {
            return PartialView();
        }

        public ActionResult BlockSliderText()
        {
            return PartialView();
        }
        public ActionResult BlockBanner4()
        {
            return PartialView();
        }
        public ActionResult BlockDontMiss()
        {
            return PartialView();
        }
        public ActionResult BlockLifeStyleNews()
        {
            return PartialView();
        }
        public ActionResult BlockHouseDesign()
        {
            return PartialView();
        }
        public ActionResult BlockTechAndGadgets()
        {
            return PartialView();
        }
        public ActionResult BlockLeftStayConnected()
        {
            return PartialView();
        }
        public ActionResult BlockLeftBanner()
        {
            return PartialView();
        }
        public ActionResult BlockLeftBanner1()
        {
            return PartialView();
        }
        public ActionResult BlockLeftItModern()
        {
            return PartialView();
        }
        public ActionResult BlockLeftReviews()
        {
            return PartialView();
        }
        public ActionResult BlockLeftHolidayRecipes()
        {
            return PartialView();
        }
        public ActionResult BlockTraining()
        {
            return PartialView();
        }
        public ActionResult BlockBannerCenter()
        {
            return PartialView();
        }
        public ActionResult BlockBannerCenter1()
        {
            return PartialView();
        }
        public ActionResult BlockWrcRacing()
        {
            return PartialView();
        }
        public ActionResult BlockHealthFitness()
        {
            return PartialView();
        }

        public ActionResult BlockBusiness()
        {
            return PartialView();
        }
        public ActionResult BlockMusic()
        {
            return PartialView();
        }

        public ActionResult BlockLeftMostPopular()
        {
            return PartialView();
        }
        public ActionResult BlockLeftRecentComments()
        {
            return PartialView();
        }
        public ActionResult BlockLatestAticles()
        {
            return PartialView();
        }

    }
}
