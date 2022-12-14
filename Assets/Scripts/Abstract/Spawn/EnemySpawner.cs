using System.Collections.Generic;
using UnityEngine;
using Zenject;

using static UnityEngine.Mathf;

public abstract class EnemySpawner : Spawner, IUpdatable, IFixedUpdatable
{
    [Tooltip("Means distance between player and spawned objects")]
    [SerializeField] protected float _maxDistanceForRespawn;

    protected int _maxUnitsOnScene;
    protected int _totalSpawned;

    protected BreakpointList<UpgradeBreakpoint> _upgradeBreakpoints;

    protected Upgrade _currentUpgrade;
    protected List<Upgrade> _levelUpgrades;

    protected List<ObjectSpawner<Enemy>> _prevSpawners;
    protected List<ObjectSpawner<Enemy>> _spawners;

    protected int CurrentSpawned
    {
        get
        {
            int spawned = 0;

            if (_spawners != null)
            {
                foreach (ObjectSpawner<Enemy> pool in _spawners)
                {
                    spawned += pool.SpawnCount;
                }
            }

            if (_prevSpawners != null)
            {
                foreach (ObjectSpawner<Enemy> pool in _prevSpawners)
                {
                    spawned += pool.SpawnCount;
                }
            }

            return spawned;
        }
    }

    [Inject] protected Player _player;
    [Inject] protected LevelContext _levelContext;

    protected override void OnEnable()
    {
        base.OnEnable();

        _upgradeBreakpoints = new BreakpointList<UpgradeBreakpoint>(_levelContext.EnemyUpgradeBreakpoints);

        _levelUpgrades = _levelContext.EnemiesUpgrades;

        _spawners = new List<ObjectSpawner<Enemy>>();
        _prevSpawners = new List<ObjectSpawner<Enemy>>();
    }

    public override void OnLevelProgressUpdate(int progress)
    {
        Breakpoint upgradeBreakpoint = _upgradeBreakpoints.CheckReaching(progress);

        if (upgradeBreakpoint != null)
        {
            if (_isDebug) Debug.Log("Enemy upgrade!");

            DispelUpgrades();

            _currentUpgrade = (upgradeBreakpoint as UpgradeBreakpoint).Upgrade;

            GetUpgrade();
        }
    }

    public virtual void OnUpdate() 
    {
        if (_spawners != null)
        {
            foreach (var spawner in _spawners)
            {
                if (spawner.SpawnCount == 0) continue;

                for (int i = 0; i < spawner.SpawnCount; i++)
                {
                    spawner.SpawnedObjects[i]?.OnUpdate();
                }
                spawner.SpawnedObjects.Cleanup();
            }
        }

        if (_prevSpawners != null)
        {
            foreach (var spawner in _prevSpawners)
            {
                if (spawner.SpawnCount == 0) continue;

                for (int i = 0; i < spawner.SpawnCount; i++)
                {
                    spawner.SpawnedObjects[i]?.OnUpdate();
                }

                spawner.SpawnedObjects.Cleanup();

                if (spawner.SpawnCount == 0)
                {
                    spawner.ClearPool();
                }
            }

            _prevSpawners.RemoveAll(item => item.SpawnedObjects == null);
        }
    }

    public virtual void OnFixedUpdate() 
    {
        Vector3 playerPos = _player.transform.position;

        if (_spawners != null)
        {
            foreach (var spawner in _spawners)
            {
                if (spawner.SpawnCount == 0) continue;

                for (int i = 0; i < spawner.SpawnCount; i++)
                {
                    if (spawner.SpawnedObjects[i] != null)
                    {
                        spawner.SpawnedObjects[i].Move(GetMoveDirection(playerPos, spawner.SpawnedObjects[i].transform.position));

                        spawner.SpawnedObjects[i]?.OnFixedUpdate();
                    }
                    else continue;
                }

                spawner.SpawnedObjects.Cleanup();
            }   
        }

        if (_prevSpawners != null)
        {
            foreach (var spawner in _prevSpawners)
            {
                if (spawner.SpawnCount == 0) continue;

                for (int i = 0; i < spawner.SpawnCount; i++)
                {
                    if (spawner.SpawnedObjects[i] != null)
                    {
                        spawner.SpawnedObjects[i].Move(GetMoveDirection(playerPos, spawner.SpawnedObjects[i].transform.position));

                        spawner.SpawnedObjects[i]?.OnFixedUpdate();
                    }
                    else continue;
                }

                spawner.SpawnedObjects.Cleanup();

                if (spawner.SpawnCount == 0)
                {
                    spawner.ClearPool();
                }
            }

            _prevSpawners.RemoveAll(item => item.SpawnedObjects == null);
        }
    }

