using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MobileUI
{
    public class UITweenerGraphicAlpha : UITweener
    {
        [SerializeField] private float _start;
        [SerializeField] private float _end;
        [SerializeField] private Graphic _graphic;

        internal override IEnumerator CoPlay(bool directly)
        {
            if (directly)
            {
                var color = _graphic.color;
                color.a = _end;
                _graphic.color = color;
            }
            else
            {
                if (_overrideCurrent)
                {
                    var color = _graphic.color;
                    color.a = _start;
                    _graphic.color = color;
                }
                _tweener = _graphic.DOFade(_end, _duration).SetEase(_ease);
                yield return _tweener.WaitForCompletion();
            }
        }
    }
}
