using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            instance = (FindObjectOfType(typeof(T)) as T);

            if (instance == null)
            {
                instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    private void OnApplicationQuit()
    {
        instance = null;
    }
}
