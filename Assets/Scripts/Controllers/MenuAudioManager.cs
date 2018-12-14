using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : Singleton<MenuAudioManager>
{
    //Control Variables
    public float audioFadeOutFactor = 0.25f;
    [SerializeField]
    private AudioSource bgmAudioSource;
    [SerializeField]
    private AudioSource sfxAudioSource;

    [Header("BGM")]
    public AudioClip menuBGM;

    [Header("SFX")]
    public AudioClip menuSelect;
    public AudioClip menuBack;

    //Start
    private void Start()
    {
        bgmAudioSource.clip = menuBGM;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void playMenuSelectSFX()
    {
        sfxAudioSource.PlayOneShot(menuSelect);
    }

    public void playMenuBackSFX()
    {
        sfxAudioSource.PlayOneShot(menuBack);
    }

    //Stop Music with Fade
    public void stopMusic()
    {
        StopAllCoroutines();
        StartCoroutine(fadeOutMusic());
    }

    private IEnumerator fadeOutMusic()
    {
        while (bgmAudioSource.volume > 0f)
        {
            bgmAudioSource.volume -= audioFadeOutFactor * Time.deltaTime;
            yield return 0;
        }

        bgmAudioSource.Stop();
    }
}
