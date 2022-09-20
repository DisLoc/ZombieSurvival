using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private GameObject _groundPrefab;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
