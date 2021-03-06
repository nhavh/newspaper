using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class AdminUserRoleController
    {
        public int InsertAdminUserRole(AdminUserRoleInfo info)
        {
            return SQL.InsertAdminUserRole(info.UserID,info.RoleID);
        }

        public void UpdateAdminUserRole(AdminUserRoleInfo info)
        {
            SQL.UpdateAdminUserRole(info.UserRoleID, info.UserID, info.RoleID);
        }

        public void DeleteAdminUserRole(int userRoleId)
        {
            SQL.DeleteAdminUserRole(userRoleId);
        }

        public void DeleteAdminUserRole(AdminUserRoleInfo info)
        {
            DeleteAdminUserRole(info.UserRoleID);
        }

        public AdminUserRoleInfo GetAdminUserRole(int userRoleId)
        {
            return (AdminUserRoleInfo)CBO.FillObject(SQL.GetAdminUserRole(userRoleId), typeof(AdminUserRoleInfo));
        }

        public ArrayList ListAdminUserRole()
        {
            return CBO.FillCollection(SQL.ListAdminUserRole(), typeof(AdminUserRoleInfo));
        }

        public DataTable SelectAdminUserRole()
        {
            return CBO.ConvertToDataTable(ListAdminUserRole(), typeof(AdminUserRoleInfo));            
        }
       
        public ArrayList ListAdminUserRoleByUserID(int userId)
        {
            return CBO.FillCollection(SQL.ListAdminUserRoleByUserID(userId),typeof(AdminUserRoleInfo));
        }

      
    }
}
