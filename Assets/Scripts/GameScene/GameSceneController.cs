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
    [SerializeField] GameObject _turnEndButton;
    [SerializeField] Button _unusedDeckButton;
    private bool _isPlayerTurnEnd;
    private List<GameObject> _enemys = new List<GameObject>();
    private GameSceneManager _gameSceneManager = new GameSceneManager();
    private GameObject _onClickCard;

    private void Start()
    {
        EventManager.CallOnPlayerHP(DataBase.PlayerMaxHP[DataManager.PlayerID], DataManager.PlayerCurrentHP);
        _종료Button.onClick.AddListener(OnClick종료Button);
        _playerDeckButton.onClick.AddListener(OnClickPlayerDeckButton);
        _unusedDeckButton.onClick.AddListener(OnClickUnusedDeckButton);
        EventManager.CallOnPlayerDeckNum(DataManager.PlayerDeck.Count.ToString());
        EventManager.SetHandCardList += AddHandCardList;
        EventManager.SetOnClickCard += SetOnClickCard;
        StartBattleRoutine();


    }
    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        EventManager.SetHandCardList -= AddHandCardList;
        EventManager.SetOnClickCard -= SetOnClickCard;
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
        }
        
        
        
        //yield return new WaitForEndOfFrame();
    }
    private void OnClickPlayerDeckButton()
    {
        _cardPanel.SetActive(true);
        GameManager.instance.IsCardPanel = true;
        EventManager.CallOnCardList(DataManager.PlayerDeck);
    }

    private void OnClickUnusedDeckButton()
    {
        _cardPanel.SetActive(true);
        GameManager.instance.IsCardPanel = true;
        EventManager.CallOnCardList(_gameSceneManager.UnusedDeck);
    }

    private void BattleRoutineInit()
    {
        _gameSceneManager.Init();
        _isPlayerTurnEnd = false;
        GameManager.instance.IsCardClick = false;
    }

    private void AddHandCardList(GameObject card)
    {
        _gameSceneManager.AddHandCardList(card);
    }

    private void SetOnClickCard(GameObject card)
    {
        if(card == null)
        {
            _onClickCard = null;
        }
        else
        {
            _onClickCard = card;
        }
    }

}
