using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPanelController : MonoBehaviour
{
    private void OnMouseDown()
    {
        
        if (GameManager.instance.IsBattle && GameManager.instance.IsClick && (int)GameManager.instance.OnClickType % 2 == 1)
        {
            EventManager.CallOnUseObj();
        }
    }
}
