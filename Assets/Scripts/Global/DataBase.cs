using System.Collections;
using System.Collections.Generic;

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

    public static List<CardData> CardList;

    public static List<EnemyData> EnemyIndexList;

    public static List<List<int>> EnemyEncounter = new List<List<int>>();

}
