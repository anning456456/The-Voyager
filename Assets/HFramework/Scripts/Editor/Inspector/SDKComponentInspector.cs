using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Editor;

namespace FrameworkExtension.Editor
{
    [CustomEditor(typeof(SDKComponent))]
    public class SDKComponentInspector : GameFrameworkInspector
    {
        private HelperInfo<SDKHelperBase> m_SDKHelperInfo = new HelperInfo<SDKHelperBase>("SDK");

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            SDKComponent t = (SDKComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                m_SDKHelperInfo.Draw();
            }
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();

            Repaint();
        }

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();

            RefreshTypeNames();
        }

        private void OnEnable()
        {

            m_SDKHelperInfo.Init(serializedObject);

            RefreshTypeNames();
        }

        private void RefreshTypeNames()
        {
            m_SDKHelperInfo.Refresh();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
