using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MobileUI
{
    public class Button : UnityEngine.UI.Button
    {
        [SerializeField] private float _longClickInvokeTime;
        private bool _down;
        private float _downDuration;

        [field: NonSerialized] public UnityEvent onLongDown { get; private set; } = new UnityEvent();
        [field: NonSerialized] public UnityEvent onLongUp { get; private set; } = new UnityEvent();

#if UNITY_EDITOR
        protected override void Reset()
        {
            _longClickInvokeTime = 1f;
        }
#endif

        private void Update()
        {
            if (!interactable) return;

            if (_down)
            {
                _downDuration += Time.deltaTime;

                if (_downDuration > _longClickInvokeTime)
                {
                    _down = false;

                    onLongDown.Invoke();
                }
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (_downDuration < _longClickInvokeTime)
            {
                base.OnPointerClick(eventData);
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (!interactable) return;

            _down = true;
            _downDuration = 0;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (!interactable) return;

            _down = false;

            if (_downDuration >= _longClickInvokeTime)
            {
                onLongUp.Invoke();
            }
        }
    }
}
