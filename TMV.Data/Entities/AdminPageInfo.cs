using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class AdminPageInfo
    {
        private int _adminPageId = Null.NullInteger;
        private int _parentId = Null.NullInteger;
        private string _name = String.Empty;
        private string _source = String.Empty;
        private bool _visible;
        private int _orderView;
        private bool _locked;

        #region Public Properties
        public int AdminPageID
        {
            get { return _adminPageId; }
            set { _adminPageId = value; }
        }

        public int ParentID
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public int OrderView
        {
            get { return _orderView; }
            set { _orderView = value; }
        }

        public bool Locked
        {
            get { return _locked; }
            set { _locked = value; }
        }

        public string Link
        {
            get { return (Globals.RelativeWebRoot + Source); }
        }
        #endregion
    }
}
