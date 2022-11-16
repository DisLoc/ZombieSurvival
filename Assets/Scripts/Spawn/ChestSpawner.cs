using UnityEngine;
using Zenject;

public sealed class ChestSpawner : Spawner, IGameStartHandler
{
    [Header("Chest settings")]
    [SerializeField] private PickablesChest _rewardChestPrefab;
    [SerializeField] private ChanceCombiner<PickableObject> _pickablesSpawnChances;
    [Tooltip("Spawn interval in seconds")]
    [SerializeField] private int _spawnCooldown;

    private float _timer;
    private bool _onGame;

    [Inject] private Player _player;
    [Inject] private LevelContext _levelContext;

    public void OnGameStart()
    {
        _pickablesSpawnChances.Initialize();
        _timer = _spawnCooldown;
        _onGame = true;
    }

    private void Update()
    {
        if (!_onGame) return;

        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            Spawn(_player.transform.position);
        }
    }

    protected override void Spawn(Vector3 position)
    {
        if (_isDebug) Debug.Log("Spawn chest");

        _timer = _spawnCooldown;

        PickablesChest chest = Instantiate(_rewardChestPrefab, transform);
        chest.transform.position = position + GetDeltaPos();

        chest.Initialize(this);
    }

    private Vector3 GetDeltaPos()
    {
        return new Vector3
            (
                Random.Range(0, 2) > 0 ? _spawnDeltaDistance : -_spawnDeltaDistance,
                0f,
                Random.Range(0, 2) > 0 ? _spawnDeltaDistance : -_spawnDeltaDistance
            );
    }

    public void OnChestDestoyed(PickablesChest chest)
    {
        PickableObject obj = Instantiate(_pickablesSpawnChances.GetStrikedObject(), transform);

        obj.transform.position = chest.transform.position;

        Destroy(chest.gameObject);

        if (obj as PickableCoin != null)
        {
            (obj as PickableCoin).Initialize(_player);
        } 

        if (obj as PickableHeart != null)
        {
            (obj as PickableHeart).Initialize(_player);
        }

        if (obj as PickableMagnet != null)
        {
            (obj as PickableMagnet).Initialize();
        }

        if (_isDebug) Debug.Log("Chest destoyed, spawning " + obj.name);
    }
}
