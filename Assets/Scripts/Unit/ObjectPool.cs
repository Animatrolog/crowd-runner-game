using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab { get; }
    private bool _autoExpand { get; set; }
    private Transform _parent { get; }
    private List<T> _pool;

    public ObjectPool(T prefab,int amount, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;
        _prefab.gameObject.SetActive(false);
        CreatePool(amount);
    }

    private void CreatePool(int amount)
    {
        _pool = new();

        for (int i = 0; i < amount; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        T createdObject = Object.Instantiate(_prefab, _parent);
        _pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeObject(out T freeObject)
    {
        foreach (T poolObject in _pool)
        {
            if (!poolObject.isActiveAndEnabled)
            {
                freeObject = poolObject;
                //poolObject.gameObject.SetActive(true);
                return true;
            }
        }

        freeObject = null;
        return false;
    }

    public T GetFreeObject()
    {
        if (HasFreeObject(out T poolObject))
            return poolObject;

        if (_autoExpand)
            return CreateObject(true);

        throw new System.Exception($"There is no free object in pool of type {typeof(T)}");
    }
}
