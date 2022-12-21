using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class HerdSpawner : EnemySpawner, IBossEventHandler
{
    [Header("Herd spawner settings")]
    [SerializeField][Range(0f, 10f)] private float _deltaSpawnPosition = 10f;
    [SerializeField][Range(5f, 15f)] private float _directionRotationDelay = 10f;
    [SerializeField] private float _minMoveSpeed = 10f;
    [SerializeField] private MarkerList _moveSpeedMarkers;

    private Upgrade _moveSpeedUpgrade;

    private BreakpointList<HerdBreakpoint> _breakpoints;
    private HerdBreakpoint _currentBreakpoint;

    private bool _onBossEvent;

    private SpawnSide _currentSide;
    private enum SpawnSide 
    {
        UpLeft,
        UpRight,
        DownLeft,
        DownRight,
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = new BreakpointList<HerdBreakpoint>(_levelContext.HerdBreakpoints);
    }

    public override void OnUpdate()
    {
        if (!_onBossEvent && CurrentSpawned > 0)
        {
            base.OnUpdate();
            TryClearPool();
        }
        else return;
    }

    public override void OnFixedUpdate()
    {
        if (!_onBossEvent && CurrentSpawned > 0)
        {
            base.OnFixedUpdate();
            TryClearPool();
        }
        else return;
    }

    public override void OnLevelProgressUpdate(int progress)
    {
        Breakpoint breakpoint = _breakpoints.CheckReaching(progress);

        if (breakpoint != null)
        {
            _currentBreakpoint = (breakpoint as HerdBreakpoint);

            if (_isDebug) Debug.Log("Herd incoming!");

            _maxUnitsOnScene = _currentBreakpoint.SpawnCount;

            DispelUpgrades();
            ReplacePools();

            _spawners.Add(new ObjectSpawner<Enemy>(_currentBreakpoint.EnemyToSpawnPrefab, _maxUnitsOnScene, transform));

            _moveSpeedUpgrade = new Upgrade
                (
                    new UpgradeData
                        (
                            _moveSpeedMarkers,
                            (_currentBreakpoint.EnemyToSpawnPrefab.Stats.Velocity.BaseValue >= _minMoveSpeed ? 0 :
                                    _minMoveSpeed - _currentBreakpoint.EnemyToSpawnPrefab.Stats.Velocity.BaseValue),
                            1f
                        ),
                    abilityMarker: null,
                    isAbilityUpgrade: false
                );

            Spawn(GetSpawnPosition());

            GetUpgrade();
        }

        base.OnLevelProgressUpdate(progress);
    }

    protected override void Spawn(Vector3 position)
    {
        if (_spawners != null && _spawners.Count > 0 && position != -Vector3.one)
        {
            List<Enemy> spawnedEnemies = _spawners[0].SpawnGroup(new Vector3
                    (
                        position.x,
                        _levelContext.LevelBuilder.GridHeight + _spawners[0].Prefab.Collider.height * _spawners[0].Prefab.transform.localScale.y * 0.5f,
                        position.z
                    ), _currentBreakpoint.SpawnCount, _deltaSpawnPosition);

            foreach (Enemy spawnedEnemy in spawnedEnemies)
            {
                spawnedEnemy.Initialize(_player, _spawners[0]);
            }

            _totalSpawned += _currentBreakpoint.SpawnCount;

            StartCoroutine(WaitRotation());
        }
        else return;
    }

    private IEnumerator WaitRotation()
    {
        yield return new WaitForSeconds(_directionRotationDelay);

        Respawn();

        StartCoroutine(WaitRotation());
    }

    private void Respawn()
    {
        if (_spawners != null && _spawners.Count > 0)
        {
            Vector3 spawnPos = GetSpawnPosition();

            for (int i = 0; i < _spawners[0].SpawnCount; i++)
            {
                if (_spawners[0].SpawnedObjects[i] != null)
                {
                    _spawners[0].SpawnedObjects[i].transform.position = spawnPos + new Vector3
                                                                                    (
                                                                                        Random.Range(-_deltaSpawnPosition, _deltaSpawnPosition),
                                                                                        0f,
                                                                                        Random.Range(-_deltaSpawnPosition, _deltaSpawnPosition)
                                                                                    );
                }
            }
        }
    }

    protected override Vector3 GetSpawnPosition()
    {
        float x = Random.Range(0, 2) > 0 ? _spawnDeltaDistance : -_spawnDeltaDistance;
        float z = Random.Range(0, 2) > 0 ? _spawnDeltaDistance : -_spawnDeltaDistance;

        if (x == _spawnDeltaDistance)
        {
            if (z == _spawnDeltaDistance)
            {
                _currentSide = SpawnSide.UpRight;
            }
            else
            {
                _currentSide = SpawnSide.DownRight;
            }
        }
        else
        {
            if (z == _spawnDeltaDistance)
            {
                _currentSide = SpawnSide.UpLeft;
            }
            else
            {
                _currentSide = SpawnSide.DownLeft;
            }
        }

        return _player.transform.position + new Vector3(x, 0f, z);
    }

    protected override Vector3 GetMoveDirection(Vector3 playerPos, Vector3 enemyPos)
    {
        Vector3 direction;

        switch (_currentSide)
        {
            case SpawnSide.UpLeft:
                direction = Vector3.right + Vector3.back;
                break;
            case SpawnSide.UpRight:
                direction = Vector3.left + Vector3.back;
                break;
            case SpawnSide.DownLeft:
                direction = Vector3.right + Vector3.forward;
                break;
            case SpawnSide.DownRight:
                direction = Vector3.left + Vector3.forward;
                break;
            default:
                Debug.Log("Missing SpawnSide!");
                return base.GetMoveDirection(playerPos, enemyPos);
        }

        return direction;
    }

    public void OnBossEvent()
    {
        if (_spawners != null)
        {
            foreach (var spawner in _spawners)
            {
                for (int i = 0; i < spawner.SpawnCount; i++)
                {
                    spawner.SpawnedObjects[i]?.Die();
                }
            }
        }

        if (_prevSpawners != null)
        {
            foreach (var spawner in _prevSpawners)
            {
                for (int i = 0; i < spawner.SpawnCount; i++)
                {
                    spawner.SpawnedObjects[i]?.Die();
                }
            }
        }

        ClearPools();
    }

    public void OnBossEventEnd()
    {
        _onBossEvent = false;
    }

    private void TryClearPool()
    {
        if (CurrentSpawned == 0)
        {
            StopAllCoroutines();

            ClearPools();
        }
    }

    protected override void GetUpgrade()
    {
        base.GetUpgrade();

        if (_spawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _spawners)
            {
                foreach (Enemy zombie in pool.Objects)
                {
                    zombie?.GetUpgrade(_moveSpeedUpgrade);
                }

                foreach (Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie?.GetUpgrade(_moveSpeedUpgrade);
                }
            }
        }

        if (_prevSpawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _prevSpawners)
            {
                foreach (Enemy zombie in pool.Objects)
                {
                    zombie?.GetUpgrade(_moveSpeedUpgrade);
                }

                foreach (Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie?.GetUpgrade(_moveSpeedUpgrade);
                }
            }
        }
    }

    protected override void DispelUpgrades()
    {
        base.DispelUpgrades(); 

        if (_spawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _spawners)
            {
                foreach (Enemy zombie in pool.Objects)
                {
                    zombie?.DispelUpgrade(_moveSpeedUpgrade);
                }

                foreach (Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie?.DispelUpgrade(_moveSpeedUpgrade);
                }
            }
        }

        if (_prevSpawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _prevSpawners)
            {
                foreach (Enemy zombie in pool.Objects)
                {
                    zombie?.DispelUpgrade(_moveSpeedUpgrade);
                }

                foreach (Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie?.DispelUpgrade(_moveSpeedUpgrade);
                }
            }
        }
    }
}
