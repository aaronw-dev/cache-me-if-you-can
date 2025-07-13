using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager stats;
    [Header("Stats")]
    public int score;
    public int streak;
    public int successfulOperations;
    public float gameDuration;
    [Header("Text/UI")]
    public TMP_Text scoreText;
    public TMP_Text streakText;
    public TMP_Text successfulText;
    public TMP_Text durationText;
    void Start()
    {
        stats = this;
        gameDuration = 0;
    }

    void Update()
    {
        gameDuration += Time.deltaTime;

        if (scoreText)
        {
            scoreText.text = score.ToString();
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
