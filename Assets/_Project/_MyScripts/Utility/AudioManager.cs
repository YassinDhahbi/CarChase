using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public List<AudioClip> musicClips;
    public List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> musicClipDict;
    private Dictionary<string, AudioClip> sfxClipDict;

    public float musicVolume = 1.0f;
    public float sfxVolume = 1.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeClipDictionaries();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeClipDictionaries()
    {
        musicClipDict = new Dictionary<string, AudioClip>();
        foreach (var clip in musicClips)
        {
            musicClipDict[clip.name] = clip;
        }

        sfxClipDict = new Dictionary<string, AudioClip>();
        foreach (var clip in sfxClips)
        {
            sfxClipDict[clip.name] = clip;
        }
    }

    private void Start()
    {
        PlayMusic("DefaultTrack");
    }

    public void PlayMusic(string clipName)
    {
        if (musicClipDict.TryGetValue(clipName, out var clip))
        {
            musicSource.clip = clip;
            musicSource.volume = musicVolume;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music clip '{clipName}' not found.");
        }
    }

    public void PlaySFX(string clipName)
    {
        if (sfxClipDict.TryGetValue(clipName, out var clip))
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
        else
        {
            Debug.LogWarning($"SFX clip '{clipName}' not found.");
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }
}
