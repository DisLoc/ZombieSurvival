using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PickableHeart : PickableObject
{
    [Header("Heart settings")]
    [SerializeField] private Upgrade _healUpgrade;

    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
    }

    public override void PickUp()
    {
        base.PickUp();

        if (_player != null)
        {
            _player.GetUpgrade(_healUpgrade);
        }
        else if (_isDebug) Debug.Log("Missing player!");
    }
}
