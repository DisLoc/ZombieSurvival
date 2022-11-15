using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        LoadLevels();
        UpdatePreview();
    }

    private void LoadLevels()
    {
        _currentLevel = _levels[0];
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
        int index = _levels.IndexOf(_currentLevel);

        if (index > 0)
        {
            _currentLevel = _levels[index - 1];
        }

        UpdatePreview();
    }

    public void OnNextLevelClick()
    {
        int index = _levels.IndexOf(_currentLevel);

        if (index < _levels.Count - 1)
        {
            _currentLevel = _levels[index + 1];
        }

        UpdatePreview();
    }

    private void UpdatePreview()
    {
        _levelIcon.sprite = _currentLevel.LevelIcon;
        _levelText.text = _currentLevel.LevelNumber.ToString() + ". " + _currentLevel.LevelName;

        _levelInstaller.SetLevel(_currentLevel);

        int index = _levels.IndexOf(_currentLevel);

        if (index == 0)
        {
            _previousLevelButton.gameObject.SetActive(false);
        }
        else
        {
            _previousLevelButton.gameObject.SetActive(true);
        }

        if (index == _levels.Count - 1)
        {
            _nextLevelButton.gameObject.SetActive(false);
        }
        else
        {
            _nextLevelButton.gameObject.SetActive(true);
        }
    }
}
