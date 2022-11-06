using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager
{ 
    private bool _cardIsSub;
    public bool CardIsSub
    {
        get { return _cardIsSub; }
    }
    private int _cardId;
    public int CardId
    {
        get { return _cardId; }
    }

    public void SetCardData(CardData data)
    {
        _cardId = data.CardId;
        _cardIsSub = data.CardIsSub;
    }
}