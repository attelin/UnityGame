using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer _mixer;

    private AudioMixerGroup _musicGroup;
    private AudioMixerGroup _sfxGroup;

    private const string MASTER_VOLUME_NAME = "MasterVolume";
    private const string MUSIC_VOLUME_NAME = "MusicVolume";
    private const string SFX_VOLUME_NAME = "SFXVolume";

    // Mixer group names (NÄIDEN TÄYTYY VASTATA AUDIOMIXERIN NIMIÄ)
    private const string MUSIC_GROUP_NAME = "Music";
    private const string SFX_GROUP_NAME = "SFX";

    private void Init()
    {
        _musicGroup = _mixer.FindMatchingGroups(MUSIC_GROUP_NAME)[0];
        _sfxGroup = _mixer.FindMatchingGroups(SFX_GROUP_NAME)[0];
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Init();
    }

    public enum SoundType
    {
        SFX,
        Music
    }

    public void PlayAudio(AudioClip audioClip, SoundType soundType, float volume, bool loop)
    {
        GameObject go = new GameObject(audioClip.name + " Source");
        AudioSource audioSource = go.AddComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.playOnAwake = true;
        audioSource.loop = loop;
        audioSource.volume = volume;

        switch (soundType)
        {
            case SoundType.SFX:
                audioSource.outputAudioMixerGroup = _sfxGroup;
                break;
            case SoundType.Music:
                audioSource.outputAudioMixerGroup = _musicGroup;
                break;
        }

        audioSource.Play();

        if (!loop)
        {
            Destroy(go, audioClip.length);
        }
    }

    public void ChangeMasterVolume(float newVol)
    {
        _mixer.SetFloat(MASTER_VOLUME_NAME, Mathf.Log10(newVol) * 20);
    }

    public void ChangeMusicVolume(float newVol)
    {
        _mixer.SetFloat(MUSIC_VOLUME_NAME, Mathf.Log10(newVol) * 20);
    }

    public void ChangeSFXVolume(float newVol)
    {
        _mixer.SetFloat(SFX_VOLUME_NAME, Mathf.Log10(newVol) * 20);
    }
}
