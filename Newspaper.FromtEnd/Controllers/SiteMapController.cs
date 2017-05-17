using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using TMV.Data.Entities;
using TMV.Utilities;

namespace Newspaper.FromtEnd.Controllers
{
    public class SiteMapController : Controller
    {
        private CategoryController ctrlCategory = new CategoryController();

        public ActionResult Index()
        {
            switch (Request.QueryString["filename"])
            {
                case "sitemap":
                    WriteSiteMap("sitemap.xml");
                    break;
                case "ror":
                    WriteRSS("ror.xml");
                    break;
                case "urllist":
                    WriteUrlList("urllist.txt");
                    break;
            }
            return View();
        }
        private struct Changefreqs
        {
            private static string _daily = "daily";
            public static string Daily
            {
                get { return _daily; }
                private set { _daily = value; }
            }

            private static string _always = "always";
            public static string Always
            {
                get { return _always; }
                set { _always = value; }
            }

            private static string _hourly = "hourly";
            public static string Hourly
            {
                get { return _hourly; }
                set { _hourly = value; }
            }

            private static string _weekly = "weekly";
            public static string Weekly
            {
                get { return _weekly; }
                set { _weekly = value; }
            }

            private static string _monthly = "monthly";
            public static string Monthly
            {
                get { return _monthly; }
                set { _monthly = value; }
            }

            private static string _yearly = "yearly";
            public static string Yearly
            {
                get { return _yearly; }
                set { _yearly = value; }
            }

            private static string _never = "never";
            public static string Never
            {
                get { return _never; }
                set { _never = value; }
            }
        }

        private void WriteSiteMap(string path)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(Server.MapPath("~/" + path), FileMode.OpenOrCreate, FileAccess.Write);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string strFormatDateTime = "yyyy-MM-ddThh:mm:sszzz";//Định dạng ngày tháng
            string strLastModified = DateTime.Now.ToString(strFormatDateTime);// .ToString(strFormatDateTime);

