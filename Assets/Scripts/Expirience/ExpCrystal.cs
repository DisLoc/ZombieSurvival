using UnityEngine;
using Zenject;

public class ExpCrystal : PickableObject, IPoolable
{
    [SerializeField] private MeshRenderer _renderer;
    private int _expValue;

    [Inject] private Player _player;

    public void Initialize(CrystalParam param)
    {
        _expValue = param.ExpValue;
        _renderer.material.color = param.Color;
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

        return base.PickUp();
    }

    public class Factory<ExpCrystal> : PlaceholderFactory<ExpCrystal> { }
}
