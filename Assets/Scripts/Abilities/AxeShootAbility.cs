using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeShootAbility : MonoBehaviour
{

    [SerializeField] protected GameObject projectile;

    [SerializeField] private Transform shootPosition;


    public void UseState()
    {
        Instantiate(projectile, shootPosition.position, Quaternion.identity);
    }
}
