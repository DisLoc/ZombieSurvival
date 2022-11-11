using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private LevelContextInstaller _levelInstaller;

    private LevelContext _currentLevel;

    public override void Initialize(MainMenu mainMenu)
    {
        base.Initialize(mainMenu);

        _currentLevel = _levels[0];
        UpdatePreview();
    }

    public void StartGame()
    {
        _levelInstaller.SetLevel(_currentLevel);

        SceneManager.LoadScene(1);
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

    private void UpdatePreview()
    {
        _levelIcon.sprite = _currentLevel.LevelIcon;
        _levelText.text = _currentLevel.LevelNumber.ToString() + ". " + _currentLevel.LevelName;
    }
}
