/*
 * 程序名称: OAuth2Client
 * 
 * 程序版本: 1.x
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */

using System;
using System.Data;
using System.Web;
using System.Text;
using System.Collections.Generic;
namespace OAuth2Client.Common.Utils
{
    /// <summary>
    /// DataTable数据转换类
    /// </summary>
    public static class dtHelper
    {
        /// <summary>
        /// 将dt转化成Json数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DT2JSON(DataTable dt)
        {
            return DT2JSON(dt, 0, "recordcount", "table");
        }
        public static string DT2JSON(bool OutUnicode, DataTable dt)
        {
            return DT2JSON(OutUnicode, dt, 0, "recordcount", "table");
        }
        public static string DT2JSON(DataTable dt, int fromCount)
        {
            return DT2JSON(dt, fromCount, "recordcount", "table");
        }
        public static string DT2JSON(bool OutUnicode, DataTable dt, int fromCount)
        {
            return DT2JSON(OutUnicode, dt, fromCount, "recordcount", "table");
        }
        /// <summary>
        /// 将dt转化成Json数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fromCount"></param>
        /// <param name="totalCountStr"></param>
        /// <param name="tbname"></param>
        /// <returns></returns>
        public static string DT2JSON(DataTable dt, int fromCount, string totalCountStr, string tbname)
        {
            return DT2JSON(dt, fromCount, "recordcount", "table", true);
        }
        public static string DT2JSON(bool OutUnicode, DataTable dt, int fromCount, string totalCountStr, string tbname)
        {
            return DT2JSON(OutUnicode, dt, fromCount, "recordcount", "table", true);
        }
        /// <summary>
        /// 将dt转化成Json数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fromCount"></param>
        /// <param name="totalCountStr"></param>
        /// <param name="tbname"></param>
        /// <returns></returns>
        /// 

        public static string DT2JSON(DataTable dt, int fromCount, string totalCountStr, string tbname, bool formatData)
        {
            return DT2JSON(false, dt, fromCount, totalCountStr, tbname, formatData);
        }
        public static string DT2JSON(bool OutUnicode, DataTable dt, int fromCount, string totalCountStr, string tbname, bool formatData)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("\"" + totalCountStr + "\":" + dt.Rows.Count + ",\"" + tbname + "\": [");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                    jsonBuilder.Append(",");
                jsonBuilder.Append("{");
                jsonBuilder.Append("\"no\":" + (fromCount + i + 1) + ",");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j > 0)
                        jsonBuilder.Append(",");
                    if (dt.Columns[j].DataType.Equals(typeof(DateTime)) && dt.Rows[i][j].ToString() != "")
                        jsonBuilder.Append("\"" + dt.Columns[j].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
                    else if (dt.Columns[j].DataType.Equals(typeof(String)))
                    {
                        if (!OutUnicode)
                            jsonBuilder.Append("\"" + dt.Columns[j].ColumnName.ToLower() + "\": \"" + dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
                        else
                            jsonBuilder.Append("\"" + dt.Columns[j].ColumnName.ToLower() + "\": \"" + Utils.Strings.String2Unicode(dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>")) + "\"");
                    }

                    else
                        jsonBuilder.Append("\"" + dt.Columns[j].ColumnName.ToLower() + "\": \"" + dt.Rows[i][j].ToString() + "\"");
                }
                jsonBuilder.Append("}");
            }
            jsonBuilder.Append("]");
                return Utils.Strings.DeleteUnVisibleChar(jsonBuilder.ToString());
            

        }
        /// <summary>
        /// 将DataTable转换为list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DT2List<T>(DataTable dt)
        {
            if (dt == null)
                return null;
            List<T> result = new List<T>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                System.Reflection.PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值 
                        if (pi.Name.ToLower().Equals(dt.Columns[i].ColumnName.ToLower()))
                        {
                            if (dt.Rows[j][i] != DBNull.Value)
                            {
                                if (pi.PropertyType.ToString() == "System.Int32")
                                {
                                    pi.SetValue(_t, Int32.Parse(dt.Rows[j][i].ToString()), null);
                                }
                                else if (pi.PropertyType.ToString() == "System.DateTime")
                                {
                                    pi.SetValue(_t, Convert.ToDateTime(dt.Rows[j][i].ToString()), null);
                                }
                                else if (pi.PropertyType.ToString() == "System.Boolean")
                                {
                                    pi.SetValue(_t, Convert.ToBoolean(dt.Rows[j][i].ToString()), null);
                                }
                                else if (pi.PropertyType.ToString() == "System.Single")
                                {
                                    pi.SetValue(_t, Convert.ToSingle(dt.Rows[j][i].ToString()), null);
                                }
                                else if (pi.PropertyType.ToString() == "System.Double")
                                {
                                    pi.SetValue(_t, Convert.ToDouble(dt.Rows[j][i].ToString()), null);
                                }
                                else
                                {
                                    pi.SetValue(_t, dt.Rows[j][i].ToString(), null);
                                }
                            }
                            else
                                pi.SetValue(_t, "", null);//为空，但不为Null
                            break;
                        }
                    }
                }
                result.Add(_t);
            }
            return result;
        }
    }
}
