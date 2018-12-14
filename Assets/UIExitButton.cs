using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExitButton : MonoBehaviour
{

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

    void Open()
    {
        animator.SetBool("opened", true);
    }

    void Close()
    {
        animator.SetBool("opened", false);
    }
}
