﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : PersistentSingleton<SoundManager>
{
    public float BgmVolume
    {
        get
        {
            _bgmVolume = PlayerPrefs.GetFloat("Volume_Bgm", 0.5f);
            return _bgmVolume;
        }

        set
        {
            _bgmVolume = value;
            bgmSource.volume = value;
            PlayerPrefs.SetFloat("Volume_Bgm", value);
        }
    }

    public float SfxVolume
    {
        get
        {
            _sfxVolume = PlayerPrefs.GetFloat("Volume_Sfx", 0.5f);
            return _sfxVolume;
        }

        set
        {
            _sfxVolume = value;
            sfxSource.volume = value;
            PlayerPrefs.SetFloat("Volume_Sfx", value);
        }
    }

    public bool BgmMute
    {
        get
        {
            _isBgmMute = PlayerPrefs.GetInt("Mute_Bgm", 0) == 1;
            return _isBgmMute;
        }

        set
        {
            _isBgmMute = value;
            bgmSource.mute = value;
            PlayerPrefs.SetInt("Mute_Bgm", value ? 1 : 0);
        }
    }

    public bool SfxMute
    {
        get
        {
            _isSfxMute = PlayerPrefs.GetInt("Mute_Sfx", 0) == 1;
            return _isSfxMute;
        }

        set
        {
            _isSfxMute = value;
            sfxSource.mute = value;
            PlayerPrefs.SetInt("Mute_Sfx", value ? 1 : 0);
        }
    }

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public List<AudioClip> sound;

    private bool _isBgmMute = false;
    private bool _isSfxMute = false;

    private float _bgmVolume = 0.5f;
    private float _sfxVolume = 0.5f;

    private void Start()
    {
        TestFoodMaker.Instance.onFeverStart += new OnFeverStart(FeverStart);
        TestFoodMaker.Instance.onFeverEnd += new OnFeverEnd(FeverEnd);

        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
        }
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        bgmSource.volume = BgmVolume;
        sfxSource.volume = SfxVolume;
        bgmSource.mute = BgmMute;
        sfxSource.mute = SfxMute;

        bgmSource.clip = sound[0];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    void FeverStart()
    {
        bgmSource.pitch = 1.2f;
    }

    void FeverEnd()
    {
        bgmSource.pitch = 1.0f;
    }
    
    /*
     * n
     * 1 : 성공
     * 2 : 피버
     * 3 : 음배불러
     * 4 : 으아악
    */
    public void Play(int n)
    {
        sfxSource.clip = sound[n];
        sfxSource.Play();
    }

    public void PlayBgm(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void PlaySfxOneShot(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void StopBgm()
    {
        bgmSource.Stop();
    }
}