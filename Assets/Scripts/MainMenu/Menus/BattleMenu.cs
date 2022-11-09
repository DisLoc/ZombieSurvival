using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenu : UIMenu
{
    [Header("Battle menu settings")]
    [SerializeField] private Image _levelIcon;
    [SerializeField] private Text _levelText;
    [SerializeField] private Button _previousLevelButton;
    [SerializeField] private Button _nextLevelButton;

    [SerializeField] private LevelRewardChest _chest1;
    [SerializeField] private LevelRewardChest _chest2;
    [SerializeField] private LevelRewardChest _chest3;

    [SerializeField] private List<LevelContext> _levels;

    public override void Initialize(MainMenu mainMenu)
    {
        base.Initialize(mainMenu);

        if (_levels.Count == 1)
        {

        }
    }

    public void StartGame()
    {
        if (_isDebug) Debug.Log("Start");
    }

    public void OnRewardClick(LevelRewardChest chest)
    {

    }

    public void OnPreviousLevelClick()
    {

    }

    public void OnNextLevelClick()
    {

    }
}
