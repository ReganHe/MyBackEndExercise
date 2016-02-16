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
    /// 范类型操作类
    /// </summary>
    public static class dicHelper
    {
        /// <summary>
        /// 排序(如果你已经升到NET3.5，就别这么傻了)
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="type">0表示按键值正序，1表示按键值倒序，2表示按键名正序，3表示按键名倒序</param>
        public static void Order(ref Dictionary<string, string> dic, int type)
        {
            if (dic == null) return;
            if (dic.Count < 1) return;
            List<KeyValuePair<string, string>> myList = new List<KeyValuePair<string, string>>(dic);
            switch (type)
            {
                case 0:
                    myList.Sort(delegate(KeyValuePair<string, string> s1, KeyValuePair<string, string> s2)
                    {
                        return s1.Value.CompareTo(s2.Value);
                    });
                    break;
                case 1:
                    myList.Sort(delegate(KeyValuePair<string, string> s1, KeyValuePair<string, string> s2)
                    {
                        return s2.Value.CompareTo(s1.Value);
                    });
                    break;
                case 2:
                    myList.Sort(delegate(KeyValuePair<string, string> s1, KeyValuePair<string, string> s2)
                    {
                        return s1.Key.CompareTo(s2.Key);
                    });
                    break;
                default:
                    myList.Sort(delegate(KeyValuePair<string, string> s1, KeyValuePair<string, string> s2)
                    {
                        return s2.Key.CompareTo(s1.Key);
                    });
                    break;
            }
            dic.Clear();
            foreach (KeyValuePair<string, string> pair in myList)
            {
                dic.Add(pair.Key, pair.Value);
            }
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="type">0表示按键值正序，1表示按键值倒序，2表示按键名正序，3表示按键名倒序</param>
        public static void Order(ref Dictionary<string, int> dic, int type)
        {
            if (dic == null) return;
            if (dic.Count < 1) return;
            List<KeyValuePair<string, int>> myList = new List<KeyValuePair<string, int>>(dic);
            switch (type)
            {
                case 0:
                    myList.Sort(delegate(KeyValuePair<string, int> s1, KeyValuePair<string, int> s2)
                    {
                        return s1.Value.CompareTo(s2.Value);
                    });
                    break;
                case 1:
                    myList.Sort(delegate(KeyValuePair<string, int> s1, KeyValuePair<string, int> s2)
                    {
                        return s2.Value.CompareTo(s1.Value);
                    });
                    break;
                case 2:
                    myList.Sort(delegate(KeyValuePair<string, int> s1, KeyValuePair<string, int> s2)
                    {
                        return s1.Key.CompareTo(s2.Key);
                    });
                    break;
                default:
                    myList.Sort(delegate(KeyValuePair<string, int> s1, KeyValuePair<string, int> s2)
                    {
                        return s2.Key.CompareTo(s1.Key);
                    });
                    break;
            }
            dic.Clear();
            foreach (KeyValuePair<string, int> pair in myList)
            {
                dic.Add(pair.Key, pair.Value);
            }
        }
    }
}
