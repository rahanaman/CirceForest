using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneStartButtonController : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] bool _isclick;
    private void Start()
    {
        _button.onClick.AddListener(OnClick);
        _isclick = false;
    }
    private void OnMouseEnter()
    {
        if (!_isclick)
        {
            EventManager.CallOnCGID(_button.gameObject);
        }

    }

    private void OnMouseExit()
    {
        EventManager.CallOnSelectionCG(0);
    }

    private void OnClick()
    {
        EventManager.CallOnID(_button.gameObject);
    }
}
