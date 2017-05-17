using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using TMV.Utilities;

namespace TMV.WebControls
{
    public class GenericItem : IBindableTemplate
    {
        #region TemplateField
        public const int ModeOfItem = 1;
        public const int ModeOfEditItem = 2;
        public const int ModeOfHeader = 3;
        public const int ModeOfFooter = 4;
        #endregion

        #region FieldType
        public const string TypeOfBool = "bool";
        public const string TypeOfInt = "int";
        public const string TypeOfByte = "byte";
        public const string TypeOfLookup = "lookup";
        public const string TypeOfFloat = "float";
        public const string TypeOfString = "string";
        public const string TypeOfText = "text";
        public const string TypeOfPassword = "password";
        public const string TypeOfDate = "date";
        public const string TypeOfDateTime = "datetime";
        public const string TypeOfCheckBox = "checkbox";    
        public const string TypeOfDecimal = "decimal";
        #endregion

        private int _modeID;
        private string _headerText;
        private int _headerWidth;

        #region Gridview Column Properties
        private string _fieldName;
        private string _fieldType;
        private bool _fieldAllowNull;
        private IdNameHelper _fieldLookupHelper;
        private string _fieldLookup;
        private bool _fieldAllowLink;
        private string _fieldUrlName;
        private string _fieldUrlText;     
        private bool _readOnly = false;
        private string _fieldTypeKey;
        private string _controllerInfo;       
        #endregion

        public GenericItem(DataRow Row, int Mode)
        {

            _fieldName = (string)Row["FieldName"];
            _fieldType = (Row["FieldType"] as string).ToLower();
            _fieldAllowNull = false;

            if (_fieldType != "object" && _fieldType != "password" && !Row.IsNull("FieldAllowNull"))
                _fieldAllowNull = (bool)Row["FieldAllowNull"];


            _readOnly = false;
            if (!Row.IsNull("ReadOnly"))
                _readOnly = (bool)Row["ReadOnly"];

            _fieldTypeKey = "int";
            if (!Row.IsNull("FieldTypeKey"))
                _fieldTypeKey = (string)Row["FieldTypeKey"];

            if (_fieldType == TypeOfLookup)
            {
                _fieldLookup = (string)Row["FieldLookup"];
                if (!_fieldLookup.Contains(".xml"))
                    _controllerInfo = (string)Row["ControllerInfo"];
                _fieldLookupHelper = new IdNameHelper(_fieldLookup, _fieldAllowNull ? "*" : null, _fieldTypeKey, _controllerInfo);
            }

            if (_fieldAllowLink = !Row.IsNull("FieldUrlText"))
            {
                _fieldUrlName = (string)Row["FieldUrlName"];
                _fieldUrlText = (string)Row["FieldUrlText"];                
            }                     

            _modeID = Mode;
            _headerText = (string)Row["HeaderText"];
            _headerWidth = (int)Row["ItemWidth"];
        }


