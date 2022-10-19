using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSceneView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _��ü��ButtonText;
    void Start()
    {
        EventManager.Set��ü�� += Set��ü��Num;
    }

    private void OnDestroy()
    {
        EventManager.Set��ü�� -= Set��ü��Num;
    }
    private void Set��ü��Num(string value)
    {
        _��ü��ButtonText.text = value;
        EventManager.Set��ü�� -= Set��ü��Num;
    }
}
