using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMV.Data.Entities;
using TMV.Framework;
using TMV.Utilities;
using TMV.WebControls;

namespace TMV.BackEnd.Pages
{
    public partial class ListHyperLink : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewManager1.GridView.RowCommand += GetGridView_RowCommand;
            GridViewManager1.LoadData();
        }

        void GetGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() != "add") return;

            var row = (e.CommandSource as Control).Parent.Parent as GridViewRow;
            var htd = new Hashtable();

            foreach (TemplateField tf in GridViewManager1.GridView.Columns)
            {
                var item = tf.FooterTemplate as GenericItem;
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
                var ctrl = new HyperLinkController();
                var info = new HyperLinkInfo();
                foreach (System.Reflection.PropertyInfo property in CBO.GetPropertyInfo(typeof(HyperLinkInfo)))
                {
                    if (htd[property.Name] != null)
                    {
                        property.SetValue(info, htd[property.Name], null);
                    }
                }
                ctrl.InsertHyperLink(info);
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