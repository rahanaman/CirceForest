using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttonforall : MonoBehaviour
{
    [SerializeField] Button _���ϴ�Button;
    private void Start()
    {
        _���ϴ�Button.onClick.AddListener(D);
    }

    private void A()
    {
        DataManager.PlayerCurrentHP -= 5;
        EventManager.CallOnPlayerHP(DataManager.PlayerCurrentHP, DataBase.PlayerMaxHP[0]);
        EventManager.CallOnPlayerAnim(1);
    }

    private void B()
    {
        EventManager.CallOnSoundID(DataBase.SoundID.DianaSelection, 5);
    }

    private void C()
    {
        EventManager.CallOnBattleRoutine();
    }
    private void D()
    {
        //EventManager.CallOnCard(1);
        
    }
}
