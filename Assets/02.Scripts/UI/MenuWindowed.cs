using MobileUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWindowed : Windowed
{
    [SerializeField] private UnityEngine.UI.Button _pauseButton;
    [SerializeField] private UnityEngine.UI.Button _resumeButton;
    [SerializeField] private UnityEngine.UI.Button _forceGameOverButton;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameEventChannel _channel;

    protected override void Awake()
    {
        base.Awake();

        _pauseButton.onClick.AddListener(_channel.Pause);
        _resumeButton.onClick.AddListener(_channel.Resume);
        _forceGameOverButton.onClick.AddListener(_channel.ForceGameOver);

        _channel.OnGameStarted += OnGameStarted;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _channel.OnGameStarted += OnGameStarted;
    }

    private void OnGameStarted()
    {
        Open();

        _menuPanel.SetActive(false);
    }

    protected override void OnOpened()
    {
        _channel.OnPaused += OnPaused;
        _channel.OnResumed += OnResumed;
        _channel.OnBackToMain += OnBackToMain;
        _channel.OnForcedGameOver += OnForcedGameOver;
    }

    protected override void OnClosed()
    {
        _channel.OnPaused -= OnPaused;
        _channel.OnResumed -= OnResumed;
        _channel.OnBackToMain -= OnBackToMain;
        _channel.OnForcedGameOver -= OnForcedGameOver;
    }

    private void OnPaused()
    {
        _menuPanel.SetActive(true);
    }

    private void OnResumed()
    {
        _menuPanel.SetActive(false);
    }

    private void OnBackToMain()
    {
        _menuPanel.SetActive(false);
    }

    private void OnForcedGameOver()
    {
        _menuPanel.SetActive(false);
    }
}
