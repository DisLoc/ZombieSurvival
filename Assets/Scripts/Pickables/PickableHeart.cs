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

    protected override void OnPickUp()
    {
        if (_player != null)
        {
            _player.GetUpgrade(_healUpgrade);
        }
        else if (_isDebug) Debug.Log("Missing player!");

        Destroy(gameObject);
    }
}