        public void InstantiateIn(Control container)
        {
            switch (_modeID)
            {
                case ModeOfItem:
                    switch (_fieldType)
                    {
                        case TypeOfCheckBox:
                            container.Controls.Add(GridviewHelper.newCheckBox("chk" + _fieldName, new EventHandler(BindItemCheckBox)));
                            break;                        
                        default:
                            if (!_fieldAllowLink)
                                container.Controls.Add(GridviewHelper.newLabel("lb" + _fieldName, new EventHandler(BindItemLabel)));
                            else
                                container.Controls.Add(GridviewHelper.newHyperLink(new EventHandler(BindItemHyperLink)));
                            break;
                    }
                    break;

                case ModeOfEditItem:
                    switch (_fieldType)
                    {
                        case TypeOfCheckBox:
                            container.Controls.Add(GridviewHelper.newCheckBox("chk" + _fieldName, new EventHandler(BindItemCheckBox)));
                            break;
                        case TypeOfBool:
                            container.Controls.Add(GridviewHelper.newBooleanDropDownList("cbo" + _fieldName, new EventHandler(BindEditItemBooleanDropDownList), _fieldAllowNull, true, null));
                            break;
                        case TypeOfLookup:
                            container.Controls.Add(GridviewHelper.newDropDownList("cbo" + _fieldName, new EventHandler(BindEditItemDropDownList), _fieldLookupHelper, false, null));
                            break;                        
                        default:
                            if (_readOnly)
                                container.Controls.Add(GridviewHelper.newLabel(new EventHandler(BindItemLabel)));
                            else
                                container.Controls.Add(GridviewHelper.newTextBox("txt" + _fieldName, new EventHandler(BindEditItemTextBox), null));
                            break;
                    }
                    break;

                case ModeOfFooter:
                    switch (_fieldType)
                    {                        
                        case TypeOfLookup:
                            container.Controls.Add(GridviewHelper.newDropDownList("cbo" + _fieldName, null, _fieldLookupHelper, false, null));
                            break;
                        case TypeOfBool:
                            container.Controls.Add(GridviewHelper.newBooleanDropDownList("cbo" + _fieldName, null, _fieldAllowNull, false, null));
                            break;
                        default:
                            container.Controls.Add(GridviewHelper.newTextBox("txt" + _fieldName, null, null));
                            break;
                    }
                    break;

                case ModeOfHeader:
                    container.Controls.Add(GridviewHelper.newHeaderButton(_fieldName, _headerText));
                   
                    switch (_fieldType)
                    {
                        case TypeOfPassword:
                            break;
                        case TypeOfBool:
                            container.Controls.Add(GridviewHelper.newBooleanDropDownList("cbo" + _fieldName, new EventHandler(BindEditItemBooleanDropDownList), _fieldAllowNull, true, null));
                            break;             
                        case TypeOfCheckBox:
                            container.Controls.Add(GridviewHelper.newCheckBox("chkAll" + _fieldName , new EventHandler(BindEditItemCheckBox)));
                            break;
                        case TypeOfLookup:
                            container.Controls.Add(GridviewHelper.newDropDownList("cbo" + _fieldName, new EventHandler(BindEditItemDropDownList), _fieldLookupHelper, true, null));
                            break;
                        default:
                            container.Controls.Add(GridviewHelper.newTextBox("txt" + _fieldName, new EventHandler(BindEditItemTextBox), null));
                            break;
                    }
                    break;
            }
        }

        public void BindItemLabel(object sender, EventArgs e)
        {
            Label l = (Label)sender;

            DataRowView drw = ((sender as Control).NamingContainer as GridViewRow).DataItem as DataRowView;
            object o = drw[_fieldName];

            switch (_fieldType)
            {
                case TypeOfLookup:
                    l.Text = _fieldLookupHelper.GetName(o).ToString();
                    break;
                case TypeOfPassword:
                    l.Text = "**********";
                    break;
                case TypeOfDate:
                    if (o != DBNull.Value)
                        l.Text = ((DateTime)o).ToShortDateString();
                    break;
                case TypeOfDateTime:
                    if (o != DBNull.Value)
                        l.Text = ((DateTime)o).ToString();
                    break;
                case TypeOfInt:
                    if (Convert.ToInt32(o) != Null.NullInteger)
                        l.Text = Convert.ToInt32(o).ToString();
                    else
                        l.Text = string.Empty;
                    break;
                case TypeOfFloat:
                    if (Convert.ToDouble(o) != Null.NullDouble)
                        l.Text = Convert.ToDouble(o).ToString("#,000", new System.Globalization.CultureInfo("vi-VN"));
                    else
                        l.Text = string.Empty;
                    break;                
                case TypeOfDecimal:
                    if(Convert.ToDecimal(o) != Null.NullDecimal)
                        l.Text = Convert.ToDecimal(o).ToString("#,000", new System.Globalization.CultureInfo("vi-VN"));
                    else

                        l.Text = string.Empty;
                    break;
                default:
                    o = HttpContext.Current.Server.HtmlDecode(o.ToString());
                    l.Text = (o.ToString().Length > 256 ? o.ToString().Substring(0, 256) + "...." : o.ToString());
                    break;
            }
        }

        public void BindItemCheckBox(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.InputAttributes.Add("class", "gridItemCheckBox");
            DataRowView drw = ((sender as Control).NamingContainer as GridViewRow).DataItem as DataRowView;
            object o = drw[_fieldName];
            if (o.ToString().ToLower().Equals("true"))
                chk.Checked = true;
            else
                chk.Checked = false;

            if (_fieldTypeKey.ToLower() != "int" )
            {
                if (drw[_fieldTypeKey] != null)
                {
                    object objValue = drw[_fieldTypeKey];
                    chk.InputAttributes.Add("value", objValue.ToString());
                }
            }
        }        

