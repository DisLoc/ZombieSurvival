using UnityEngine;
using Zenject;

public class ExpCrystal : PickableObject, IPoolable
{
    [SerializeField] private MeshRenderer _renderer;

    [Inject] private Player _player;

    private int _expValue;
    private FactoryMonoPool<ExpCrystal, Factory> _pool;

    /// <summary>
    /// Value that character get when take this crystal
    /// </summary>
    public int ExpValue => _expValue;

    /// <summary>
    /// Initializing exp value and color of this crystal & pool to release to
    /// </summary>
    /// <param name="param">Params need to set</param>
    /// <param name="pool">Pool to release to</param>
    public void Initialize(CrystalParam param, FactoryMonoPool<ExpCrystal, Factory> pool)
    {
        _expValue = param.ExpValue;
        _renderer.material.color = param.Color;

        _pool = pool;
    }

    public void ResetObject()
    {
        _expValue = 0;
    }


    public override PickableObject PickUp()
    {
        if (_player != null)
            (_player.Stats as PlayerStats).AddExpirience(_expValue);
        else if (_isDebug) Debug.Log("Missing player!");

        _pool.Release(this);

        return base.PickUp();
    }

    /// <summary>
    /// Zenject factory for auto injection fields
    /// </summary>
    public class Factory : PlaceholderFactory<ExpCrystal> { }
}
