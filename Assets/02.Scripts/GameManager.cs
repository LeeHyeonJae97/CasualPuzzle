using MobileUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int Score
    {
        get { return _score; }

        set
        {
            _score = value;

            OnScoreUpdated?.Invoke(_score);
        }
    }
    private float CurTime
    {
        get { return _curTime; }

        set
        {
            _curTime = value;

            OnTimerUpdated?.Invoke(_curTime, _playTime);
        }
    }

    [SerializeField] private float _timerInterval;
    [SerializeField] private int _playTime;
    [SerializeField] private GameEventChannel _channel;
    private float _curTime;
    private Coroutine _corTimer;
    private int _score;

    public UnityAction<float, int> OnTimerUpdated;
    public UnityAction<int> OnScoreUpdated;

    private void Start()
    {
        AudioChannel.Get("BGM").Play("Main");

        Window.Get<MainFullScreen>().Open();
        Window.Get<StartWindowed>().Open();
    }

    private void OnEnable()
    {
        _channel.OnGameStarted += OnGameStarted;
        _channel.OnPaused += OnPaused;
        _channel.OnResumed += OnResumed;
        _channel.OnRestarted += OnRestarted;
        _channel.OnForcedGameOver += OnForcedGameOver;
        _channel.OnGameOver += OnGameOver;
        _channel.OnBackToMain += OnBackToMain;
    }

    private void OnDisable()
    {
        _channel.OnGameStarted -= OnGameStarted;
        _channel.OnPaused -= OnPaused;
        _channel.OnResumed -= OnResumed;
        _channel.OnRestarted -= OnRestarted;
        _channel.OnForcedGameOver -= OnForcedGameOver;
        _channel.OnGameOver -= OnGameOver;
        _channel.OnBackToMain -= OnBackToMain;
    }

    private void OnGameStarted()
    {
        Score = 0;

        Begin();
    }

    private void OnPaused()
    {
        Time.timeScale = 0;
    }

    private void OnResumed()
    {
        Time.timeScale = 1;
    }

    private void OnRestarted()
    {
        Time.timeScale = 1;

        Score = 0;

        Begin();

        AudioChannel.Get("BGM").Play("Main");
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;

        Stop();
    }

    private void OnBackToMain()
    {
        Time.timeScale = 1;

        AudioChannel.Get("BGM").Play("Main");
    }

    public void OnForcedGameOver()
    {
        _channel.GameOver();
    }

    private void Begin()
    {
        CurTime = _playTime;

        if (_corTimer != null)
        {
            StopCoroutine(_corTimer);
        }
        _corTimer = StartCoroutine(TimerCoroutine());
    }

    private void Stop()
    {
        if (_corTimer != null)
        {
            StopCoroutine(_corTimer);
            _corTimer = null;
        }
    }

    public void GetExtraTime(float amount)
    {
        _curTime += amount;

        OnTimerUpdated?.Invoke(CurTime, _playTime);
    }

    private IEnumerator TimerCoroutine()
    {
        WaitForSeconds interval = new WaitForSeconds(_timerInterval);

        while (_curTime > 0)
        {
            yield return interval;

            CurTime -= _timerInterval;
        }

        _channel.GameOver();
        _corTimer = null;
    }
}
