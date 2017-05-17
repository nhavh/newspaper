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
    public partial class EditPicture : AdminPageBase
    {
        public string ThumbnailPreviewAvatar = "";
        public string ThumbnailSrcAvatar = "";
        public string ThumbnailPreview = String.Empty;
        public string ThumbnailSrc = String.Empty;

        private PictureInfo _pictureInfo = new PictureInfo();
        private readonly PictureController _pictureController = new PictureController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["PictureId"]))
                _pictureInfo = _pictureController.GetPicture(Int32.Parse(Request.QueryString["PictureId"]));

            if (Page.IsPostBack) return;

            LoadTime();

            if (_pictureInfo.PictureId != -1)
                RenderForm();
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        protected void lbtClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("");
        }

        private void SaveData()
        {
            _pictureInfo.Title = txtTitle.Text;
            _pictureInfo.Slug = HtmlHelper.RemoveIllegalCharacters(txtTitle.Text);
            if (!String.IsNullOrEmpty(Request.Params["thumbnailSrcAvatar"]))
                _pictureInfo.ImagePath = Request.Params["thumbnailSrcAvatar"];
            _pictureInfo.Description = txtDescription.Text;
            if (!String.IsNullOrEmpty(dteStartDate.Value))
            {
                var startDate = Convert.ToDateTime(dteStartDate.Value, new CultureInfo("vi-VN"));
                _pictureInfo.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, Convert.ToInt32(ddlStartHours.Value), Convert.ToInt32(ddlStartMinute.Value), 0);
            }
            if (!String.IsNullOrEmpty(dteEndDate.Value))
            {
                var endDate = Convert.ToDateTime(dteEndDate.Value, new CultureInfo("vi-VN"));
                _pictureInfo.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, Convert.ToInt32(ddlEndHours.Value), Convert.ToInt32(ddlEndMinute.Value), 0);
            }
            _pictureInfo.UrlPath = UrlPath.Text;
            _pictureInfo.Tags = txtTags.Text;
            _pictureInfo.SeoTitle = String.IsNullOrEmpty(txtSeoTitle.Text) ? txtTitle.Text : txtSeoTitle.Text;
            _pictureInfo.SeoDescription = String.IsNullOrEmpty(txtSeoDescription.Text) ? txtDescription.Text : txtSeoDescription.Text;
            _pictureInfo.IsHome = cbIsHome.Checked;
            if (_pictureInfo.PictureId == 0)
            {
                _pictureInfo.CreatedBy = UserId;
                _pictureController.InsertPicture(_pictureInfo);
            }
            else
            {
                _pictureInfo.UpdatedBy = UserId;
                _pictureController.UpdatePicture(_pictureInfo);
            }
            Response.Redirect("~/Pages/ListPicture.aspx?xml=Picture");
        }
        private void RenderForm()
        {
            //UrlPreview = Globals.FrontEndUrl + _pictureInfo.NavigationUrl;

            txtTitle.Text = _pictureInfo.Title;

            if (!Null.IsNull(_pictureInfo.ImagePath))
            {
                ThumbnailPreviewAvatar = string.Format("<img style='margin: 5px; border: 1px solid rgb(127, 127, 127); padding: 2px; opacity: 1' src='{0}' />", Globals.ImageUrlNoCDN(_pictureInfo.ImagePath, 350, false));
                ThumbnailSrcAvatar = _pictureInfo.ImagePath;
            }

            UrlPath.Text = _pictureInfo.UrlPath;
            txtDescription.Text = _pictureInfo.Description;
            dteStartDate.Value = Null.NullDate.Equals(_pictureInfo.StartDate) ? String.Empty : _pictureInfo.StartDate.ToString("dd/MM/yyyy");
            ddlStartHours.Value = _pictureInfo.StartDate.Hour.ToString();
            ddlStartMinute.Value = _pictureInfo.StartDate.Minute.ToString();
            dteEndDate.Value = Null.NullDate.Equals(_pictureInfo.EndDate) ? String.Empty : _pictureInfo.EndDate.ToString("dd/MM/yyyy");
            ddlEndHours.Value = _pictureInfo.EndDate.Hour.ToString();
            ddlEndMinute.Value = _pictureInfo.EndDate.Minute.ToString();
            txtTags.Text = _pictureInfo.Tags;
            txtSeoTitle.Text = _pictureInfo.SeoTitle;
            txtSeoDescription.Text = _pictureInfo.SeoDescription;
            cbIsHome.Checked = _pictureInfo.IsHome;
        }

        private void LoadTime()
        {
            for (var i = 0; i <= 23; i++)
            {
                ddlStartHours.Items.Add(i < 10
                                    ? new ListItem('0' + i.ToString(), i.ToString())
                                    : new ListItem(i.ToString(), i.ToString()));
                ddlEndHours.Items.Add(i < 10
                                    ? new ListItem('0' + i.ToString(), i.ToString())
                                    : new ListItem(i.ToString(), i.ToString()));
            }
            for (var i = 0; i <= 59; i++)
            {
                ddlStartMinute.Items.Add(i < 10
                                    ? new ListItem('0' + i.ToString(), i.ToString())
                                    : new ListItem(i.ToString(), i.ToString()));
                ddlEndMinute.Items.Add(i < 10
                                    ? new ListItem('0' + i.ToString(), i.ToString())
                                    : new ListItem(i.ToString(), i.ToString()));
            }
        }
    }
}