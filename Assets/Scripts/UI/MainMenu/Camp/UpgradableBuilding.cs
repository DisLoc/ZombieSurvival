using UnityEngine;
using Zenject;

public class UpgradableBuilding : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Building settings")]
    [SerializeField] private int _unlockLevel;

    private bool _unlocked;

    public int UnlockLevel => _unlockLevel;
    public bool Unlocked => _unlocked;

    [Inject] private MainInventory _mainInventory;

    private void Start()
    {
        if (_mainInventory.PlayerLevel.Value >= _unlockLevel)
        {
            _unlocked = true;
        }
        else
        {
            _unlocked = false;
        }
    }
}
