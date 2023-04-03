using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public abstract class UIEffect : MonoBehaviour
    {
        [SerializeField] protected bool _playOnEnabled;
        [SerializeField] protected float _duration;
        protected Tweener _tweener;

        protected virtual void Reset()
        {
            _playOnEnabled = true;
            _duration = 1;
        }

        private void OnEnable()
        {
            if (_playOnEnabled)
            {
                Play();
            }
        }

        public abstract void Play();
    }
}
