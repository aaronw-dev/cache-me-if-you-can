using System;
using System.Globalization;
using TMPro;
using UnityEngine;
public enum GameEndReason
{
    MEMORY_MANAGEMENT,
    CPU_HEAT
}
public class CPUTemperature : MonoBehaviour
{
    public float temperature = 30;
    public float temperatureIncreaseSpeed = 0.1f;
    public TMP_Text thermometerText;
    public static CPUTemperature global;
    public GameObject blueScreen;
    public AudioClip blueScreenSFX;
    public bool blueScreened;
    void Start()
    {
        global = this;
        blueScreen.SetActive(false);
    }

    void Update()
    {
        if (StatsManager.global.gameRunning)
        {
            temperature += Time.deltaTime * temperatureIncreaseSpeed;
            temperature = Mathf.Clamp(temperature, 30, 120);
        }
        thermometerText.text = Math.Round(temperature, 0).ToString(CultureInfo.InvariantCulture) + " C";

        if (temperature >= 120 && !blueScreened)
        {
            blueScreened = true;
            blueScreen.SetActive(true);

            StatsManager.global.EndGame(GameEndReason.MEMORY_MANAGEMENT);

            SoundManager.global.PlaySound(blueScreenSFX);
            SoundManager.global.soundtrackAudioSource.Pause();
        }
    }
}
