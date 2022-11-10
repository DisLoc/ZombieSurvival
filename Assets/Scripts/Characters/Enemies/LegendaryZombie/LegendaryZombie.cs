using UnityEngine;

public class LegendaryZombie : Enemy
{
    [Header("Legendary zombie settings")]
    [SerializeField] private ZombieChest _reward;

    public override void Die()
    {
        base.Die();
    }

}
