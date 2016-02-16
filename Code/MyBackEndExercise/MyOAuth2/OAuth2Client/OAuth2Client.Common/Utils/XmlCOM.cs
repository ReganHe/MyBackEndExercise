﻿/*
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
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.IO;
using System.Xml;
namespace OAuth2Client.Common.Utils
{
    public static class XmlCOM
    {
        public static DataSet ReadXml(string path)
        {
            DataSet ds = new DataSet();
            FileStream fs = null;
            StreamReader reader = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs, System.Text.Encoding.UTF8);
                ds.ReadXml(reader);
                return ds;
            }
            finally
            {
                fs.Close();
                reader.Close();
            }
        }
        /// <summary>
        /// 读取Config参数
        /// </summary>
        public static string ReadConfig(string name, string key)
        {
            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.Load(HttpContext.Current.Server.MapPath(name + ".config"));
            System.Xml.XmlNodeList xnl = xd.GetElementsByTagName(key);
            if (xnl.Count == 0)
                return "";
            else
            {
                System.Xml.XmlNode mNode = xnl[0];
                return mNode.InnerText;
            }
        }
        public static string GetText(string name, string _XmlPathNode)
        {
            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.Load(HttpContext.Current.Server.MapPath(name + ".config"));
            XmlNode _Node = xd.SelectSingleNode(_XmlPathNode);
            if (_Node != null)
                return _Node.InnerText;
            else
                return "";
        }
        /// <summary>
        /// 保存Config参数
        /// </summary>
        public static void UpdateConfig(string name, string nKey, string nValue)
        {
            if (ReadConfig(name, nKey) != "")
            {
                try
                {
                    System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
                    XmlDoc.Load(HttpContext.Current.Server.MapPath(name + ".config"));
                    System.Xml.XmlNodeList elemList = XmlDoc.GetElementsByTagName(nKey);
                    System.Xml.XmlNode mNode = elemList[0];
                    mNode.InnerText = nValue;
                    System.Xml.XmlTextWriter xw = new System.Xml.XmlTextWriter(new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(name + ".config")));
                    xw.Formatting = System.Xml.Formatting.Indented;
                    XmlDoc.WriteTo(xw);
                    xw.Close();
                }catch
                {
                }
            }
        }
    }
}
