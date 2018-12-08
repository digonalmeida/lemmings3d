using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class AutoSpawn : NetworkBehaviour 
{
	public void Start()
	{
		if(NetworkServer.active)
		{
			Debug.Log("spawning");
			NetworkServer.Spawn(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
