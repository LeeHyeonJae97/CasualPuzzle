using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MobileUI
{
    public class ExtendedContentSizeFilter : ContentSizeFitter
    {
        public FitMode RadialFit { get { return _radialFit; } set { _radialFit = value; } }

        [SerializeField] private FitMode _radialFit;

        public override void SetLayoutHorizontal()
        {
            base.SetLayoutHorizontal();
        }

        public override void SetLayoutVertical()
        {
            base.SetLayoutVertical();
        }
    }
}