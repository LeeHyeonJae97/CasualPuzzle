using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor.AnimatedValues;
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(ExtendedScrollRect))]
[CanEditMultipleObjects]
public class ExtendedScrollRectEditor : Editor
{
    private static string _hError = "For this visibility mode, the Viewport property and the Horizontal Scrollbar property both needs to be set to a Rect Transform that is a child to the Scroll Rect.";
    private static string _vError = "For this visibility mode, the Viewport property and the Vertical Scrollbar property both needs to be set to a Rect Transform that is a child to the Scroll Rect.";
    private static string _typeError = "Type is not matched with content's layout group";
    private static string _noLayoutGroupError = "There's no layout group component on \'content\'";

    private SerializedProperty _content;
    private SerializedProperty _horizontal;
    private SerializedProperty _vertical;
    private SerializedProperty _movementType;
    private SerializedProperty _elasticity;
    private SerializedProperty _inertia;
    private SerializedProperty _decelerationRate;
    private SerializedProperty _scrollSensitivity;
    private SerializedProperty _viewport;
    private SerializedProperty _horizontalScrollbar;
    private SerializedProperty _verticalScrollbar;
    private SerializedProperty _horizontalScrollbarVisibility;
    private SerializedProperty _verticalScrollbarVisibility;
    private SerializedProperty _horizontalScrollbarSpacing;
    private SerializedProperty _verticalScrollbarSpacing;
    //private SerializedProperty _onValueChanged;
    private SerializedProperty _type;
    private SerializedProperty _circularLoop;
    private SerializedProperty _scrollbar;
    private AnimBool _showElasticity;
    private AnimBool _showDecelerationRate;
    private bool _viewportIsNotChild, _hScrollbarIsNotChild, _vScrollbarIsNotChild;

    protected virtual void OnEnable()
    {
        _content = serializedObject.FindProperty("m_Content");
        _horizontal = serializedObject.FindProperty("m_Horizontal");
        _vertical = serializedObject.FindProperty("m_Vertical");
        _movementType = serializedObject.FindProperty("m_MovementType");
        _elasticity = serializedObject.FindProperty("m_Elasticity");
        _inertia = serializedObject.FindProperty("m_Inertia");
        _decelerationRate = serializedObject.FindProperty("m_DecelerationRate");
        _scrollSensitivity = serializedObject.FindProperty("m_ScrollSensitivity");
        _viewport = serializedObject.FindProperty("m_Viewport");
        _horizontalScrollbar = serializedObject.FindProperty("m_HorizontalScrollbar");
        _verticalScrollbar = serializedObject.FindProperty("m_VerticalScrollbar");
        _horizontalScrollbarVisibility = serializedObject.FindProperty("m_HorizontalScrollbarVisibility");
        _verticalScrollbarVisibility = serializedObject.FindProperty("m_VerticalScrollbarVisibility");
        _horizontalScrollbarSpacing = serializedObject.FindProperty("m_HorizontalScrollbarSpacing");
        _verticalScrollbarSpacing = serializedObject.FindProperty("m_VerticalScrollbarSpacing");
        //_onValueChanged = serializedObject.FindProperty("m_OnValueChanged");
        _type = serializedObject.FindProperty("_type");
        _circularLoop = serializedObject.FindProperty("_circularLoop");
        _scrollbar = serializedObject.FindProperty("_scrollbar");
        _showElasticity = new AnimBool(Repaint);
        _showDecelerationRate = new AnimBool(Repaint);

        SetAnimBools(true);
    }

    protected virtual void OnDisable()
    {
        _showElasticity.valueChanged.RemoveListener(Repaint);
        _showDecelerationRate.valueChanged.RemoveListener(Repaint);
    }

    private void SetAnimBools(bool instant)
    {
        SetAnimBool(_showElasticity, !_movementType.hasMultipleDifferentValues && _movementType.enumValueIndex == (int)ScrollRect.MovementType.Elastic, instant);
        SetAnimBool(_showDecelerationRate, !_inertia.hasMultipleDifferentValues && _inertia.boolValue == true, instant);
    }

    private void SetAnimBool(AnimBool a, bool value, bool instant)
    {
        if (instant)
        {
            a.value = value;
        }
        else
        {
            a.target = value;
        }
    }

    private void CalculateCachedValues()
    {
        _viewportIsNotChild = false;
        _hScrollbarIsNotChild = false;
        _vScrollbarIsNotChild = false;
        if (targets.Length == 1)
        {
            Transform transform = ((ScrollRect)target).transform;
            if (_viewport.objectReferenceValue == null || ((RectTransform)_viewport.objectReferenceValue).transform.parent != transform)
                _viewportIsNotChild = true;
            if (_horizontalScrollbar.objectReferenceValue == null || ((Scrollbar)_horizontalScrollbar.objectReferenceValue).transform.parent != transform)
                _hScrollbarIsNotChild = true;
            if (_verticalScrollbar.objectReferenceValue == null || ((Scrollbar)_verticalScrollbar.objectReferenceValue).transform.parent != transform)
                _vScrollbarIsNotChild = true;
        }
    }

