using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class FullScreen : Window
    {
        protected override sealed IEnumerator CoOpen(bool directly, bool kill, bool complete)
        {
            // can't open duplicately
            if (_activatedStack.Contains(this)) yield break;

            // can't open over popup window
            //if (_activatedStack.Count > 0) yield break;

            // push new window to stack
            _activatedStack.Push(this);

            OnBeforeOpened();

            // open new window
            yield return StartCoroutine(CoSetActive(true, directly, kill, complete));

            OnOpened();

            // close all the windows until fullscreen
            foreach (Window window in _activatedStack)
            {
                if (window.SortingOrder < SortingOrder)
                {
                    StartCoroutine(window.CoSetActive(false, true, kill, complete));
                }

                if (window is FullScreen && window != this) break;
            }
        }

        protected override sealed IEnumerator CoClose(bool directly, bool kill, bool complete)
        {
            // can't close not opened window
            if (!_activatedStack.Contains(this)) yield break;

            // recover closed windows when this window is opened
            foreach (Window window in _activatedStack)
            {
                if (window.SortingOrder < SortingOrder)
                {
                    StartCoroutine(window.CoSetActive(true, true, kill, complete));
                }

                if (window is FullScreen && window != this) break;
            }

            OnBeforeClosed();

            // close this fullscreen window with windowed window over this            
            while (true)
            {
                var window = _activatedStack.Pop();

                yield return StartCoroutine(window.CoSetActive(false, directly, kill, complete));

                if (window == this) break;
            }

            OnClosed();
        }
    }
}
