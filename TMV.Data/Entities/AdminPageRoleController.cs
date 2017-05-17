using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMV.Utilities;
using System.Collections;

namespace TMV.Data.Entities
{
    public class AdminPageRoleController
    {

        public int InsertAdminPageRole(AdminPageRoleInfo info)
        {
            return SQL.InsertAdminPageRole(info.PageID,info.RoleID);
        }
        public void UpdateAdminPageRole(AdminPageRoleInfo info)
        {
            SQL.UpdateAdminPageRole(info.PageRoleID, info.PageID, info.RoleID);
        }
        public void DeleteAdminPageRole(int id)
        {
            SQL.DeleteAdminPageRole(id);
        }
        public void DeleteAdminPageRole(AdminPageRoleInfo info)
        {
            DeleteAdminPageRole(info.PageRoleID);
        }
        public ArrayList ListAdminPageRole()
        {
            return CBO.FillCollection(SQL.ListAdminPageRole(), typeof(AdminPageRoleInfo));
        }
        public DataTable SelectAdminPageRole()
        {
            return CBO.ConvertToDataTable(ListAdminPageRole(), typeof(AdminPageRoleInfo));
        }

      
    }
}
