using UnityEngine;
using Zenject;

public class FactoryMonoPool<TObject, TFactory> : MonoPool<TObject> where TObject : MonoBehaviour, IPoolable
                                                                    where TFactory : PlaceholderFactory<TObject>
{
    private TFactory _factory;

    public FactoryMonoPool(TObject prefab, TFactory factory, int capacity, Transform poolParent = null) : base(prefab, capacity, poolParent) 
    {
        _factory = factory;

        for (int i = 0; i < capacity; i++)
        {
            CreateObject();
        }
    } 

    protected override void CreateObject()
    {
        if (_factory == null)
        {
            return;
        }

        TObject obj = _factory.Create();

        obj.transform.parent = _parent;

        obj.gameObject.SetActive(false);

        _objects.Add(obj);
    }
}
