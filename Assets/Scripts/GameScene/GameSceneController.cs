using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] Button _����Button;
    [SerializeField] Button _��ü��Button;
    [SerializeField] GameObject _ī��Panel;
    [SerializeField] GameObject _����Panel;

    private void Start()
    {
        EventManager.CallOnPlayerHP(DataBase.PlayerMaxHP[DataManager.PlayerID], DataManager.PlayerCurrentHP);
        EventManager.SetBattleRoutine += StartBattleRoutine;
        _����Button.onClick.AddListener(OnClick����Button);
        _��ü��Button.onClick.AddListener(OnClick��ü��Button);
        EventManager.CallOn��ü��(DataManager.PlayerDeck.Count.ToString());
        
    }
    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        EventManager.SetBattleRoutine -= StartBattleRoutine;
    }
    private void Init()
    {
        DataManager.PlayerCurrentHP = DataBase.PlayerMaxHP[DataManager.PlayerID];
        
    }
    private void OnClick����Button()
    {
        SceneManager.LoadScene("MainScene");

    }
    private void StartBattleRoutine()
    {
        StartCoroutine(BattleRoutine());
    }
    private IEnumerator BattleRoutine()
    {
        yield return new WaitForEndOfFrame();
    }
    private void OnClick��ü��Button()
    {
        _ī��Panel.SetActive(true);
        EventManager.CallOnCardList(DataManager.PlayerDeck);
    }


}
