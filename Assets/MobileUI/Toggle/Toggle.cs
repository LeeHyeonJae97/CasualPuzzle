using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MobileUI
{
    public class Toggle : MonoBehaviour, IPointerClickHandler
    {
        public bool IsOn => _isOn;

        [SerializeField] private bool _interactable;
        [SerializeField] private bool _isOn;
        [SerializeField] private RectTransform _handle;
        [SerializeField] private float _onHandlePosition;
        [SerializeField] private float _offHandlePosition;

        [field: NonSerialized] public UnityEvent<bool> onStateChanged { get; private set; } = new UnityEvent<bool>();

#if UNITY_EDITOR
        private void Reset()
        {
            _interactable = true;
            _isOn = true;
        }
#endif

        private void Awake()
        {
            if (!_interactable) return;

            Switch(_isOn, true, false);
        }

        public void Switch(bool directly, bool invokeEvent)
        {
            if (!_interactable) return;

            Switch(!_isOn, directly, invokeEvent);
        }

        public void Switch(bool value, bool directly, bool invokeEvent)
        {
            if (!_interactable) return;

            _isOn = value;

            if (directly)
            {
                _handle.anchoredPosition = new Vector2(value ? _onHandlePosition : _offHandlePosition, _handle.anchoredPosition.y);
            }
            else
            {
                _interactable = false;
                _handle.DOAnchorPosX(value ? _onHandlePosition : _offHandlePosition, .5f).onComplete += () => _interactable = true;
            }

            if (invokeEvent)
            {
                onStateChanged.Invoke(value);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_interactable) return;

            Switch(false, true);
        }
    }
}