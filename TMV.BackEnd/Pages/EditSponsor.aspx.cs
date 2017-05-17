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
    public partial class EditSponsor : AdminPageBase
    {
        private readonly SponsorController _ctrl = new SponsorController();
        private SponsorInfo _info = new SponsorInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["SponsorId"]))
                _info = _ctrl.GetSponsor(int.Parse(Request.QueryString["SponsorId"]));

            if (Page.IsPostBack) return;

            LoadTime();
            LoadItemType();

            if (_info.SponsorId != -1)
                RenderForm();
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        protected void lbtClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/ListSponsor.aspx?xml=Sponsor");
        }

        private void SaveData()
        {
            _info.ItemId = int.Parse(hdItemId.Value);
            _info.ItemType = byte.Parse(ddlItemType.SelectedValue);
            if (dpStartDate.Value != "")
            {
                var startDate = Convert.ToDateTime(dpStartDate.Value, new CultureInfo("vi-VN"));
                _info.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, Convert.ToInt32(ddlStartDateHour.Value), Convert.ToInt32(ddlStartDateMinute.Value), 0);
            }
            if (dpStartDate.Value != "")
            {
                var endDate = Convert.ToDateTime(dpEndDate.Value, new CultureInfo("vi-VN"));
                _info.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, Convert.ToInt32(ddlEndDateHour.Value), Convert.ToInt32(ddlEndDateMinute.Value), 0);
            }
            if (_info.SponsorId == -1)
            {
                _ctrl.InsertSponsor(_info);
            }
            else
            {
                _ctrl.UpdateSponsor(_info);
            }
            Response.Redirect("~/Pages/ListSponsor.aspx?xml=Sponsor");
        }
        private void RenderForm()
        {
            hdItemId.Value = _info.ItemId.ToString();
            txtItemId.Value = new ArticleController().GetArticle(_info.ItemId).Title;
            ddlItemType.SelectedValue = _info.ItemType.ToString();
            if (_info.StartDate != Null.NullDate)
            {
                dpStartDate.Value = _info.StartDate.ToString("dd/MM/yyyy");
                ddlStartDateHour.Value = _info.StartDate.Hour.ToString();
                ddlStartDateMinute.Value = _info.StartDate.Minute.ToString();
            }
            if (_info.EndDate != Null.NullDate)
            {
                dpEndDate.Value = _info.EndDate.ToString("dd/MM/yyyy");
                ddlEndDateHour.Value = _info.EndDate.Hour.ToString();
                ddlEndDateMinute.Value = _info.EndDate.Minute.ToString();
            }
        }
        private void LoadItemType()
        {
            var categoryController = new CategoryController();
            ddlItemType.DataSource = categoryController.HelpBindCategory(categoryController.ListCategory());
            ddlItemType.DataTextField = "CategoryName";
            ddlItemType.DataValueField = "CategoryId";
            ddlItemType.DataBind();

            ddlItemType.Items.Insert(0, new ListItem("*", "-1"));
        }
        private void LoadTime()
        {
            for (var i = 0; i <= 23; i++)
            {
                ddlStartDateHour.Items.Add(i < 10
                                        ? new ListItem('0' + i.ToString(), i.ToString())
                                        : new ListItem(i.ToString(), i.ToString()));

                ddlEndDateHour.Items.Add(i < 10
                                       ? new ListItem('0' + i.ToString(), i.ToString())
                                       : new ListItem(i.ToString(), i.ToString()));
            }
            for (var i = 0; i <= 59; i++)
            {
                ddlStartDateMinute.Items.Add(i < 10
                                        ? new ListItem('0' + i.ToString(), i.ToString())
                                        : new ListItem(i.ToString(), i.ToString()));
                ddlEndDateMinute.Items.Add(i < 10
                                        ? new ListItem('0' + i.ToString(), i.ToString())
                                        : new ListItem(i.ToString(), i.ToString()));
            }
        }
    }
}