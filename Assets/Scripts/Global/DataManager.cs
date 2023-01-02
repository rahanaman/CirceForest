using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    private static bool _isStart;
    public static bool IsStart
    {
        get { return _isStart; }
    }

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
    }

    private static int _gameTurn;

    public static int GameTurn
    {
        get { return _gameTurn; }
    }

    private static DataBase.State _currentState;

    public static DataBase.State CurrentState
    {
        get { return _currentState; }
    }

    private static int _currentStateData;

    public static int CurrentStateData
    {
        get { return _currentStateData; }
    }
    
    public static void SaveCurrentState(DataBase.State state, int data)
    {
        _currentState = state;
        _currentStateData = data;
    }
    
    public static void SetSelctionData(int value)
    {
        PlayerID = value - 1; //CGID - 1 = PlayerID
        Init();
        LoadDebug();
    }

    public static void PlusPlayerDeck(int value)
    {
        PlayerDeck.Add(value);
        PlayerDeck.Sort();
    }

    private static void Init()
    {
        DataManager.PlayerCurrentHP = DataBase.PlayerMaxHP[DataManager.PlayerID];
        _playerDeck = new List<int>();
        _currentState = DataBase.State.Selection;
        _currentStateData = 0;
        _gameTurn = 0;
        _isStart = true;
    }

    public static void AddTurn()
    {
        _gameTurn++;
    }

    private static void LoadDebug()
    {
        DataManager.PlusPlayerDeck(0);
        DataManager.PlusPlayerDeck(0);
        DataManager.PlusPlayerDeck(0);
        DataManager.PlusPlayerDeck(1);
        DataManager.PlusPlayerDeck(1);
        DataManager.PlusPlayerDeck(1);
    }



}
