using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusDamageAbility : MonoBehaviour
{
    private Damage _damage;
    Upgrade upgrade;

    public void UseAbility()
    {
        _damage.Upgrade(upgrade);
    }
}
