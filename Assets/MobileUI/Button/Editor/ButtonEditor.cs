using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
namespace MobileUI
{
    [CustomEditor(typeof(Button))]
    public class ButtonEditor : UnityEditor.UI.ButtonEditor
    {
        private SerializedProperty _longClickInvokeTime;

        protected override void OnEnable()
        {
            base.OnEnable();

            _longClickInvokeTime = serializedObject.FindProperty("_longClickInvokeTime");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(_longClickInvokeTime);
        }
    }
}
#endif