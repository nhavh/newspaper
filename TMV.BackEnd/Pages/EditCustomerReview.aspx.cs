using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMV.Data.Entities;
using TMV.Framework;
using TMV.Utilities;

namespace TMV.BackEnd.Pages
{
    public partial class EditCustomerReview : AdminPageBase
    {
        public string ThumbnailPreviewAvatar = "";
        public string ThumbnailSrcAvatar = "";
        public string ThumbnailPreview = String.Empty;
        public string ThumbnailSrc = String.Empty;
        public string UrlPreview = "";

        private CustomerReviewInfo _customerReviewInfo = new CustomerReviewInfo();
        private readonly CustomerReviewController _customerReviewController = new CustomerReviewController();

        private readonly char[] _delimiterChar = { '|' };
        private int _articleId = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ArticleId"]))
                _articleId = Int32.Parse(Request.QueryString["ArticleId"]);

            if (!String.IsNullOrEmpty(Request.QueryString["CustomerReviewId"]))
            {
                _customerReviewInfo = _customerReviewController.GetCustomerReview(Int32.Parse(Request.QueryString["CustomerReviewId"]));
                _articleId = _customerReviewInfo.ArticleId;
            }

            if (Page.IsPostBack) return;

            LoadTime();

            if (_customerReviewInfo.CustomerReviewId != -1)
                RenderForm(); 
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        protected void lbtClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetRedirectUrl());
        }

        private void SaveData()
        {
            _customerReviewInfo.Title = txtTitle.Text;
            if (!String.IsNullOrEmpty(Request.Params["thumbnailSrcAvatar"]))
                _customerReviewInfo.Avatar = Request.Params["thumbnailSrcAvatar"];
            if (!String.IsNullOrEmpty(dtePublishDate.Value))
            {
                var publishDate = Convert.ToDateTime(dtePublishDate.Value, new CultureInfo("vi-VN"));
                _customerReviewInfo.PublishDate = new DateTime(publishDate.Year, publishDate.Month, publishDate.Day, Convert.ToInt32(ddlPublishHours.Value), Convert.ToInt32(ddlPublishMinute.Value), 0);
            }
            _customerReviewInfo.Content = txtContent.Text;
            if (_customerReviewInfo.CustomerReviewId == 0)
            {
                _customerReviewInfo.ArticleId = _articleId;
                _customerReviewInfo.AuthorId = UserId;
                _customerReviewInfo.EditorId = UserId;
                _customerReviewInfo.RoleId = 0;

                _customerReviewController.InsertCustomerReview(_customerReviewInfo);
            }
            else
            {
                _customerReviewInfo.EditorId = UserId;
                _customerReviewController.UpdateCustomerReview(_customerReviewInfo);
            }
            Response.Redirect(GetRedirectUrl());
        }
        private void RenderForm()
        {
            txtTitle.Text = _customerReviewInfo.Title;

            if (!Null.IsNull(_customerReviewInfo.Avatar))
            {
                ThumbnailPreviewAvatar = string.Format("<img style='margin: 5px; border: 1px solid rgb(127, 127, 127); padding: 2px; opacity: 1' src='{0}' />", Globals.ImageUrlNoCDN(_customerReviewInfo.Avatar, 200, false));
                ThumbnailSrcAvatar = _customerReviewInfo.Avatar;
            }

            txtContent.Text = _customerReviewInfo.Content;
            dtePublishDate.Value = Null.NullDate.Equals(_customerReviewInfo.PublishDate) ? String.Empty : _customerReviewInfo.PublishDate.ToString("dd/MM/yyyy");
            ddlPublishHours.Value = _customerReviewInfo.PublishDate.Hour.ToString();
            ddlPublishMinute.Value = _customerReviewInfo.PublishDate.Minute.ToString();
        }
        private string GetRedirectUrl()
        {
            return "~/Pages/ListCustomerReview.aspx?xml=CustomerReview";
            //return "~/Pages/ListCustomerReview.aspx?xml=CustomerReview&ArticleId=" + _articleId;
        }
        private void LoadTime()
        {
            for (var i = 0; i <= 23; i++)
            {
                ddlPublishHours.Items.Add(i < 10
                                              ? new ListItem('0' + i.ToString(), i.ToString())
                                              : new ListItem(i.ToString(), i.ToString()));
            }
            for (var i = 0; i <= 59; i++)
            {
                ddlPublishMinute.Items.Add(i < 10
                                                ? new ListItem('0' + i.ToString(), i.ToString())
                                                : new ListItem(i.ToString(), i.ToString()));
            }
        }
    }
}