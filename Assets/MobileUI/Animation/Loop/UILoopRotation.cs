using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(RectTransform))]
    public class UILoopRotation : UILoop
    {
        [SerializeField] private Vector3 _start;
        [SerializeField] private Vector3 _end;
        [SerializeField] private RotateMode _rotateMode;
        [SerializeField] private RectTransform _rectTr;

        protected override void Reset()
        {
            base.Reset();

            _rotateMode = RotateMode.FastBeyond360;
            _rectTr = GetComponent<RectTransform>();
        }

        public override void Play()
        {
            _rectTr.localEulerAngles = _start;
            _rectTr.DORotate(_end, _duration, _rotateMode).SetEase(_ease).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
