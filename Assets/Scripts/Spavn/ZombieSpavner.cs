using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpavner : MonoBehaviour
{
    [SerializeField] private List<GameObject> zombies = new List<GameObject>();

    void Start()
    {
        InvokeRepeating(nameof(Spavn), 5, 5);
    }
    
    private void Spavn()
    {
        for (int i = 0; i < zombies.Count; i++)
        {
            Instantiate(zombies[Random.Range(0, zombies.Count)], transform.position - new Vector3(15, 4, 0), Quaternion.identity);
        }


    }
}
