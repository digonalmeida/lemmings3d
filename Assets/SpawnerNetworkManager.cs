using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class SpawnerNetworkManager : NetworkBehaviour 
{
	Spawner spawner;
	public void Awake()
	{
		spawner = GetComponent<Spawner>();
		spawner.enabled = false;
	}

	public void Start()
	{
		if(!NetworkServer.active && !NetworkClient.active)
		{
			Destroy(gameObject);
		}
		else if(isLocalPlayer)
		{
			NetworkServer.Spawn(gameObject);
		}
		
	}

	public void Update()
	{
		if(!isServer)
		{
			//Destroy(gameObject);
			return;
		}

		spawner.enabled = true;
	}
}
