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
    public partial class EditQuestionAnswer : AdminPageBase
    {
        private QuestionAnswerInfo _questionAnswerInfo = new QuestionAnswerInfo();
        private readonly QuestionAnswerController _QuestionAnswerController = new QuestionAnswerController();

        private readonly char[] _delimiterChar = { '|' };
        private int _articleId = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ArticleId"]))
                _articleId = Int32.Parse(Request.QueryString["ArticleId"]);

            if (!String.IsNullOrEmpty(Request.QueryString["QuestionAnswerId"]))
            {
                _questionAnswerInfo = _QuestionAnswerController.GetQuestionAnswer(Int32.Parse(Request.QueryString["QuestionAnswerId"]));
                _articleId = _questionAnswerInfo.ArticleId;
            }

            if (Page.IsPostBack) return;

            LoadTime();

            if (_questionAnswerInfo.QuestionAnswerId != -1)
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
            _questionAnswerInfo.Title = txtTitle.Text;
            _questionAnswerInfo.Slug = HtmlHelper.RemoveIllegalCharacters(txtTitle.Text);
            if (!String.IsNullOrEmpty(dtePublishDate.Value))
            {
                var publishDate = Convert.ToDateTime(dtePublishDate.Value, new CultureInfo("vi-VN"));
                _questionAnswerInfo.PublishDate = new DateTime(publishDate.Year, publishDate.Month, publishDate.Day, Convert.ToInt32(ddlPublishHours.Value), Convert.ToInt32(ddlPublishMinute.Value), 0);
            }
            _questionAnswerInfo.Content = txtContent.Text;
            _questionAnswerInfo.Question = txtQuestion.Text;
            if (_questionAnswerInfo.QuestionAnswerId == 0)
            {
                _questionAnswerInfo.ArticleId = _articleId;
                _questionAnswerInfo.AuthorId = UserId;
                _questionAnswerInfo.EditorId = UserId;
                _questionAnswerInfo.RoleId = 0;

                _QuestionAnswerController.InsertQuestionAnswer(_questionAnswerInfo);
            }
            else
            {
                _questionAnswerInfo.EditorId = UserId;
                _QuestionAnswerController.UpdateQuestionAnswer(_questionAnswerInfo);
            }
            Response.Redirect(GetRedirectUrl());
        }
        private void RenderForm()
        {
            txtTitle.Text = _questionAnswerInfo.Title;
            txtContent.Text = _questionAnswerInfo.Content;
            txtQuestion.Text = _questionAnswerInfo.Question;
            dtePublishDate.Value = Null.NullDate.Equals(_questionAnswerInfo.PublishDate) ? String.Empty : _questionAnswerInfo.PublishDate.ToString("dd/MM/yyyy");
            ddlPublishHours.Value = _questionAnswerInfo.PublishDate.Hour.ToString();
            ddlPublishMinute.Value = _questionAnswerInfo.PublishDate.Minute.ToString();
        }
        private string GetRedirectUrl()
        {
            return "~/Pages/ListQuestionAnswer.aspx?xml=QuestionAnswer&ArticleId=" + _articleId;
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