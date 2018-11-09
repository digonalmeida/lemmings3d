using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    private Coroutine volumeTransitionCoroutine;
    private float globalVolume = 1f;
    private float currentVolume = 0f;

    [Header("BGM")]
    public AudioClip mainBGM;

    [Header("SFX Audios")]
    public AudioClip selectSkill;
    public AudioClip giveSkill;
    public AudioList lemmingDie;

    private void OnEnable()
    {
        GameEvents.GameState.OnStartGame += () => StartBGM(mainBGM);
        GameEvents.GameState.OnEndGame += StopBGM;
        GameEvents.UI.SelectedSkill += () => sfxAudioSource.PlayOneShot(selectSkill);
        GameEvents.Lemmings.LemmingUsedSkill += () => sfxAudioSource.PlayOneShot(giveSkill);
        GameEvents.Lemmings.LemmingDied += () => sfxAudioSource.PlayOneShot(lemmingDie.GetUniqueRandom());

    }

    private void OnDisable()
    {
        GameEvents.GameState.OnStartGame -= () => StartBGM(mainBGM);
        GameEvents.GameState.OnEndGame -= StopBGM;
        GameEvents.UI.SelectedSkill -= () => sfxAudioSource.PlayOneShot(selectSkill);
        GameEvents.Lemmings.LemmingUsedSkill -= () => sfxAudioSource.PlayOneShot(giveSkill);
        GameEvents.Lemmings.LemmingDied -= () => sfxAudioSource.PlayOneShot(lemmingDie.GetUniqueRandom());
    }

    private void ChangeGlobalVolume(float newVolume, bool updateAudioSources)
    {
        globalVolume = newVolume;
        if(updateAudioSources) ChangeAudioSourcesVolume(globalVolume);
    }

    private void ChangeAudioSourcesVolume(float newVolume)
    {
        currentVolume = newVolume;
        bgmAudioSource.volume = currentVolume;
        sfxAudioSource.volume = currentVolume;
    }

    private void StartBGM(AudioClip bgm) {

        // set bgm
        bgmAudioSource.clip = bgm;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();

        // transition volume from zero to current
        ChangeAudioSourcesVolume(0f);
        MaxVolume(null);
    }

    private void StopBGM() {
        Mute(()=> {
            bgmAudioSource.clip = null;
            bgmAudioSource.Stop();
        });
    }

    public void Mute(Action callback)
    {
        if(volumeTransitionCoroutine!=null) StopCoroutine(volumeTransitionCoroutine);
        volumeTransitionCoroutine = StartCoroutine(VolumeTransition(1f, 0f, callback));
    }

    private void MaxVolume(Action callback) {
        if (volumeTransitionCoroutine != null) StopCoroutine(volumeTransitionCoroutine);
        volumeTransitionCoroutine = StartCoroutine(VolumeTransition(1f, globalVolume, callback));
    }

    private IEnumerator VolumeTransition(float duration, float finalVolume, Action callback)
    {
        float initialValue = currentVolume;
        float timer = 0;
        while (timer <= duration)
        {
            ChangeAudioSourcesVolume(timer / duration * (finalVolume - initialValue) + initialValue);
            timer += Time.deltaTime;
            yield return null;
        }
        ChangeAudioSourcesVolume(finalVolume);

        if (callback != null) callback.Invoke();
        
    }



}
