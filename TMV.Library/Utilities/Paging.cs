using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace TMV.Utilities
{
    public class Paging
    {
        public string Url { get; set; }
        public int Page { get; set; }
        public int TotalPage { get; set; }

        private readonly List<Link> _pageList = new List<Link>();

        private int _start = 1; // hien thi trang bat dau
        private int _end = 5; // hien thi trang ket thuc

        public string LoadPaging()
        {
            Init();

            if (_pageList.Count < 2) return String.Empty;

            var htmlPaging = _pageList.Aggregate("<div class=\"paging\"><ul class=\"pages\">", (current, info) => current + ("<li " + info.Css + ">" + info.Name + "</li>"));
            htmlPaging += "</ul></div>";
            return htmlPaging;
        }

        private void Init()
        {
            if (TotalPage <= 1) return;

            if (!String.IsNullOrEmpty(HttpContext.Current.Request["page"]))
            {
                Page = int.Parse(HttpContext.Current.Request["page"]);
            }

            if (Page - 2 > 1 && Page + 2 < TotalPage)
            {
                _start = Page - 2;
                _end = Page + 2;
            }
            else if (Page - 2 <= 1)
            {
                _start = 1;
                _end = _start + 4;
            }
            else if (Page + 2 >= TotalPage || TotalPage - Page == 1)
            {
                _end = TotalPage;
                if (Page == 4 && TotalPage == 4)
                { _start = 1; }
                else
                {
                    _start = _end - 4;
                }
            }
            if (_end > TotalPage)
                _end = TotalPage;

            var pageInfo = new Link();

            string formatUrl;
            var rgPage = new Regex("trang-([^/d]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (rgPage.IsMatch(Url))
            {
                var p = rgPage.Match(Url).Value;
                formatUrl = Url.Replace(p, p.Replace(p, "trang-[pagecurrent]"));
            }
            else
            {
                formatUrl = Url + "/trang-[pagecurrent]/";
            }

            if (Page > 1)//show preview
            {
                pageInfo.Url = formatUrl.Replace("/trang-[pagecurrent]/", "/");
                pageInfo.Name = "<a href='" + pageInfo.Url + "'><span>«</span></a>";
                _pageList.Add(pageInfo);

                pageInfo = Page == 2 
                    ? new Link {Url = formatUrl.Replace("/trang-[pagecurrent]/", "")}
                    : new Link {Url = formatUrl.Replace("[pagecurrent]", (Page - 1).ToString())};
                pageInfo.Name = "<a href='" + pageInfo.Url + "'><span>‹</span></a>";
                pageInfo.Css = "class='previous'";
                _pageList.Add(pageInfo);
            }
            for (var i = _start; i <= _end; i++)
            {
                pageInfo = i == 1 
                    ? new Link {Url = formatUrl.Replace("/trang-[pagecurrent]/", "/")}
                    : new Link {Url = formatUrl.Replace("[pagecurrent]", i.ToString())};

                pageInfo.Name = "<a href='" + pageInfo.Url + "'><span>" + i + "</span></a>";
                if (i == Page)
                {
                    pageInfo.Css = "class='selected'";
                }
                _pageList.Add(pageInfo);
            }

            if (Page >= TotalPage) return;

            pageInfo = new Link {Url = formatUrl.Replace("[pagecurrent]", (Page + 1).ToString())};

            pageInfo.Name = "<a href='" + pageInfo.Url + "'><span>›</span></a>";
            pageInfo.Css = "class='next'";

            _pageList.Add(pageInfo);

            pageInfo = new Link {Url = formatUrl.Replace("[pagecurrent]", TotalPage.ToString())};

            pageInfo.Name = "<a href='" + pageInfo.Url + "'><span>»</span></a>";
            _pageList.Add(pageInfo);
        }
    }
    public class Link
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Css { get; set; }
    }
}