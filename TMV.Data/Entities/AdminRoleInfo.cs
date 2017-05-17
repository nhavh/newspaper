using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class AdminRoleInfo
    {
        private int _roleid;
        private string _rolename;
     
        
        #region properties
        public int RoleID
        {
            get { return _roleid; }
            set { _roleid = value; }
        }
        public string RoleName
        {
            get { return _rolename; }
            set { _rolename = value; }
        }

        #endregion

    }
}
