using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{

    public static T Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this.gameObject); //DestroyImmediate(gameObject);
            return;
        }

        Instance = this.GetComponent<T>();
        DontDestroyOnLoad(this.gameObject);
    }

}