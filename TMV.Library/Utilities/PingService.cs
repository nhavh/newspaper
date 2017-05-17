using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Xml;

namespace TMV.Library.Utilities
{
    public class PingService
    {
        public string Title = "";
        public string Url = "";

        public PingService(string title, string url)
        {
            this.Title = title;
            this.Url = url;
        }

        public void Excute()
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(Send));
            th.IsBackground = true;
            th.Start();
        }

        private void Send()
        {
            StringCollection services = new StringCollection();
            services.Add("http://rpc.pingomatic.com/rpc2");
            services.Add("http://blogsearch.google.com/ping/RPC2");
            services.Add("http://blogsearch.google.com.vn/ping/RPC2");
            services.Add("http://blogsearch.google.co.cr/ping/RPC2");
            services.Add("http://services.newsgator.com/ngws/xmlrpcping.aspx");
            services.Add("http://ping.blo.gs/");
            services.Add("http://blogsearch.google.com.br/ping/RPC2");
            services.Add("http://blogsearch.google.com.au/ping/RPC2");
            services.Add("http://blogsearch.google.bg/ping/RPC2");
            services.Add("http://rpc.bloggerei.de/ping/");
            services.Add("http://blogsearch.google.co.uk/ping/RPC2");
            services.Add("http://blogsearch.google.ie/ping/RPC2");
            services.Add("http://blogsearch.google.lt/ping/RPC2");
            services.Add("http://blog.goo.ne.jp/XMLRPC");
            services.Add("http://ping.bitacoras.com");
            services.Add("http://blogsearch.google.cl/ping/RPC2");
            services.Add("http://blogsearch.google.co.th/ping/RPC2");
            services.Add("http://blogsearch.google.ae/ping/RPC2");
            services.Add("http://blogsearch.google.com.do/ping/RPC2");
            services.Add("http://blogsearch.google.co.hu/ping/RPC2");
            services.Add("http://audiorpc.weblogs.com/RPC2");
            services.Add("http://rpc.weblogs.com/RPC2");
            services.Add("http://www.a2b.cc/");
            services.Add("http://blogsearch.google.com.ua/ping/RPC2");
            services.Add("http://www.pingoo.jp/ping/");
            services.Add("http://blogsearch.google.it/ping/RPC2");
            services.Add("http://blogsearch.google.co.id/ping/RPC2");
            services.Add("http://ping.blogs.yandex.ru/RPC2");
            services.Add("http://blogsearch.google.ro/ping/RPC2");
            services.Add("http://blogsearch.google.es/ping/RPC2");
            services.Add("http://blogsearch.google.be/ping/RPC2");
            services.Add("http://blogsearch.google.com.my/ping/RPC2");
            services.Add("http://www.veneblogs.com/ping/");
            services.Add("http://blogsearch.google.co.in/ping/RPC2");
            services.Add("http://blogsearch.google.com.tr/ping/RPC2");
            services.Add("http://blogsearch.google.gr/ping/RPC2");
            services.Add("http://blogsearch.google.pt/ping/RPC2");
            services.Add("http://blogsearch.google.ru/ping/RPC2");
            services.Add("http://blogsearch.google.sk/ping/RPC2");
            services.Add("http://blogsearch.google.com.co/ping/RPC2");
            services.Add("http://www.weblogs.com/RPC2");
            services.Add("http://blogsearch.google.com.pe/ping/RPC2");
            services.Add("http://blogsearch.google.com.sg/ping/RPC2");
            services.Add("http://blogsearch.google.ch/ping/RPC2");
            services.Add("http://blogsearch.google.hr/ping/RPC2");
            services.Add("http://blogsearch.google.at/ping/RPC2");
            services.Add("http://blogsearch.google.com.ar/ping/RPC2");
            services.Add("http://blogsearch.google.us/ping/RPC2");
            services.Add("http://blogsearch.google.se/ping/RPC2");
            services.Add("http://blogsearch.google.com.sa/ping/RPC2");
            services.Add("http://blogsearch.google.com.uy/ping/RPC2");
            services.Add("http://blogsearch.google.co.il/ping/RPC2");
            services.Add("http://blogsearch.google.co.jp/ping/RPC2");
            services.Add("http://blogsearch.google.com.tw/ping/RPC2");
            services.Add("http://blogsearch.google.jp/ping/RPC2");
            services.Add("http://blogsearch.google.ca/ping/RPC2");
            services.Add("http://blogsearch.google.pl/ping/RPC2");
            services.Add("http://blogsearch.google.co.ma/ping/RPC2");
            services.Add("http://blogsearch.google.com.mx/ping/RPC2");
            services.Add("http://blogsearch.google.co.ve/ping/RPC2");
            services.Add("http://blogsearch.google.co.za/ping/RPC2");
            services.Add("http://blogsearch.google.de/ping/RPC2");
            services.Add("http://blogsearch.google.fi/ping/RPC2");
            services.Add("http://blogsearch.google.co.nz/ping/RPC2");
            services.Add("http://blogsearch.google.nl/ping/RPC2");
            services.Add("http://blogsearch.google.fr/ping/RPC2");
            services.Add("http://blogsearch.google.co.it/ping/RPC2");
            services.Add("http://ping.fc2.com");
            services.Add("http://blo.gs/ping.php");
            services.Add("http://blogpeople.net/ping");
            services.Add("http://ping.feedburner.com/");
            services.Add("http://pong.blo.gs");
            services.Add("http://www.blogdex.net/");
            services.Add("http://api.my.yahoo.co.jp/RPC2");
            services.Add("http://serenebach.net/rep.cgi");
            services.Add("http://rpc.reader.livedoor.com/ping");
            services.Add("http://ping.myblog.jp/");
            services.Add("http://rpc.twingly.com");
            services.Add("http://blogsearch.google.in/ping/RPC2");
            services.Add("http://blogsearch.google.tw/ping/RPC2");
            services.Add("http://geourl.org/ping/");
            services.Add("http://xmlrpc.bloggernetz.de/RPC2");
            services.Add("http://pinger.blogflux.com/rpc");
            services.Add("http://ping.bloggers.jp/rpc/");
            services.Add("http://pinger.blogflux.com/rpc/");
            services.Add("http://rpc.technorati.jp/rpc/ping");
            services.Add("");
            foreach (string service in services)
            {
                Execute(service);
            }
        }

        private void Execute(string service)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(service);
                request.Method = "POST";
                request.ContentType = "text/xml";
                request.Timeout = 3000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                AddXmlToRequest(request);
                request.GetResponse();
            }
            catch
            {
                //Exception ex1 = new Exception(service);
                //Exceptions.Logger.Error(ex1);
            }
        }

        private void AddXmlToRequest(HttpWebRequest request)
        {
            Stream stream = (Stream)request.GetRequestStream();
            using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.ASCII))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("methodCall");
                writer.WriteElementString("methodName", "weblogUpdates.ping");
                writer.WriteStartElement("params");
                writer.WriteStartElement("param");
                writer.WriteElementString("value", this.Title);
                writer.WriteEndElement();
                writer.WriteStartElement("param");
                writer.WriteElementString("value", this.Url);
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
    }
}
