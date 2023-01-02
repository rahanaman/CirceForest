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
    [SerializeField] GameObject _selectionPanel;
    [SerializeField] Button _turnEndButton;
    [SerializeField] Button _unusedDeckButton;
    [SerializeField] Button _usedDeckButton;
    [SerializeField] List<Button> _selectionButton;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _사망Panel;
    [SerializeField] GameObject _승리Panel;
    [SerializeField] GameObject _backgroundPanel;
    [SerializeField] Image _fade;
    private bool _isSelected;
    private bool _isPlayerTurnEnd;
    private List<GameObject> _enemys = new List<GameObject>();
    private GameSceneManager _gameSceneManager = new GameSceneManager();
    private IEnumerator _battleRoutine;
    private IEnumerator _selectionRoutine;

    private List<GameObject> _cards;


    private void Start()
    {
        EventManager.CallOnPlayerHP(DataBase.PlayerMaxHP[DataManager.PlayerID], DataManager.PlayerCurrentHP);
        _종료Button.onClick.AddListener(OnClick종료Button);
        _playerDeckButton.onClick.AddListener(OnClickPlayerDeckButton);
        _unusedDeckButton.onClick.AddListener(OnClickUnusedDeckButton);
        _usedDeckButton.onClick.AddListener(OnClickUsedDeckButton);
        EventManager.CallOnPlayerDeckNum(DataManager.PlayerDeck.Count.ToString());
        EventManager.SetHandCardList += AddHandCardList;
        EventManager.UseObj += UseObj;
        EventManager.SetCheckBattle += CheckBattle;
        EventManager.SetEndBattle += EndBattlePanel;
        _turnEndButton.onClick.AddListener(OnClickTurnEndButton);
        _cards = new List<GameObject>();


        for (int i = 0; i < _selectionButton.Count; i++)
        {
            int index = i;
            _selectionButton[i].onClick.AddListener(()=> OnClickSelectionButton(index));
        }
        _isSelected = false;

        CheckCurrentState();


    }
    void CheckCurrentState() // 세이브 정보 읽어오기
    {
        _battleRoutine = BattleRoutine();
        _selectionRoutine = SelectionRoutine();
        switch (DataManager.CurrentState)
        {
            case DataBase.State.Selection:
                StartSelectionRoutine();
                break;
            case DataBase.State.Battle:
                StartBattleRoutine();
                break;
        }
    }

    private void StartSelectionRoutine()
    {
        _selectionPanel.SetActive(true);
        StartCoroutine(_selectionRoutine);
    }
    private void StartBattleRoutine()
    {
        _battlePanel.SetActive(true);
        StartCoroutine(_battleRoutine);
    }

    private void OnDestroy()
    {
        EventManager.SetHandCardList -= AddHandCardList;
        EventManager.UseObj -= UseObj;
        EventManager.SetEndBattle -= EndBattlePanel;
        EventManager.SetCheckBattle -= CheckBattle;
    }
    
    private void OnClick종료Button()
    {
        SceneManager.LoadScene("MainScene");

    }

    private void CheckSelection()
    {
        var data = DataBase.SelectionList[DataManager.CurrentStateData];
        int num = data.Count;
        for (int i = 0; i < num; i++)
        {
            if(data[i] == DataBase.State.None)
            {
                //_selectionButton[i].gameObject.SetActive(false); // 나중에 살리기
            }
            else
            {
                _selectionButton[i].gameObject.SetActive(true);
            }
        }

    }

    private void OnClickSelectionButton(int value)
    {
        var data = DataBase.SelectionList[DataManager.CurrentStateData];
        DataBase.State state = data[value];
        if(state == DataBase.State.None)
        {
            Debug.Log("None");
            return;
        }
        int stateData = DataBase.SetStateData(state);
        DataManager.SaveCurrentState(state, stateData);
        _isSelected = true;
        

    }

    private IEnumerator FadeOut()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        _fade.gameObject.SetActive(true);
        float alpha = 0f;
        float deltaAlpha = 0.005f;
        _fade.color = new Color(0, 0, 0, alpha);
        while (alpha < 1.0f)
        {
            yield return waitForEndOfFrame;
            alpha += deltaAlpha;
            _fade.color = new Color(0, 0, 0, alpha);
        }
        
    }
    private IEnumerator FadeIn()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        float alpha = 0f;
        float deltaAlpha = 0.005f;
        while (alpha > 0)
        {
            yield return waitForEndOfFrame;
            alpha -= deltaAlpha;
            _fade.color = new Color(0, 0, 0, alpha);
        }
        _fade.gameObject.SetActive(false);
    }


    private IEnumerator SelectionRoutine()
    {
        CheckSelection();
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        _backgroundPanel.GetComponent<RectTransform>().localPosition = new Vector3(960, 0, 0);
        Vector3 speed = new Vector3(-700f, 0, 0);
        while (_backgroundPanel.GetComponent<RectTransform>().localPosition.x > -959.99f)
        {
            yield return waitForEndOfFrame;
            
            _backgroundPanel.GetComponent<RectTransform>().localPosition += speed * Time.deltaTime;
        }
        yield return new WaitUntil(() => _isSelected);
        _isSelected = false;
        yield return StartCoroutine(FadeOut());
        _selectionPanel.SetActive(false);
        yield return StartCoroutine(FadeIn());

        CheckCurrentState();

    }
    private IEnumerator BattleRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        _battlePanel.SetActive(true);
        int encounter = Random.Range(0, 2); // 적 encounter 
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
        InitBattleRoutine();
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
    } // 전체 덱 불러오기

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
        for (int i = 0; i < _cards.Count; i++)
        {
            Destroy(_cards[i]);
        }
        _gameSceneManager.End();
        if (!_player.GetComponent<PlayerController>().IsAlive()) // 플레이어 죽음
        {
            _사망Panel.SetActive(true);
        }
        else
        {
            _승리Panel.SetActive(true);
        }
    }

    private void EndBattlePanel()
    {
        _battlePanel.SetActive(false);
        int stateData = DataBase.SetStateData(DataBase.State.Selection);
        DataManager.SaveCurrentState(DataBase.State.Selection, stateData);
        DataManager.AddTurn();
        CheckCurrentState();
    }

    private void InitBattleRoutine()
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
        _cards = new List<GameObject>();
        for (int i = 0; i < n; i++)
        {
            GameObject card = Instantiate(DataLoader.CardPref[DataManager.PlayerDeck[i]]);
            card.GetComponent<CardController>().SetCard(DataBase.CardList[DataManager.PlayerDeck[i]]);
            _cards.Add(card);
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
