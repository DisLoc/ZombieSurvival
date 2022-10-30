using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFatZombie : MonoBehaviour
{
    [SerializeField] private Zombie _zombie;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _zombie.Die();
        }
    }


}
