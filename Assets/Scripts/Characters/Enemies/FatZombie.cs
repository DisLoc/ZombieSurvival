using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatZombie : Zombie
{
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            Die();
        }
    }
}
