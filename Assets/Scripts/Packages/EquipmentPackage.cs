using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EquipmentPackage : RewardPackage
{
    [SerializeField] private ChanceCombiner<EquipmentReward> _rewards;

    [SerializeField] private Currency _singlePackageCost;
    [SerializeField] private Text _singleCostText;
    [SerializeField] private Image _currencyIcon;

    [SerializeField] private Currency _x10PackageCost;
    [SerializeField] private Text _x10CostText;
    [SerializeField] private Image _x10CurrencyIcon;

    [Inject] private MainMenu _mainMenu;
    [Inject] private MainInventory _mainInventory;

    private void OnEnable()
    {
        _singleCostText.text = _singlePackageCost.CurrencyValue.ToString();
        _currencyIcon.sprite = _singlePackageCost.CurrencyData.Icon;

        _x10CostText.text = _x10PackageCost.CurrencyValue.ToString();
        _x10CurrencyIcon.sprite = _x10PackageCost.CurrencyData.Icon;

        _rewards.Initialize();
    }

    public void OnSingleClick()
    {
        if (_mainInventory.Spend(_singlePackageCost))
        {
            _mainMenu.ShowRewards(_rewards.GetStrikedObject().Rewards);
        }
        else
        {
            _mainMenu.ShowPopupMessage("Not enough resources!");
        }
    }

    public void OnX10Click()
    {
        if (_mainInventory.Spend(_x10PackageCost))
        {
            List<Reward> rewards = new List<Reward>();

            for (int i = 0; i < 10; i++)
            {
                rewards.AddRange(_rewards.GetStrikedObject().Rewards);
            }

            _mainMenu.ShowRewards(rewards);
        }
        else
        {
            _mainMenu.ShowPopupMessage("Not enough resources!");
        }
    }
}
