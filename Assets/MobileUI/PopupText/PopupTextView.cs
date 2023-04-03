using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MobileUI
{
    public class PopupTextView : Popup
    {
        [SerializeField] private TextMeshProUGUI _text;

#if UNITY_EDITOR
        private void Reset()
        {
            _data = new PopupTextViewData("Content", 1f);
        }
#endif

        protected override void OnBeforeOpened()
        {
            base.OnBeforeOpened();

            var data = _data as PopupTextViewData;

            _text.text = data.Content;

            StartCoroutine(CoTimer());

            IEnumerator CoTimer()
            {
                yield return new WaitForSeconds(data.Duration);

                Close();
            }
        }
    }

    [System.Serializable]
    public class PopupTextViewData : IViewData
    {
        public string Content => _content;
        public float Duration => _duration;

        [SerializeField] private string _content;
        [SerializeField] private float _duration;

        public PopupTextViewData(string content, float duration)
        {
            _content = content;
            _duration = duration;
        }
    }    
}
