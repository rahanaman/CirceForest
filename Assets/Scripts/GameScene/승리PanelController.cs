using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class 승리PanelController : MonoBehaviour
{

    [SerializeField] Button _Button;
    void Start()
    {
        _Button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        EventManager.CallOnEndBattle();
        gameObject.SetActive(false);
    }
}
