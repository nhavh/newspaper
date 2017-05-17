using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualBasic;
using TMV.Caching;
using System.ComponentModel;

namespace TMV.Utilities
{
    public static class CBO
    {       
        private static object CreateObject(Type objType, IDataReader dr, ArrayList objProperties, int[] arrOrdinals)
        {
            Type objPropertyType = null;

            object objObject = Activator.CreateInstance(objType, null);

            // fill object with values from datareader
            for (int intProperty = 0; intProperty <= objProperties.Count - 1; intProperty++)
            {
                PropertyInfo objPropertyInfo = (PropertyInfo)objProperties[intProperty];
                if (objPropertyInfo.CanWrite)
                {
                    object objValue = Null.SetNull(objPropertyInfo);
                    if (arrOrdinals[intProperty] != -1)
                    {
                        if (Information.IsDBNull(dr.GetValue(arrOrdinals[intProperty])))
                        {
                            // translate Null value
                            objPropertyInfo.SetValue(objObject, objValue, null);
                        }
                        else
                        {
                            try
                            {
                                // try implicit conversion first
                                objPropertyInfo.SetValue(objObject, dr.GetValue(arrOrdinals[intProperty]), null);
                            }
                            catch
                            {
                                // business object info class member data type does not match datareader member data type
                                try
                                {
                                    objPropertyType = objPropertyInfo.PropertyType;
                                    //need to handle enumeration conversions differently than other base types
                                    if (objPropertyType.BaseType.Equals(typeof(Enum)))
                                    {
                                        // check if value is numeric and if not convert to integer ( supports databases like Oracle )
                                        if (Information.IsNumeric(dr.GetValue(arrOrdinals[intProperty])))
                                        {
                                            ((PropertyInfo)objProperties[intProperty]).SetValue(objObject, Enum.ToObject(objPropertyType, Convert.ToInt32(dr.GetValue(arrOrdinals[intProperty]))), null);
                                        }
                                        else
                                        {
                                            ((PropertyInfo)objProperties[intProperty]).SetValue(objObject, Enum.ToObject(objPropertyType, dr.GetValue(arrOrdinals[intProperty])), null);
                                        }
                                    }
                                    else if (objPropertyType.FullName.Equals("System.Guid"))
                                    {
                                        // guid is not a datatype common across all databases ( ie. Oracle )
                                        objPropertyInfo.SetValue(objObject, Convert.ChangeType(new Guid(dr.GetValue(arrOrdinals[intProperty]).ToString()), objPropertyType), null);
                                    }
                                    else
                                    {
                                        // try explicit conversion
                                        objPropertyInfo.SetValue(objObject, Convert.ChangeType(dr.GetValue(arrOrdinals[intProperty]), objPropertyType), null);
                                    }
                                }
                                catch
                                {
                                    objPropertyInfo.SetValue(objObject, Convert.ChangeType(dr.GetValue(arrOrdinals[intProperty]), objPropertyType), null);
                                }
                            }
                        }
                    }
                    else
                    {
                        // property does not exist in datareader
                    }
                }
            }

            return objObject;
        }

        /// <summary>
        /// Generic version of CreateObject creates an object of a specified type from the
        /// provided DataReader
        /// </summary>
        /// <typeparam name="T">The type of the business object</typeparam>
        /// <param name="dr">The DataReader</param>
        /// <returns>The custom business object</returns>
        /// <remarks></remarks>
        private static T CreateObject<T>(IDataReader dr)
        {
            Type objPropertyType = null;

            T objObject = Activator.CreateInstance<T>();

            // get properties for type
            ArrayList objProperties = GetPropertyInfo(objObject.GetType());

            // get ordinal positions in datareader
            int[] arrOrdinals = GetOrdinals(objProperties, dr);

            // fill object with values from datareader
            for (int intProperty = 0; intProperty <= objProperties.Count - 1; intProperty++)
            {
                PropertyInfo objPropertyInfo = (PropertyInfo)objProperties[intProperty];
                if (objPropertyInfo.CanWrite)
                {
                    object objValue = Null.SetNull(objPropertyInfo);
                    if (arrOrdinals[intProperty] != -1)
                    {
                        if (Information.IsDBNull(dr.GetValue(arrOrdinals[intProperty])))
                        {
                            // translate Null value
                            objPropertyInfo.SetValue(objObject, objValue, null);
                        }
                        else
                        {
                            try
                            {
                                // try implicit conversion first
                                objPropertyInfo.SetValue(objObject, dr.GetValue(arrOrdinals[intProperty]), null);
                            }
                            catch
                            {
                                // business object info class member data type does not match datareader member data type
                                try
                                {
                                    objPropertyType = objPropertyInfo.PropertyType;
                                    //need to handle enumeration conversions differently than other base types
                                    if (objPropertyType.BaseType.Equals(typeof(Enum)))
                                    {
                                        // check if value is numeric and if not convert to integer ( supports databases like Oracle )
                                        if (Information.IsNumeric(dr.GetValue(arrOrdinals[intProperty])))
                                        {
                                            ((PropertyInfo)objProperties[intProperty]).SetValue(objObject, Enum.ToObject(objPropertyType, Convert.ToInt32(dr.GetValue(arrOrdinals[intProperty]))), null);
                                        }
                                        else
                                        {
                                            ((PropertyInfo)objProperties[intProperty]).SetValue(objObject, Enum.ToObject(objPropertyType, dr.GetValue(arrOrdinals[intProperty])), null);
                                        }
                                    }
                                    else
                                    {
                                        // try explicit conversion
                                        objPropertyInfo.SetValue(objObject, Convert.ChangeType(dr.GetValue(arrOrdinals[intProperty]), objPropertyType), null);
                                    }
                                }
                                catch
                                {
                                    objPropertyInfo.SetValue(objObject, Convert.ChangeType(dr.GetValue(arrOrdinals[intProperty]), objPropertyType), null);
                                }
                            }
                        }
                    }
                    else
                    {
                        // property does not exist in datareader
                    }
                }
            }

            return objObject;
        }

