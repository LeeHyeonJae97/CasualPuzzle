using MobileUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWindowed : Windowed
{
    [SerializeField] private UnityEngine.UI.Button _startButton;
    [SerializeField] private UnityEngine.UI.Button _exitButton;
    [SerializeField] private GameEventChannel _channel;

    protected override void Awake()
    {
        base.Awake();

        _startButton.onClick.AddListener(OnClickStartButton);
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

    private void OnClickStartButton()
    {
        _channel.StartGame();
    }

    private void OnClickExitButton()
    {
        Application.Quit();
    }

    private void OnGameStarted()
    {
        Close();
    }

    private void OnBackToMain()
    {
        Open();
    }    
}
