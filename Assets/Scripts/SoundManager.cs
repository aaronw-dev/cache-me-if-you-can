using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource soundtrackAudioSource;
    public AudioSource sfxAudioSource;
    public static SoundManager global;
    [Header("SFX")]
    public AudioClip[] errorSounds;
    private void Start()
    {
        if (global == null)
        {
            global = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, float volume = 0.5f)
    {
        sfxAudioSource.PlayOneShot(clip, volume);
    }
    public void Error()
    {
        AudioClip clip = errorSounds[Random.Range(0, errorSounds.Length)];
        PlaySound(clip, 0.5f);
    }
}