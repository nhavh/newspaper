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
    public partial class EditCategory : AdminPageBase
    {
        public string ThumbnailPreviewAvatar = "";
        public string ThumbnailSrcAvatar = "";
        public string ThumbnailPreview = String.Empty;
        public string ThumbnailSrc = String.Empty;

        private readonly CategoryController _ctrl = new CategoryController();
        private CategoryInfo _info = new CategoryInfo();
        private const string RedirectLink = "/Pages/ListCategory.aspx?xml=Category";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["CategoryId"]))
                _info = _ctrl.GetCategory(int.Parse(Request.QueryString["CategoryId"]));

            if (Page.IsPostBack) return;

            LoadCategory();
            LoadGroup();

            if (_info.CategoryId != -1)
                RenderForm();
        }
        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        protected void lbtClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(RedirectLink);
        }
        private void SaveData()
        {
            _info.IsShowDetail = cbIsShowDetail.Checked;
            _info.IsShowMenuTop = cbIsShowMenuTop.Checked;
            _info.IsShowMenuBot = cbIsShowMenuBot.Checked;
            _info.OrderBy = int.Parse(txtOrderBy.Text);
            _info.CategoryName = txtCategoryName.Text;
            if (!String.IsNullOrEmpty(Request.Params["thumbnailSrcAvatar"]))
                _info.Avatar = Request.Params["thumbnailSrcAvatar"];
            _info.Slug = txtSlug.Text;
            _info.ParentId = int.Parse(ddlCategory.SelectedValue);
            _info.SeoH1 = txtSeoH1.Text;
            _info.SeoTitle = txtSeoTitle.Text;
            _info.SeoKeyword = txtSeoKeyword.Text;
            _info.SeoDescription = txtSeoDescription.Text;
            _info.Group = int.Parse(ddlGroup.SelectedValue);

            if (_info.CategoryId == -1) {
                _ctrl.InsertCategory(_info);
            } else {
                _ctrl.UpdateCategory(_info);
            }
            Response.Redirect(RedirectLink);
        }
        private void RenderForm()
        {
            cbIsShowDetail.Checked = _info.IsShowDetail;
            cbIsShowMenuTop.Checked = _info.IsShowMenuTop;
            cbIsShowMenuBot.Checked = _info.IsShowMenuBot;
            txtOrderBy.Text = _info.OrderBy.ToString();
            txtCategoryName.Text = _info.CategoryName;
            if (!Null.IsNull(_info.Avatar))
            {
                ThumbnailPreviewAvatar = string.Format("<img style='margin: 5px; border: 1px solid rgb(127, 127, 127); padding: 2px; opacity: 1' src='{0}' />", Globals.ImageUrlNoCDN(_info.Avatar, 200, false));
                ThumbnailSrcAvatar = _info.Avatar;
            }
            ddlCategory.SelectedValue = _info.ParentId.ToString();
            txtSeoH1.Text = _info.SeoH1;
            txtSlug.Text = _info.Slug;
            txtSeoTitle.Text = _info.SeoTitle;
            txtSeoKeyword.Text = _info.SeoKeyword;
            txtSeoDescription.Text = _info.SeoDescription;
            ddlGroup.SelectedValue = _info.Group.ToString();
        }
        private void LoadCategory()
        {
            ddlCategory.DataSource = _ctrl.HelpBindCategory(_ctrl.ListCategory());
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();

            ddlCategory.Items.Insert(0, new ListItem("*", "-1"));
        }
        private void LoadGroup()
        {
            ddlGroup.DataSource = _ctrl.HelpBindCategory(_ctrl.ListCategory().Where(p => p.ParentId == -1).ToList());
            ddlGroup.DataTextField = "CategoryName";
            ddlGroup.DataValueField = "CategoryId";
            ddlGroup.DataBind();

            ddlGroup.Items.Insert(0, new ListItem("*", "-1"));
        }
    }
}
