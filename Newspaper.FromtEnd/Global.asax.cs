using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using Newspaper.FromtEnd;
using TMV.Utilities;

namespace Newspaper.FromtEnd
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Func<HttpContextBase, bool> contextCheckDelegate = ContextCheck;
            var mobileMode = new DefaultDisplayMode("Mobile") { ContextCondition = contextCheckDelegate };
            DisplayModeProvider.Instance.Modes.Add(mobileMode);
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfig.RegisterGlobalConfig();
        }
        public static bool ContextCheck(HttpContextBase objHttpContextBase)
        {
            return IsMobileMode();
        }
        public static bool IsMobileMode()
        {
            var request = HttpContext.Current.Request;
            var contextBase = new HttpContextWrapper(HttpContext.Current);
            // Auto detect
            if (request.Cookies["CK_TMV_WEB_VERSION"] != null &&
               request.Cookies["CK_TMV_WEB_VERSION"].Value == "MOBILE")
            {
                if (request.Browser.IsMobileDevice == false)
                {
                    contextBase.ClearOverriddenBrowser();
                }
                contextBase.SetOverriddenBrowser(BrowserOverride.Mobile);

                return true;
            }
            if (request.Cookies["CK_TMV_WEB_VERSION"] != null &&
                request.Cookies["CK_TMV_WEB_VERSION"].Value == "DESKTOP")
            {
                if (request.Browser.IsMobileDevice)
                {
                    contextBase.ClearOverriddenBrowser();
                }

                contextBase.SetOverriddenBrowser(BrowserOverride.Desktop);
                return false;
            }

            var u = request.ServerVariables["HTTP_USER_AGENT"];
            Regex b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            if ((b.IsMatch(u) || v.IsMatch(u.Substring(0, 4))) || request.Browser.IsMobileDevice)
            {
                contextBase.SetOverriddenBrowser(BrowserOverride.Mobile);
                return true;
            }
            contextBase.SetOverriddenBrowser(BrowserOverride.Desktop);
            return false;
        }

        protected void Application_BeginRequest()
        {
            #region Redirect
            Redirect();
            #endregion

            #region SetCookie Device

            // lưu cookie phiên bản khi chuyển từ bản mobile
            if (Request.QueryString["sclient"] == null) return;

            switch (Request.QueryString["sclient"])
            {
                case "full":
                    {
                        var versionCookie = new HttpCookie("CK_TMV_WEB_VERSION", "DESKTOP")
                        {
                            Expires = DateTime.Now.AddDays(30),
                            Domain = Globals.DomainValue
                        };
                        Response.Cookies.Add(versionCookie);
                        Response.Redirect(
                            Request.Url.ToString()
                                .Replace("?sclient=full", string.Empty)
                                .Replace("&sclient=full", string.Empty));

                    }
                    break;
                case "mobile":
                    {

                        var versionCookie = new HttpCookie("CK_TMV_WEB_VERSION", "MOBILE")
                        {
                            Expires = DateTime.Now.AddDays(30),
                            Domain = Globals.DomainValue
                        };
                        Response.Cookies.Add(versionCookie);
                        Response.Redirect(
                            Request.Url.ToString()
                                .Replace("?sclient=mobile", string.Empty)
                                .Replace("&sclient=mobile", string.Empty));
                    }
                    break;
            }

            #endregion
        }

        private void Redirect()
        {
            var url = Request.Url.ToString();
            if (url.Contains("www")) Response.Redirect(url.Replace("www.", ""));

            var urlCurrent = Request.RawUrl.ToLower();
            if (urlCurrent == "/") return;
            if (urlCurrent.Contains("/home")) Response.Redirect("/");
            if (urlCurrent.Contains("?")) urlCurrent = urlCurrent.Substring(urlCurrent.IndexOf("?") + 1);

            var array = urlCurrent.Split('/');
            string exception = "/image/images/img/ajax/article/common/home/pages/sitemap/";
            if (array != null && !array.Any(p => exception.Contains("/" + p.ToLower() + "/")) && array.Length > 1 && !string.IsNullOrEmpty(array[array.Length - 1]))
                Response.Redirect(urlCurrent + "/");
        }
    }
}