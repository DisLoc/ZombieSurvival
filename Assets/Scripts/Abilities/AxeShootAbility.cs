using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeShootAbility : MonoBehaviour
{
    
    [SerializeField] protected GameObject projectile;

    [SerializeField] private Transform shootPosition;

    private int _coolDown;

    private void Start()
    {
        StartCoroutine(Shoot());
    }


    public void UseState()
    {
        Instantiate(projectile, shootPosition.position, Quaternion.identity);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolDown);
            UseState();
        }
    }

    public void SetCoolDown(int coolDown)
    {
        _coolDown = coolDown;
    }
}
