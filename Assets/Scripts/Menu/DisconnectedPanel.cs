using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconnectedPanel : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void okButton()
    {
        Destroy(this.gameObject);
    }
}
