using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner<TObject, TFactory> : FactoryMonoPool<TObject, TFactory> where TObject : MonoBehaviour, IPoolable
                                                                                            where TFactory: Zenject.PlaceholderFactory<TObject>
{
    public ObjectSpawner(TObject prefab, TFactory factory, int capacity, Transform parent = null) : base(prefab, factory, capacity, parent)
    {

    }

    public TObject Spawn(Vector3 position)
    {
        TObject obj = Pull();

        obj.transform.position = position;

        return obj;
    }

    public List<TObject> SpawnGroup(Vector3 position, int count, float deltaPositionXZ)
    {
        List<TObject> objects = new List<TObject>(count);

        for(int i = 0; i < count; i++)
        {
            objects.Add(Spawn(GetRandomPosition(position, deltaPositionXZ)));
        }

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
}
