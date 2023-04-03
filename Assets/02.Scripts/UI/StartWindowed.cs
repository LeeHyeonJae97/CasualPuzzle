using MobileUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWindowed : Windowed
{
    [SerializeField] private UnityEngine.UI.Button _startTimeAttackModeButton;
    [SerializeField] private UnityEngine.UI.Button _startInfinityModeButton;
    [SerializeField] private UnityEngine.UI.Button _exitButton;
    [SerializeField] private GameEventChannel _channel;

    protected override void Awake()
    {
        base.Awake();

        _startTimeAttackModeButton.onClick.AddListener(OnClickStartTimeAttackModeButton);
        _startInfinityModeButton.onClick.AddListener(OnClickStartInfinityModeButton);
        _exitButton.onClick.AddListener(OnClickExitButton);

        _channel.OnBackToMain += OnBackToMain;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _channel.OnBackToMain -= OnBackToMain;
    }

    protected override void OnOpened()
    {
        _channel.OnGameStarted += OnGameStarted;
    }

    protected override void OnClosed()
    {
        _channel.OnGameStarted -= OnGameStarted;
    }

    private void OnClickStartTimeAttackModeButton()
    {
        _channel.StartGame(GameMode.TimeAttack);
    }

    private void OnClickStartInfinityModeButton()
    {
        _channel.StartGame(GameMode.Infinity);
    }

    private void OnClickExitButton()
    {
        Application.Quit();
    }

    private void OnGameStarted(GameMode mode)
    {
        Close();
    }

    private void OnBackToMain()
    {
        Open();
    }    
}
