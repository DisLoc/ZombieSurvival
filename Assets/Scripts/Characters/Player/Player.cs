using UnityEngine;

public sealed class Player : CharacterBase
{
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private ObjectCatcher _catcher;

    public override CharacterStats Stats => _stats;

    private void OnEnable()
    {
        _stats.Initialize();
        _catcher.Initialize(_stats.PickUpRange);

        _healthBar.UpdateHealth(HP);
    }

    public override void Move(Vector3 direction)
    {
        Vector3 pos = transform.position;

        transform.LookAt(pos + direction);
        transform.position = Vector3.MoveTowards(pos, pos + direction * _stats.Velocity, _stats.Velocity * Time.fixedDeltaTime);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}
