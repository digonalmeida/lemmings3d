using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyLemmingAnimationController : MonoBehaviour
{
    //References
    private Animator lemmingAnimator;
    [SerializeField]
    private LNetworkLobbyPlayer lobbyPlayer;

    //Control Variables
    public float changeAnimationInterval = 6f;
    private float timerChangeAnimation;
    public Player playerNum;
    

    // Use this for initialization
    void Start ()
    {
        lemmingAnimator = GetComponent<Animator>();
        timerChangeAnimation = changeAnimationInterval;
    }

    //Reset Bools
    private void resetBools()
    {
        lemmingAnimator.SetBool("Climbing", false);
        lemmingAnimator.SetBool("Walking", false);
        lemmingAnimator.SetBool("Falling", false);
        lemmingAnimator.SetBool("Blocking", false);
    }

    // Update is called once per frame
    void Update ()
    {
        //Verify if there is a player connected
        if(lobbyPlayer == null)
        {
            if (playerNum == Player.Player1 && LNetworkLobbyPlayer.Player1Instance != null) lobbyPlayer = LNetworkLobbyPlayer.Player1Instance;
            else if (playerNum == Player.Player2 && LNetworkLobbyPlayer.Player2Instance != null) lobbyPlayer = LNetworkLobbyPlayer.Player2Instance;
        }

        //If there is a player & animation has ended => change animation
        timerChangeAnimation -= Time.deltaTime;
        if (lobbyPlayer != null && timerChangeAnimation <= 0f)
        {
            resetBools();
            timerChangeAnimation = changeAnimationInterval;

            float random = Random.Range(0, 6);
            if (random == 2) lemmingAnimator.SetBool("Climbing", true);
            else if (random == 3) lemmingAnimator.SetBool("Walking", true);
            else if (random == 4) lemmingAnimator.SetBool("Falling", true);
            else if (random == 5) lemmingAnimator.SetBool("Blocking", true);
        }
    }
}
