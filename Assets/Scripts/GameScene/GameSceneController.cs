using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] Button _종료Button;
    private void Start()
    {
        DataManager.PlayerCurrentHP = DataBase.PlayerMaxHP[DataManager.PlayerID];
        EventManager.CallOnPlayerHP(DataBase.PlayerMaxHP[DataManager.PlayerID], DataManager.PlayerCurrentHP);
        _종료Button.onClick.AddListener(OnClick종료Button);
    }

    private void OnClick종료Button()
    {
        SceneManager.LoadScene("MainScene");
    }

}
