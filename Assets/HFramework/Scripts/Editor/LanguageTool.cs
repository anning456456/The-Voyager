using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityGameFramework.Runtime;


namespace FrameworkExtension.Editor
{
    public class LanguageTool : EditorWindow
    {
        private const string endPath = "/Dictionaries/Default.xml";
        private const string path = "Assets/GameMain/Localization";

        [MenuItem("Tool/对比多语言")]
        public static void CompareLanguage()
        {
            DirectoryInfo root = new DirectoryInfo(path);
            // FileInfo[] files = root.GetFiles();
            DirectoryInfo[] dics = root.GetDirectories();
            List<Dictionary<string, string>> listLanguage = new List<Dictionary<string, string>>();
            List<string> listLanguageKinds = new List<string>();
            for (int i = 0; i < dics.Length; i++)
            {
                string dirPath = dics[i].FullName + endPath;
                dirPath.Replace("\\", "/");
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(dirPath);

                XmlNode xmlRoot = xmlDocument.SelectSingleNode("Dictionaries");
                XmlNodeList xmlNodeDictionaryList = xmlRoot.ChildNodes;
                XmlNode xmlNodeDictionary = xmlNodeDictionaryList.Item(0);
                string language = xmlNodeDictionary.Attributes.GetNamedItem("Language").Value;
                listLanguageKinds.Add(language);
                XmlNodeList xmlNodeStringList = xmlNodeDictionary.ChildNodes;
                Dictionary<string, string> languageDic = new Dictionary<string, string>();              
                for (int j = 0; j < xmlNodeStringList.Count; j++)
                {
                    XmlNode xmlNodeString = xmlNodeStringList.Item(j);
                    string key = xmlNodeString.Attributes.GetNamedItem("Key").Value;
                    string value = xmlNodeString.Attributes.GetNamedItem("Value").Value;
                    languageDic.Add(key, value);
                }
                listLanguage.Add(languageDic);

                Log.Info(language + "语言key值个数   " + xmlNodeDictionary.ChildNodes.Count);
            }
            bool sucess = true;
            foreach (var t in listLanguage[0])
            {
               for(int i = 1;i<listLanguage.Count;i++)
                {
                    if (!listLanguage[i].ContainsKey(t.Key))
                    {
                        Log.Warning(listLanguageKinds[i] + "不包含key     " + t.Key);
                        sucess = false;
                    }
                        
                }
            }
            if (sucess)
                Log.Info("对比成功");
            else
                Log.Error("对比失败");

        }
    }
}
