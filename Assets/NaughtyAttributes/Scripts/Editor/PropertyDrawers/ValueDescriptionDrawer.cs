using UnityEditor;
using UnityEngine;

namespace NaughtyAttributes.Editor
{
	[CustomPropertyDrawer(typeof(ValueDescriptionAttribute))]
	public class ValueDescriptionDrawer : PropertyDrawerBase
	{
		private ValueDescriptionAttribute Attribute => (ValueDescriptionAttribute)attribute;

		protected override float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight_Internal(property, label)
				+ (string.IsNullOrEmpty(Attribute.Url) ? GetHelpBoxHeight() : GetHelpBoxWithDocsHeight())
				+ NaughtyEditorGUI.HorizontalSpacing;
		}

		protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(rect, label, property);

			// Convert descriptionInfo to messageType
			MessageType messageType = MessageType.None;
			switch (Attribute.Type)
			{
				case DescriptionType.Info:
					messageType = MessageType.Info;
					break;

				case DescriptionType.Warning:
					messageType = MessageType.Warning;
					break;
			}

			if (string.IsNullOrEmpty(Attribute.Url))
			{
				DrawDescription(rect, property, Attribute.Text, messageType);
			}
			else
			{
				DrawDescriptionWithDocs(rect, property, Attribute.Text, messageType, Attribute.Url);
			}

			EditorGUI.EndProperty();
		}

		private void DrawDescription(Rect rect, SerializedProperty property, string description, MessageType type)
		{
			DrawDefaultPropertyAndHelpBox(rect, property, description, type);
		}

		private void DrawDescriptionWithDocs(Rect rect, SerializedProperty property, string message, MessageType messageType, string docsUrl)
		{
			float docsButtonWidth = 40f, docsButtonHeight = 20f;

			Rect propertyRect = new Rect(
				rect.x,
				rect.y,
				rect.width,
				GetPropertyHeight(property));

			EditorGUI.PropertyField(propertyRect, property, true);

			Rect helpBoxRect = new Rect(
				rect.x,
				rect.y + GetPropertyHeight(property),
				rect.width,
				GetHelpBoxHeight() + docsButtonHeight);

			NaughtyEditorGUI.HelpBox(helpBoxRect, message, messageType, context: property.serializedObject.targetObject);

			Rect docsButtonRect = new Rect(
				propertyRect.x + propertyRect.width - docsButtonWidth,
				helpBoxRect.y + helpBoxRect.height - docsButtonHeight,
				docsButtonWidth,
				docsButtonHeight);

			if (GUI.Button(docsButtonRect, "Docs"))
			{
				if (docsUrl.StartsWith("http"))
					Application.OpenURL(docsUrl);
				else
					Application.OpenURL($"https://docs.unity3d.com/ScriptReference/{docsUrl}");
			}
		}
	}
}
