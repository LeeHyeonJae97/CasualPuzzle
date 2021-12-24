using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private int score;
    public int Score
    {
        get { return score; }

        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
    }

    public void Gain(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
