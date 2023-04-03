using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MobileUI
{
    public class TabGroup : MonoBehaviour
    {
        public TabButtonBase Selected { get; internal set; }

        [SerializeField] private TabButtonBase _initial;

        private void Start()
        {
            if (_initial != null)
            {
                Initialize(_initial);
            }
            else
            {
                Debug.LogWarning("There's no initial TabButton");
            }
        }

        public void Initialize(TabButtonBase initial)
        {
            if (initial == null) return;

            var buttons = GetComponentsInChildren<TabButtonBase>().Where((TabButtonBase button) => button.TabGroup == this);

            foreach (var button in buttons)
            {
                button.OnStateChanged(false, false);
            }

            initial.Select();
        }
    }
}
