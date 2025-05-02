using UnityEngine;

public class PersistentGameEvent: MonoBehaviour
{
    private static PersistentGameEvent instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
