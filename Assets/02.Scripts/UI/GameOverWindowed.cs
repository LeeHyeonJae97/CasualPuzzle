using MobileUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindowed : Windowed
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private UnityEngine.UI.Button _restartButton;
    [SerializeField] private UnityEngine.UI.Button _backToMainButton;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameEventChannel _channel;

    protected override void Awake()
    {
        base.Awake();

        _restartButton.onClick.AddListener(OnClickRestartButton);
        _backToMainButton.onClick.AddListener(OnClickBackToMainButton);

        _channel.OnGameOver += OnGameOver;
        _channel.OnRestarted += OnRestarted;
        _channel.OnBackToMain += OnBackToMain;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _channel.OnGameOver -= OnGameOver;
        _channel.OnRestarted -= OnRestarted;
        _channel.OnBackToMain -= OnBackToMain;
    }

    private void OnClickRestartButton()
    {
        _channel.Restart();
    }

    private void OnClickBackToMainButton()
    {
        _channel.BackToMain();
    }

    private void OnGameOver()
    {
        _scoreText.text = $"{_gameManager.Score}";

        Open();
    }

    private void OnRestarted()
    {
        Close();
    }

    private void OnBackToMain()
    {
        Close();
    }
}
