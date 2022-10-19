using UnityEngine;

[System.Serializable]
public struct CurrentUpgrade
{
    [SerializeField] private string _description;
    [SerializeField] private Upgrade _upgrade;
    [SerializeField] private int _requiredLevel;

    public string Description => _description;
    public Upgrade Upgrade => _upgrade;
    public int RequiredLevel => _requiredLevel;
}