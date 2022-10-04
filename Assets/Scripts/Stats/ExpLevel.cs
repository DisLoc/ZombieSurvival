using UnityEngine;

[System.Serializable]
public class ExpLevel : Stat
{
    [SerializeField] protected Expirience _expirience;

    public float LevelProgress { get; }

    public override bool Upgrade(Upgrade upgrade)
    {
        if (base.Upgrade(upgrade))
        {

            

            return true;
        }
        else return false;
    }

    public void AddExp(int exp)
    {
        _value += (exp + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier;
    }
}
