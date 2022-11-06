using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataBase
{
    public static int[] PlayerMaxHP = new int[]{ 100, 101, 102 };

    public enum SoundID
    {
        MainSceneBgm,
        ApolloSelection,
        ScyllaSelection,
        DianaSelection,
        UISound1
    };

    public enum EnemyPattern
    {
        Attack, // 0
        Defence, //1
        AttackAndDefence //2,3
    };

    private static Sprite[] _startSceneBackgroundCG = new Sprite[] { };
    public static Sprite[] StartSceneBackgroundCG
    {
        get { return _startSceneBackgroundCG; }
        set { _startSceneBackgroundCG = value; }
    }
    // 아폴로 0 스킬라 1 디아나 2

    private static Sprite[] _cardIcon = new Sprite[] { };
    public static Sprite[] CardIcon
    {
        get { return _cardIcon; }
        set { _cardIcon = value; }
    }

    private static Sprite[] _enemySprite = new Sprite[] { };

    public static Sprite[] EnemySprite
    {
        get { return _enemySprite; }
        set { _enemySprite = value; }
    }

    public static List<CardData> CardList = new List<CardData>();

    public static List<EnemyData> EnemyIndexList = new List<EnemyData>();

    public static List<List<int>> EnemyEncounter = new List<List<int>>();
    public static List<List<Vector2>> EnemyPosition = new List<List<Vector2>>();


    public static void CardPattern(GameObject Card, GameObject Enemy) //난리남
    {
        int value = Card.GetComponent<int>();
        switch (value)
        {
            case 0:
                CardPatternData.CardAttack(Enemy);
                break;
        }

    }




}
