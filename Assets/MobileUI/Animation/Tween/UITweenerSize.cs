using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class UITweenerSize : UITweener
    {
        [SerializeField] private Vector2 _start;
        [SerializeField] private Vector2 _end;
        [SerializeField] private RectTransform _rectTr;

        internal override IEnumerator CoPlay(bool directly)
        {
            if (directly)
            {
                _rectTr.sizeDelta = _end;
            }
            else
            {
                if (_overrideCurrent)
                {
                    _rectTr.sizeDelta = _start;
                }
                _tweener = _rectTr.DOSizeDelta(_end, _duration).SetEase(_ease);
                yield return _tweener.WaitForCompletion();
            }
        }
    }
}
