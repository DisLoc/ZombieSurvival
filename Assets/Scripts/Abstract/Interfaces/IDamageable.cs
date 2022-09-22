public interface IDamageable
{
    public int HP { get; } 
    public int MaxHP { get; }

    public void TakeDamage(int damage);

    public void Die();
}