            using (XmlTextWriter writer = new XmlTextWriter(fs, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 0;
                writer.IndentChar = '\n';
                writer.WriteStartDocument();

                writer.WriteStartElement("urlset");
                writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");

                //Home
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", Globals.FrontEndUrl);
                writer.WriteElementString("lastmod", strLastModified);
                writer.WriteElementString("changefreq", Changefreqs.Daily);
                writer.WriteElementString("priority", "1.00");
                writer.WriteEndElement();

                //Categories
                foreach (CategoryInfo info in ctrlCategory.ListCategory())
                {
                    if (info.CategoryId > -1)
                    {
                        this.xmlWriteItem
                        (
                            writer,
                            info.NavigationUrl,
                            Changefreqs.Hourly,
                            "0.85"
                        );
                    }
                }


                //Danh sách top 10000
                foreach (var info in new TMV.Data.Entities.ArticleController().ListArticleByCategory(0, 1, 10000))
                {
                    this.xmlWriteItem
                    (
                        writer,
                        info.NavigationUrl,
                        Changefreqs.Hourly,
                        "0.65"
                    );
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();

                writer.Close();
            }

            Response.Redirect("/" + path);
        }

        private void WriteRSS(string path)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(Server.MapPath("~/" + path), FileMode.OpenOrCreate, FileAccess.Write);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string strFormatDateTime = "yyyy-MM-ddThh:mm:sszzz";//Định dạng ngày tháng
            string strLastModified = DateTime.Now.ToString(strFormatDateTime);// .ToString(strFormatDateTime);

            using (XmlTextWriter writer = new XmlTextWriter(fs, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                writer.IndentChar = '\n';
                writer.WriteStartDocument();

                writer.WriteStartElement("rss");
                writer.WriteAttributeString("xmlns:ror", "http://rorweb.com/0.1/");
                writer.WriteAttributeString("version", "2.0");


                writer.WriteStartElement("channel");
                writer.WriteElementString("title", string.Format("ROR Sitemap for {0}", Globals.FrontEndUrl));
                writer.WriteElementString("link", Globals.FrontEndUrl);
                writer.WriteElementString("copyright", Globals.FrontEndUrl);
                writer.WriteElementString("pubDate", strLastModified);
                writer.WriteEndElement();

                //Home
                writer.WriteStartElement("item");
                writer.WriteElementString("link", string.Format("{0}", Globals.FrontEndUrl));
                writer.WriteElementString("title", "Thẩm mỹ viện Hoàng Anh");
                writer.WriteElementString("description", "Thẩm mỹ viện Hoàng Anh");
                writer.WriteElementString("pubDate", strLastModified);
                writer.WriteElementString("ror:updatePeriod", Changefreqs.Daily);
                writer.WriteElementString("ror:sortOrder", "0");
                writer.WriteElementString("ror:resourceOf", "sitemap");
                writer.WriteEndElement();

                //Category
                foreach (CategoryInfo info in new CategoryController().ListCategory())
                {
                    if (info.CategoryId > -1)
                    {
                        RssWriteItem
                        (
                            writer,
                            info.CategoryName,
                            info.SeoDescription,
                            info.NavigationUrl,
                            "2"
                        );
                    }
                }

                //Danh sách 10.000 tin tức mới nhất
                foreach (var info in new TMV.Data.Entities.ArticleController().ListArticleByCategory(0, 1, 10000))
                {
                    this.RssWriteItem
                    (
                        writer,
                        info.SeoTitle,
                        info.SeoDescription,
                        info.NavigationUrl,
                        "2"
                    );
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();

                writer.Close();
            }

            Response.Redirect("/" + path);
        }

        private void WriteUrlList(string path)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(Server.MapPath("~/" + path), FileMode.OpenOrCreate, FileAccess.Write);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            const string stringFormat = "[url=\"{0}\"]{1}[/url]";

            using (var writer = new StreamWriter(fs, Encoding.UTF8))
            {

                writer.WriteLine(stringFormat, Globals.FrontEndUrl, "Thẩm mỹ viện Hoàng Anh");

                //Categories Store
                foreach (var info in ctrlCategory.ListCategory())
                {
                    writer.WriteLine(stringFormat, Globals.FrontEndUrl + "/" + info.Slug + "/", info.CategoryName);
                }

                //Danh sách 10000 tin tức mới nhất
                foreach (var info in new TMV.Data.Entities.ArticleController().ListArticleByCategory(0, 1, 10000))
                {
                    writer.WriteLine(stringFormat, info.NavigationUrl, info.Title);
                }

                writer.Flush();
                writer.Close();
            }

            Response.Redirect("/" + path);
        }

        private void RssWriteItem(XmlTextWriter writer, string title, string description, string url, string sortOrder)
        {
            writer.WriteStartElement("item");
            writer.WriteElementString("link", url);
            writer.WriteElementString("title", title);
            writer.WriteElementString("description", description);
            writer.WriteElementString("ror:updatePeriod", Changefreqs.Daily);
            writer.WriteElementString("ror:sortOrder", sortOrder);
            writer.WriteElementString("ror:resourceOf", "sitemap");
            writer.WriteEndElement();
        }

        private void xmlWriteItem(XmlTextWriter writer, string loc, string changefreq, string priority)
        {
            string strFormatDateTime = "yyyy-MM-ddThh:mm:sszzz";//Định dạng ngày tháng
            string strLastModified = DateTime.Now.ToString(strFormatDateTime);// .ToString(strFormatDateTime);

            writer.WriteStartElement("url");
            writer.WriteElementString("loc", loc);
            writer.WriteElementString("lastmod", strLastModified);
            writer.WriteElementString("changefreq", changefreq);
            writer.WriteElementString("priority", priority);
            writer.WriteEndElement();
        }
    }
}
