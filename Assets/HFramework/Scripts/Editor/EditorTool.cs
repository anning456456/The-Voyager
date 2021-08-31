using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Editor;

namespace FrameworkExtension.Editor
{
	public class EditorTool
	{
        [MenuItem("Tool/Delete All PlayerPrefs")]
        private static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        [MenuItem("Tool/TEST/Enable")]
        private static void EnabelTest()
        {
            ScriptingDefineSymbols.AddScriptingDefineSymbol("TEST");
        }

        [MenuItem("Tool/TEST/Disable")]
        private static void DisableTest()
        {
            ScriptingDefineSymbols.RemoveScriptingDefineSymbol("TEST");
        }

        [MenuItem("Tool/CN/Enable")]
        private static void EnabelCN()
        {
            ScriptingDefineSymbols.AddScriptingDefineSymbol("CN");
        }

        [MenuItem("Tool/CN/Disable")]
        private static void DisableCN()
        {
            ScriptingDefineSymbols.RemoveScriptingDefineSymbol("CN");
        }
    }
}