        public void BindItemHyperLink(object sender, EventArgs e)
        {
            HyperLink hl = (HyperLink)sender;
            DataRowView drw = ((sender as Control).NamingContainer as GridViewRow).DataItem as DataRowView;
            object o = drw[_fieldName];

            switch (_fieldType)
            {
                case TypeOfLookup:
                    hl.Text = _fieldLookupHelper.GetName(o).ToString();
                    break;
                default:
                    hl.ID = "hpLink";
                    hl.Text = drw[_fieldName].ToString();
                    break;
            }           
            hl.NavigateUrl = string.Format(_fieldUrlText,
                                HttpUtility.UrlEncode(drw[_fieldUrlName].ToString()),
                                        HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString())).Replace("%2f","/");            
        }

        public void BindEditItemTextBox(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;

            DataRowView drw = ((sender as Control).NamingContainer as GridViewRow).DataItem as DataRowView;
            if (drw == null)
            {
                t.Text = HttpContext.Current.Request.Form[t.ClientID.Replace('_', '$')];
                return;
            }
            object o = drw[_fieldName];

            switch (_fieldType)
            {
                case TypeOfText:
                    t.TextMode = TextBoxMode.MultiLine;
                    t.Rows = 4;
                    t.Text = o.ToString();
                    break;
                case TypeOfPassword:
                    t.TextMode = TextBoxMode.Password;
                    break;
                case TypeOfDate:
                    if (o != DBNull.Value)
                        t.Text = ((DateTime)o).ToShortDateString();
                    break;
                case TypeOfDateTime:
                    if (o != DBNull.Value)
                        t.Text = ((DateTime)o).ToString();
                    break;
                case TypeOfInt:
                    if (Convert.ToInt32(o) != Null.NullInteger)
                        t.Text = Convert.ToInt32(o).ToString();
                    else
                        t.Text = string.Empty;
                    break;
                case TypeOfFloat:
                    if (Convert.ToDouble(o) != Null.NullDouble)
                        t.Text = Convert.ToDouble(o).ToString();
                    else
                        t.Text = string.Empty;
                    break;
                case TypeOfDecimal:
                    if(Convert.ToDecimal(o) != Null.NullDecimal)
                        t.Text = Convert.ToDecimal(o).ToString();
                    else
                        t.Text = string.Empty;
                    break;
                default:
                    t.Text = o.ToString();
                    break;
            }
        }

        public void BindEditItemCheckBox(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.InputAttributes.Add("class", "gridHeaderCheckbox");
            DataRowView drw = ((sender as Control).NamingContainer as GridViewRow).DataItem as DataRowView;
            if (drw == null)
            {
                chk.Checked = false;
            }
            else
            {
                object o = drw[_fieldName];
                if (o.ToString().ToLower().Equals("true"))
                    chk.Checked = true;
                else
                    chk.Checked = false;
            }

        }

        public void BindEditItemBooleanDropDownList(object sender, EventArgs e)
        {
            DropDownList d = (DropDownList)sender;

            DataRowView drw = ((sender as Control).NamingContainer as GridViewRow).DataItem as DataRowView;
            if (drw == null)
            {
                d.SelectedValue = HttpContext.Current.Request.Form[d.ClientID.Replace('_', '$')];
                return;
            }
            object o = drw[_fieldName];

            if (o == null)
                d.SelectedValue = "-1";
            else
            {
                d.SelectedValue = (o.ToString().ToLower() == "true" ? "1" : "0");
            }
        }

        public void BindEditItemDropDownList(object sender, EventArgs e)
        {
            DropDownList d = (DropDownList)sender;

            DataRowView drw = ((sender as Control).NamingContainer as GridViewRow).DataItem as DataRowView;
            if (drw == null)
            {
                d.SelectedValue = HttpContext.Current.Request.Form[d.ClientID.Replace('_', '$')];
                return;
            }
            object o = drw[_fieldName];
            if (o == null)
                o = Null.GetNullOfTypeKey(_fieldTypeKey);

            ListItem i = d.Items.FindByValue(o.ToString());
            if (i != null)
                i.Selected = true;
        }

