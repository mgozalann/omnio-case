using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitObjectPool : MonoBehaviour
{
    [SerializeField] private BaseUnit[] _baseUnitPrefabs;
    [SerializeField] private int _initialPoolSize = 20;
    
    private Dictionary<Type, Queue<BaseUnit>> _objectPools = new Dictionary<System.Type, Queue<BaseUnit>>();
    
    private void Start()
    {
        foreach (BaseUnit prefab in _baseUnitPrefabs)
        {
            InitializePool(prefab);
        }
    }
    
    private void InitializePool(BaseUnit prefab)
    {

        Type prefabType = prefab.GetType();
        if (!_objectPools.ContainsKey(prefabType))
        {
            _objectPools[prefabType] = new Queue<BaseUnit>();

            for (int i = 0; i < _initialPoolSize; i++)
            {
                BaseUnit obj = Instantiate(prefab);
                obj.gameObject.SetActive(false);
                _objectPools[prefabType].Enqueue(obj);
            }
        }
    }
    
    public BaseUnit GetObjectFromPool(BaseUnit obj)
    {
        Type objectType = obj.GetType();

        if (_objectPools.ContainsKey(objectType))
        {
            if (_objectPools[objectType].Count > 0)
            {
                return _objectPools[objectType].Dequeue();
            }

            BaseUnit newObj = Instantiate(obj);
            return newObj;
        }

        Debug.LogWarning("Object pool does not contain objects of type " + objectType); //count 0 olunca yeni üretsin
        return null;

    }
    
    
    public void ReturnObjectToPool(BaseUnit obj)
    {
        Type objectType = obj.GetType();
        if (_objectPools.ContainsKey(objectType))
        {
            _objectPools[objectType].Enqueue(obj);
            obj.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Object pool does not contain objects of type " + objectType);
        }
    }
}