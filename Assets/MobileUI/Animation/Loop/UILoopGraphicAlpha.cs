using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MobileUI
{
    [RequireComponent(typeof(Graphic))]
    public class UILoopGraphicAlpha : UILoop
    {
        [SerializeField] private float _start;
        [SerializeField] private float _end;
        [SerializeField] private Graphic _graphic;

        protected override void Reset()
        {
            base.Reset();

            _graphic = GetComponent<Graphic>();
        }

        public override void Play()
        {
            var color = _graphic.color;
            color.a = _start;
            _graphic.color = color;            
            _graphic.DOFade(_end, _duration).SetEase(_ease).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
