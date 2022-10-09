using UnityEngine;
using Zenject;

public class ExpCrystal : PickableObject, IPoolable
{
    [SerializeField] private MeshRenderer _renderer;

    [Inject] private Player _player;

    private int _expValue;
    private FactoryPool<ExpCrystal, Factory> _pool;

    public void Initialize(CrystalParam param, FactoryPool<ExpCrystal, Factory> pool)
    {
        _expValue = param.ExpValue;
        _renderer.material.color = param.Color;

        _pool = pool;
    }

    public void ResetObject()
    {
        _expValue = 0;
        _renderer.material = null;
    }


    public override PickableObject PickUp()
    {
        if (_player != null)
            (_player.Stats as PlayerStats).AddExpirience(_expValue);
        else if (_isDebug) Debug.Log("Missing player!");

        _pool.Release(this);

        return base.PickUp();
    }

    public class Factory : PlaceholderFactory<ExpCrystal> { }
}
