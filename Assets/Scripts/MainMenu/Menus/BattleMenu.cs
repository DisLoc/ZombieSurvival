using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenu : UIMenu
{

    public void StartGame()
    {
        if (_isDebug) Debug.Log("Start");
    }
}
