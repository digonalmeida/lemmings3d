using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class AutoSpawn : NetworkBehaviour {

    void Awake()
    {
        if(NetworkServer.active)
        {
            NetworkServer.Spawn(gameObject);
        }
    }

	// Use this for initialization
	void Start ()
    {
		if(!isServer)
        {
            Destroy(gameObject);
        }
	}
}
