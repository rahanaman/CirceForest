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

    public static List<CardData> CardList = new List<CardData>();

    public static List<EnemyData> EnemyIndexList = new List<EnemyData>();

    public static List<List<int>> EnemyEncounter = new List<List<int>>();



    public static void CardPattern(GameObject Card, GameObject Enemy) //³­¸®³²
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
