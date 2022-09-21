using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private GameObject _groundPrefab;
    

    [Header("Mobs")]
    [SerializeField] private Transform _player;

    private Vector3 _startPlayerPosition;

    private void OnEnable()
    {
        _startPlayerPosition = _player.position;
    }

    private void Update()
    {
        if((_startPlayerPosition.x - _player.position.x) / -3.3f == 1)
        {
            Instantiate(_groundPrefab, _player.position, Quaternion.identity);
        }
    }

    private void OnDisable()
    {
        
    }
}
