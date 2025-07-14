using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource soundtrackAudioSource;
    public AudioSource sfxAudioSource;
    public static SoundManager global;
    [Header("SFX")]
    public AudioClip[] errorSounds;

    [Header("Soundtracks")]
    public AudioClip[] soundtracks;
    private int currentSoundtrackIndex = 0;

    private void Start()
    {
        if (global == null)
        {
            global = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(PlaySoundtracksSequentially());
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator PlaySoundtracksSequentially()
    {
        while (soundtracks != null && soundtracks.Length > 0)
        {
            soundtrackAudioSource.clip = soundtracks[currentSoundtrackIndex];
            soundtrackAudioSource.Play();
            yield return new WaitForSeconds(soundtrackAudioSource.clip.length + 1f);
            currentSoundtrackIndex = (currentSoundtrackIndex + 1) % soundtracks.Length;
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