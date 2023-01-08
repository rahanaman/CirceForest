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

    public void DrawCard() // ī��̱�
    {
        var n = _unusedDeck.Count;
        if(n <= 0)
        {
            if(_usedDeck.Count <= 0)
            {
                // unusedcard�� 0 ���ϸ� �� �̴´ٴ� �޼��� �ʿ�!
            }
            else
            {
                Reset();
            }

        }
        if(_handDeck.Count > _handCardLimit)
        {
            //���� �ѵ� ������ ���⿡ ������ ��

        }
        else
        {
            var i = Random.Range(0, n);
            EventManager.CallOnHandCard(_unusedDeck[i]);
            _unusedDeck.Remove(_unusedDeck[i]);
        }
        
        
    }

    public void AddHandCardList(GameObject card) // ����Ʈ�� �ֱ�
    {
        _handDeck.Add(card);
        SetHandCard();
    }

    public void AddHandCard(GameObject data) //���� ī�� �߰�
    {
        EventManager.CallOnHandCard(data);

    }

    private void RemoveHandCard(GameObject card)
    {
        _handDeck.Remove(card);
        UseCardList(card);
        SetHandCard();
    }
    private void UseCardList(GameObject card) // ���п��� ī�� ���
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

    private int GetCost() // �̹� �� �ڽ�Ʈ ����
    {
        return 3;
    }

    private void UseCost(int value)
    {
        _currentCost -= value;
        EventManager.SetCost(_currentCost, _maxCost);
    }


}
