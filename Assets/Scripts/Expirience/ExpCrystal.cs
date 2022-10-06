using UnityEngine;

public class ExpCrystal : PickableObject, IPoolable
{
    [SerializeField] MeshRenderer _renderer;
    private int _expValue;

    public int ExpValue => _expValue;

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
        return base.PickUp();
    }
}
