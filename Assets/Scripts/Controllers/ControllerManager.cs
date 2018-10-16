using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    //Singleton Instance Variable
    private static ControllerManager instance;
    public static ControllerManager Instance
    {
        get
        {
            return instance;
        }
    }

    //On Object Awake
    private void Awake()
    {
        //Check Singleton
        if (instance != null && instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            instance = this;
            setReferences();
        }

        skillController = GetComponent<SkillsController>();
        levelController = GetComponent<LevelController>();
        mapController = GetComponentInChildren<LevelMap.MapController>();
    }

    //On Object Destroy (Safeguard)
    public void OnDestroy()
    {
        instance = null;
    }

    //References
    public SkillsController skillController { get; private set; }
    public LevelController levelController { get; private set; }
    public LevelMap.MapController mapController { get; private set; }

    //Start
    private void setReferences()
    {
        skillController = GetComponent<SkillsController>();
        levelController = GetComponent<LevelController>();
        mapController = GetComponentInChildren<LevelMap.MapController>();
    }
}
