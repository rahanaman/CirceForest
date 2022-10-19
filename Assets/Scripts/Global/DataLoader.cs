using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public static DataLoader instance;
    [SerializeField] private GameObject _cardCon;
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
        LoadEnemyData();
        LoadCardData();
        LoadSprite();
        LoadCardCon();
        LoadDebug();
    }
    private void LoadEnemyData() /// 테스트용 아가들임.
    {
        EnemyData enemyData = new EnemyData();
        enemyData.EnemyHP = 50;
        enemyData.EnemyPatternData = new int[4] {6,5,6,6};
        enemyData.EnemyPatternList = new List<DataBase.EnemyPattern> { DataBase.EnemyPattern.Attack, DataBase.EnemyPattern.Attack, DataBase.EnemyPattern.Defence, DataBase.EnemyPattern.AttackAndDefence };
        DataBase.EnemyIndexList.Add(enemyData);
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
        cardData1.CardIsSub = false;
        cardData1.CardPatternData.Add(5);
        DataBase.CardList.Add(cardData1);

    }

    private void LoadSprite()
    {
        DataManager.StartSceneBackgroundCG = Resources.LoadAll<Sprite>("SelectionCG");
        DataManager.CardIcon = Resources.LoadAll<Sprite>("CardIcon");
    }

    private void LoadCardCon()
    {
        int n = DataBase.CardList.Count;
        for (int i = 0; i < n; i++)
        {
            GameObject card = Instantiate(_cardCon,gameObject.transform);
            card.GetComponent<CardView>().SetCardUI(DataBase.CardList[i], DataManager.CardIcon[i]);
            card.GetComponent<CardManager>().SetCardData(DataBase.CardList[i]);
            CardPref.Add(card);
            card.SetActive(false);
        }
    }

    private static List<GameObject> _cardPref = new List<GameObject>();
    public static List<GameObject> CardPref
    {
        get { return _cardPref; }
        set { _cardPref = value; }
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
    }
}
