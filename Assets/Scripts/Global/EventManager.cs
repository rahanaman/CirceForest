using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EventManager
{

    public delegate void Int1Event(int vlaue1);
    public delegate void Int2Event(int value1, int value2);
    public delegate void GO1Event(GameObject value1);
    public delegate void SoundIDEvent(DataBase.SoundID value1, float value2);
    public delegate void VoidEvent();
    public delegate void ListIntEvent(List<int> value);
    public delegate void StringEvent(string value);

    public static Int2Event SetPlayerHP;
    public static Int1Event SetPlayerAnim;
    public static Int1Event SetSelectionCG;
    public static Int1Event SetSelection;
    public static GO1Event SetCGID;
    public static GO1Event SetID;
    public static GO1Event SetHandCardList;
    public static GO1Event SetOnClickCard;
    public static SoundIDEvent SetEFXSoundID;
    public static ListIntEvent SetCardList;
    public static StringEvent SetPlayerDeckNum;
    public static Int1Event SetHandCard;



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
    
    public static void CallOnCGID(GameObject value1 = null)
    {
        SetCGID?.Invoke(value1);
    }
    public static void CallOnID(GameObject value1)
    {
        SetID?.Invoke(value1);
    }

    public static void CallOnSoundID(DataBase.SoundID value1 , float value2 = 0)
    {
        SetEFXSoundID?.Invoke(value1, value2);
    }


    public static void CallOnCardList(List<int> value) // 덱패널 열고 카드 확인
    {
        SetCardList?.Invoke(value);
    } 
    public static void CallOnPlayerDeckNum(string value) // 전체덱 장수
    {
        SetPlayerDeckNum?.Invoke(value);
    }

    public static void CallOnHandCard(int value) // 손패 정리
    {
        SetHandCard?.Invoke(value);
    }

    public static void CallOnHandCardList(GameObject value) // 손패 정리
    {
        SetHandCardList?.Invoke(value);
    }

    public static void CallOnOnClickCard(GameObject value=null) // 든카드
    {
        SetOnClickCard?.Invoke(value);
    }

}
