using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class CustomerController
    {
        public void InsertCustomer(CustomerInfo info)
        {
            SQL.InsertCustomer(info.FullName, info.Avatar, info.Content, info.PublishDate, info.RoleId, info.AuthorId, info.EditorId, info.ApproverId, info.AdminNote);
        }
        public void UpdateCustomer(CustomerInfo info)
        {
            SQL.UpdateCustomer(info.CustomerId, info.FullName, info.Avatar, info.Content, info.PublishDate, info.RoleId, info.AuthorId, info.EditorId, info.ApproverId, info.AdminNote);
        }
        public void DeleteCustomer(CustomerInfo info)
        {
            SQL.DeleteCustomer(info.CustomerId);
        }
        public void DeleteCustomer(int CustomerId)
        {
            SQL.DeleteCustomer(CustomerId);
        }
        public List<CustomerInfo> ListCustomer()
        {
            return CBO.FillCollection<CustomerInfo>(SQL.ListCustomer());
        }
        public DataTable SelectCustomer()
        {
            return CBO.ConvertToDataTable(ListCustomer(), typeof(CustomerInfo));
        }
        public CustomerInfo GetCustomer(int customerId)
        {
            return CBO.FillObject<CustomerInfo>(SQL.GetCustomer(customerId));
        }

        public List<CustomerInfo> ListCustomerByHome(bool isClearCache = false)
        {
            string strCacheKey = "TMV_ListCustomerByHome";
            if (isClearCache) System.Web.HttpContext.Current.Cache.Remove(strCacheKey);
            var res = System.Web.HttpContext.Current.Cache.Get(strCacheKey) as List<CustomerInfo>;
            if (res != null) return res;
            var tmp = CBO.FillCollection<CustomerInfo>(SQL.ListCustomerByHome());
            if (tmp == null || tmp.Count == 0) return new List<CustomerInfo>();
            res = new List<CustomerInfo>(tmp);
            System.Web.HttpContext.Current.Cache.Add(strCacheKey, res, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            return res;
        }
    }
}
