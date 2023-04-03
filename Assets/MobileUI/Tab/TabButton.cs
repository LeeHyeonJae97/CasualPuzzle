using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class TabButton : TabButtonBase
    {
        [SerializeField] private GameObject _tab;

        protected override void OnSelected()
        {
            base.OnSelected();

            _tab.SetActive(true);
            _tab.transform.SetAsLastSibling();
        }

        protected override void OnDeselected()
        {
            base.OnDeselected();

            _tab.SetActive(false);
        }
    }
}
