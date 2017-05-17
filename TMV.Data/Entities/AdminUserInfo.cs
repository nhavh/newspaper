using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TMV.Utilities;
using System.Collections;

namespace TMV.Data.Entities
{
    public class AdminUserInfo
    {
        private int _userId = Null.NullInteger;
        private string _username = String.Empty;
        private string _password = String.Empty;
        private bool _isAdministrator;
        private bool _deleted;

        #region Public Properties
        public int UserID
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public bool IsAdministrator
        {
            get { return _isAdministrator; }
            set { _isAdministrator = value; }
        }

        public bool Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }
        #endregion

        public ArrayList Pages
        {
            get
            {
                var ctrl = new AdminPageController();
                return IsAdministrator ? ctrl.ListAdminPage() : ctrl.ListAdminPageByRoles(UserID);
            }
        }
        public List<AdminPageInfo> PagesEx
        {
            get
            {
                var ctrl = new AdminPageController();
                return IsAdministrator ? ctrl.ListAdminPageEx() : ctrl.ListAdminPageByRolesEx(UserID);
            }
        }
        public bool IsInPage(string page)
        {
            return Pages.Cast<AdminPageInfo>().Any(info => info.Source.Contains(page));
        }
    }
}
