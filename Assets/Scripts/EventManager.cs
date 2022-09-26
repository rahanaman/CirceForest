using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void Int1Event(int vlaue1);
    public delegate void Int2Event(int value1, int value2);
    public delegate void GO1Event(GameObject value1);
    public delegate void SoundIDEvent(DataBase.SoundID value1, float value2);


    public static Int2Event SetPlayerHP;
    public static Int1Event SetPlayerAnim;
    public static Int1Event SetSelectionCG;
    public static Int1Event SetSelection;
    public static GO1Event SetCGID;
    public static GO1Event SetID;
    public static SoundIDEvent SetEFXSoundID;



    public static void CallOnPlayerHP(int value1, int value2)
    {
        SetPlayerHP?.Invoke(value1, value2);
    }
    public static void CallOnPlayerAnim(int value1)
    {
        SetPlayerAnim?.Invoke(value1);
    }

    public static void CallOnSelectionCG(int value1)
    {
        SetSelectionCG?.Invoke(value1);
    }

    public static void CallOnSelection(int value1)
    {
        SetSelection?.Invoke(value1);
    }
    
    public static void CallOnCGID(GameObject value1)
    {
        SetCGID?.Invoke(value1);
    }
    public static void CallOnID(GameObject value1)
    {
        SetID?.Invoke(value1);
    }

    public static void CallOnID(DataBase.SoundID value1 , float value2 = 0)
    {
        SetEFXSoundID?.Invoke(value1, value2);
    }


}
