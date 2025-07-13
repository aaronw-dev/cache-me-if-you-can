using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager global;
    [Header("Stats")]
    public int score;
    private int shownScore;
    public int streak;
    public int successfulOperations;
    public float gameDuration;
    [Header("Text/UI")]
    public TMP_Text scoreText;
    public float scoreCountSpeed = 10;
    public TMP_Text streakText;
    public TMP_Text successfulText;
    public TMP_Text durationText;
    void Start()
    {
        global = this;
        gameDuration = 0;
    }

    void Update()
    {
        gameDuration += Time.deltaTime;

        if (scoreText)
        {
            if (shownScore < score)
            {
                shownScore = (int)Math.Ceiling(Mathf.Lerp(shownScore, score, Time.deltaTime * scoreCountSpeed));
            }
            else
            {
                shownScore = (int)Math.Floor(Mathf.Lerp(shownScore, score, Time.deltaTime * scoreCountSpeed));
            }
            scoreText.text = shownScore.ToString();
        }
        if (streakText)
        {
            streakText.text = streak.ToString();
        }
        if (successfulText)
        {
            successfulText.text = successfulOperations.ToString();
        }
        if (durationText)
        {
            int minutes = Mathf.FloorToInt(gameDuration / 60f);
            int seconds = Mathf.FloorToInt(gameDuration % 60f);
            durationText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
