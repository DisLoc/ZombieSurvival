using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalTime : MonoBehaviour, IGameStartHandler
{
    [SerializeField] private Text _survivalTimeCountText;

    private int _survivalTimeCount;
    private int _seconds;

    void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        StopAllCoroutines();

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
