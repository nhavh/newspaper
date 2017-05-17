using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Globalization;
using System.Net;
using System.IO;
using System.Xml;
using System.Security.Cryptography;

namespace TMV.Utilities
{
    public class Globals
    {
        private static Random _rd = new Random();

        public static string InstanceExtension = ".aspx";
        public static string PathXML = "~/xml/";
        public static string RelativeWebRoot = VirtualPathUtility.ToAbsolute("~/");
        public static string ApplicationMapPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 1).Replace("/", "\\");
        public static string FrontEndUrl = WebConfigurationManager.AppSettings["FrontEndUrl"];
        public static string EmailTo = WebConfigurationManager.AppSettings["EmailTo"];
        public static string WebServiceFile = WebConfigurationManager.AppSettings["WebServiceFile"];
        public static string WebServiceImageHandler = WebConfigurationManager.AppSettings["WebServiceImageHandler"];
        public static string StaticUrl = WebConfigurationManager.AppSettings["StaticUrl"];
        public static string UploadUrl = WebConfigurationManager.AppSettings["StaticUrl"];
        public static string PathUploadFile = WebConfigurationManager.AppSettings["PathUploadFile"] ?? "~/Upload/";
        public static string UsersPreviewList = WebConfigurationManager.AppSettings["UsersPreviewList"];
        public static string UserBlackList = WebConfigurationManager.AppSettings["UserBlackList"];
        public static string DomainValue = WebConfigurationManager.AppSettings["DomainValue"];
        public static int PageSize = WebConfigurationManager.AppSettings["PageSize"] == null ? 10 : int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
        public static int TimeCache = WebConfigurationManager.AppSettings["TimeCache"] == null ? 5 : int.Parse(WebConfigurationManager.AppSettings["TimeCache"]);

        public static string HomeName = WebConfigurationManager.AppSettings["HomeName"] == null ? "TMV" : WebConfigurationManager.AppSettings["HomeName"];

        public static string CDNUrl = WebConfigurationManager.AppSettings["CDNUrl"];
        public static string PathNoImage = WebConfigurationManager.AppSettings["PathNoImage"];

        public static string StaticPathUploadDocument = WebConfigurationManager.AppSettings["StaticPathUploadDocument"] ?? "~/Upload/Document/";

        public static string StaticPathUploadFile = ConfigurationManager.AppSettings["StaticPathUploadFile"] ?? "~/Upload/File/";

        public enum BannerType
        {
            Image = 1,
            Text = 2
        }
        public enum PriorityBanner
        {
            PriceList = 1,
            Rating = 2,
            RightColum = 3,
            Home = 4,
            RightAngle = 5
        }

        public enum DocumentStatus
        {
            Editting = -1,
            Online = 0,
            WaittingApprove = 1,
            Approving = 2
        }
        public enum UserType
        {
            Member = 1,
            Store = 2,
            Admin = 3
        }
        public enum UserStatus
        {
            Register = 0,
            Normal = 1,
            Trial = 2,
            Member = 3,
            AccountBlock = 4,
            Customer = 5
        }
        
        public static string GetLocation(string address)
        {
            string url = "http://maps.google.com/maps/api/geocode/xml?address=" + address;
            string googleMap = GetWebContent(url);

            XmlDocument dom = new XmlDocument();
            dom.LoadXml(googleMap);

            XmlNodeList root = dom.GetElementsByTagName("location");
            List<string> list = new List<string>();

            foreach (XmlNode node in root)
            {
                XmlElement companyElement = (XmlElement)node;

                list.Insert(0, companyElement.GetElementsByTagName("lat")[0].InnerText);
                list.Insert(1, companyElement.GetElementsByTagName("lng")[0].InnerText);
            }

            return list.Count > 0 ? list[0] + "," + list[1] : "";
        }
        public static bool IsUserPreview(int userId)
        {
            return UsersPreviewList.Contains("," + userId.ToString() + ",");
        }
        public static string ImageUrl(string pathImage, int size, bool logo)
        {
            var filename = System.IO.Path.GetFileName(pathImage);
            if (string.IsNullOrEmpty(filename)) return PathNoImage;

            var folderName = logo ? "/img" : "/image";

            pathImage = pathImage.Contains("~/Upload")
                ? pathImage.Replace("~/Upload", folderName)
                : pathImage.Replace("~/", folderName + "/");

            //return CDNUrl + pathImage.Replace(filename, size + "_" + filename);
            return GetCDNUrl(CDNUrl) + pathImage.Replace(filename, size + "_" + filename);
        }

        public static string ImageUrlNoCDN(string pathImage, int size, bool logo)
        {
            var filename = System.IO.Path.GetFileName(pathImage);
            if (string.IsNullOrEmpty(filename)) return PathNoImage;

            var folderName = logo ? "/img" : "/image";

            pathImage = pathImage.Contains("~/Upload")
                ? pathImage.Replace("~/Upload", folderName)
                : pathImage.Replace("~/", folderName + "/");

            return CDNUrl + pathImage.Replace(filename, size + "_" + filename);
            //return GetCDNUrl(CDNUrl) + pathImage.Replace(filename, size + "_" + filename);
        }
        //Mã hóa chuỗi string bằng SHA 
        public static string SHA1Encryption(string strForEncryption)
        {
            byte[] result;
            SHA1 sHA1Managed = new SHA1Managed();
            result = sHA1Managed.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strForEncryption));
            System.Text.StringBuilder encryptedString = new System.Text.StringBuilder();
            foreach (byte outputByte in result)
                // convert each byte to a Hexadecimal upper case string
                encryptedString.Append(outputByte.ToString("x2").ToUpper());
            return encryptedString.ToString();
        }
        private static string GetCDNUrl(string cndUrl)
        {
            var index = _rd.Next(1, 4);
            return cndUrl.Replace("cdn", "cdn" + index);
        }

        private static void CreateImage(string url)
        {
            var uri = new Uri(url);
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;
        }
        private static string GetWebContent(string url)
        {
            var uri = new Uri(url);
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var output = reader.ReadToEnd();
            response.Close();

            return output;
        }
    }
}
