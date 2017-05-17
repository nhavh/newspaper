using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class LinkWebsiteInfo
    {
        #region Field
        private int _linkWebsiteId = Null.NullInteger;
        private string _anchorText = Null.NullString;
        private string _url = Null.NullString;
        private byte _orderView = Null.NullByte;
        private bool _target = Null.NullBoolean;
        private byte _priority = Null.NullByte;
        
        #endregion

        #region Property
        public int LinkWebsiteID
        {
            get { return _linkWebsiteId; }
            set { _linkWebsiteId = value; }
        }
        public string AnchorText
        {
            get { return _anchorText; }
            set { _anchorText = value; }
        }
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        public byte OrderView
        {
            get { return _orderView; }
            set { _orderView = value; }
        }
        public bool Target
        {
            get { return _target; }
            set { _target = value; }
        }
        public byte Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
        #endregion
    }
}
