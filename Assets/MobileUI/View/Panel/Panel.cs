using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MobileUI
{
    public sealed class Panel : UIBehaviour
    {
        public UITweenerKey Key => _key;

        private UITweenerKey _key;
        private UITween _tween;

        private void Awake()
        {
            _tween = GetComponent<UITween>();
        }

        public void Open(UITweenerKey key, bool directly = false)
        {
            if (_tween != null)
            {
                StartCoroutine(CoOpen(key, directly));
            }

            IEnumerator CoOpen(UITweenerKey key, bool directly = false)
            {
                _key = key;

                if (key == UITweenerKey.Open)
                {
                    gameObject.SetActive(true);
                }

                yield return StartCoroutine(_tween.CoPlay(key, directly));

                if (key == UITweenerKey.Close)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public void Open(bool directly = false)
        {
            Open(UITweenerKey.Open, directly);
        }

        public void Close(bool directly = false)
        {
            Open(UITweenerKey.Close, directly);
        }
    }
}
