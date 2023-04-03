using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.UI;
using UnityEditor;
#endif

#if UNITY_EDITOR
namespace MobileUI
{
    [CustomEditor(typeof(ExtendedHorizontalLayoutGroup))]
    public class ExtendedHorizontalLayoutGroupEditor : HorizontalOrVerticalLayoutGroupEditor
    {
        private SerializedProperty _runtimeMode;

        protected override void OnEnable()
        {
            base.OnEnable();

            _runtimeMode = serializedObject.FindProperty("_runtimeMode");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_runtimeMode);

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}
#endif
