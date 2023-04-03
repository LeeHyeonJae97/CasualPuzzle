using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class UITweenerPosition : UITweener
    {
        [SerializeField] private Vector2 _start;
        [SerializeField] private Vector2 _end;
        [SerializeField] private RectTransform _rectTr;

        internal override IEnumerator CoPlay(bool directly)
        {
            if (directly)
            {
                _rectTr.anchoredPosition = _end;
            }
            else
            {
                if (_overrideCurrent)
                {
                    _rectTr.anchoredPosition = _start;
                }
                _tweener = _rectTr.DOAnchorPos(_end, _duration).SetEase(_ease);
                yield return _tweener.WaitForCompletion();
            }
        }
    }
}
