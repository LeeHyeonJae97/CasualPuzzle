using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class Window : View
    {
        protected static Dictionary<string, Window> _windowDic = new Dictionary<string, Window>();
        protected static Stack<Window> _activatedStack = new Stack<Window>();

        public static T Get<T>() where T : Window
        {
            return _windowDic.TryGetValue(typeof(T).ToString(), out var window) ? (T)window : null;
        }

        public static void Pop(bool directly = false, bool kill = true, bool complete = false)
        {
            _activatedStack.Peek().Close(directly, kill, complete);
        }

        public static void Clear(bool directly = false, bool kill = true, bool complete = false)
        {
            while (_activatedStack.Count > 0)
            {
                _activatedStack.Pop().Close(directly, kill, complete);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _canvas = GetComponent<Canvas>();

            _windowDic.Add(GetType().ToString(), this);

            StartCoroutine(CoSetActive(false, true, true, false));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _windowDic.Remove(GetType().ToString());
        }
    }
}
