using System.Collections.Generic;

public abstract class ObjectPool<TObject> where TObject : IPoolable
{
    protected List<TObject> _objects;
    
    /// <summary>
    /// Objects in pool 
    /// </summary>
    public List<TObject> Objects => _objects;
    public bool IsEmpty => _objects.Count == 0;

    /// <summary>
    /// Create and add to pool new object
    /// </summary>
    protected abstract void CreateObject();

    /// <summary>
    /// Pull object from pool
    /// </summary>
    /// <returns>Return last object of pool</returns>
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

    /// <summary>
    /// Pull X object from pool
    /// </summary>
    /// <param name="count">Objects count need to pull</param>
    /// <returns>Return X last objects of pool</returns>
    public virtual List<TObject> PullObjects(int count)
    {
        List<TObject> objects = new List<TObject>();
        
        for (int i = 0; i < count; i++)
        {
            objects.Add(Pull());
        }

        return objects;
    }

    /// <summary>
    /// Release object to pool
    /// </summary>
    /// <param name="obj">Object need to release</param>
    public virtual void Release(TObject obj)
    {
        if (obj == null)
        {
            obj.ResetObject();
            _objects.Add(obj);
        }
        else return;
    }

    /// <summary>
    /// Remove all objects from pool
    /// </summary>
    public virtual void ClearPool()
    {
        _objects.Clear();
    }
}
