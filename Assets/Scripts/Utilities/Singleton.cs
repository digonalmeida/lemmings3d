using UnityEngine;

public class Singleton : MonoBehaviour
{

    public static Singleton Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this); //DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

}