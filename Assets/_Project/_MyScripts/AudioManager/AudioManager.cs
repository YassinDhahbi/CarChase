using UnityEngine;

public class AudioManager : Singelton<AudioManager>
{
    [SerializeField] private AudioSource _sfxPlayer;
    [SerializeField] private AudioSource _musicPlayer;
    [Header("Test")]
    [SerializeField] private float _testVolume;
    [SerializeField] private bool _isMuted;
    [ContextMenu("Test")]
    void Test()
    {
        SetMusicVolume(_testVolume);
        SetSFXVolume(_testVolume);
    }

    [ContextMenu("Mute All")]
    public void Mute() => MuteAll(_isMuted);

    public void MuteAll(bool isEverythingMuted)
    {
        var allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.mute = isEverythingMuted;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _sfxPlayer.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        _musicPlayer.Play();
    }

    public void StopMusic(bool isStopped)
    {
        if (isStopped)
        {
            _musicPlayer.Stop();
        }
        else
        {
            _musicPlayer.Play();
        }
    }
    public void StopSFX(bool isStopped)
    {
        if (isStopped)
        {
            _sfxPlayer.Stop();
        }
        else
        {
            _sfxPlayer.Play();
        }
    }

    public void SetSFXVolume(float volume)
    {
        _sfxPlayer.volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        _musicPlayer.volume = volume;
    }

    public AudioSource GetSFXPlayer()
    {
        return _sfxPlayer;
    }


}
