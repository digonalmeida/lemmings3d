using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDockInfo_Timer : UIDockInfo
{

    public Text timeText;
    public float time;

    private int minutes, seconds;
    private Animator animator;

    void OnEnable()
    {
        GameEvents.UI.OpenInGameUI += Open;
        GameEvents.UI.CloseInGameUI += Close;
    }

    void OnDisable()
    {
        GameEvents.UI.OpenInGameUI -= Open;
        GameEvents.UI.CloseInGameUI -= Close;
    }


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //TODO
    private void Update()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        if (LevelController.Instance.gameStateManager == null) return;

        time = LevelController.Instance.remainingTime;
        minutes = ((int)time / 60);
        seconds = (int)(time % 60);
        timeText.text = "";

        if (minutes > 0)
        {
            timeText.text += minutes.ToString() + "m";
        }
        timeText.text += seconds.ToString() + "s";

    }


    void Open()
    {
        animator.SetBool("opened", true);
    }

    void Close()
    {
        animator.SetBool("opened", false);
    }
}
