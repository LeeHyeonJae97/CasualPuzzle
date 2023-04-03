using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class PopupTextViewTest : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Popup.Get<PopupTextView>().Open(new PopupTextViewData($"{Random.value}", 1.5f));
            }
        }
    }
}
