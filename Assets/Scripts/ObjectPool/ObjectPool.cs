using System.Collections.Generic;

public abstract class ObjectPool<TObject> where TObject : IPoolable
{
    protected List<TObject> _objects;
    public List<TObject> Objects => _objects;
    public bool IsEmpty => _objects.Count == 0;

    protected abstract void CreateObject();

    public virtual TObject Pull()
    {
        if (IsEmpty)
        {
            CreateObject();
        }

        TObject obj = _objects[_objects.Count - 1];
        _objects.RemoveAt(_objects.Count - 1);

        return obj;
    }

    public virtual List<TObject> PullObjects(int count)
    {
        List<TObject> objects = new List<TObject>();
        
        for (int i = 0; i < count; i++)
        {
            objects.Add(Pull());
        }

        return objects;
    }

    public virtual void Release(TObject obj)
    {
        obj.ResetObject();
        _objects.Add(obj);
    }

    public virtual void ClearPool()
    {
        _objects.Clear();
    }
}
