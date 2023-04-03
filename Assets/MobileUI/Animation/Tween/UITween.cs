using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(UIBehaviour))]
    public class UITween : UIBehaviour
    {
        [SerializeReference] private List<UITweener> _tweeners = new List<UITweener>();

        protected void OnValidate()
        {
            foreach(var tweener in _tweeners)
            {
                tweener.OnValidate();
            }
        }

        internal IEnumerator CoPlay(UITweenerKey key, bool directly)
        {
            if (directly)
            {
                foreach (var tweener in _tweeners)
                {
                    if (tweener.Key == key)
                    {
                        StartCoroutine(tweener.CoPlay(directly));
                    }
                }
            }
            else
            {
                var cors = new List<Coroutine>();

                foreach (var tweener in _tweeners)
                {
                    if (tweener.Key == key)
                    {
                        cors.Add(StartCoroutine(tweener.CoPlay(directly)));
                    }
                }

                foreach (var cor in cors)
                {
                    yield return cor;
                }
            }
        }

        internal void Kill(bool complete)
        {
            foreach (var tweener in _tweeners)
            {
                tweener.Kill(complete);
            }
        }

#if UNITY_EDITOR
        public void AddTweener(UITweener tweener)
        {
            _tweeners.Add(tweener);
        }
#endif
    }
}