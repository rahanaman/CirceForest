using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] Button _����button;
    private void Start()
    {
        DataManager.PlayerCurrentHP = DataManager.PlayerMaxHP;
        EventManager.CallOnPlayerHP(DataManager.PlayerMaxHP, DataManager.PlayerCurrentHP);
        _����button.onClick.AddListener(OnClick����Button);
    }

    private void OnClick����Button()
    {
        SceneManager.LoadScene("MainScene");
    }

}
