﻿using System;
using System.Linq;
using System.Web.Mvc;
using TMV.Data.Entities;
using TMV.Utilities;

namespace Newspaper.FromtEnd.Com.Controllers
{
    public class CommonController : Controller
    {
        private Random _rd = new Random();
        private bool _isClearCache = true;
        private string _cookieName = "CK_RIGHT_ANGLE";
        int _pageSize = 5;
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

        #region news home

        public ActionResult BlockLogin()
        {
            return PartialView();
        }
        public ActionResult BlockBannerTop()
        {
            return PartialView();
        }

        public ActionResult BlockMenu(int type = 1)
        {
            var data = new CategoryController().ListMenu(type, true);
            return PartialView(data);
        }

        public ActionResult BlockSliderText()
        {
            var data = new BannerController().TextListByHome(1);
            return PartialView(data);
        }
        public ActionResult BlockBanner4()
        {
            var data = new BannerController().TextListByHome(2);
            return PartialView(data);
        }
        public ActionResult BlockDontMiss(int type = 2, int categoryid = -1, int groupid = -2, int page = 1)
        {
            var dataMenu = new CategoryController().ListMenuDontMiss(type, _isClearCache);
            var datanews = new ArticleController().BlockNewsByGroup(categoryid, page, _pageSize, groupid, _isClearCache);
            var rs = new BlockModel { ListMenu = dataMenu, ListNews = datanews };
            return PartialView(rs);
        }
        public ActionResult BlockLifeStyleNews()
        {
            return PartialView();
        }
        public ActionResult BlockHouseDesign()
        {
            return PartialView();
        }
        public ActionResult BlockTechAndGadgets(int type = 3, int categoryid = 2, int groupid = -2, int page = 1)
        {
            var dataMenu = new CategoryController().ListMenuDontMiss(type, _isClearCache);
            var datanews = new ArticleController().BlockNewsByGroup(categoryid, page, _pageSize, groupid, _isClearCache);
            var rs = new BlockModel { ListMenu = dataMenu, ListNews = datanews };
            return PartialView(rs);
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
        public ActionResult BlockLatestAticles(int categoryid = 3, int groupid = -2, int page = 1)
        {
            _pageSize = 12;
            var data = new ArticleController().BlockNewsByGroup(categoryid, page, _pageSize, groupid, _isClearCache);
            return PartialView(data);
        }

        public ActionResult BlockFooterEditPick()
        {
            return PartialView();
        }
        public ActionResult BlockFooterPopularPost()
        {
            return PartialView();
        }
        public ActionResult BlockFooterPopularCategory()
        {
            return PartialView();
        }

        public ActionResult BlockFooter()
        {
            return PartialView();
        }
        public ActionResult BlockHeader()
        {
            return PartialView();
        }

        #endregion


    }
}
