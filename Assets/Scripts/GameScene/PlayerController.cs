using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerManager _playerManager = new PlayerManager();

    public void Start()
    {
        _playerManager.ResetDefence();
    }
    public void GetDamage(int damage)
    {

        _playerManager.GetDamage(damage);
        EventManager.CallOnPlayerDefence(_playerManager.Defence);
        EventManager.CallOnPlayerHP(DataManager.PlayerCurrentHP, DataBase.PlayerMaxHP[DataManager.PlayerID]);
        EventManager.CallOnPlayerAnim(1);
    }

    public void GetDefence(int defence)
    {
        _playerManager.GetDefence(defence);
        EventManager.CallOnPlayerDefence(_playerManager.Defence);

    }
       
    
}
