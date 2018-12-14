using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExitButton : MonoBehaviour
{

    private Animator animator;
    public GameObject exitPanel;

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

    public void OpenConfirmPanel()
    {
        exitPanel.SetActive(true);
    }

    private void CloseConfirmPanel()
    {
        exitPanel.SetActive(false);
    }

    public void ConfirmQuit()
    {
		LNetworkLobbyManager.singleton.StopClient();
        CloseConfirmPanel();
    }

    public void CancelQuit()
    {
        CloseConfirmPanel();
    }
}
