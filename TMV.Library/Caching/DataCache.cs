using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace TMV.Caching
{
    public class DataCache
    {
        private static System.Web.Caching.Cache _objCache;        
        public const int TabCacheTimeOut = 20;        
        public const int TabModuleCacheTimeOut = 20;

        private static System.Web.Caching.Cache objCache
        {
            get
            {
                if (_objCache == null)
                {
                    _objCache = HttpRuntime.Cache;
                }
                return _objCache;
            }
        }

        public static void ClearAdminUserCache(string username)
        {
            RemoveCache(string.Format("AdminUserInfo|{0}", username));
        }

        public static void ClearUserCache(string username)
        {
            RemoveCache(string.Format("UserInfo|{0}", username));
        }

        public static void RemoveCache(string CacheKey)
        {
            objCache.Remove(CacheKey);
        }

        public static object GetCache(string CacheKey)
        {
            return objCache.Get(CacheKey);
        }

        public static void SetCache(string CacheKey, object objObject)
        {
            objCache.Insert(CacheKey, objObject);
        }

        public static void SetCache(string CacheKey, object objObject, CacheDependency objDependency)
        {
            objCache.Insert(CacheKey, objObject, objDependency);
        }

        public static void SetCache(string CacheKey, object objObject, CacheDependency objDependency, DateTime AbsoluteExpiration, TimeSpan SlidingExpiration)
        {
            objCache.Insert(CacheKey, objObject, objDependency, AbsoluteExpiration, SlidingExpiration);
        }

        public static void SetCache(string CacheKey, object objObject, TimeSpan SlidingExpiration)
        {
            objCache.Insert(CacheKey, objObject, null, System.Web.Caching.Cache.NoAbsoluteExpiration, SlidingExpiration);
        }

        public static void SetCache(string CacheKey, object objObject, CacheDependency objDependency, DateTime AbsoluteExpiration, TimeSpan SlidingExpiration, CacheItemPriority Priority, CacheItemRemovedCallback OnRemoveCallback)
        {
            objCache.Insert(CacheKey, objObject, objDependency, AbsoluteExpiration, SlidingExpiration, Priority, OnRemoveCallback);
        }

        public static void SetCache(string CacheKey, object objObject, DateTime AbsoluteExpiration)
        {
            objCache.Insert(CacheKey, objObject, null, AbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
        } 
    }
}
