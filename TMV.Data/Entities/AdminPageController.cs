using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using TMV.Data;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class AdminPageController
    {
        public int InsertAdminPage(AdminPageInfo info)
        {
            return SQL.InsertAdminPage(info.ParentID, info.Name, info.Source, info.OrderView, info.Visible, info.Locked);
        }

        public void UpdateAdminPage(AdminPageInfo info)
        {
            SQL.UpdateAdminPage(info.AdminPageID, info.ParentID, info.Name, info.Source, info.OrderView, info.Visible, info.Locked);
        }

        public void DeleteAdminPage(int adminPageId)
        {
            SQL.DeleteAdminPage(adminPageId);
        }

        public void DeleteAdminPage(AdminPageInfo info)
        {
            DeleteAdminPage(info.AdminPageID);
        }

        public AdminPageInfo GetAdminPage(int adminPageId)
        {
            return (AdminPageInfo)CBO.FillObject(SQL.GetAdminPage(adminPageId), typeof(AdminPageInfo));
        }

        public ArrayList ListAdminPage()
        {
            return CBO.FillCollection(SQL.ListAdminPage(), typeof(AdminPageInfo));
        }

        public DataTable SelectAdminPage()
        {
            return CBO.ConvertToDataTable(ListAdminPage(), typeof(AdminPageInfo));            
        }              

        public Dictionary<object, object> ListAdminPageLookup()
        {
            var pages = ListAdminPage();
            pages = HelpBindAdminPages(pages);
            var dicpages = new Dictionary<object,object>();
            foreach (AdminPageInfo info in pages)
                dicpages.Add(info.AdminPageID, info.Name);

            return dicpages;
        }

        public ArrayList HelpBindAdminPages(ArrayList pages)
        {
            var list = new ArrayList();
            foreach (AdminPageInfo info in pages)
            {
                if (info.AdminPageID != info.ParentID) continue;

                list.Add(info);
                pages.Remove(info);
                break;
            }
            if (pages.Count > 0)
                SetIndent(pages, -1, 0, ref list);

            return list;
        }

        private void SetIndent(ArrayList pages, int adminPageId, int level, ref ArrayList listResult)
        {
            foreach (AdminPageInfo info in pages)
            {
                if (info.ParentID != adminPageId) continue;

                if (level != 0)
                    info.Name = new string('.', level * 3) + info.Name;

                listResult.Add(info);
                SetIndent(pages, info.AdminPageID, level + 1, ref listResult);
            }
        }
        //lấy danh sách PageID theo các RolesID của 1 UserID
        public ArrayList ListAdminPageByRoles(int userid)
        {
            return CBO.FillCollection(SQL.ListAdminPageByRole(userid), typeof(AdminPageInfo));
        }

        public List<AdminPageInfo> ListAdminPageEx()
        {
            return CBO.FillCollection<AdminPageInfo>(SQL.ListAdminPage());
        }
        public List<AdminPageInfo> ListAdminPageByRolesEx(int userid)
        {
            return CBO.FillCollection<AdminPageInfo>(SQL.ListAdminPageByRole(userid));
        }
    }
}
