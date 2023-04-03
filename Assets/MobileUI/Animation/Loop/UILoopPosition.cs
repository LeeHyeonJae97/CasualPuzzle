using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(RectTransform))]
    public class UILoopPosition : UILoop
    {
        [SerializeField] private Vector2 _start;
        [SerializeField] private Vector2 _end;
        [SerializeField] private RectTransform _rectTr;

        protected override void Reset()
        {
            base.Reset();

            _rectTr = GetComponent<RectTransform>();
        }

        public override void Play()
        {
            _rectTr.anchoredPosition = _start;
            _rectTr.DOAnchorPos(_end, _duration).SetEase(_ease).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
