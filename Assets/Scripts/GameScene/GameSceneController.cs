using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] Button _종료button;
    private void Start()
    {
        DataManager.PlayerCurrentHP = DataManager.PlayerMaxHP;
        EventManager.CallOnPlayerHP(DataManager.PlayerMaxHP, DataManager.PlayerCurrentHP);
        _종료button.onClick.AddListener(OnClick종료Button);
    }

    private void OnClick종료Button()
    {
        SceneManager.LoadScene("MainScene");
    }

}
