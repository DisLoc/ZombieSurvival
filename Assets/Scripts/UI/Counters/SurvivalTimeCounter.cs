using System;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalTimeCounter : MonoBehaviour, IGameStartHandler
{
    [SerializeField] private LevelProgress _levelProgress;
    [SerializeField] private Text _survivalTimeText;

    private float _survivalTime;

    public float SurvivalTime => _survivalTime;

    public void OnGameStart()
    {
        _survivalTime = 0f;
        UpdateTimerText();
    }

    private void FixedUpdate()
    {
        _survivalTime += Time.fixedDeltaTime;

        _levelProgress.OnTimerUpdate();

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        _survivalTimeText.text = IntegerFormatter.GetTime((int)_survivalTime);
    }
}
