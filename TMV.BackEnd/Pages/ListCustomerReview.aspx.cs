using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMV.Data.Entities;
using TMV.Framework;
using TMV.Utilities;

namespace TMV.BackEnd.Pages
{
    public partial class ListCustomerReview : AdminPageBase
    {
        protected string Title = String.Empty;
        protected string UrlAdd = "";
        protected string UrlArticles = "";

        private int _articleId = 0;
        private ArticleInfo _articleInfo = new ArticleInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ArticleId"]))
            {
                _articleId = int.Parse(Request.QueryString["ArticleId"]);
                UrlAdd = "/Pages/EditCustomerReview.aspx?ArticleId=" + _articleId;

                _articleInfo = new ArticleController().GetArticle(_articleId);
                Title = _articleInfo.Title;
                UrlArticles = GetRedirectUrl();
            }

            GridViewManager1.DataSource.SelectParameters.Clear();
            GridViewManager1.DataSource.SelectParameters.Add("ArticleId", TypeCode.Int32, _articleId.ToString());
            GridViewManager1.LoadData();
        }

        private string GetRedirectUrl()
        {
            string redirect;
            var curentRoleId = _articleInfo.RoleId;
            switch (curentRoleId)
            {
                case (int)Globals.DocumentStatus.Editting:
                    redirect = "/Pages/ListArticle.aspx?xml=ArticleEditting";
                    break;
                case (int)Globals.DocumentStatus.Online:
                    redirect = "/Pages/ListArticle.aspx?xml=ArticleOnline";
                    break;
                case (int)Globals.DocumentStatus.WaittingApprove:
                    redirect = "/Pages/ListArticle.aspx?xml=ArticleWaittingApprove";
                    break;
                default:
                    redirect = "/Pages/ListArticle.aspx?xml=ArticleApproving";
                    break;
            }
            return redirect;
        }
    }
}