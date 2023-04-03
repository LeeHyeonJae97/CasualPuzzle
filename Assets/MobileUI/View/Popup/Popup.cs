using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(Canvas))]
    public class Popup : View
    {
        public static T Get<T>() where T : Popup
        {
            return Instantiate(Resources.Load<T>($"Canvas - {typeof(T).Name}"));
        }

        protected override void Awake()
        {
            base.Awake();

            StartCoroutine(CoSetActive(false, true, true, false));
        }

        protected override sealed IEnumerator CoOpen(bool directly, bool kill, bool complete)
        {
            OnBeforeOpened();

            yield return StartCoroutine(CoSetActive(true, directly, kill, complete));

            OnOpened();
        }

        protected override sealed IEnumerator CoClose(bool directly, bool kill, bool complete)
        {
            OnBeforeClosed();

            yield return StartCoroutine(CoSetActive(false, directly, kill, complete));

            OnClosed();
        }

        protected override void OnClosed()
        {
            Destroy(gameObject);
        }
    }
}
