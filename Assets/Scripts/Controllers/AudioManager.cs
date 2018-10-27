using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    private Coroutine volumeTransitionCoroutine;
    private float globalVolume = 1f;

    [Header("SFX Audios")]
    public AudioClip selectSkill;
    public AudioClip giveSkill;

    private void OnEnable()
    {
        GameEvents.UI.SelectedSkill += () => sfxAudioSource.PlayOneShot(selectSkill);
        GameEvents.Lemmings.LemmingUsedSkill += () => sfxAudioSource.PlayOneShot(giveSkill);
    }

    private void OnDisable()
    {
        GameEvents.UI.SelectedSkill -= () => sfxAudioSource.PlayOneShot(selectSkill);
        GameEvents.Lemmings.LemmingUsedSkill -= () => sfxAudioSource.PlayOneShot(giveSkill);
    }

    private void ChangeVolume(float newVolume)
    {
        globalVolume = newVolume;
        bgmAudioSource.volume = globalVolume;
        sfxAudioSource.volume = globalVolume;
    }

    private void StartBGM(AudioClip bgm) {
        bgmAudioSource.clip = bgm;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void Mute()
    {
        if(volumeTransitionCoroutine!=null) StopCoroutine(volumeTransitionCoroutine);
        volumeTransitionCoroutine = StartCoroutine(VolumeTransition(1f, 0f));
    }

    public void MaxVolume()
    {
        if (volumeTransitionCoroutine != null) StopCoroutine(volumeTransitionCoroutine);
        volumeTransitionCoroutine = StartCoroutine(VolumeTransition(1f, 1f));
    }

    private IEnumerator VolumeTransition(float duration, float finalVolume)
    {
        float initialValue = globalVolume;
        float timer = 0;
        while (timer <= duration)
        {
            ChangeVolume(timer / duration * (finalVolume - initialValue) + initialValue);
            timer += Time.deltaTime;
            yield return null;
        }
        globalVolume = finalVolume;
        
    }



}
