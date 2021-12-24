using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private Image timerBarImage;

    [SerializeField] private float interval;

    private int time;
    private float curTime;

    private Coroutine corTimer;

    [SerializeField] private UnityEvent onTimeout;

    public void Begin(int time)
    {
        this.time = time;
        curTime = time;

        timerText.text = curTime.ToString();
        timerBarImage.fillAmount = 1;

        if (corTimer != null) StopCoroutine(corTimer);
        corTimer = StartCoroutine(TimerCoroutine());
    }

    public void Stop()
    {
        if (corTimer != null) StopCoroutine(corTimer);
        corTimer = null;
    }

    public void GetExtraTime(float amount)
    {
        curTime += amount;
        timerText.text = ((int)curTime).ToString();
        timerBarImage.fillAmount = curTime / time;
    }

    private IEnumerator TimerCoroutine()
    {
        WaitForSeconds interval = new WaitForSeconds(this.interval);

        while (curTime > 0)
        {
            yield return interval;

            curTime -= this.interval;
            timerText.text = ((int)curTime).ToString();
            timerBarImage.fillAmount = curTime / time;
        }

        onTimeout?.Invoke();
        corTimer = null;
    }
}
