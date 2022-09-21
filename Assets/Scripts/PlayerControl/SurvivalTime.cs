using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalTime : MonoBehaviour
{

    [SerializeField] private UIController _uIController;

    private Text _survivalTimeCountText;

    private int _survivalTimeCount;
    void Start()
    {
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
        }
    }
}
