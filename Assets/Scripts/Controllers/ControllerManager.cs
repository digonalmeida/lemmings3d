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
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //On Object Destroy (Safeguard)
    public void OnDestroy()
    {
        instance = null;
    }

    //References
    public SkillsController skillController;
    public LevelController levelController;
    public LevelMap.MapController mapController;

    //Start
    private void Start()
    {
        skillController = GetComponent<SkillsController>();
        levelController = GetComponent<LevelController>();
        mapController = GetComponentInChildren<LevelMap.MapController>();
    }
}
