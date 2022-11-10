using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] Button _종료Button;
    [SerializeField] Button _playerDeckButton;
    [SerializeField] GameObject _cardPanel;
    [SerializeField] GameObject _battlePanel;
    [SerializeField] Button _turnEndButton;
    [SerializeField] Button _unusedDeckButton;
    [SerializeField] GameObject _player;
    private bool _isPlayerTurnEnd;
    private List<GameObject> _enemys = new List<GameObject>();
    private GameSceneManager _gameSceneManager = new GameSceneManager();


    private void Start()
    {
        EventManager.CallOnPlayerHP(DataBase.PlayerMaxHP[DataManager.PlayerID], DataManager.PlayerCurrentHP);
        _종료Button.onClick.AddListener(OnClick종료Button);
        _playerDeckButton.onClick.AddListener(OnClickPlayerDeckButton);
        _unusedDeckButton.onClick.AddListener(OnClickUnusedDeckButton);
        EventManager.CallOnPlayerDeckNum(DataManager.PlayerDeck.Count.ToString());
        EventManager.SetHandCardList += AddHandCardList;
        EventManager.UseObj += UseObj;
        _turnEndButton.onClick.AddListener(OnClickTurnEndButton);
        //EventManager.SetOnClickObj += SetOnClickObj;
        StartBattleRoutine();


    }
    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        EventManager.SetHandCardList -= AddHandCardList;
        EventManager.UseObj -= UseObj;
        //EventManager.SetOnClickObj -= SetOnClickObj;
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
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        _battlePanel.SetActive(true);
        int encounter = Random.Range(0, 2);
        var encounterList = DataBase.EnemyEncounter[encounter];
        var n = encounterList.Count;
        for (int i=0; i < n; i++)
        {
            var enemy = Instantiate(DataLoader.EnemyPref[encounterList[i]], DataBase.EnemyPosition[n][i],Quaternion.identity, _battlePanel.transform);
            enemy.GetComponent<EnemyController>().SetEnemy(DataBase.EnemyIndexList[encounterList[i]]);
            enemy.SetActive(true);
            _enemys.Add(enemy);
        }
        BattleRoutineInit();
        while (true)
        {
            int num = _gameSceneManager.GetDrawCardNum();
            for (int i = 0; i < num; i++)
            {
                _gameSceneManager.DrawCard();
                yield return delay;
            }
            yield return new WaitUntil(() => _isPlayerTurnEnd);
            _isPlayerTurnEnd = false;
            int enemynum = _enemys.Count;
            
            for (int i = 0; i < enemynum; i++)
            {
                float time = _enemys[i].GetComponent<EnemyController>().GetEnemyPattern(_gameSceneManager.TurnNum, _player);
                yield return new WaitForSeconds(time);
                
            }
            _gameSceneManager.AddTurnNum();

        }
        
        
        
        //yield return new WaitForEndOfFrame();
    }
    private void OnClickPlayerDeckButton()
    {
        _cardPanel.SetActive(true);
        GameManager.instance.WasBattle = GameManager.instance.IsBattle;
        GameManager.instance.IsBattle = false;
        EventManager.CallOnCardList(DataManager.PlayerDeck);
    }

    private void OnClickUnusedDeckButton()
    {
        _cardPanel.SetActive(true);
        GameManager.instance.WasBattle = GameManager.instance.IsBattle;
        GameManager.instance.IsBattle = false;
        EventManager.CallOnCardList(_gameSceneManager.UnusedDeck);
    }

    private void BattleRoutineInit()
    {
        _gameSceneManager.Init();
        _isPlayerTurnEnd = false;
        GameManager.instance.IsClick = false;
        GameManager.instance.IsBattle = true;
    }

    private void AddHandCardList(GameObject card)
    {
        _gameSceneManager.AddHandCardList(card);
    }

    private void OnClickTurnEndButton()
    {
        _isPlayerTurnEnd = true;
    }

    private void UseObj(GameObject enemy = null)
    {
        switch (GameManager.instance.OnClickType)
        {
            
            case DataBase.ObjType.TargetingCard:
                _gameSceneManager.UseTargetingCard(GameManager.instance.OnClickObj, _player, enemy);
                

                break;
            case DataBase.ObjType.Card:

                _gameSceneManager.UseCard(GameManager.instance.OnClickObj, _player, _enemys.ToArray());

                break;

        }
    }


}
