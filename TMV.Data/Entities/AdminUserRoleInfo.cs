using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using TMV.Utilities;
using System.Collections;

namespace TMV.Data.Entities
{
    public class AdminUserRoleInfo
    {
        private int _userRoleId = Null.NullInteger;
        private int _userId = Null.NullInteger;
        private int _roleId = Null.NullInteger;

        #region Public Properties
        public int UserRoleID
        {
            get { return _userRoleId; }
            set { _userRoleId = value; }
        }

        public int UserID
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public int RoleID
        {
            get { return _roleId; }
            set { _roleId = value; }
        }

        #endregion
    }
}
