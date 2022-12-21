using UnityEngine;

[System.Serializable]
public class Talent
{
    [SerializeField] private Sprite _lockedIcon;
    [SerializeField] private Sprite _unlockedIcon;

    [Space(5)]
    [SerializeField] private Upgrade _upgrade;
    [SerializeField] private string _description;
    [SerializeField] private int _requiredLevel;
    [SerializeField] private Currency _requiredCurrency;

    [SerializeField] private TalentSlot _slot;

    private bool _unlocked;

    public Sprite LockedIcon => _lockedIcon;
    public Sprite UnlockedIcon => _unlockedIcon;
    public string Description => _description;
    public Upgrade Upgrade => _upgrade;
    public int RequiredLevel => _requiredLevel;
    public Currency RequiredCurrency => _requiredCurrency;
    public bool Unlocked => _unlocked;

    public void Initialize(bool unlocked = false)
    {
        _unlocked = unlocked;

        _slot?.Initialize(this);
    }

    public void Unlock()
    {
        _unlocked = true;

        _slot?.Initialize(this);
    }
}
