using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Sprite _playerSprite;
    [SerializeField] private TextMeshProUGUI _playerHpText;
    [SerializeField] private Image _playerHpBar;
    [SerializeField] private GameObject _playerDefence;
    [SerializeField] private Animator _playerAnim;
    private void Awake()
    {
        EventManager.SetPlayerHP += SetPlayerHPUI;
        EventManager.SetPlayerAnim += SetPlayerAnim;
    }

    private void SetPlayerHPUI(int value1, int value2)
    {
        _playerHpBar.fillAmount = (float)value1 / value2;
        _playerHpText.text = value1.ToString() + "/" + value2.ToString();
    }

    private void SetPlayerAnim(int value1)
    {
        switch (value1)
        {
            case 1:
                _playerAnim.SetTrigger("IsHit");
                break;
        }
    }


}
