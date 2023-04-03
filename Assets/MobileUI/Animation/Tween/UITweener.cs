using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MobileUI
{
    // reserved keys
    //
    // Shut : inactivate gameobject when tween is over
    // Close : just play tween
    public enum UITweenerKey { Open, Close, }

    [System.Serializable]
    public abstract class UITweener
    {
        // type을 Open (UITweenerSize) 이런 식으로 설정
        
        public UITweenerKey Key => _key;

        [SerializeField, HideInInspector] protected string _type;
        [SerializeField] protected UITweenerKey _key;
        [SerializeField] protected Ease _ease;
        [SerializeField] protected float _duration;
        [SerializeField] protected bool _overrideCurrent;
        protected Tweener _tweener;

        internal abstract IEnumerator CoPlay(bool directly);

        public UITweener()
        {
            _type = $"{GetType().Name.Replace("UITweener", "")} ({_key})";
            _key = UITweenerKey.Open;
            _ease = Ease.Unset;
        }

        internal void OnValidate()
        {
            _type = $"{GetType().Name.Replace("UITweener", "")} ({_key})";
        }

        internal void Kill(bool complete)
        {
            if (_tweener != null && _tweener.IsActive())
            {
                _tweener.Kill(complete);
            }
        }
    }
}
