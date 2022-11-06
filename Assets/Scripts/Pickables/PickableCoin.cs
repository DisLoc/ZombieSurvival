using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PickableCoin : PickableObject
{
    [Header("Coin settings")]
    [SerializeField][Range(0, 1000)] private int _coinValue;

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

        }
        else if (_isDebug) Debug.Log("Missing player!");
    }
}
