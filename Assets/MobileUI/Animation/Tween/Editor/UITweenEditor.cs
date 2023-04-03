using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
namespace MobileUI
{
    [CustomEditor(typeof(UITween)), CanEditMultipleObjects]
    public class UITweenEditor : Editor
    {
        private SerializedProperty _tweeners;

        private void OnEnable()
        {
            _tweeners = serializedObject.FindProperty("_tweeners");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_tweeners);

            GUILayout.Space(10);
            
            if (GUILayout.Button("CanvasGroupAlpha"))
            {
                (serializedObject.targetObject as UITween).AddTweener(new UITweenerCanvasGroupAlpha());
            }

            if (GUILayout.Button("GraphicAlpha"))
            {
                (serializedObject.targetObject as UITween).AddTweener(new UITweenerGraphicAlpha());
            }

            if (GUILayout.Button("Position"))
            {
                (serializedObject.targetObject as UITween).AddTweener(new UITweenerPosition());
            }

            if (GUILayout.Button("Rotation"))
            {
                (serializedObject.targetObject as UITween).AddTweener(new UITweenerRotation());
            }

            if (GUILayout.Button("Size"))
            {
                (serializedObject.targetObject as UITween).AddTweener(new UITweenerSize());
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
