using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class UIInitializer : MonoBehaviour
{
    private enum TargetReference { Children, Manual, Scene }

    [SerializeField] private TargetReference _reference;
    [ShowIf("_reference", TargetReference.Manual)]
    [SerializeField] private UIInitializeTarget[] _targets;

    private void Start()
    {
        switch (_reference)
        {
            case TargetReference.Children:
                _targets = GetComponentsInChildren<UIInitializeTarget>(true);
                for (int i = 0; i < _targets.Length; i++)
                    _targets[i].Init();
                Destroy(this);
                break;

            case TargetReference.Manual:
                for (int i = 0; i < _targets.Length; i++)
                    _targets[i].Init();
                Destroy(gameObject);
                break;

            case TargetReference.Scene:
                _targets = FindObjectsOfType<UIInitializeTarget>(true);
                for (int i = 0; i < _targets.Length; i++)
                    _targets[i].Init();
                Destroy(gameObject);
                break;
        }
    }

    private void OnValidate()
    {
        if (_reference != TargetReference.Manual)
            _targets = null;
    }
}
