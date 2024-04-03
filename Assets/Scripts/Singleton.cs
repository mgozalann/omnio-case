using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                _instance = FindObjectOfType<T>();
                if(_instance == null)
                {
                    Debug.Log($"Error couldn't find gameObject of type {nameof(T)} ");
                }
            }
            return _instance;
        }
    }


    protected virtual void Awake()
    {
        _instance = this as T; 
    }
}