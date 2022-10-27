using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour, ILevelProgressUpdateHandler, IBossEventHandler
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Spawn settings")]
    [SerializeField][Range(1, 100)] protected int _maxUnitsOnScene;


    #region Inject
    [Inject] private Zombie.Factory _factory;
    [Inject] private LevelBuilder _levelBuilder;
    [Inject] private LevelProgress _levelProgress;
    [Inject] private Player _player;
    #endregion

    public void OnLevelProgressUpdate(int progress)
    {

    }

    public void OnBossEvent()
    {

    }

    public virtual void Spawn()
    {

    }

    public virtual void SpawnHorde()
    {

    }

    public virtual void SpawnBoss()
    {

    }
}
