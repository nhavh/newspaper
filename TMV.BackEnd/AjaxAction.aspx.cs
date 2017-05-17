using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMV.Data.Entities;

namespace TMV.BackEnd
{
    public partial class AjaxAction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["t"] == "ListArticleByTitle")
            {
                Response.Clear();
                Response.ContentType = "text/html";
                Response.ContentEncoding = Encoding.UTF8;
                Response.Write(ListArticleByTitle(Request["q"]));
                Response.End();
            }
        }

        private string ListArticleByTitle(string title)
        {
            var articles = new ArticleController().ListArticleByTitle(title);
            var res = new StringBuilder();
            foreach (var info in articles)
            {
                res.Append(info.Title + "|");
                res.Append(info.ArticleId.ToString() + "\n");
            }
            return res.ToString();
        }
    }
}