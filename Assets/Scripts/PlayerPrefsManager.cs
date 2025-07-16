using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static PlayerPrefsManager global;
    private void Awake()
    {
        if (global != null)
        {
            Destroy(gameObject);
        }
        global = this;
        DontDestroyOnLoad(gameObject);
    }
    public int InitInt(string key, int defaultValue)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, defaultValue);
            PlayerPrefs.Save();
        }
        return GetInt(key);
    }

    public float InitFloat(string key, float defaultValue)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetFloat(key, defaultValue);
            PlayerPrefs.Save();
        }
        return GetFloat(key);
    }

    public string InitString(string key, string defaultValue)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, defaultValue);
            PlayerPrefs.Save();
        }
        return GetString(key);
    }

    public int SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
        return value;
    }

    public int GetInt(string key, int fallback = 0)
    {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : fallback;
    }

    public float SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
        return value;
    }

    public float GetFloat(string key, float fallback = 0f)
    {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : fallback;
    }

    public string SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
        return value;
    }

    public string GetString(string key, string fallback = "")
    {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : fallback;
    }
}