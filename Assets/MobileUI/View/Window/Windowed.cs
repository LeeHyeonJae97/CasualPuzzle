using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class Windowed : Window
    {
        protected override sealed IEnumerator CoOpen(bool directly, bool kill, bool complete)
        {
            // can't open duplicately
            if (_activatedStack.Contains(this)) yield break;

            // can't open over popup window
            //if (_activatedStack.Count > 0) yield break;

            OnBeforeOpened();

            // open new window
            yield return StartCoroutine(CoSetActive(true, directly, kill, complete));

            // push new window to stack
            _activatedStack.Push(this);

            OnOpened();
        }

        protected override sealed IEnumerator CoClose(bool directly, bool kill, bool complete)
        {
            // can't close not opened window
            if (!_activatedStack.Contains(this)) yield break;

            OnBeforeClosed();

            // pop(remove) from stack
            _activatedStack.Pop();

            // close old window
            yield return StartCoroutine(CoSetActive(false, directly, kill, complete));

            OnClosed();
        }
    }
}
