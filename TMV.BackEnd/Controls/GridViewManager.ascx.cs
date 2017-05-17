using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMV.WebControls;
using TMV.Utilities;

namespace TMV.BackEnd.Controls
{
    public partial class GridViewManager : UserControl
    {        
        #region variable
        private DataSet ds;
        public string PrimaryKeyName = string.Empty;        
        private bool _showUpdate = true;
        private bool _showDelete = true;
        private bool _showID = true;
        
        public string SetTitleGridManager
        {
            set { lblTitle.Text = value; }
        }
        
        public bool ShowUpdateCommand
        {
            set { _showUpdate = value; }
            get { return _showUpdate; }
        }                

        public bool ShowDeleteCommand
        {
            set { _showDelete = value; }
            get { return _showDelete; }
        }

        public bool ShowID
        {
            set { _showID = value; }
            get { return _showID; }
        }
        #endregion

        public GridView GridView
        {
            get { return gvManager; }
        }

        public ObjectDataSource DataSource
        {
            get { return objDataSource; }
        }       

        public void LoadData()
        {
            if (gvManager.DataKeyNames.Length <= 0)
                gvManager.DataKeyNames = new string[] { PrimaryKeyName };
            gvManager.DataSourceID = "objDataSource";
            objDataSource.FilterExpression = ViewState[this.ClientID + "FilterExpression"] as string;
        }

        void SelectParametersInfo()
        {
            DataTable dsObjField = ds.Tables["SelectParameter"];
            foreach (DataRow dr in dsObjField.Rows)
            {
                Parameter param = new Parameter();
                param.Name = dr["Name"].ToString();
                switch (((string)dr["Type"]).ToLower())
                {
                    case "double":
                        param.Type = TypeCode.Double;
                        break;
                    case "bool":
                        param.Type = TypeCode.Boolean;
                        break;
                    case "byte":
                        param.Type = TypeCode.Byte;
                        break;
                    case "int":
                    case "int32":
                        param.Type = TypeCode.Int32;
                        break;
                    case "string":
                        param.Type = TypeCode.String;
                        break;
                    case "datetime":
                        param.Type = TypeCode.DateTime;
                        break;
                }

                if (param.Type == TypeCode.DateTime)
                    param.DefaultValue = DateTime.Now.ToString();
                else
                    param.DefaultValue = dr.IsNull("Default") ? string.Empty : dr["Default"].ToString();

                param.Direction = ParameterDirection.Input;
                objDataSource.SelectParameters.Add(param);
            }
        }
        void ObjectDataSourceInfo()
        {
            DataTable dsObjField = ds.Tables["ObjectDataSource"];
            if (dsObjField.Rows.Count > 0)
            {
                DataRow row = dsObjField.Rows[0];
                objDataSource.DataObjectTypeName = row.IsNull("ClassInfo") ? string.Empty : row["ClassInfo"].ToString();
                objDataSource.TypeName = row.IsNull("ClassController") ? string.Empty : row["ClassController"].ToString();
                objDataSource.SelectMethod = row.IsNull("SelectMethod") ? string.Empty : row["SelectMethod"].ToString();
                objDataSource.UpdateMethod = row.IsNull("UpdateMethod") ? string.Empty : row["UpdateMethod"].ToString();
                objDataSource.DeleteMethod = row.IsNull("DeleteMethod") ? string.Empty : row["DeleteMethod"].ToString();
                this.PrimaryKeyName = row.IsNull("PrimaryKeyName") ? string.Empty : row["PrimaryKeyName"].ToString();

            }
        }

        void GridConfigInfo()
        {
            DataTable dsObjField = ds.Tables["GridConfig"];
            if (dsObjField.Rows.Count > 0)
            {
                DataRow row = dsObjField.Rows[0];
                SetTitleGridManager = row.IsNull("Title") ? string.Empty : row["Title"].ToString();
                ShowUpdateCommand = row.IsNull("ShowUpdate") ? true : bool.Parse(row["ShowUpdate"].ToString());
                ShowDeleteCommand = row.IsNull("ShowDelete") ? true : bool.Parse(row["ShowDelete"].ToString());
                ShowID = row.IsNull("ShowID") ? true : bool.Parse(row["ShowID"].ToString());
                gvManager.ShowFooter = row.IsNull("ShowInsert") ? true : bool.Parse(row["ShowInsert"].ToString());                
            }
        }

        protected void gvManager_Init(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["xml"]))
                Response.Redirect("~/Default.aspx", true);

            string XmlFile = Request.QueryString["xml"];
            string pathfile = Server.MapPath("~/xml/");

