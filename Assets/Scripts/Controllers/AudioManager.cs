using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    //Control Variables
    public float audioFadeOutFactor = 0.15f;
    [SerializeField]
    private AudioSource bgmAudioSource;
    [SerializeField]
    private AudioSource sfxAudioSource;

    [Header("BGM")]
    public AudioClip levelSelectBGM;
    public AudioClip inGameBGM;
    public AudioClip scorePanelBGM;

    [Header("SFX Audios")]
    public AudioClip selectSkill;
    public AudioClip deselectSkill;
    public AudioClip giveSkill;
    public AudioList lemmingDie;

    //Play SFX Methods
    private void PlaySFX_SelectSkill() {sfxAudioSource.PlayOneShot(selectSkill);}
    private void PlaySFX_DeselectSkill() {sfxAudioSource.PlayOneShot(deselectSkill);}
    private void PlaySFX_GiveSkill(LemmingStateController lemming) {sfxAudioSource.PlayOneShot(giveSkill);}
    private void PlaySFX_LemmingDie(LemmingStateController lemming) {sfxAudioSource.PlayOneShot(lemmingDie.GetUniqueRandom());}

    //Play BGM Methods
    private void PlayBGM_inGameBGM() { playMusic(inGameBGM); }
    private void PlayBGM_ScorePanelBGM() { playMusic(scorePanelBGM); }

    private void OnEnable()
    {
        GameEvents.UI.SelectedSkill += PlaySFX_SelectSkill;
        GameEvents.UI.DeselectedSkill += PlaySFX_DeselectSkill;
        GameEvents.Lemmings.LemmingUsedSkill += PlaySFX_GiveSkill;
        GameEvents.Lemmings.LemmingDied += PlaySFX_LemmingDie;
        GameEvents.GameState.OnStartGame += PlayBGM_inGameBGM;
        GameEvents.GameState.OnEndGame += PlayBGM_ScorePanelBGM;
        playMusic(levelSelectBGM);
    }

    private void OnDisable()
    {
        GameEvents.UI.SelectedSkill -= PlaySFX_SelectSkill;
        GameEvents.UI.DeselectedSkill -= PlaySFX_DeselectSkill;
        GameEvents.Lemmings.LemmingUsedSkill -= PlaySFX_GiveSkill;
        GameEvents.Lemmings.LemmingDied -= PlaySFX_LemmingDie;
        GameEvents.GameState.OnStartGame -= PlayBGM_inGameBGM;
        GameEvents.GameState.OnEndGame -= PlayBGM_ScorePanelBGM;
    }

    //Play Music Main Method
    public void playMusic(AudioClip musicClip)
    {
        if (bgmAudioSource.clip == null)
        {
            bgmAudioSource.clip = musicClip;
            bgmAudioSource.Play();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(changeMusicWithFade(musicClip));
        }
    }

    //Change Music with Fade
    private IEnumerator changeMusicWithFade(AudioClip musicClip)
    {
        while (bgmAudioSource.volume > 0f)
        {
            bgmAudioSource.volume -= audioFadeOutFactor * Time.deltaTime;
            yield return 0;
        }

        //Change Music
        bgmAudioSource.Stop();
        bgmAudioSource.clip = musicClip;
        bgmAudioSource.Play();

        while (bgmAudioSource.volume < 1f)
        {
            bgmAudioSource.volume += audioFadeOutFactor * Time.deltaTime;
            yield return 0;
        }
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
