using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using TMV.Caching;
using TMV.Utilities;
using System.Web.Security;
using System.Threading;
using TMV.Caching;
using System.Security.Cryptography;
using System;
using System.Web;

namespace TMV.Data.Entities
{
    public class AdminUserController
    {
        public static void AdminUserLogin(AdminUserInfo info, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(info.Username, createPersistentCookie);
        }

        public static void AdminUserSignOut()
        {
            FormsAuthentication.SignOut();
            DataCache.ClearAdminUserCache(Thread.CurrentPrincipal.Identity.Name);
        }

        public static AdminUserInfo GetCurrentAdminUser()
        {
            var info = new AdminUserInfo();
            if (HttpContext.Current.Items["AdminUserInfo"] == null)
            {
                if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                {
                    info = GetAdminUser(Thread.CurrentPrincipal.Identity.Name);
                }
            }
            else
            {
                info = (AdminUserInfo)HttpContext.Current.Items["AdminUserInfo"];
            }
            return info;
        }

        public static AdminUserInfo GetCachedAdminUser(string username)
        {
            var cacheKey = string.Format("AdminUserInfo|{0}", username);
            var cache = (AdminUserInfo)DataCache.GetCache(cacheKey);
            if ((cache == null) || (cache.Username != username))
            {
                cache = GetAdminUser(username);
                if (cache != null)
                {
                    DataCache.SetCache(cacheKey, cache, TimeSpan.FromMinutes(3));
                }
            }
            return cache;
        }

        public int InsertAdminUser(AdminUserInfo info)
        {
            return SQL.InsertAdminUser(info.Username, SHA1Encryption(info.Password), info.IsAdministrator, info.Deleted);
        }

        public void UpdateAdminUser(AdminUserInfo info)
        {
            SQL.UpdateAdminUser(info.UserID, info.Username, SHA1Encryption(info.Password), info.IsAdministrator, info.Deleted); 
        }

        public void DeleteAdminUser(int adminUserId)
        {
            SQL.DeleteAdminUser(adminUserId);
        }

        public void DeleteAdminUser(AdminUserInfo info)
        {
            DeleteAdminUser(info.UserID);
        }

        public string SHA1Encryption(string strForEncryption)
        {
            var sHa1Managed = new SHA1Managed();
            var result = sHa1Managed.ComputeHash(Encoding.ASCII.GetBytes(strForEncryption));
            var encryptedString = new System.Text.StringBuilder();
            foreach (byte outputByte in result)
                // convert each byte to a Hexadecimal upper case string
                encryptedString.Append(outputByte.ToString("x2").ToUpper());
            return encryptedString.ToString();
        }

        public AdminUserInfo GetAdminUser(int userId)
        {
            return (AdminUserInfo)CBO.FillObject(SQL.GetAdminUser(userId), typeof(AdminUserInfo));
        }

        public static AdminUserInfo GetAdminUser(string username)
        {
            return (AdminUserInfo)CBO.FillObject(SQL.GetAdminUser(username), typeof(AdminUserInfo));
        }

        public AdminUserInfo AdminUserLogin(string username, string password)
        {
            return (AdminUserInfo)CBO.FillObject(SQL.GetAdminUser(username, SHA1Encryption(password)), typeof(AdminUserInfo));
        }

        public ArrayList ListAdminUser()
        {
            return CBO.FillCollection(SQL.ListAdminUser(), typeof(AdminUserInfo));
        }
        public List<AdminUserInfo> ListAdminUserByReturn()
        {
            return CBO.FillCollection<AdminUserInfo>(SQL.ListAdminUserByReturn());
        }
        public DataTable SelectAdminUser()
        {
            return CBO.ConvertToDataTable(ListAdminUser(), typeof(AdminUserInfo));
        }


        public Dictionary<object, object> ListAdminUserLookup()
        {
            var users = ListAdminUser();
            var dicusers = new Dictionary<object, object>();
            foreach (AdminUserInfo info in users)
                dicusers.Add(info.UserID, info.Username);

            return dicusers;
        }
    }
}
