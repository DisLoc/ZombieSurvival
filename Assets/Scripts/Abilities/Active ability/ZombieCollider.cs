public sealed class ZombieCollider : ColliderWeapon
{
    public override bool Upgrade(Upgrade upgrade)
    {
        bool isLevelUp = base.Upgrade(upgrade);

        _targetDetector.UpdateRadius();

        return isLevelUp;
    }
}
