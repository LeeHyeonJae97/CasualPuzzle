using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class ItemData : IScrollRectItem
    {
        public int index;

        public ItemData(int index)
        {
            this.index = index;
        }
    }
}
