using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CPUTemperature : MonoBehaviour
{
    public float temperature = 30;
    public float temperatureIncreaseSpeed = 0.1f;
    public TMP_Text thermometerText;
    public static CPUTemperature global;
    void Start()
    {
        global = this;
    }

    void Update()
    {
        temperature += Time.deltaTime * temperatureIncreaseSpeed;
        temperature = Mathf.Clamp(temperature, 30, 120);
        thermometerText.text = Math.Round(temperature, 0).ToString(CultureInfo.InvariantCulture) + " C";
    }
}
