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

    [SerializeField] private BattleMenuLevelProgress _levelProgress;
    [SerializeField] private LevelRewardChest _chest1;
    [SerializeField] private LevelRewardChest _chest2;
    [SerializeField] private LevelRewardChest _chest3;

    [SerializeField] private List<LevelContext> _levels;
    [SerializeField] private LevelContextInstaller _levelInstaller;

    private LevelContext _currentLevel;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        _levelProgress.Initialize();

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
        Reward reward = chest.Reward;
        
        // TODO add reward to inventory
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
        int levelIndex = _levels.IndexOf(_currentLevel);

        List<LevelBreakpoint> breakpoints = _currentLevel.LevelRewards.Breakpoints;

        if (breakpoints.Count != 3)
        {
            if (_isDebug) Debug.Log("Rewards count error!");

            _levelProgress.gameObject.SetActive(false);
            _chest1.gameObject.SetActive(false);
            _chest2.gameObject.SetActive(false);
            _chest3.gameObject.SetActive(false);
        }
        else
        {
            _levelProgress.gameObject.SetActive(true);
            _chest1.gameObject.SetActive(true);
            _chest2.gameObject.SetActive(true);
            _chest3.gameObject.SetActive(true);

            string description = IntegerFormatter.GetMinutes(breakpoints[0].RequiredTime);
            _chest1.Initialize(this, description, breakpoints[0]);

            description = IntegerFormatter.GetMinutes(breakpoints[1].RequiredTime);
            _chest2.Initialize(this, description, breakpoints[1]);

            description = levelIndex < _levels.Count - 1 ? "Chapter " + (levelIndex + 2) : IntegerFormatter.GetMinutes(breakpoints[2].RequiredTime);
            _chest3.Initialize(this, description, breakpoints[2]);

            _levelProgress.Initialize(_currentLevel.LevelRewards);
        }

        if (levelIndex == 0)
        {
            _previousLevelButton.gameObject.SetActive(false);
        }
        else
        {
            _previousLevelButton.gameObject.SetActive(true);
        }

        if (levelIndex == _levels.Count - 1 || (!_isDebug && _currentLevel.wasPassed == false))
        {
            _nextLevelButton.gameObject.SetActive(false);
        }
        else
        {
            _nextLevelButton.gameObject.SetActive(true);
        }
    }
}
