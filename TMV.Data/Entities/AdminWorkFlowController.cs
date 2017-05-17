using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class AdminWorkFlowController
    {
        public int InsertAdminWorkFlow(AdminWorkFlowInfo info)
        {
            return SQL.InsertAdminWorkFlow(info.FromID,info.ToID);
        }

        public void UpdateAdminWorkFlow(AdminWorkFlowInfo info)
        {
            SQL.UpdateAdminWorkFlow(info.WorkFlowID, info.FromID, info.ToID);
        }

        public void DeleteAdminWorkFlow(int workFlowId)
        {
            SQL.DeleteAdminWorkFlow(workFlowId);
        }

        public void DeleteAdminWorkFlow(AdminWorkFlowInfo info)
        {
            DeleteAdminWorkFlow(info.WorkFlowID);
        }

        public AdminWorkFlowInfo GetAdminWorkFlow(int workFlowId)
        {
            return (AdminWorkFlowInfo)CBO.FillObject(SQL.GetAdminWorkFlow(workFlowId), typeof(AdminWorkFlowInfo));
        }

        public ArrayList ListAdminWorkFlow()
        {
            return CBO.FillCollection(SQL.ListAdminWorkFlow(), typeof(AdminWorkFlowInfo));
        }

        public DataTable SelectAdminWorkFlow()
        {
            return CBO.ConvertToDataTable(ListAdminWorkFlow(), typeof(AdminWorkFlowInfo));            
        }                     
    }
}
