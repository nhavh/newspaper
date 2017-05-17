using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class RegisterController
    {
        public int InsertRegister(RegisterInfo info)
        {
            return SQL.InsertRegister(info.Gender, info.FullName, info.PhoneNumber, info.Email, info.Content,info.KhoaHoc,info.NgayHoc);
        }
        public void UpdateRegister(RegisterInfo info)
        {
            SQL.UpdateRegister(info.RegisterId, info.Gender, info.FullName, info.PhoneNumber, info.Email, info.Content);
        }
        public void UpdateRegisterStatus(RegisterInfo info)
        {
            SQL.UpdateRegisterStatus(info.RegisterId, info.Status);
        }
        public void DeleteRegister(RegisterInfo info)
        {
            SQL.UpdateRegisterStatus(info.RegisterId, 3);
        }
        public List<RegisterInfo> ListRegisterByStatus(int status)
        {
            return CBO.FillCollection<RegisterInfo>(SQL.ListRegisterByStatus(status));
        }
        public DataTable SelectRegisterPending()
        {
            return CBO.ConvertToDataTable(ListRegisterByStatus(1), typeof(RegisterInfo));
        }
        public DataTable SelectRegisterSuccess()
        {
            return CBO.ConvertToDataTable(ListRegisterByStatus(2), typeof(RegisterInfo));
        }
        public DataTable SelectRegisterCancel()
        {
            return CBO.ConvertToDataTable(ListRegisterByStatus(3), typeof(RegisterInfo));
        }
    }
}
