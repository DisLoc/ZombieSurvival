using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnHerd : MonoBehaviour, IStartHerd
{
    [SerializeField] private ZombieSpawner zombieSpawner;

    [Inject] Player _player;

    public void OnStartHerd()
    {
        var zombie = zombieSpawner.Pool.Pull();
        zombie.Initialize(_player, zombieSpawner.Pool);
        zombie.gameObject.transform.position = new Vector3(0, zombieSpawner.LevelBuilder.GridHeight + 1f, 0);

        Invoke(nameof(OnStartHerd), 45);
    }
}
