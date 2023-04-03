using UnityEditor;
using UnityEngine;

namespace NaughtyAttributes.Editor
{
	[CustomPropertyDrawer(typeof(DescriptionAttribute))]
	public class DescriptionPropertyDrawer : PropertyDrawerBase
	{
		private DescriptionAttribute Attribute => (DescriptionAttribute)attribute;

		protected override float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
		{
			return (string.IsNullOrEmpty(Attribute.Url) ? GetHelpBoxHeight() : GetHelpBoxWithDocsHeight())
				+ NaughtyEditorGUI.HorizontalSpacing;
		}

		protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
		{
			// Validate
			if (property.propertyType != SerializedPropertyType.String)
			{
				Debug.LogWarning("property should be string");
				return;
			}

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
				DrawDescription(rect, property, messageType);
			}
			else
			{
				DrawDescriptionWithDocs(rect, property, messageType, Attribute.Url);
			}

			EditorGUI.EndProperty();
		}

		private void DrawDescription(Rect rect, SerializedProperty property, MessageType type)
		{
			Rect helpBoxRect = new Rect(
				rect.x,
				rect.y,
				rect.width,
				GetHelpBoxHeight());

			NaughtyEditorGUI.HelpBox(helpBoxRect, property.stringValue, type, context: property.serializedObject.targetObject);
		}

		private void DrawDescriptionWithDocs(Rect rect, SerializedProperty property, MessageType messageType, string docsUrl)
		{
			float docsButtonWidth = 40f, docsButtonHeight = 20f;

			Rect helpBoxRect = new Rect(
				rect.x,
				rect.y,
				rect.width,
				GetHelpBoxHeight() + docsButtonHeight);

			NaughtyEditorGUI.HelpBox(helpBoxRect, property.stringValue, messageType, context: property.serializedObject.targetObject);

			Rect docsButtonRect = new Rect(
				helpBoxRect.x + helpBoxRect.width - docsButtonWidth,
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
