using MobileUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusWindowed : Windowed
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timerText;
    [SerializeField] private Image _timerBarImage;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameEventChannel _channel;

    protected override void Awake()
    {
        base.Awake();

        _gameManager.OnScoreUpdated += OnScoreUpdated;
        _gameManager.OnTimerUpdated += OnTimerUpdated;

        _channel.OnGameStarted += OnGameStarted;
        _channel.OnBackToMain += OnBackToMain;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _gameManager.OnScoreUpdated -= OnScoreUpdated;
        _gameManager.OnTimerUpdated -= OnTimerUpdated;

        _channel.OnGameStarted -= OnGameStarted;
        _channel.OnBackToMain -= OnBackToMain;
    }

    private void OnGameStarted(GameMode arg0)
    {
        Open();
    }

    private void OnBackToMain()
    {
        Close();
    }

    private void OnTimerUpdated(float curTime, int time)
    {
        _timerText.text = ((int)curTime).ToString();
        _timerBarImage.fillAmount = curTime / time;
    }

    private void OnScoreUpdated(int score)
    {
        _scoreText.text = $"{score}";
    }
}
