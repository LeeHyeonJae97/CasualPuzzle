using UnityEngine;
using UnityEditor;

namespace NaughtyAttributes.Editor
{
	[CustomPropertyDrawer(typeof(PropertySpaceAttribute))]
	public class PropertySpacePropertyDrawer : PropertyDrawerBase
	{
		private PropertySpaceAttribute Attribute => (PropertySpaceAttribute)attribute;

		protected override float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight_Internal(property, label) + Attribute.Upper + Attribute.Under;
		}

		protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(rect, label, property);

			GUILayout.Space(Attribute.Upper);
			EditorGUI.PropertyField(rect, property, true);
			GUILayout.Space(Attribute.Under);

			EditorGUI.EndProperty();
		}
	}
}
