using MobileUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsWindowed : Windowed
{
    [SerializeField] private GameEventChannel _channel;

    protected override void Awake()
    {
        base.Awake();

        _channel.OnGameStarted += OnGameStarted;
        _channel.OnBackToMain += OnBackToMain;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

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
}
