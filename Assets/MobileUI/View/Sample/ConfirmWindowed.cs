using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class ConfirmWindowed : Windowed
    {
        [SerializeField] private UnityEngine.UI.Button _openSettingsFullScreenButton;
        [SerializeField] private UnityEngine.UI.Button _closeButton;

        protected override void Awake()
        {
            base.Awake();

            _openSettingsFullScreenButton.onClick.AddListener(OnClickOpenSettingsFullScreenButton);
            _closeButton.onClick.AddListener(OnClickCloseButton);
        }

        private void OnClickOpenSettingsFullScreenButton()
        {
            Get<SettingsFullScreen>().Open();
        }

        private void OnClickCloseButton()
        {
            Close();
        }
    }
}