    public override void OnInspectorGUI()
    {
        SetAnimBools(false);

        serializedObject.Update();
        // Once we have a reliable way to know if the object changed, only re-cache in that case.
        CalculateCachedValues();

        EditorGUILayout.PropertyField(_content);

        if (_content.objectReferenceValue != null)
        {
            LayoutGroup layoutGroup = (_content.objectReferenceValue as RectTransform).GetComponent<LayoutGroup>();

            if (layoutGroup != null)
            {
                if ((_type.enumValueIndex == (int)ExtendedScrollRect.Type.Horizontal && !(layoutGroup is HorizontalLayoutGroup))
                  || (_type.enumValueIndex == (int)ExtendedScrollRect.Type.Vertical && !(layoutGroup is VerticalLayoutGroup))
                  || (_type.enumValueIndex == (int)ExtendedScrollRect.Type.GridHorizontal && !(layoutGroup is GridLayoutGroup))
                  || (_type.enumValueIndex == (int)ExtendedScrollRect.Type.GridVertical && !(layoutGroup is GridLayoutGroup)))
                {
                    EditorGUILayout.HelpBox(_typeError, MessageType.Error);
                }
            }
            else
            {
                EditorGUILayout.HelpBox(_noLayoutGroupError, MessageType.Error);
            }
        }

        EditorGUILayout.PropertyField(_type);      

        switch (_type.enumValueIndex)
        {
            case (int)ExtendedScrollRect.Type.Horizontal:
                {
                    _horizontal.boolValue = true;
                    _vertical.boolValue = false;
                    break;
                }
            case (int)ExtendedScrollRect.Type.Vertical:
                {
                    _horizontal.boolValue = false;
                    _vertical.boolValue = true;
                    break;
                }
            case (int)ExtendedScrollRect.Type.GridHorizontal:
                {
                    _horizontal.boolValue = true;
                    _vertical.boolValue = false;
                    break;
                }
            case (int)ExtendedScrollRect.Type.GridVertical:
                {
                    _horizontal.boolValue = false;
                    _vertical.boolValue = true;
                    break;
                }
        }

        EditorGUILayout.PropertyField(_circularLoop);
        EditorGUILayout.PropertyField(_scrollbar);

        EditorGUILayout.PropertyField(_movementType);

        if (_circularLoop.boolValue)
        {
            _movementType.enumValueIndex = (int)ScrollRect.MovementType.Unrestricted;
        }
        else if (_movementType.enumValueIndex == (int)ScrollRect.MovementType.Unrestricted)
        {
            _movementType.enumValueIndex = (int)ScrollRect.MovementType.Elastic;
        }

        if (EditorGUILayout.BeginFadeGroup(_showElasticity.faded))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_elasticity);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        EditorGUILayout.PropertyField(_inertia);
        if (EditorGUILayout.BeginFadeGroup(_showDecelerationRate.faded))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_decelerationRate);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        EditorGUILayout.PropertyField(_scrollSensitivity);

        EditorGUILayout.Space();

        if (_scrollbar.boolValue && !_circularLoop.boolValue)
        {
            EditorGUILayout.PropertyField(_viewport);

            switch (_type.enumValueIndex)
            {
                case (int)ExtendedScrollRect.Type.Horizontal:
                case (int)ExtendedScrollRect.Type.GridHorizontal:
                    {
                        EditorGUILayout.PropertyField(_horizontalScrollbar);
                        if (_horizontalScrollbar.objectReferenceValue && !_horizontalScrollbar.hasMultipleDifferentValues)
                        {
                            EditorGUI.indentLevel++;
                            EditorGUILayout.PropertyField(_horizontalScrollbarVisibility, EditorGUIUtility.TrTextContent("Visibility"));

                            if ((ScrollRect.ScrollbarVisibility)_horizontalScrollbarVisibility.enumValueIndex == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport
                                && !_horizontalScrollbarVisibility.hasMultipleDifferentValues)
                            {
                                if (_viewportIsNotChild || _hScrollbarIsNotChild)
                                    EditorGUILayout.HelpBox(_hError, MessageType.Error);
                                EditorGUILayout.PropertyField(_horizontalScrollbarSpacing, EditorGUIUtility.TrTextContent("Spacing"));
                            }

                            EditorGUI.indentLevel--;
                        }

                        _verticalScrollbar.objectReferenceValue = null;
                        break;
                    }
                case (int)ExtendedScrollRect.Type.Vertical:
                case (int)ExtendedScrollRect.Type.GridVertical:
                    {
                        EditorGUILayout.PropertyField(_verticalScrollbar);
                        if (_verticalScrollbar.objectReferenceValue && !_verticalScrollbar.hasMultipleDifferentValues)
                        {
                            EditorGUI.indentLevel++;
                            EditorGUILayout.PropertyField(_verticalScrollbarVisibility, EditorGUIUtility.TrTextContent("Visibility"));

                            if ((ScrollRect.ScrollbarVisibility)_verticalScrollbarVisibility.enumValueIndex == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport
                                && !_verticalScrollbarVisibility.hasMultipleDifferentValues)
                            {
                                if (_viewportIsNotChild || _vScrollbarIsNotChild)
                                    EditorGUILayout.HelpBox(_vError, MessageType.Error);
                                EditorGUILayout.PropertyField(_verticalScrollbarSpacing, EditorGUIUtility.TrTextContent("Spacing"));
                            }

                            EditorGUI.indentLevel--;
                        }

                        _horizontalScrollbar.objectReferenceValue = null;
                        break;
                    }
            }
        }

        EditorGUILayout.Space();

        //EditorGUILayout.PropertyField(_onValueChanged);

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
