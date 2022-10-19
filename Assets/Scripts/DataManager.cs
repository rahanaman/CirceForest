using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    private static int _playerCurrentHP;
    public static int PlayerCurrentHP
    {
        get { return _playerCurrentHP; }
        set { _playerCurrentHP = value; }
    }

    private static int _playerID;
    public static int PlayerID
    {
        get { return _playerID; }
        set { _playerID = value; }
    }

    private static List<int> _playerDeck = new List<int>();
    public static List<int> PlayerDeck
    {
        get { return _playerDeck; }
        set { _playerDeck = value; }
    }

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

    
    public static void SetSelctionData(int value)
    {
        PlayerID = value - 1; //CGID - 1 = PlayerID
    }

    public static void PlusPlayerDeck(int value)
    {
        PlayerDeck.Add(value);
        PlayerDeck.Sort();
    }



}
