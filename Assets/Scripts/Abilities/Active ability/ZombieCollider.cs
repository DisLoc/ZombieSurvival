public sealed class ZombieCollider : ColliderWeapon
{
    public void OnReset()
    {
        _targetDetector.Cleanup();
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        bool isLevelUp = base.Upgrade(upgrade);

        _targetDetector.UpdateRadius();

        return isLevelUp;
    }
}
