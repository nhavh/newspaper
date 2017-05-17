using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class AdminRoleController
    {
        public int InsertAdminRole(AdminRoleInfo info)
        {
            return SQL.InsertAdminRole(info.RoleName);
        }

        public void UpdateAdminRole(AdminRoleInfo info)
        {
            SQL.UpdateAdminRole(info.RoleID, info.RoleName);
        }

        public void DeleteAdminRole(int adminRoleId)
        {
            SQL.DeleteAdminRole(adminRoleId);
        }

        public void DeleteAdminRole(AdminRoleInfo info)
        {
            this.DeleteAdminRole(info.RoleID);
        }
        public AdminRoleInfo GetAdminRole(int adminRoleId)
        {
            return (AdminRoleInfo)CBO.FillObject(SQL.GetAdminPage(adminRoleId), typeof(AdminRoleInfo));
        }
        public ArrayList ListAdminRole()
        {
            return CBO.FillCollection(SQL.ListAdminRole(), typeof(AdminRoleInfo));
        }

        public ArrayList ListAdminRoleToByUser(int userId)
        {
            return CBO.FillCollection(SQL.ListAdminRoleToByUser(userId), typeof(AdminRoleInfo));
        }

        public DataTable SelectAdminRole()
        {
            return CBO.ConvertToDataTable(ListAdminRole(), typeof(AdminRoleInfo));
        }

        public DataTable SelectAdminRoleToByUser(int userId)
        {
            return CBO.ConvertToDataTable(ListAdminRoleToByUser(userId), typeof(AdminRoleInfo));
        }

        public Dictionary<object, object> ListAdminRoleLookup()
        {
            var pages = ListAdminRole();          
            var dicpages = new Dictionary<object, object>();
            foreach (AdminRoleInfo info in pages)
                dicpages.Add(info.RoleID, info.RoleName);

            return dicpages;
        }
    }
}
