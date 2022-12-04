using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.Mathf;

public sealed class HerdSpawner : EnemySpawner, IBossEventHandler
{
    [Header("Herd spawner settings")]
    [SerializeField][Range(0f, 10f)] private float _deltaSpawnPosition = 10f;
    [SerializeField][Range(5f, 15f)] private float _directionRotationDelay = 10f;

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

        _currentSide = (SpawnSide)Abs((int)_currentSide - 3);

        StartCoroutine(WaitRotation());
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
}
