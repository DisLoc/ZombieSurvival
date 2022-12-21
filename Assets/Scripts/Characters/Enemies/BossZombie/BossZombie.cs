using System.Collections.Generic;
using UnityEngine;

public class BossZombie : Enemy
{
    [SerializeField] private List<Weapon> _additionalAbilities;

    private BossSpawner _spawner;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < _additionalAbilities.Count; i++)
        {
            GetAbility(_additionalAbilities[i]);
        }
    }

    public void InitializeSpawner(BossSpawner spawner)
    {
        _spawner = spawner;
    }

    public override void Die()
    {
        base.Die();

        _spawner.OnBossDies(transform.position);
    }
}
