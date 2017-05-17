using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class AdminPageRoleInfo
    {
        private int _pageroleid = Null.NullInteger;
        private int _pageid = Null.NullInteger;     
        private int _roleid = Null.NullInteger;

        #region Public Properties
        public int PageRoleID
        {
            get { return _pageroleid; }
            set { _pageroleid = value; }
        }
        public int PageID
        {
            get { return _pageid; }
            set { _pageid = value; }
        }
        public int RoleID
        {
            get { return _roleid; }
            set { _roleid = value; }
        }

        #endregion

    }
}
