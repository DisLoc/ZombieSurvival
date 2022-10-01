using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour, IHerdSpawnHandler, IBossEventHandler, IGameOverHandler, IGameStartHandler
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private GameObject _bossEventFence;

    [Inject] private Player _player;
    
    public void OnGameStart()
    {

    }

    public void OnGameOver()
    {

    }

    public void OnHerdSpawn()
    {

    }

    public void OnBossEvent()
    {

    }
}
