using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FrameworkExtension.Editor
{
    public class FontChangeTool : EditorWindow
    {
        [MenuItem("Tool/更换字体")]
        public static void Open()
        {
            EditorWindow.GetWindow(typeof(FontChangeTool));
        }

        Font toChange;
        static Font toChangeFont;
        FontStyle toFontStyle;
        static FontStyle toChangeFontStyle;

        [System.Obsolete]
        void OnGUI()
        {
            toChange = (Font)EditorGUILayout.ObjectField(toChange, typeof(Font), true, GUILayout.MinWidth(100f));
            toChangeFont = toChange;
            //toFontStyle = (FontStyle)EditorGUILayout.EnumPopup(toFontStyle, GUILayout.MinWidth(100f));
            //toChangeFontStyle = toFontStyle;
            if (GUILayout.Button("更换Hierarchy"))
            {
                Change();
            }
            if (GUILayout.Button("更换选中预设"))
            {
                ChangePrefabs();
            }
            if (GUILayout.Button("更换选中文件夹"))
            {
                ChangeFolderPrefabs();
            }
        }

        [System.Obsolete]
        public static void Change()
        {
            //寻找Hierarchy面板下所有的Text
            var tArray = FindObjectsOfTypeAll(typeof(Text));
            for (int i = 0; i < tArray.Length; i++)
            {
                Text t = tArray[i] as Text;
                //这个很重要，博主发现如果没有这个代码，unity是不会察觉到编辑器有改动的，自然设置完后直接切换场景改变是不被保存
                //的  如果不加这个代码  在做完更改后 自己随便手动修改下场景里物体的状态 在保存就好了 
                Undo.RecordObject(t, t.gameObject.name);
                t.font = toChangeFont;
                //Debug.Log(i + " : " + t.name);
                //t.fontStyle = toChangeFontStyle;
                //相当于让他刷新下 不然unity显示界面还不知道自己的东西被换掉了  还会呆呆的显示之前的东西
                EditorUtility.SetDirty(t);
            }
            Debug.Log("Hierarchy Succed " + tArray.Length);
        }

        public static void ChangePrefabs()
        {
            //获取所有UILabel组件  
            if (Selection.objects == null || Selection.objects.Length == 0) return;
            //如果是UGUI讲UILabel换成Text就可以  
            Object[] labels = Selection.GetFiltered(typeof(Text), SelectionMode.Deep);
            int i = 0;
            foreach (Object item in labels)
            {
                //如果是UGUI讲UILabel换成Text就可以  
                Text label = (Text)item;
                label.font = toChangeFont;
                //label.fontStyle = toChangeFontStyle;
                //label.font = toChangeFont;（UGUI）  
                //Debug.Log(item.name + ":" + label.text);
                //  
                //Debug.Log(i + " : " + label.name);
                i++;
                EditorUtility.SetDirty(item);//重要  
            }
            Debug.Log("Frefabs Succed " + labels.Length);
        }

        public static void ChangeFolderPrefabs()
        {
            //获取所有UILabel组件  
            if (Selection.objects == null || Selection.objects.Length == 0) return;
            //如果是UGUI讲UILabel换成Text就可以  
            Object[] labels = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
            int i = 0;
            foreach (Object item in labels)
            {
                string assetPath = AssetDatabase.GetAssetPath(item);
                if (assetPath.Contains(".meta") || !assetPath.Contains(".prefab"))
                    continue;
                GameObject contentsRoot = PrefabUtility.LoadPrefabContents(assetPath);
                contentsRoot.transform.localScale = Vector3.one;
                Text[] texts = contentsRoot.GetComponentsInChildren<Text>(true);
                if (texts.Length > 0)
                {
                    foreach(Text t in texts)
                    {
                        t.font = toChangeFont;
                        //Debug.Log(i + " : " + t.name);
                        i++;
                    }
                }
                PrefabUtility.SaveAsPrefabAsset(contentsRoot, assetPath);
                PrefabUtility.UnloadPrefabContents(contentsRoot);
            }
            Debug.Log("Folder Succed " + labels.Length + " text: " + i);
        }
    }
}
