using UnityEditor;
using UnityEngine;

namespace NaughtyAttributes.Editor
{
	public abstract class PropertyDrawerBase : PropertyDrawer
	{
		public sealed override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			// Check if visible
			bool visible = PropertyUtility.IsVisible(property);
			if (!visible)
			{
				return;
			}

			// Check if enabled and draw
			EditorGUI.BeginChangeCheck();
			bool enabled = PropertyUtility.IsEnabled(property);

			using (new EditorGUI.DisabledScope(disabled: !enabled))
			{
				OnGUI_Internal(rect, property, PropertyUtility.GetLabel(property));
			}

			// Validate
			ValidatorAttribute[] validatorAttributes = PropertyUtility.GetAttributes<ValidatorAttribute>(property);
			foreach (var validatorAttribute in validatorAttributes)
			{
				validatorAttribute.GetValidator().ValidateProperty(property);
			}

			// Call OnValueChanged callbacks
			if (EditorGUI.EndChangeCheck())
			{
				PropertyUtility.CallOnValueChangedCallbacks(property);
			}
		}

		protected abstract void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label);

		sealed override public float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			bool visible = PropertyUtility.IsVisible(property);
			if (!visible)
			{
				return 0.0f;
			}

			return GetPropertyHeight_Internal(property, label);
		}

		protected virtual float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, includeChildren: true);
		}

		protected float GetPropertyHeight(SerializedProperty property)
		{
			SpecialCaseDrawerAttribute specialCaseAttribute = PropertyUtility.GetAttribute<SpecialCaseDrawerAttribute>(property);
			if (specialCaseAttribute != null)
			{
				return specialCaseAttribute.GetDrawer().GetPropertyHeight(property);
			}

			return EditorGUI.GetPropertyHeight(property, includeChildren: true);
		}

		public virtual float GetHelpBoxHeight()
		{
			return EditorGUIUtility.singleLineHeight * 2.0f;
		}

		public virtual float GetHelpBoxWithDocsHeight()
		{
			return GetHelpBoxHeight() + 20f;
		}

		public void DrawDefaultPropertyAndHelpBox(Rect rect, SerializedProperty property, string message, MessageType messageType)
		{
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
				GetHelpBoxHeight());

			NaughtyEditorGUI.HelpBox(helpBoxRect, message, messageType, context: property.serializedObject.targetObject);
		}
	}
}
