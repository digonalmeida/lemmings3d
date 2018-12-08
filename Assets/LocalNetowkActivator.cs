using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class LocalNetowkActivator : NetworkBehaviour 
{
	public List<MonoBehaviour> localComopnents;

	public void SetEnabled(bool enable)
	{
		foreach(var component in localComopnents)
		{
			component.enabled = enable;
		}
	}

	public void Update()
	{
		SetEnabled(isServer);
	}

}
