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
