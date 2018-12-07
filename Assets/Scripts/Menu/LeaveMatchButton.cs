using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveMatchButton : MonoBehaviour
{
    public void leaveMatch()
    {
        LNetworkLobbyManager.singleton.GetComponent<LNetworkLobbyManager>().StopClient();
    }
}
