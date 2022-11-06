using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public static DataLoader instance;
    [SerializeField] private GameObject _cardCon;
    [SerializeField] private GameObject _enemy;
    private void Awake()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        LoadData();
        DontDestroyOnLoad(gameObject);
    }

    private void LoadData()
    {
        LoadDataBase();
        LoadEnemyData();
        LoadCardData();
        LoadSprite();
        LoadPrefs();
        LoadDebug();
    }
    private void LoadEnemyData() /// 테스트용 아가들임.
    {
        EnemyData enemyData = new EnemyData();
        enemyData.EnemyId = 0;
        enemyData.EnemyHP = 50;
        enemyData.EnemyPatternData = new int[4] {6,5,6,6};
        enemyData.EnemyPatternList = new List<DataBase.EnemyPattern> { DataBase.EnemyPattern.Attack, DataBase.EnemyPattern.Attack, DataBase.EnemyPattern.Defence, DataBase.EnemyPattern.AttackAndDefence };
        DataBase.EnemyIndexList.Add(enemyData);
        EnemyData enemyData1 = new EnemyData();
        enemyData1.EnemyId = 1;
        enemyData1.EnemyHP = 60;
        enemyData1.EnemyPatternData = new int[4] { 7, 5, 6, 6 };
        enemyData1.EnemyPatternList = new List<DataBase.EnemyPattern> { DataBase.EnemyPattern.Attack, DataBase.EnemyPattern.Attack, DataBase.EnemyPattern.Defence, DataBase.EnemyPattern.AttackAndDefence };
        DataBase.EnemyIndexList.Add(enemyData1);
    }
    private void LoadCardData()
    {
        CardData cardData = new CardData();
        CardData cardData1 = new CardData();
        cardData.CardName = "Attack";
        cardData.CardDesc = "hurt";
        cardData.CardId = 0;
        cardData.CardCost = 1;
        cardData.CardIsSub = false;
        cardData.CardPatternData.Add(10);
        DataBase.CardList.Add(cardData);
        cardData1.CardName = "Defence";
        cardData1.CardDesc = "T.T";
        cardData1.CardId = 1;
        cardData1.CardCost = 1;
        cardData1.CardIsSub = true;
        cardData1.CardSubDesc = "Defence is defence";
        cardData1.CardPatternData.Add(5);
        DataBase.CardList.Add(cardData1);

    }

    private void LoadDataBase()
    {
        DataBase.EnemyEncounter.Add(new List<int> { 0, 1 });
        DataBase.EnemyEncounter.Add(new List<int> { 0, 1 });
        DataBase.EnemyPosition.Add(new List<Vector2> { });
        DataBase.EnemyPosition.Add(new List<Vector2> {new Vector2(3.0f,2.6f)});
        DataBase.EnemyPosition.Add(new List<Vector2> {new Vector2(3.0f,2.6f), new Vector2(10.0f,2.6f)});
    }

    private void LoadSprite()
    {
        DataBase.StartSceneBackgroundCG = Resources.LoadAll<Sprite>("SelectionCG");
        DataBase.CardIcon = Resources.LoadAll<Sprite>("CardIcon");
        DataBase.EnemySprite = Resources.LoadAll<Sprite>("Enemy");
    }

    private void LoadPrefs()
    {
        int n = DataBase.CardList.Count;
        for (int i = 0; i < n; i++)
        {
            GameObject card = Instantiate(_cardCon,gameObject.transform);
            card.GetComponent<CardView>().SetCardUI(DataBase.CardList[i], DataBase.CardIcon[i]);
            card.GetComponent<CardController>().SetCard(DataBase.CardList[i]);
            CardPref.Add(card);
            card.SetActive(false);
        }
        n = DataBase.EnemyIndexList.Count;
        for(int i = 0; i < n; i++)
        {
            GameObject enemy = Instantiate(_enemy,gameObject.transform);
            enemy.GetComponent<EnemyView>().SetEnemyUI(DataBase.EnemyIndexList[i], DataBase.EnemySprite[i]);
            enemy.GetComponent<EnemyController>().SetEnemy(DataBase.EnemyIndexList[i]);
            EnemyPref.Add(enemy);
            enemy.SetActive(false);
        }

    }

    private static List<GameObject> _cardPref = new List<GameObject>();
    public static List<GameObject> CardPref
    {
        get { return _cardPref; }
        set { _cardPref = value; }
    }

    private static List<GameObject> _enemyPref = new List<GameObject>();
    public static List <GameObject> EnemyPref
    {
        get { return _enemyPref; }
    }

    private void LoadDebug()
    {
        DataManager.PlusPlayerDeck(0);
        DataManager.PlusPlayerDeck(0);
        DataManager.PlusPlayerDeck(0);
        DataManager.PlusPlayerDeck(1);
        DataManager.PlusPlayerDeck(1);
        DataManager.PlusPlayerDeck(1);
        DataManager.PlusPlayerDeck(1);
        DataManager.PlusPlayerDeck(1);
        DataManager.PlusPlayerDeck(1);
        DataManager.PlusPlayerDeck(1);
        DataManager.PlusPlayerDeck(1);
        DataManager.PlusPlayerDeck(1);
    }
}
