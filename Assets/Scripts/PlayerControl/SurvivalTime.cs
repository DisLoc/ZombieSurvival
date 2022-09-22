using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalTime : MonoBehaviour, IGameStartHandler
{

    [SerializeField] private UIController _uIController;

    private Text _survivalTimeCountText;

    private int _survivalTimeCount;
    private int _seconds;

    void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        StopAllCoroutines();

        _survivalTimeCountText = _uIController.SurvivalTimeCountText;
        StartCoroutine(PlusTimeCount());
    }

    private IEnumerator PlusTimeCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _survivalTimeCount++;
            _survivalTimeCountText.text = _survivalTimeCount.ToString();
            
            _seconds++;
            if (_seconds >= 60)
            {
                _seconds = 0;
                EventBus.Publish<IMinuteLeftHandler>(handler => handler.OnMinuteLeft());
            }
        }
    }
}