        public static ArrayList FillCollection(IDataReader dr, Type objType)
        {
            ArrayList objFillCollection = new ArrayList();

            // get properties for type
            ArrayList objProperties = GetPropertyInfo(objType);

            // get ordinal positions in datareader
            int[] arrOrdinals = GetOrdinals(objProperties, dr);

            if (dr != null)
            {
                // iterate datareader
                while (dr.Read())
                {
                    // fill business object
                    object objFillObject = CreateObject(objType, dr, objProperties, arrOrdinals);
                    // add to collection
                    objFillCollection.Add(objFillObject);
                }

                // close datareader
                dr.Close();
            }

            return objFillCollection;
        }

        public static List<T> FillCollection<T>(IDataReader dr, bool manageDataReader)
        {
            List<T> objFillCollection = new List<T>();            

            if (dr != null)
            {
                // iterate datareader
                while (dr.Read())
                {
                    // // fill business object
                    T objFillObject = CreateObject<T>(dr);
                    // add to collection
                    objFillCollection.Add(objFillObject);
                }

                // close datareader
                if(manageDataReader)
                    dr.Close();
            }

            return objFillCollection;
        }       

        /// <summary>
        /// Generic version of FillCollection fills a List custom business object of a specified type
        /// from the supplied DataReader
        /// </summary>
        /// <typeparam name="T">The type of the business object</typeparam>
        /// <param name="dr">The IDataReader to use to fill the object</param>
        /// <returns>A List of custom business objects</returns>
        /// <remarks></remarks>
        public static List<T> FillCollection<T>(IDataReader dr)
        {
            List<T> objFillCollection = new List<T>();

            if (dr != null)
            {
                // iterate datareader
                while (dr.Read())
                {
                    // fill business object
                    T objFillObject = CreateObject<T>(dr);
                    // add to collection
                    objFillCollection.Add(objFillObject);
                }

                // close datareader
                dr.Close();
            }

            return objFillCollection;
        }

        /// <summary>
        /// Generic version of FillCollection fills a provided IList with custom business
        /// objects of a specified type from the supplied DataReader
        /// </summary>
        /// <typeparam name="T">The type of the business object</typeparam>
        /// <param name="dr">The IDataReader to use to fill the object</param>
        /// <param name="objToFill">The IList to fill</param>
        /// <returns>An IList of custom business objects</returns>
        /// <remarks></remarks>
        public static IList<T> FillCollection<T>(IDataReader dr, ref IList<T> objToFill)
        {
            if (dr != null)
            {
                // iterate datareader
                while (dr.Read())
                {
                    // fill business object
                    T objFillObject = CreateObject<T>(dr);
                    // add to collection
                    objToFill.Add(objFillObject);
                }

                // close datarader
                dr.Close();
            }

            return objToFill;
        }

        public static object FillObject(IDataReader dr, Type objType)
        {
            return FillObject(dr, objType, true);
        }

        public static object FillObject(IDataReader dr, Type objType, bool manageDataReader)
        {
            object objFillObject;

            // get properties for type
            ArrayList objProperties = GetPropertyInfo(objType);

            // get ordinal positions in datareader
            int[] arrOrdinals = GetOrdinals(objProperties, dr);

            bool isContinue;
            if (manageDataReader)
            {
                isContinue = false;
                // read datareader
                if (dr != null && dr.Read())
                {
                    isContinue = true;
                }
            }
            else
            {
                isContinue = true;
            }

            if (isContinue)
            {
                // create custom business object
                objFillObject = CreateObject(objType, dr, objProperties, arrOrdinals);
            }
            else
            {
                objFillObject = null;
            }

            if (manageDataReader)
            {
                // close datareader
                if (dr != null)
                {
                    dr.Close();
                }
            }

            return objFillObject;
        }

        /// <summary>
        /// Generic version of FillObject fills a custom business object of a specified type
        /// from the supplied DataReader
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="dr">The IDataReader to use to fill the object</param>
        /// <returns>The object</returns>
        /// <remarks>This overloads sets the ManageDataReader parameter to true and calls
        /// the other overload</remarks>
        public static T FillObject<T>(IDataReader dr)
        {
            return FillObject<T>(dr, true);
        }

