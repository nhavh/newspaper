using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Globalization;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Net;
using System.IO;

namespace TMV.Utilities
{
    public class HtmlHelper
    {
        public static string MetaTag(string title, string description, string keywords, string permalink, string type, string thumbnail)
        {
            string htmlMeta = String.Empty;
            htmlMeta += "<meta charset=\"utf-8\">\r\n";
            htmlMeta += "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n";
            htmlMeta += "<meta content=\"INDEX,FOLLOW\" name=\"robots\" />\r\n";
            htmlMeta += "<meta name=\"viewport\" content=\"width=device-width\" />\r\n";
            htmlMeta += "<meta name=\"copyright\" content=\"THAM MY VIEN HOANG ANH\" />\r\n";
            htmlMeta += "<meta name=\"author\" content=\"THAM MY VIEN HOANG ANH\" />\r\n";
            htmlMeta += "<meta http-equiv=\"audience\" content=\"General\" />\r\n";
            htmlMeta += "<meta name=\"resource-type\" content=\"Document\" />\r\n";
            htmlMeta += "<meta name=\"distribution\" content=\"Global\" />\r\n";
            htmlMeta += "<meta name=\"revisit-after\" content=\"1 days\" />";
            htmlMeta += "<meta name=\"GENERATOR\" content=\"THAM MY VIEN HOANG ANH\" />\r\n";
            htmlMeta += "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\" />\r\n";
            htmlMeta += "<link rel=\"publisher\" href=\"https://plus.google.com/108708791863144596732/about?rel=author\" />\r\n";
            htmlMeta += "<title>{0}</title>\r\n";
            htmlMeta += "<meta name=\"description\" content=\"{1}\" />\r\n";
            htmlMeta += "<meta name=\"keywords\" content=\"{2}\" />\r\n";
            htmlMeta += "<meta itemprop=\"description\" content=\"{1}\">\r\n";
            htmlMeta += "<meta itemprop=\"name\" content=\"{0}\">\r\n";
            htmlMeta += "<meta itemprop=\"image\" content=\"{3}\" />\r\n";
            htmlMeta += "<meta property=\"og:url\" content=\"{4}\">\r\n";
            htmlMeta += "<meta property=\"og:title\" content=\"{0}\">\r\n";
            htmlMeta += "<meta property=\"og:image\" content=\"{3}\" />\r\n";
            htmlMeta += "<meta property=\"og:description\" itemprop=\"description\" content=\"{1}\" />\r\n";
            htmlMeta += "<meta property=\"og:type\" content=\"website\" />\r\n";
            htmlMeta += "<meta property=\"og:locale\" content=\"vi_VN\" />\r\n";
            htmlMeta += "<meta property=\"fb:app_id\" content=\"692928060812875\" />\r\n";

            return String.Format(htmlMeta, title, description, keywords, thumbnail, permalink);
        }
        public static string Author(double rating, int vote)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("<span itemprop=\"review\" itemscope itemtype=\"http://data-vocabulary.org/Review-aggregate\"><span itemprop=\"rating\">{0}</span> sao trên <span itemprop=\"count\">{1}</span> lượt đánh giá</span>", rating, vote));
            return sb.ToString();
        }
        public static string RemoveAllHtmlTag(string str)
        {
            Regex reg = new Regex("</?\\w+((\\s+\\w+(\\s*=\\s*(?:\".*?\"|'.*?'|[^'\">\\s]+))?)+\\s*|\\s*)/?>");

            while (reg.IsMatch(str))
            {
                str = reg.Replace(str, string.Empty);
            }
            return str;
        }
        public static void StartupScript(string script, Page page)
        {
            if (script != string.Empty)
                page.ClientScript.RegisterStartupScript(typeof(Page), "StartupScript", "<script type='text/javascript'>" + script + "</script>");
        }
        public static void Alert(string message, Page page)
        {
            if (message != string.Empty)
                page.ClientScript.RegisterStartupScript(typeof(Page), "AlertScript", "<script type='text/javascript'>alert(" + EscapeQuote(message) + ")</script>");
        }
        public static void AlertAndForward(string message,string UrlForward, Page page)
        {
            if (message != string.Empty)
                page.ClientScript.RegisterStartupScript(typeof(Page), "AlertScript", "<script type='text/javascript'>alert(" + EscapeQuote(message) + "); window.location ='" + UrlForward + "';</script>");
        }
        public static void Confirm(string message, string ifTrue, string ifFalse, Page page)
        {
            if (message != string.Empty)
                page.ClientScript.RegisterStartupScript(typeof(Page), "ConfirmScript", string.Format("<script type='text/javascript'>\r\nif (window.confirm({0}))\r\n{{{1}}}\r\nelse\r\n{{{2}}}\r\n</script>", EscapeQuote(message), ifTrue, ifFalse));
        }
        public static void Back(int step, Page page)
        {
            page.ClientScript.RegisterStartupScript(typeof(Page), "BackScript", "<script type='text/javascript'>history.go(" + step + ")</script>");
        }
        public static void Navigate(string href, Page page)
        {
            page.ClientScript.RegisterStartupScript(typeof(Page), "NavigateScript", "<script type='text/javascript'>location.href='" + href + "'</script>");
        }
        public static string EscapeQuote(string S)
        {
            if (S == null)
                return "''";
          
            StringBuilder sb = new StringBuilder("'", S.Length + 2);
            foreach (char c in S)
            {
                switch (c)
                {
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\'':
                        sb.Append("\\'");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    default:                       
                            sb.Append(c);
                        break;
                }
            }
            sb.Append("'");
            return sb.ToString();
        }
        public static string StandardizeFileName(string path, string filename)
        {
            string name = Path.GetFileNameWithoutExtension(filename).Trim();
            name = HtmlHelper.RemoveIllegalCharacters(name);
            if (name.Length > 70)
                name = name.Substring(0, 70);
            string ext = Path.GetExtension(filename);
            string result = name;
            int count = 1;
            string sFileName = name + ext;
            string sFilePath = System.IO.Path.Combine(path, sFileName);
            while (File.Exists(sFilePath) == true)
            {
                result = name + "(" + count.ToString() + ")";
                sFilePath = System.IO.Path.Combine(path, result) + ext;
                count++;
            }
            result = result + ext;
            return result;
        }
        public static string FolderPath(string path)
        {
            path = path + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.DayOfWeek.ToString("d") + "/";
            return path;
        }
        public static string RemoveIllegalCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            text = HttpUtility.HtmlDecode(text);
            text = text.ToLower().Trim();
            text = Regex.Replace(text, "đ", "d");
            text = RemoveDiacritics(text);
            text = Regex.Replace(text, "[^a-z0-9]", "-");

            text = text.Trim(new char[] { '-' });

            text = RemoveExtraHyphen(text);

            return HttpUtility.UrlEncode(text).Replace("%", string.Empty);
        }
        private static string RemoveExtraHyphen(string text)
        {
            if (text.Contains("--"))
            {
                text = text.Replace("--", "-");
                return RemoveExtraHyphen(text);
            }

            return text;
        }
        public static String RemoveDiacritics(string text)
        {
            String normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < normalized.Length; i++)
            {
                Char c = normalized[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString();
        }
        public static bool Validate(string text, string pattern)
        {
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return regex.IsMatch(text);
        }
        public static bool ValidatePhoneNumber(string phoneNum)
        {
            if (string.IsNullOrEmpty(phoneNum)) return false;
            string sMailPattern = @"^((09(\d){8})|(01(\d){9}))$";
            return Regex.IsMatch(phoneNum.Trim(), sMailPattern);
        }
        public static bool ValidateEmail(string email)
        {
            return Validate(email, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
        public static bool ValidateUrl(string url)
        {
            return Validate(url, @"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$");
        }
        public static bool IsNullOrEmpty(string value)
        {
            if (value != null)
                return (value.Trim().Length == 0);

            return true;
        }
        public static string EscapeQuote(DateTime d)
        {
            return d.ToString("\\'yyyy-MM-dd HH:mm:ss\\'");
        }
        public static string EscapeName(string s)
        {
            if (s.IndexOfAny(new char[] { '[', ']', '*', '.', ' ', '(', ')' }) != -1)
                return s;
            else
                return "[" + s + "]";
        }
        public static string EscapeQuoteUnicode(string s)
        {
            return "N'" + s.Trim().Replace("'", "''") + "'";
        }
        public static string UnicodeForFullText(string s)
        {
            s = s.Replace("'", "''");
            s = s.Replace("\"", "");
            s = "\"" + s + "\"";

            return s;
        }
        public static string UnicodeForSearchLike(string s)
        {
            s = s.Replace("'", "");
            s = "N'%" + s + "%'";

            return s;
        }
        public static string ParseInt(string s)
        {
            try
            {
                if (!IsNullOrEmpty(s))
                {
                    int i = int.Parse(s);
                    if (i != int.MinValue)
                        return i.ToString();
                }
            }
            catch { }
            return "NULL";
        }
        public static string FormatString(int count, string str)
        {
            if (str != null)
            {
                str = Regex.Replace(str, "<[^>]*>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline); 
                if (str.Length > count)
                {                                      
                    str = str.Substring(0, count);
                    if (str.LastIndexOf(" ") > 0)
                        str = str.Substring(0, str.LastIndexOf(" "));

                    str = str + "...";
                }
            }
            return str;
        }
        public static string OutLink(string content, string linkOut)
        {
            Regex rgTitle = new Regex(@"<(p*)(?=[^>]*pTitle).*?>(.*?)</\1>");
            Regex rgSubTitle = new Regex(@"<(p*)(?=[^>]*pSubTitle).*?>(.*?)</\1>");
            Regex rgpSource = new Regex(@"<(p*)(?=[^>]*pSource).*?>(.*?)</\1>");
            if (rgTitle.IsMatch(content))
            {
                string title = rgTitle.Match(content).Value;
                content = content.Replace(title, title.Replace("<p", "<h1").Replace("</p>", "</h1>"));
            }
            if (rgSubTitle.IsMatch(content))
            {
                string subtitle = rgSubTitle.Match(content).Value;
                content = content.Replace(subtitle, subtitle.Replace("<p", "<h2").Replace("</p>", "</h2>"));
            }
            if (rgpSource.IsMatch(content))
            {
                string pSource = rgpSource.Match(content).Value;
                content = content.Replace(pSource, pSource.Replace("<p", "<h2").Replace("</p>", "</h2>"));
            }
            MatchCollection mCol = Regex.Matches(content, @"<a[\s]+[^>]*?href[\s]?=[\s\""\']+(.*?)[\""\']+.*?>([^<]+|.*?)?<\/a>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (mCol != null)
            {
                foreach (Match m in mCol)
                {
                    if (m.Groups.Count == 3)
                    {
                        if (m.Groups[1].Value.Contains(linkOut) == false)
                        {
                            if (m.Value.Contains("_blank") && m.Value.Contains("nofollow"))
                                content = content.Replace("\"" + m.Groups[1].Value + "\"", "\"" + linkOut + "/Go.aspx?url=" + HttpUtility.UrlEncode(m.Groups[1].Value) + "\"");
                            else
                                content = content.Replace("\"" + m.Groups[1].Value + "\"", "\"" + linkOut + "/Go.aspx?url=" + HttpUtility.UrlEncode(m.Groups[1].Value) + "\" target='_blank' rel='nofollow'");
                        }
                    }
                }
            }
            return content;
        }
        public static string RemoveTitle(string content)
        {
            Regex rgTitle = new Regex(@"<(p*)(?=[^>]*pTitle).*?>(.*?)</\1>");

            if (rgTitle.IsMatch(content))
            {
                string title = rgTitle.Match(content).Value;
                content = content.Replace(title, "");
            }
            return content;
        }
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static string ShowTime(DateTime d)
        {
            string result = "";
            DateTime today = DateTime.Now;
            double t = (today - d).TotalDays;
            if (t > 2.0)
            {
                result = d.ToString("dd/MM/yyyy") + ", lúc " + d.ToString("HH:mm", new System.Globalization.CultureInfo("en-US"));
            }
            else
            {
                if (today.Day - d.Day > 1)
                {
                    result = d.ToString("dd/MM/yyyy") + ", lúc " + d.ToString("HH:mm", new System.Globalization.CultureInfo("en-US"));
                }
                else if (t > 1.0)
                {
                    result = "Hôm qua, lúc " + d.ToString("HH:mm", new System.Globalization.CultureInfo("en-US"));
                }
                else
                {
                    if (d.Day == today.Day)
                    {
                        result = "Hôm nay, lúc " + d.ToString("HH:mm", new System.Globalization.CultureInfo("en-US"));
                    }
                    else
                    {
                        result = "Hôm qua, lúc " + d.ToString("HH:mm", new System.Globalization.CultureInfo("en-US"));
                    }
                }
            }
            return result;
        }
        public static string FormatFloat(double number)
        {
            return number.ToString("0.0").Replace(",", ".");
        }
        public static string CssInline(string fileName)
        {
            var cacheKey = string.Format("STATIC_CSS_INLINE_{0}", fileName);
            if (System.Web.HttpContext.Current.Request.QueryString["RemoveCssCache"] != null)
                System.Web.HttpContext.Current.Cache.Remove(cacheKey);
            var content = System.Web.HttpContext.Current.Cache.Get(cacheKey) as string;
            if (content != null) return string.Format("<style>{0}</style>", content);

            var path = System.Web.HttpContext.Current.Server.MapPath("~/Content/" + fileName);
            content = File.ReadAllText(path);
            content = content.Replace("images/", Globals.CDNUrl + "/Content/images/");
            System.Web.HttpContext.Current.Cache.Add(cacheKey, content, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return string.Format("<style>{0}</style>", content);
        }

        public static string CssInlineUrl(string fileName)
        {
            var cacheKey = string.Format("STATIC_CSS_INLINE_{0}", fileName);
            if (System.Web.HttpContext.Current.Request.QueryString["RemoveCssCache"] != null)
                System.Web.HttpContext.Current.Cache.Remove(cacheKey);
            var content = System.Web.HttpContext.Current.Cache.Get(cacheKey) as string;
            if (content != null) return string.Format("<style>{0}</style>", content);

            var path = System.Web.HttpContext.Current.Server.MapPath(fileName);
            content = File.ReadAllText(path);
            System.Web.HttpContext.Current.Cache.Add(cacheKey, content, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return string.Format("<style>{0}</style>", content);
        }

        public static string FormatMoney(decimal price)
        {
            return price == Null.NullDecimal ? "" : price.ToString("#,###", new CultureInfo("vi-VN")) + "₫";
        }
        public static string HtmlTags(string tag, string url)
        {
            string htmlTags = "";
            char[] delimiterChar = { ',' };
            string[] tags = tag.Split(delimiterChar, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tags.Length; i++)
            {
                htmlTags += "<a href='" + url + "'>" + tags[i].Trim() + "</a>";
            }

            return string.Format("<div class=\"tags\">{0}</div>", htmlTags);
        }

        public static string HtmlTagsNew(string tag, string url)
        {
            string htmlTags = "";
            char[] delimiterChar = { ',' };
            string[] tags = tag.Split(delimiterChar, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tags.Length; i++)
            {
                htmlTags += "<li><a class='tag' href='" + url + "'>" + tags[i].Trim() + "</a></li>";
            }

            return string.Format("<ul class=\"tags\">{0}</ul>", htmlTags);
        }


        public static string HtmlSource(string url, string text)
        {
            var htmlSource = "<div class=\"source\">Bài viết thuộc bản quyền <h2><a href='{0}'>{1}</a></h2>, <h2>theo chuyên mục <a href='{2}'>{3}</a></h2></div>";
            return string.Format(htmlSource, "/", "Thẩm mỹ viện Hoàng Anh", url, text);
        }

    }
}
