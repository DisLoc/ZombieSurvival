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

        _spawnedObjects.Add(obj);

        return obj;
    }

    public List<TObject> SpawnGroup(Vector3 position, int count, float deltaPositionXZ)
    {
        List<TObject> objects = new List<TObject>(count);

        for(int i = 0; i < count; i++)
        {
            objects.Add(Spawn(GetRandomPosition(position, deltaPositionXZ)));
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
        base.Release(obj);

        _spawnedObjects.Remove(obj, true);
    }
}
