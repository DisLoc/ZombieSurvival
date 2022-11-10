using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner<TObject> : MonoPool<TObject> where TObject : MonoBehaviour, IPoolable
{
    private CleanupableList<TObject> _spawnedObjects;

    public CleanupableList<TObject> SpawnedObjects => _spawnedObjects;
    public int SpawnCount => _spawnedObjects.Count;

    public ObjectSpawner(TObject prefab, int capacity, Transform parent = null) : base(prefab, capacity, parent)
    {
        _spawnedObjects = new CleanupableList<TObject>(capacity);
    }

    public TObject Spawn(Vector3 position)
    {
        TObject obj = Pull();

        if (obj == null) return null;
        
        obj.transform.position = position;

        if (_spawnedObjects == null) _spawnedObjects = new CleanupableList<TObject>();

        _spawnedObjects.Add(obj);

        return obj;
    }

    public TObject SpawnDisabled(Vector3 position)
    {
        TObject obj = PullDisabled();

        if (obj == null) return null;

        obj.transform.position = position;

        _spawnedObjects.Add(obj);

        return obj;
    }

    public List<TObject> SpawnGroup(Vector3 position, int count, float deltaPositionXZ)
    {
        List<TObject> objects = new List<TObject>(count);

        for(int i = 0; i < count; i++)
        {
            TObject obj = Spawn(GetRandomPosition(position, deltaPositionXZ));

            if (obj != null)
            {
                objects.Add(obj);
            }
        }

        _spawnedObjects.List.AddRange(objects);

        return objects;
    }

    public List<TObject> SpawnGroupDisabled(Vector3 position, int count, float deltaPositionXZ)
    {
        List<TObject> objects = new List<TObject>(count);

        for (int i = 0; i < count; i++)
        {
            TObject obj = SpawnDisabled(GetRandomPosition(position, deltaPositionXZ));
            
            if (obj != null)
            {
                objects.Add(obj);
            }
        }

        _spawnedObjects.List.AddRange(objects);

        return objects;
    }

    protected Vector3 GetRandomPosition(Vector3 position, float deltaPositionXZ)
    {
        return new Vector3(
            position.x + Random.Range(-deltaPositionXZ, deltaPositionXZ),
            position.y,
            position.z + Random.Range(-deltaPositionXZ, deltaPositionXZ)
            );
    }

    public override void Release(TObject obj)
    {
        if (obj == null) return;

        _spawnedObjects.Remove(obj, true);

        base.Release(obj);
    }

    public override void ClearPool()
    {
        base.ClearPool();

        _spawnedObjects.Cleanup();

        if (_spawnedObjects.Count > 0)
        {
            for (int i = 0; i < _spawnedObjects.Count; i++)
            {
                Object.Destroy(_spawnedObjects[i]);
            }
        }

        _spawnedObjects = null;
    }
}
