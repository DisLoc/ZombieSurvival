using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private MainInventory _inventory;

    private void OnEnable()
    {
        _inventory.Initialize();
    }
}
