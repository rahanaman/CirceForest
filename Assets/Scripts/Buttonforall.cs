using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttonforall : MonoBehaviour
{
    [SerializeField] Button _다하는Button;
    private void Start()
    {
        _다하는Button.onClick.AddListener(A);
    }

    private void A()
    {
        DataManager.PlayerCurrentHP -= 5;
        EventManager.CallOnPlayerHp(DataManager.PlayerCurrentHP, DataManager.PlayerMaxHP);
        EventManager.CallOnPlayerAnim(1);
    }

}