            ds = (DataSet)Cache["Manager" + XmlFile];
            if (ds == null)
            {
                ds = new DataSet("Manager");
                DataTable dtField = new DataTable("SelectParameter");
                dtField.Columns.Add("Name", typeof(string));
                dtField.Columns.Add("Type", typeof(string));
                dtField.Columns.Add("Default", typeof(string));
                ds.Tables.Add(dtField);                

                dtField = new DataTable("ObjectDataSource");
                dtField.Columns.Add("ClassInfo", typeof(string));
                dtField.Columns.Add("ClassController", typeof(string));
                dtField.Columns.Add("SelectMethod", typeof(string));
                dtField.Columns.Add("UpdateMethod", typeof(string));
                dtField.Columns.Add("DeleteMethod", typeof(string));                
                dtField.Columns.Add("PrimaryKeyName", typeof(string));
                ds.Tables.Add(dtField);

                dtField = new DataTable("GridConfig");
                dtField.Columns.Add("Title", typeof(string));
                dtField.Columns.Add("ShowUpdate", typeof(bool));
                dtField.Columns.Add("ShowInsert", typeof(bool));
                dtField.Columns.Add("ShowDelete", typeof(bool));
                dtField.Columns.Add("ShowID", typeof(bool)); 
                ds.Tables.Add(dtField);

                dtField = new DataTable("Column");
                dtField.Columns.Add("HeaderText", typeof(string));
                dtField.Columns.Add("ItemWidth", typeof(int));
                dtField.Columns.Add("FieldName", typeof(string));
                dtField.Columns.Add("FieldType", typeof(string));
                dtField.Columns.Add("FieldTypeKey", typeof(string));
                dtField.Columns.Add("FieldAllowNull", typeof(bool));
                dtField.Columns.Add("FieldLookup", typeof(string));         
                dtField.Columns.Add("FieldUrlName", typeof(string));
                dtField.Columns.Add("FieldUrlText", typeof(string));                         
                dtField.Columns.Add("ReadOnly", typeof(bool));
                dtField.Columns.Add("ControllerInfo", typeof(string));
                ds.Tables.Add(dtField);


                ds.ReadXml(pathfile + XmlFile + ".xml", XmlReadMode.IgnoreSchema);
                Cache.Insert("Manager" + XmlFile, ds, new System.Web.Caching.CacheDependency(pathfile + XmlFile + ".xml"));
            }

            int i = 1; //Column 0 is the ID;
            foreach (DataRow dr in ds.Tables["Column"].Rows)
            {
                TemplateField tf = new TemplateField();

                tf.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                tf.HeaderStyle.VerticalAlign = VerticalAlign.Top;
                tf.HeaderStyle.Width = (int)dr["ItemWidth"];
                tf.ItemStyle.Width = (int)dr["ItemWidth"];
                tf.FooterStyle.Width = (int)dr["ItemWidth"];

                tf.ItemTemplate = new GenericItem(dr, GenericItem.ModeOfItem);
                tf.EditItemTemplate = new GenericItem(dr, GenericItem.ModeOfEditItem);
                tf.FooterTemplate = new GenericItem(dr, GenericItem.ModeOfFooter);
                tf.HeaderTemplate = new GenericItem(dr, GenericItem.ModeOfHeader);

                gvManager.Columns.Insert(i++, tf);
            }

            GridConfigInfo();
            ObjectDataSourceInfo();
            SelectParametersInfo();                   
        }

        protected void gvManager_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToLower())
                {
                    case "filter":
                        FilterCommand(e);
                        LoadData();
                        break;                    
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                //Exceptions.Logger.Error(ex);
                HtmlHelper.Alert(ex.Message, Page);
            }
        }        

        protected void FilterCommand(GridViewCommandEventArgs e)
        {
            GridViewRow row = (e.CommandSource as Control).Parent.Parent as GridViewRow;
            string filterExpression = string.Empty;

            foreach (TemplateField tf in gvManager.Columns)
            {
                GenericItem item = tf.HeaderTemplate as GenericItem;
                if (item == null)
                    continue;
                foreach (DictionaryEntry de in item.ExtractValues(row))
                {
                    string key = "[" + de.Key + "]";
                    string value;
                    if (de.Value == DBNull.Value)
                        value = " IS NULL";
                    else if (de.Value.GetType() == typeof(string))
                        value = " LIKE '%" + de.Value.ToString() + "%' ";
                    else if (de.Value.GetType() == typeof(DateTime))
                        value = "='" + de.Value.ToString() + "'";
                    else
                        value = "=" + de.Value.ToString();

                    filterExpression += " AND " + key + value;
                }
            }

            filterExpression = GridviewHelper.getFilter(filterExpression);
            ViewState[this.ClientID + "FilterExpression"] = filterExpression;
        }

        protected void gvManager_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (gvManager.DataKeyNames.Length == 0 && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Controls.Clear();
            }
        }

        protected void gvManager_PreRender(object sender, EventArgs e)
        {
            if (gvManager.Rows.Count == 0)
            {
                gvManager.DataSourceID = null;
                gvManager.DataKeyNames = new string[0];

                ArrayList al = new ArrayList();
                al.Add(string.Empty);
                gvManager.AllowSorting = false;
                try
                {
                    gvManager.DataSource = al;
                    gvManager.DataBind();
                }
                catch
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
        }
    }
}