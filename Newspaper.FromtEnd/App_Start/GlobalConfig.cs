using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Newspaper.FromtEnd
{
    public class GlobalConfig
    {
        private static string frontEndUrl;
        public static string FrontEndUrl
        {
            get { return GlobalConfig.frontEndUrl; }
        }

        private static bool bolIsHtmlMinifyEnabled;
        public static bool IsHtmlMinifyEnabled
        {
            get { return GlobalConfig.bolIsHtmlMinifyEnabled; }
        }

        public static void RegisterGlobalConfig()
        {
            frontEndUrl = ConfigurationManager.AppSettings["FrontEndUrl"];
            if (string.IsNullOrEmpty(frontEndUrl))
                throw new ArgumentNullException("CDN Path is not configured");

            bolIsHtmlMinifyEnabled = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["IsHtmlMinifyEnabled"]) ?
                false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsHtmlMinifyEnabled"]));
        }
    }
}