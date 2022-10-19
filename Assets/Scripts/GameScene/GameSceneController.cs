using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] Button _종료Button;
    [SerializeField] Button _전체덱Button;
    [SerializeField] GameObject _카드Panel;
    [SerializeField] GameObject _전투Panel;

    private void Start()
    {
        EventManager.CallOnPlayerHP(DataBase.PlayerMaxHP[DataManager.PlayerID], DataManager.PlayerCurrentHP);
        EventManager.SetBattleRoutine += StartBattleRoutine;
        _종료Button.onClick.AddListener(OnClick종료Button);
        _전체덱Button.onClick.AddListener(OnClick전체덱Button);
        EventManager.CallOn전체덱(DataManager.PlayerDeck.Count.ToString());
        
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
    private void OnClick종료Button()
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
    private void OnClick전체덱Button()
    {
        _카드Panel.SetActive(true);
        EventManager.CallOnCardList(DataManager.PlayerDeck);
    }


}
