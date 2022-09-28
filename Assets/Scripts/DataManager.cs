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
    // 아폴로 0 스킬라 1 디아나 2

    public static void SetSelctionData(int value)
    {
        PlayerID = value - 1; //CGID - 1 = PlayerID
    }

    public static void CardPattern(int value, GameObject Enemy)
    {
        switch (value)
        {
            case 0:
                CardPatternData.CardAttack(Enemy);
                break;
        }

    }


}
