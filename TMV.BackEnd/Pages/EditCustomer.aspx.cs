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
    public partial class EditCustomer : AdminPageBase
    {
        public string ThumbnailPreviewAvatar = "";
        public string ThumbnailSrcAvatar = "";
        public string ThumbnailPreview = String.Empty;
        public string ThumbnailSrc = String.Empty;
        public string UrlPreview = "";

        private CustomerInfo _customerInfo = new CustomerInfo();
        private readonly CustomerController _customerController = new CustomerController();

        private readonly char[] _delimiterChar = { '|' };
        private int _CustomerId = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["CustomerId"]))
                _CustomerId = Int32.Parse(Request.QueryString["CustomerId"]);

            if (!String.IsNullOrEmpty(Request.QueryString["CustomerId"]))
            {
                _customerInfo = _customerController.GetCustomer(Int32.Parse(Request.QueryString["CustomerId"]));
                _CustomerId = _customerInfo.CustomerId;
            }

            if (Page.IsPostBack) return;

            LoadTime();

            if (_customerInfo.CustomerId != -1)
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
            _customerInfo.FullName = txtFullName.Text;
            if (!String.IsNullOrEmpty(Request.Params["thumbnailSrcAvatar"]))
                _customerInfo.Avatar = Request.Params["thumbnailSrcAvatar"];
            if (!String.IsNullOrEmpty(dtePublishDate.Value))
            {
                var publishDate = Convert.ToDateTime(dtePublishDate.Value, new CultureInfo("vi-VN"));
                _customerInfo.PublishDate = new DateTime(publishDate.Year, publishDate.Month, publishDate.Day, Convert.ToInt32(ddlPublishHours.Value), Convert.ToInt32(ddlPublishMinute.Value), 0);
            }
            _customerInfo.Content = txtContent.Text;
            if (_customerInfo.CustomerId == 0)
            {
                _customerInfo.AuthorId = UserId;
                _customerInfo.EditorId = UserId;
                _customerInfo.RoleId = 0;

                _customerController.InsertCustomer(_customerInfo);
            }
            else
            {
                _customerInfo.EditorId = UserId;
                _customerController.UpdateCustomer(_customerInfo);
            }
            Response.Redirect(GetRedirectUrl());
        }
        private void RenderForm()
        {
            txtFullName.Text = _customerInfo.FullName;

            if (!Null.IsNull(_customerInfo.Avatar))
            {
                ThumbnailPreviewAvatar = string.Format("<img style='margin: 5px; border: 1px solid rgb(127, 127, 127); padding: 2px; opacity: 1' src='{0}' />", Globals.ImageUrlNoCDN(_customerInfo.Avatar, 300, false));
                ThumbnailSrcAvatar = _customerInfo.Avatar;
            }

            txtContent.Text = _customerInfo.Content;
            dtePublishDate.Value = Null.NullDate.Equals(_customerInfo.PublishDate) ? String.Empty : _customerInfo.PublishDate.ToString("dd/MM/yyyy");
            ddlPublishHours.Value = _customerInfo.PublishDate.Hour.ToString();
            ddlPublishMinute.Value = _customerInfo.PublishDate.Minute.ToString();
        }
        private string GetRedirectUrl()
        {
            return "~/Pages/ListCustomer.aspx?xml=Customer&CustomerId=" + _CustomerId;
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