using UnityEngine;

[System.Serializable]
public struct CurrentUpgrade
{
    [SerializeField] private string _description;
    [SerializeField] private Upgrade _upgrade;
    [Tooltip("Required level for this upgrade")]
    [SerializeField] private int _level;

    public string Description => _description;
    public Upgrade Upgrade => _upgrade;
    public int Level => _level;
}