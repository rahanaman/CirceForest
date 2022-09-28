using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] Button _����Button;
    private void Start()
    {
        DataManager.PlayerCurrentHP = DataBase.PlayerMaxHP[DataManager.PlayerID];
        EventManager.CallOnPlayerHP(DataBase.PlayerMaxHP[DataManager.PlayerID], DataManager.PlayerCurrentHP);
        _����Button.onClick.AddListener(OnClick����Button);
    }

    private void OnClick����Button()
    {
        SceneManager.LoadScene("MainScene");
    }

}
