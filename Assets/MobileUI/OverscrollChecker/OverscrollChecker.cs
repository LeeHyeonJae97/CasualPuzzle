using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MobileUI
{
    [RequireComponent(typeof(ScrollRect))]
    public class OverscrollChecker : MonoBehaviour
    {
        private RectTransform _scrollRect;
        private RectTransform _content;
        private bool _overscrolled;

        public event UnityAction<bool> onOverscrolled;

        private void Awake()
        {
            var scrollRect = GetComponent<ScrollRect>();

            if (scrollRect.horizontal)
            {
                scrollRect.onValueChanged.AddListener(OnValueChangedHorizontal);
            }
            else if (scrollRect.vertical)
            {
                scrollRect.onValueChanged.AddListener(OnValueChangedVertical);
            }

            _scrollRect = scrollRect.GetComponent<RectTransform>();
            _content = scrollRect.content;

            onOverscrolled += (value) => Debug.Log(value);
        }

        private void OnValueChangedVertical(Vector2 value)
        {
            var min = 0;
            var max = (int)(_content.rect.height - _scrollRect.rect.height);
            var y = (int)_content.anchoredPosition.y;

            if (y < min)
            {
                if (!_overscrolled)
                {
                    _overscrolled = true;

                    onOverscrolled?.Invoke(true);
                }
            }
            else if (y >= max)
            {
                if (!_overscrolled)
                {
                    _overscrolled = true;

                    onOverscrolled?.Invoke(false);
                }
            }
            else
            {
                _overscrolled = false;
            }
        }

        private void OnValueChangedHorizontal(Vector2 value)
        {
            var min = (int)(_content.rect.width - _scrollRect.rect.width);
            var max = 0;
            var x = (int)_content.anchoredPosition.x;

            if (x <= -min)
            {
                if (!_overscrolled)
                {
                    _overscrolled = true;

                    onOverscrolled?.Invoke(false);
                }
            }
            else if (x > max)
            {
                if (!_overscrolled)
                {
                    _overscrolled = true;

                    onOverscrolled?.Invoke(true);
                }
            }
            else
            {
                _overscrolled = false;
            }
        }
    }
}
