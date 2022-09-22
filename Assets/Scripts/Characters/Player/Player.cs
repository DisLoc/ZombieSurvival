using UnityEngine;

public sealed class Player : CharacterBase
{
    [Header("Settings")]
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private Collider _pickUpCollider;

    public override CharacterStats Stats => _stats;

    public override void Move(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " collides player");
    }
}
