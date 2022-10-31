using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;

    private void Update()
    {
        foreach(var spawner in _spawners)
        {
            spawner.OnUpdate();
        }
    }

    private void FixedUpdate()
    {
        foreach(var spawner in _spawners)
        {
            spawner.OnFixedUpdate();
        }   
    }
}
