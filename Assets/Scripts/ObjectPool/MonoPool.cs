using System.Collections.Generic;
using UnityEngine;

public class MonoPool<TObject> : ObjectPool<TObject> where TObject : MonoBehaviour, IPoolable
{
    protected TObject _prefab;
    protected Transform _parent;

    public TObject Prefab => _prefab;

    public MonoPool(TObject prefab, int capacity, Transform poolParent = null)
    {
        _prefab = prefab;
        _parent = new GameObject(prefab.name + " pool").transform;

        if (poolParent != null)
        {
            _parent.transform.parent = poolParent;
        }

        _objects = new List<TObject>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            CreateObject();
        }
    }

    protected override void CreateObject()
    {
        TObject obj = Object.Instantiate(_prefab, _parent);

        obj.gameObject.SetActive(false);

        _objects.Add(obj);
    }

    public override void Release(TObject obj)
    {
        obj.gameObject.SetActive(false);

        base.Release(obj);
    }

    public override TObject Pull()
    {
        TObject obj = base.Pull();

        obj.gameObject.SetActive(true);

        return obj;
    }

    /// <summary>
    /// Pull object without enabling
    /// </summary>
    /// <returns>Return last object of pool</returns>
    public TObject PullDisabled()
    {
        return base.Pull();
    }

    public override List<TObject> PullObjects(int count)
    {
        List<TObject> objects = base.PullObjects(count);

        foreach (TObject obj in objects)
        {
            obj.gameObject.SetActive(true);
        }

        return objects;
    }

    /// <summary>
    /// Pull X objects from pool without enabling
    /// </summary>
    /// <param name="count">Object count need to pull</param>
    /// <returns>Return X last objects of pool</returns>
    public List<TObject> PullObjectsDisabled(int count)
    {
        return base.PullObjects(count);
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
