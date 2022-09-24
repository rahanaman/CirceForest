using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneStartButtonController : MonoBehaviour
{
    [SerializeField] private Button _button;
    private void Start()
    {
        _button.onClick.AddListener(OnClick);
    }
    private void OnMouseEnter()
    {
        Debug.Log(_button.gameObject.name);
        EventManager.CallOnSelectionCGID(_button.gameObject);

    }

    private void OnMouseExit()
    {
        EventManager.CallOnSelectionCG(0);
    }

    private void OnClick()
    {
        EventManager.CallOnSelectionID(_button.gameObject);
    }
}
