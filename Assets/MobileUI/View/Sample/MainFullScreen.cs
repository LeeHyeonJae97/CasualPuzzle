using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class MainFullScreen : FullScreen
    {
        [SerializeField] private UnityEngine.UI.Button _menuButton;

        protected override void Awake()
        {
            base.Awake();

            _menuButton.onClick.AddListener(OnClickMenuButton);
        }

        private void OnClickMenuButton()
        {
            Get<MenuWindowed>().Open();
        }
    }
}
