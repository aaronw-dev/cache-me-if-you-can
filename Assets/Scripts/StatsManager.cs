using System;
using System.Collections;
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
    public int highestStreak;
    public int successfulOperations;
    public float gameDuration;
    public string gameTime
    {
        get
        {
            int minutes = Mathf.FloorToInt(gameDuration / 60f);
            int seconds = Mathf.FloorToInt(gameDuration % 60f);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    [Header("Text/UI")]
    public TMP_Text scoreText;
    public float scoreCountSpeed = 10;
    public TMP_Text streakText;
    public TMP_Text successfulText;
    public TMP_Text durationText;
    [Header("Game End")]
    public TMP_Text loseReasonText;
    public TMP_Text endScoreText;
    public TMP_Text endTimeText;
    public TMP_Text endStreakText;
    [Header("Game State")]
    public bool overclocked;

    [HideInInspector]
    public bool gameRunning = true;
    Coroutine overclockCoroutine;
    void Start()
    {
        global = this;
        gameDuration = 0;
        gameRunning = true;
    }
    public void EndGame(GameEndReason reason)
    {
        gameRunning = false;
        endScoreText.text = $"Score: {score} points";
        loseReasonText.text = $"Stop code: {reason}";
        endTimeText.text = $"Time: {gameTime}";
        endStreakText.text = $"Highest streak: {highestStreak} tiles";
    }

    public void Overclock(float duration)
    {
        if (overclockCoroutine != null)
            StopCoroutine(overclockCoroutine);
        overclockCoroutine = StartCoroutine(setOverclock(duration));
    }

    public IEnumerator setOverclock(float duration)
    {
        overclocked = true;
        yield return new WaitForSeconds(duration);
        overclocked = false;
    }

    void Update()
    {
        if (gameRunning)
            gameDuration += Time.deltaTime;
        score = Math.Clamp(score, 0, 999999);

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
            durationText.text = gameTime;
        }
    }
}
