using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MobileUI
{
    public class TabButtonBase : Button
    {
        internal TabGroup TabGroup => _tabGroup;

        [SerializeField] private TabGroup _tabGroup;

        [field: NonSerialized] public UnityEvent<bool> onStateChanged { get; private set; } = new UnityEvent<bool>();
        [field: NonSerialized] public UnityEvent onSelected { get; private set; } = new UnityEvent();
        [field: NonSerialized] public UnityEvent onDeselected { get; private set; } = new UnityEvent();

#if UNITY_EDITOR
        protected override void Reset()
        {
            _tabGroup = GetComponentInParent<TabGroup>();
        }
#endif

        public override void Select()
        {
            _tabGroup.Selected?.OnStateChanged(false, true);
            _tabGroup.Selected = this;
            _tabGroup.Selected.OnStateChanged(true, true);
        }

        internal void OnStateChanged(bool value, bool invokeEvent)
        {
            if (value)
            {
                OnSelected();

                if (invokeEvent)
                {
                    onSelected.Invoke();
                    onStateChanged.Invoke(true);
                }
            }
            else
            {
                OnDeselected();

                if (invokeEvent)
                {
                    onDeselected.Invoke();
                    onStateChanged.Invoke(false);
                }
            }
        }

        protected virtual void OnSelected()
        {

        }

        protected virtual void OnDeselected()
        {

        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (!interactable) return;

            Select();
        }
    }
}
