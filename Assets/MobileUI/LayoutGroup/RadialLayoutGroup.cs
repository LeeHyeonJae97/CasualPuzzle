using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MobileUI
{
    public class RadialLayoutGroup : LayoutGroup
    {
        private enum Origin { Top, Right, Bottom, Left }

        [SerializeField] private float _angle;
        [SerializeField] private float _spacing;
        [SerializeField] private float _radius;
        [SerializeField] private bool _clockwise;
        [SerializeField] private Origin _origin;

#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();

            _angle = 360;
            _spacing = 10;
            _radius = 100;
            _clockwise = true;

            CalculateRadial();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            CalculateRadial();
        }
#endif

        public override void CalculateLayoutInputHorizontal()
        {
            CalculateRadial();
        }

        public override void CalculateLayoutInputVertical()
        {
            CalculateRadial();
        }

        public override void SetLayoutHorizontal()
        {

        }

        public override void SetLayoutVertical()
        {

        }

        private void CalculateRadial()
        {
            if (transform.childCount == 0) return;

            m_Tracker.Clear();

            float angle = m_Padding.left * (_clockwise ? -1 : 1) + 90 - (int)_origin * 90;

            for (int i = 0; i < transform.childCount; i++)
            {
                RectTransform child = (RectTransform)transform.GetChild(i);

                // Adding the elements to the tracker stops the user from modifiying their positions via the editor.
                m_Tracker.Add(this, child, DrivenTransformProperties.Anchors | DrivenTransformProperties.AnchoredPosition | DrivenTransformProperties.Pivot);

                child.localPosition = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * _radius;

                //Force objects to be center aligned, this can be changed however I'd suggest you keep all of the objects with the same anchor points.
                child.anchorMin = child.anchorMax = child.pivot = new Vector2(0.5f, 0.5f);

                angle = _clockwise ? angle - _spacing : angle + _spacing;
            }
        }
    }
}
