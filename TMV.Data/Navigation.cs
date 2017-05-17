using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data
{
    public class Navigation
    {
        public static string NavigateCategory(string categorySlug)
        {
            return Globals.FrontEndUrl + "/" + categorySlug + "/";
        }
        public static string NavigateCategoryChild(string parentSlug, string categorySlug)
        {
            return Globals.FrontEndUrl + Globals.RelativeWebRoot + categorySlug;
        }
        public static string NavigateArticleDetail(string categorySlug, string slug)
        {
            return Globals.FrontEndUrl + "/" + categorySlug + "/" + slug + "/";
        }
        public static string NavigatePictureDetail(string slug)
        {
            return Globals.FrontEndUrl + "/hinh-anh/" + slug + "/";
        }
        public static string NavigateVideoDetail(string slug)
        {
            return Globals.FrontEndUrl + "/video/" + slug + "/";
        }
        public static string NavigateQADetail(string slug)
        {
            return Globals.FrontEndUrl + "/hoi-dap/" + slug + "/";
        }
        public static string NavigatePriceDetail(string slug)
        {
            return Globals.FrontEndUrl + "/bang-gia/" + slug + "/";
        }
    }
}
