using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(RectTransform))]
    public class UITweenerRotation : UITweener
    {
        [SerializeField] private Vector3 _start;
        [SerializeField] private Vector3 _end;
        [SerializeField] private RotateMode _rotateMode;
        [SerializeField] private RectTransform _rectTr;

        internal override IEnumerator CoPlay(bool directly)
        {
            if (directly)
            {
                _rectTr.localEulerAngles = _end;
            }
            else
            {
                if (_overrideCurrent)
                {
                    _rectTr.localEulerAngles = _start;
                }
                _tweener = _rectTr.DORotate(_end, _duration, _rotateMode).SetEase(_ease);
                yield return _tweener.WaitForCompletion();
            }
        }
    }
}
