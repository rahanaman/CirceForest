using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] Button _Á¾·áButton;
    [SerializeField] Button _playerDeckButton;
    [SerializeField] GameObject _cardPanel;
    [SerializeField] GameObject _battlePanel;
    [SerializeField] Button _turnEndButton;
    [SerializeField] Button _unusedDeckButton;
    [SerializeField] Button _usedDeckButton;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _»ç¸ÁPanel;
    [SerializeField] GameObject _½Â¸®Panel;
    private bool _isPlayerTurnEnd;
    private List<GameObject> _enemys = new List<GameObject>();
    private GameSceneManager _gameSceneManager = new GameSceneManager();
    private IEnumerator _battleRoutine;


    private void Start()
    {
        EventManager.CallOnPlayerHP(DataBase.PlayerMaxHP[DataManager.PlayerID], DataManager.PlayerCurrentHP);
        _Á¾·áButton.onClick.AddListener(OnClickÁ¾·áButton);
        _playerDeckButton.onClick.AddListener(OnClickPlayerDeckButton);
        _unusedDeckButton.onClick.AddListener(OnClickUnusedDeckButton);
        _usedDeckButton.onClick.AddListener(OnClickUsedDeckButton);
        EventManager.CallOnPlayerDeckNum(DataManager.PlayerDeck.Count.ToString());
        EventManager.SetHandCardList += AddHandCardList;
        EventManager.UseObj += UseObj;
        EventManager.SetCheckBattle += CheckBattle;
        _turnEndButton.onClick.AddListener(OnClickTurnEndButton);
        //EventManager.SetOnClickObj += SetOnClickObj;
        _battleRoutine = BattleRoutine();
        StartCoroutine(_battleRoutine);


    }


    private void OnDestroy()
    {
        EventManager.SetHandCardList -= AddHandCardList;
        EventManager.UseObj -= UseObj;
        //EventManager.SetOnClickObj -= SetOnClickObj;
    }
    
    private void OnClickÁ¾·áButton()
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
        Debug.Log(_enemys.Count);
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
        List<int> list = new List<int>();
        foreach(var i in _gameSceneManager.UnusedDeck)
        {
            list.Add(i.GetComponent<CardController>().GetData());
        }
        EventManager.CallOnCardList(list);
    }

    private void OnClickUsedDeckButton()
    {
        _cardPanel.SetActive(true);
        GameManager.instance.WasBattle = GameManager.instance.IsBattle;
        GameManager.instance.IsBattle = false;
        List<int> list = new List<int>();
        foreach (var i in _gameSceneManager.UsedDeck)
        {
            list.Add(i.GetComponent<CardController>().GetData());
        }
        EventManager.CallOnCardList(list);
    }

    private void BattleRoutineEnd()
    {
        _gameSceneManager.End();
        if (!_player.GetComponent<PlayerController>().IsAlive()) // ÇÃ·¹ÀÌ¾î Á×À½
        {
            _»ç¸ÁPanel.SetActive(true);
        }
        else
        {
            _½Â¸®Panel.SetActive(true);
        }
    }

    private void BattleRoutineInit()
    {
        _gameSceneManager.Init();
        _isPlayerTurnEnd = false;
        GameManager.instance.IsClick = false;
        GameManager.instance.IsBattle = true;
        InitCardDeck();
    }

    private void InitCardDeck()
    {
        int n = DataManager.PlayerDeck.Count;
        for (int i = 0; i < n; i++)
        {
            GameObject card = Instantiate(DataLoader.CardPref[DataManager.PlayerDeck[i]]);
            card.GetComponent<CardController>().SetCard(DataBase.CardList[DataManager.PlayerDeck[i]]);
            _gameSceneManager.InitCardDeck(card);
        }
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

    private void CheckBattle(GameObject enemy)
    {
        if(enemy == _player)
        {
            StopCoroutine(_battleRoutine);
            BattleRoutineEnd();
        }
        else
        {
            _enemys.Remove(enemy);
            Debug.Log(_enemys.Count);
            if (_enemys.Count == 0)
            {
                StopCoroutine(_battleRoutine);
                BattleRoutineEnd();
            }
        }
        
    }


}
