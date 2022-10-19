using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private bool _cardIsSub;
    private int _cardId;
    //public bool IsSet;
    void Start()
    {
        //IsSet = false;
        //EventManager.SetCard += SetCardData;
        //IsSet = true;
    }

    // Update is called once per frame
    public void SetCardData(CardData data)
    {
        _cardId = data.CardId;
        _cardIsSub = data.CardIsSub;
    }
}
