using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TalentSlot : MonoBehaviour
{
    
    [SerializeField] private Button _button;

    [SerializeField] private Sprite _lockedBackground;
    [SerializeField] private Sprite _unlockedBackground;

    [SerializeField] private Image _talentBackround;
    [SerializeField] private Image _talentIcon;
    [SerializeField] private Text _talentDescriptionText;
    [SerializeField] private Text _requiredLevelText;

    private Talent _talent;
    private CampTalentsMenu _menu;

    public Talent Talent => _talent;

    [Inject] private MainInventory _mainInventory;
    [Inject] private MainMenu _mainMenu;

    public void Initialize(CampTalentsMenu menu)
    {
        _menu = menu;
    }

    public void Initialize(Talent talent)
    {
        _talent = talent;

        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (_talent.Unlocked)
        {
            _talentBackround.sprite = _unlockedBackground;
            _talentIcon.sprite = _talent.UnlockedIcon;
            _button.interactable = false;
        }
        else
        {
            _talentBackround.sprite = _lockedBackground;
            _talentIcon.sprite = _talent.LockedIcon;
            _button.interactable = true;
        }

        _talentDescriptionText.text = _talent.Description;
        _requiredLevelText.text = _talent.RequiredLevel.ToString();
    }

    public void OnClick()
    {
        _menu.ShowHint(this);
    }

    public void Unlock()
    {
        if (_mainInventory.PlayerLevel.Value >= _talent.RequiredLevel)
        {
            if (_mainInventory.Spend(_talent.RequiredCurrency))
            {
                _talent.Unlock();

                _mainInventory.SaveInventory(_mainInventory.CampInventory);
            }
            else
            {
                _mainMenu.ShowPopupMessage("Not enough resources!");
            }
        }
        else
        {
            _mainMenu.ShowPopupMessage("You need " + _talent.RequiredLevel + " level to unlock this talent!");
        }
    }
}
