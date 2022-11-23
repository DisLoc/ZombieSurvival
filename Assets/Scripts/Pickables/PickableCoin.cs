using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PickableCoin : PickableObject
{
    [Header("Coin settings")]
    [SerializeField] private Coin _coin;

    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
    }

    protected override void OnPickUp()
    {
        if (_player != null)
        {
            _player.CoinInventory.Add(_coin);
        }
        else if (_isDebug) Debug.Log("Missing player!");

        Destroy(gameObject);
    }
}
