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
    public partial class EditPrice : AdminPageBase
    {
        public string ThumbnailPreviewAvatar = "";
        public string ThumbnailSrcAvatar = "";
        public string ThumbnailPreview = String.Empty;
        public string ThumbnailSrc = String.Empty;

        private BannerInfo _bannerInfo = new BannerInfo();
        private readonly BannerController _bannerController = new BannerController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["PriceId"]))
                _bannerInfo = _bannerController.GetBanner(Int32.Parse(Request.QueryString["PriceId"]));

            if (Page.IsPostBack) return;

            LoadTime();
            LoadType();
            LoadCategory();

            if (_bannerInfo.BannerId != -1)
                RenderForm();
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        protected void lbtClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pages/ListPrice.aspx?xml=Price");
        }
        private void SaveData()
        {
            _bannerInfo.Title = txtTitle.Text;
            _bannerInfo.Slug = HtmlHelper.RemoveIllegalCharacters(txtTitle.Text);
            if (!String.IsNullOrEmpty(Request.Params["thumbnailSrcAvatar"]))
                _bannerInfo.ImagePath = Request.Params["thumbnailSrcAvatar"];
            _bannerInfo.Priority = 1;
            _bannerInfo.Position = Convert.ToInt32(txtPosition.Text);
            _bannerInfo.Type = int.Parse(ddlType.SelectedValue);
            _bannerInfo.IsMobile = false;//cbIsMobile.Checked;
            _bannerInfo.Content = txtContent.Text;
            _bannerInfo.CategoryId = int.Parse(ddlCategory.SelectedValue);
            if (!String.IsNullOrEmpty(dteStartDate.Value))
            {
                var startDate = Convert.ToDateTime(dteStartDate.Value, new CultureInfo("vi-VN"));
                _bannerInfo.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, Convert.ToInt32(ddlStartHours.Value), Convert.ToInt32(ddlStartMinute.Value), 0);
            }
            if (!String.IsNullOrEmpty(dteEndDate.Value))
            {
                var endDate = Convert.ToDateTime(dteEndDate.Value, new CultureInfo("vi-VN"));
                _bannerInfo.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, Convert.ToInt32(ddlEndHours.Value), Convert.ToInt32(ddlEndMinute.Value), 0);
            }
            if (_bannerInfo.BannerId == 0)
            {
                _bannerController.InsertBanner(_bannerInfo);
            }
            else
            {
                _bannerController.UpdateBanner(_bannerInfo);
            }
            Response.Redirect("~/Pages/ListPrice.aspx?xml=Price");
        }

        private void RenderForm()
        {
            txtTitle.Text = _bannerInfo.Title;
            //cbIsMobile.Checked = _bannerInfo.IsMobile;
            if (!Null.IsNull(_bannerInfo.ImagePath))
            {
                ThumbnailPreviewAvatar = string.Format("<img style='margin: 5px; border: 1px solid rgb(127, 127, 127); padding: 2px; opacity: 1' src='{0}' />", Globals.ImageUrlNoCDN(_bannerInfo.ImagePath, 800, true));
                ThumbnailSrcAvatar = _bannerInfo.ImagePath;
            }
            ddlType.SelectedValue = _bannerInfo.Type.ToString();
            txtContent.Text = _bannerInfo.Content;
            txtPosition.Text = _bannerInfo.Position.ToString();
            dteStartDate.Value = Null.NullDate.Equals(_bannerInfo.StartDate) ? String.Empty : _bannerInfo.StartDate.ToString("dd/MM/yyyy");
            ddlStartHours.Value = _bannerInfo.StartDate.Hour.ToString();
            ddlStartMinute.Value = _bannerInfo.StartDate.Minute.ToString();
            dteEndDate.Value = Null.NullDate.Equals(_bannerInfo.EndDate) ? String.Empty : _bannerInfo.EndDate.ToString("dd/MM/yyyy");
            ddlEndHours.Value = _bannerInfo.EndDate.Hour.ToString();
            ddlEndMinute.Value = _bannerInfo.EndDate.Minute.ToString();
            ddlCategory.SelectedValue = _bannerInfo.CategoryId.ToString();
        }
        private void LoadType()
        {
            ddlType.Items.Add(new ListItem("Hình ảnh", "1"));
            ddlType.Items.Add(new ListItem("Nội dung", "2"));
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
        private void LoadCategory()
        {
            var categoryCtrl = new CategoryController();
            ddlCategory.DataSource = categoryCtrl.HelpBindCategory(categoryCtrl.ListCategory());
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();

            ddlCategory.Items.Insert(0, new ListItem("*", "-1"));
        }
    }
}