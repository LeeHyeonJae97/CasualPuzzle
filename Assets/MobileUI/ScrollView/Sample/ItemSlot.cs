using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MobileUI
{
    public class ItemSlot : ScrollRectSlot
    {
        [SerializeField] private TextMeshProUGUI _text;

        public override void Init(IScrollRectItem data)
        {
            _text.text = $"{(data as ItemData).index}";
        }
    }
}