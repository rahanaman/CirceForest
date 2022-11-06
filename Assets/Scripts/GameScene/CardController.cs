using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    CardManager _cardManager = new CardManager();
    [SerializeField]private GameObject _efx;
    private int _index;
    private Vector3 _pos;
    private Quaternion _rot;
    public void SetCard(CardData data)
    {
        _cardManager.SetCardData(data);
    }

    private void OnMouseEnter()
    {
        if (!GameManager.instance.IsCardClick && !GameManager.instance.IsCardPanel)
        {
            Vector3 chpos = _pos;
            chpos.y = -400;
            
            transform.localPosition = chpos;
            //transform.rotation = Quaternion.identity;
            transform.SetAsLastSibling();
            _efx.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (!GameManager.instance.IsCardClick && !GameManager.instance.IsCardPanel)
        {
            transform.localPosition = _pos;
            //transform.rotation = _rot;
            _efx.SetActive(false);
            transform.SetSiblingIndex(_index);
            //_cardCon.transform.localScale = _scale;
        }
    }

    private void OnMouseDown()
    {
        if (!GameManager.instance.IsCardPanel)
        {
            if (!GameManager.instance.IsCardClick)
            {
                GameManager.instance.IsCardClick = true;
                transform.rotation = Quaternion.identity;
                EventManager.CallOnOnClickCard(gameObject);
                GameManager.instance.SetCursorCard();
            }
            else
            {
                GameManager.instance.IsCardClick = false;
                transform.localPosition = _pos;
                transform.rotation = _rot;
                _efx.SetActive(false);
                transform.SetSiblingIndex(_index);
                GameManager.instance.SetCursorIdle();
            }
        }
        
    }

    public void SetPosRot()
    {
        _pos = transform.localPosition;
        _rot = transform.rotation;
        
    }

    public void SetIndex()
    {
        _index = transform.GetSiblingIndex();
    }


}
