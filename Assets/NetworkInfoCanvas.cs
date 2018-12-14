using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkInfoCanvas : Singleton<NetworkInfoCanvas> 
{
    [SerializeField]
    private Text pingInfoText;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        OnHideInfo();
        GameEvents.Networking.NetworkInfoEnabled += OnShowInfo;
        GameEvents.Networking.NetworkInfoDisabled += OnHideInfo;
        GameEvents.Networking.NetworkInfoUpdated += OnUpdateInfo;
    }

    private void OnDestroy()
    {
        GameEvents.Networking.NetworkInfoEnabled -= OnShowInfo;
        GameEvents.Networking.NetworkInfoDisabled -= OnHideInfo;
        GameEvents.Networking.NetworkInfoUpdated -= OnUpdateInfo;
    }


    private void OnShowInfo(NetworkInfo info)
    {
        if (pingInfoText == null)
        {
            return;
        }

        pingInfoText.gameObject.SetActive(true);
        OnUpdateInfo(info);
    }

    private void OnHideInfo()
    {
        if (pingInfoText == null)
        {
            return;
        }
        
        pingInfoText.gameObject.SetActive(false);
    }

    private void OnUpdateInfo(NetworkInfo info)
    {
        if (pingInfoText == null)
        {
            return;
        }

        pingInfoText.text = string.Format("ping: {0}ms", info.ping);
    }
}
