using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;
using System;

public class UIInitializeTarget : MonoBehaviour
{
    private enum ActivateTarget { Canvas, Self }

    [SerializeField] private bool _overrideInitPos;
    [ShowIf("_overrideInitPos")]
    [Indent(1)]
    [SerializeField] private Vector2 _anchoredPos;
    [SerializeField] private bool _setActiveOnAwake;
    [ShowIf("_setActiveOnAwake")]
    [Indent(1)]
    [SerializeField] private bool _active;
    [ShowIf("_setActiveOnAwake")]
    [Indent(1)]
    [SerializeField] private ActivateTarget _activateTarget;

    private void OnValidate()
    {
        if (!_overrideInitPos)
            _anchoredPos = Vector2.zero;

        if (!_setActiveOnAwake)
        {
            _active = false;
            _activateTarget = ActivateTarget.Canvas;
        }
    }

    public void Init()
    {
        GetComponent<RectTransform>().anchoredPosition = _anchoredPos;
        if (_setActiveOnAwake)
        {
            switch (_activateTarget)
            {
                case ActivateTarget.Canvas:
                    GetComponentsInParent<Canvas>(true)[0].enabled = _active;
                    break;

                case ActivateTarget.Self:
                    gameObject.SetActive(_active);
                    break;
            }
        }

        Destroy(this);
    }

    [Button("Load", EButtonEnableMode.Editor, 10)]
    public void Load()
    {
        //Undo.RecordObject(gameObject.transform, "Load");
        GetComponent<RectTransform>().anchoredPosition = _anchoredPos;
    }

    [ShowIf("_overrideInitPos")]
    [Button("Save", EButtonEnableMode.Editor)]
    public void Save()
    {
        _anchoredPos = GetComponent<RectTransform>().anchoredPosition;
    }
}
