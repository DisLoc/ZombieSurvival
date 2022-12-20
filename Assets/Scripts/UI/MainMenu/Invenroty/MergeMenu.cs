using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MergeMenu : UIMenu
{
    [SerializeField] private Image _hideButton;

    [Inject] private MainInventory _mainInventory;

    public override void Display(bool playAnimation = false)
    {
        _hideButton.raycastTarget = true;

        base.Display(playAnimation);
    }

    public override void Hide(bool playAnimation = false)
    {
        _hideButton.raycastTarget = false;

        base.Hide(playAnimation);
    }

    public void QuickMerge()
    {
        int index = 0;

        while (index < _mainInventory.EquipmentInventory.Equipment.Count)
        {
            if (_mainInventory.EquipmentInventory.Equipment[index].EquipmentData.NextRarityEquipment == null)
            {
                index++;
                continue;
            }

            List<Equipment> combines = _mainInventory.EquipmentInventory.Equipment.FindAll(
                item => item.EquipmentData.Equals(_mainInventory.EquipmentInventory.Equipment[index].EquipmentData));
            
            int requiredEquipmentAmount = _mainInventory.EquipmentInventory.Equipment[index].EquipmentData.NextRarityEquipment.EquipmentUpgrades.GetUpgrade(1).UpgradeMaterials.RequiredEquipmentAmount;
            
            if (combines != null && combines.Count >= requiredEquipmentAmount)
            {
                _mainInventory.Add(combines[0].EquipmentData.MergeResult, combines.Count / requiredEquipmentAmount);

                for (int i = 0; i < combines.Count / requiredEquipmentAmount * requiredEquipmentAmount; i++)
                {
                    (_parentMenu as InventoryMenu).RemoveEquipment(combines[i]);

                    _mainInventory.Spend(combines[i]);
                }

                index = 0;
            }
            else index++;
        }

        _parentMenu.Display();
    }
}
