using System.Collections.Generic;
using UnityEngine;

public class MonoPool<TObject> : ObjectPool<TObject> where TObject : MonoBehaviour, IPoolable
{
    private readonly TObject _prefab;
    private readonly Transform _parent;

    public MonoPool(TObject prefab, int capacity)
    {
        _prefab = prefab;
        _parent = new GameObject(prefab.name + " pool").transform;

        _objects = new List<TObject>(capacity);
        for (int i = 0; i < capacity; i++) CreateObject();
    }

    protected override void CreateObject()
    {
        TObject obj = Object.Instantiate(_prefab, _parent);
        
        _objects.Add(obj);
    }

    public override void Add(TObject obj)
    {
        obj.gameObject.SetActive(false);

        base.Add(obj);
    }

    public override TObject Pull()
    {
        TObject obj = base.Pull();

        obj.gameObject.SetActive(true);

        return obj;
    }

    public override void ClearPool()
    {
        foreach(TObject obj in _objects)
        {
            Object.Destroy(obj.gameObject);
        }

        base.ClearPool();
    }
}
