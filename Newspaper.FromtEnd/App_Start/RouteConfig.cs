using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Newspaper.FromtEnd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Mặc định
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = @"^Home$|^Common$|^Ajax$|^Article$|^Pages$|^SiteMap$" }
            );
            #endregion

            #region 404
            routes.MapRoute(
                name: "404",
                url: "404",
                defaults: new { controller = "Common", action = "CatchAll404" }
            );
            #endregion

            routes.MapRoute(
                name: "LienHe",
                url: "lien-he",
                defaults: new { controller = "LienHe", action = "Index" }
            );

            #region Redirect
            routes.MapRoute(
                name: "RedirectKienthuc",
                url: "blog",
                defaults: new { controller = "Redirect", action = "RedirectKienThuc" }
            );
            
            routes.MapRoute(
               name: "RedirectKienthuc100",
               url: "cham-soc-da-theo-do-tuoi",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail100" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc101",
               url: "hoc-cham-soc-da-o-dau",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail101" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc102",
               url: "blog/truong-hop-nao-nen-cat-mi-mat-o-nguoi-lon-tuoi",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail102" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc103",
               url: "blog/truong-hop-nao-nen-ap-dung-phuong-phap-cat-mi-mat",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail103" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc104",
               url: "Contact",
               defaults: new { controller = "Redirect", action = "RedirectLienHe" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc105",
               url: "bang-gia/bang-gia-phau-thuat-vung-mat",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail105" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc106",
               url: "bang-gia/bang-gia-tham-my-nguc-mong",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail106" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc107",
               url: "bang-gia/bang-gia-tham-my-bung-dui",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail107" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc108",
               url: "bang-gia/bang-gia-tham-my-noi-khoa",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail108" }
           );

            routes.MapRoute(
               name: "RedirectKienthuc109",
               url: "bang-gia/bang-gia-dao-tao",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail109" }
           );

            routes.MapRoute(
               name: "RedirectKienthuc110",
               url: "bang-gia/bang-gia-spa-cham-soc-da",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail110" }
           );

            routes.MapRoute(
               name: "RedirectKienthuc111",
               url: "bung",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail111" }
           );

            routes.MapRoute(
               name: "RedirectKienthuc112",
               url: "bac-si-kim",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail112" }
           );

            routes.MapRoute(
               name: "RedirectKienthuc113",
               url: "bac-si-hoang",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail113" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc114",
               url: "dich-vu",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail114" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc115",
               url: "content/dieu-tri-da",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail115" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc116",
               url: "content/dao-tao-hoc-vien",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail116" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc117",
               url: "content/phun-theu-tham-my",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail117" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc118",
               url: "content/cham-soc-da-0",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail118" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc119",
               url: "bam-mi-mat-0",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail119" }
           );
            routes.MapRoute(
               name: "RedirectKienthuc120",
               url: "triet-long-vinh-vien-0",
               defaults: new { controller = "Redirect", action = "RedirectKienThucDetail120" }
           );

            #endregion

            #region Site Map
            routes.MapRoute(
                name: "SiteMap",
                url: "site-map",
                defaults: new { controller = "Pages", action = "SiteMap" }
            );
            #endregion

            #region CamNhanHocVien
            routes.MapRoute(
                name: "CamNhanHocVien",
                url: "cam-nhan-hoc-vien",
                defaults: new { controller = "CustomerRV", action = "Index" }
            );
            #endregion
            #region Hình ảnh
            routes.MapRoute(
                name: "Picture",
                url: "hinh-anh",
                defaults: new { controller = "Pages", action = "Picture" }
            );
            routes.MapRoute(
                name: "PicturePaging",
                url: "hinh-anh/trang-{page}",
                defaults: new { controller = "Pages", action = "Picture", page = UrlParameter.Optional },
                constraints: new { page = @"([\d]+)" }
            );
            routes.MapRoute(
                name: "PictureDetail",
                url: "hinh-anh/{slug}",
                defaults: new { controller = "Pages", action = "PictureDetail", slug = UrlParameter.Optional },
                constraints: new { slug = @"([_0-9a-z-]+)" }
            );
            #endregion

            #region Video
            routes.MapRoute(
                name: "Video",
                url: "video",
                defaults: new { controller = "Pages", action = "Video" }
            );
            routes.MapRoute(
                name: "VideoPaging",
                url: "video/trang-{page}",
                defaults: new { controller = "Pages", action = "Video", page = UrlParameter.Optional },
                constraints: new { page = @"([\d]+)" }
            );
            routes.MapRoute(
                name: "VideoDetail",
                url: "video/{slug}",
                defaults: new { controller = "Pages", action = "VideoDetail", slug = UrlParameter.Optional },
                constraints: new { slug = @"([_0-9a-z-]+)" }
            );
            #endregion

            #region Bảng giá
            routes.MapRoute(
                name: "PriceList",
                url: "bang-gia",
                defaults: new { controller = "Pages", action = "PriceListNew" }
            );
            routes.MapRoute(
                name: "PriceListDetail",
                url: "bang-gia/{slug}",
                defaults: new { controller = "Pages", action = "PriceDetail", slug = UrlParameter.Optional },
                constraints: new { slug = @"([_0-9a-z-]+)" }
            );
            #endregion

            #region Hỏi đáp
            routes.MapRoute(
                name: "QADetail",
                url: "hoi-dap/{slug}",
                defaults: new { controller = "Pages", action = "QADetail", slug = UrlParameter.Optional },
                constraints: new { slug = @"([_0-9a-z-]+)" }
            );
            #endregion

            #region Tin tức
            routes.MapRoute(
                name: "ArticleCategory",
                url: "{categorySlug}",
                defaults: new { controller = "Article", action = "Category", categorySlug = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ArticleCategoryPaging",
                url: "{categorySlug}/trang-{page}",
                defaults: new { controller = "Article", action = "Category", categorySlug = UrlParameter.Optional, page = UrlParameter.Optional },
                constraints: new { categorySlug = @"([_0-9a-z-]+)", page = @"([\d]+)" }
            );
            routes.MapRoute(
                name: "ArticleDetail",
                url: "{categorySlug}/{slug}",
                defaults: new { controller = "Article", action = "Detail", categorySlug = UrlParameter.Optional, slug = UrlParameter.Optional },
                constraints: new { categorySlug = @"([_0-9a-z-]+)", slug = @"([_0-9a-z-]+)" }
            );
            #endregion

            routes.MapRoute(
                name: "CatchAll404",
                url: "{*dynamicRoute}",
                defaults: new { controller = "Common", action = "CatchAll404" }
            );


        }
    }
}