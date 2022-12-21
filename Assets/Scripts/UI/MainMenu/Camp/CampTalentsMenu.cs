using System.Collections.Generic;
using UnityEngine;

public class CampTalentsMenu : UIMenu
{
    [Header("TalentsMenu settings")]
    [SerializeField] private PopupHint _hint;
    [SerializeField] private List<TalentSlot> _slots;

    private TalentSlot _currentSlot;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        _hint.Initialize(mainMenu, this);
        _hint.Hide();

        foreach(var slot in _slots)
        {
            slot.Initialize(this);
        }
    }

    public override void Display(bool playAnimation = false)
    {
        base.Display(playAnimation);
        
        _hint.Hide();

        _currentSlot = null;

        foreach(var slot in _slots)
        {
            slot.UpdateSlot();
        }
    }

    public void ShowHint(TalentSlot slot)
    {
        _currentSlot = slot;

        _hint.Show(slot.Talent.RequiredCurrency);
        _hint.Display(true);
    }

    public void HideHint()
    {
        _hint.Hide(true);

        _currentSlot = null;
    }

    public void Unlock()
    {
        _hint.Hide(true);

        _currentSlot.Unlock();
        _currentSlot = null;
    }
}
