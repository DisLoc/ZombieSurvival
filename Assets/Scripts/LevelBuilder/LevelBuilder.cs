using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private List<GameObject> _groundPrefabs;

    
    [Header("Mobs")]
    [SerializeField] private Transform _player;

    [SerializeField]private int _generatedCountMinusX = 0;
    [SerializeField] private int _generatedCountPlusX = 0;

    [SerializeField] private int _generatedCountMinusZ = 0;
    [SerializeField] private int _generatedCountPlusZ = 0;

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        StartCoroutine(SetZero());
    }

    private void Update()
    {
        //if (_player.position.x % 3f != 0 || _player.position.x % -3 != 0)
        //{
        //    if (_player.position.x / 3f > _generatedCountPlusX)
        //    {
        //        Instantiate(_groundPrefabs[Random.Range(0, _groundPrefabs.Count)], _player.position - new Vector3(0, 1.48f, 0), Quaternion.identity);
        //        _generatedCountPlusX++;
        //        _generatedCountPlusZ = 1;

        //    }

        //    if (_player.position.x / -3f > _generatedCountMinusX)
        //    {
        //        Instantiate(_groundPrefabs[Random.Range(0, _groundPrefabs.Count)], _player.position - new Vector3(0, 1.48f, 0), Quaternion.identity);
        //        _generatedCountMinusX++;
        //        _generatedCountMinusZ = 1;


        //    }
        //}

        //if (_player.position.z % 3 != 0 || _player.position.z % -3 != 0)
        //{
        //    if (_player.position.z / 3 > _generatedCountPlusZ)
        //    {
        //        Instantiate(_groundPrefabs[Random.Range(0, _groundPrefabs.Count)], _player.position - new Vector3(0, 1.48f, 0), Quaternion.identity);
        //        _generatedCountPlusZ++;
        //        _generatedCountPlusX = 1;

        //    }

        //    if (_player.position.z / -3 > _generatedCountMinusZ)
        //    {
        //        Instantiate(_groundPrefabs[Random.Range(0, _groundPrefabs.Count)], _player.position - new Vector3(0, 1.48f, 0), Quaternion.identity);
        //        _generatedCountMinusZ++;
        //        _generatedCountMinusX = 1;

        //    }
        //}
    }


    private IEnumerator SetZero()
    {
        yield return new WaitForSeconds(0.1f);
        _generatedCountPlusX = 0;
    }


    private void OnDisable()
    {
        
    }
}
