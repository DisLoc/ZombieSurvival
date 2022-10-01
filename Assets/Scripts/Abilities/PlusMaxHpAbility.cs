using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusMaxHpAbility : MonoBehaviour
{
    private HealthPoint _healthpoint;
    Upgrade upgrade;

    public void UseAbility()
    {
        _healthpoint.Upgrade(upgrade);
 
    }

}
