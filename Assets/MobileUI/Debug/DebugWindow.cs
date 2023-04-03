using MobileUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugWindow : Windowed
{
    [SerializeField] private ExtendedScrollRect _scrollRect;
    [SerializeField] private Draggable _group;
    [SerializeField] private MobileUI.Button _switchButton;
    [SerializeField] private Panel _logPanel;
    [SerializeField] private UnityEngine.UI.Button _logButton;

    protected override void Awake()
    {
        base.Awake();

//#if UNITY_EDITOR
//        Destroy(gameObject);
//#elif UNITY_ANDROID
//                Application.logMessageReceived += OnLogMessageReceived;
//#endif

        Application.logMessageReceived += OnLogMessageReceived;

        _switchButton.onClick.AddListener(OnClickSwitchButton);
        _switchButton.onLongDown.AddListener(OnLongDownSwitchButton);
        _switchButton.onLongUp.AddListener(OnLongUpSwitchButton);
        _logButton.onClick.AddListener(OnClickLogButton);

        _scrollRect.Initialize(new List<IScrollRectItem>());

        _logPanel.Close(true);

        Open();
    }

    protected override void OnDestroy()
    {
        Application.logMessageReceived -= OnLogMessageReceived;
    }

    private void OnLogMessageReceived(string condition, string stackTrace, LogType type)
    {
        _scrollRect.AddItem(new LogItem($"[{System.DateTime.Now:HH:mm:ss}] {condition}", $"{stackTrace}"));
    }

    private void OnClickSwitchButton()
    {
        _logPanel.Open(_logPanel.Key == UITweenerKey.Open ? UITweenerKey.Close : UITweenerKey.Open);
    }

    private void OnLongDownSwitchButton()
    {
        _group.enabled = true;
    }

    private void OnLongUpSwitchButton()
    {
        _group.enabled = false;
    }

    private void OnClickLogButton()
    {
        Debug.Log($"Test : {Random.Range(0, 100)}");
    }
}
