using System.Collections.Generic;
using UnityEngine;

public class UnitObjectPool : MonoBehaviour
{
    [SerializeField] private Unit _prefab; 
    [SerializeField] private int _poolSize = 10; 

    private Queue<Unit> _pool = new Queue<Unit>();

    void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Unit obj = Instantiate(_prefab, transform);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public Unit GetObject()
    {
        if (_pool.Count > 0)
        {
            Unit obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            Unit obj = Instantiate(_prefab, transform);
            return obj;
        }
    }

    public void ReturnObject(Unit obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.parent = null;
        _pool.Enqueue(obj);
    }
}