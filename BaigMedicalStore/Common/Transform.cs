using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Xml.Serialization;

namespace BaigMedicalStore.Common
{
    /// <summary>
    /// This class will transform JSON to DTO and from DTO to Business Object and vice versa.
    /// </summary>
    public static class Transform
    {
        /// <summary>
        /// Transforms json string into provided single business object.
        /// </summary>
        /// <typeparam name="T">Type of business object that is required.</typeparam>
        /// <typeparam name="J">Type of DTO that is compatible with provided json.</typeparam>
        /// <param name="jsonData">JSON string.</param>
        /// <returns>Business object.</returns>
        public static T FromJSONToBO<T, J>(string jsonData)
        {
            try
            {
                J dtoEntity = Newtonsoft.Json.JsonConvert.DeserializeObject<J>(jsonData);
                return FromObjectToObject<T, J>(dtoEntity);
            }
            catch (Exception)
            {
                return default(T);
            }
            finally
            {
            }
        }

        /// <summary>
        /// Transforms json string into provided object type.
        /// </summary>
        /// <typeparam name="T">Type of object in which to convert.</typeparam>
        /// <param name="jsonData">Json string to be converted.</param>
        /// <returns>Object of provided type.</returns>
        public static T FromJSONToObject<T>(string jsonData)
        {

            try
            {
                T dtoEntity = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonData);
                return dtoEntity;
            }
            catch (Exception)
            {
                return default(T);
            }

        }
        /// <summary>
        /// This function serialize xml string to custom object.
        /// </summary>
        /// <param name="XMLString"></param>
        /// <param name="objCustom"></param>
        /// <returns></returns>
        public static T FromXMLToBO<T>(string XMLString)
        {
            try
            {
                XmlSerializer oXmlSerializer = new XmlSerializer(typeof(T));
                T result = (T)oXmlSerializer.Deserialize(new StringReader(XMLString));
                return result;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// This function serialize an object to xml string.
        /// </summary>
        /// <param name="XMLString"></param>
        /// <param name="objCustom"></param>
        /// <returns></returns>
        public static string FromBOToXML<T>(T sourceObject)
        {
            try
            {
                StringWriter stringWriter = new StringWriter();
                XmlSerializer oXmlSerializer = new XmlSerializer(typeof(T));
                oXmlSerializer.Serialize(stringWriter, sourceObject);
                return stringWriter.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Transforms single business object into json string.
        /// </summary>
        /// <typeparam name="T">Type of business object from where to read data.</typeparam>
        /// <typeparam name="J">Type of DTO in which the business object will be converted.</typeparam>
        /// <param name="sourceBO">Source business object that contains the data.</param>
        /// <returns>JSON string.</returns>
        public static string FromBOToJSON<T, J>(T sourceBO)
        {
            try
            {
                J dtoEntity = FromObjectToObject<J, T>(sourceBO);
                return Newtonsoft.Json.JsonConvert.SerializeObject(dtoEntity);
            }
            catch (Exception)
            {
                return string.Empty;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Transforms list of business objects into json string.
        /// </summary>
        /// <typeparam name="T">List type of business object from where to read data.</typeparam>
        /// <typeparam name="J">Type of DTO in which each business object will be converted.</typeparam>
        /// <param name="sourceBO">List of source business objects that contains the data.</param>
        /// <returns>JSON string of business objects collection.</returns>
        public static string FromBOToJSON<T, J>(IList<T> sourceBO)
        {
            List<J> dtoList = new List<J>();
            try
            {
                foreach (T bo in sourceBO)
                {
                    dtoList.Add(FromObjectToObject<J, T>(bo));
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(dtoList);
            }
            catch (Exception)
            {
                return string.Empty;
            }
            finally
            {
            }


        }

        /// <summary>
        /// Generic method to copy data from source object to destination object by matching field names.
        /// </summary>
        /// <typeparam name="T">Type of object that is required in return.</typeparam>
        /// <typeparam name="J">Type of source object from where to read data.</typeparam>
        /// <param name="sourceObject">Source object to read data from.</param>
        /// <returns>Generic type.</returns>
        public static T FromObjectToObject<T, J>(J sourceObject)
        {
            T destinationBO = Activator.CreateInstance<T>();
            PropertyInfo[] dtoProperties = sourceObject.GetType().GetProperties();
            PropertyInfo[] boProperties = destinationBO.GetType().GetProperties();

            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                foreach (PropertyInfo boProperty in boProperties)
                {
                    if (boProperty.Name.Equals(dtoProperty.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        boProperty.SetValue(destinationBO, dtoProperty.GetValue(sourceObject, null), null);
                        break;
                    }
                }
            }
            return destinationBO;
        }
        /// <summary>
        /// Generic method to copy data from source object to destination object by matching field names.
        /// </summary>
        /// <typeparam name="T">Type of object that is required in return.</typeparam>
        /// <typeparam name="J">Type of source object from where to read data.</typeparam>
        /// <param name="sourceObject">Source object to read data from.</param>
        /// <returns>Generic type.</returns>
        public static T FromObjectToObject<T, J>(J sourceObject, T destinationBO)
        {
            PropertyInfo[] dtoProperties = sourceObject.GetType().GetProperties();
            PropertyInfo[] boProperties = destinationBO.GetType().GetProperties();

            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                foreach (PropertyInfo boProperty in boProperties)
                {
                    if (boProperty.Name.Equals(dtoProperty.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        boProperty.SetValue(destinationBO, dtoProperty.GetValue(sourceObject, null), null);
                        break;
                    }
                }
            }
            return destinationBO;
        }

        /// <summary>
        /// Generic method to transform one type of list into other type of list.
        /// </summary>
        /// <typeparam name="T">Target type to be converted.</typeparam>
        /// <typeparam name="J">Source type from which to convert.</typeparam>
        /// <param name="sourceObjects">List of source objects.</param>
        /// <returns>List of Target type.</returns>
        public static IList<T> FromObjectToObject<T, J>(IList<J> sourceObjects)
        {
            List<T> targetList = new List<T>();
            try
            {
                foreach (J sourceObject in sourceObjects)
                {
                    targetList.Add(FromObjectToObject<T, J>(sourceObject));
                }
                return targetList;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                targetList = null;
            }
        }

        /// <summary>
        /// This method converts the passed object into JSON format.
        /// </summary>
        /// <param name="source">Target object to convert into JSON string.</param>
        /// <returns>JSON string.</returns>
        public static string FromObjectToJSON(object source)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(source);
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// Used to get field from collection of properties
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="parameterName"></param>
        /// <returns>Specific property</returns>
        private static PropertyInfo GetField(PropertyInfo[] properties, string parameterName)
        {
            PropertyInfo pinfo = null;
            foreach (PropertyInfo property in properties)
            {
                if (property.Name.Equals(parameterName, StringComparison.OrdinalIgnoreCase))
                {
                    pinfo = property;
                    break;
                }
            }
            return pinfo;
        }

        /// <summary>
        /// Load the list of objects from data table
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">specific object</param>
        /// <param name="dt">data table having values to be loaded</param>
        /// <returns>List of objects</returns>
        public static List<T> LoadListObject<T>(object obj, DataTable dt)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            PropertyInfo member;
            List<object> objList = new List<object>();

            List<T> tList = new List<T>();

            for (int i = 0; i < dt.Rows.Count; ++i)
            {

                obj = Activator.CreateInstance(obj.GetType());

                for (int j = 0; j < dt.Columns.Count; ++j)
                {

                    member = GetField(properties, dt.Columns[j].ColumnName);
                    if (member != null)
                    {
                        if (member.PropertyType.UnderlyingSystemType.FullName.Contains("Nullable")
                            && member.PropertyType.UnderlyingSystemType.FullName.Contains("Guid"))
                        {
                            if (dt.Rows[i][j].ToString().Length > 0)
                                member.SetValue(obj, new Guid(dt.Rows[i][j].ToString()), null);
                        }
                        else if (member.PropertyType.UnderlyingSystemType.FullName.Contains("Nullable")
                            && member.PropertyType.UnderlyingSystemType.FullName.Contains("Boolean"))
                        {
                            member.SetValue(obj, bool.Parse(dt.Rows[i][j].ToString()), null);
                        }
                        else if (member.PropertyType.UnderlyingSystemType.FullName.Contains("Nullable")
                        && member.PropertyType.UnderlyingSystemType.FullName.Contains("Int")
                        && dt.Rows[i][j].ToString().Length > 0)
                        {
                            member.SetValue(obj, int.Parse(dt.Rows[i][j].ToString()), null);
                        }
                        else if (member.PropertyType.UnderlyingSystemType.FullName.Contains("Nullable")
                        && member.PropertyType.UnderlyingSystemType.FullName.Contains("DateTime")
                        && dt.Rows[i][j].ToString().Length > 0)
                        {
                            member.SetValue(obj, DateTime.Parse(dt.Rows[i][j].ToString()), null);
                        }
                        else if (member.PropertyType.UnderlyingSystemType.FullName.Contains("Byte[]"))
                        {
                            if (dt.Rows[i][j] != null && dt.Rows[i][j].ToString().Length > 0)
                            {
                                member.SetValue(obj, dt.Rows[i][j], null);
                            }
                        }
                        else if (member.PropertyType.UnderlyingSystemType.Name.Equals("Guid"))
                            member.SetValue(obj, new Guid(dt.Rows[i][j].ToString()), null);
                        else if (dt.Rows[i][j] != null && dt.Rows[i][j].ToString().Length > 0)
                            member.SetValue(obj, Convert.ChangeType(dt.Rows[i][j].ToString(), Type.GetType(member.PropertyType.FullName)), null);
                    }
                }
                tList.Add((T)obj);
            }
            return tList;

        }

        /// <summary>
        /// Creates a deep copy of a reference type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T GetDeepCopy<T>(T source)
        {
            string ObjectJSON = Transform.FromObjectToJSON(source);
            T objectDeepCopy = Transform.FromJSONToObject<T>(ObjectJSON);

            return objectDeepCopy;
        }
    }
}