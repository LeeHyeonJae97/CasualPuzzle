using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public abstract class View : UIBehaviour
    {
        public bool IsActive { get; private set; }
        public int SortingOrder => _canvas.sortingOrder;

        [SerializeField] private bool _activeOnViewClosed;
        protected Canvas _canvas;
        [SerializeReference] protected IViewData _data;
        private UITween _tween;

        protected abstract IEnumerator CoOpen(bool directly, bool kill, bool complete);
        protected abstract IEnumerator CoClose(bool directly, bool kill, bool complete);

        protected virtual void Awake()
        {
            _tween = GetComponent<UITween>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnDestroy()
        {

        }

        internal IEnumerator CoSetActive(bool value, bool directly, bool kill, bool complete)
        {
            IsActive = value;

            if (_tween == null)
            {
                gameObject.SetActive(value || _activeOnViewClosed);
            }
            else
            {
                if (kill)
                {
                    StopAllCoroutines();
                    _tween.Kill(complete);
                }

                if (value)
                {
                    gameObject.SetActive(true);

                    if (directly)
                    {
                        StartCoroutine(_tween.CoPlay(UITweenerKey.Open, true));
                    }
                    else
                    {
                        yield return StartCoroutine(_tween.CoPlay(UITweenerKey.Open, false));
                    }
                }
                else
                {
                    if (directly)
                    {
                        StartCoroutine(_tween.CoPlay(UITweenerKey.Close, true));
                    }
                    else
                    {
                        yield return StartCoroutine(_tween.CoPlay(UITweenerKey.Close, false));
                    }

                    gameObject.SetActive(_activeOnViewClosed);
                }
            }
        }

        public void Open(bool directly = false, bool kill = true, bool complete = false)
        {
            StartCoroutine(CoOpen(directly, kill, complete));
        }

        public void Open(IViewData data, bool directly = false, bool kill = true, bool complete = false)
        {
            _data = data;

            StartCoroutine(CoOpen(directly, kill, complete));
        }

        public void Close(bool directly = false, bool kill = true, bool complete = false)
        {
            StartCoroutine(CoClose(directly, kill, complete));
        }

        public void Open(bool value, bool directly = false, bool kill = true, bool complete = false)
        {
            StartCoroutine(value ? CoOpen(directly, kill, complete) : CoClose(directly, kill, complete));
        }

        protected virtual void OnBeforeOpened()
        {

        }

        protected virtual void OnOpened()
        {

        }

        protected virtual void OnBeforeClosed()
        {

        }

        protected virtual void OnClosed()
        {

        }
    }
}
