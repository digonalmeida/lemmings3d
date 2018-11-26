using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanelManager : MonoBehaviour
{
    //References
    public GameObject player1Lemming;
    public GameObject player2Lemming;
    public Text player1Text;
    public Text player2Text;

    //Singleton
    private static LobbyPanelManager instance;
    public static LobbyPanelManager Instance
    {
        get
        {
            return instance;
        }
    }

    //On Object Awake
    private void Awake()
    {
        //Check Singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    //On Object Destroy (Safeguard)
    public void OnDestroy()
    {
        instance = null;
    }
}
