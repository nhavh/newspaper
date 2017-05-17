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
    public partial class EditVideo : AdminPageBase
    {
        private VideoInfo _videoInfo = new VideoInfo();
        private readonly VideoController _videoController = new VideoController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["VideoId"]))
                _videoInfo = _videoController.GetVideo(Int32.Parse(Request.QueryString["VideoId"]));

            if (Page.IsPostBack) return;

            LoadTime();

            if (_videoInfo.VideoId != -1)
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
            _videoInfo.Title = txtTitle.Text;
            _videoInfo.Slug = HtmlHelper.RemoveIllegalCharacters(txtTitle.Text);
            _videoInfo.Url = txtUrl.Text;
            _videoInfo.Description = txtDescription.Text;
            if (!String.IsNullOrEmpty(dteStartDate.Value))
            {
                var startDate = Convert.ToDateTime(dteStartDate.Value, new CultureInfo("vi-VN"));
                _videoInfo.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, Convert.ToInt32(ddlStartHours.Value), Convert.ToInt32(ddlStartMinute.Value), 0);
            }
            if (!String.IsNullOrEmpty(dteEndDate.Value))
            {
                var endDate = Convert.ToDateTime(dteEndDate.Value, new CultureInfo("vi-VN"));
                _videoInfo.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, Convert.ToInt32(ddlEndHours.Value), Convert.ToInt32(ddlEndMinute.Value), 0);
            }
            _videoInfo.Tags = txtTags.Text;
            _videoInfo.SeoTitle = String.IsNullOrEmpty(txtSeoTitle.Text) ? txtTitle.Text : txtSeoTitle.Text;
            _videoInfo.SeoDescription = String.IsNullOrEmpty(txtSeoDescription.Text) ? txtDescription.Text : txtSeoDescription.Text;
            _videoInfo.IsHome = cbIsHome.Checked;
            if (_videoInfo.VideoId == 0)
            {
                _videoInfo.CreatedBy = UserId;
                _videoController.InsertVideo(_videoInfo);
            }
            else
            {
                _videoInfo.UpdatedBy = UserId;
                _videoController.UpdateVideo(_videoInfo);
            }
            Response.Redirect("~/Pages/ListVideo.aspx?xml=Video");
        }
        private void RenderForm()
        {
            //UrlPreview = Globals.FrontEndUrl + _videoInfo.NavigationUrl;

            txtTitle.Text = _videoInfo.Title;
            txtUrl.Text = _videoInfo.Url;
            txtDescription.Text = _videoInfo.Description;
            dteStartDate.Value = Null.NullDate.Equals(_videoInfo.StartDate) ? String.Empty : _videoInfo.StartDate.ToString("dd/MM/yyyy");
            ddlStartHours.Value = _videoInfo.StartDate.Hour.ToString();
            ddlStartMinute.Value = _videoInfo.StartDate.Minute.ToString();
            dteEndDate.Value = Null.NullDate.Equals(_videoInfo.EndDate) ? String.Empty : _videoInfo.EndDate.ToString("dd/MM/yyyy");
            ddlEndHours.Value = _videoInfo.EndDate.Hour.ToString();
            ddlEndMinute.Value = _videoInfo.EndDate.Minute.ToString();
            txtTags.Text = _videoInfo.Tags;
            txtSeoTitle.Text = _videoInfo.SeoTitle;
            txtSeoDescription.Text = _videoInfo.SeoDescription;
            cbIsHome.Checked = _videoInfo.IsHome;
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