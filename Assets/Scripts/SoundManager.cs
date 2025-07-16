using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
public enum VolumeType
{
    MUSIC,
    SFX,
    ALL
}
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

    [ReadOnly]
    public float masterVolume, musicVolume, sfxVolume;
    public SlidePotentiometer masterVolumeSlider, musicVolumeSlider, sfxVolumeSlider;
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
        masterVolume = PlayerPrefsManager.global.InitFloat("MASTERVOLUME", 1f);
        musicVolume = PlayerPrefsManager.global.InitFloat("MUSICVOLUME", 0.1f);
        sfxVolume = PlayerPrefsManager.global.InitFloat("SFXVOLUME", 0.5f);

        masterVolumeSlider.SetSlider(masterVolume);
        musicVolumeSlider.SetSlider(musicVolume);
        sfxVolumeSlider.SetSlider(sfxVolume);

        UpdateVolume(VolumeType.ALL);
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

    public void SetMasterVolume(float volume)
    {
        masterVolume = PlayerPrefsManager.global.SetFloat("MASTERVOLUME", volume);
        UpdateVolume(VolumeType.ALL);
    }
    public void SetMusicVolume(float volume)
    {
        musicVolume = PlayerPrefsManager.global.SetFloat("MUSICVOLUME", volume);
        UpdateVolume(VolumeType.MUSIC);
    }
    public void SetSFXVolume(float volume)
    {
        sfxVolume = PlayerPrefsManager.global.SetFloat("SFXVOLUME", volume);
        UpdateVolume(VolumeType.SFX);
    }
    public void UpdateVolume(VolumeType type)
    {
        switch (type)
        {
            case VolumeType.MUSIC:
                {
                    soundtrackAudioSource.volume = musicVolume * masterVolume * 0.2f;
                    break;
                }
            case VolumeType.SFX:
                {
                    sfxAudioSource.volume = sfxVolume * masterVolume;
                    break;
                }
            default:
                {
                    UpdateVolume(VolumeType.MUSIC);
                    UpdateVolume(VolumeType.SFX);
                    break;
                }
        }
    }

    public void PlaySound(AudioClip clip, float volume = 0.5f)
    {
        sfxAudioSource.PlayOneShot(clip, volume);
    }
    public void Error()
    {
        AudioClip clip = errorSounds[UnityEngine.Random.Range(0, errorSounds.Length)];
        PlaySound(clip, 0.5f);
    }
}