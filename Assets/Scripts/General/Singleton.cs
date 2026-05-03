using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance => instance;
    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this as T;
        }
    }

    protected virtual void OnApplicationQuit()
    {
        instance = null; 
        Destroy(gameObject);
    }

    public abstract class PersistentSingleton<TSingleton> : Singleton<TSingleton> where TSingleton : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject); // Make the singleton persist across scenes
        }
    }

}
