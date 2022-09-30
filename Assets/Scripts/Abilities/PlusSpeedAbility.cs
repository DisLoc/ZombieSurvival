using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusSpeedAbility : MonoBehaviour
{
    private MoveSpeed _moveSpeed;
    Upgrade upgrade;

    public void UseAbility()
    {
        _moveSpeed.Upgrade(upgrade);
    }

}
