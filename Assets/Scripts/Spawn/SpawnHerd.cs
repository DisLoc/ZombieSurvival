using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHerd : MonoBehaviour, IStartHerd
{
    [SerializeField] private ZombieSpawner zombieSpawner;

    public void OnStartHerd()
    {
        var zombie = zombieSpawner.Pool.Pull();
        zombie.Initialize(zombieSpawner.Pool);
        zombie.gameObject.transform.position = new Vector3(0, zombieSpawner.LevelBuilder.GridHeight + 1f, 0);

        Invoke(nameof(OnStartHerd), 45);
    }
}