    protected virtual Vector3 GetMoveDirection(Vector3 playerPos, Vector3 enemyPos)
    {
        return new Vector3
                        (
                            playerPos.x - enemyPos.x,
                            0f,
                            playerPos.z - enemyPos.z
                        );
    }

    protected virtual Vector3 GetSpawnPosition()
    {
        Vector3 playerPos = _player.transform.position;

        return new Vector3
            (
                Cos(_totalSpawned % _maxUnitsOnScene) * _spawnDeltaDistance + playerPos.x,
                0f,
                Sin(_totalSpawned % _maxUnitsOnScene) * _spawnDeltaDistance + playerPos.z
            );
    }

    /// <summary>
    /// Removes all enemies from scene (spawned and enemies in pool)
    /// </summary>
    protected virtual void ClearPools()
    {
        if (_spawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _spawners)
            {
                pool.ClearPool();
            }

            _spawners.Clear();
        }
        
        if (_prevSpawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _prevSpawners)
            {
                pool.ClearPool();
            }

            _prevSpawners.Clear();
        }

        _totalSpawned = 0;
    }

    /// <summary>
    /// Replace current pools to prevPools (it will be destroyed when all enemies return to pool)
    /// </summary>
    protected void ReplacePools()
    {
        if (_totalSpawned == 0) return;

        if (_spawners != null)
        {
            if (_prevSpawners == null)
            {
                _prevSpawners = new List<ObjectSpawner<Enemy>>();
            }

            foreach (ObjectSpawner<Enemy> pool in _spawners)
            {
                if (pool.SpawnCount == 0)
                {
                    pool.ClearPool();
                }
                else
                {
                    _prevSpawners.Add(pool);
                }
            }

            _spawners.Clear();
        }
        else
        {
            _spawners = new List<ObjectSpawner<Enemy>>();
        }
    }

    /// <summary>
    /// Add upgrade to enemies (spawned and enemies in pool)
    /// </summary>
    protected virtual void GetUpgrade()
    {
        if (_spawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _spawners)
            {
                foreach (Enemy zombie in pool.Objects)
                {
                    zombie?.GetUpgrade(_currentUpgrade);
                    
                    foreach(Upgrade levelUpgrade in _levelUpgrades)
                    {
                        zombie?.GetUpgrade(levelUpgrade);
                    }
                }

                foreach (Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie?.GetUpgrade(_currentUpgrade);

                    foreach (Upgrade levelUpgrade in _levelUpgrades)
                    {
                        zombie?.GetUpgrade(levelUpgrade);
                    }
                }
            }
        }

        if (_prevSpawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _prevSpawners)
            {
                foreach (Enemy zombie in pool.Objects)
                {
                    zombie?.GetUpgrade(_currentUpgrade);

                    foreach (Upgrade levelUpgrade in _levelUpgrades)
                    {
                        zombie?.GetUpgrade(levelUpgrade);
                    }
                }

                foreach (Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie?.GetUpgrade(_currentUpgrade);

                    foreach (Upgrade levelUpgrade in _levelUpgrades)
                    {
                        zombie?.GetUpgrade(levelUpgrade);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dispel upgrade from enemies (spawned and enemies in pool)
    /// </summary>
    protected virtual void DispelUpgrades()
    {
        if (_currentUpgrade == null) return;

        if (_spawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _spawners)
            {
                foreach (Enemy zombie in pool.Objects)
                {
                    zombie?.DispelUpgrade(_currentUpgrade);

                    foreach (Upgrade levelUpgrade in _levelUpgrades)
                    {
                        zombie?.DispelUpgrade(levelUpgrade);
                    }
                }

                foreach (Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie?.DispelUpgrade(_currentUpgrade);

                    foreach (Upgrade levelUpgrade in _levelUpgrades)
                    {
                        zombie?.DispelUpgrade(levelUpgrade);
                    }
                }
            }
        }

        if (_prevSpawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _prevSpawners)
            {
                foreach (Enemy zombie in pool.Objects)
                {
                    zombie?.DispelUpgrade(_currentUpgrade);

                    foreach (Upgrade levelUpgrade in _levelUpgrades)
                    {
                        zombie?.DispelUpgrade(levelUpgrade);
                    }
                }

                foreach (Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie?.DispelUpgrade(_currentUpgrade);

                    foreach (Upgrade levelUpgrade in _levelUpgrades)
                    {
                        zombie?.DispelUpgrade(levelUpgrade);
                    }
                }
            }
        }
    }
}
