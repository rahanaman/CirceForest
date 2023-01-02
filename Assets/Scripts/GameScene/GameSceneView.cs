using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSceneView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerDeckButtonText;
    [SerializeField] GameObject _handCardPanel;

    void Start()
    {
        EventManager.SetPlayerDeckNum += SetPlayerDeckNum;
        EventManager.SetHandCard += AddHandCard;
    }

    private void OnDestroy()
    {
        EventManager.SetPlayerDeckNum -= SetPlayerDeckNum;
        EventManager.SetHandCard -= AddHandCard;
    }
    private void SetPlayerDeckNum(string value)
    {
        _playerDeckButtonText.text = value;
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
