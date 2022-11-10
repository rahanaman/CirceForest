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
    public int TurnNum
    {
        get { return _turnNum; }
    }
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

    public void DrawCard() // 카드뽑기
    {
        var n = _unusedDeck.Count;
        if(n <= 0)
        {

        }
        var i = Random.Range(0, n);
        EventManager.CallOnHandCard(_unusedDeck[i]);
        //View
        _unusedDeck.Remove(_unusedDeck[i]);
    }

    public void AddHandCardList(GameObject card) // 리스트에 넣기
    {
        _handDeck.Add(card);
        SetHandCard();
    }

    public void AddHandCard(int data) //손패 카드 추가
    {
        EventManager.CallOnHandCard(data);

    }

    private void RemoveHandCard(GameObject card)
    {
        _handDeck.Remove(card);
        UseCardList(card);
        SetHandCard();
    }
    private void UseCardList(GameObject card) // 손패에서 카드 사용
    {
        _usedDeck.Add(card.GetComponent<CardController>().GetData());
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
            _handDeck[i].GetComponent<CardController>().SetIndex(n-i-1);
        }

    }

    public void AddTurnNum()
    {
        _turnNum++;
    }

    public void UseTargetingCard(GameObject card, GameObject player, GameObject enemy)
    {
        int id = card.GetComponent<CardController>().GetData();
        switch (id)
        {
            case 0:
                enemy.GetComponent<EnemyController>().GetDamage(DataBase.CardList[id].CardPatternData[0]);
                break;
        }
        RemoveHandCard(card);
        GameObject.Destroy(card);
        GameManager.instance.EmptyObj();
    }

    public void UseCard(GameObject card, GameObject player, GameObject[] enemys)
    {
        int id = card.GetComponent<CardController>().GetData();
        switch (id)
        {
            case 1:
                player.GetComponent<PlayerController>().GetDefence(DataBase.CardList[id].CardPatternData[0]);
                break;
        }
        RemoveHandCard(card);
        GameObject.Destroy(card);
        GameManager.instance.EmptyObj();

    }


}
