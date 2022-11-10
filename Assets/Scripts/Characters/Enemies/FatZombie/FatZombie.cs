using UnityEngine;

public class FatZombie : Enemy
{
    [Header("Fat zombie settings")]
    [SerializeField] private FatZombieExplosion _explosion;

    public override void Die()
    {
        base.Die();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Die();
        }
    }
}
