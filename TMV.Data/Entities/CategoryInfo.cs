using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class CategoryInfo
    {

        #region private fields

        private int _categoryId = Null.NullInteger;
        private int _parentId = Null.NullInteger;
        private string _categoryName = Null.NullString;
        private string _slug = Null.NullString;
        private string _seoH1 = Null.NullString;
        private string _seoTitle = Null.NullString;
        private string _seoKeyword = Null.NullString;
        private string _seoDescription = Null.NullString;
        private int _group = Null.NullInteger;
        private string _avatar = Null.NullString;
        private bool _isShowDetail = Null.NullBoolean;
        private bool _isShowMenuTop = Null.NullBoolean;
        private bool _isShowMenuBot = Null.NullBoolean;
        private int _orderBy = Null.NullInteger;
        #endregion

        #region public properties

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }
        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }
        public string Slug
        {
            get { return _slug; }
            set { _slug = value; }
        }
        public string SeoH1
        {
            get { return _seoH1; }
            set { _seoH1 = value; }
        }
        public string SeoTitle
        {
            get { return _seoTitle; }
            set { _seoTitle = value; }
        }
        public string SeoKeyword
        {
            get { return _seoKeyword; }
            set { _seoKeyword = value; }
        }
        public string SeoDescription
        {
            get { return _seoDescription; }
            set { _seoDescription = value; }
        }
        public int Group
        {
            get { return _group; }
            set { _group = value; }
        }
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        public bool IsShowDetail
        {
            get { return _isShowDetail; }
            set { _isShowDetail = value; }
        }
        public bool IsShowMenuTop
        {
            get { return _isShowMenuTop; }
            set { _isShowMenuTop = value; }
        }
        public bool IsShowMenuBot
        {
            get { return _isShowMenuBot; }
            set { _isShowMenuBot = value; }
        }
        public int OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value; }
        }
        #endregion

        #region Custom

        public string NavigationUrl
        {
            get { return Navigation.NavigateCategory(Slug); }
        }
        public string GroupName { get; set; }

        #endregion

    }
}
