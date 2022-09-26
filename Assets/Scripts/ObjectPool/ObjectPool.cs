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

        TObject obj = _objects[0];
        _objects.RemoveAt(0);

        return obj;
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
