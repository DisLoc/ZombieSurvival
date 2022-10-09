using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static ExpCrystal;

public class FactoryPool<TObject, TFactory> : MonoPool<TObject> where TObject : MonoBehaviour, IPoolable
                                                                where TFactory : PlaceholderFactory<TObject>
{
    private TFactory _factory;

    public FactoryPool(TObject prefab, TFactory factory, int capacity) : base(prefab, capacity) 
    {
        _factory = factory;
    } 

    protected override void CreateObject()
    {
        TObject obj = _factory.Create();

        obj.transform.parent = _parent;

        obj.gameObject.SetActive(false);

        _objects.Add(obj);
    }
}
