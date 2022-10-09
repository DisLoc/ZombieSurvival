using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> zombies = new List<GameObject>();

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 5, 5);
    }
    
    private void Spawn()
    {
        for (int i = 0; i < zombies.Count; i++)
        {
            Instantiate(zombies[Random.Range(0, zombies.Count)], new Vector3(0, 5, 0), Quaternion.identity);
        }


    }
}
