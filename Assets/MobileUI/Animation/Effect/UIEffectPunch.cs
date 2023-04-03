using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(RectTransform))]
    public class UIEffectPunch : UIEffect
    {
        private enum PunchType { Scale, Rotation }

        [SerializeField] private PunchType _type;
        [SerializeField] private Vector3 _punch;
        [SerializeField] private int _vibrato;
        [SerializeField] private float _elasticity;
        [SerializeField] private RectTransform _rectTr;

        protected override void Reset()
        {
            base.Reset();

            _type = PunchType.Scale;
            _punch = Vector3.one;
            _vibrato = 10;
            _elasticity = 1f;
            _rectTr = GetComponent<RectTransform>();
        }

        public override void Play()
        {
            if (_type == PunchType.Scale)
            {
                _rectTr.DOPunchScale(_punch, _duration, _vibrato, _elasticity);
            }
            else if (_type == PunchType.Rotation)
            {
                _rectTr.DOPunchRotation(_punch, _duration, _vibrato, _elasticity);
            }
        }
    }
}
