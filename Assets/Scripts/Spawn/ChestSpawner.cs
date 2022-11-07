using UnityEngine;
using Zenject;

public class ChestSpawner : Spawner
{
    [Inject] private LevelContext _levelContext;

    protected override void Spawn(Vector3 position)
    {
        
    }
}