        /// <summary>
        /// Generic version of FillObject fills a custom business object of a specified type
        /// from the supplied DataReader
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="dr">The IDataReader to use to fill the object</param>
        /// <param name="manageDataReader">A boolean that determines whether the DatReader
        /// is managed</param>
        /// <returns>The object</returns>
        /// <remarks>This overloads allows the caller to determine whether the ManageDataReader
        /// parameter is set</remarks>
        public static T FillObject<T>(IDataReader dr, bool manageDataReader)
        {
            T objFillObject;

            bool doContinue;
            if (manageDataReader)
            {
                doContinue = false;
                // read datareader
                if (dr != null && dr.Read())
                {
                    doContinue = true;
                }
            }
            else
            {
                doContinue = true;
            }

            if (doContinue)
            {
                // create custom business object
                objFillObject = CreateObject<T>(dr);
            }
            else
            {
                objFillObject = default(T);
            }

            if (manageDataReader)
            {
                // close datareader
                if (dr != null)
                {
                    dr.Close();
                }
            }

            return objFillObject;
        }

        private static int[] GetOrdinals(ArrayList objProperties, IDataReader dr)
        {
            int[] arrOrdinals = new int[objProperties.Count + 1];
            Hashtable columns = new Hashtable();
            string propertyName = string.Empty;

            if (dr != null)
            {
                for (int intIndex = 0; intIndex < dr.FieldCount; intIndex++)
                    columns.Add(dr.GetName(intIndex).ToUpperInvariant(), "");     

                for (int intProperty = 0; intProperty < objProperties.Count; intProperty++)
                {
                    arrOrdinals[intProperty] = -1;
                    propertyName = ((PropertyInfo)objProperties[intProperty]).Name.ToUpperInvariant();
                    if (columns.ContainsKey(propertyName))
                        arrOrdinals[intProperty] = dr.GetOrdinal(propertyName);                    
                }
            }

            return arrOrdinals;
        }

        public static ArrayList GetPropertyInfo(Type objType)
        {
            // Use the cache because the reflection used later is expensive
            ArrayList objProperties = (ArrayList)DataCache.GetCache(objType.FullName);

            if (objProperties == null)
            {
                objProperties = new ArrayList();
                foreach (PropertyInfo objProperty in objType.GetProperties())
                {
                    objProperties.Add(objProperty);
                }
                DataCache.SetCache(objType.FullName, objProperties);
            }

            return objProperties;
        }

        public static PropertyInfo GetPropertyInfo(Type objType, string propertyName)
        {
            foreach (PropertyInfo objProperty in GetPropertyInfo(objType))
            {
                if (objProperty.Name == propertyName)
                    return objProperty;
            }
            return null;
        }

        public static object InitializeObject(object objObject, Type objType)
        {
            // get properties for type
            ArrayList objProperties = GetPropertyInfo(objType);

            // initialize properties
            for (int intProperty = 0; intProperty < objProperties.Count; intProperty++)
            {
                PropertyInfo objPropertyInfo = (PropertyInfo)objProperties[intProperty];
                if (objPropertyInfo.CanWrite)
                {
                    object objValue = Null.SetNull(objPropertyInfo);
                    objPropertyInfo.SetValue(objObject, objValue, null);
                }
            }

            return objObject;
        }

        public static XmlDocument Serialize(object objObject)
        {
            XmlSerializer objXmlSerializer = new XmlSerializer(objObject.GetType());

            StringBuilder objStringBuilder = new StringBuilder();

            TextWriter objTextWriter = new StringWriter(objStringBuilder);

            objXmlSerializer.Serialize(objTextWriter, objObject);

            StringReader objStringReader = new StringReader(objTextWriter.ToString());

            DataSet objDataSet = new DataSet();

            objDataSet.ReadXml(objStringReader);

            XmlDocument xmlSerializedObject = new XmlDocument();

            xmlSerializedObject.LoadXml(objDataSet.GetXml());

            return xmlSerializedObject;
        }
        
        public static DataTable ConvertToDataTable(IList list, Type type)
        {
            try
            {
                IEnumerator enumerator = null;
                DataTable table = new DataTable();
                ArrayList properties = GetPropertyInfo(type);
                foreach (PropertyInfo info in properties)
                {
                    if(info.CanWrite)
                        table.Columns.Add(new DataColumn(info.Name, info.PropertyType));

                }
                try
                {
                    enumerator = list.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        object objectValue = enumerator.Current;
                        object[] values = new object[table.Columns.Count];
                        int index = 0;                        
                        foreach (PropertyInfo info in properties)
                        {
                            if (info.CanWrite)
                            {
                                values[index] = info.GetValue(objectValue, null);
                                index++;
                            }
                        }
                        table.Rows.Add(values);
                    }
                }
                finally
                {
                    if (enumerator is IDisposable)
                    {
                        (enumerator as IDisposable).Dispose();
                    }
                }
                return table;
            }
            catch
            {
            }
            return null;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
