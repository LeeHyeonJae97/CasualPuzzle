using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEventChannel", menuName = "ScriptableObject/GameEventChannel")]
public class GameEventChannel : EventChannel
{
    public event UnityAction OnGameStarted;
    public event UnityAction OnPaused;
    public event UnityAction OnResumed;
    public event UnityAction OnRestarted;
    public event UnityAction OnGameOver;
    public event UnityAction OnBackToMain;
    public event UnityAction OnForcedGameOver;

    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    public void Pause()
    {
        OnPaused?.Invoke();
    }

    public void Resume()
    {
        OnResumed?.Invoke();
    }

    public void Restart()
    {
        OnRestarted?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void BackToMain()
    {
        OnBackToMain?.Invoke();
    }

    public void ForceGameOver()
    {
        OnForcedGameOver?.Invoke();
    }
}
