using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class SponsorInfo
    {
        #region private fields
        private int _sponsorId = Null.NullInteger;
        private int _itemId = Null.NullInteger;
        private byte _itemType = Null.NullByte;
        private System.DateTime _startDate = Null.NullDate;
        private System.DateTime _endDate = Null.NullDate;
        private System.DateTime _createdDate = Null.NullDate;
        #endregion

        #region public properties
        public int SponsorId
        {
            get { return _sponsorId; }
            set { _sponsorId = value; }
        }
        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }
        public byte ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }
        public System.DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        public System.DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        public System.DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        #endregion

        #region Custom
        public bool IsShowDetail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public string Slug { get; set; }

        public string CategorySlug { get; set; }
        public string NavigationUrl
        {
            get
            {
                return IsShowDetail
                    ? Navigation.NavigateCategory(CategorySlug)
                    : Navigation.NavigateArticleDetail(CategorySlug, Slug);
            }
        }
        #endregion
    }
}
