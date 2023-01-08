using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSceneView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerDeckButtonText;
    [SerializeField] GameObject _handCardPanel;
    [SerializeField] TextMeshProUGUI _costText;

    void Start()
    {
        EventManager.SetPlayerDeckNum += SetPlayerDeckNum;
        EventManager.SetHandCard += AddHandCard;
        EventManager.SetCost += SetCost;
    }

    private void OnDestroy()
    {
        EventManager.SetPlayerDeckNum -= SetPlayerDeckNum;
        EventManager.SetHandCard -= AddHandCard;
        EventManager.SetCost -= SetCost;
    }
    private void SetPlayerDeckNum(string value)
    {
        _playerDeckButtonText.text = value;
    }

    private void SetCost(int currentCost, int maxCost)
    {
        _costText.text = currentCost.ToString()+'/'+maxCost.ToString();
    }



    private void AddHandCard(GameObject card)
    {
        
        card.SetActive(true);
        card.transform.parent = _handCardPanel.transform;
        card.transform.SetAsFirstSibling();
        EventManager.CallOnHandCardList(card);
    }

    public void UseHandCard(GameObject card)
    {
    }

}
