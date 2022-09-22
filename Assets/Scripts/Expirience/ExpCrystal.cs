using UnityEngine;

public class ExpCrystal : PickableObject, IPoolable
{
    Expirience _expirience;

    [SerializeField] MeshRenderer _renderer;

    public void Initialize(CrystalParam param)
    {
        _expirience = new Expirience(param.ExpValue);
        _renderer.material = param.Material;
    }

    public void ResetObject()
    {
        _expirience = null;
        _renderer.material = null;
    }


    public override PickableObject PickUp()
    {
        return base.PickUp();
    }
}
