using UnityEngine;

public class SingletonDontDestroy<T> : MonoBehaviour where T : Component
{

    public static T Instance
    {
        get; private set;
    }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}