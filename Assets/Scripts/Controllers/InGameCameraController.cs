using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCameraController : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.GameState.OnStartGame += TriggerStartCamera;
        GameEvents.GameState.OnEndGame += TriggerStopCamera;
        this.GetComponent<PivotCameraMovement>().enabled = false;
    }

    private void OnDestroy()
    {
        GameEvents.GameState.OnStartGame -= TriggerStartCamera;
        GameEvents.GameState.OnEndGame -= TriggerStopCamera;
    }

    void TriggerStartCamera()
    {
        this.GetComponent<PivotCameraMovement>().enabled = true;
    }

    void TriggerStopCamera()
    {
        this.GetComponent<PivotCameraMovement>().enabled = false;
        this.GetComponent<Camera>().transform.rotation = Quaternion.identity;
    }
}
