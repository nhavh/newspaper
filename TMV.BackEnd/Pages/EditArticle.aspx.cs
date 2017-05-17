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
    public partial class EditArticle : AdminPageBase
    {
        public string ThumbnailPreviewAvatar = "";
        public string ThumbnailSrcAvatar = "";
        public string ThumbnailPreview = String.Empty;
        public string ThumbnailSrc = String.Empty;
        public string UrlPreview = "";

        private ArticleInfo _articleInfo = new ArticleInfo();
        private readonly ArticleController _articleController = new ArticleController();
        private Random _rd = new Random();
        private readonly char[] _delimiterChar = { '|' };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ArticleId"]))
                _articleInfo = _articleController.GetArticle(Int32.Parse(Request.QueryString["ArticleId"]));

            if (Page.IsPostBack) return;

            LoadCategory();
            LoadTime();
            LoadRoles();

            if (_articleInfo.ArticleId != -1) {
                RenderForm();
                qa.Visible = review.Visible = true;
            }
            else
            {
                qa.Visible = review.Visible = false;
            }
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        protected void lbtClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetRedirectUrl());
        }
        protected void btnTransferTo_Click(object sender, EventArgs e)
        {
            _articleInfo.EditorId = -1;
            _articleInfo.RoleId = int.Parse(ddlRole.SelectedValue);
            _articleInfo.ApproverId = UserId;
            SaveData(0);
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            _articleInfo.EditorId = _articleInfo.AuthorId;
            _articleInfo.AdminNote = txtAdminNote.Text.Trim();
            _articleInfo.RoleId = -1;
            _articleInfo.ApproverId = UserId;
            _articleController.UpdateArticle(_articleInfo);
            Response.Redirect(GetRedirectUrl());
        }

        private void SaveData(int type = -1)
        {
            _articleInfo.CategoryId = int.Parse(ddlCategory.SelectedValue);
            _articleInfo.Title = txtTitle.Text;
            _articleInfo.IsTab = cbIsTab.Checked;
            _articleInfo.Slug = HtmlHelper.RemoveIllegalCharacters(txtSlug.Text);
            if (!String.IsNullOrEmpty(Request.Params["thumbnailSrcAvatar"])) 
                _articleInfo.Avatar = Request.Params["thumbnailSrcAvatar"];
            _articleInfo.Description = txtDescription.Text;
            _articleInfo.Intro = ReplaceImageCDNNoIndex(txtInfo.Text);
            _articleInfo.Advantage = ReplaceImageCDNNoIndex(txtAdvantage.Text);
            _articleInfo.Process = ReplaceImageCDNNoIndex(txtProcess.Text);
            _articleInfo.Result = ReplaceImageCDNNoIndex(txtResult.Text);
            //_articleInfo.Qa = txtQA.Text;
            _articleInfo.FrontBack = ReplaceImageCDNNoIndex(txtFrontBack.Text);
            _articleInfo.Video = txtVideo.Text.Replace("<p>", "").Replace("</p>", "");
            //_articleInfo.Review = txtReview.Text;
            if (!String.IsNullOrEmpty(dtePublishDate.Value))
            {
                var publishDate = Convert.ToDateTime(dtePublishDate.Value, new CultureInfo("vi-VN"));
                _articleInfo.PublishDate = new DateTime(publishDate.Year, publishDate.Month, publishDate.Day, Convert.ToInt32(ddlPublishHours.Value), Convert.ToInt32(ddlPublishMinute.Value), 0);
            }
            _articleInfo.Tags = txtTags.Text;
            _articleInfo.SeoTitle = String.IsNullOrEmpty(txtSeoTitle.Text) ? txtTitle.Text : txtSeoTitle.Text;
            _articleInfo.SeoDescription = String.IsNullOrEmpty(txtSeoDescription.Text) ? txtDescription.Text : txtSeoDescription.Text;

            if (_articleInfo.ArticleId == -1)
            {
                _articleInfo.AuthorId = UserId;
                if (type == -1)
                {
                    _articleInfo.EditorId = UserId;
                    _articleInfo.RoleId = -1;
                }
                
                _articleController.InsertArticle(_articleInfo);
            }
            else
            {
                _articleController.UpdateArticle(_articleInfo);
            }
            Response.Redirect(GetRedirectUrl());
        }
        private void RenderForm()
        {
            UrlPreview = Globals.FrontEndUrl + _articleInfo.NavigationUrl;

            if (_articleInfo.RoleId == -1 && String.IsNullOrEmpty(_articleInfo.AdminNote))
            {
                btnReturn.Visible = false;
                txtAdminNote.ReadOnly = true;
            }

            if (_articleInfo.EditorId == -1 && _articleInfo.RoleId != 0)
            {
                _articleInfo.EditorId = UserId;
                _articleController.UpdateArticle(_articleInfo);
                hdCurrentRole.Value = ((int)Globals.DocumentStatus.WaittingApprove).ToString();
            }
            else
                hdCurrentRole.Value = _articleInfo.RoleId.ToString();

            ddlCategory.SelectedValue = _articleInfo.CategoryId.ToString();
            txtTitle.Text = _articleInfo.Title;
            txtSlug.Text = _articleInfo.Slug;
            cbIsTab.Checked = _articleInfo.IsTab;
            if (!Null.IsNull(_articleInfo.Avatar))
            {
                ThumbnailPreviewAvatar = string.Format("<img style='margin: 5px; border: 1px solid rgb(127, 127, 127); padding: 2px; opacity: 1' src='{0}' />", Globals.ImageUrlNoCDN(_articleInfo.Avatar, 230, false));
                ThumbnailSrcAvatar = _articleInfo.Avatar;
            }

            txtDescription.Text = _articleInfo.Description;
            txtInfo.Text = _articleInfo.Intro;
            txtAdvantage.Text = _articleInfo.Advantage;
            txtProcess.Text = _articleInfo.Process;
            txtResult.Text = _articleInfo.Result;
            //txtQA.Text = _articleInfo.Qa;
            txtFrontBack.Text = _articleInfo.FrontBack;
            txtVideo.Text = _articleInfo.Video;
            //txtReview.Text = _articleInfo.Review;
            dtePublishDate.Value = Null.NullDate.Equals(_articleInfo.PublishDate) ? String.Empty : _articleInfo.PublishDate.ToString("dd/MM/yyyy");
            ddlPublishHours.Value = _articleInfo.PublishDate.Hour.ToString();
            ddlPublishMinute.Value = _articleInfo.PublishDate.Minute.ToString();
            txtTags.Text = _articleInfo.Tags;
            txtSeoTitle.Text = _articleInfo.SeoTitle;
            txtSeoDescription.Text = _articleInfo.SeoDescription;
            ddlRole.SelectedValue = _articleInfo.RoleId.ToString();
        }

        private string GetRedirectUrl()
        {
            string redirect;
            var curentRoleId = Int32.Parse(hdCurrentRole.Value);
            switch (curentRoleId)
            {
                case (int)Globals.DocumentStatus.Editting:
                    redirect = "~/Pages/ListArticle.aspx?xml=ArticleEditting";
                    break;
                case (int)Globals.DocumentStatus.Online:
                    redirect = "~/Pages/ListArticle.aspx?xml=ArticleOnline";
                    break;
                case (int)Globals.DocumentStatus.WaittingApprove:
                    redirect = "~/Pages/ListArticle.aspx?xml=ArticleWaittingApprove";
                    break;
                default:
                    redirect = "~/Pages/ListArticle.aspx?xml=ArticleApproving";
                    break;
            }
            return redirect;
        }
        private void LoadCategory()
        {
            var ctrlCategory = new CategoryController();
            ddlCategory.DataSource = ctrlCategory.HelpBindCategory(ctrlCategory.ListCategory());
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();

            ddlCategory.Items.Insert(0, new ListItem("*", "-1"));
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
        private void LoadRoles()
        {
            ddlRole.DataSource = new AdminRoleController().SelectAdminRoleToByUser(UserId);
            ddlRole.DataBind();
        }
        private string ReplaceImageCDN(string value)
        {
            var idx = _rd.Next(1, 4);
            var cndName = "cdn" + idx.ToString();
            return value.Replace(Globals.StaticUrl, Globals.CDNUrl.Replace("cdn", cndName));
            //return value.Replace(Globals.StaticUrl, Globals.CDNUrl);
        }
        private string ReplaceImageCDNNoIndex(string value)
        {
            return value.Replace(Globals.StaticUrl, Globals.CDNUrl);
        }
    }
}