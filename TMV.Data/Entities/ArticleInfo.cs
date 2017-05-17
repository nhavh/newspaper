using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class ArticleInfo
    {

        #region private fields

        private int _articleId = Null.NullInteger;
        private int _categoryId = Null.NullInteger;
        private string _title = Null.NullString;
        private string _slug = Null.NullString;
        private string _avatar = Null.NullString;
        private string _description = Null.NullString;
        private string _content = Null.NullString;
        private DateTime _createdDate = Null.NullDate;
        private DateTime _publishDate = Null.NullDate;
        private int _roleId = Null.NullInteger;
        private int _authorId = Null.NullInteger;
        private int _editorId = Null.NullInteger;
        private int _approverId = Null.NullInteger;
        private string _adminNote = Null.NullString;
        private string _tags = Null.NullString;
        private string _seoTitle = Null.NullString;
        private string _seoDescription = Null.NullString;
        private int _totalView = Null.NullInteger;
        private float _totalRating = Null.NullInteger;
        private int _totalVote = Null.NullInteger;
        private string _intro = Null.NullString;
        private string _advantage = Null.NullString;
        private string _process = Null.NullString;
        private string _qa = Null.NullString;
        private string _frontBack = Null.NullString;
        private string _video = Null.NullString;
        private string _review = Null.NullString;
        private string _result = Null.NullString;
        private bool _isTab = Null.NullBoolean;
        #endregion

        #region public properties

        public int ArticleId
        {
            get { return _articleId; }
            set { _articleId = value; }
        }
        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public string Slug
        {
            get { return _slug; }
            set { _slug = value; }
        }
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        public System.DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        public System.DateTime PublishDate
        {
            get { return _publishDate; }
            set { _publishDate = value; }
        }
        public int RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }
        public int AuthorId
        {
            get { return _authorId; }
            set { _authorId = value; }
        }
        public int EditorId
        {
            get { return _editorId; }
            set { _editorId = value; }
        }
        public int ApproverId
        {
            get { return _approverId; }
            set { _approverId = value; }
        }
        public string AdminNote
        {
            get { return _adminNote; }
            set { _adminNote = value; }
        }
        public string Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }
        public string SeoTitle
        {
            get { return _seoTitle; }
            set { _seoTitle = value; }
        }
        public string SeoDescription
        {
            get { return _seoDescription; }
            set { _seoDescription = value; }
        }
        public int TotalView
        {
            get { return _totalView; }
            set { _totalView = value; }
        }
        public float TotalRating
        {
            get { return _totalRating; }
            set { _totalRating = value; }
        }
        public int TotalVote
        {
            get { return _totalVote; }
            set { _totalVote = value; }
        }
        public string Intro
        {
            get { return _intro; }
            set { _intro = value; }
        }
        public string Advantage
        {
            get { return _advantage; }
            set { _advantage = value; }
        }
        public string Process
        {
            get { return _process; }
            set { _process = value; }
        }
        public string Qa
        {
            get { return _qa; }
            set { _qa = value; }
        }
        public string FrontBack
        {
            get { return _frontBack; }
            set { _frontBack = value; }
        }
        public string Video
        {
            get { return _video; }
            set { _video = value; }
        }
        public string Review
        {
            get { return _review; }
            set { _review = value; }
        }
        public string Result
        {
            get { return _result; }
            set { _result = value; }
        }
        public bool IsTab
        {
            get { return _isTab; }
            set { _isTab = value; }
        }
        #endregion

        #region Custom
        public bool IsShowDetail { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
        public string NavigationUrl
        {
            get { 
                return IsShowDetail 
                    ? Navigation.NavigateCategory(CategorySlug) 
                    : Navigation.NavigateArticleDetail(CategorySlug, Slug); 
            }
        }
        public string NavigationCategoryUrl
        {
            get { return Navigation.NavigateCategory(CategorySlug); }
        }
        public int Total { get; set; }
        public float AveragePrice { get; set; }
        public float AverageQuality { get; set; }
        public float AverageDoctor { get; set; }
        public float AverageAttitude { get; set; }
        public float AverageFacility { get; set; }
        public float AverageCustomer { get; set; }

        #endregion

    }
}
