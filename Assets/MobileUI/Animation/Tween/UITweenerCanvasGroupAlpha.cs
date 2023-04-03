using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class UITweenerCanvasGroupAlpha : UITweener
    {
        [SerializeField] private float _start;
        [SerializeField] private float _end;
        [SerializeField] private CanvasGroup _group;

        internal void Reset(MonoBehaviour behaviour)
        {
            _ease = Ease.Unset;
            _duration = 1f;
            _group = behaviour.GetComponent<CanvasGroup>();
        }

        internal override IEnumerator CoPlay(bool directly)
        {
            if (directly)
            {
                _group.alpha = _end;
            }
            else
            {
                if (_overrideCurrent)
                {
                    _group.alpha = _start;
                }
                _tweener = _group.DOFade(_end, _duration).SetEase(_ease);
                yield return _tweener.WaitForCompletion();
            }
        }
    }
}
