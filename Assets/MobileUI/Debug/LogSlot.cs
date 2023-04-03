using MobileUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LogSlot : ScrollRectSlot
{
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _stackTraceText;
    [SerializeField] private UnityEngine.UI.Button _button;
    [SerializeField] private TextMeshProUGUI _logText;
    private LogItem _item;

    private void Awake()
    {
        _button.onClick.AddListener(OnClickSlotButton);
    }

    public override void Init(IScrollRectItem item)
    {
        _item = item as LogItem;

        _messageText.text = _item.Message;
        _stackTraceText.text = _item.StackTrace;
    }

    private void OnClickSlotButton()
    {
        _logText.text = $"{_item.Message}\n{_item.StackTrace}";
    }
}

public class LogItem : IScrollRectItem
{
    public string Message { get; private set; }
    public string StackTrace { get; private set; }

    public LogItem(string message, string stackTrace)
    {
        Message = message;
        StackTrace = stackTrace;
    }
}