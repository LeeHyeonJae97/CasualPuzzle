using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class TabButtonPanel : TabButtonBase
    {
        [SerializeField] private Panel _panel;

        protected override void OnSelected()
        {
            base.OnSelected();

            _panel.Open();
            _panel.transform.SetAsLastSibling();
        }

        protected override void OnDeselected()
        {
            base.OnDeselected();

            _panel.Close();
        }
    }
}