        public IOrderedDictionary ExtractValues(Control container)
        {
            OrderedDictionary dic = new OrderedDictionary();

            if (_modeID == ModeOfItem)
                return dic;

            string fieldName = _fieldName;
            object fieldValue = null;
            string sValue = null;

            GridviewHelper helperGridview = new GridviewHelper(container as GridViewRow);
            TextBox txt = helperGridview.findTextBox("txt" + fieldName);
            if (txt != null)
                sValue = txt.Text.Trim();
            DropDownList cbo = helperGridview.findDropDownList("cbo" + fieldName);
            if (cbo != null)
                sValue = cbo.SelectedValue.Trim();

            if (_modeID == ModeOfHeader && HtmlHelper.IsNullOrEmpty(sValue))
                return dic;

            if (_fieldAllowNull && (HtmlHelper.IsNullOrEmpty(sValue) || sValue == int.MinValue.ToString()))
            {
                switch (_fieldType)
                {
                    case TypeOfBool:
                        fieldValue = Null.NullBoolean;
                        break;
                    case TypeOfInt:
                        fieldValue = Null.NullInteger;
                        break;
                    case TypeOfByte:
                        fieldValue = Null.NullShort;
                        break;
                    case TypeOfLookup:
                        fieldValue = Null.GetNullOfTypeKey(_fieldTypeKey);
                        break;
                    case TypeOfFloat:
                        fieldValue = Null.NullDouble;
                        break;
                    case TypeOfDecimal:
                        fieldValue = Null.NullDecimal;
                        break;
                    case TypeOfString:
                    case TypeOfText:
                        fieldValue = Null.NullString;
                        break;
                    case TypeOfPassword:
                        fieldValue = Null.NullString;
                        break;
                    case TypeOfDate:
                    case TypeOfDateTime:
                        fieldValue = Null.NullDate;
                        break;
                }
            }
            else
            {
                try
                {
                    if (_fieldAllowNull == false && HtmlHelper.IsNullOrEmpty(sValue))
                        throw new Exception("Field '" + fieldName + "' not allow Null!");

                    switch (_fieldType)
                    {
                        case TypeOfBool:
                            fieldValue = cbo.SelectedValue != "0";
                            break;
                        case TypeOfInt:
                            fieldValue = int.Parse(sValue);
                            break;
                        case TypeOfByte:
                            fieldValue = byte.Parse(sValue);
                            break;
                        case TypeOfLookup:
                            fieldValue = Null.GetNullOfTypeKey(_fieldTypeKey);
                            switch (_fieldTypeKey.ToLower())
                            {
                                case "double":
                                    fieldValue = double.Parse(sValue);
                                    break;

                                case "bool":
                                    fieldValue = bool.Parse(sValue);
                                    break;
                                case "byte":
                                    fieldValue = byte.Parse(sValue);
                                    break;
                                case "int":
                                case "int32":
                                    fieldValue = int.Parse(sValue);
                                    break;
                                case "string":
                                    fieldValue = sValue;
                                    break;
                                case "datetime":
                                    fieldValue = DateTime.Parse(sValue);
                                    break;
                            }

                            break;
                        case TypeOfFloat:
                            fieldValue = float.Parse(sValue);
                            break;
                        case TypeOfDecimal:
                            fieldValue = decimal.Parse(sValue);
                            break;
                        case TypeOfString:
                        case TypeOfText:
                            fieldValue = sValue;
                            break;
                        case TypeOfPassword:
                            fieldValue = sValue;
                            break;
                        case TypeOfDate:
                        case TypeOfDateTime:
                            fieldValue = DateTime.Parse(sValue);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (_modeID == ModeOfFooter)
                    {
                        if (_fieldAllowNull)
                        {
                            switch (_fieldType)
                            {
                                case TypeOfBool:
                                    fieldValue = Null.NullBoolean;
                                    break;
                                case TypeOfInt:
                                    fieldValue = Null.NullInteger;
                                    break;
                                case TypeOfLookup:
                                    fieldValue = Null.GetNullOfTypeKey(_fieldTypeKey);
                                    break;
                                case TypeOfFloat:
                                    fieldValue = Null.NullDouble;
                                    break;
                                case TypeOfDecimal:
                                    fieldValue = Null.NullDecimal;
                                    break;
                                case TypeOfString:
                                case TypeOfText:
                                    fieldValue = Null.NullString;
                                    break;
                                case TypeOfPassword:
                                    fieldValue = Null.NullString;
                                    break;
                                case TypeOfDate:
                                case TypeOfDateTime:
                                    fieldValue = Null.NullDate;
                                    break;
                            }
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                }
            }

            if (fieldValue != null)
                dic.Add(fieldName, fieldValue);

            return dic;
        }

        public IOrderedDictionary ExtractItemTemplateValues(Control container)
        {
            OrderedDictionary dic = new OrderedDictionary();
            string fieldName = _fieldName;
            object fieldValue = null;
            string sValue = null;


            if (_modeID == ModeOfItem)
            {
                GridviewHelper helperGridview = new GridviewHelper(container as GridViewRow);
                Label lb = helperGridview.findLabel("lb" + fieldName);
                if (lb != null)
                    sValue = lb.Text.Trim();
                CheckBox chk = helperGridview.findCheckBox("chk" + fieldName);
                if (chk != null)
                    sValue = chk.Checked.ToString();

                if (_modeID == ModeOfHeader && HtmlHelper.IsNullOrEmpty(sValue))
                    return dic;

                if (_fieldAllowNull && (HtmlHelper.IsNullOrEmpty(sValue) || sValue == int.MinValue.ToString()))
                {
                    switch (_fieldType)
                    {
                        case TypeOfBool:
                            fieldValue = Null.NullBoolean;
                            break;
                        case TypeOfInt:
                            fieldValue = Null.NullInteger;
                            break;
                        case TypeOfLookup:
                            fieldValue = Null.GetNullOfTypeKey(_fieldTypeKey);
                            break;
                        case TypeOfFloat:
                            fieldValue = Null.NullDouble;
                            break;
                        case TypeOfDecimal:
                            fieldValue = Null.NullDecimal;
                            break;
                        case TypeOfString:
                        case TypeOfText:
                            fieldValue = Null.NullString;
                            break;
                        case TypeOfPassword:
                            fieldValue = Null.NullString;
                            break;
                        case TypeOfDate:
                        case TypeOfDateTime:
                            fieldValue = Null.NullDate;
                            break;
                    }
                }
                else
                {
                    try
                    {
                        if (_fieldAllowNull == false && HtmlHelper.IsNullOrEmpty(sValue))
                            throw new Exception("Field '" + fieldName + "' not allow Null!");

                        switch (_fieldType)
                        {
                            case TypeOfBool:
                                fieldValue = chk.Checked;
                                break;
                            case TypeOfInt:
                                fieldValue = int.Parse(sValue);
                                break;
                            case TypeOfLookup:
                                fieldValue = Null.GetNullOfTypeKey(_fieldTypeKey);
                                switch (_fieldTypeKey.ToLower())
                                {
                                    case "double":
                                        fieldValue = double.Parse(sValue);
                                        break;

                                    case "bool":
                                        fieldValue = bool.Parse(sValue);
                                        break;
                                    case "int":
                                    case "int32":
                                        fieldValue = int.Parse(sValue);
                                        break;
                                    case "string":
                                        fieldValue = sValue;
                                        break;
                                    case "datetime":
                                        fieldValue = DateTime.Parse(sValue);
                                        break;
                                }

                                break;
                            case TypeOfFloat:
                                fieldValue = float.Parse(sValue);
                                break;
                            case TypeOfDecimal:
                                fieldValue = decimal.Parse(sValue);
                                break;
                            case TypeOfString:
                            case TypeOfText:
                                fieldValue = sValue;
                                break;
                            case TypeOfPassword:
                                fieldValue = sValue;
                                break;
                            case TypeOfDate:
                            case TypeOfDateTime:
                                fieldValue = DateTime.Parse(sValue);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (_modeID == ModeOfFooter)
                        {
                            if (_fieldAllowNull)
                            {
                                switch (_fieldType)
                                {
                                    case TypeOfBool:
                                        fieldValue = Null.NullBoolean;
                                        break;
                                    case TypeOfInt:
                                        fieldValue = Null.NullInteger;
                                        break;
                                    case TypeOfLookup:
                                        fieldValue = Null.GetNullOfTypeKey(_fieldTypeKey);
                                        break;
                                    case TypeOfFloat:
                                        fieldValue = Null.NullDouble;
                                        break;
                                    case TypeOfDecimal:
                                        fieldValue = Null.NullDecimal;
                                        break;
                                    case TypeOfString:
                                    case TypeOfText:
                                        fieldValue = Null.NullString;
                                        break;
                                    case TypeOfPassword:
                                        fieldValue = Null.NullString;
                                        break;
                                    case TypeOfDate:
                                    case TypeOfDateTime:
                                        fieldValue = Null.NullDate;
                                        break;
                                }
                            }
                            else
                            {
                                throw ex;
                            }
                        }
                    }
                }

            }



            if (fieldValue != null)
                dic.Add(fieldName, fieldValue);

            return dic;
        }


    }

    public class GridviewHelper
    {
        private TableRow item;

        public GridviewHelper(TableRow item)
        {
            this.item = item;
        }

        public bool isItem()
        {
            DataGridItem item = this.item as DataGridItem;
            return (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem);
        }

        public LinkButton findLinkButton(string controlName)
        {
            return (LinkButton)item.FindControl(controlName);
        }

        public DropDownList findDropDownList(string controlName)
        {
            return (DropDownList)item.FindControl(controlName);
        }

        public TextBox findTextBox(string controlName)
        {
            return (TextBox)item.FindControl(controlName);
        }

        public CheckBox findCheckBox(string controlName)
        {
            return (CheckBox)item.FindControl(controlName);
        }

        public FileUpload findFileUpload(string controlName)
        {
            return (FileUpload)item.FindControl(controlName);
        }

        public Label findLabel(string controlName)
        {
            return (Label)item.FindControl(controlName);
        }

        public static TextBox newTextBox(string id, System.EventHandler e, string defaultValue)
        {
            TextBox t = new TextBox();

            t.ID = id;
            t.DataBinding += e;
            t.Width = new Unit(100, UnitType.Percentage);
            if (defaultValue != null)
                t.Text = defaultValue;

            return t;
        }

        public static DropDownList newDropDownList(string id, System.EventHandler e, IdNameHelper helper, bool hasEmpty, string defaultValue)
        {
            DropDownList d = new DropDownList();

            d.ID = id;
            d.DataBinding += e;
            d.Width = new Unit(100, UnitType.Percentage);
            if (hasEmpty)
                d.Items.Add(string.Empty);
            helper.PopulateList(d);
            if (defaultValue != null)
            {
                ListItem i = (ListItem)d.Items.FindByValue(defaultValue);
                if (i != null)
                    i.Selected = true;
            }

            return d;
        }

        public static DropDownList newBooleanDropDownList(string id, System.EventHandler e, bool allowNull, bool hasEmpty, string defaultValue)
        {
            DropDownList d = new DropDownList();

            d.ID = id;
            d.DataBinding += e;
            d.Width = new Unit(100, UnitType.Percentage);
            if (hasEmpty)
                d.Items.Add(string.Empty);
            if (allowNull)
                d.Items.Add(new ListItem("*", Null.NullBoolean.ToString()));  
          
            d.Items.Add(new ListItem("False", "0"));
            d.Items.Add(new ListItem("True", "1"));            
            if (defaultValue != null)
            {
                ListItem i = (ListItem)d.Items.FindByValue(defaultValue);
                if (i != null)
                    i.Selected = true;
            }

            return d;
        }

        public static CheckBox newCheckBox(string id, System.EventHandler e)
        {
            CheckBox chk = new CheckBox();
            chk.ID = id;
            chk.DataBinding += e;
            chk.Width = new Unit(100, UnitType.Percentage);
            chk.Checked = false;
            return chk;
        }        

        public static Label newLabel(System.EventHandler e)
        {
            Label l = new Label();

            l.DataBinding += e;
            l.Width = new Unit(100, UnitType.Percentage);

            return l;
        }

        public static Label newLabel(string id, System.EventHandler e)
        {
            Label l = new Label();
            l.ID = id;
            l.DataBinding += e;
            l.Width = new Unit(100, UnitType.Percentage);

            return l;
        }

        public static HyperLink newHyperLink(System.EventHandler e)
        {
            HyperLink hl = new HyperLink();

            hl.DataBinding += e;
            hl.Width = new Unit(100, UnitType.Percentage);

            return hl;
        }

        public static LinkButton newHeaderButton(string id, string text)
        {
            LinkButton lb = new LinkButton();
            lb.ID = "cmdS" + id;
            lb.CommandName = "Sort";
            lb.CommandArgument = id;
            lb.ToolTip = "Sort by " + text;
            lb.Text = text;
            lb.Width = new Unit(100, UnitType.Percentage);
            lb.CssClass = "HeaderButton";

            return lb;
        }

        public static string parseSortExpression(string NewValue, string OldValue, string DefaultValue)
        {
            if (NewValue == string.Empty)
                return DefaultValue;
            else
            {
                string SortExpression = HtmlHelper.EscapeName(NewValue);
                if (OldValue == SortExpression)
                    return SortExpression + " DESC";
                else
                    return SortExpression;
            }
        }

        public static string addLikeFilter(string fieldName, string fieldFormat, TextBox likeTextBox)
        {
            string fieldValue = likeTextBox.Text.Trim();

            if (fieldValue.Length != 0)
            {
                if (fieldFormat != null)
                    fieldValue = string.Format(fieldFormat, fieldValue);
                return string.Format(" AND {0} LIKE {1}", HtmlHelper.EscapeName(fieldName), HtmlHelper.EscapeQuoteUnicode(fieldValue));
            }
            else
                return string.Empty;
        }

        public static string addContainFilter(string fieldName, TextBox fieldTextBox)
        {
            string fieldValue = fieldTextBox.Text.Trim().ToLower();
            if (fieldValue.Length != 0)
            {
                if (fieldValue.IndexOf(" ") != -1 &&
                    fieldValue.IndexOf("\"") == -1 &&
                    fieldValue.IndexOf(" and ") == -1 &&
                    fieldValue.IndexOf(" or ") == -1 &&
                    fieldValue.IndexOf(" near ") == -1)
                    fieldValue = "\"" + fieldValue + "\"";
                return string.Format(" AND CONTAINS({0},{1})", fieldName, HtmlHelper.EscapeQuoteUnicode(fieldValue));
            }
            else
                return string.Empty;
        }

        public static string addEqualFilter(string fieldName, TextBox fieldTextBox, bool isNumber)
        {
            string fieldValue = fieldTextBox.Text.Trim();
            if (fieldValue.Length != 0)
            {
                if (isNumber)
                    fieldValue = HtmlHelper.ParseInt(fieldValue);
                else
                    fieldValue = HtmlHelper.EscapeQuoteUnicode(fieldValue);
                return string.Format(" AND {0}={1}", HtmlHelper.EscapeName(fieldName), fieldValue);
            }
            else
                return string.Empty;
        }

        public static string addLookUpFilter(string fieldName, ListControl fieldListBox, bool skipFirstItem)
        {
            int fieldIndex = fieldListBox.SelectedIndex;
            if (fieldIndex != -1 && (fieldIndex != 0 || !skipFirstItem))
                return string.Format(" AND {0}={1}", HtmlHelper.EscapeName(fieldName), int.Parse(fieldListBox.SelectedValue));
            else
                return string.Empty;
        }

        public static string addDateFilter(string fieldName, TextBox fromTextBox, TextBox toTextBox)
        {
            if (fieldName == string.Empty)
                return string.Empty;
            string filterExpression = string.Empty;
            try
            {
                DateTime fromDate = DateTime.Parse(fromTextBox.Text).Date;
                filterExpression += string.Format(" AND {0}>={1}", HtmlHelper.EscapeName(fieldName), HtmlHelper.EscapeQuote(fromDate));
            }
            catch { }
            try
            {
                DateTime toDate = DateTime.Parse(toTextBox.Text).Date;
                filterExpression += string.Format(" AND {0}<{1}", HtmlHelper.EscapeName(fieldName), HtmlHelper.EscapeQuote(toDate.AddDays(1)));
            }
            catch { }
            return filterExpression;
        }

        public static string addIntFilter(string fieldName, TextBox fromTextBox, TextBox toTextBox)
        {
            if (fieldName == string.Empty)
                return string.Empty;
            string filterExpression = string.Empty;
            try
            {
                int fromInt = int.Parse(fromTextBox.Text);
                filterExpression += string.Format(" AND {0}>={1}", HtmlHelper.EscapeName(fieldName), fromInt);
            }
            catch { }
            try
            {
                int toInt = int.Parse(toTextBox.Text);
                filterExpression += string.Format(" AND {0}<={1}", HtmlHelper.EscapeName(fieldName), toInt);
            }
            catch { }
            return filterExpression;
        }

        public static string getFilter(string filterExpression)
        {
            if (filterExpression.Length == 0)
                return null;
            else
                return filterExpression.Substring(5);
        }

        public static string[] getCheckedItemList(GridView grid, string FieldName)
        {
            List<String> scChecked = new List<String>();
            foreach (GridViewRow row in grid.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    GridviewHelper helperGrid = new GridviewHelper(row);
                    if (helperGrid.findCheckBox("chk" + FieldName).Checked)
                    {
                        scChecked.Add(grid.DataKeys[row.RowIndex].Value.ToString());
                    }
                }
            }

            return scChecked.ToArray();
        }

    }
}
