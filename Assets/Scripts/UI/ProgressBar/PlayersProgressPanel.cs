using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersProgressPanel : MonoBehaviour
{

    public PlayerProgressBar player1bar;
    public PlayerProgressBar player2bar;
    private Animator animator;

    void OnEnable()
    {
        GameEvents.UI.OpenInGameUI += Open;
        GameEvents.UI.CloseInGameUI += Close;
		GameEvents.GameState.OnLoadGame += Setup;
    }


    void OnDisable()
    {
        GameEvents.UI.OpenInGameUI -= Open;
        GameEvents.UI.CloseInGameUI -= Close;
		GameEvents.GameState.OnLoadGame -= Setup;
    }


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetupBars(int maxLemmings, int minLemmings)
    {
        player1bar.Clear();
        player2bar.Clear();
        player1bar.Initialize(maxLemmings, minLemmings);
        player2bar.Initialize(maxLemmings, minLemmings);
    }

	public void Setup(){
        SetupBars(LevelController.Instance.CurrentMapSettings.LemmingsCount,LevelController.Instance.CurrentMapSettings.MinimumVictoryCount);
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
