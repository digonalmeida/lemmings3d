using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NewtorkInfoManager : MonoBehaviour 
{
	[SerializeField]
	private float updateInterval = 0.4f;

	private NetworkInfo info = new NetworkInfo();
	private Coroutine coroutine = null;
	

	
	private void OnEnable()
	{
		info.Clear();
		GameEvents.Networking.NetworkInfoEnabled.SafeInvoke(info);

		if (coroutine != null)
		{
			StopCoroutine(coroutine);
		}
		
		coroutine = StartCoroutine(UpdateCoroutine());
	}

	private void OnDisable()
	{
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
		}

		GameEvents.Networking.NetworkInfoDisabled.SafeInvoke();
	}

	

	private void UpdateInfo()
	{
		if (!NetworkClient.active)
		{
			return;
		}
		
		NetworkClient.GetTotalConnectionStats();
		info.ping = NetworkManager.singleton.client.GetRTT();
		GameEvents.Networking.NetworkInfoUpdated.SafeInvoke(info);
	}

	public IEnumerator UpdateCoroutine()
	{
		var wait = new WaitForSeconds(updateInterval);
		for (;;)
		{
			UpdateInfo();
			yield return wait;
		}
	}
}

public class NetworkInfo
{
	public int ping;

	public void Clear()
	{
		ping = 0;
	}
}