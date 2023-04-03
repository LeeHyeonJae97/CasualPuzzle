using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class DebugButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(() => Debug.Log("OnClick"));
            _button.onLongDown.AddListener(() => Debug.Log("OnLongDown"));
            _button.onLongUp.AddListener(() => Debug.Log("OnLongUp"));
        }
    }
}