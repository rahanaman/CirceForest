using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSceneView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _ÀüÃ¼µ¦ButtonText;
    void Start()
    {
        EventManager.SetÀüÃ¼µ¦ += SetÀüÃ¼µ¦Num;
    }

    private void OnDestroy()
    {
        EventManager.SetÀüÃ¼µ¦ -= SetÀüÃ¼µ¦Num;
    }
    private void SetÀüÃ¼µ¦Num(string value)
    {
        _ÀüÃ¼µ¦ButtonText.text = value;
        EventManager.SetÀüÃ¼µ¦ -= SetÀüÃ¼µ¦Num;
    }
}
