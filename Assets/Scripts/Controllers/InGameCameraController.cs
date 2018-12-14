using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCameraController : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.GameState.OnStartGame += TriggerStartCamera;
        GameEvents.GameState.OnEndGame += TriggerStopCamera;
        this.GetComponent<PivotCameraMovement>().movementLock = true;
    }

    private void OnDestroy()
    {
        GameEvents.GameState.OnStartGame -= TriggerStartCamera;
        GameEvents.GameState.OnEndGame -= TriggerStopCamera;
    }

    void TriggerStartCamera()
    {
        this.GetComponent<PivotCameraMovement>().movementLock = false;
    }

    void TriggerStopCamera()
    {
        this.GetComponent<PivotCameraMovement>().movementLock = true;
        this.GetComponent<PivotCameraMovement>().unlockMouse();
        this.GetComponent<Camera>().transform.rotation = Quaternion.identity;
    }
}
