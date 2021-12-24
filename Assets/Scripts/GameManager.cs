using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool IsPlaying;

    [SerializeField] private SlotManager slotManager;
    [SerializeField] private Timer timer;
    [SerializeField] private SettingsSO settings;

    [SerializeField] private Canvas gameStartPanel;

    [SerializeField] private Canvas menuPanel;

    [SerializeField] private Canvas gameOverPanel;
    [SerializeField] private GameObject gameOverCoverPanel;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Text scoreText;

    [Space(10)]
    [SerializeField] private int gameTime;

    private void Start()
    {
        settings.Load();
        AudioManager.Instance.PlayBgm("Main");
    }

    public void GameStartTimeAttack()
    {
        IsPlaying = true;
        scoreManager.Score = 0;
        timer.Begin(gameTime);
        timer.gameObject.SetActive(true);
        slotManager.Organize();
        gameStartPanel.enabled = false;
    }

    public void GameStartInfinity()
    {
        IsPlaying = true;
        scoreManager.Score = 0;
        timer.gameObject.SetActive(false);
        slotManager.Organize();
        gameStartPanel.enabled = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        menuPanel.enabled = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        menuPanel.enabled = false;
    }

    public void Restart()
    {
        IsPlaying = true;
        Time.timeScale = 1;

        AudioManager.Instance.PlayBgm("Main");
        scoreManager.Score = 0;
        timer.Begin(gameTime);

        slotManager.Clear();
        slotManager.Organize();

        menuPanel.enabled = false;
        gameOverPanel.enabled = false;
    }

    public void BackToMain()
    {
        Time.timeScale = 1;

        slotManager.Clear();

        gameStartPanel.enabled = true;

        menuPanel.enabled = false;
        gameOverPanel.enabled = false;

        AudioManager.Instance.PlayBgm("Main");
    }

    public void ForceGameOver()
    {
        Time.timeScale = 1;

        timer.Stop();
        menuPanel.enabled = false;
        StartCoroutine(GameOverCoroutine());
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    public IEnumerator GameOverCoroutine()
    {
        IsPlaying = false;

        AudioManager.Instance.StopBgm();
        AudioManager.Instance.PlaySfx("GameOver");
        gameOverCoverPanel.SetActive(false);
        gameOverPanel.enabled = true;

        yield return new WaitForSeconds(2f);

        scoreText.text = scoreManager.Score.ToString();
        gameOverCoverPanel.SetActive(true);
    }
}
