using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ExpCrystal : PickableObject, IPoolable
{
    [SerializeField] private SpriteRenderer _renderer;
    private int _expValue;

    private Player _player;
    private MonoPool<ExpCrystal> _pool;

    /// <summary>
    /// Value that character get when take this crystal
    /// </summary>
    public int ExpValue => _expValue;

    /// <summary>
    /// Initializing exp value and color of this crystal & pool to release to
    /// </summary>
    /// <param name="param">Params need to set</param>
    /// <param name="pool">Pool to release to</param>
    public void Initialize(CrystalParam param, MonoPool<ExpCrystal> pool, Player player)
    {
        _expValue = param.ExpValue;
        _renderer.material.color = param.Color;

        _pool = pool;
        _player = player;
    }

    public void ResetObject()
    {
        _expValue = 0;
    }

    protected override void OnPickUp()
    {
        base.OnPickUp();

        StopAllCoroutines();

        if (_player != null)
        {
            if (_isDebug) Debug.Log("Add exp to player");
            
            (_player.Stats as PlayerStats).AddExpirience(_expValue);
        }    
        else if (_isDebug) Debug.Log("Missing player!");

        if (_pool != null)
            _pool.Release(this);
        else Destroy(gameObject);
    }
}
