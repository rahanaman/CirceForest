using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttonforall : MonoBehaviour
{
    [SerializeField] Button _���ϴ�Button;
    private void Start()
    {
        _���ϴ�Button.onClick.AddListener(A);
    }

    private void A()
    {
        DataManager.PlayerCurrentHP -= 5;
        EventManager.CallOnPlayerHp(DataManager.PlayerCurrentHP, DataManager.PlayerMaxHP);
        EventManager.CallOnPlayerAnim(1);
    }

}
