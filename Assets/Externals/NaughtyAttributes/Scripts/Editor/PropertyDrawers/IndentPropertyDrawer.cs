using UnityEditor;
using UnityEngine;

namespace NaughtyAttributes.Editor
{
	[CustomPropertyDrawer(typeof(IndentAttribute))]
	public class IndentPropertyDrawer : PropertyDrawerBase
	{
		private IndentAttribute Attribute => (IndentAttribute)attribute;

		protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(rect, label, property);

			int orgLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = Attribute.Level;
			EditorGUI.PropertyField(rect, property, true);
			EditorGUI.indentLevel = orgLevel;

			EditorGUI.EndProperty();
		}
	}
}
