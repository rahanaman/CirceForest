using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static int PlayerMaxHP = 100;
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

    public static void SetSelctionData(int value)
    {
        PlayerID = value;
    }


}
