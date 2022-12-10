using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GOGameOverMenu : UIMenu
{
    [Header("Game over menu settings")]
    [SerializeField] private SoundList _sounds;
    [SerializeField] private ZombieCounter _enemyCounter;
    [SerializeField] private SurvivalTimeCounter _survivalTimeCounter;

    [SerializeField] private Text _totalKilledText;
    [SerializeField] private Text _survivalTimeText;

    public override void Display(bool playAnimation = false)
    {
        base.Display(playAnimation);

        _totalKilledText.text = _enemyCounter.TotalKilled.ToString();
        _survivalTimeText.text = IntegerFormatter.GetTime((int)_survivalTimeCounter.SurvivalTime);

        (_mainMenu as GameMenu).SaveLevel();

        _sounds.PlaySound(SoundTypes.GameOver);
    }
}
