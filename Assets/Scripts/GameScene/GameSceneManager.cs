using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameSceneManager
{
    private List<int> _unusedDeck;
    public List<int> UnusedDeck
    {
        get { return _unusedDeck; }
    }
    private List<GameObject> _handDeck;
    private List<int> _usedDeck;
    private List<int> _excludedDeck;
    private int _turnNum;

    public void Init()
    {
        _unusedDeck = DataManager.PlayerDeck.ToList();
        _handDeck = new List<GameObject>();
        _usedDeck = new List<int>();
        _excludedDeck = new List<int>();
        _turnNum = 0;  
    }

    public void Reset()
    {
        _unusedDeck = _usedDeck.ToList();
        _usedDeck.Clear();
    }

    public void DrawCard()
    {
        var n = _unusedDeck.Count;
        var i = Random.Range(0, n);
        EventManager.CallOnHandCard(_unusedDeck[i]);
        //View
        _unusedDeck.Remove(_unusedDeck[i]);

    }

    public void AddHandCardList(GameObject card)
    {
        _handDeck.Add(card);
        SetHandCard();
    }
    public void AddHandCard(int data)
    {
        EventManager.CallOnHandCard(data);

    }
    public void UseCard(int data)
    {
        _usedDeck.Add(data);
    }
    public void ExcludeCardFromUnused(int data)
    {

    }

    public void ExcludeCardFromHand(int data)
    {

    }

    public int GetDrawCardNum()
    {
        if (_turnNum == 0)
        {
            return 4;
        }
        return 1;
    }
    private void SetHandCard()
    {
        Vector2 pos = new Vector2();
        Vector3 rot = new Vector3();
        Vector3 scale = new Vector3();
        int n = _handDeck.Count;
        int x = 110 - 5 * n;
        pos.x = (x * n) - 25;
        pos.y = -425 - (15 * n);
        rot.z = -2 * (n - 1);
        scale.x = 1.02f - 0.02f * n;
        scale.y = 1.02f - 0.02f * n;
        scale.z = 1.02f - 0.02f * n;
        for (int i = 0; i < n; i++)
        {
            _handDeck[i].transform.localPosition = pos;
            _handDeck[i].transform.rotation = Quaternion.Euler(rot);
            _handDeck[i].transform.localScale = scale;
            _handDeck[i].GetComponent<CardController>().SetPosRot();
            pos.x -= 2 * x;
            rot.z += 4;
            if (i < (n - 1) / 2)
            {
                pos.y += 30;
            }
            if (i >= (n / 2) && i != (n - 1))
            {
                pos.y -= 30;
            }
        }
        for (int i = 0; i < n; i++)
            _handDeck[i].GetComponent<CardController>().SetIndex();

    }
}
