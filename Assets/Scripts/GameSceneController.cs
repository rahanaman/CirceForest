using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    private void Start()
    {
        DataManager.PlayerCurrentHP = DataManager.PlayerMaxHP;
        EventManager.CallOnPlayerHp(DataManager.PlayerMaxHP, DataManager.PlayerCurrentHP);
    }
}
