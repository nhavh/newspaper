using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMV.Framework;
using System.Collections;
using TMV.WebControls;
using TMV.Utilities;
using TMV.Data.Entities;

namespace TMV.BackEnd.Pages
{
    public partial class ListAdminWorkFlow : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewManager1.GridView.RowCommand += new GridViewCommandEventHandler(GetGridView_RowCommand);
            GridViewManager1.LoadData();
        }

        void GetGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "add")
            {
                GridViewRow row = (e.CommandSource as Control).Parent.Parent as GridViewRow;
                Hashtable htd = new Hashtable();

                foreach (TemplateField tf in GridViewManager1.GridView.Columns)
                {
                    GenericItem item = tf.FooterTemplate as GenericItem;
                    if (item == null)
                        continue;
                    try
                    {
                        foreach (DictionaryEntry de in item.ExtractValues(row))
                        {
                            htd.Add(de.Key, de.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        Exceptions.Logger.Error(ex);
                        HtmlHelper.Alert(ex.Message, Page);
                        return;
                    }
                }

                try
                {

                    AdminWorkFlowController ctrl = new AdminWorkFlowController();
                    AdminWorkFlowInfo info = new AdminWorkFlowInfo();
                    foreach (System.Reflection.PropertyInfo property in CBO.GetPropertyInfo(typeof(AdminWorkFlowInfo)))
                    {
                        if (htd[property.Name] != null)
                        {
                            property.SetValue(info, htd[property.Name], null);
                        }
                    }
                    ctrl.InsertAdminWorkFlow(info);
                    GridViewManager1.GridView.PageIndex = GridViewManager1.GridView.PageCount;
                    GridViewManager1.LoadData();
                }
                catch (Exception ex)
                {
                    Exceptions.Logger.Error(ex);
                    HtmlHelper.Alert(ex.Message, Page);
                }
            }
        }
    }
}
