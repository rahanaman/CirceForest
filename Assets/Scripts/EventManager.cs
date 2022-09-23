using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void Int1Event(int vlaue1);
    public delegate void Int2Event(int value1, int value2);


    public static Int2Event SetPlayerHp;
    public static Int1Event SetPlayerAnim;
    public static Int1Event SetSelectionBG;


    public static void CallOnPlayerHp(int value1, int value2)
    {
        SetPlayerHp?.Invoke(value1, value2);
    }
    public static void CallOnPlayerAnim(int value1)
    {
        SetPlayerAnim?.Invoke(value1);
    }

    public static void CallOnSelectionCG(int value1)
    {
        SetSelectionBG?.Invoke(value1);
    }

}
