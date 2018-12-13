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

    [Header("BGM")]
    public AudioClip menuBGM;

    //Start
    private void Start()
    {
        bgmAudioSource.clip = menuBGM;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
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
