using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UILoopCanvasGroupAlpha : UILoop
    {
        [SerializeField] private float _start;
        [SerializeField] private float _end;
        [SerializeField] private CanvasGroup _group;

        protected override void Reset()
        {
            base.Reset();

            _group = GetComponent<CanvasGroup>();
        }

        public override void Play()
        {
            _group.alpha = _start;
            _group.DOFade(_end, _duration).SetEase(_ease).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
