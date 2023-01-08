using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameSceneManager
{
    private int _handCardLimit = 8;
    private List<GameObject> _unusedDeck;
    public List<GameObject> UnusedDeck
    {
        get { return _unusedDeck; }
    }
    private List<GameObject> _handDeck;
    private List<GameObject> _usedDeck;
    public List<GameObject> UsedDeck
    {
        get { return _usedDeck; }
    }
    private List<GameObject> _excludedDeck;
    private int _turnNum;
    public int TurnNum
    {
        get { return _turnNum; }
    }

    private int _currentCost;
    private int _maxCost;
    public void InitBattleRoutine()
    {
        _unusedDeck = new List<GameObject>();
        _handDeck = new List<GameObject>();
        _usedDeck = new List<GameObject>();
        _excludedDeck = new List<GameObject>();
        _turnNum = 0;  
    }
    public void EndBattleRoutine()
    {

    }
    public void InitCardDeck(GameObject value)
    {
        _unusedDeck.Add(value);
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
            if(_usedDeck.Count <= 0)
            {
                // unusedcard도 0 이하면 못 뽑는다는 메세지 필요!
            }
            else
            {
                Reset();
            }

        }
        if(_handDeck.Count > _handCardLimit)
        {
            //손패 한도 있으면 여기에 정해질 듯

        }
        else
        {
            var i = Random.Range(0, n);
            EventManager.CallOnHandCard(_unusedDeck[i]);
            _unusedDeck.Remove(_unusedDeck[i]);
        }
        
        
    }

    public void AddHandCardList(GameObject card) // 리스트에 넣기
    {
        _handDeck.Add(card);
        SetHandCard();
    }

    public void AddHandCard(GameObject data) //손패 카드 추가
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
        _usedDeck.Add(card);
        card.SetActive(false);
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
        int cost = card.GetComponent<CardController>().GetCost();
        if(_currentCost < cost)
        {
            card.GetComponent<CardController>().FailToUse();
            return;
        }
        UseCost(cost);
        switch (id)
        {
            case 0:
                enemy.GetComponent<EnemyController>().GetDamage(DataBase.CardList[id].CardPatternData[0]);
                break;
        }
        RemoveHandCard(card);
        GameManager.instance.EmptyObj();
    }

    public void UseCard(GameObject card, GameObject player, GameObject[] enemys)
    {
        int id = card.GetComponent<CardController>().GetData();
        int cost = card.GetComponent<CardController>().GetCost();
        if (_currentCost < cost)
        {
            card.GetComponent<CardController>().FailToUse();
            return;
        }
        UseCost(cost);
        switch (id)
        {
            case 1:
                player.GetComponent<PlayerController>().GetDefence(DataBase.CardList[id].CardPatternData[0]);
                break;
        }
        RemoveHandCard(card);
        GameManager.instance.EmptyObj();

    }

    public void SetCost()
    {
        _maxCost = GetCost();
        _currentCost = _maxCost;
        EventManager.SetCost(_currentCost, _maxCost);
    }

    private int GetCost() // 이번 턴 코스트 연산
    {
        return 3;
    }

    private void UseCost(int value)
    {
        _currentCost -= value;
        EventManager.SetCost(_currentCost, _maxCost);
    }


}
