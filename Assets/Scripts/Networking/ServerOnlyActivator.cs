using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class ServerOnlyActivator : NetworkBehaviour
{
    [SerializeField]
    private List<MonoBehaviour> serverOnlyComponents = new List<MonoBehaviour>();

    private void Start()
    {
        SetEnabled(isServer);
    }

    private void SetEnabled(bool enable)
    {
        for(int i = 0; i < serverOnlyComponents.Count; i++)
        {
            if(serverOnlyComponents[i] == null)
            {
                continue;
            }
            serverOnlyComponents[i].enabled = enable;
        }
    }
}
