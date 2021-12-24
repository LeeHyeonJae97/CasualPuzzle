using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private ToggleButton bgmMuteButton;
    [SerializeField] private ToggleButton sfxMuteButton;
    [SerializeField] private SettingsSO settings;

    private void Start()
    {
        bgmMuteButton.SetValueWithoutEvent(settings.BgmMute);
        sfxMuteButton.SetValueWithoutEvent(settings.SfxMute);
    }
}
