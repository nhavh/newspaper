using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data.SqlTypes;
using System.Data;
using System.IO;
using System.Xml;
using TMV.Utilities;

namespace TMV.WebControls
{
    public class IdNameHelper
    {
        const int MaxRowCount = 4096;
        protected int _RowCount = 0;
        protected object[] _IDCol = new object[MaxRowCount];
        protected object[] _NameCol = new object[MaxRowCount];
        static string _fieldTypeKey = string.Empty;
        protected bool addRow(object rowID, object rowName)
        {
            if (_RowCount >= MaxRowCount)
                return false;

            _IDCol[_RowCount] = rowID;
            _NameCol[_RowCount] = rowName;
            _RowCount++;

            return true;
        }

        public IdNameHelper()
        {

        }

        public IdNameHelper(string fieldLookup, object nullName, string FieldTypeKey, string ControllerInfo)
        {
            _fieldTypeKey = FieldTypeKey.ToLower();

            if (nullName != null)
                addRow(Null.GetNullOfTypeKey(_fieldTypeKey), nullName);

            string tableFullName = fieldLookup.ToLower();
            string idFieldName = "ID";
            string nameFieldName = "Name";

            if (tableFullName.EndsWith(".xml"))
            {
                if (tableFullName.IndexOf(Path.PathSeparator) == -1)
                    tableFullName = System.Web.HttpContext.Current.Server.MapPath(Globals.PathXML) + tableFullName;

                fieldLookup = Path.GetFileNameWithoutExtension(fieldLookup);

                XmlTextReader xr = new XmlTextReader(tableFullName);
                try
                {
                    while (xr.Read())
                    {
                        if (xr.Depth != 1 || xr.NodeType != XmlNodeType.Element)
                            continue;

                        xr.ReadStartElement(fieldLookup);
                        addRow(xr.ReadElementString(idFieldName), xr.ReadElementString(nameFieldName));
                    }
                }
                finally
                {
                    xr.Close();
                }
            }
            else
            {                                             
                Dictionary<object, object> dicList = GetMethodFromController(fieldLookup, null, ControllerInfo);
                
                foreach (KeyValuePair<object, object> info in dicList)
                    addRow(info.Key, info.Value);
            }
        }

        Dictionary<object, object> GetMethodFromController(string name, object[] args, string controllerinfo)
        {
            Assembly assembly = Assembly.Load("TMV.Data");
            System.Type objType = assembly.GetType(controllerinfo);
            object objObject = Activator.CreateInstance(objType);
            Dictionary<object, object> result = (Dictionary<object, object>)objType.InvokeMember(name, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, objObject, args);

            return result;
        }

        public IdNameHelper(DataTable dt, string nullName)
        {
            if (nullName != null)
                addRow(int.MinValue, nullName);

            foreach (DataRow row in dt.Rows)
                addRow((int)row[0], (string)row[1]);
        }

        public IdNameHelper(Dictionary<int, string> htLookUp, string nullName)
        {

            if (nullName != null)
                addRow(Null.NullInteger, nullName);

            foreach (KeyValuePair<int, string> entry in htLookUp)
            {
                addRow(entry.Key, entry.Value);
            }
        }

        public object GetName(object identity)
        {
            object id = Null.GetNullOfTypeKey(_fieldTypeKey);

            if (identity != null && identity != Null.GetNullOfTypeKey(_fieldTypeKey))
                id = identity;


            for (int i = 0; i < _RowCount; i++)
                if (id.ToString().Equals(_IDCol[i].ToString()))
                    return _NameCol[i];

            return "?";
        }        

        public void PopulateList(System.Web.UI.WebControls.ListControl list)
        {
            for (int i = 0; i < _RowCount; i++)
                list.Items.Add(new System.Web.UI.WebControls.ListItem(_NameCol[i].ToString(), _IDCol[i].ToString()));
        }        
    }
}
